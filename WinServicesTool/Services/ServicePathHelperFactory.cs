using Microsoft.Extensions.Logging;

namespace WinServicesTool.Services;

/// <summary>
/// Factory-based wrapper around <see cref="ServicePathHelper"/> to provide a scoped service
/// for dependency injection without holding persistent SCManager connections.
/// </summary>
public sealed class ServicePathHelperFactory(ILogger<ServicePathHelperFactory> logger) : IServicePathHelper
{
    public string[] GetAllServiceNames()
    {
        using var helper = new ServicePathHelper();

        return helper.GetAllServiceNames();
    }

    public ServiceConfiguration? GetServiceConfiguration(string serviceName)
    {
        using var helper = new ServicePathHelper();

        return helper.GetServiceConfiguration(serviceName);
    }

    public async Task<ServiceConfiguration?> GetServiceConfigurationAsync(
        string serviceName,
        CancellationToken cancellationToken = default)
    {
        using var helper = new ServicePathHelper();

        return await helper.GetServiceConfigurationAsync(serviceName, cancellationToken);
    }

    public async Task<Dictionary<string, ServiceConfiguration?>> GetServiceConfigurationsAsync(
        IEnumerable<string> serviceNames,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default)
    {
        using var helper = new ServicePathHelper();

        return await helper.GetServiceConfigurationsAsync(serviceNames, progress, cancellationToken);
    }

    public async Task<Dictionary<string, ServiceConfiguration?>> GetAllServiceConfigurationsAsync(
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default)
    {
        using var helper = new ServicePathHelper();

        return await helper.GetAllServiceConfigurationsAsync(progress, cancellationToken);
    }

    public async Task<List<ServiceConfiguration>> GetServicesAsync(IProgress<int>? progress = null, CancellationToken cancellationToken = default)
    {
        try
        {
            using var helper = new ServicePathHelper();

            var serviceNames = helper.GetAllServiceNames();
            var services = new List<ServiceConfiguration>(serviceNames.Length);
            var completed = 0;

            foreach (var serviceName in serviceNames)
            {
                var config = await helper.GetServiceConfigurationAsync(serviceName, cancellationToken);

                Interlocked.Increment(ref completed);
                progress?.Report(completed);

                if (config != null)
                    services.Add(config);
            }

            return [.. services.OrderBy(s => s.DisplayName)];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to enumerate services");
            throw;
        }
    }

    public async Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default)
    {
        try
        {
            using var helper = new ServicePathHelper();
            await helper.StartServiceAsync(serviceName, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to start service {ServiceName}", serviceName);
            throw;
        }
    }

    public async Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default)
    {
        try
        {
            using var helper = new ServicePathHelper();
            await helper.StopServiceAsync(serviceName, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to stop service {ServiceName}", serviceName);
            throw;
        }
    }

    public void Dispose()
    {
        // Nothing to dispose in factory pattern
    }
}
