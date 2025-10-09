using System.ComponentModel;
using System.ServiceProcess;

namespace WinServicesTool.Models;

/// <summary>
/// Represents metadata and runtime state for a Windows service.
/// </summary>
/// <remarks>
/// This model contains identifying information (display and service names),
/// configuration details (start mode and service type), and runtime state
/// (current status and capability flags). The class is partial and sealed,
/// and it implements <see cref="INotifyPropertyChanged"/> so consumers may
/// observe property changes when the other partial declaration raises the
/// <c>PropertyChanged</c> event.
/// </remarks>
public sealed partial class Service : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the friendly display name of the service as shown in the Services MMC.
    /// </summary>
    /// <value>The localized display name of the service.</value>
    public required string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the internal service name (the short name) used by the Service Control Manager.
    /// </summary>
    /// <value>The unique service name.</value>
    public required string ServiceName { get; set; }

    /// <summary>
    /// Gets or sets the current runtime status of the service.
    /// </summary>
    /// <value>
    /// A <see cref="ServiceControllerStatus"/> value indicating whether the service is running,
    /// stopped, paused, starting, stopping, etc.
    /// </value>
    public required ServiceControllerStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the start mode of the service.
    /// </summary>
    /// <value>
    /// A <see cref="ServiceStartMode"/> value indicating how the service is configured to start:
    /// Automatic, Manual, or Disabled.
    /// </value>
    public ServiceStartMode StartMode { get; set; }

    /// <summary>
    /// Gets or sets the type of the service.
    /// </summary>
    /// <value>
    /// A <see cref="ServiceType"/> value describing whether the service runs in its own process,
    /// as a shared process, as a driver, etc.
    /// </value>
    public ServiceType ServiceType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the service supports pause and continue commands.
    /// </summary>
    /// <value><c>true</c> if the service can be paused and continued; otherwise, <c>false</c>.</value>
    public bool CanPauseAndContinue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the service receives a notification when the system is shutting down.
    /// </summary>
    /// <value><c>true</c> if the service will receive shutdown notifications; otherwise, <c>false</c>.</value>
    public bool CanShutdown { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the service can be stopped.
    /// </summary>
    /// <value><c>true</c> if the service supports stop requests; otherwise, <c>false</c>.</value>
    public bool CanStop { get; set; }
}