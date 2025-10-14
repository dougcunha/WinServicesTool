namespace WinServicesTool.Services;

using Microsoft.Win32;

/// <summary>
/// Concrete implementation of <see cref="IRegistryService"/> which performs small
/// registry writes and launches regedit.exe so the UI can navigate to a service key.
/// </summary>
public sealed class RegistryService(IRegistryEditor editor, IProcessLauncher launcher) : IRegistryService
{
    /// <inheritdoc />
    public void SetRegeditLastKey(string registryPath)
    {
        if (string.IsNullOrEmpty(registryPath))
            throw new ArgumentException("registryPath must be provided", nameof(registryPath));

        editor.SetLastKey(registryPath);

        // Give the registry a short moment to persist before launching regedit
        Thread.Sleep(100);

        launcher.Start("regedit.exe");
    }

    /// <inheritdoc />
    public void UpdateServiceInfo(string serviceName, string displayName, string description)
    {
        if (string.IsNullOrEmpty(serviceName))
            throw new ArgumentException("serviceName must be provided", nameof(serviceName));

        if (string.IsNullOrEmpty(displayName))
            throw new ArgumentException("displayName must be provided", nameof(displayName));

        var keyPath = $@"SYSTEM\CurrentControlSet\Services\{serviceName}";

        using var key = Registry.LocalMachine.OpenSubKey(keyPath, writable: true);

        if (key == null)
            throw new InvalidOperationException($"Service '{serviceName}' not found in registry.");

        key.SetValue("DisplayName", displayName, RegistryValueKind.String);

        if (!string.IsNullOrEmpty(description))
            key.SetValue("Description", description, RegistryValueKind.String);
        else if (key.GetValue("Description") != null)
            key.DeleteValue("Description", throwOnMissingValue: false);
    }
}
