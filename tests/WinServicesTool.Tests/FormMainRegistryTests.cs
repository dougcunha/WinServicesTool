using Microsoft.Extensions.Logging;
using NSubstitute;
using WinServicesTool.Forms;
using WinServicesTool.Services;
using WinServicesTool.Utils;
using Xunit;

namespace WinServicesTool.Tests;

public sealed class FormMainRegistryTests
{
    [Fact]
    public void OpenServiceInRegistry_Invokes_RegistryService_With_Correct_Path()
    {
        // Arrange
        var logger = Substitute.For<ILogger<FormMain>>();
        var serviceHelper = Substitute.For<IServicePathHelper>();
        var priv = Substitute.For<IPrivilegeService>();
        var orchestrator = Substitute.For<IServiceOperationOrchestrator>();
        var registry = Substitute.For<IRegistryService>();
        var registryEditor = Substitute.For<IRegistryEditor>();

        var cfg = new AppConfig();

        // Create FormMain with mocked dependencies
        var form = new FormMain(logger, serviceHelper, priv, orchestrator, registry, registryEditor, cfg);

        var serviceName = "TestServiceName";
        var expectedPath = $"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\{serviceName}";

        // Act
        form.OpenServiceInRegistry(serviceName);

        // Assert: registry service was asked to open the expected path
        registry.Received(1).SetRegeditLastKey(expectedPath);
    }
}
