using Microsoft.Extensions.Logging;

namespace WinServicesTool.Services;

/// <summary>
/// Default implementation of <see cref="IServiceOperationOrchestrator"/>.
/// </summary>
public sealed class ServiceOperationOrchestrator(IServicePathHelper serviceHelper, ILogger<ServiceOperationOrchestrator> logger) : IServiceOperationOrchestrator
{
    public async Task<Dictionary<string, bool>> StartServicesAsync(IEnumerable<ServiceConfiguration> services, CancellationToken cancellationToken = default)
    {
        var results = new Dictionary<string, bool>();

        foreach (var s in services)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await serviceHelper.StartServiceAsync(s.ServiceName, cancellationToken);
                results[s.ServiceName] = true;
                logger.LogInformation("Started service {ServiceName}", s.ServiceName);
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Start cancelled for {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to start service {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
        }

        return results;
    }

    public async Task<Dictionary<string, bool>> StopServicesAsync(IEnumerable<ServiceConfiguration> services, CancellationToken cancellationToken = default)
    {
        var results = new Dictionary<string, bool>();

        foreach (var s in services)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await serviceHelper.StopServiceAsync(s.ServiceName, cancellationToken);
                results[s.ServiceName] = true;
                logger.LogInformation("Stopped service {ServiceName}", s.ServiceName);
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Stop cancelled for {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to stop service {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
        }

        return results;
    }

    public async Task<Dictionary<string, bool>> RestartServicesAsync(IEnumerable<ServiceConfiguration> services, CancellationToken cancellationToken = default)
    {
        var results = new Dictionary<string, bool>();

        foreach (var s in services)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await serviceHelper.StopServiceAsync(s.ServiceName, cancellationToken);
                await serviceHelper.StartServiceAsync(s.ServiceName, cancellationToken);
                results[s.ServiceName] = true;
                logger.LogInformation("Restarted service {ServiceName}", s.ServiceName);
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Restart cancelled for {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to restart service {ServiceName}", s.ServiceName);
                results[s.ServiceName] = false;
            }
        }

        return results;
    }
}
