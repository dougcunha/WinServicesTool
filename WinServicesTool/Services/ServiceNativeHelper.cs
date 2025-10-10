namespace WinServicesTool.Services;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Helper class to query Windows service executable paths using native API.
/// Implements IDisposable to allow reusing the SCManager connection across multiple queries.
/// </summary>
public sealed class ServicePathHelper : IDisposable
{
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern IntPtr OpenSCManager(string? machineName, string? databaseName, uint dwAccess);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern bool QueryServiceConfig(IntPtr hService, IntPtr lpServiceConfig, uint cbBufSize, out uint pcbBytesNeeded);

    [DllImport("advapi32.dll", SetLastError = true)]
    static extern bool CloseServiceHandle(IntPtr hSCObject);

    private const uint SC_MANAGER_CONNECT = 0x0001;
    private const uint SERVICE_QUERY_CONFIG = 0x0001;
    private const int ERROR_INSUFFICIENT_BUFFER = 122;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct QUERY_SERVICE_CONFIG
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

    private readonly IntPtr _scmHandle;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServicePathHelper"/> class and opens the SCManager connection.
    /// </summary>
    /// <exception cref="System.ComponentModel.Win32Exception">Thrown when OpenSCManager fails.</exception>
    public ServicePathHelper()
    {
        _scmHandle = OpenSCManager(null, null, SC_MANAGER_CONNECT);

        if (_scmHandle == IntPtr.Zero)
            throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "OpenSCManager failed");
    }

    /// <summary>
    /// Gets the executable path for the specified Windows service.
    /// </summary>
    /// <param name="serviceName">The name of the service.</param>
    /// <returns>The full executable path with environment variables expanded, or null if not found.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the helper has been disposed.</exception>
    /// <exception cref="System.ComponentModel.Win32Exception">Thrown when the service cannot be opened or queried.</exception>
    public string? GetExecutablePath(string serviceName)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        IntPtr service = IntPtr.Zero;

        try
        {
            service = OpenService(_scmHandle, serviceName, SERVICE_QUERY_CONFIG);

            if (service == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), $"OpenService failed for {serviceName}");

            QueryServiceConfig(service, IntPtr.Zero, 0, out var bytesNeeded);

            if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
                return null;

            IntPtr buffer = Marshal.AllocHGlobal((int)bytesNeeded);

            try
            {
                if (!QueryServiceConfig(service, buffer, bytesNeeded, out _))
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), $"QueryServiceConfig failed for {serviceName}");

                var config = Marshal.PtrToStructure<QUERY_SERVICE_CONFIG>(buffer);
                var path = Marshal.PtrToStringUni(config.lpBinaryPathName);

                return Environment.ExpandEnvironmentVariables(path ?? "");
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        finally
        {
            if (service != IntPtr.Zero)
                CloseServiceHandle(service);
        }
    }

    /// <summary>
    /// Releases the SCManager handle.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        if (_scmHandle != IntPtr.Zero)
            CloseServiceHandle(_scmHandle);

        _disposed = true;
    }
}
