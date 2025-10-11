namespace WinServicesTool.Services;

/// <summary>
/// Abstraction for operations that enumerate and manipulate Windows services.
/// </summary>
public interface IWindowsServiceManager
{
    /// <summary>
    /// Enumerates known services and returns a list of lightweight <see cref="Models.Service"/> models.
    /// </summary>

    Task<List<Models.Service>> GetServicesAsync();

    /// <summary>
    /// Starts the specified service by service name and waits until the service is running or timeout.
    /// </summary>
    Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops the specified service and waits until stopped or timeout.
    /// </summary>
    Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default);
}
