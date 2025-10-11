using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Shouldly;
using WinServicesTool.Models;
using WinServicesTool.Services;
using Xunit;

namespace WinServicesTool.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class ServiceOperationOrchestratorAdditionalTests
{
    [Fact]
    public async Task RestartServices_AllOk_ReturnsAllTrue()
    {
        var svcManager = Substitute.For<IWindowsServiceManager>();
        svcManager.StopServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        svcManager.StartServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        var orchestrator = new ServiceOperationOrchestrator(svcManager, NullLogger<ServiceOperationOrchestrator>.Instance);

        var services = new List<Service>
        {
            new Service { ServiceName = "s1", DisplayName = "S1", Status = System.ServiceProcess.ServiceControllerStatus.Running },
            new Service { ServiceName = "s2", DisplayName = "S2", Status = System.ServiceProcess.ServiceControllerStatus.Running }
        };

        var results = await orchestrator.RestartServicesAsync(services);

        results.Count.ShouldBe(2);
        results.Values.ShouldAllBe(v => v);
    }

    [Fact]
    public async Task StartServices_AllOk_ReturnsAllTrue()
    {
        var svcManager = Substitute.For<IWindowsServiceManager>();
        svcManager.StartServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        var orchestrator = new ServiceOperationOrchestrator(svcManager, NullLogger<ServiceOperationOrchestrator>.Instance);

        var services = new List<Service>
        {
            new Service { ServiceName = "s1", DisplayName = "S1", Status = System.ServiceProcess.ServiceControllerStatus.Stopped },
            new Service { ServiceName = "s2", DisplayName = "S2", Status = System.ServiceProcess.ServiceControllerStatus.Stopped }
        };

        var results = await orchestrator.StartServicesAsync(services);

        results.Count.ShouldBe(2);
        results.Values.ShouldAllBe(v => v);
    }
}
