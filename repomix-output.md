This file is a merged representation of the entire codebase, combined into a single document by Repomix.
The content has been processed where empty lines have been removed, content has been compressed (code blocks are separated by ⋮---- delimiter).

# File Summary

## Purpose
This file contains a packed representation of the entire repository's contents.
It is designed to be easily consumable by AI systems for analysis, code review,
or other automated processes.

## File Format
The content is organized as follows:
1. This summary section
2. Repository information
3. Directory structure
4. Repository files (if enabled)
5. Multiple file entries, each consisting of:
  a. A header with the file path (## File: path/to/file)
  b. The full contents of the file in a code block

## Usage Guidelines
- This file should be treated as read-only. Any changes should be made to the
  original repository files, not this packed version.
- When processing this file, use the file path to distinguish
  between different files in the repository.
- Be aware that this file may contain sensitive information. Handle it with
  the same level of security as you would the original repository.

## Notes
- Some files may have been excluded based on .gitignore rules and Repomix's configuration
- Binary files are not included in this packed representation. Please refer to the Repository Structure section for a complete list of file paths, including binary files
- Files matching patterns in .gitignore are excluded
- Files matching default ignore patterns are excluded
- Empty lines have been removed from all files
- Content has been compressed - code blocks are separated by ⋮---- delimiter
- Files are sorted by Git change count (files with more changes are at the bottom)

# Directory Structure
```
README.md
TODO.md
WinServicesTool.slnx
WinServicesTool/Extensions/ControlExtensions.cs
WinServicesTool/FodyWeavers.xml
WinServicesTool/Forms/FormChangeStartMode.cs
WinServicesTool/Forms/FormMain.cs
WinServicesTool/Forms/FormMain.Designer.cs
WinServicesTool/Models/Service.cs
WinServicesTool/Models/ServiceTypeEx.cs
WinServicesTool/nlog.config
WinServicesTool/Program.cs
WinServicesTool/Properties/DataSources/WinServicesTool.Models.Service.datasource
WinServicesTool/Services/IPrivilegeService.cs
WinServicesTool/Services/IWindowsServiceManager.cs
WinServicesTool/Services/PrivilegeService.cs
WinServicesTool/Services/ServiceNativeHelper.cs
WinServicesTool/Services/WindowsServiceManager.cs
WinServicesTool/Utils/AppConfig.cs
WinServicesTool/Utils/ColumnWidthStore.cs
WinServicesTool/WinServicesTool.csproj
```

# Files

## File: TODO.md
````markdown
# TODO - WinServicesTool

This file lists features and implementation tasks derived from the project's README. Use it to track development progress.

> Status: working draft. Update tasks, priorities and assignees as work progresses.

---

## High level roadmap

1. Core service discovery and UI
2. Service control (start/stop/restart) with secure handling
3. Filtering, sorting and search
4. Saved services (Favorites) with export/import
5. Editing and removal of services (where permitted)
6. Logging, CI and packaging

---

## Tasks (feature-driven)

### 1) Service listing (Core)

- [x] Implement service enumeration using `System.ServiceProcess.ServiceController`
- [x] Show essential columns: Service Name, Display Name, Status, Startup Type
- [ ] Paging/virtualization for large lists (performance)
- [x] Auto-adjust DataGridView column widths to use available monitor space (Fill and AllCells mode)
- [x] Allow manual column resize and persist column widths between sessions (user settings or JSON)
- Priority: High

### 3) Filter, Search and Sort

- [X] Text filter by Service Name or Display Name (contains / starts-with)
- [X] Status filter (Running, Stopped, Paused, All)
- [X] Sort by Name, Status, Startup Type (clickable column headers)
- [ ] Preserve sorting/filter selections between app sessions (optional)
- Priority: High

### 4) Edit service properties

- [ ] Allow editing Display Name where permitted
- [ ] Allow changing Startup Type (Automatic, Manual, Disabled) where permitted
- [ ] Validate changes and show confirmations
- Priority: Medium

### 5) Remove / Uninstall service

- [ ] Provide a controlled flow to uninstall/remove a service (with multiple confirmations)
- [ ] Check permissions and refuse action when not allowed
- [ ] Optionally backup current configuration before removal
- Priority: Low (dangerous)

### 6) Saved Services (Favorites)

- [ ] Add UI to add/remove services to a saved/favorites list
- [ ] Add a dedicated tab that displays only saved services
- [ ] Persist saved lists locally (JSON serialization)
- [ ] Support multiple named lists (profiles)
- Priority: High

### 7) Export / Import saved lists

- [ ] Export favorites list to JSON file (with simple schema)
- [ ] Import favorites list and validate entries (skip missing services)
- [ ] Provide an import preview and undo option
- Priority: Medium

### 8) Logging & Diagnostics

- [X] Improve NLog configuration and include a UI-accessible log viewer
- Priority: Medium

### 9) Permissions & Elevation UX

- [X] Detect whether the app is running elevated
- [X] Present a clear indication when running without admin rights
- [X] Provide quick guidance / button to restart with elevation
- Priority: High

### 10) CI / Packaging / Releases

- [ ] Add GitHub Actions to build on push and PR
- [ ] Produce release artifacts (zip) and optionally installer
- [ ] Tagging and release workflow configured
- Priority: Medium

### 11) Localization

- [ ] Prepare UI for localization (resource files)
- [ ] Add PT-BR and EN translations for core UI strings
- Priority: Low

---

## File format and conventions

- Saved lists: JSON array of objects: { "ServiceName": "", "DisplayName": "", "Notes": "" }
- Tests should avoid requiring elevation; mock System.ServiceProcess where possible.

---

## Example implementation milestones

- Milestone 1 (MVP): Tasks 1, 2, 3, 6, 9
- Milestone 2: Tasks 4, 7, 8, 10
- Milestone 3: Tasks 11

---

## How to update this TODO

Edit this file and use checkboxes to mark progress. Add small, focused subtasks and reference PR numbers when tasks are completed.
````

## File: WinServicesTool.slnx
````
<Solution>
  <Project Path="WinServicesTool/WinServicesTool.csproj" />
</Solution>
````

## File: WinServicesTool/Extensions/ControlExtensions.cs
````csharp
/// <summary>
/// Helper extension methods for Control to simplify thread-safe UI actions.
/// </summary>
internal static class ControlExtensions
⋮----
/// Invokes the given action on the control's thread if required.
⋮----
public static void InvokeIfRequired(this Control? control, Action action)
⋮----
control.Invoke(action);
````

## File: WinServicesTool/FodyWeavers.xml
````xml
<Weavers xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="FodyWeavers.xsd">
  <PropertyChanged />
</Weavers>
````

## File: WinServicesTool/nlog.config
````
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <targets>
    <target name="file" xsi:type="File" fileName="${basedir}/Logs/${shortdate}.logc"
      layout="${date:format=yyyy-MM-dd HH\:mm\:ss.fff}|${logger}|${level:uppercase=true}|${threadid}|${message}${onexception:|${exception:format=tostring}}|"
  />
  </targets>
  <rules>
    <!--<logger name="*" levels="Info,Error,Debug,Warn,Trace,Fail" writeTo="console" />-->
    <logger name="*" levels="Info,Debug,Error" writeTo="file" />
    <!-- <logger name="*" levels="Error" writeTo="email" /> -->
  </rules>
</nlog>
````

## File: WinServicesTool/Properties/DataSources/WinServicesTool.Models.Service.datasource
````
<?xml version="1.0" encoding="utf-8"?>
<!--
    This file is automatically generated by Visual Studio. It is
    used to store generic object data source configuration information.
    Renaming the file extension or editing the content of this file may
    cause the file to be unrecognizable by the program.
-->
<GenericObjectDataSource DisplayName="Service" Version="1.0" xmlns="urn:schemas-microsoft-com:xml-msdatasource">
  <TypeInfo>WinServicesTool.Models.Service, WinServicesTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null</TypeInfo>
</GenericObjectDataSource>
````

## File: WinServicesTool/Services/IWindowsServiceManager.cs
````csharp
/// <summary>
/// Abstraction for operations that enumerate and manipulate Windows services.
/// </summary>
public interface IWindowsServiceManager
⋮----
/// Enumerates known services and returns a list of lightweight <see cref="Models.Service"/> models.
⋮----
Task<List<Models.Service>> GetServicesAsync();
⋮----
/// Starts the specified service by service name and waits until the service is running or timeout.
⋮----
Task StartServiceAsync(string serviceName);
⋮----
/// Stops the specified service and waits until stopped or timeout.
⋮----
Task StopServiceAsync(string serviceName);
````

## File: README.md
````markdown
# WinServicesTool

A Windows Services manager tool to easily view and control Windows services (start, stop, restart). Designed as a lightweight GUI utility for power users and administrators who need quick control and organization of services on Windows machines.

## Table of Contents

- [Features](#features)
- [Screenshots](#screenshots)
- [Requirements](#requirements)
- [Installation](#installation)
- [Usage](#usage)
- [Saved Services List (Favorites)](#saved-services-list-favorites)
- [Permissions](#permissions)
- [Development](#development)
- [Contributing](#contributing)
- [License](#license)

## Features

- List all installed Windows services with their current status.
- Start, Stop and Restart services from the UI.
- Filter and search services by name, display name or status.
- Sort services by name, status or startup type.
- Edit service properties (display name, startup type) where supported.
- Remove/uninstall entries (where permitted by Windows and the current user privileges).
- Save a custom list of services (favorites) to quickly access important services in a dedicated tab.
- Export/Import saved lists for sharing or backups.
- Simple, responsive UI with logging support (NLog) for troubleshooting.

## Screenshots

Add screenshots here to illustrate the app. Example suggestions:

- Main services list with filters
- Favorites / Saved Services tab
- Start/Stop confirmation dialog

You can place images in the repository (e.g. `docs/screenshots/`) and reference them here.

## Requirements

- Windows 10 or newer (desktop) with .NET 10.0 runtime installed.
- Administrator privileges to control services (start/stop/restart/edit/remove).
- Optional: Visual Studio 2022/2026 or .NET SDK to build from source.

## Installation

There are two main ways to run WinServicesTool:

1. Download a prebuilt binary (from Releases) and run the executable.
2. Build from source.

Build from source

1. Clone the repository:

    ```powershell
    git clone https://github.com/dougcunha/WinServicesTool.git
    cd WinServicesTool
    ```

2. Open the solution `WinServicesTool.slnx` in Visual Studio or build with the .NET SDK:

    ```powershell
    dotnet build WinServicesTool/WinServicesTool.csproj -c Release
    ```

3. The compiled executable will be under `WinServicesTool/bin/Debug/net10.0-windows/` (or `Release`).

## Usage

1. Run the `WinServicesTool.exe` as administrator:

    ```powershell
    # Right-click the executable and choose "Run as administrator" or
    Start-Process -FilePath .\WinServicesTool.exe -Verb RunAs
    ```

2. Browse the list of services.
3. Use the filter box to find services by name or status.
4. Select a service and use the Start / Stop / Restart buttons.
5. To persist a set of services, add them to the "Saved Services" tab (Favorites).

Notes

- Some operations (like uninstalling or changing startup type) may be blocked by Windows or require elevated permissions.
- The app uses `System.ServiceProcess.ServiceController` and standard Windows APIs — behavior follows Windows service security rules.

## Saved Services List (Favorites)

You can create and maintain a list of frequently used services. Features:

- Add / Remove services from your saved list.
- Quickly switch to the saved list tab to view only those services.
- Export saved lists to a file for backup or sharing.
- Import saved lists to restore or load a friend's / team list.

File format for Export/Import

- The app serializes the saved list into a simple JSON file with service names and optional metadata.

## Permissions

The tool requires administrator rights to perform most actions on services. Without elevated permissions the app will still list services but will be limited to read-only operations for many services.

Tips to run as admin:

- Right-click the .exe and choose "Run as administrator".
- Use PowerShell to start with elevation: `Start-Process -FilePath .\WinServicesTool.exe -Verb RunAs`.

## Development

Project details:

- Language: C#
- Framework: .NET 10.0 (Windows Desktop)
- Logging: NLog

Open the solution in Visual Studio and run.

Recommended tasks

- Run the project from Visual Studio with Administrator privileges when testing service control features.
- Modify `nlog.config` to adjust logging verbosity and targets.

Building and running from command-line

```powershell
dotnet build WinServicesTool/WinServicesTool.csproj -c Debug
Start-Process -FilePath "WinServicesTool/bin/Debug/net10.0-windows/WinServicesTool.exe" -Verb RunAs
```

## Contributing

Contributions are welcome. Please follow these guidelines:

1. Fork the repository and create a feature branch.
2. Keep commits small and focused.
3. Add tests where feasible and run them locally.
4. Open a pull request describing the change and why it's needed.

Suggested improvements

- Add unit/integration tests for non-UI logic.
- Add automated packaging and CI (GitHub Actions) to build releases.
- Add localization support for multiple languages.

Code of Conduct

This project follows a standard open-source code of conduct. Be respectful and inclusive.

## Security

- The application uses Windows service APIs and requires admin rights for control operations; never run untrusted builds with elevated privileges.
- Report security issues via the repository's issue tracker and avoid disclosing them publicly until fixed.

## License

This project is licensed under the terms in `LICENSE.txt`.
````

## File: WinServicesTool/Models/ServiceTypeEx.cs
````csharp
public static class ServiceTypeHelper
⋮----
public static string Describe(int rawType)
⋮----
foreach (ServiceTypeEx flag in Enum.GetValues(typeof(ServiceTypeEx)))
⋮----
if (type.HasFlag(flag))
parts.Add(flag.ToString());
⋮----
? string.Join(" + ", parts)
````

## File: WinServicesTool/Services/IPrivilegeService.cs
````csharp
/// <summary>
/// Abstraction for privilege and elevation related operations.
/// </summary>
public interface IPrivilegeService
⋮----
/// Checks if the current process is running as administrator.
⋮----
bool IsAdministrator();
⋮----
/// Prompt the user to restart the application as administrator and attempt to relaunch.
/// Returns true if the relaunch was initiated.
⋮----
void AskAndRestartAsAdmin(Form? owner, bool shouldAsk);
````

## File: WinServicesTool/Services/PrivilegeService.cs
````csharp
public sealed class PrivilegeService : IPrivilegeService
⋮----
public bool IsAdministrator()
⋮----
using var identity = WindowsIdentity.GetCurrent();
var principal = new WindowsPrincipal(identity);
return principal.IsInRole(WindowsBuiltInRole.Administrator);
⋮----
_logger.LogWarning(ex, "Failed to determine administrator status");
⋮----
public void AskAndRestartAsAdmin(Form? owner, bool alwaysStartAsAdm)
⋮----
var resp = MessageBox.Show
⋮----
var psi = new ProcessStartInfo
⋮----
FileName = Process.GetCurrentProcess().MainModule?.FileName ?? Application.ExecutablePath,
⋮----
Process.Start(psi);
Application.Exit();
⋮----
_logger.LogError(ex, "Failed to relaunch elevated");
MessageBox.Show(owner, "Unable to start the application with elevated privileges.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
````

## File: WinServicesTool/Services/ServiceNativeHelper.cs
````csharp
/// <summary>
/// Helper class to query Windows service executable paths using native API.
/// Implements IDisposable to allow reusing the SCManager connection across multiple queries.
/// </summary>
public sealed class ServicePathHelper : IDisposable
⋮----
static extern IntPtr OpenSCManager(string? machineName, string? databaseName, uint dwAccess);
⋮----
static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);
⋮----
static extern bool QueryServiceConfig(IntPtr hService, IntPtr lpServiceConfig, uint cbBufSize, out uint pcbBytesNeeded);
⋮----
static extern bool CloseServiceHandle(IntPtr hSCObject);
⋮----
public IntPtr lpBinaryPathName;
public IntPtr lpLoadOrderGroup;
⋮----
public IntPtr lpDependencies;
public IntPtr lpServiceStartName;
public IntPtr lpDisplayName;
⋮----
private readonly IntPtr _scmHandle;
⋮----
/// Initializes a new instance of the <see cref="ServicePathHelper"/> class and opens the SCManager connection.
⋮----
/// <exception cref="System.ComponentModel.Win32Exception">Thrown when OpenSCManager fails.</exception>
⋮----
throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "OpenSCManager failed");
⋮----
/// Gets the executable path for the specified Windows service.
⋮----
/// <param name="serviceName">The name of the service.</param>
/// <returns>The full executable path with environment variables expanded, or null if not found.</returns>
/// <exception cref="ObjectDisposedException">Thrown when the helper has been disposed.</exception>
/// <exception cref="System.ComponentModel.Win32Exception">Thrown when the service cannot be opened or queried.</exception>
public string? GetExecutablePath(string serviceName)
⋮----
ObjectDisposedException.ThrowIf(_disposed, this);
IntPtr service = IntPtr.Zero;
⋮----
throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), $"OpenService failed for {serviceName}");
⋮----
if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
⋮----
IntPtr buffer = Marshal.AllocHGlobal((int)bytesNeeded);
⋮----
throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), $"QueryServiceConfig failed for {serviceName}");
⋮----
var path = Marshal.PtrToStringUni(config.lpBinaryPathName);
return Environment.ExpandEnvironmentVariables(path ?? "");
⋮----
Marshal.FreeHGlobal(buffer);
⋮----
/// Releases the SCManager handle.
⋮----
public void Dispose()
````

## File: WinServicesTool/Utils/AppConfig.cs
````csharp
public sealed partial class AppConfig : INotifyPropertyChanged
⋮----
/// <summary>
/// Gets or sets a value indicating whether the main grid should automatically adjust column widths.
/// </summary>
⋮----
/// Gets or sets a value indicating whether the Path column should be shown in the services grid.
⋮----
/// Gets or sets a value indicating whether the application should always start with administrator privileges.
⋮----
private static readonly string _filePath = Path.Combine(_appFolder, "app_config.json");
public static AppConfig Load()
⋮----
if (!File.Exists(_filePath))
return new AppConfig();
var json = File.ReadAllText(_filePath);
⋮----
return cfg ?? new AppConfig();
⋮----
public void Save()
⋮----
Directory.CreateDirectory(_appFolder);
var options = new JsonSerializerOptions { WriteIndented = true };
File.WriteAllText(_filePath, JsonSerializer.Serialize(this, options));
⋮----
// non-critical
````

## File: WinServicesTool/Forms/FormChangeStartMode.cs
````csharp
public sealed class FormChangeStartMode : Form
⋮----
private void InitializeComponent(string initialMode)
⋮----
ClientSize = new Size(320, 120);
var lbl = new Label { Left = 12, Top = 12, Width = 296, Text = "Select new start type:" };
_cmb = new ComboBox { Left = 12, Top = 36, Width = 296, DropDownStyle = ComboBoxStyle.DropDownList };
_cmb.Items.AddRange(["Automatic", "Manual", "Disabled"]);
// Try to select the initial mode if present
var idx = _cmb.Items.IndexOf(initialMode);
⋮----
_cmb.SelectedIndex = 1; // default Manual
var btnOk = new Button { Text = "OK", Left = 148, Width = 80, Top = 76, DialogResult = DialogResult.OK };
var btnCancel = new Button { Text = "Cancel", Left = 236, Width = 80, Top = 76, DialogResult = DialogResult.Cancel };
⋮----
Controls.Add(lbl);
Controls.Add(_cmb);
Controls.Add(btnOk);
Controls.Add(btnCancel);
````

## File: WinServicesTool/Models/Service.cs
````csharp
/// <summary>
/// Represents metadata and runtime state for a Windows service.
/// </summary>
/// <remarks>
/// This model contains identifying information (display and service names),
/// configuration details (start mode and service type), and runtime state
/// (current status and capability flags). The class is partial and sealed,
/// and it implements <see cref="INotifyPropertyChanged"/> so consumers may
/// observe property changes when the other partial declaration raises the
/// <c>PropertyChanged</c> event.
/// </remarks>
public sealed partial class Service : INotifyPropertyChanged
⋮----
/// Gets or sets the path to the service executable, if known.
⋮----
/// Gets or sets the friendly display name of the service as shown in the Services MMC.
⋮----
/// <value>The localized display name of the service.</value>
⋮----
/// Gets or sets the internal service name (the short name) used by the Service Control Manager.
⋮----
/// <value>The unique service name.</value>
⋮----
/// Gets or sets the current runtime status of the service.
⋮----
/// <value>
/// A <see cref="ServiceControllerStatus"/> value indicating whether the service is running,
/// stopped, paused, starting, stopping, etc.
/// </value>
⋮----
/// Gets or sets the start mode of the service.
⋮----
/// A <see cref="ServiceStartMode"/> value indicating how the service is configured to start:
/// Automatic, Manual, or Disabled.
⋮----
/// Gets or sets the type of the service.
⋮----
/// A <see cref="ServiceType"/> value describing whether the service runs in its own process,
/// as a shared process, as a driver, etc.
⋮----
/// Gets or sets a value indicating whether the service supports pause and continue commands.
⋮----
/// <value><c>true</c> if the service can be paused and continued; otherwise, <c>false</c>.</value>
⋮----
/// Gets or sets a value indicating whether the service receives a notification when the system is shutting down.
⋮----
/// <value><c>true</c> if the service will receive shutdown notifications; otherwise, <c>false</c>.</value>
⋮----
/// Gets or sets a value indicating whether the service can be stopped.
⋮----
/// <value><c>true</c> if the service supports stop requests; otherwise, <c>false</c>.</value>
````

## File: WinServicesTool/Services/WindowsServiceManager.cs
````csharp
/// <summary>
/// Default implementation of <see cref="IWindowsServiceManager"/> using <see cref="ServiceController"/>.
/// </summary>
public sealed class WindowsServiceManager : IWindowsServiceManager
⋮----
private readonly AppConfig _appConfig;
⋮----
public Task<List<Service>> GetServicesAsync()
⋮----
return Task.Run(() =>
⋮----
var services = ServiceController.GetServices();
⋮----
.Select(serv => new Service
⋮----
ServiceType = ServiceTypeHelper.Describe((int)serv.ServiceType),
⋮----
.OrderBy(s => s.DisplayName)];
⋮----
using var pathHelper = new ServicePathHelper();
⋮----
Path = pathHelper.GetExecutablePath(serv.ServiceName) ?? string.Empty,
⋮----
.OrderBy(s => s.DisplayName)
.ToList();
⋮----
_logger.LogError(ex, "Failed to enumerate services");
⋮----
public Task StartServiceAsync(string serviceName)
⋮----
using var sc = new ServiceController(serviceName);
⋮----
sc.Start();
sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
⋮----
public Task StopServiceAsync(string serviceName)
⋮----
sc.Stop();
sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
⋮----
private static ServiceStartMode GetStartTypeSafe(ServiceController serv)
````

## File: WinServicesTool/Utils/ColumnWidthStore.cs
````csharp
internal static class ColumnWidthStore
⋮----
private static readonly string _filePath = Path.Combine(_appFolder, "column_widths.json");
/// <summary>
/// Saves the given column widths to a JSON file in the application data folder.
/// </summary>
/// <param name="widths">
/// A dictionary mapping column identifiers to their widths in pixels.
/// </param>
public static void Save(IDictionary<string, int> widths)
⋮----
Directory.CreateDirectory(_appFolder);
var options = new JsonSerializerOptions { WriteIndented = true };
File.WriteAllText(_filePath, JsonSerializer.Serialize(widths, options));
⋮----
// Swallow exceptions — non-critical
⋮----
/// Loads a dictionary of string keys and integer values from the configured file path, if the file exists and can
/// be deserialized.
⋮----
/// <remarks>Returns <see langword="null"/> if the file does not exist, cannot be read, or contains
/// invalid data. The returned dictionary may be empty if the file contains no entries.</remarks>
/// <returns>A <see cref="Dictionary{string, int}"/> containing the deserialized data if the file exists and is valid;
/// otherwise, <see langword="null"/>.</returns>
public static Dictionary<string, int>? Load()
⋮----
if (!File.Exists(_filePath)) return null;
var json = File.ReadAllText(_filePath);
````

## File: WinServicesTool/WinServicesTool.csproj
````
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <ApplicationIcon>app.ico</ApplicationIcon>
    <PlatformTarget>x64</PlatformTarget>
  </PropertyGroup>


  <PropertyGroup>
    <PropertyChangedAnalyzerConfiguration>
      <IsCodeGeneratorDisabled>false</IsCodeGeneratorDisabled>
      <EventInvokerName>OnPropertyChanged</EventInvokerName>
    </PropertyChangedAnalyzerConfiguration>
  </PropertyGroup>

  <ItemGroup>
    <None Remove="nlog.config" />
  </ItemGroup>

  <ItemGroup>
    <Content Include="nlog.config">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Include="app.ico" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Fody" Version="6.9.3">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="9.0.9" />
    <PackageReference Include="NLog" Version="6.0.5" />
    <PackageReference Include="NLog.Extensions.Logging" Version="6.0.5" />
    <PackageReference Include="PropertyChanged.Fody" Version="4.1.0">
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="System.ServiceProcess.ServiceController" Version="9.0.9" />
  </ItemGroup>

</Project>
````

## File: WinServicesTool/Forms/FormMain.Designer.cs
````csharp
sealed partial class FormMain
⋮----
/// <summary>
///  Required designer variable.
/// </summary>
⋮----
///  Clean up any resources being used.
⋮----
/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
protected override void Dispose(bool disposing)
⋮----
components.Dispose();
⋮----
base.Dispose(disposing);
⋮----
///  Required method for Designer support - do not modify
///  the contents of this method with the code editor.
⋮----
private void InitializeComponent()
⋮----
TabCtrl = new TabControl();
TabMain = new TabPage();
tableLayoutPanel2 = new TableLayoutPanel();
tableLayoutPanel3 = new TableLayoutPanel();
BtnLoad = new Button();
Imgs = new ImageList(components);
BtnStart = new Button();
BtnStop = new Button();
BtnRestart = new Button();
BtnChangeStartMode = new Button();
tableLayoutPanelFilter = new TableLayoutPanel();
CbFilterStatus = new ComboBox();
CbFilterStartMode = new ComboBox();
TxtFilter = new TextBox();
LblFilterStatus = new Label();
LblStartMode = new Label();
GridServs = new DataGridView();
serviceBindingSource = new BindingSource(components);
TabSettings = new TabPage();
PnlSettings = new TableLayoutPanel();
GrpStarting = new GroupBox();
ChkStartAsAdm = new CheckBox();
GrpSettingsGrid = new GroupBox();
ChkAutoWidth = new CheckBox();
ChkShowPath = new CheckBox();
TextLog = new RichTextBox();
SplitMain = new SplitContainer();
ColDisplayName = new DataGridViewTextBoxColumn();
ColServiceName = new DataGridViewTextBoxColumn();
ColStatus = new DataGridViewTextBoxColumn();
ColStartMode = new DataGridViewTextBoxColumn();
ColServiceType = new DataGridViewTextBoxColumn();
ColCanPauseAndContinue = new DataGridViewCheckBoxColumn();
ColCanShutdown = new DataGridViewCheckBoxColumn();
ColCanStop = new DataGridViewCheckBoxColumn();
ColPath = new DataGridViewTextBoxColumn();
TabCtrl.SuspendLayout();
TabMain.SuspendLayout();
tableLayoutPanel2.SuspendLayout();
tableLayoutPanel3.SuspendLayout();
tableLayoutPanelFilter.SuspendLayout();
((System.ComponentModel.ISupportInitialize)GridServs).BeginInit();
((System.ComponentModel.ISupportInitialize)serviceBindingSource).BeginInit();
TabSettings.SuspendLayout();
PnlSettings.SuspendLayout();
GrpStarting.SuspendLayout();
GrpSettingsGrid.SuspendLayout();
((System.ComponentModel.ISupportInitialize)SplitMain).BeginInit();
SplitMain.Panel1.SuspendLayout();
SplitMain.Panel2.SuspendLayout();
SplitMain.SuspendLayout();
⋮----
//
// TabCtrl
⋮----
TabCtrl.Controls.Add(TabMain);
TabCtrl.Controls.Add(TabSettings);
⋮----
TabCtrl.Location = new Point(0, 0);
⋮----
TabCtrl.Size = new Size(1088, 561);
⋮----
// TabMain
⋮----
TabMain.Controls.Add(tableLayoutPanel2);
TabMain.Location = new Point(4, 24);
⋮----
TabMain.Padding = new Padding(3);
TabMain.Size = new Size(1080, 533);
⋮----
// tableLayoutPanel2
⋮----
tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
tableLayoutPanel2.Controls.Add(tableLayoutPanelFilter, 0, 1);
tableLayoutPanel2.Controls.Add(GridServs, 0, 2);
⋮----
tableLayoutPanel2.Location = new Point(3, 3);
⋮----
tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
tableLayoutPanel2.Size = new Size(1074, 527);
⋮----
// tableLayoutPanel3
⋮----
tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
⋮----
tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
tableLayoutPanel3.Controls.Add(BtnLoad, 0, 0);
tableLayoutPanel3.Controls.Add(BtnStart, 1, 0);
tableLayoutPanel3.Controls.Add(BtnStop, 2, 0);
tableLayoutPanel3.Controls.Add(BtnRestart, 3, 0);
tableLayoutPanel3.Controls.Add(BtnChangeStartMode, 4, 0);
⋮----
tableLayoutPanel3.Location = new Point(3, 3);
⋮----
tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
tableLayoutPanel3.Size = new Size(1068, 44);
⋮----
// BtnLoad
⋮----
BtnLoad.Location = new Point(3, 3);
⋮----
BtnLoad.Size = new Size(94, 38);
⋮----
// Imgs
⋮----
Imgs.ImageStream = (ImageListStreamer)resources.GetObject("Imgs.ImageStream");
⋮----
Imgs.Images.SetKeyName(0, "refresh-cw.png");
Imgs.Images.SetKeyName(1, "columns-3-cog.png");
Imgs.Images.SetKeyName(2, "play.png");
Imgs.Images.SetKeyName(3, "square.png");
Imgs.Images.SetKeyName(4, "rotate-ccw.png");
Imgs.Images.SetKeyName(5, "trending-up.png");
⋮----
// BtnStart
⋮----
BtnStart.Location = new Point(103, 3);
⋮----
BtnStart.Size = new Size(94, 38);
⋮----
// BtnStop
⋮----
BtnStop.Location = new Point(203, 3);
⋮----
BtnStop.Size = new Size(94, 38);
⋮----
// BtnRestart
⋮----
BtnRestart.Location = new Point(303, 3);
⋮----
BtnRestart.Size = new Size(94, 38);
⋮----
// BtnChangeStartMode
⋮----
BtnChangeStartMode.Location = new Point(403, 3);
⋮----
BtnChangeStartMode.Size = new Size(94, 38);
⋮----
// tableLayoutPanelFilter
⋮----
tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
⋮----
tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
tableLayoutPanelFilter.Controls.Add(CbFilterStatus, 1, 0);
tableLayoutPanelFilter.Controls.Add(CbFilterStartMode, 3, 0);
tableLayoutPanelFilter.Controls.Add(TxtFilter, 4, 0);
tableLayoutPanelFilter.Controls.Add(LblFilterStatus, 0, 0);
tableLayoutPanelFilter.Controls.Add(LblStartMode, 2, 0);
⋮----
tableLayoutPanelFilter.Location = new Point(3, 53);
⋮----
tableLayoutPanelFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
tableLayoutPanelFilter.Size = new Size(1068, 34);
⋮----
// CbFilterStatus
⋮----
CbFilterStatus.Location = new Point(53, 5);
CbFilterStatus.Margin = new Padding(3, 5, 3, 3);
⋮----
CbFilterStatus.Size = new Size(194, 23);
⋮----
// CbFilterStartMode
⋮----
CbFilterStartMode.Location = new Point(303, 5);
CbFilterStartMode.Margin = new Padding(3, 5, 3, 3);
⋮----
CbFilterStartMode.Size = new Size(194, 23);
⋮----
// TxtFilter
⋮----
TxtFilter.Font = new Font("Segoe UI", 10F);
TxtFilter.Location = new Point(505, 5);
TxtFilter.Margin = new Padding(5, 5, 5, 3);
⋮----
TxtFilter.Size = new Size(558, 25);
⋮----
// LblFilterStatus
⋮----
LblFilterStatus.Location = new Point(0, 0);
LblFilterStatus.Margin = new Padding(0);
⋮----
LblFilterStatus.Size = new Size(50, 34);
⋮----
// LblStartMode
⋮----
LblStartMode.Location = new Point(253, 0);
⋮----
LblStartMode.Size = new Size(38, 30);
⋮----
// GridServs
⋮----
GridServs.Columns.AddRange(new DataGridViewColumn[] { ColDisplayName, ColServiceName, ColStatus, ColStartMode, ColServiceType, ColCanPauseAndContinue, ColCanShutdown, ColCanStop, ColPath });
⋮----
GridServs.Location = new Point(3, 93);
⋮----
GridServs.Size = new Size(1068, 431);
⋮----
// serviceBindingSource
⋮----
// TabSettings
⋮----
TabSettings.Controls.Add(PnlSettings);
TabSettings.Location = new Point(4, 24);
⋮----
TabSettings.Padding = new Padding(3);
TabSettings.Size = new Size(1080, 533);
⋮----
// PnlSettings
⋮----
PnlSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
⋮----
PnlSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
PnlSettings.Controls.Add(GrpStarting, 1, 0);
PnlSettings.Controls.Add(GrpSettingsGrid, 0, 0);
⋮----
PnlSettings.Location = new Point(3, 3);
⋮----
PnlSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 182F));
PnlSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
PnlSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
PnlSettings.Size = new Size(1074, 527);
⋮----
// GrpStarting
⋮----
GrpStarting.Controls.Add(ChkStartAsAdm);
⋮----
GrpStarting.Location = new Point(203, 3);
⋮----
GrpStarting.Padding = new Padding(5);
GrpStarting.Size = new Size(194, 176);
⋮----
// ChkStartAsAdm
⋮----
ChkStartAsAdm.Location = new Point(5, 21);
⋮----
ChkStartAsAdm.Size = new Size(184, 19);
⋮----
// GrpSettingsGrid
⋮----
GrpSettingsGrid.Controls.Add(ChkAutoWidth);
GrpSettingsGrid.Controls.Add(ChkShowPath);
⋮----
GrpSettingsGrid.Location = new Point(3, 3);
⋮----
GrpSettingsGrid.Padding = new Padding(5);
GrpSettingsGrid.Size = new Size(194, 176);
⋮----
// ChkAutoWidth
⋮----
ChkAutoWidth.Location = new Point(5, 21);
⋮----
ChkAutoWidth.Size = new Size(184, 19);
⋮----
// ChkShowPath
⋮----
ChkShowPath.Location = new Point(5, 40);
⋮----
ChkShowPath.Size = new Size(184, 19);
⋮----
// TextLog
⋮----
TextLog.Location = new Point(0, 0);
TextLog.Margin = new Padding(10);
⋮----
TextLog.Size = new Size(1088, 126);
⋮----
// SplitMain
⋮----
SplitMain.Location = new Point(0, 0);
⋮----
// SplitMain.Panel1
⋮----
SplitMain.Panel1.Controls.Add(TabCtrl);
⋮----
// SplitMain.Panel2
⋮----
SplitMain.Panel2.Controls.Add(TextLog);
SplitMain.Size = new Size(1088, 691);
⋮----
// ColDisplayName
⋮----
// ColServiceName
⋮----
// ColStatus
⋮----
// ColStartMode
⋮----
// ColServiceType
⋮----
// ColCanPauseAndContinue
⋮----
// ColCanShutdown
⋮----
// ColCanStop
⋮----
// Path
⋮----
// FormMain
⋮----
AutoScaleDimensions = new SizeF(7F, 15F);
⋮----
ClientSize = new Size(1088, 691);
Controls.Add(SplitMain);
Icon = (Icon)resources.GetObject("$this.Icon");
⋮----
TabCtrl.ResumeLayout(false);
TabMain.ResumeLayout(false);
tableLayoutPanel2.ResumeLayout(false);
tableLayoutPanel3.ResumeLayout(false);
tableLayoutPanelFilter.ResumeLayout(false);
tableLayoutPanelFilter.PerformLayout();
((System.ComponentModel.ISupportInitialize)GridServs).EndInit();
((System.ComponentModel.ISupportInitialize)serviceBindingSource).EndInit();
TabSettings.ResumeLayout(false);
PnlSettings.ResumeLayout(false);
GrpStarting.ResumeLayout(false);
GrpStarting.PerformLayout();
GrpSettingsGrid.ResumeLayout(false);
GrpSettingsGrid.PerformLayout();
SplitMain.Panel1.ResumeLayout(false);
SplitMain.Panel2.ResumeLayout(false);
((System.ComponentModel.ISupportInitialize)SplitMain).EndInit();
SplitMain.ResumeLayout(false);
⋮----
private TabControl TabCtrl;
private TabPage TabMain;
private TabPage TabSettings;
private TableLayoutPanel tableLayoutPanel2;
private DataGridView GridServs;
private BindingSource serviceBindingSource;
private TableLayoutPanel tableLayoutPanel3;
private TableLayoutPanel tableLayoutPanelFilter;
private ComboBox CbFilterStatus;
private ComboBox CbFilterStartMode;
private Button BtnStart;
private Button BtnStop;
private Button BtnRestart;
private Button BtnChangeStartMode;
private Button BtnLoad;
private TextBox TxtFilter;
private ImageList Imgs;
private RichTextBox TextLog;
private SplitContainer SplitMain;
private Label LblFilterStatus;
private Label LblStartMode;
private TableLayoutPanel PnlSettings;
private GroupBox GrpSettingsGrid;
private CheckBox ChkAutoWidth;
private CheckBox ChkShowPath;
private GroupBox GrpStarting;
private CheckBox ChkStartAsAdm;
private DataGridViewTextBoxColumn ColDisplayName;
private DataGridViewTextBoxColumn ColServiceName;
private DataGridViewTextBoxColumn ColStatus;
private DataGridViewTextBoxColumn ColStartMode;
private DataGridViewTextBoxColumn ColServiceType;
private DataGridViewCheckBoxColumn ColCanPauseAndContinue;
private DataGridViewCheckBoxColumn ColCanShutdown;
private DataGridViewCheckBoxColumn ColCanStop;
private DataGridViewTextBoxColumn ColPath;
````

## File: WinServicesTool/Program.cs
````csharp
var services = new ServiceCollection();
⋮----
services.AddSingleton<AppConfig>(_ => AppConfig.Load());
// Add NLog
services.AddLogging(builder =>
⋮----
builder.SetMinimumLevel(LogLevel.Trace);
builder.AddNLog("nlog.config");
⋮----
var serviceProvider = services.BuildServiceProvider();
⋮----
logger.LogInformation("Application Starting...");
logger.LogInformation
⋮----
ApplicationConfiguration.Initialize();
⋮----
Application.Run(mainForm);
⋮----
logger.LogError(ex, "Exception not handled");
````

## File: WinServicesTool/Forms/FormMain.cs
````csharp
// ReSharper disable AsyncVoidEventHandlerMethod
public sealed partial class FormMain : Form
⋮----
// App configuration
private readonly AppConfig _appConfig;
// Timer used to debounce form resize events when AutoWidthColumns is enabled
⋮----
private SortOrder _sortOrder = SortOrder.None;
⋮----
private readonly IWindowsServiceManager _serviceManager;
private readonly IPrivilegeService _privilegeService;
⋮----
ChkAutoWidth.DataBindings.Add("Checked", _appConfig, nameof(AppConfig.AutoWidthColumns), false, DataSourceUpdateMode.OnPropertyChanged);
ChkShowPath.DataBindings.Add("Checked", _appConfig, nameof(AppConfig.ShowPathColumn), false, DataSourceUpdateMode.OnPropertyChanged);
ChkStartAsAdm.DataBindings.Add("Checked", _appConfig, nameof(AppConfig.AlwaysStartsAsAdministrator), false, DataSourceUpdateMode.OnPropertyChanged);
// Make header selection color match header background so headers don't show as "selected" in blue
⋮----
hdrStyle.WrapMode = DataGridViewTriState.True; // Enable word wrap for multi-line headers
hdrStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center align headers vertically
⋮----
// On startup, if we are not running elevated, ask the user to restart as admin
if (_privilegeService.IsAdministrator())
⋮----
_privilegeService.AskAndRestartAsAdmin(this, _appConfig.AlwaysStartsAsAdministrator);
⋮----
private void AppConfigChanged(object? sender, PropertyChangedEventArgs e)
⋮----
_appConfig.Save();
// Handle dynamic column visibility changes
⋮----
// Designer-based filter controls are wired in constructor
private async void FormPrincipal_Load(object? sender, EventArgs e)
⋮----
if (!_privilegeService.IsAdministrator())
⋮----
private void UpdateFilterLists()
⋮----
// Preserve previous selections
⋮----
CbFilterStatus.Items.Clear();
CbFilterStatus.Items.Add("All");
foreach (var st in _allServices.Select(s => s.Status).Distinct().Order())
⋮----
CbFilterStatus.Items.Add(st.ToString());
⋮----
if (!string.IsNullOrEmpty(prevStatus) && CbFilterStatus.Items.Contains(prevStatus))
⋮----
CbFilterStartMode.Items.Clear();
CbFilterStartMode.Items.Add("All");
foreach (var sm in _allServices.Select(static s => s.StartMode).Distinct().Order())
⋮----
CbFilterStartMode.Items.Add(sm.ToString());
⋮----
if (!string.IsNullOrEmpty(prevStart) && CbFilterStartMode.Items.Contains(prevStart))
⋮----
_logger.LogError(ex, "Failed to update filter lists");
⋮----
private void GridServs_SelectionChanged(object? sender, EventArgs e)
⋮----
var sel = GetSelectedServices().ToList();
⋮----
var statuses = sel.Select(s => s.Status).Distinct().ToList();
⋮----
_logger.LogError(ex, "Error evaluating selection state");
⋮----
private void GridServs_MouseUp(object? sender, MouseEventArgs e)
⋮----
// Determine the row under the mouse
var hit = GridServs.HitTest(e.X, e.Y);
⋮----
// Select the row if not already selected
⋮----
GridServs.ClearSelection();
⋮----
var selected = GetSelectedServices().ToList();
⋮----
// Build context menu dynamically based on status
var menu = new ContextMenuStrip();
// If all selected are stopped, show Start
if (selected.All(s => s.Status == ServiceControllerStatus.Stopped))
⋮----
var startItem = new ToolStripMenuItem("Start") { Enabled = true };
⋮----
menu.Items.Add(startItem);
⋮----
// If any selected are running or paused, show Stop and Restart
if (selected.Any(s => s.Status is ServiceControllerStatus.Running or ServiceControllerStatus.Paused))
⋮----
var stopItem = new ToolStripMenuItem("Stop") { Enabled = true };
⋮----
menu.Items.Add(stopItem);
var restartItem = new ToolStripMenuItem("Restart") { Enabled = true };
⋮----
menu.Items.Add(restartItem);
⋮----
// Separator
menu.Items.Add(new ToolStripSeparator());
// Change start mode
var changeStart = new ToolStripMenuItem("Change Start Mode...") { Enabled = true };
⋮----
menu.Items.Add(changeStart);
// Go to registry (only for single selection)
⋮----
var goToRegistry = new ToolStripMenuItem("Go to Registry") { Enabled = true };
⋮----
menu.Items.Add(goToRegistry);
⋮----
// Show menu at cursor
menu.Show(GridServs, new Point(e.X, e.Y));
⋮----
_logger.LogError(ex, "Error showing context menu");
⋮----
private void OpenServiceInRegistry(string serviceName)
⋮----
// Registry path for the service
⋮----
// Set the LastKey in the current user's registry so regedit opens at the correct location
using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit"))
⋮----
// Small delay to ensure registry is written
Thread.Sleep(100);
// Now open regedit - it should automatically navigate to the LastKey
var regeditStart = new ProcessStartInfo
⋮----
var process = Process.Start(regeditStart);
⋮----
_logger.LogError(ex, "Failed to open registry for service {ServiceName}", serviceName);
⋮----
private void AppendLog(string message, LogLevel level = LogLevel.Information)
⋮----
var ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
⋮----
// Log to injected logger
⋮----
_logger.LogError(message);
⋮----
_logger.LogWarning(message);
⋮----
_logger.LogInformation(message);
⋮----
// Append to TextLog on UI thread and auto-scroll to bottom
⋮----
TextLog.AppendText(line);
// move caret to end and scroll
⋮----
TextLog.ScrollToCaret();
⋮----
TextLog.InvokeIfRequired(AppendAndScroll);
⋮----
// swallow
⋮----
// Draw a custom sort glyph (triangle) in the header so it's visible across themes
private void GridServs_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
⋮----
// Only care about header cells
⋮----
// Let default paint run first
e.PaintBackground(e.CellBounds, true);
e.PaintContent(e.CellBounds);
if (string.IsNullOrEmpty(_sortPropertyName) || _sortOrder == SortOrder.None)
⋮----
var propName = !string.IsNullOrEmpty(col.DataPropertyName)
⋮----
// Draw a small triangle on the right side of the header
⋮----
var size = Math.Min(12, rect.Height - 8);
⋮----
? [new Point(cx, cy + (size / 2)), new Point(cx + size, cy + (size / 2)), new Point(cx + (size / 2), cy - (size / 2))]
: [new Point(cx, cy - (size / 2)), new Point(cx + size, cy - (size / 2)), new Point(cx + (size / 2), cy + (size / 2))];
using var brush = new SolidBrush(Color.FromArgb(80, 80, 80));
⋮----
graphicsContext.FillPolygon(brush, pts);
⋮----
// swallow painting errors
⋮----
private async void BtnLoad_Click(object? sender, EventArgs e)
⋮----
_allServices = await _serviceManager.GetServicesAsync();
// Configure column sizing BEFORE populating data
// This is critical for Fill mode to work correctly
⋮----
// Set all to None before loading saved widths
⋮----
// Update the filter dropdowns to show only values present in the loaded list
⋮----
// Now populate the grid with data
⋮----
// Load saved widths only if NOT in auto-width mode (after data is populated)
⋮----
MessageBox.Show(this, $"Failed to load services: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
⋮----
_logger.LogError(ex, "Failed to load services");
⋮----
private void ApplyColumnSizing()
⋮----
// Temporarily suspend layout to avoid flickering
GridServs.SuspendLayout();
⋮----
// First, configure non-Fill columns to take minimal space
⋮----
.Where(c => c != ColDisplayName && c != ColPath))
⋮----
col.FillWeight = 50; // Minimal weight so they don't expand
⋮----
// Configure Fill columns to share remaining space
⋮----
// Path column should also fill when visible
⋮----
ColPath.FillWeight = 150; // More weight for Path (longer content)
⋮----
// When Path is hidden, ensure it doesn't interfere
⋮----
// Manual sizing mode - set all to None to respect saved widths
⋮----
GridServs.ResumeLayout();
GridServs.Refresh();
⋮----
private void TxtFilter_TextChanged(object? sender, EventArgs e)
⋮----
// Debounce filter input
⋮----
_filterCts = new CancellationTokenSource();
⋮----
private async Task ApplyFilterWithDelay(CancellationToken ct)
⋮----
// Wait 400ms after last keystroke
await Task.Delay(400, ct);
⋮----
// expected on rapid typing
⋮----
private void ApplyFilterImmediately()
⋮----
private void ApplyFilterAndSort()
⋮----
var text = TxtFilter.Text.Trim();
⋮----
if (string.IsNullOrEmpty(text))
⋮----
var lower = text.ToLowerInvariant();
working = [.. _allServices.Where(s =>
(!string.IsNullOrEmpty(s.DisplayName) && s.DisplayName.Contains(lower, StringComparison.InvariantCultureIgnoreCase)) ||
(!string.IsNullOrEmpty(s.ServiceName) && s.ServiceName.Contains(lower, StringComparison.InvariantCultureIgnoreCase))
⋮----
// Apply column filters if present
⋮----
if (CbFilterStatus.SelectedItem is string statusSel && !string.Equals(statusSel, "All", StringComparison.OrdinalIgnoreCase))
⋮----
working = working.Where(s => s.Status == parsedStatus).ToList();
⋮----
if (CbFilterStartMode.SelectedItem is string startSel && !string.Equals(startSel, "All", StringComparison.OrdinalIgnoreCase))
⋮----
working = working.Where(s => s.StartMode == parsedStart).ToList();
⋮----
// swallow filter errors — filters are convenience UI only
⋮----
// Apply sorting if requested
if (!string.IsNullOrEmpty(_sortPropertyName) && _sortOrder != SortOrder.None)
⋮----
var prop = typeof(Service).GetProperty(_sortPropertyName!);
⋮----
? working.OrderBy(s => prop.GetValue(s, null)).ToList()
: working.OrderByDescending(s => prop.GetValue(s, null)).ToList();
⋮----
// ChangeStartMode helper methods are defined after this method
// Update header glyphs and font: reset all, then apply only when there's an active sort (Asc/Desc)
⋮----
// Reset header font
c.HeaderCell.Style.Font = new Font(GridServs.Font, FontStyle.Regular);
⋮----
var col = GridServs.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.DataPropertyName == _sortPropertyName || c.Name == _sortPropertyName);
⋮----
col.HeaderCell.Style.Font = new Font(GridServs.Font, FontStyle.Bold);
⋮----
serviceBindingSource.ResetBindings(false);
// Ensure the DataGridView repaints so the sort glyph is shown/cleared immediately
⋮----
// If we have a sorted column, invalidate its header to ensure glyph is painted
⋮----
var idx = GridServs.Columns.Cast<DataGridViewColumn>().ToList().FindIndex(c => c.DataPropertyName == _sortPropertyName || c.Name == _sortPropertyName);
⋮----
GridServs.InvalidateColumn(idx);
// additional aggressive repaints
GridServs.Invalidate();
GridServs.Update();
// clear any selection that might leave header visually selected
⋮----
private void BtnChangeStartMode_Click(object? sender, EventArgs e)
⋮----
var selecteds = GetSelectedServices().ToList();
⋮----
// Preselect current start mode from first selected service
⋮----
using var dlg = new FormChangeStartMode(initial);
if (dlg.ShowDialog(this) != DialogResult.OK)
⋮----
var newMode = dlg.SelectedMode; // 'Automatic' | 'Manual' | 'Disabled'
⋮----
Task.Run(() =>
⋮----
using var key = Registry.LocalMachine.OpenSubKey($"SYSTEM\\CurrentControlSet\\Services\\{serv.ServiceName}", writable: true);
⋮----
results.Add(msg);
⋮----
key.SetValue("Start", startValue, RegistryValueKind.DWord);
⋮----
results.Add(okMsg);
⋮----
results.Add(err);
⋮----
// Refresh list on UI thread and show summary
this.InvokeIfRequired(() =>
⋮----
var summary = string.Join(Environment.NewLine, results);
MessageBox.Show(this, summary, $"Start Type changed to \"{newMode}\".", MessageBoxButtons.OK, MessageBoxIcon.Information);
⋮----
private static int StartModeToDword(string mode)
⋮----
private void GridServs_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
⋮----
// cycle: None -> Asc -> Desc -> None
⋮----
// clear property when cycling back to no-sort
⋮----
private void GridServs_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
⋮----
// Color the entire row by status
⋮----
row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230); // light green
⋮----
row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230); // light red
⋮----
row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 230); // light yellow
⋮----
// For the Status column, prefix with an emoji
⋮----
_logger.LogError(ex, "Error formatting cell");
⋮----
private IEnumerable<Service> GetSelectedServices()
⋮----
.Select(r => r.DataBoundItem as Service)
.Where(s => s != null)
⋮----
private async void BtnStart_Click(object? sender, EventArgs e)
⋮----
var selectedServices = GetSelectedServices().ToList();
⋮----
// Only allow starting services that are stopped
if (selectedServices.Any(s => s.Status != ServiceControllerStatus.Stopped))
⋮----
MessageBox.Show(this, "Please select only services that are stopped to start.", "Start services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
⋮----
if (MessageBox.Show(this, $"Start {selectedServices.Count} service(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
⋮----
await _serviceManager.StartServiceAsync(serv.ServiceName);
⋮----
_logger.LogInformation("Started service {ServiceName}", serv.ServiceName);
⋮----
_logger.LogError(ex, "Failed to start service {ServiceName}", serv.ServiceName);
⋮----
private async void BtnStop_Click(object? sender, EventArgs e)
⋮----
// Only allow stopping services that are running or paused
if (selectedServices.Any(s => s.Status != ServiceControllerStatus.Running && s.Status != ServiceControllerStatus.Paused))
⋮----
MessageBox.Show(this, "Please select only services that are running or paused to stop.", "Stop services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
⋮----
if (MessageBox.Show(this, $"Stop {selectedServices.Count} service(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
⋮----
await _serviceManager.StopServiceAsync(serv.ServiceName);
⋮----
_logger.LogInformation("Stopped service {ServiceName}", serv.ServiceName);
⋮----
_logger.LogError(ex, "Failed to stop service {ServiceName}", serv.ServiceName);
⋮----
private async void BtnRestart_Click(object? sender, EventArgs e)
⋮----
// For restart, require services to be running or paused
if (sel.Any(s => s.Status != ServiceControllerStatus.Running && s.Status != ServiceControllerStatus.Paused))
⋮----
MessageBox.Show(this, "Please select only services that are running or paused to restart.", "Restart services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
⋮----
if (MessageBox.Show(this, $"Restart {sel.Count} service(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
⋮----
// Stop then start using the service manager to centralize logic
await _serviceManager.StopServiceAsync(s.ServiceName);
await _serviceManager.StartServiceAsync(s.ServiceName);
⋮----
_logger.LogInformation("Restarted service {ServiceName}", s.ServiceName);
⋮----
_logger.LogError(ex, "Failed to restart service {ServiceName}", s.ServiceName);
⋮----
private void LoadColumnWidths()
⋮----
var map = ColumnWidthStore.Load();
⋮----
if (!map.TryGetValue(col.Name, out var w) || w <= 0)
⋮----
// When width explicitly set, change autosize to None so it persists
⋮----
_logger.LogError(ex, "Failed to load column widths");
⋮----
// Persist app settings when user toggles checkbox
private void ChkAutoWidth_CheckedChanged(object? sender, EventArgs e)
⋮----
// Switching to auto-width mode
⋮----
// Switching to manual mode - load saved widths
ApplyColumnSizing(); // First set all to None
LoadColumnWidths();  // Then apply saved widths
⋮----
private async void ChkShowPath_CheckedChanged(object? sender, EventArgs e)
⋮----
private void SaveColumnWidths()
⋮----
var map = GridServs.Columns.Cast<DataGridViewColumn>().ToDictionary(c => c.Name, c => c.Width);
ColumnWidthStore.Save(map);
⋮----
_logger.LogError(ex, "Failed to save column widths");
⋮----
private void GridServs_ColumnWidthChanged(object? sender, DataGridViewColumnEventArgs e)
⋮----
private void FormPrincipal_FormClosing(object? sender, FormClosingEventArgs e)
⋮----
private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
⋮----
TxtFilter.Focus();
⋮----
TxtFilter.Clear();
⋮----
private async Task TogglePathColumnVisibilityAsync()
⋮----
// Suspend layout to avoid flickering
⋮----
// Apply column sizing to adjust layout immediately
⋮----
// If hiding the column, we're done
⋮----
var servicesToUpdate = _servicesList.Where(s => string.IsNullOrEmpty(s.Path)).ToList();
⋮----
// Run the path loading in a background thread to avoid UI freezing
await Task.Run(() =>
⋮----
using var pathHelper = new ServicePathHelper();
⋮----
service.Path = pathHelper.GetExecutablePath(service.ServiceName) ?? string.Empty;
⋮----
// Notify the binding source that data changed
⋮----
// Reapply column sizing after data is loaded
````
