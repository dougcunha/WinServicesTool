using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Shouldly;
using WinServicesTool.Services;
using Xunit;

namespace WinServicesTool.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class ServiceOperationOrchestratorTests
{
    [Fact]
    public async Task StartServices_WhenSomeFail_ReturnsCorrectMap()
    {
        // Arrange
        var serviceHelper = Substitute.For<IServicePathHelper>();
        serviceHelper.StartServiceAsync("ok").Returns(Task.CompletedTask);
        serviceHelper.When(x => x.StartServiceAsync("bad")).Do(_ => throw new Exception("fail"));

        var orchestrator = new ServiceOperationOrchestrator(serviceHelper, NullLogger<ServiceOperationOrchestrator>.Instance);

        var services = new List<ServiceConfiguration>
        {
            new() { ServiceName = "ok", DisplayName = "OK", CurrentState = ServiceState.Stopped },
            new() { ServiceName = "bad", DisplayName = "Bad", CurrentState = ServiceState.Stopped }
        };

        // Act
        var results = await orchestrator.StartServicesAsync(services);

        // Assert
        results.Count.ShouldBe(2);
        results["ok"].ShouldBeTrue();
        results["bad"].ShouldBeFalse();
    }
}
