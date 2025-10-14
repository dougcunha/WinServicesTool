namespace WinServicesTool.Services;

/// <summary>
/// Abstraction for querying and controlling Windows services using native APIs.
/// </summary>
public interface IServicePathHelper : IDisposable
{
    /// <summary>
    /// Gets the names of all services on the system.
    /// </summary>
    /// <returns>Array of service names</returns>
    string[] GetAllServiceNames();

    /// <summary>
    /// Gets the complete configuration for the specified Windows service (synchronous).
    /// For UI applications, prefer GetServiceConfigurationAsync.
    /// </summary>
    /// <param name="serviceName">The service name to query</param>
    /// <returns>Service configuration or null if not found</returns>
    ServiceConfiguration? GetServiceConfiguration(string serviceName);

    /// <summary>
    /// Gets the complete configuration for the specified Windows service asynchronously.
    /// Offloads the Win32 calls to a thread pool thread to avoid blocking the UI.
    /// </summary>
    /// <param name="serviceName">The service name to query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service configuration or null if not found</returns>
    Task<ServiceConfiguration?> GetServiceConfigurationAsync(string serviceName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets configurations for multiple services in parallel with progress reporting.
    /// Ideal for listing all services in a UI application.
    /// </summary>
    /// <param name="serviceNames">Service names to query</param>
    /// <param name="progress">Optional progress reporter (reports completed count)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary mapping service names to their configurations</returns>
    Task<Dictionary<string, ServiceConfiguration?>> GetServiceConfigurationsAsync
    (
        IEnumerable<string> serviceNames,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets configurations for ALL services on the system asynchronously with progress reporting.
    /// Perfect for populating a service list UI.
    /// </summary>
    /// <param name="progress">Optional progress reporter (reports completed count)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary mapping service names to their configurations</returns>
    Task<Dictionary<string, ServiceConfiguration?>> GetAllServiceConfigurationsAsync
    (
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets all service configurations as a list, ordered by display name.
    /// </summary>
    /// <param name="progress">Optional progress reporter (reports completed count)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of service configurations</returns>
    Task<List<ServiceConfiguration>> GetServicesAsync(IProgress<int>? progress = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts the specified service and waits until it reaches the running state or timeout.
    /// </summary>
    /// <param name="serviceName">The service name to start</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="InvalidOperationException">Thrown when the service cannot be started</exception>
    Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops the specified service and waits until it reaches the stopped state or timeout.
    /// </summary>
    /// <param name="serviceName">The service name to stop</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="InvalidOperationException">Thrown when the service cannot be stopped</exception>
    Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default);
}
