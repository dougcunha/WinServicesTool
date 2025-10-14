using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Shouldly;
using WinServicesTool.Services;
using Xunit;

namespace WinServicesTool.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class ServiceOperationOrchestratorAdditionalTests
{
    [Fact]
    public async Task RestartServices_AllOk_ReturnsAllTrue()
    {
        var serviceHelper = Substitute.For<IServicePathHelper>();
        serviceHelper.StopServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        serviceHelper.StartServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        var orchestrator = new ServiceOperationOrchestrator(serviceHelper, NullLogger<ServiceOperationOrchestrator>.Instance);

        var services = new List<ServiceConfiguration>
        {
            new() { ServiceName = "s1", DisplayName = "S1", CurrentState = ServiceState.Running },
            new() { ServiceName = "s2", DisplayName = "S2", CurrentState = ServiceState.Running }
        };

        var results = await orchestrator.RestartServicesAsync(services);

        results.Count.ShouldBe(2);
        results.Values.ShouldAllBe(v => v);
    }

    [Fact]
    public async Task StartServices_AllOk_ReturnsAllTrue()
    {
        var serviceHelper = Substitute.For<IServicePathHelper>();
        serviceHelper.StartServiceAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        var orchestrator = new ServiceOperationOrchestrator(serviceHelper, NullLogger<ServiceOperationOrchestrator>.Instance);

        var services = new List<ServiceConfiguration>
        {
            new() { ServiceName = "s1", DisplayName = "S1", CurrentState = ServiceState.Stopped },
            new() { ServiceName = "s2", DisplayName = "S2", CurrentState = ServiceState.Stopped }
        };

        var results = await orchestrator.StartServicesAsync(services);

        results.Count.ShouldBe(2);
        results.Values.ShouldAllBe(v => v);
    }
}
