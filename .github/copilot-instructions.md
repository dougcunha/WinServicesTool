# WinServicesTool - AI Agent Instructions

## Architecture Overview

This is a **Windows Forms (.NET 10.0)** desktop application for managing Windows services with admin privileges. Key architectural decisions:

- **Dependency Injection**: Uses `Microsoft.Extensions.DependencyInjection` configured in `Program.cs`. All major components (forms, services, config) are registered as singletons.
- **Services Layer**: Business logic isolated in `Services/` folder with interfaces (`IWindowsServiceManager`, `IPrivilegeService`). This enables testability and separation of concerns.
- **Configuration**: `AppConfig` class uses `INotifyPropertyChanged` (via Fody) with **two-way databinding** to UI controls. Config persists to `app_config.json` in current directory (not AppData).
- **Logging**: NLog configured via `nlog.config`. All logs go to `Logs/` directory with daily rotation.

## Critical Patterns

### 1. Configuration Management (AppConfig)
```csharp
// Config is a singleton with PropertyChanged.Fody
public sealed partial class AppConfig : INotifyPropertyChanged
{
    public bool AutoWidthColumns { get; set; }
    public void Save() { /* JSON to app_config.json */ }
}

// In forms: use databinding, NOT manual event handlers
ChkAutoWidth.DataBindings.Add("Checked", _appConfig, nameof(AppConfig.AutoWidthColumns), ...);
_appConfig.PropertyChanged += (s, e) => _appConfig.Save(); // Auto-save on any change
```
**Why**: Manual event handlers were causing double-saves. Databinding + PropertyChanged is the canonical approach here.

### 2. Grid Context Menus (ContextMenuStrip)
```csharp
// DO NOT use 'using' or manual Dispose - causes ObjectDisposedException
var menu = new ContextMenuStrip();
// ... build menu items ...
menu.Show(GridServs, new Point(e.X, e.Y));
// Let GC handle disposal - WinForms manages internal references
```
**Why**: The framework can access the menu after the event handler returns. Disposing prematurely crashes with `ObjectDisposedException`.

### 3. Service Operations Pattern
All service operations (start/stop/restart) follow this pattern:
1. Check `_privilegeService.IsAdministrator()` first
2. Validate selection (status must match operation)
3. Show confirmation dialog
4. Disable button, run async operation in `Task.Run()`
5. Update `Service` model properties after success
6. Re-enable button in `finally`

See `BtnStart_Click` in `FormMain.cs` for canonical example.

### 4. Context Menu for Services
Right-click context menu on service grid shows dynamic options based on service status:
- **Start**: Only when all selected services are stopped
- **Stop/Restart**: Only when any selected services are running/paused
- **Change Start Mode**: Always available
- **Go to Registry**: Only for single service selection, opens regedit at service key

Menu uses existing button handlers for consistency. Registry navigation sets `HKCU\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit\LastKey` then opens regedit.exe.

### 4. Column Widths Persistence
- `ColumnWidthStore` saves to `column_widths.json` (separate from AppConfig)
- Saved on every `ColumnWidthChanged` event AND form close
- Loaded once after `BtnLoad_Click` populates services
- When setting widths programmatically, set `col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None` to prevent auto-resize

## Development Workflows

### Build and Run
```powershell
# Debug build (requires admin to test service operations)
dotnet build WinServicesTool/WinServicesTool.csproj -c Debug
Start-Process -FilePath "WinServicesTool/bin/Debug/net10.0-windows/WinServicesTool.exe" -Verb RunAs

# Release build
dotnet build -c Release
```

### Key Files to Modify

| Task | Files |
|------|-------|
| Add new config option | `Utils/AppConfig.cs` + bind in `FormMain.cs` constructor |
| Add service operation | `Services/IWindowsServiceManager.cs` + implementation |
| Add UI control | `Forms/FormMain.Designer.cs` (designer) + wire in `FormMain.cs` |
| Add context menu option | `FormMain.cs` GridServs_MouseUp method |
| Change logging | `nlog.config` |

### Code Style (enforced by `.github/instructions/code style.instructions.md`)
- **Sealed by default**: All non-inheritable classes must be `sealed`
- **XMLDoc required**: Public classes and methods must have XML documentation
- **Blank lines**: Before `if`, `for`, `return`; between methods
- **Modern syntax**: Use `[]` for collections, `var` when type is obvious
- **Databinding**: Prefer databinding over manual event handlers for config

## Integration Points

### Windows Registry (Service Start Mode)
Changing service start mode uses registry directly (not ServiceController API):
```csharp
using var key = Registry.LocalMachine.OpenSubKey($"SYSTEM\\CurrentControlSet\\Services\\{serviceName}", writable: true);
key.SetValue("Start", startValue, RegistryValueKind.DWord);
// 2=Automatic, 3=Manual, 4=Disabled
```

### Registry Navigation
Opening regedit at specific service key:
```csharp
// Set LastKey for regedit to open at correct location
using var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit");
key.SetValue("LastKey", $"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\{serviceName}");
// Small delay ensures registry write completes
Thread.Sleep(100);
Process.Start("regedit.exe");
```

### Elevation (UAC)
`PrivilegeService` handles elevation restart:
```csharp
ProcessStartInfo psi = new() {
    FileName = exePath,
    UseShellExecute = true,
    Verb = "runas" // Triggers UAC prompt
};
```

## Common Gotchas

1. **ContextMenuStrip disposal**: Never use `using` or attach `Closed` event to Dispose. Let GC handle it.
2. **Config file location**: `app_config.json` is in current directory (not AppData), so it travels with the exe.
3. **GridServs events**: `ColumnWidthChanged` fires on *every* width change, including programmatic. Use debouncing if needed.
4. **Service refresh**: After operations, manually update `Service.Status` in the model - grid doesn't auto-refresh from Windows.
5. **Fody weaving**: `PropertyChanged.Fody` auto-implements `INotifyPropertyChanged`. Don't manually raise events for auto-properties.
6. **Registry navigation**: Use `LastKey` in `HKCU\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit` + small delay before opening regedit.

## Testing Strategy

Currently no automated tests. When adding:
- Use **xUnit**, **Shouldly**, **NSubstitute** (per code style guide)
- Mock `IWindowsServiceManager` and `IPrivilegeService` for form tests
- Test classes must be `public sealed` with `[ExcludeFromCodeCoverage]`
- AAA pattern: Arrange, Act, Assert

## Credits

- **NLog**: Logging framework
- **Fody/PropertyChanged.Fody**: Auto-implements INotifyPropertyChanged
- **System.ServiceProcess.ServiceController**: Windows service enumeration/control
