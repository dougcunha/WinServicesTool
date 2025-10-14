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
}
