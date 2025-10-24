// ReSharper disable PropertyCanBeMadeInitOnly.Global
using System.ComponentModel;
using System.Runtime.InteropServices;
using WinServicesTool.Models;
using TimeoutException = System.TimeoutException;

namespace WinServicesTool.Services;

using System.Text.RegularExpressions;
using PropertyChanged;

/// <summary>
/// Represents the complete configuration of a Windows service.
/// </summary>
public sealed partial class ServiceConfiguration : INotifyPropertyChanged
{
    // ReSharper disable once UseRawString
    [GeneratedRegex(@"^""?([^""]+?\.exe)(?=""|$|\s)", RegexOptions.IgnoreCase)]
    private static partial Regex RegexExtractPath();

    /// <summary>
    /// Retorna o caminho do executável principal de uma string de comando de serviço.
    /// </summary>
    /// <param name="serviceCommand">Linha de comando completa.</param>
    /// <returns>Caminho do executável principal.</returns>
    private static string? ExtractExePath(string serviceCommand)
    {
        if (string.IsNullOrWhiteSpace(serviceCommand))
            return null;

        var match = RegexExtractPath().Match(serviceCommand);

        return match.Success ? match.Groups[1].Value : null;
    }

    public required string ServiceName { get; set; }
    public ServiceTypeEx ServiceType { get; set; }
    public StartType StartType { get; set; }
    public ErrorControl ErrorControl { get; set; }

    public string BinaryPathName
    {
        get => field ?? string.Empty;
        set
        {
            if (field == value)
                return;

            field = value;
            MainExePath = ExtractExePath(value) ?? string.Empty;
            OnPropertyChanged();
        }
    } = string.Empty;

    [DoNotNotify]
    public string MainExePath { get; private set; } = string.Empty;

    [DoNotNotify]
    public string MainExeDirectory
        => string.IsNullOrEmpty(MainExePath)
            ? string.Empty
            : Path.GetDirectoryName(MainExePath) ?? string.Empty;

    public string? LoadOrderGroup { get; set; }
    public uint TagId { get; set; }
    public string[]? Dependencies { get; set; }
    public string? ServiceStartName { get; set; }
    public string? DisplayName { get; set; }
    public string? Description { get; set; }
    public bool IsDelayedAutoStart { get; set; }

    // Status information
    public ServiceState CurrentState { get; set; }
    public uint ProcessId { get; set; }
    public uint Win32ExitCode { get; set; }
    public uint ServiceSpecificExitCode { get; set; }

    // Control capabilities
    public bool CanStop { get; set; }
    public bool CanPauseAndContinue { get; set; }
    public bool CanShutdown { get; set; }

    // NSSM detection
    public bool IsNssmManaged { get; set; }

    /// <summary>
    /// Gets a human-readable description of the service type for display purposes.
    /// </summary>
    [Browsable(false)]
    public string ServiceTypeDescription
        => ServiceType.Describe();
}

/// <summary>
/// Specifies when the service should be started.
/// </summary>
public enum StartType : uint
{
    Boot = 0,
    System = 1,
    Automatic = 2,
    Manual = 3,
    Disabled = 4
}

/// <summary>
/// Specifies the severity of the error if the service fails to start.
/// </summary>
public enum ErrorControl : uint
{
    Ignore = 0,
    Normal = 1,
    Severe = 2,
    Critical = 3
}

/// <summary>
/// Specifies the current state of the service.
/// </summary>
public enum ServiceState : uint
{
    Stopped = 0x00000001,
    StartPending = 0x00000002,
    StopPending = 0x00000003,
    Running = 0x00000004,
    ContinuePending = 0x00000005,
    PausePending = 0x00000006,
    Paused = 0x00000007
}

/// <summary>
/// Extension methods for <see cref="ServiceConfiguration"/>.
/// </summary>
public static class ServiceConfigurationExtensions
{
    /// <summary>
    /// Converts <see cref="ServiceState"/> to <see cref="System.ServiceProcess.ServiceControllerStatus"/>.
    /// </summary>
    public static System.ServiceProcess.ServiceControllerStatus ToServiceControllerStatus(this ServiceState state)
        => state switch
        {
            ServiceState.Stopped => System.ServiceProcess.ServiceControllerStatus.Stopped,
            ServiceState.StartPending => System.ServiceProcess.ServiceControllerStatus.StartPending,
            ServiceState.StopPending => System.ServiceProcess.ServiceControllerStatus.StopPending,
            ServiceState.Running => System.ServiceProcess.ServiceControllerStatus.Running,
            ServiceState.ContinuePending => System.ServiceProcess.ServiceControllerStatus.ContinuePending,
            ServiceState.PausePending => System.ServiceProcess.ServiceControllerStatus.PausePending,
            ServiceState.Paused => System.ServiceProcess.ServiceControllerStatus.Paused,
            var _ => System.ServiceProcess.ServiceControllerStatus.Stopped
        };

    /// <summary>
    /// Converts <see cref="StartType"/> to <see cref="System.ServiceProcess.ServiceStartMode"/>.
    /// </summary>
    public static System.ServiceProcess.ServiceStartMode ToServiceStartMode(this StartType startType)
        => startType switch
        {
            StartType.Boot => System.ServiceProcess.ServiceStartMode.Automatic,
            StartType.System => System.ServiceProcess.ServiceStartMode.Automatic,
            StartType.Automatic => System.ServiceProcess.ServiceStartMode.Automatic,
            StartType.Manual => System.ServiceProcess.ServiceStartMode.Manual,
            StartType.Disabled => System.ServiceProcess.ServiceStartMode.Disabled,
            var _ => System.ServiceProcess.ServiceStartMode.Manual
        };

    /// <summary>
    /// Gets the <see cref="System.ServiceProcess.ServiceControllerStatus"/> for this configuration.
    /// </summary>
    public static System.ServiceProcess.ServiceControllerStatus GetStatus(this ServiceConfiguration config)
        => config.CurrentState.ToServiceControllerStatus();

    /// <summary>
    /// Gets the <see cref="System.ServiceProcess.ServiceStartMode"/> for this configuration.
    /// </summary>
    public static System.ServiceProcess.ServiceStartMode GetStartMode(this ServiceConfiguration config)
        => config.StartType.ToServiceStartMode();

}

/// <summary>
/// Helper class to query Windows service configuration using native API.
/// Implements IDisposable to allow reusing the SCManager connection across multiple queries.
/// </summary>
public sealed class ServicePathHelper : IServicePathHelper
{
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern nint OpenSCManager(string? machineName, string? databaseName, uint dwAccess);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern nint OpenService(nint hSCManager, string lpServiceName, uint dwDesiredAccess);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool QueryServiceConfig(nint hService, nint lpServiceConfig, uint cbBufSize, out uint pcbBytesNeeded);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool QueryServiceConfig2(nint hService, uint dwInfoLevel, nint lpBuffer, uint cbBufSize, out uint pcbBytesNeeded);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool QueryServiceStatusEx(nint hService, int infoLevel, nint lpBuffer, uint cbBufSize, out uint pcbBytesNeeded);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool EnumServicesStatusEx
    (
        nint hSCManager,
        int infoLevel,
        uint dwServiceType,
        uint dwServiceState,
        nint lpServices,
        uint cbBufSize,
        out uint pcbBytesNeeded,
        out uint lpServicesReturned,
        ref uint lpResumeHandle,
        string? pszGroupName
    );

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool CloseServiceHandle(nint hSCObject);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool StartService(nint hService, uint dwNumServiceArgs, nint lpServiceArgVectors);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool ControlService(nint hService, uint dwControl, ref ServiceStatus lpServiceStatus);

    [StructLayout(LayoutKind.Sequential)]
    private struct ServiceStatus
    {
        public uint dwServiceType;
        public uint dwCurrentState;
        public uint dwControlsAccepted;
        public uint dwWin32ExitCode;
        public uint dwServiceSpecificExitCode;
        public uint dwCheckPoint;
        public uint dwWaitHint;
    }

    private const uint SC_MANAGER_CONNECT = 0x0001;
    private const uint SC_MANAGER_ENUMERATE_SERVICE = 0x0004;
    private const uint SERVICE_QUERY_CONFIG = 0x0001;
    private const uint SERVICE_QUERY_STATUS = 0x0004;
    private const uint SERVICE_START = 0x0010;
    private const uint SERVICE_STOP = 0x0020;
    private const uint SERVICE_CONFIG_DESCRIPTION = 1;
    private const uint SERVICE_CONFIG_DELAYED_AUTO_START_INFO = 3;
    private const int SC_STATUS_PROCESS_INFO = 0;
    private const int SC_ENUM_PROCESS_INFO = 0;
    private const int ERROR_INSUFFICIENT_BUFFER = 122;
    private const int ERROR_MORE_DATA = 234;

    private const uint SERVICE_WIN32 = 0x00000030; // WIN32_OWN_PROCESS | WIN32_SHARE_PROCESS
    private const uint SERVICE_DRIVER = 0x0000000B; // KERNEL_DRIVER | FILE_SYSTEM_DRIVER
    private const uint SERVICE_STATE_ALL = 0x00000003;

    // Service control acceptance flags
    private const uint SERVICE_ACCEPT_STOP = 0x00000001;
    private const uint SERVICE_ACCEPT_PAUSE_CONTINUE = 0x00000002;
    private const uint SERVICE_ACCEPT_SHUTDOWN = 0x00000004;

    // Service control codes
    private const uint SERVICE_CONTROL_STOP = 0x00000001;
    private const uint SERVICE_CONTROL_PAUSE = 0x00000002;
    private const uint SERVICE_CONTROL_CONTINUE = 0x00000003;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct QueryServiceConf
    {
        public uint dwServiceType;
        public uint dwStartType;
        public uint dwErrorControl;
        public IntPtr lpBinaryPathName;
        public IntPtr lpLoadOrderGroup;
        public uint dwTagId;
        public IntPtr lpDependencies;
        public IntPtr lpServiceStartName;
        public IntPtr lpDisplayName;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct ServiceDelayedAutoStartInfo
    {
        public bool fDelayedAutostart;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct ServiceDescription
    {
        public IntPtr lpDescription;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct ServiceStatusProcess
    {
        public uint dwServiceType;
        public uint dwCurrentState;
        public uint dwControlsAccepted;
        public uint dwWin32ExitCode;
        public uint dwServiceSpecificExitCode;
        public uint dwCheckPoint;
        public uint dwWaitHint;
        public uint dwProcessId;
        public uint dwServiceFlags;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct EnumServiceStatusProcess
    {
        public IntPtr lpServiceName;
        public IntPtr lpDisplayName;
        public ServiceStatusProcess ServiceStatusProcess;
    }

    private readonly nint _scmHandle;
    private readonly SemaphoreSlim _semaphore;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServicePathHelper"/> class and opens the SCManager connection.
    /// </summary>
    /// <param name="maxConcurrency">Maximum number of concurrent service queries (default: 10)</param>
    /// <exception cref="Win32Exception">Thrown when OpenSCManager fails.</exception>
    public ServicePathHelper(int maxConcurrency = 10)
    {
        _scmHandle = OpenSCManager(null, null, SC_MANAGER_CONNECT | SC_MANAGER_ENUMERATE_SERVICE);

        if (_scmHandle == nint.Zero)
            throw new Win32Exception(Marshal.GetLastWin32Error(), "OpenSCManager failed");

        _semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
    }

    /// <summary>
    /// Gets the names of all services on the system.
    /// </summary>
    /// <returns>Array of service names</returns>
    public string[] GetAllServiceNames()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        uint resumeHandle = 0;
        var services = new List<string>();

        while (true)
        {
            EnumServicesStatusEx
            (
                _scmHandle,
                SC_ENUM_PROCESS_INFO,
                SERVICE_WIN32,
                SERVICE_STATE_ALL,
                nint.Zero,
                0,
                out var bytesNeeded,
                out var _,
                ref resumeHandle,
                null
            );

            var lastError = Marshal.GetLastWin32Error();
            if (lastError != ERROR_INSUFFICIENT_BUFFER && lastError != ERROR_MORE_DATA)
                break;

            var buffer = Marshal.AllocHGlobal((int)bytesNeeded);

            try
            {
                if (!EnumServicesStatusEx(
                    _scmHandle,
                    SC_ENUM_PROCESS_INFO,
                    SERVICE_WIN32,
                    SERVICE_STATE_ALL,
                    buffer,
                    bytesNeeded,
                    out var _,
                    out var servicesReturned,
                    ref resumeHandle,
                    null)
                )
                    break;

                var structSize = Marshal.SizeOf<EnumServiceStatusProcess>();
                var current = buffer;

                for (var i = 0; i < servicesReturned; i++)
                {
                    var service = Marshal.PtrToStructure<EnumServiceStatusProcess>(current);
                    var serviceName = Marshal.PtrToStringUni(service.lpServiceName);

                    if (!string.IsNullOrEmpty(serviceName))
                        services.Add(serviceName);

                    current = nint.Add(current, structSize);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            if (resumeHandle == 0)
                break;
        }

        return [.. services];
    }

    /// <summary>
    /// Gets the complete configuration for the specified Windows service (synchronous).
    /// For UI applications, prefer GetServiceConfigurationAsync.
    /// </summary>
    public ServiceConfiguration? GetServiceConfiguration(string serviceName)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        var service = nint.Zero;

        try
        {
            service = OpenService(_scmHandle, serviceName, SERVICE_QUERY_CONFIG | SERVICE_QUERY_STATUS);

            if (service == nint.Zero)
                return null; // Service not found or access denied

            QueryServiceConfig(service, nint.Zero, 0, out var bytesNeeded);

            if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
                return null;

            var buffer = Marshal.AllocHGlobal((int)bytesNeeded);

            try
            {
                if (!QueryServiceConfig(service, buffer, bytesNeeded, out var _))
                    return null;

                var config = Marshal.PtrToStructure<QueryServiceConf>(buffer);

                var binaryPath = Marshal.PtrToStringUni(config.lpBinaryPathName);
                var expandedPath = Environment.ExpandEnvironmentVariables(binaryPath ?? "");

                var loadOrderGroup = config.lpLoadOrderGroup != nint.Zero
                    ? Marshal.PtrToStringUni(config.lpLoadOrderGroup)
                    : null;

                string[]? dependencies = null;

                if (config.lpDependencies != nint.Zero)
                {
                    var depList = new List<string>();
                    var offset = 0;

                    while (true)
                    {
                        var dep = Marshal.PtrToStringUni(config.lpDependencies + (offset * 2));

                        if (string.IsNullOrEmpty(dep))
                            break;

                        depList.Add(dep);
                        offset += dep.Length + 1;
                    }

                    dependencies = depList.Count > 0 ? [.. depList] : null;
                }

                var serviceStartName = config.lpServiceStartName != nint.Zero
                    ? Marshal.PtrToStringUni(config.lpServiceStartName)
                    : null;

                var displayName = config.lpDisplayName != nint.Zero
                    ? Marshal.PtrToStringUni(config.lpDisplayName)
                    : null;

                var isDelayedAutoStart = QueryDelayedAutoStart(service);
                var description = QueryServiceDescription(service);
                var status = QueryServiceStatus(service);

                return new ServiceConfiguration
                {
                    ServiceName = serviceName,
                    ServiceType = (ServiceTypeEx)config.dwServiceType,
                    StartType = (StartType)config.dwStartType,
                    ErrorControl = (ErrorControl)config.dwErrorControl,
                    BinaryPathName = expandedPath,
                    LoadOrderGroup = loadOrderGroup,
                    TagId = config.dwTagId,
                    Dependencies = dependencies,
                    ServiceStartName = serviceStartName,
                    DisplayName = displayName,
                    Description = description,
                    IsDelayedAutoStart = isDelayedAutoStart,
                    CurrentState = (ServiceState)status.dwCurrentState,
                    ProcessId = status.dwProcessId,
                    Win32ExitCode = status.dwWin32ExitCode,
                    ServiceSpecificExitCode = status.dwServiceSpecificExitCode,
                    CanStop = (status.dwControlsAccepted & SERVICE_ACCEPT_STOP) != 0,
                    CanPauseAndContinue = (status.dwControlsAccepted & SERVICE_ACCEPT_PAUSE_CONTINUE) != 0,
                    CanShutdown = (status.dwControlsAccepted & SERVICE_ACCEPT_SHUTDOWN) != 0
                };
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        finally
        {
            if (service != nint.Zero)
                CloseServiceHandle(service);
        }
    }

    /// <summary>
    /// Gets the complete configuration for the specified Windows service asynchronously.
    /// Offloads the Win32 calls to a thread pool thread to avoid blocking the UI.
    /// </summary>
    public async Task<ServiceConfiguration?> GetServiceConfigurationAsync(
        string serviceName,
        CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            return await Task.Run(() => GetServiceConfiguration(serviceName), cancellationToken);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Gets configurations for multiple services in parallel with progress reporting.
    /// Ideal for listing all services in a UI application.
    /// </summary>
    /// <param name="serviceNames">Service names to query</param>
    /// <param name="progress">Optional progress reporter (reports completed count)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary mapping service names to their configurations</returns>
    public async Task<Dictionary<string, ServiceConfiguration?>> GetServiceConfigurationsAsync
    (
        IEnumerable<string> serviceNames,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default
    )
    {
        var serviceList = serviceNames.ToList();
        var results = new Dictionary<string, ServiceConfiguration?>(serviceList.Count);
        var completed = 0;

        var tasks = serviceList.Select(async serviceName =>
        {
            var config = await GetServiceConfigurationAsync(serviceName, cancellationToken);

            Interlocked.Increment(ref completed);
            progress?.Report(completed);

            return (serviceName, config);
        });

        var configurations = await Task.WhenAll(tasks);

        foreach (var (serviceName, config) in configurations)
            results[serviceName] = config;

        return results;
    }

    /// <summary>
    /// Gets configurations for ALL services on the system asynchronously with progress reporting.
    /// Perfect for populating a service list UI.
    /// </summary>
    /// <param name="progress">Optional progress reporter (reports completed count)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary mapping service names to their configurations</returns>
    public async Task<Dictionary<string, ServiceConfiguration?>> GetAllServiceConfigurationsAsync(
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default)
    {
        var serviceNames = await Task.Run(GetAllServiceNames, cancellationToken);

        return await GetServiceConfigurationsAsync(serviceNames, progress, cancellationToken);
    }

    private static bool QueryDelayedAutoStart(nint serviceHandle)
    {
        var bufferSize = (uint)Marshal.SizeOf<ServiceDelayedAutoStartInfo>();
        var buffer = Marshal.AllocHGlobal((int)bufferSize);

        try
        {
            if (QueryServiceConfig2(serviceHandle, SERVICE_CONFIG_DELAYED_AUTO_START_INFO, buffer, bufferSize, out var _))
            {
                var delayedInfo = Marshal.PtrToStructure<ServiceDelayedAutoStartInfo>(buffer);

                return delayedInfo.fDelayedAutostart;
            }

            return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    private static string? QueryServiceDescription(nint serviceHandle)
    {
        QueryServiceConfig2(serviceHandle, SERVICE_CONFIG_DESCRIPTION, nint.Zero, 0, out var bytesNeeded);

        if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
            return null;

        var buffer = Marshal.AllocHGlobal((int)bytesNeeded);

        try
        {
            if (!QueryServiceConfig2(serviceHandle, SERVICE_CONFIG_DESCRIPTION, buffer, bytesNeeded, out var _))
                return null;

            var descInfo = Marshal.PtrToStructure<ServiceDescription>(buffer);

            return descInfo.lpDescription != nint.Zero
                ? Marshal.PtrToStringUni(descInfo.lpDescription)
                : null;
        }
        catch
        {
            return null;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    private static ServiceStatusProcess QueryServiceStatus(nint serviceHandle)
    {
        var bufferSize = (uint)Marshal.SizeOf<ServiceStatusProcess>();
        var buffer = Marshal.AllocHGlobal((int)bufferSize);

        try
        {
            return QueryServiceStatusEx(serviceHandle, SC_STATUS_PROCESS_INFO, buffer, bufferSize, out var _)
                ? Marshal.PtrToStructure<ServiceStatusProcess>(buffer)
                : default;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    /// <summary>
    /// Starts the specified service and waits until it reaches the running state or timeout.
    /// </summary>
    /// <param name="serviceName">The service name to start</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="InvalidOperationException">Thrown when the service cannot be started</exception>
    public async Task StartServiceAsync(string serviceName, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);

            var serviceHandle = nint.Zero;

            try
            {
                serviceHandle = OpenService(_scmHandle, serviceName, SERVICE_QUERY_STATUS | SERVICE_START);

                if (serviceHandle == nint.Zero)
                    throw new InvalidOperationException($"Failed to open service '{serviceName}'. Error: {Marshal.GetLastWin32Error()}");

                var status = QueryServiceStatus(serviceHandle);

                if (status.dwCurrentState == (uint)ServiceState.Running)
                    return;

                if (!StartService(serviceHandle, 0, nint.Zero))
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new InvalidOperationException($"Failed to start service '{serviceName}'. Error: {error}");
                }

                var timeout = TimeSpan.FromSeconds(10);
                var sw = System.Diagnostics.Stopwatch.StartNew();

                while (sw.Elapsed < timeout)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(200);

                    status = QueryServiceStatus(serviceHandle);

                    if (status.dwCurrentState == (uint)ServiceState.Running)
                        return;
                }

                throw new TimeoutException($"Service '{serviceName}' did not start within the timeout period.");
            }
            finally
            {
                if (serviceHandle != nint.Zero)
                    CloseServiceHandle(serviceHandle);
            }
        }, cancellationToken);
    }

    /// <summary>
    /// Stops the specified service and waits until it reaches the stopped state or timeout.
    /// </summary>
    /// <param name="serviceName">The service name to stop</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="InvalidOperationException">Thrown when the service cannot be stopped</exception>
    public async Task StopServiceAsync(string serviceName, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);

            var serviceHandle = nint.Zero;

            try
            {
                serviceHandle = OpenService(_scmHandle, serviceName, SERVICE_QUERY_STATUS | SERVICE_STOP);

                if (serviceHandle == nint.Zero)
                    throw new InvalidOperationException($"Failed to open service '{serviceName}'. Error: {Marshal.GetLastWin32Error()}");

                var status = QueryServiceStatus(serviceHandle);

                if (status.dwCurrentState == (uint)ServiceState.Stopped)
                    return;

                var serviceStatus = new ServiceStatus();

                if (!ControlService(serviceHandle, SERVICE_CONTROL_STOP, ref serviceStatus))
                {
                    var error = Marshal.GetLastWin32Error();
                    throw new InvalidOperationException($"Failed to stop service '{serviceName}'. Error: {error}");
                }

                var timeout = TimeSpan.FromSeconds(10);
                var sw = System.Diagnostics.Stopwatch.StartNew();

                while (sw.Elapsed < timeout)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(200);

                    status = QueryServiceStatus(serviceHandle);

                    if (status.dwCurrentState == (uint)ServiceState.Stopped)
                        return;
                }

                throw new TimeoutException($"Service '{serviceName}' did not stop within the timeout period.");
            }
            finally
            {
                if (serviceHandle != nint.Zero)
                    CloseServiceHandle(serviceHandle);
            }
        }, cancellationToken);
    }

    /// <summary>
    /// Gets all service configurations as a list, ordered by display name.
    /// </summary>
    /// <param name="progress">Optional progress reporter (reports completed count)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of service configurations</returns>
    public async Task<List<ServiceConfiguration>> GetServicesAsync(IProgress<int>? progress = null, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            ObjectDisposedException.ThrowIf(_disposed, this);

            var serviceNames = GetAllServiceNames();
            var services = new List<ServiceConfiguration>();
            var completed = 0;

            foreach (var serviceName in serviceNames)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var config = GetServiceConfiguration(serviceName);

                    Interlocked.Increment(ref completed);
                    progress?.Report(completed);

                    if (config != null)
                        services.Add(config);
                }
                catch
                {
                    // Ignora serviços que não podem ser consultados
                }
            }

            return services;
        }, cancellationToken);
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _semaphore.Dispose();

        if (_scmHandle != nint.Zero)
            CloseServiceHandle(_scmHandle);

        _disposed = true;
    }
}
