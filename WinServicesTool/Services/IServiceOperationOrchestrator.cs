using WinServicesTool.Models;

namespace WinServicesTool.Services;

/// <summary>
/// Orchestrates higher-level operations across multiple services (start/stop/restart).
/// </summary>
public interface IServiceOperationOrchestrator
{
    /// <summary>
    /// Starts the provided services and returns a map of serviceName->success.
    /// </summary>
    Task<Dictionary<string, bool>> StartServicesAsync(IEnumerable<Service> services);

    /// <summary>
    /// Stops the provided services and returns a map of serviceName->success.
    /// </summary>
    Task<Dictionary<string, bool>> StopServicesAsync(IEnumerable<Service> services);

    /// <summary>
    /// Restarts the provided services (stop then start) and returns a map of serviceName->success.
    /// </summary>
    Task<Dictionary<string, bool>> RestartServicesAsync(IEnumerable<Service> services);
}
