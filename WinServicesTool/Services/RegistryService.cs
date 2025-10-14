namespace WinServicesTool.Services;

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
}
