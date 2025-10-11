using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Shouldly;
using WinServicesTool.Models;
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
        var svcManager = Substitute.For<IWindowsServiceManager>();
        svcManager.StartServiceAsync("ok").Returns(Task.CompletedTask);
        svcManager.When(x => x.StartServiceAsync("bad")).Do(_ => throw new Exception("fail"));

        var orchestrator = new ServiceOperationOrchestrator(svcManager, NullLogger<ServiceOperationOrchestrator>.Instance);

        var services = new List<Service>
        {
            new Service { ServiceName = "ok", DisplayName = "OK", Status = System.ServiceProcess.ServiceControllerStatus.Stopped },
            new Service { ServiceName = "bad", DisplayName = "Bad", Status = System.ServiceProcess.ServiceControllerStatus.Stopped }
        };

        // Act
        var results = await orchestrator.StartServicesAsync(services);

        // Assert
        results.Count.ShouldBe(2);
        results["ok"].ShouldBeTrue();
        results["bad"].ShouldBeFalse();
    }
}
