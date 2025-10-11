using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
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
        var winSvcMgr = Substitute.For<IWindowsServiceManager>();
        var priv = Substitute.For<IPrivilegeService>();
        var orchestrator = Substitute.For<IServiceOperationOrchestrator>();
        var registry = Substitute.For<IRegistryService>();
        var registryEditor = Substitute.For<IRegistryEditor>();

        var cfg = new AppConfig();

        // Create FormMain with mocked dependencies
        var form = new FormMain(logger, winSvcMgr, priv, orchestrator, registry, registryEditor, cfg);

        // Use reflection to call the private method OpenServiceInRegistry
        var method = typeof(FormMain).GetMethod("OpenServiceInRegistry", BindingFlags.Instance | BindingFlags.NonPublic);
        method.ShouldNotBeNull();

        var serviceName = "TestServiceName";
        var expectedPath = $"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\{serviceName}";

        // Act
        method!.Invoke(form, new object[] { serviceName });

        // Assert: registry service was asked to open the expected path
        registry.Received(1).SetRegeditLastKey(expectedPath);
    }
}
