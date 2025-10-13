using System.ServiceProcess;
using Microsoft.Extensions.Logging;
using WinServicesTool.Models;
using WinServicesTool.Utils;

namespace WinServicesTool.Services;

/// <summary>
/// Default implementation of <see cref="IWindowsServiceManager"/> using <see cref="ServiceController"/>.
/// </summary>
public sealed class WindowsServiceManager(ILogger<WindowsServiceManager> logger, AppConfig appConfig) : IWindowsServiceManager
{
    public Task<List<Service>> GetServicesAsync()
        => Task.Run(() =>
        {
            try
            {
                var services = ServiceController.GetServices();

                // Check if Path column is visible in config
                var shouldLoadPath = appConfig.IsPathColumnVisible;

                if (!shouldLoadPath)
                {
                    return [.. services
                    .Select(serv => new Service
                    {
                        Path = string.Empty,
                        DisplayName = serv.DisplayName,
                        ServiceName = serv.ServiceName,
                        Status = serv.Status,
                        StartMode = GetStartTypeSafe(serv),
                        ServiceType = ServiceTypeHelper.Describe((int)serv.ServiceType),
                        CanPauseAndContinue = serv.CanPauseAndContinue,
                        CanShutdown = serv.CanShutdown,
                        CanStop = serv.CanStop
                    })
                    .OrderBy(s => s.DisplayName)];
                }

                using var pathHelper = new ServicePathHelper();

                return services
                .Select(serv => new Service
                {
                    Path = pathHelper.GetExecutablePath(serv.ServiceName) ?? string.Empty,
                    DisplayName = serv.DisplayName,
                    ServiceName = serv.ServiceName,
                    Status = serv.Status,
                    StartMode = GetStartTypeSafe(serv),
                    ServiceType = ServiceTypeHelper.Describe((int)serv.ServiceType),
                    CanPauseAndContinue = serv.CanPauseAndContinue,
                    CanShutdown = serv.CanShutdown,
                    CanStop = serv.CanStop
                })
                .OrderBy(s => s.DisplayName)
                .ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to enumerate services");
                throw;
            }
        });

    public Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default)
        => Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var sc = new ServiceController(serviceName);

            if (sc.Status != ServiceControllerStatus.Stopped)
                return;

            sc.Start();
            // Wait loop with cancellation checks
            var timeout = TimeSpan.FromSeconds(10);
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sc.Status != ServiceControllerStatus.Running && sw.Elapsed < timeout)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Thread.Sleep(200);
                sc.Refresh();
            }
        }, cancellationToken);

    public Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default)
        => Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var sc = new ServiceController(serviceName);

            if (sc.Status is not (ServiceControllerStatus.Running or ServiceControllerStatus.Paused))
                return;

            sc.Stop();
            var timeout = TimeSpan.FromSeconds(10);
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sc.Status != ServiceControllerStatus.Stopped && sw.Elapsed < timeout)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Thread.Sleep(200);
                sc.Refresh();
            }
        }, cancellationToken);

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
