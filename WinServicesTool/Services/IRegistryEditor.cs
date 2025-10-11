namespace WinServicesTool.Services;

/// <summary>
/// Abstraction around registry edits used by the UI.
/// </summary>
public interface IRegistryEditor
{
    /// <summary>
    /// Sets the regedit LastKey value for the current user.
    /// </summary>
    void SetLastKey(string registryPath);

    /// <summary>
    /// Sets a DWORD value under HKEY_LOCAL_MACHINE for the specified subkey path.
    /// The subKeyPath should be relative to the root of HKLM (for example: "SYSTEM\\CurrentControlSet\\Services\\MyService").
    /// </summary>
    void SetDwordInLocalMachine(string subKeyPath, string valueName, int value);
}
