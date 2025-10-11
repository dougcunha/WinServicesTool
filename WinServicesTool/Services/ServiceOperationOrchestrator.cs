using Microsoft.Extensions.Logging;
using WinServicesTool.Models;

namespace WinServicesTool.Services;

/// <summary>
/// Default implementation of <see cref="IServiceOperationOrchestrator"/>.
/// </summary>
public sealed class ServiceOperationOrchestrator : IServiceOperationOrchestrator
{
    private readonly IWindowsServiceManager _svcManager;
    private readonly ILogger<ServiceOperationOrchestrator> _logger;

    public ServiceOperationOrchestrator(IWindowsServiceManager svcManager, ILogger<ServiceOperationOrchestrator> logger)
    {
        _svcManager = svcManager;
        _logger = logger;
    }

    public async Task<Dictionary<string, bool>> StartServicesAsync(IEnumerable<Service> services)
    {
        var results = new Dictionary<string, bool>();

        foreach (var s in services)
        {
            try
            {
                await _svcManager.StartServiceAsync(s.ServiceName);
                results[s.ServiceName] = true;
                _logger.LogInformation("Started service {ServiceName}", s.ServiceName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start service {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
        }

        return results;
    }

    public async Task<Dictionary<string, bool>> StopServicesAsync(IEnumerable<Service> services)
    {
        var results = new Dictionary<string, bool>();

        foreach (var s in services)
        {
            try
            {
                await _svcManager.StopServiceAsync(s.ServiceName);
                results[s.ServiceName] = true;
                _logger.LogInformation("Stopped service {ServiceName}", s.ServiceName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to stop service {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
        }

        return results;
    }

    public async Task<Dictionary<string, bool>> RestartServicesAsync(IEnumerable<Service> services)
    {
        var results = new Dictionary<string, bool>();

        foreach (var s in services)
        {
            try
            {
                await _svcManager.StopServiceAsync(s.ServiceName);
                await _svcManager.StartServiceAsync(s.ServiceName);
                results[s.ServiceName] = true;
                _logger.LogInformation("Restarted service {ServiceName}", s.ServiceName);
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
