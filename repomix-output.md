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
tests/WinServicesTool.Tests/WinServicesTool.Tests.csproj
WinServicesTool.slnx
WinServicesTool/Extensions/ControlExtensions.cs
WinServicesTool/FodyWeavers.xml
WinServicesTool/Forms/FormChangeStartMode.cs
WinServicesTool/Forms/FormColumnChooser.cs
WinServicesTool/Forms/FormEditService.cs
WinServicesTool/Forms/FormMain.cs
WinServicesTool/Forms/FormMain.Designer.cs
WinServicesTool/Models/ServiceTypeEx.cs
WinServicesTool/nlog.config
WinServicesTool/Program.cs
WinServicesTool/Properties/AssemblyInternals.cs
WinServicesTool/Properties/Resources.Designer.cs
WinServicesTool/Services/IPrivilegeService.cs
WinServicesTool/Services/IProcessLauncher.cs
WinServicesTool/Services/IRegistryEditor.cs
WinServicesTool/Services/IRegistryService.cs
WinServicesTool/Services/IServiceOperationOrchestrator.cs
WinServicesTool/Services/IServicePathHelper.cs
WinServicesTool/Services/PrivilegeService.cs
WinServicesTool/Services/ProcessLauncher.cs
WinServicesTool/Services/RegistryEditor.cs
WinServicesTool/Services/RegistryService.cs
WinServicesTool/Services/ServiceNativeHelper.cs
WinServicesTool/Services/ServiceOperationOrchestrator.cs
WinServicesTool/Services/ServicePathHelperFactory.cs
WinServicesTool/Utils/AppConfig.cs
WinServicesTool/Utils/ColumnHeaderHeightSettings.cs
WinServicesTool/WinServicesTool.csproj
```

# Files

## File: WinServicesTool/FodyWeavers.xml
```xml
<Weavers xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="FodyWeavers.xsd">
  <PropertyChanged />
</Weavers>
```

## File: WinServicesTool/Forms/FormEditService.cs
```csharp
/// <summary>
/// Form for editing service display name and description.
/// </summary>
public sealed class FormEditService : Form
⋮----
private readonly IRegistryService _registryService;
⋮----
private TextBox _txtDisplayName = null!;
private TextBox _txtDescription = null!;
private Button _btnOk = null!;
private Button _btnCancel = null!;
private Label _lblDisplayName = null!;
private Label _lblDescription = null!;
private Label _lblServiceName = null!;
⋮----
/// Gets the updated display name entered by the user.
⋮----
/// Gets the updated description entered by the user.
⋮----
private void InitializeComponent()
⋮----
// Form properties
⋮----
ClientSize = new Size(500, 290);
⋮----
// Service Name Label
_lblServiceName = new Label
⋮----
Location = new Point(12, 12),
Size = new Size(476, 20),
Font = new Font(Font, FontStyle.Bold)
⋮----
// Display Name Label
_lblDisplayName = new Label
⋮----
Location = new Point(12, 42),
⋮----
// Display Name TextBox
_txtDisplayName = new TextBox
⋮----
Location = new Point(12, 65),
Size = new Size(476, 23),
⋮----
// Description Label
_lblDescription = new Label
⋮----
Location = new Point(12, 98),
⋮----
// Description TextBox
_txtDescription = new TextBox
⋮----
Location = new Point(12, 121),
Size = new Size(476, 115),
⋮----
// OK Button
_btnOk = new Button
⋮----
Location = new Point(332, 243),
Size = new Size(75, 38)
⋮----
// Cancel Button
_btnCancel = new Button
⋮----
Location = new Point(413, 243),
⋮----
// Add controls to form
Controls.Add(_lblServiceName);
Controls.Add(_lblDisplayName);
Controls.Add(_txtDisplayName);
Controls.Add(_lblDescription);
Controls.Add(_txtDescription);
Controls.Add(_btnOk);
Controls.Add(_btnCancel);
⋮----
private void InitializeControls(string serviceName, string currentDisplayName, string currentDescription)
⋮----
private async void BtnOk_Click(object? sender, EventArgs e)
⋮----
var displayName = _txtDisplayName.Text.Trim();
var description = _txtDescription.Text.Trim();
if (string.IsNullOrEmpty(displayName))
⋮----
MessageBox.Show
⋮----
await Task.Run(() => _registryService.UpdateServiceInfo(_serviceName, displayName, description));
⋮----
_logger.LogError(ex, "Failed to update service information for {ServiceName}", _serviceName);
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

## File: WinServicesTool/Services/IServicePathHelper.cs
```csharp
/// <summary>
/// Abstraction for querying and controlling Windows services using native APIs.
/// </summary>
public interface IServicePathHelper : IDisposable
⋮----
/// Gets the names of all services on the system.
⋮----
/// <returns>Array of service names</returns>
string[] GetAllServiceNames();
⋮----
/// Gets the complete configuration for the specified Windows service (synchronous).
/// For UI applications, prefer GetServiceConfigurationAsync.
⋮----
/// <param name="serviceName">The service name to query</param>
/// <returns>Service configuration or null if not found</returns>
ServiceConfiguration? GetServiceConfiguration(string serviceName);
⋮----
/// Gets the complete configuration for the specified Windows service asynchronously.
/// Offloads the Win32 calls to a thread pool thread to avoid blocking the UI.
⋮----
/// <param name="cancellationToken">Cancellation token</param>
⋮----
Task<ServiceConfiguration?> GetServiceConfigurationAsync(string serviceName, CancellationToken cancellationToken = default);
⋮----
/// Gets configurations for multiple services in parallel with progress reporting.
/// Ideal for listing all services in a UI application.
⋮----
/// <param name="serviceNames">Service names to query</param>
/// <param name="progress">Optional progress reporter (reports completed count)</param>
⋮----
/// <returns>Dictionary mapping service names to their configurations</returns>
Task<Dictionary<string, ServiceConfiguration?>> GetServiceConfigurationsAsync
⋮----
/// Gets configurations for ALL services on the system asynchronously with progress reporting.
/// Perfect for populating a service list UI.
⋮----
Task<Dictionary<string, ServiceConfiguration?>> GetAllServiceConfigurationsAsync
⋮----
/// Gets all service configurations as a list, ordered by display name.
⋮----
/// <returns>List of service configurations</returns>
Task<List<ServiceConfiguration>> GetServicesAsync(IProgress<int>? progress = null, CancellationToken cancellationToken = default);
⋮----
/// Starts the specified service and waits until it reaches the running state or timeout.
⋮----
/// <param name="serviceName">The service name to start</param>
⋮----
/// <exception cref="InvalidOperationException">Thrown when the service cannot be started</exception>
Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default);
⋮----
/// Stops the specified service and waits until it reaches the stopped state or timeout.
⋮----
/// <param name="serviceName">The service name to stop</param>
⋮----
/// <exception cref="InvalidOperationException">Thrown when the service cannot be stopped</exception>
Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default);
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

## File: WinServicesTool/Services/ServicePathHelperFactory.cs
```csharp
/// <summary>
/// Factory-based wrapper around <see cref="ServicePathHelper"/> to provide a scoped service
/// for dependency injection without holding persistent SCManager connections.
/// </summary>
public sealed class ServicePathHelperFactory(ILogger<ServicePathHelperFactory> logger) : IServicePathHelper
⋮----
public string[] GetAllServiceNames()
⋮----
using var helper = new ServicePathHelper();
return helper.GetAllServiceNames();
⋮----
public ServiceConfiguration? GetServiceConfiguration(string serviceName)
⋮----
return helper.GetServiceConfiguration(serviceName);
⋮----
public async Task<ServiceConfiguration?> GetServiceConfigurationAsync(
⋮----
return await helper.GetServiceConfigurationAsync(serviceName, cancellationToken);
⋮----
public async Task<Dictionary<string, ServiceConfiguration?>> GetServiceConfigurationsAsync(
⋮----
return await helper.GetServiceConfigurationsAsync(serviceNames, progress, cancellationToken);
⋮----
public async Task<Dictionary<string, ServiceConfiguration?>> GetAllServiceConfigurationsAsync(
⋮----
return await helper.GetAllServiceConfigurationsAsync(progress, cancellationToken);
⋮----
public async Task<List<ServiceConfiguration>> GetServicesAsync(IProgress<int>? progress = null, CancellationToken cancellationToken = default)
⋮----
var serviceNames = helper.GetAllServiceNames();
⋮----
var config = await helper.GetServiceConfigurationAsync(serviceName, cancellationToken);
Interlocked.Increment(ref completed);
⋮----
services.Add(config);
⋮----
return [.. services.OrderBy(s => s.DisplayName)];
⋮----
logger.LogError(ex, "Failed to enumerate services");
⋮----
public async Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default)
⋮----
await helper.StartServiceAsync(serviceName, cancellationToken);
⋮----
logger.LogError(ex, "Failed to start service {ServiceName}", serviceName);
⋮----
public async Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default)
⋮----
await helper.StopServiceAsync(serviceName, cancellationToken);
⋮----
logger.LogError(ex, "Failed to stop service {ServiceName}", serviceName);
⋮----
public void Dispose()
⋮----
// Nothing to dispose in factory pattern
```

## File: WinServicesTool/Utils/ColumnHeaderHeightSettings.cs
```csharp
/// <summary>
/// Contains configuration for automatic column header height calculation based on text wrapping.
/// </summary>
public static class ColumnHeaderHeightSettings
⋮----
/// Height for column headers displaying a single line of text.
⋮----
/// Height for column headers displaying two lines of text.
⋮----
/// Height for column headers displaying three or more lines of text.
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

## File: WinServicesTool/Services/IProcessLauncher.cs
```csharp
/// <summary>
/// Abstraction for launching processes so calls can be mocked in tests.
/// </summary>
public interface IProcessLauncher
⋮----
/// Starts a process given a filename and optional start info.
⋮----
// ReSharper disable once UnusedMethodReturnValue.Global
Process? Start(string fileName);
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

## File: tests/WinServicesTool.Tests/WinServicesTool.Tests.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0-windows7.0</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <IsPackable>false</IsPackable>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.0.0" />
    <PackageReference Include="xunit" Version="2.9.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.5">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="coverlet.collector" Version="6.0.4">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Shouldly" Version="4.3.0" />
    <PackageReference Include="NSubstitute" Version="5.3.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\WinServicesTool\WinServicesTool.csproj" />
  </ItemGroup>

</Project>
```

## File: WinServicesTool.slnx
```
<Solution>
  <Folder Name="/conf/">
    <File Path=".editorconfig" />
    <File Path=".repomixignore" />
    <File Path="README.md" />
    <File Path="repomix-output.md" />
    <File Path="repomix.config.json" />
  </Folder>
  <Folder Name="/tests/">
    <Project Path="tests/WinServicesTool.Tests/WinServicesTool.Tests.csproj" />
  </Folder>
  <Project Path="WinServicesTool/WinServicesTool.csproj" />
</Solution>
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
ChosenColumnNames = [.. _lstColumns.CheckedIndices.Cast<int>().Select(i => columns[i].Name)];
⋮----
Controls.Add(lbl);
Controls.Add(_lstColumns);
Controls.Add(btnSelectAll);
Controls.Add(btnDeselectAll);
Controls.Add(btnOk);
Controls.Add(btnCancel);
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
⋮----
/// Updates the display name and description of a Windows service in the registry.
⋮----
/// <param name="serviceName">The service name to update.</param>
/// <param name="displayName">The new display name.</param>
/// <param name="description">The new description.</param>
void UpdateServiceInfo(string serviceName, string displayName, string description);
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

# Microsoft .NET properties
csharp_new_line_before_members_in_object_initializers = true
csharp_style_namespace_declarations = file_scoped:warning

# ReSharper properties
resharper_blank_lines_around_single_line_type = 0
resharper_blank_lines_around_type = 1
resharper_blank_lines_before_block_statements = 1
resharper_blank_lines_before_control_transfer_statements = 1
resharper_blank_lines_before_multiline_statements = 1
resharper_blank_lines_before_single_line_comment = 1
resharper_braces_for_dowhile = required_for_multiline
resharper_braces_for_for = required_for_multiline
resharper_braces_for_foreach = required_for_multiline
resharper_braces_for_while = required_for_multiline
resharper_csharp_align_multiline_parameter = false
resharper_csharp_wrap_before_binary_opsign = true
resharper_csharp_wrap_multiple_declaration_style = chop_always
resharper_indent_preprocessor_if = usual_indent
resharper_indent_primary_constructor_decl_pars = inside
resharper_indent_raw_literal_string = indent
resharper_keep_existing_primary_constructor_declaration_parens_arrangement = false
resharper_keep_existing_switch_expression_arrangement = false
resharper_max_array_initializer_elements_on_line = 5
resharper_max_formal_parameters_on_line = 5
resharper_max_invocation_arguments_on_line = 5
resharper_max_primary_constructor_parameters_on_line = 3
resharper_place_linq_into_on_new_line = true
resharper_place_simple_anonymousmethod_on_single_line = false
resharper_place_single_method_argument_lambda_on_same_line = false
resharper_place_type_constraints_on_same_line = false
resharper_prefer_wrap_around_eq = avoid
resharper_trailing_comma_in_multiline_lists = true
resharper_treat_case_statement_with_break_as_simple = false
resharper_wrap_after_property_in_chained_method_calls = true
resharper_wrap_before_primary_constructor_declaration_lpar = true
resharper_wrap_before_primary_constructor_declaration_rpar = true
resharper_wrap_enum_declaration = chop_always
resharper_wrap_list_pattern = chop_if_long
resharper_wrap_object_and_collection_initializer_style = chop_always
resharper_wrap_verbatim_interpolated_strings = no_wrap

# ReSharper inspection severities
resharper_enforce_do_while_statement_braces_highlighting = suggestion
resharper_enforce_foreach_statement_braces_highlighting = suggestion
resharper_enforce_for_statement_braces_highlighting = suggestion
resharper_enforce_if_statement_braces_highlighting = suggestion
resharper_enforce_while_statement_braces_highlighting = suggestion

[*.md]
trim_trailing_whitespace = false
end_of_line = lf

[*.{json,xml,config,yml,yaml}]
indent_style = space
indent_size = 2
end_of_line = lf
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
var form = new FormMain(logger, serviceHelper, priv, orchestrator, registry, registryEditor, formEditServiceFactory, cfg);
⋮----
// Act
form.OpenServiceInRegistry(serviceName);
// Assert: registry service was asked to open the expected path
registry.Received(1).SetRegeditLastKey(expectedPath);
```

## File: tests/WinServicesTool.Tests/ServiceOperationOrchestratorAdditionalTests.cs
```csharp
public sealed class ServiceOperationOrchestratorAdditionalTests
⋮----
public async Task RestartServices_AllOk_ReturnsAllTrue()
⋮----
serviceHelper.StopServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
serviceHelper.StartServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
var orchestrator = new ServiceOperationOrchestrator(serviceHelper, NullLogger<ServiceOperationOrchestrator>.Instance);
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
serviceHelper.StartServiceAsync("ok").Returns(Task.CompletedTask);
serviceHelper.When(x => x.StartServiceAsync("bad")).Do(_ => throw new Exception("fail"));
var orchestrator = new ServiceOperationOrchestrator(serviceHelper, NullLogger<ServiceOperationOrchestrator>.Instance);
⋮----
// Act
var results = await orchestrator.StartServicesAsync(services);
// Assert
results.Count.ShouldBe(2);
results["ok"].ShouldBeTrue();
results["bad"].ShouldBeFalse();
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
Task<Dictionary<string, bool>> StartServicesAsync(IEnumerable<ServiceConfiguration> services, CancellationToken cancellationToken = default);
⋮----
/// Stops the provided services and returns a map of serviceName->success.
⋮----
Task<Dictionary<string, bool>> StopServicesAsync(IEnumerable<ServiceConfiguration> services, CancellationToken cancellationToken = default);
⋮----
/// Restarts the provided services (stop then start) and returns a map of serviceName->success.
⋮----
Task<Dictionary<string, bool>> RestartServicesAsync(IEnumerable<ServiceConfiguration> services, CancellationToken cancellationToken = default);
```

## File: WinServicesTool/Services/RegistryService.cs
```csharp
/// <summary>
/// Concrete implementation of <see cref="IRegistryService"/> which performs small
/// registry writes and launches regedit.exe so the UI can navigate to a service key.
/// </summary>
public sealed class RegistryService(IRegistryEditor editor, IProcessLauncher launcher) : IRegistryService
⋮----
/// <inheritdoc />
public void SetRegeditLastKey(string registryPath)
⋮----
if (string.IsNullOrEmpty(registryPath))
throw new ArgumentException("registryPath must be provided", nameof(registryPath));
editor.SetLastKey(registryPath);
// Give the registry a short moment to persist before launching regedit
Thread.Sleep(100);
launcher.Start("regedit.exe");
⋮----
public void UpdateServiceInfo(string serviceName, string displayName, string description)
⋮----
if (string.IsNullOrEmpty(serviceName))
throw new ArgumentException("serviceName must be provided", nameof(serviceName));
if (string.IsNullOrEmpty(displayName))
throw new ArgumentException("displayName must be provided", nameof(displayName));
⋮----
using var key = Registry.LocalMachine.OpenSubKey(keyPath, writable: true);
⋮----
throw new InvalidOperationException($"Service '{serviceName}' not found in registry.");
key.SetValue("DisplayName", displayName, RegistryValueKind.String);
if (!string.IsNullOrEmpty(description))
key.SetValue("Description", description, RegistryValueKind.String);
else if (key.GetValue("Description") != null)
key.DeleteValue("Description", throwOnMissingValue: false);
```

## File: WinServicesTool/Services/ServiceNativeHelper.cs
```csharp
// ReSharper disable PropertyCanBeMadeInitOnly.Global
⋮----
/// <summary>
/// Represents the complete configuration of a Windows service.
/// </summary>
public sealed partial class ServiceConfiguration : INotifyPropertyChanged
⋮----
// Status information
⋮----
// Control capabilities
⋮----
/// Gets a human-readable description of the service type for display purposes.
⋮----
=> ServiceType.Describe();
⋮----
/// Specifies when the service should be started.
⋮----
/// Specifies the severity of the error if the service fails to start.
⋮----
/// Specifies the current state of the service.
⋮----
/// Extension methods for <see cref="ServiceConfiguration"/>.
⋮----
public static class ServiceConfigurationExtensions
⋮----
/// Converts <see cref="ServiceState"/> to <see cref="System.ServiceProcess.ServiceControllerStatus"/>.
⋮----
public static System.ServiceProcess.ServiceControllerStatus ToServiceControllerStatus(this ServiceState state)
⋮----
/// Converts <see cref="StartType"/> to <see cref="System.ServiceProcess.ServiceStartMode"/>.
⋮----
public static System.ServiceProcess.ServiceStartMode ToServiceStartMode(this StartType startType)
⋮----
/// Gets the <see cref="System.ServiceProcess.ServiceControllerStatus"/> for this configuration.
⋮----
public static System.ServiceProcess.ServiceControllerStatus GetStatus(this ServiceConfiguration config)
=> config.CurrentState.ToServiceControllerStatus();
⋮----
/// Gets the <see cref="System.ServiceProcess.ServiceStartMode"/> for this configuration.
⋮----
public static System.ServiceProcess.ServiceStartMode GetStartMode(this ServiceConfiguration config)
=> config.StartType.ToServiceStartMode();
⋮----
/// Helper class to query Windows service configuration using native API.
/// Implements IDisposable to allow reusing the SCManager connection across multiple queries.
⋮----
public sealed class ServicePathHelper : IServicePathHelper
⋮----
private static extern nint OpenSCManager(string? machineName, string? databaseName, uint dwAccess);
⋮----
private static extern nint OpenService(nint hSCManager, string lpServiceName, uint dwDesiredAccess);
⋮----
private static extern bool QueryServiceConfig(nint hService, nint lpServiceConfig, uint cbBufSize, out uint pcbBytesNeeded);
⋮----
private static extern bool QueryServiceConfig2(nint hService, uint dwInfoLevel, nint lpBuffer, uint cbBufSize, out uint pcbBytesNeeded);
⋮----
private static extern bool QueryServiceStatusEx(nint hService, int infoLevel, nint lpBuffer, uint cbBufSize, out uint pcbBytesNeeded);
⋮----
private static extern bool EnumServicesStatusEx
⋮----
private static extern bool CloseServiceHandle(nint hSCObject);
⋮----
private static extern bool StartService(nint hService, uint dwNumServiceArgs, nint lpServiceArgVectors);
⋮----
private static extern bool ControlService(nint hService, uint dwControl, ref ServiceStatus lpServiceStatus);
⋮----
private const uint SERVICE_WIN32 = 0x00000030; // WIN32_OWN_PROCESS | WIN32_SHARE_PROCESS
private const uint SERVICE_DRIVER = 0x0000000B; // KERNEL_DRIVER | FILE_SYSTEM_DRIVER
⋮----
// Service control acceptance flags
⋮----
// Service control codes
⋮----
public IntPtr lpBinaryPathName;
public IntPtr lpLoadOrderGroup;
⋮----
public IntPtr lpDependencies;
public IntPtr lpServiceStartName;
public IntPtr lpDisplayName;
⋮----
public IntPtr lpDescription;
⋮----
public IntPtr lpServiceName;
⋮----
public ServiceStatusProcess ServiceStatusProcess;
⋮----
private readonly SemaphoreSlim _semaphore;
⋮----
/// Initializes a new instance of the <see cref="ServicePathHelper"/> class and opens the SCManager connection.
⋮----
/// <param name="maxConcurrency">Maximum number of concurrent service queries (default: 10)</param>
/// <exception cref="Win32Exception">Thrown when OpenSCManager fails.</exception>
⋮----
throw new Win32Exception(Marshal.GetLastWin32Error(), "OpenSCManager failed");
_semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
⋮----
/// Gets the names of all services on the system.
⋮----
/// <returns>Array of service names</returns>
public string[] GetAllServiceNames()
⋮----
ObjectDisposedException.ThrowIf(_disposed, this);
⋮----
var lastError = Marshal.GetLastWin32Error();
⋮----
var buffer = Marshal.AllocHGlobal((int)bytesNeeded);
⋮----
var serviceName = Marshal.PtrToStringUni(service.lpServiceName);
if (!string.IsNullOrEmpty(serviceName))
services.Add(serviceName);
current = nint.Add(current, structSize);
⋮----
Marshal.FreeHGlobal(buffer);
⋮----
/// Gets the complete configuration for the specified Windows service (synchronous).
/// For UI applications, prefer GetServiceConfigurationAsync.
⋮----
public ServiceConfiguration? GetServiceConfiguration(string serviceName)
⋮----
return null; // Service not found or access denied
⋮----
if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
⋮----
var binaryPath = Marshal.PtrToStringUni(config.lpBinaryPathName);
var expandedPath = Environment.ExpandEnvironmentVariables(binaryPath ?? "");
⋮----
? Marshal.PtrToStringUni(config.lpLoadOrderGroup)
⋮----
var dep = Marshal.PtrToStringUni(config.lpDependencies + offset * 2);
if (string.IsNullOrEmpty(dep))
⋮----
depList.Add(dep);
⋮----
? Marshal.PtrToStringUni(config.lpServiceStartName)
⋮----
? Marshal.PtrToStringUni(config.lpDisplayName)
⋮----
return new ServiceConfiguration
⋮----
/// Gets the complete configuration for the specified Windows service asynchronously.
/// Offloads the Win32 calls to a thread pool thread to avoid blocking the UI.
⋮----
public async Task<ServiceConfiguration?> GetServiceConfigurationAsync(
⋮----
await _semaphore.WaitAsync(cancellationToken);
⋮----
return await Task.Run(() => GetServiceConfiguration(serviceName), cancellationToken);
⋮----
_semaphore.Release();
⋮----
/// Gets configurations for multiple services in parallel with progress reporting.
/// Ideal for listing all services in a UI application.
⋮----
/// <param name="serviceNames">Service names to query</param>
/// <param name="progress">Optional progress reporter (reports completed count)</param>
/// <param name="cancellationToken">Cancellation token</param>
/// <returns>Dictionary mapping service names to their configurations</returns>
public async Task<Dictionary<string, ServiceConfiguration?>> GetServiceConfigurationsAsync
⋮----
var serviceList = serviceNames.ToList();
⋮----
var tasks = serviceList.Select(async serviceName =>
⋮----
Interlocked.Increment(ref completed);
⋮----
var configurations = await Task.WhenAll(tasks);
⋮----
/// Gets configurations for ALL services on the system asynchronously with progress reporting.
/// Perfect for populating a service list UI.
⋮----
public async Task<Dictionary<string, ServiceConfiguration?>> GetAllServiceConfigurationsAsync(
⋮----
var serviceNames = await Task.Run(GetAllServiceNames, cancellationToken);
⋮----
private static bool QueryDelayedAutoStart(nint serviceHandle)
⋮----
var buffer = Marshal.AllocHGlobal((int)bufferSize);
⋮----
private static string? QueryServiceDescription(nint serviceHandle)
⋮----
? Marshal.PtrToStringUni(descInfo.lpDescription)
⋮----
private static ServiceStatusProcess QueryServiceStatus(nint serviceHandle)
⋮----
/// Starts the specified service and waits until it reaches the running state or timeout.
⋮----
/// <param name="serviceName">The service name to start</param>
⋮----
/// <exception cref="InvalidOperationException">Thrown when the service cannot be started</exception>
public async Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default)
⋮----
await Task.Run(() =>
⋮----
cancellationToken.ThrowIfCancellationRequested();
⋮----
throw new InvalidOperationException($"Failed to open service '{serviceName}'. Error: {Marshal.GetLastWin32Error()}");
⋮----
var error = Marshal.GetLastWin32Error();
throw new InvalidOperationException($"Failed to start service '{serviceName}'. Error: {error}");
⋮----
var timeout = TimeSpan.FromSeconds(10);
var sw = System.Diagnostics.Stopwatch.StartNew();
⋮----
Thread.Sleep(200);
⋮----
throw new TimeoutException($"Service '{serviceName}' did not start within the timeout period.");
⋮----
/// Stops the specified service and waits until it reaches the stopped state or timeout.
⋮----
/// <param name="serviceName">The service name to stop</param>
⋮----
/// <exception cref="InvalidOperationException">Thrown when the service cannot be stopped</exception>
public async Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default)
⋮----
var serviceStatus = new ServiceStatus();
⋮----
throw new InvalidOperationException($"Failed to stop service '{serviceName}'. Error: {error}");
⋮----
throw new TimeoutException($"Service '{serviceName}' did not stop within the timeout period.");
⋮----
/// Gets all service configurations as a list, ordered by display name.
⋮----
/// <returns>List of service configurations</returns>
public async Task<List<ServiceConfiguration>> GetServicesAsync(IProgress<int>? progress = null, CancellationToken cancellationToken = default)
⋮----
return await Task.Run(() =>
⋮----
services.Add(config);
⋮----
// Ignora serviços que não podem ser consultados
⋮----
public void Dispose()
⋮----
_semaphore.Dispose();
```

## File: WinServicesTool/Models/ServiceTypeEx.cs
```csharp
public static class ServiceTypeHelper
⋮----
public static string Describe(this ServiceTypeEx rawType)
⋮----
where rawType.HasFlag(flag)
select flag.ToString()).ToList();
⋮----
? string.Join(" + ", parts)
```

## File: WinServicesTool/Services/ServiceOperationOrchestrator.cs
```csharp
/// <summary>
/// Default implementation of <see cref="IServiceOperationOrchestrator"/>.
/// </summary>
public sealed class ServiceOperationOrchestrator(IServicePathHelper serviceHelper, ILogger<ServiceOperationOrchestrator> logger) : IServiceOperationOrchestrator
⋮----
public async Task<Dictionary<string, bool>> StartServicesAsync(IEnumerable<ServiceConfiguration> services, CancellationToken cancellationToken = default)
⋮----
cancellationToken.ThrowIfCancellationRequested();
⋮----
await serviceHelper.StartServiceAsync(s.ServiceName, cancellationToken);
⋮----
logger.LogInformation("Started service {ServiceName}", s.ServiceName);
⋮----
logger.LogInformation("Start cancelled for {ServiceName}", s.ServiceName);
⋮----
logger.LogError(ex, "Failed to start service {ServiceName}", s.ServiceName);
⋮----
public async Task<Dictionary<string, bool>> StopServicesAsync(IEnumerable<ServiceConfiguration> services, CancellationToken cancellationToken = default)
⋮----
await serviceHelper.StopServiceAsync(s.ServiceName, cancellationToken);
⋮----
logger.LogInformation("Stopped service {ServiceName}", s.ServiceName);
⋮----
logger.LogInformation("Stop cancelled for {ServiceName}", s.ServiceName);
⋮----
logger.LogError(ex, "Failed to stop service {ServiceName}", s.ServiceName);
⋮----
public async Task<Dictionary<string, bool>> RestartServicesAsync(IEnumerable<ServiceConfiguration> services, CancellationToken cancellationToken = default)
⋮----
logger.LogInformation("Restarted service {ServiceName}", s.ServiceName);
⋮----
logger.LogInformation("Restart cancelled for {ServiceName}", s.ServiceName);
⋮----
logger.LogError(ex, "Failed to restart service {ServiceName}", s.ServiceName);
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
logger.LogWarning(ex, "Failed to determine administrator status");
⋮----
public void AskAndRestartAsAdmin(Form? owner, bool shouldAsk)
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
logger.LogError(ex, "Failed to relaunch elevated");
MessageBox.Show(owner, "Unable to start the application with elevated privileges.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
```

## File: WinServicesTool/WinServicesTool.csproj
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net9.0-windows</TargetFramework>
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
    <PackageReference Include="AsyncAwaitBestPractices" Version="9.0.0" />
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

## File: WinServicesTool/Utils/AppConfig.cs
```csharp
public sealed partial class AppConfig : INotifyPropertyChanged
⋮----
/// <summary>
/// Gets or sets the list of visible column names in the services grid.
/// </summary>
⋮----
/// Gets or sets the ordered list of column names representing their display order in the grid.
/// Column names are stored in the order they should appear from left to right.
⋮----
/// Gets or sets a dictionary mapping column names to their custom FillWeight values.
/// Only columns with user-modified widths are stored. Empty dictionary means all columns use default weights.
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
_cmb.Items.AddRange("Automatic", "Manual", "Disabled");
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

## File: WinServicesTool/Program.cs
```csharp
var services = new ServiceCollection();
⋮----
services.AddSingleton(_ => AppConfig.Load());
// Register FormEditService factory
⋮----
return new FormEditService(logger, registryService, serviceName, displayName, description);
⋮----
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
/// Required method for Designer support - do not modify
/// the contents of this method with the code editor.
⋮----
private void InitializeComponent()
⋮----
tableLayoutPanel2 = new TableLayoutPanel();
PnlActions = new TableLayoutPanel();
BtnRestartAsAdm = new Button();
Imgs = new ImageList(components);
ChkStartAsAdm = new CheckBox();
BtnLoad = new Button();
BtnStart = new Button();
BtnStop = new Button();
BtnRestart = new Button();
BtnChangeStartMode = new Button();
BtnCancel = new Button();
PnlFiltros = new TableLayoutPanel();
CbFilterStatus = new ComboBox();
CbFilterStartMode = new ComboBox();
TxtFilter = new TextBox();
LblFilterStatus = new Label();
LblStartMode = new Label();
BtnColumns = new Button();
GridServs = new DataGridView();
ColServiceName = new DataGridViewTextBoxColumn();
ColServiceStartName = new DataGridViewTextBoxColumn();
ColDisplayName = new DataGridViewTextBoxColumn();
ColDescription = new DataGridViewTextBoxColumn();
ColServiceType = new DataGridViewTextBoxColumn();
ColStartType = new DataGridViewTextBoxColumn();
ColErrorControl = new DataGridViewTextBoxColumn();
ColBinaryPathName = new DataGridViewTextBoxColumn();
ColLoadOrderGroup = new DataGridViewTextBoxColumn();
ColTagId = new DataGridViewTextBoxColumn();
ColIsDelayedAutoStart = new DataGridViewCheckBoxColumn();
ColCurrentState = new DataGridViewTextBoxColumn();
ColProcessId = new DataGridViewTextBoxColumn();
ColWin32ExitCode = new DataGridViewTextBoxColumn();
ColServiceSpecificExitCode = new DataGridViewTextBoxColumn();
ColCanStop = new DataGridViewCheckBoxColumn();
ColCanPauseAndContinue = new DataGridViewCheckBoxColumn();
ColCanShutdown = new DataGridViewCheckBoxColumn();
serviceBindingSource = new BindingSource(components);
TextLog = new RichTextBox();
MnuLog = new ContextMenuStrip(components);
clearLogToolStripMenuItem = new ToolStripMenuItem();
SplitMain = new SplitContainer();
TabCtrl = new TabControl();
TabLog = new TabPage();
TabDetail = new TabPage();
PnlDetails = new TableLayoutPanel();
LblDependencies = new Label();
LblDescription = new Label();
LstDependencies = new ListView();
TextDescription = new TextBox();
StatusBar = new StatusStrip();
LblStatusServices = new ToolStripStatusLabel();
LblStatusSeparator = new ToolStripStatusLabel();
LblStatusServicesRunning = new ToolStripStatusLabel();
LblProgresso = new ToolStripStatusLabel();
ProgressBar = new ToolStripProgressBar();
tableLayoutPanel2.SuspendLayout();
PnlActions.SuspendLayout();
PnlFiltros.SuspendLayout();
((System.ComponentModel.ISupportInitialize)GridServs).BeginInit();
((System.ComponentModel.ISupportInitialize)serviceBindingSource).BeginInit();
MnuLog.SuspendLayout();
((System.ComponentModel.ISupportInitialize)SplitMain).BeginInit();
SplitMain.Panel1.SuspendLayout();
SplitMain.Panel2.SuspendLayout();
SplitMain.SuspendLayout();
TabCtrl.SuspendLayout();
TabLog.SuspendLayout();
TabDetail.SuspendLayout();
PnlDetails.SuspendLayout();
StatusBar.SuspendLayout();
⋮----
//
// tableLayoutPanel2
⋮----
tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
tableLayoutPanel2.Controls.Add(PnlActions, 0, 0);
tableLayoutPanel2.Controls.Add(PnlFiltros, 0, 1);
tableLayoutPanel2.Controls.Add(GridServs, 0, 2);
⋮----
tableLayoutPanel2.Location = new Point(0, 0);
⋮----
tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
tableLayoutPanel2.Size = new Size(1252, 561);
⋮----
// PnlActions
⋮----
PnlActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
⋮----
PnlActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 129F));
PnlActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 113F));
PnlActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
PnlActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 122F));
PnlActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
PnlActions.Controls.Add(BtnRestartAsAdm, 7, 0);
PnlActions.Controls.Add(ChkStartAsAdm, 8, 0);
PnlActions.Controls.Add(BtnLoad, 0, 0);
PnlActions.Controls.Add(BtnStart, 1, 0);
PnlActions.Controls.Add(BtnStop, 2, 0);
PnlActions.Controls.Add(BtnRestart, 3, 0);
PnlActions.Controls.Add(BtnChangeStartMode, 4, 0);
PnlActions.Controls.Add(BtnCancel, 5, 0);
⋮----
PnlActions.Location = new Point(3, 3);
⋮----
PnlActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
PnlActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
PnlActions.Size = new Size(1246, 44);
⋮----
// BtnRestartAsAdm
⋮----
BtnRestartAsAdm.Font = new Font("Segoe UI", 9F);
⋮----
BtnRestartAsAdm.Location = new Point(1007, 3);
⋮----
BtnRestartAsAdm.Size = new Size(116, 38);
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
Imgs.Images.SetKeyName(7, "shield-user.png");
Imgs.Images.SetKeyName(8, "columns-3-cog (1).png");
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
BtnLoad.Size = new Size(104, 38);
⋮----
// BtnStart
⋮----
BtnStart.Location = new Point(113, 3);
⋮----
BtnStart.Size = new Size(104, 38);
⋮----
// BtnStop
⋮----
BtnStop.Location = new Point(223, 3);
⋮----
BtnStop.Size = new Size(104, 38);
⋮----
// BtnRestart
⋮----
BtnRestart.Location = new Point(333, 3);
⋮----
BtnRestart.Size = new Size(104, 38);
⋮----
// BtnChangeStartMode
⋮----
BtnChangeStartMode.Location = new Point(443, 3);
⋮----
BtnChangeStartMode.Size = new Size(123, 38);
⋮----
// BtnCancel
⋮----
BtnCancel.Font = new Font("Segoe UI", 9F);
⋮----
BtnCancel.Location = new Point(572, 3);
⋮----
BtnCancel.Size = new Size(107, 38);
⋮----
// PnlFiltros
⋮----
PnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
PnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
PnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
⋮----
PnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
PnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
PnlFiltros.Controls.Add(CbFilterStatus, 1, 0);
PnlFiltros.Controls.Add(CbFilterStartMode, 3, 0);
PnlFiltros.Controls.Add(TxtFilter, 4, 0);
PnlFiltros.Controls.Add(LblFilterStatus, 0, 0);
PnlFiltros.Controls.Add(LblStartMode, 2, 0);
PnlFiltros.Controls.Add(BtnColumns, 5, 0);
⋮----
PnlFiltros.Location = new Point(3, 53);
⋮----
PnlFiltros.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
PnlFiltros.Size = new Size(1246, 34);
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
TxtFilter.Size = new Size(686, 25);
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
// BtnColumns
⋮----
BtnColumns.Location = new Point(1219, 3);
⋮----
BtnColumns.Size = new Size(24, 28);
⋮----
// GridServs
⋮----
GridServs.Columns.AddRange(new DataGridViewColumn[] { ColServiceName, ColServiceStartName, ColDisplayName, ColDescription, ColServiceType, ColStartType, ColErrorControl, ColBinaryPathName, ColLoadOrderGroup, ColTagId, ColIsDelayedAutoStart, ColCurrentState, ColProcessId, ColWin32ExitCode, ColServiceSpecificExitCode, ColCanStop, ColCanPauseAndContinue, ColCanShutdown });
⋮----
GridServs.Location = new Point(3, 93);
⋮----
GridServs.Size = new Size(1246, 465);
⋮----
// ColServiceName
⋮----
// ColServiceStartName
⋮----
// ColDisplayName
⋮----
// ColDescription
⋮----
// ColServiceType
⋮----
// ColStartType
⋮----
// ColErrorControl
⋮----
// ColBinaryPathName
⋮----
// ColLoadOrderGroup
⋮----
// ColTagId
⋮----
// ColIsDelayedAutoStart
⋮----
// ColCurrentState
⋮----
// ColProcessId
⋮----
// ColWin32ExitCode
⋮----
// ColServiceSpecificExitCode
⋮----
// ColCanStop
⋮----
// ColCanPauseAndContinue
⋮----
// ColCanShutdown
⋮----
// serviceBindingSource
⋮----
// TextLog
⋮----
TextLog.Location = new Point(3, 3);
TextLog.Margin = new Padding(10);
⋮----
TextLog.Size = new Size(1238, 70);
⋮----
// MnuLog
⋮----
MnuLog.Items.AddRange(new ToolStripItem[] { clearLogToolStripMenuItem });
⋮----
MnuLog.Size = new Size(122, 26);
⋮----
// clearLogToolStripMenuItem
⋮----
clearLogToolStripMenuItem.Size = new Size(121, 22);
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
SplitMain.Panel2.Controls.Add(TabCtrl);
SplitMain.Panel2.Controls.Add(StatusBar);
SplitMain.Size = new Size(1252, 691);
⋮----
// TabCtrl
⋮----
TabCtrl.Controls.Add(TabLog);
TabCtrl.Controls.Add(TabDetail);
⋮----
TabCtrl.Location = new Point(0, 0);
⋮----
TabCtrl.Size = new Size(1252, 104);
⋮----
// TabLog
⋮----
TabLog.Controls.Add(TextLog);
TabLog.Location = new Point(4, 24);
⋮----
TabLog.Padding = new Padding(3);
TabLog.Size = new Size(1244, 76);
⋮----
// TabDetail
⋮----
TabDetail.Controls.Add(PnlDetails);
TabDetail.Location = new Point(4, 24);
⋮----
TabDetail.Padding = new Padding(3);
TabDetail.Size = new Size(1244, 76);
⋮----
// PnlDetails
⋮----
PnlDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
⋮----
PnlDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
PnlDetails.Controls.Add(LblDependencies, 1, 0);
PnlDetails.Controls.Add(LblDescription, 0, 0);
PnlDetails.Controls.Add(LstDependencies, 1, 1);
PnlDetails.Controls.Add(TextDescription, 0, 1);
⋮----
PnlDetails.Location = new Point(3, 3);
⋮----
PnlDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
PnlDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
PnlDetails.Size = new Size(1238, 70);
⋮----
// LblDependencies
⋮----
LblDependencies.Location = new Point(622, 0);
⋮----
LblDependencies.Size = new Size(613, 20);
⋮----
// LblDescription
⋮----
LblDescription.Location = new Point(3, 0);
⋮----
LblDescription.Size = new Size(613, 20);
⋮----
// LstDependencies
⋮----
LstDependencies.Location = new Point(622, 23);
⋮----
LstDependencies.Size = new Size(613, 44);
⋮----
// TextDescription
⋮----
TextDescription.Location = new Point(3, 23);
⋮----
TextDescription.Size = new Size(613, 44);
⋮----
// StatusBar
⋮----
StatusBar.Items.AddRange(new ToolStripItem[] { LblStatusServices, LblStatusSeparator, LblStatusServicesRunning, LblProgresso, ProgressBar });
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
// LblProgresso
⋮----
LblProgresso.Size = new Size(473, 17);
⋮----
// ProgressBar
⋮----
ProgressBar.Size = new Size(500, 16);
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
PnlActions.ResumeLayout(false);
PnlActions.PerformLayout();
PnlFiltros.ResumeLayout(false);
PnlFiltros.PerformLayout();
((System.ComponentModel.ISupportInitialize)GridServs).EndInit();
((System.ComponentModel.ISupportInitialize)serviceBindingSource).EndInit();
MnuLog.ResumeLayout(false);
SplitMain.Panel1.ResumeLayout(false);
SplitMain.Panel2.ResumeLayout(false);
SplitMain.Panel2.PerformLayout();
((System.ComponentModel.ISupportInitialize)SplitMain).EndInit();
SplitMain.ResumeLayout(false);
TabCtrl.ResumeLayout(false);
TabLog.ResumeLayout(false);
TabDetail.ResumeLayout(false);
PnlDetails.ResumeLayout(false);
PnlDetails.PerformLayout();
StatusBar.ResumeLayout(false);
StatusBar.PerformLayout();
⋮----
private TableLayoutPanel tableLayoutPanel2;
⋮----
private BindingSource serviceBindingSource;
private TableLayoutPanel PnlActions;
private TableLayoutPanel PnlFiltros;
⋮----
private Button BtnStart;
private Button BtnStop;
private Button BtnRestart;
private Button BtnChangeStartMode;
private Button BtnLoad;
⋮----
private ImageList Imgs;
⋮----
private SplitContainer SplitMain;
private Label LblFilterStatus;
private Label LblStartMode;
private Button BtnCancel;
private CheckBox ChkStartAsAdm;
private StatusStrip StatusBar;
private ToolStripStatusLabel LblStatusServices;
private ToolStripStatusLabel LblStatusServicesRunning;
private ToolStripStatusLabel LblStatusSeparator;
private DataGridViewTextBoxColumn ColServiceName;
private DataGridViewTextBoxColumn ColServiceStartName;
private DataGridViewTextBoxColumn ColDisplayName;
private DataGridViewTextBoxColumn ColDescription;
private DataGridViewTextBoxColumn ColServiceType;
private DataGridViewTextBoxColumn ColStartType;
private DataGridViewTextBoxColumn ColErrorControl;
private DataGridViewTextBoxColumn ColBinaryPathName;
private DataGridViewTextBoxColumn ColLoadOrderGroup;
private DataGridViewTextBoxColumn ColTagId;
private DataGridViewCheckBoxColumn ColIsDelayedAutoStart;
private DataGridViewTextBoxColumn ColCurrentState;
private DataGridViewTextBoxColumn ColProcessId;
private DataGridViewTextBoxColumn ColWin32ExitCode;
private DataGridViewTextBoxColumn ColServiceSpecificExitCode;
private DataGridViewCheckBoxColumn ColCanStop;
private DataGridViewCheckBoxColumn ColCanPauseAndContinue;
private DataGridViewCheckBoxColumn ColCanShutdown;
private ToolStripStatusLabel LblProgresso;
private ToolStripProgressBar ProgressBar;
private Button BtnRestartAsAdm;
private Button BtnColumns;
private ContextMenuStrip MnuLog;
private ToolStripMenuItem clearLogToolStripMenuItem;
private Label LblDependencies;
private Label LblDescription;
private TextBox TextDescription;
```

## File: WinServicesTool/Forms/FormMain.cs
```csharp
// ReSharper disable AsyncVoidEventHandlerMethod
public sealed partial class FormMain : Form
⋮----
// ReSharper disable once UseRawString
⋮----
private static partial Regex RegexExtractPath();
// App configuration
private readonly AppConfig _appConfig;
// Timer used to debounce form resize events when AutoWidthColumns is enabled
⋮----
private SortOrder _sortOrder = SortOrder.None;
⋮----
private readonly IServicePathHelper _serviceHelper;
private readonly IPrivilegeService _privilegeService;
private readonly IServiceOperationOrchestrator _orchestrator;
private readonly IRegistryService _registryService;
private readonly IRegistryEditor _registryEditor;
⋮----
// Store original FillWeight values to detect user modifications
⋮----
// Flag to prevent saving column settings during initialization
⋮----
// Show the app name and version in the title bar
⋮----
_isRunningAsAdmin = privilegeService.IsAdministrator();
⋮----
// Ensure Cancel button starts disabled
⋮----
ChkStartAsAdm.DataBindings.Add("Checked", _appConfig, nameof(AppConfig.AlwaysStartsAsAdministrator), false, DataSourceUpdateMode.OnPropertyChanged);
// Make header selection color match header background so headers don't show as "selected" in blue
⋮----
hdrStyle.WrapMode = DataGridViewTriState.True; // Enable word wrap for multi-line headers
hdrStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center align headers vertically
⋮----
_privilegeService.AskAndRestartAsAdmin(this, shouldAsk: false);
⋮----
// Initialize columns before loading services
⋮----
RefreshServiceListAsync().SafeFireAndForget(x => _logger.LogError(x, "Failed to load services"));
⋮----
private void InitializeEventBindings()
⋮----
private void TabCtrl_Selected(object? sender, TabControlEventArgs e)
⋮----
private void BtnRestartAsAdm_Click(object? sender, EventArgs e)
=> _privilegeService.AskAndRestartAsAdmin(this, shouldAsk: false);
private void AppConfigChanged(object? sender, PropertyChangedEventArgs e)
⋮----
_appConfig.Save();
// Handle dynamic column visibility changes
⋮----
/// <summary>
/// Initializes column order, widths, and visibility from saved configuration.
/// Must be called before loading data to prevent incorrect saves during initialization.
/// </summary>
private void InitializeColumns()
⋮----
// Set flag to prevent saving during initialization
⋮----
// Allow saving after initialization is complete
⋮----
// Designer-based filter controls are wired in constructor
private void FormPrincipal_Load(object? sender, EventArgs e)
⋮----
// If columns weren't initialized in constructor (e.g., not admin on first run),
// initialize them now
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
foreach (var st in _allServices.Select(s => s.GetStatus()).Distinct().Order())
CbFilterStatus.Items.Add(st.ToString());
if (!string.IsNullOrEmpty(prevStatus) && CbFilterStatus.Items.Contains(prevStatus))
⋮----
CbFilterStartMode.Items.Clear();
CbFilterStartMode.Items.Add("All");
foreach (var sm in _allServices.Select(static s => s.GetStartMode()).Distinct().Order())
CbFilterStartMode.Items.Add(sm.ToString());
if (!string.IsNullOrEmpty(prevStart) && CbFilterStartMode.Items.Contains(prevStart))
⋮----
_logger.LogError(ex, "Failed to update filter lists");
⋮----
private void GridServs_SelectionChanged(object? sender, EventArgs e)
⋮----
var sel = GetSelectedServices().ToList();
⋮----
var statuses = sel.Select(s => s.GetStatus()).Distinct().ToList();
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
if (selected.All(s => s.GetStatus() == ServiceControllerStatus.Stopped))
⋮----
var startItem = new ToolStripMenuItem("Start") { Enabled = true };
⋮----
menu.Items.Add(startItem);
⋮----
// If any selected are running or paused, show Stop and Restart
if (selected.Any(s => s.GetStatus() is ServiceControllerStatus.Running or ServiceControllerStatus.Paused))
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
// Edit service info (only for single selection and admin mode)
⋮----
var editService = new ToolStripMenuItem("Edit Service Info...") { Enabled = _isRunningAsAdmin };
⋮----
menu.Items.Add(editService);
⋮----
// Go to registry (only for single selection)
⋮----
var goToRegistry = new ToolStripMenuItem("Go to Registry") { Enabled = true };
⋮----
menu.Items.Add(goToRegistry);
var openInExplorer = new ToolStripMenuItem("Open Service Path in Explorer") { Enabled = true };
⋮----
if (File.Exists(ExtractExePath(selected[0].BinaryPathName)))
menu.Items.Add(openInExplorer);
⋮----
// Show menu at cursor
menu.Show(GridServs, new Point(e.X, e.Y));
⋮----
_logger.LogError(ex, "Error showing context menu");
⋮----
/// Retorna o caminho do executável principal de uma string de comando de serviço.
⋮----
/// <param name="serviceCommand">Linha de comando completa.</param>
/// <returns>Caminho do executável principal.</returns>
private static string? ExtractExePath(string serviceCommand)
⋮----
if (string.IsNullOrWhiteSpace(serviceCommand))
⋮----
var match = RegexExtractPath().Match(serviceCommand);
⋮----
private void OpenSelectedServicePathOnExplorer(object? sender, EventArgs e)
⋮----
// Extract the first path from BinaryPathName (it may contain arguments)
⋮----
if (string.IsNullOrEmpty(path) || !File.Exists(path))
⋮----
_logger.LogDebug("Service executable path not found for {ServiceName}", selected[0].ServiceName);
⋮----
_logger.LogInformation("Opening Explorer at {Path}", path);
// Open Explorer and select the file
Process.Start("explorer.exe", $"/select,\"{path}\"");
⋮----
_logger.LogError(ex, "Failed to open service path for {ServiceName}", selected[0].ServiceName);
⋮----
/// Opens the Edit Service Info dialog for the specified service.
⋮----
/// <param name="service">The service to edit.</param>
private void EditServiceInfo(ServiceConfiguration service)
⋮----
MessageBox.Show(
⋮----
if (editForm.ShowDialog(this) != DialogResult.OK)
⋮----
// Update the service configuration in memory
⋮----
// Refresh the grid to show updated values
GridServs.Refresh();
⋮----
_logger.LogDebug("Service information updated for {ServiceName}", service.ServiceName);
⋮----
_logger.LogError(ex, "Failed to edit service info for {ServiceName}", service.ServiceName);
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
private void GridServs_ColumnWidthChanged(object? sender, DataGridViewColumnEventArgs e)
⋮----
private void GridServs_ColumnDisplayIndexChanged(object? sender, DataGridViewColumnEventArgs e)
⋮----
private async Task ShowColumnChooserDialogAsync()
⋮----
// Get currently visible columns
⋮----
using var dlg = new FormColumnChooser([.. GridServs.Columns.Cast<DataGridViewColumn>()], visibleColumns);
⋮----
if (await dlg.ShowDialogAsync(this) != DialogResult.OK)
⋮----
// Update config with selected columns
⋮----
// Apply visibility
⋮----
_logger.LogError(ex, "Error showing column chooser dialog");
⋮----
private void ApplyColumnVisibility()
⋮----
col.Visible = visibleColumns.Contains(col.Name);
⋮----
_logger.LogError(ex, "Error applying column visibility");
⋮----
/// Calculates and updates the column header height based on the maximum number of lines
/// in visible column headers.
⋮----
private void UpdateColumnHeaderHeight()
⋮----
// Skip during initialization to avoid conflicts with auto-resize
⋮----
using var g = GridServs.CreateGraphics();
⋮----
// Measure the header text to determine how many lines it needs
⋮----
if (string.IsNullOrEmpty(headerText))
⋮----
// Calculate available width for text (column width minus padding)
var availableWidth = col.Width - 10; // 10px padding
⋮----
var textSize = g.MeasureString(headerText, font, availableWidth);
var lineHeight = g.MeasureString("A", font).Height;
var lines = (int)Math.Ceiling(textSize.Height / lineHeight);
⋮----
// Ignore if operation cannot be performed during auto-resize
// This can happen during Fill mode adjustments
⋮----
// Set height based on maximum number of lines
⋮----
_logger.LogDebug("Column header height updated to {Height} for {MaxLines} lines", GridServs.ColumnHeadersHeight, maxLines);
⋮----
_logger.LogTrace("Column header height update deferred due to auto-resize");
⋮----
_logger.LogError(ex, "Error updating column header height");
⋮----
/// Saves the current display order of columns to the configuration.
⋮----
private void SaveColumnOrder()
⋮----
// Don't save during initialization
⋮----
// Sort columns by their DisplayIndex to get the actual display order
⋮----
.OrderBy(col => col.DisplayIndex)
.Select(col => col.Name)
.ToList();
⋮----
_logger.LogDebug("Saved column order: {ColumnOrder}", string.Join(", ", columnOrder));
⋮----
_logger.LogError(ex, "Error saving column order");
⋮----
/// Restores the display order of columns from the configuration.
⋮----
private void RestoreColumnOrder()
⋮----
_logger.LogDebug("No saved column order found");
⋮----
// Create a dictionary to quickly look up columns by name
⋮----
.ToDictionary(col => col.Name, col => col);
// Apply the saved order
⋮----
if (!columnDict.TryGetValue(columnName, out var column))
⋮----
// Handle any columns that weren't in the saved order (e.g., newly added columns)
foreach (var col in GridServs.Columns.Cast<DataGridViewColumn>().Where(col => !_appConfig.ColumnOrder.Contains(col.Name)))
⋮----
_logger.LogDebug("Restored column order from config");
⋮----
_logger.LogError(ex, "Error restoring column order");
⋮----
/// Captures the original FillWeight values of all columns.
/// Should be called once after the form is initialized and before user interactions.
⋮----
private void CaptureOriginalColumnWidths()
⋮----
_originalFillWeights.Clear();
⋮----
_logger.LogDebug("Captured original FillWeight values for {Count} columns", _originalFillWeights.Count);
⋮----
_logger.LogError(ex, "Error capturing original column widths");
⋮----
/// Saves the current FillWeight of columns that have been modified by the user.
/// Only stores columns with widths different from their original values.
⋮----
private void SaveColumnWidths()
⋮----
// Only save if the FillWeight has changed from the original
if (!_originalFillWeights.TryGetValue(col.Name, out var originalWeight))
⋮----
// Use a small tolerance for floating point comparison
if (Math.Abs(col.FillWeight - originalWeight) > 0.01f)
⋮----
_logger.LogDebug("Saved {Count} modified column widths", modifiedWidths.Count);
⋮----
_logger.LogError(ex, "Error saving column widths");
⋮----
/// Restores saved FillWeight values for columns that were previously modified.
⋮----
private void RestoreColumnWidths()
⋮----
_logger.LogDebug("No saved column widths found");
⋮----
// Temporarily disable auto-size to prevent the grid from adjusting other columns
⋮----
.FirstOrDefault(col => col.Name == columnName);
⋮----
_logger.LogTrace("Restored FillWeight {FillWeight} for column {ColumnName}", fillWeight, columnName);
⋮----
_logger.LogDebug("Restored {Count} column widths from config", _appConfig.ColumnFillWeights.Count);
⋮----
// Restore auto-size mode
⋮----
_logger.LogError(ex, "Error restoring column widths");
⋮----
internal void OpenServiceInRegistry(string serviceName)
⋮----
// Registry path for the service
⋮----
_registryService.SetRegeditLastKey(registryPath);
⋮----
_logger.LogError(ex, "Failed to open registry for service {ServiceName}", serviceName);
⋮----
private void AppendLog(string message)
⋮----
var ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
private void BtnLoad_Click(object? sender, EventArgs e)
=> RefreshServiceListAsync().SafeFireAndForget(x => _logger.LogError(x, "Failed to load services"));
private async Task RefreshServiceListAsync()
⋮----
var servicesNames = _serviceHelper.GetAllServiceNames();
⋮----
var allServices = await _serviceHelper.GetServiceConfigurationsAsync
⋮----
new Progress<int>(completed => this.InvokeIfRequired(() => ProgressBar.Value = completed)),
⋮----
_allServices = [.. allServices.Values.Where(s => s != null).Select(s => s!).OrderBy(s => s.DisplayName)];
// Update the filter dropdowns to show only values present in the loaded list
⋮----
// Now populate the grid with data
⋮----
// Update column header height after data is loaded and columns have their final widths
// Use a small delay to ensure all auto-resize operations are complete
await Task.Delay(50);
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
ApplyFilterWithDelayAsync(ct).SafeFireAndForget(x => _logger.LogError(x, "Error applying filter"));
⋮----
private async Task ApplyFilterWithDelayAsync(CancellationToken ct)
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
working = [.. working.Where(s => s.GetStatus() == parsedStatus)];
if (CbFilterStartMode.SelectedItem is string startSel && !string.Equals(startSel, "All", StringComparison.OrdinalIgnoreCase))
⋮----
working = [.. working.Where(s => s.GetStartMode() == parsedStart)];
⋮----
// swallow filter errors — filters are convenience UI only
⋮----
// Apply sorting if requested
if (!string.IsNullOrEmpty(_sortPropertyName) && _sortOrder != SortOrder.None)
⋮----
var prop = typeof(ServiceConfiguration).GetProperty(_sortPropertyName!);
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
serviceBindingSource.ResetBindings(false);
// Ensure the DataGridView repaints so the sort glyph is shown/cleared immediately
⋮----
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
private void ServicesList_ListChanged(object? sender, ListChangedEventArgs e)
⋮----
private void UpdateServiceStatusLabels()
⋮----
LblStatusServicesRunning.Text = $"Running: {_servicesList.Count(s => s.GetStatus() == ServiceControllerStatus.Running)}";
⋮----
private void BtnChangeStartMode_Click(object? sender, EventArgs e)
⋮----
var selecteds = GetSelectedServices().ToList();
⋮----
// Preselect current start mode from first selected service
var initial = selecteds[0].GetStartMode() switch
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
_logger.LogError(ex, "Failed to change StartType for service {ServiceName}", serv.ServiceName);
⋮----
// Refresh list on UI thread and show summary
this.InvokeIfRequired(() =>
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
row.DefaultCellStyle.BackColor = item.GetStatus() switch
⋮----
ServiceControllerStatus.Running => Color.FromArgb(230, 255, 230), // light green
ServiceControllerStatus.Stopped => Color.FromArgb(255, 230, 230), // light red
ServiceControllerStatus.Paused => Color.FromArgb(255, 255, 230),  // light yellow
⋮----
// For the Status column, prefix with an emoji
⋮----
e.Value = prefix + status.ToServiceControllerStatus();
⋮----
_logger.LogError(ex, "Error formatting cell");
⋮----
private IEnumerable<ServiceConfiguration> GetSelectedServices()
⋮----
.Select(r => r.DataBoundItem as ServiceConfiguration)
.Where(s => s != null)
⋮----
private ServiceConfiguration? GetFocusedService()
⋮----
private async void BtnStart_Click(object? sender, EventArgs e)
⋮----
var selectedServices = GetSelectedServices().ToList();
⋮----
// Only allow starting services that are stopped
if (selectedServices.Any(s => s.GetStatus() != ServiceControllerStatus.Stopped))
⋮----
MessageBox.Show(this, "Please select only services that are stopped to start.", "Start services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
⋮----
_logger.LogDebug("Start aborted: selection contains non-stopped services.");
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
if (selectedServices.Any(s => s.GetStatus() != ServiceControllerStatus.Running && s.GetStatus() != ServiceControllerStatus.Paused))
⋮----
MessageBox.Show(this, "Please select only services that are running or paused to stop.", "Stop services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
⋮----
_logger.LogDebug("Stop aborted: selection contains services not running/paused.");
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
if (sel.Any(s => s.GetStatus() != ServiceControllerStatus.Running && s.GetStatus() != ServiceControllerStatus.Paused))
⋮----
MessageBox.Show(this, "Please select only services that are running or paused to restart.", "Restart services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
// Save column widths before closing
⋮----
_logger.LogError(ex, "Failed to save window bounds");
⋮----
private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
⋮----
TxtFilter.Focus();
⋮----
TxtFilter.Clear();
⋮----
private async void BtnColumns_Click(object sender, EventArgs e)
⋮----
private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
=> TextLog.Clear();
private void GridServs_RowEnter(object sender, DataGridViewCellEventArgs e)
⋮----
LstDependencies.Items.Clear();
TextDescription.Clear();
⋮----
private void PopulateDependenciesList(ServiceConfiguration? service)
⋮----
foreach (var dep in service.Dependencies.Select(s => _allServices.Find(a => a.ServiceName == s)).Where(s => s is not null))
LstDependencies.Items.Add($"• {dep!.DisplayName} ({dep.ServiceName})");
```
