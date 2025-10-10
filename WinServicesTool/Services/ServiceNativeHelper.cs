namespace WinServicesTool.Services;

using System;
using System.Runtime.InteropServices;

public static class ServicePathHelper
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

    public static string? GetExecutablePath(string serviceName)
    {
        IntPtr scm = IntPtr.Zero, service = IntPtr.Zero;
        try
        {
            scm = OpenSCManager(null, null, SC_MANAGER_CONNECT);
            if (scm == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "OpenSCManager failed");

            service = OpenService(scm, serviceName, SERVICE_QUERY_CONFIG);
            if (service == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), $"OpenService failed for {serviceName}");

            // Descobrir tamanho necessário
            QueryServiceConfig(service, IntPtr.Zero, 0, out var bytesNeeded);
            if (Marshal.GetLastWin32Error() != 122) // ERROR_INSUFFICIENT_BUFFER
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
            if (service != IntPtr.Zero) CloseServiceHandle(service);
            if (scm != IntPtr.Zero) CloseServiceHandle(scm);
        }
    }
}
