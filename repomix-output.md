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
.editorconfig
tests/WinServicesTool.Tests/AppConfigTests.cs
tests/WinServicesTool.Tests/FormMainRegistryTests.cs
tests/WinServicesTool.Tests/ServiceOperationOrchestratorAdditionalTests.cs
tests/WinServicesTool.Tests/ServiceOperationOrchestratorTests.cs
tests/WinServicesTool.Tests/ServiceTypeHelperTests.cs
tests/WinServicesTool.Tests/WinServicesTool.Tests.csproj
WinServicesTool.slnx
WinServicesTool/Extensions/ControlExtensions.cs
WinServicesTool/FodyWeavers.xml
WinServicesTool/Forms/FormChangeStartMode.cs
WinServicesTool/Forms/FormColumnChooser.cs
WinServicesTool/Forms/FormMain.cs
WinServicesTool/Forms/FormMain.Designer.cs
WinServicesTool/Models/Service.cs
WinServicesTool/Models/ServiceTypeEx.cs
WinServicesTool/nlog.config
WinServicesTool/Program.cs
WinServicesTool/Properties/AssemblyInternals.cs
WinServicesTool/Properties/DataSources/WinServicesTool.Models.Service.datasource
WinServicesTool/Properties/Resources.Designer.cs
WinServicesTool/Services/IPrivilegeService.cs
WinServicesTool/Services/IProcessLauncher.cs
WinServicesTool/Services/IRegistryEditor.cs
WinServicesTool/Services/IRegistryService.cs
WinServicesTool/Services/IServiceOperationOrchestrator.cs
WinServicesTool/Services/IWindowsServiceManager.cs
WinServicesTool/Services/PrivilegeService.cs
WinServicesTool/Services/ProcessLauncher.cs
WinServicesTool/Services/RegistryEditor.cs
WinServicesTool/Services/RegistryService.cs
WinServicesTool/Services/ServiceNativeHelper.cs
WinServicesTool/Services/ServiceOperationOrchestrator.cs
WinServicesTool/Services/WindowsServiceManager.cs
WinServicesTool/Utils/AppConfig.cs
WinServicesTool/WinServicesTool.csproj
```

# Files

## File: tests/WinServicesTool.Tests/WinServicesTool.Tests.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0-windows7.0</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <IsPackable>false</IsPackable>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.0.0" />
    <PackageReference Include="xunit" Version="2.4.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.5" />
    <PackageReference Include="Shouldly" Version="4.3.0" />
    <PackageReference Include="NSubstitute" Version="4.4.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\WinServicesTool\WinServicesTool.csproj" />
  </ItemGroup>

</Project>
```

## File: WinServicesTool/FodyWeavers.xml
```xml
<Weavers xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="FodyWeavers.xsd">
  <PropertyChanged />
</Weavers>
```

## File: WinServicesTool/nlog.config
```
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
```

## File: WinServicesTool/Properties/AssemblyInternals.cs
```csharp

```

## File: WinServicesTool/Properties/DataSources/WinServicesTool.Models.Service.datasource
```
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
```

## File: WinServicesTool/Properties/Resources.Designer.cs
```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
⋮----
/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
// This class was auto-generated by the StronglyTypedResourceBuilder
// class via a tool like ResGen or Visual Studio.
// To add or remove a member, edit your .ResX file then rerun ResGen
// with the /str option, or rebuild your VS project.
⋮----
internal class Resources {
⋮----
///   Returns the cached ResourceManager instance used by this class.
⋮----
if (object.ReferenceEquals(resourceMan, null)) {
⋮----
///   Overrides the current thread's CurrentUICulture property for all
///   resource lookups using this strongly typed resource class.
```

## File: WinServicesTool/Services/IProcessLauncher.cs
```csharp
/// <summary>
/// Abstraction for launching processes so calls can be mocked in tests.
/// </summary>
public interface IProcessLauncher
⋮----
/// Starts a process given a filename and optional start info.
⋮----
Process? Start(string fileName);
```

## File: WinServicesTool/Services/IRegistryEditor.cs
```csharp
/// <summary>
/// Abstraction around registry edits used by the UI.
/// </summary>
public interface IRegistryEditor
⋮----
/// Sets the regedit LastKey value for the current user.
⋮----
void SetLastKey(string registryPath);
⋮----
/// Sets a DWORD value under HKEY_LOCAL_MACHINE for the specified subkey path.
/// The subKeyPath should be relative to the root of HKLM (for example: "SYSTEM\\CurrentControlSet\\Services\\MyService").
⋮----
void SetDwordInLocalMachine(string subKeyPath, string valueName, int value);
```

## File: WinServicesTool/Services/IRegistryService.cs
```csharp
/// <summary>
/// Abstraction for operations against the Windows Registry used by the UI.
/// </summary>
public interface IRegistryService
⋮----
/// Sets the LastKey value for Regedit so regedit opens at the specified path.
⋮----
void SetRegeditLastKey(string registryPath);
```

## File: WinServicesTool/Services/ProcessLauncher.cs
```csharp
public sealed class ProcessLauncher : IProcessLauncher
⋮----
public Process? Start(string fileName)
⋮----
var psi = new ProcessStartInfo
⋮----
return Process.Start(psi);
```

## File: tests/WinServicesTool.Tests/FormMainRegistryTests.cs
```csharp
public sealed class FormMainRegistryTests
⋮----
public void OpenServiceInRegistry_Invokes_RegistryService_With_Correct_Path()
⋮----
// Arrange
⋮----
var cfg = new AppConfig();
// Create FormMain with mocked dependencies
var form = new FormMain(logger, winSvcMgr, priv, orchestrator, registry, registryEditor, cfg);
⋮----
// Act
form.OpenServiceInRegistry(serviceName);
// Assert: registry service was asked to open the expected path
registry.Received(1).SetRegeditLastKey(expectedPath);
```

## File: tests/WinServicesTool.Tests/ServiceTypeHelperTests.cs
```csharp
public sealed class ServiceTypeHelperTests
⋮----
public void Describe_KnownType_ReturnsNonEmptyString()
⋮----
// Arrange
⋮----
// Act
var desc = ServiceTypeHelper.Describe(knownType);
// Assert
desc.ShouldNotBeNullOrEmpty();
```

## File: WinServicesTool.slnx
```
<Solution>
  <Folder Name="/tests/">
    <Project Path="tests/WinServicesTool.Tests/WinServicesTool.Tests.csproj" />
  </Folder>
  <Project Path="WinServicesTool/WinServicesTool.csproj" />
</Solution>
```

## File: WinServicesTool/Extensions/ControlExtensions.cs
```csharp
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
```

## File: WinServicesTool/Forms/FormColumnChooser.cs
```csharp
/// <summary>
/// Dialog for selecting visible columns in the services grid.
/// </summary>
public sealed class FormColumnChooser : Form
⋮----
/// Gets the list of selected column names after dialog is closed with OK.
⋮----
/// Creates a new column chooser dialog.
⋮----
/// <param name="allColumns">All available column names.</param>
/// <param name="visibleColumns">Currently visible column names.</param>
⋮----
private void InitializeComponent(List<DataGridViewColumn> allColumns, List<string> visibleColumnsName)
⋮----
ClientSize = new Size(380, 400);
var columns = allColumns.ConvertAll(col => new { col.Name, Caption = col.HeaderText.Replace('\n', ' ') });
var lbl = new Label
⋮----
_lstColumns = new CheckedListBox
⋮----
// Add all columns to the list
⋮----
var isChecked = visibleColumnsName.Count is 0 || visibleColumnsName.Contains(col.Name);
_lstColumns.Items.Add(col.Caption, isChecked);
⋮----
var btnSelectAll = new Button
⋮----
var btnDeselectAll = new Button
⋮----
var btnOk = new Button
⋮----
var btnCancel = new Button
⋮----
_lstColumns.SetItemChecked(i, true);
⋮----
_lstColumns.SetItemChecked(i, false);
⋮----
// Map checked captions back to column names
⋮----
.Select(i => columns[i].Name)
.ToList();
⋮----
Controls.Add(lbl);
Controls.Add(_lstColumns);
Controls.Add(btnSelectAll);
Controls.Add(btnDeselectAll);
Controls.Add(btnOk);
Controls.Add(btnCancel);
```

## File: WinServicesTool/Services/RegistryEditor.cs
```csharp
public sealed class RegistryEditor : IRegistryEditor
⋮----
public void SetLastKey(string registryPath)
⋮----
using var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit") ?? throw new InvalidOperationException("Unable to create or open Regedit LastKey location.");
key.SetValue("LastKey", registryPath, RegistryValueKind.String);
⋮----
public void SetDwordInLocalMachine(string subKeyPath, string valueName, int value)
⋮----
if (string.IsNullOrEmpty(subKeyPath))
throw new ArgumentException("subKeyPath must be provided", nameof(subKeyPath));
using var key = Registry.LocalMachine.OpenSubKey(subKeyPath, writable: true) ?? throw new InvalidOperationException($"HKLM\\{subKeyPath} not found");
key.SetValue(valueName, value, RegistryValueKind.DWord);
```

## File: WinServicesTool/Services/RegistryService.cs
```csharp
/// <summary>
/// Concrete implementation of <see cref="IRegistryService"/> which performs small
/// registry writes and launches regedit.exe so the UI can navigate to a service key.
/// </summary>
public sealed class RegistryService(IRegistryEditor editor, IProcessLauncher launcher) : IRegistryService
⋮----
private readonly IRegistryEditor _editor = editor;
private readonly IProcessLauncher _launcher = launcher;
/// <inheritdoc />
public void SetRegeditLastKey(string registryPath)
⋮----
if (string.IsNullOrEmpty(registryPath))
throw new ArgumentException("registryPath must be provided", nameof(registryPath));
_editor.SetLastKey(registryPath);
// Give the registry a short moment to persist before launching regedit
Thread.Sleep(100);
_launcher.Start("regedit.exe");
```

## File: .editorconfig
```
root = true

[*.{cs,csproj}]
indent_style = space
indent_size = 4
charset = utf-8-bom
end_of_line = crlf
insert_final_newline = true
trim_trailing_whitespace = true

# Preferência por 'var'
csharp_style_var_for_built_in_types = true:suggestion
csharp_style_var_when_type_is_apparent = true:suggestion
csharp_style_var_elsewhere = true:suggestion

# Evita this. em membros
dotnet_style_qualification_for_field = false:suggestion
dotnet_style_qualification_for_property = false:suggestion
dotnet_style_qualification_for_method = false:suggestion
dotnet_style_qualification_for_event = false:suggestion

# Usa expression-bodied members quando possível
csharp_style_expression_bodied_methods = when_on_single_line:suggestion
csharp_style_expression_bodied_constructors = when_on_single_line:suggestion
csharp_style_expression_bodied_properties = when_on_single_line:suggestion
csharp_style_expression_bodied_indexers = when_on_single_line:suggestion
csharp_style_expression_bodied_operators = when_on_single_line:suggestion
csharp_style_expression_bodied_accessors = when_on_single_line:suggestion

# Usa 'readonly' sempre que possível
dotnet_style_readonly_field = true:suggestion

# Organização de using
dotnet_remove_unnecessary_usings = true:silent
dotnet_sort_system_directives_first = true

# Regras de formatação adicionais
csharp_new_line_before_open_brace = all
csharp_indent_case_contents = true
csharp_indent_switch_labels = true

[*.md]
trim_trailing_whitespace = false
end_of_line = lf

[*.{json,xml,config,yml,yaml}]
indent_style = space
indent_size = 2
end_of_line = lf
```

## File: tests/WinServicesTool.Tests/ServiceOperationOrchestratorAdditionalTests.cs
```csharp
public sealed class ServiceOperationOrchestratorAdditionalTests
⋮----
public async Task RestartServices_AllOk_ReturnsAllTrue()
⋮----
svcManager.StopServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
svcManager.StartServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
var orchestrator = new ServiceOperationOrchestrator(svcManager, NullLogger<ServiceOperationOrchestrator>.Instance);
⋮----
var results = await orchestrator.RestartServicesAsync(services);
results.Count.ShouldBe(2);
results.Values.ShouldAllBe(v => v);
⋮----
public async Task StartServices_AllOk_ReturnsAllTrue()
⋮----
var results = await orchestrator.StartServicesAsync(services);
```

## File: tests/WinServicesTool.Tests/ServiceOperationOrchestratorTests.cs
```csharp
public sealed class ServiceOperationOrchestratorTests
⋮----
public async Task StartServices_WhenSomeFail_ReturnsCorrectMap()
⋮----
// Arrange
⋮----
svcManager.StartServiceAsync("ok").Returns(Task.CompletedTask);
svcManager.When(x => x.StartServiceAsync("bad")).Do(_ => throw new Exception("fail"));
var orchestrator = new ServiceOperationOrchestrator(svcManager, NullLogger<ServiceOperationOrchestrator>.Instance);
⋮----
// Act
var results = await orchestrator.StartServicesAsync(services);
// Assert
results.Count.ShouldBe(2);
results["ok"].ShouldBeTrue();
results["bad"].ShouldBeFalse();
```

## File: WinServicesTool/Models/ServiceTypeEx.cs
```csharp
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
```

## File: WinServicesTool/Services/IPrivilegeService.cs
```csharp
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
```

## File: WinServicesTool/Services/IServiceOperationOrchestrator.cs
```csharp
/// <summary>
/// Orchestrates higher-level operations across multiple services (start/stop/restart).
/// </summary>
public interface IServiceOperationOrchestrator
⋮----
/// Starts the provided services and returns a map of serviceName->success.
⋮----
Task<Dictionary<string, bool>> StartServicesAsync(IEnumerable<Service> services, CancellationToken cancellationToken = default);
⋮----
/// Stops the provided services and returns a map of serviceName->success.
⋮----
Task<Dictionary<string, bool>> StopServicesAsync(IEnumerable<Service> services, CancellationToken cancellationToken = default);
⋮----
/// Restarts the provided services (stop then start) and returns a map of serviceName->success.
⋮----
Task<Dictionary<string, bool>> RestartServicesAsync(IEnumerable<Service> services, CancellationToken cancellationToken = default);
```

## File: WinServicesTool/Services/IWindowsServiceManager.cs
```csharp
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
Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default);
⋮----
/// Stops the specified service and waits until stopped or timeout.
⋮----
Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default);
```

## File: WinServicesTool/Services/ServiceNativeHelper.cs
```csharp
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
⋮----
throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), $"OpenService failed for {serviceName}");
⋮----
if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
⋮----
var buffer = Marshal.AllocHGlobal((int)bytesNeeded);
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
```

## File: tests/WinServicesTool.Tests/AppConfigTests.cs
```csharp
public sealed class AppConfigTests
⋮----
public void Save_WhenCalled_PersistedAndLoadedValuesMatch()
⋮----
// Arrange
var tmpDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
Directory.CreateDirectory(tmpDir);
⋮----
var cfg = new AppConfig { VisibleColumns = ["Path"], AlwaysStartsAsAdministrator = true };
// Act
cfg.Save();
// Assert
var loaded = AppConfig.Load();
loaded.VisibleColumns.ShouldBe(["Path"]);
loaded.AlwaysStartsAsAdministrator.ShouldBe(true);
⋮----
var f = Path.Combine(tmpDir, "app_config.json");
if (File.Exists(f)) File.Delete(f);
⋮----
Directory.Delete(tmpDir);
⋮----
// ignored
```

## File: WinServicesTool/Models/Service.cs
```csharp
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
```

## File: WinServicesTool/Services/PrivilegeService.cs
```csharp
public sealed class PrivilegeService(ILogger<PrivilegeService> logger) : IPrivilegeService
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
```

## File: WinServicesTool/Services/ServiceOperationOrchestrator.cs
```csharp
/// <summary>
/// Default implementation of <see cref="IServiceOperationOrchestrator"/>.
/// </summary>
public sealed class ServiceOperationOrchestrator(IWindowsServiceManager svcManager, ILogger<ServiceOperationOrchestrator> logger) : IServiceOperationOrchestrator
⋮----
private readonly IWindowsServiceManager _svcManager = svcManager;
⋮----
public async Task<Dictionary<string, bool>> StartServicesAsync(IEnumerable<Service> services, CancellationToken cancellationToken = default)
⋮----
cancellationToken.ThrowIfCancellationRequested();
⋮----
await _svcManager.StartServiceAsync(s.ServiceName, cancellationToken);
⋮----
_logger.LogInformation("Started service {ServiceName}", s.ServiceName);
⋮----
_logger.LogInformation("Start cancelled for {ServiceName}", s.ServiceName);
⋮----
_logger.LogError(ex, "Failed to start service {ServiceName}", s.ServiceName);
⋮----
public async Task<Dictionary<string, bool>> StopServicesAsync(IEnumerable<Service> services, CancellationToken cancellationToken = default)
⋮----
await _svcManager.StopServiceAsync(s.ServiceName, cancellationToken);
⋮----
_logger.LogInformation("Stopped service {ServiceName}", s.ServiceName);
⋮----
_logger.LogInformation("Stop cancelled for {ServiceName}", s.ServiceName);
⋮----
_logger.LogError(ex, "Failed to stop service {ServiceName}", s.ServiceName);
⋮----
public async Task<Dictionary<string, bool>> RestartServicesAsync(IEnumerable<Service> services, CancellationToken cancellationToken = default)
⋮----
_logger.LogInformation("Restarted service {ServiceName}", s.ServiceName);
⋮----
_logger.LogInformation("Restart cancelled for {ServiceName}", s.ServiceName);
⋮----
_logger.LogError(ex, "Failed to restart service {ServiceName}", s.ServiceName);
```

## File: WinServicesTool/WinServicesTool.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <ApplicationIcon>app.ico</ApplicationIcon>
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

  <ItemGroup>
    <Compile Update="Properties\Resources.Designer.cs">
      <DesignTime>True</DesignTime>
      <AutoGen>True</AutoGen>
      <DependentUpon>Resources.resx</DependentUpon>
    </Compile>
  </ItemGroup>

  <ItemGroup>
    <EmbeddedResource Update="Properties\Resources.resx">
      <Generator>ResXFileCodeGenerator</Generator>
      <LastGenOutput>Resources.Designer.cs</LastGenOutput>
    </EmbeddedResource>
  </ItemGroup>

</Project>
```

## File: WinServicesTool/Forms/FormChangeStartMode.cs
```csharp
public sealed class FormChangeStartMode : Form
⋮----
private void InitializeComponent(string initialMode)
⋮----
ClientSize = new Size(320, 120);
var lbl = new Label
⋮----
_cmb = new ComboBox
⋮----
_cmb.Items.AddRange(["Automatic", "Manual", "Disabled"]);
// Try to select the initial mode if present
var idx = _cmb.Items.IndexOf(initialMode);
⋮----
: 1; // default Manual
var btnOk = new Button
⋮----
var btnCancel = new Button
⋮----
Controls.Add(lbl);
Controls.Add(_cmb);
Controls.Add(btnOk);
Controls.Add(btnCancel);
```

## File: WinServicesTool/Utils/AppConfig.cs
```csharp
public sealed partial class AppConfig : INotifyPropertyChanged
⋮----
/// <summary>
/// Gets or sets the list of visible column names in the services grid.
/// </summary>
⋮----
/// Gets a value indicating whether the Path column is currently visible in the view.
⋮----
=> VisibleColumns.Count is 0 || VisibleColumns.Contains("ColPath");
⋮----
/// Gets or sets a value indicating whether the application should always start with administrator privileges.
⋮----
/// Saved left position of the main window.
⋮----
/// Saved top position of the main window.
⋮----
/// Saved width of the main window.
⋮----
/// Saved height of the main window.
⋮----
/// Saved window state (Normal, Minimized, Maximized).
⋮----
/// Saved SplitContainer.SplitterDistance for the main split (log/grid).
⋮----
// Use the application's base directory (where the executable lives) so load/save
// happen from the same location regardless of current working directory.
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
```

## File: WinServicesTool/Program.cs
```csharp
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
```

## File: WinServicesTool/Services/WindowsServiceManager.cs
```csharp
/// <summary>
/// Default implementation of <see cref="IWindowsServiceManager"/> using <see cref="ServiceController"/>.
/// </summary>
public sealed class WindowsServiceManager(ILogger<WindowsServiceManager> logger, AppConfig appConfig) : IWindowsServiceManager
⋮----
public Task<List<Service>> GetServicesAsync()
=> Task.Run(() =>
⋮----
var services = ServiceController.GetServices();
// Check if Path column is visible in config
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
logger.LogError(ex, "Failed to enumerate services");
⋮----
public Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default)
⋮----
cancellationToken.ThrowIfCancellationRequested();
using var sc = new ServiceController(serviceName);
⋮----
sc.Start();
// Wait loop with cancellation checks
var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
⋮----
Thread.Sleep(200);
sc.Refresh();
⋮----
public Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default)
⋮----
sc.Stop();
⋮----
private static ServiceStartMode GetStartTypeSafe(ServiceController serv)
```

## File: WinServicesTool/Forms/FormMain.Designer.cs
```csharp
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
tableLayoutPanel2 = new TableLayoutPanel();
tableLayoutPanel3 = new TableLayoutPanel();
ChkStartAsAdm = new CheckBox();
BtnLoad = new Button();
Imgs = new ImageList(components);
BtnStart = new Button();
BtnStop = new Button();
BtnRestart = new Button();
BtnChangeStartMode = new Button();
BtnCancel = new Button();
tableLayoutPanelFilter = new TableLayoutPanel();
CbFilterStatus = new ComboBox();
CbFilterStartMode = new ComboBox();
TxtFilter = new TextBox();
LblFilterStatus = new Label();
LblStartMode = new Label();
GridServs = new DataGridView();
ColDisplayName = new DataGridViewTextBoxColumn();
ColServiceName = new DataGridViewTextBoxColumn();
ColStatus = new DataGridViewTextBoxColumn();
ColStartMode = new DataGridViewTextBoxColumn();
ColServiceType = new DataGridViewTextBoxColumn();
ColCanPauseAndContinue = new DataGridViewCheckBoxColumn();
ColCanShutdown = new DataGridViewCheckBoxColumn();
ColCanStop = new DataGridViewCheckBoxColumn();
ColPath = new DataGridViewTextBoxColumn();
serviceBindingSource = new BindingSource(components);
TextLog = new RichTextBox();
SplitMain = new SplitContainer();
StatusBar = new StatusStrip();
LblStatusServices = new ToolStripStatusLabel();
LblStatusSeparator = new ToolStripStatusLabel();
LblStatusServicesRunning = new ToolStripStatusLabel();
tableLayoutPanel2.SuspendLayout();
tableLayoutPanel3.SuspendLayout();
tableLayoutPanelFilter.SuspendLayout();
((System.ComponentModel.ISupportInitialize)GridServs).BeginInit();
((System.ComponentModel.ISupportInitialize)serviceBindingSource).BeginInit();
((System.ComponentModel.ISupportInitialize)SplitMain).BeginInit();
SplitMain.Panel1.SuspendLayout();
SplitMain.Panel2.SuspendLayout();
SplitMain.SuspendLayout();
StatusBar.SuspendLayout();
⋮----
//
// tableLayoutPanel2
⋮----
tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
tableLayoutPanel2.Controls.Add(tableLayoutPanelFilter, 0, 1);
tableLayoutPanel2.Controls.Add(GridServs, 0, 2);
⋮----
tableLayoutPanel2.Location = new Point(0, 0);
⋮----
tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
tableLayoutPanel2.Size = new Size(1252, 561);
⋮----
// tableLayoutPanel3
⋮----
tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
⋮----
tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
tableLayoutPanel3.Controls.Add(ChkStartAsAdm, 7, 0);
tableLayoutPanel3.Controls.Add(BtnLoad, 0, 0);
tableLayoutPanel3.Controls.Add(BtnStart, 1, 0);
tableLayoutPanel3.Controls.Add(BtnStop, 2, 0);
tableLayoutPanel3.Controls.Add(BtnRestart, 3, 0);
tableLayoutPanel3.Controls.Add(BtnChangeStartMode, 4, 0);
tableLayoutPanel3.Controls.Add(BtnCancel, 5, 0);
⋮----
tableLayoutPanel3.Location = new Point(3, 3);
⋮----
tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
tableLayoutPanel3.Size = new Size(1246, 44);
⋮----
// ChkStartAsAdm
⋮----
ChkStartAsAdm.Location = new Point(1129, 3);
⋮----
ChkStartAsAdm.Size = new Size(114, 38);
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
Imgs.Images.SetKeyName(6, "ban.png");
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
// BtnCancel
⋮----
BtnCancel.Font = new Font("Segoe UI", 9F);
⋮----
BtnCancel.Location = new Point(503, 3);
⋮----
BtnCancel.Size = new Size(94, 38);
⋮----
// tableLayoutPanelFilter
⋮----
tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
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
tableLayoutPanelFilter.Size = new Size(1246, 34);
⋮----
// CbFilterStatus
⋮----
CbFilterStatus.Location = new Point(73, 5);
CbFilterStatus.Margin = new Padding(3, 5, 3, 3);
⋮----
CbFilterStatus.Size = new Size(194, 23);
⋮----
// CbFilterStartMode
⋮----
CbFilterStartMode.Location = new Point(323, 5);
CbFilterStartMode.Margin = new Padding(3, 5, 3, 3);
⋮----
CbFilterStartMode.Size = new Size(194, 23);
⋮----
// TxtFilter
⋮----
TxtFilter.Font = new Font("Segoe UI", 10F);
TxtFilter.Location = new Point(525, 5);
TxtFilter.Margin = new Padding(5, 5, 5, 3);
⋮----
TxtFilter.Size = new Size(716, 25);
⋮----
// LblFilterStatus
⋮----
LblFilterStatus.Image = (Image)resources.GetObject("LblFilterStatus.Image");
⋮----
LblFilterStatus.Location = new Point(0, 0);
LblFilterStatus.Margin = new Padding(0);
⋮----
LblFilterStatus.Size = new Size(70, 34);
⋮----
// LblStartMode
⋮----
LblStartMode.Location = new Point(273, 0);
⋮----
LblStartMode.Size = new Size(38, 30);
⋮----
// GridServs
⋮----
GridServs.Columns.AddRange(new DataGridViewColumn[] { ColDisplayName, ColServiceName, ColStatus, ColStartMode, ColServiceType, ColCanPauseAndContinue, ColCanShutdown, ColCanStop, ColPath });
⋮----
GridServs.Location = new Point(3, 93);
⋮----
GridServs.Size = new Size(1246, 465);
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
// ColPath
⋮----
// serviceBindingSource
⋮----
// TextLog
⋮----
TextLog.Location = new Point(0, 0);
TextLog.Margin = new Padding(10);
⋮----
TextLog.Size = new Size(1252, 104);
⋮----
// SplitMain
⋮----
SplitMain.Location = new Point(0, 0);
⋮----
// SplitMain.Panel1
⋮----
SplitMain.Panel1.Controls.Add(tableLayoutPanel2);
⋮----
// SplitMain.Panel2
⋮----
SplitMain.Panel2.Controls.Add(TextLog);
SplitMain.Panel2.Controls.Add(StatusBar);
SplitMain.Size = new Size(1252, 691);
⋮----
// StatusBar
⋮----
StatusBar.Items.AddRange(new ToolStripItem[] { LblStatusServices, LblStatusSeparator, LblStatusServicesRunning });
StatusBar.Location = new Point(0, 104);
⋮----
StatusBar.Size = new Size(1252, 22);
⋮----
// LblStatusServices
⋮----
LblStatusServices.Image = (Image)resources.GetObject("LblStatusServices.Image");
⋮----
LblStatusServices.Size = new Size(120, 17);
⋮----
// LblStatusSeparator
⋮----
LblStatusSeparator.Image = (Image)resources.GetObject("LblStatusSeparator.Image");
⋮----
LblStatusSeparator.Size = new Size(16, 17);
⋮----
// LblStatusServicesRunning
⋮----
LblStatusServicesRunning.Image = (Image)resources.GetObject("LblStatusServicesRunning.Image");
⋮----
LblStatusServicesRunning.Size = new Size(126, 17);
⋮----
// FormMain
⋮----
AutoScaleDimensions = new SizeF(7F, 15F);
⋮----
ClientSize = new Size(1252, 691);
Controls.Add(SplitMain);
Icon = (Icon)resources.GetObject("$this.Icon");
⋮----
tableLayoutPanel2.ResumeLayout(false);
tableLayoutPanel3.ResumeLayout(false);
tableLayoutPanel3.PerformLayout();
tableLayoutPanelFilter.ResumeLayout(false);
tableLayoutPanelFilter.PerformLayout();
((System.ComponentModel.ISupportInitialize)GridServs).EndInit();
((System.ComponentModel.ISupportInitialize)serviceBindingSource).EndInit();
SplitMain.Panel1.ResumeLayout(false);
SplitMain.Panel2.ResumeLayout(false);
SplitMain.Panel2.PerformLayout();
((System.ComponentModel.ISupportInitialize)SplitMain).EndInit();
SplitMain.ResumeLayout(false);
StatusBar.ResumeLayout(false);
StatusBar.PerformLayout();
⋮----
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
private DataGridViewTextBoxColumn ColDisplayName;
private DataGridViewTextBoxColumn ColServiceName;
private DataGridViewTextBoxColumn ColStatus;
private DataGridViewTextBoxColumn ColStartMode;
private DataGridViewTextBoxColumn ColServiceType;
private DataGridViewCheckBoxColumn ColCanPauseAndContinue;
private DataGridViewCheckBoxColumn ColCanShutdown;
private DataGridViewCheckBoxColumn ColCanStop;
private DataGridViewTextBoxColumn ColPath;
private Button BtnCancel;
private CheckBox ChkStartAsAdm;
private StatusStrip StatusBar;
private ToolStripStatusLabel LblStatusServices;
private ToolStripStatusLabel LblStatusServicesRunning;
private ToolStripStatusLabel LblStatusSeparator;
```

## File: WinServicesTool/Forms/FormMain.cs
```csharp
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
private readonly IServiceOperationOrchestrator _orchestrator;
private readonly IRegistryService _registryService;
private readonly IRegistryEditor _registryEditor;
/// <summary>
/// Delegate used to open Regedit for a given registry path. Tests can replace this delegate
/// to avoid starting external processes.
/// </summary>
⋮----
// Show the app name and version in the title bar
⋮----
// Ensure Cancel button starts disabled
⋮----
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
private async void AppConfigChanged(object? sender, PropertyChangedEventArgs e)
⋮----
_appConfig.Save();
// Handle dynamic column visibility changes
⋮----
// Designer-based filter controls are wired in constructor
private async void FormPrincipal_Load(object? sender, EventArgs e)
⋮----
if (!_privilegeService.IsAdministrator())
⋮----
// Apply column visibility from config
⋮----
private void FormPrincipal_Shown(object? sender, EventArgs e)
⋮----
// Restore window position/size/state from config
⋮----
Bounds = new Rectangle(_appConfig.WindowLeft, _appConfig.WindowTop, _appConfig.WindowWidth, _appConfig.WindowHeight);
⋮----
if (!string.IsNullOrEmpty(_appConfig.WindowState) && Enum.TryParse<FormWindowState>(_appConfig.WindowState, out var ws))
⋮----
// Restore splitter distance if available
⋮----
// ignore invalid splitter values
⋮----
// ignore restore failures
⋮----
private void UpdateFilterLists()
⋮----
// Preserve previous selections
⋮----
CbFilterStatus.Items.Clear();
CbFilterStatus.Items.Add("All");
foreach (var st in _allServices.Select(s => s.Status).Distinct().Order())
CbFilterStatus.Items.Add(st.ToString());
if (!string.IsNullOrEmpty(prevStatus) && CbFilterStatus.Items.Contains(prevStatus))
⋮----
CbFilterStartMode.Items.Clear();
CbFilterStartMode.Items.Add("All");
foreach (var sm in _allServices.Select(static s => s.StartMode).Distinct().Order())
CbFilterStartMode.Items.Add(sm.ToString());
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
private void BtnCancel_Click(object? sender, EventArgs e)
⋮----
_currentOperationCts.Cancel();
⋮----
_logger.LogError(ex, "Error while cancelling operation");
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
private void GridServs_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
⋮----
// Handle right-click for column visibility
⋮----
// Build context menu for column visibility
⋮----
var chooseColumnsItem = new ToolStripMenuItem("Choose Visible Columns...") { Enabled = true };
⋮----
menu.Items.Add(chooseColumnsItem);
⋮----
var screenPoint = GridServs.PointToScreen(new Point(e.X, e.Y));
menu.Show(screenPoint);
⋮----
_logger.LogError(ex, "Error showing column header context menu");
⋮----
// Handle left-click for sorting
⋮----
var propName = !string.IsNullOrEmpty(col.DataPropertyName)
⋮----
// cycle: None -> Asc -> Desc -> None
⋮----
// clear property when cycling back to no-sort
⋮----
private async Task ShowColumnChooserDialogAsync()
⋮----
// Get currently visible columns
⋮----
using var dlg = new FormColumnChooser([.. GridServs.Columns.Cast<DataGridViewColumn>()], visibleColumns);
if (await dlg.ShowDialogAsync(this) != DialogResult.OK)
⋮----
// Update config with selected columns
⋮----
// Apply visibility
⋮----
_logger.LogError(ex, "Error showing column chooser dialog");
⋮----
private async Task LoadServicePathsAsync()
⋮----
// Get services that don't have paths loaded yet
var servicesToUpdate = _servicesList.Where(s => string.IsNullOrEmpty(s.Path)).ToList();
⋮----
// Run the path loading in a background thread to avoid UI freezing
await Task.Run(() =>
⋮----
using var pathHelper = new ServicePathHelper();
⋮----
service.Path = pathHelper.GetExecutablePath(service.ServiceName) ?? string.Empty;
⋮----
_logger.LogError(ex, "Failed to get path for service {ServiceName}", service.ServiceName);
⋮----
// Notify the binding source that data changed
this.InvokeIfRequired(() =>
⋮----
serviceBindingSource.ResetBindings(false);
⋮----
_logger.LogError(ex, "Error loading service paths");
⋮----
private async Task ApplyColumnVisibilityAsync()
⋮----
col.Visible = visibleColumns.Contains(col.Name);
⋮----
_logger.LogError(ex, "Error applying column visibility");
⋮----
internal void OpenServiceInRegistry(string serviceName)
⋮----
// Registry path for the service
⋮----
_registryService.SetRegeditLastKey(registryPath);
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
// Update the filter dropdowns to show only values present in the loaded list
⋮----
// Now populate the grid with data
⋮----
MessageBox.Show(this, $"Failed to load services: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
⋮----
_logger.LogError(ex, "Failed to load services");
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
working = [.. working.Where(s => s.Status == parsedStatus)];
if (CbFilterStartMode.SelectedItem is string startSel && !string.Equals(startSel, "All", StringComparison.OrdinalIgnoreCase))
⋮----
working = [.. working.Where(s => s.StartMode == parsedStart)];
⋮----
// swallow filter errors — filters are convenience UI only
⋮----
// Apply sorting if requested
if (!string.IsNullOrEmpty(_sortPropertyName) && _sortOrder != SortOrder.None)
⋮----
var prop = typeof(Service).GetProperty(_sortPropertyName!);
⋮----
? [.. working.OrderBy(s => prop.GetValue(s, null))]
: [.. working.OrderByDescending(s => prop.GetValue(s, null))];
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
// Ensure the DataGridView repaints so the sort glyph is shown/cleared immediately
GridServs.Refresh();
// If we have a sorted column, invalidate its header to ensure glyph is painted
⋮----
.ToList()
.FindIndex(c => c.DataPropertyName == _sortPropertyName || c.Name == _sortPropertyName);
⋮----
GridServs.InvalidateColumn(idx);
// additional aggressive repaints
GridServs.Invalidate();
GridServs.Update();
// clear any selection that might leave header visually selected
⋮----
private void _servicesList_ListChanged(object? sender, ListChangedEventArgs e)
⋮----
private void UpdateServiceStatusLabels()
⋮----
LblStatusServicesRunning.Text = $"Running: {_servicesList.Count(s => s.Status == ServiceControllerStatus.Running)}";
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
_registryEditor.SetDwordInLocalMachine(subKey, "Start", startValue);
⋮----
results.Add(okMsg);
⋮----
results.Add(err);
⋮----
// Refresh list on UI thread and show summary
⋮----
var summary = string.Join(Environment.NewLine, results);
MessageBox.Show(this, summary, $"Start Type changed to \"{newMode}\".", MessageBoxButtons.OK, MessageBoxIcon.Information);
⋮----
private static int StartModeToDword(string mode)
⋮----
private void GridServs_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
⋮----
// Color the entire row by status
⋮----
ServiceControllerStatus.Running => Color.FromArgb(230, 255, 230), // light green
ServiceControllerStatus.Stopped => Color.FromArgb(255, 230, 230), // light red
ServiceControllerStatus.Paused => Color.FromArgb(255, 255, 230),  // light yellow
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
// ReSharper disable once MethodHasAsyncOverload
⋮----
_currentOperationCts = new CancellationTokenSource();
⋮----
var results = await _orchestrator.StartServicesAsync(selectedServices, _currentOperationCts.Token);
⋮----
if (results.TryGetValue(serv.ServiceName, out var ok) && ok)
⋮----
_logger.LogInformation("Started service {ServiceName}", serv.ServiceName);
⋮----
_logger.LogError("Failed to start service {ServiceName}", serv.ServiceName);
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
var results = await _orchestrator.StopServicesAsync(selectedServices, _currentOperationCts.Token);
⋮----
_logger.LogInformation("Stopped service {ServiceName}", serv.ServiceName);
⋮----
_logger.LogError("Failed to stop service {ServiceName}", serv.ServiceName);
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
var results = await _orchestrator.RestartServicesAsync(sel, _currentOperationCts.Token);
⋮----
if (results.TryGetValue(s.ServiceName, out var ok) && ok)
⋮----
_logger.LogInformation("Restarted service {ServiceName}", s.ServiceName);
⋮----
_logger.LogError("Failed to restart service {ServiceName}", s.ServiceName);
⋮----
private void FormPrincipal_FormClosing(object? sender, FormClosingEventArgs e)
⋮----
// Save current window bounds/state to config
⋮----
_appConfig.WindowState = WindowState.ToString();
⋮----
_logger.LogError(ex, "Failed to save window bounds");
⋮----
private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
⋮----
TxtFilter.Focus();
⋮----
TxtFilter.Clear();
```
