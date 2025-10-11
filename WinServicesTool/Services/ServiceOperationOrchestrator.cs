using Microsoft.Extensions.Logging;
using WinServicesTool.Models;

namespace WinServicesTool.Services;

/// <summary>
/// Default implementation of <see cref="IServiceOperationOrchestrator"/>.
/// </summary>
public sealed class ServiceOperationOrchestrator(IWindowsServiceManager svcManager, ILogger<ServiceOperationOrchestrator> logger) : IServiceOperationOrchestrator
{
    private readonly IWindowsServiceManager _svcManager = svcManager;
    private readonly ILogger<ServiceOperationOrchestrator> _logger = logger;

    public async Task<Dictionary<string, bool>> StartServicesAsync(IEnumerable<Service> services, CancellationToken cancellationToken = default)
    {
        var results = new Dictionary<string, bool>();

        foreach (var s in services)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await _svcManager.StartServiceAsync(s.ServiceName, cancellationToken);
                results[s.ServiceName] = true;
                _logger.LogInformation("Started service {ServiceName}", s.ServiceName);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Start cancelled for {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start service {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
        }

        return results;
    }

    public async Task<Dictionary<string, bool>> StopServicesAsync(IEnumerable<Service> services, CancellationToken cancellationToken = default)
    {
        var results = new Dictionary<string, bool>();

        foreach (var s in services)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await _svcManager.StopServiceAsync(s.ServiceName, cancellationToken);
                results[s.ServiceName] = true;
                _logger.LogInformation("Stopped service {ServiceName}", s.ServiceName);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Stop cancelled for {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to stop service {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
        }

        return results;
    }

    public async Task<Dictionary<string, bool>> RestartServicesAsync(IEnumerable<Service> services, CancellationToken cancellationToken = default)
    {
        var results = new Dictionary<string, bool>();

        foreach (var s in services)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await _svcManager.StopServiceAsync(s.ServiceName, cancellationToken);
                await _svcManager.StartServiceAsync(s.ServiceName, cancellationToken);
                results[s.ServiceName] = true;
                _logger.LogInformation("Restarted service {ServiceName}", s.ServiceName);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Restart cancelled for {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to restart service {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
        }

        return results;
    }
}
