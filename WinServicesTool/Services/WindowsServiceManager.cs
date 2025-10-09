using System.ServiceProcess;
using Microsoft.Extensions.Logging;

namespace WinServicesTool.Services;

/// <summary>
/// Default implementation of <see cref="IWindowsServiceManager"/> using <see cref="ServiceController"/>.
/// </summary>
public sealed class WindowsServiceManager : IWindowsServiceManager
{
    private readonly ILogger<WindowsServiceManager> _logger;

    public WindowsServiceManager(ILogger<WindowsServiceManager> logger)
    {
        _logger = logger;
    }

    public Task<List<Models.Service>> GetServicesAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                return ServiceController.GetServices()
                    .Select(serv => new Models.Service
                    {
                        DisplayName = serv.DisplayName,
                        ServiceName = serv.ServiceName,
                        Status = serv.Status,
                        StartMode = GetStartTypeSafe(serv),
                        ServiceType = serv.ServiceType,
                        CanPauseAndContinue = serv.CanPauseAndContinue,
                        CanShutdown = serv.CanShutdown,
                        CanStop = serv.CanStop
                    })
                    .OrderBy(s => s.DisplayName)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to enumerate services");
                throw;
            }
        });
    }

    public Task StartServiceAsync(string serviceName)
    {
        return Task.Run(() =>
        {
            using var sc = new ServiceController(serviceName);
            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
            }
        });
    }

    public Task StopServiceAsync(string serviceName)
    {
        return Task.Run(() =>
        {
            using var sc = new ServiceController(serviceName);
            if (sc.Status is ServiceControllerStatus.Running or ServiceControllerStatus.Paused)
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
            }
        });
    }

    private static ServiceStartMode GetStartTypeSafe(ServiceController serv)
    {
        try
        {
            return serv.StartType;
        }
        catch
        {
            return ServiceStartMode.System;
        }
    }
}
