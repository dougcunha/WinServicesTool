using System.ComponentModel;
using System.ServiceProcess;

namespace WinServicesTool.Models;

public sealed partial class Service : INotifyPropertyChanged
{
    public required string DisplayName { get; set; }
    public required string ServiceName { get; set; }
    public required ServiceControllerStatus Status { get; set; }
    public ServiceStartMode StartMode { get; set; }
    public ServiceType ServiceType { get; set; }

    public bool CanPauseAndContinue { get; set; }

    public bool CanShutdown { get; set; }

    public bool CanStop { get; set; }
}