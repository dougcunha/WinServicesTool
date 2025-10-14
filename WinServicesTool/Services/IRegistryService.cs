namespace WinServicesTool.Services;

/// <summary>
/// Abstraction for operations against the Windows Registry used by the UI.
/// </summary>
public interface IRegistryService
{
    /// <summary>
    /// Sets the LastKey value for Regedit so regedit opens at the specified path.
    /// </summary>
    void SetRegeditLastKey(string registryPath);

    /// <summary>
    /// Updates the display name and description of a Windows service in the registry.
    /// </summary>
    /// <param name="serviceName">The service name to update.</param>
    /// <param name="displayName">The new display name.</param>
    /// <param name="description">The new description.</param>
    void UpdateServiceInfo(string serviceName, string displayName, string description);
}
