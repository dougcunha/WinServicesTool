using Microsoft.Win32;

namespace WinServicesTool.Services;

public sealed class RegistryEditor : IRegistryEditor
{
    public void SetLastKey(string registryPath)
    {
        using var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit") ?? throw new InvalidOperationException("Unable to create or open Regedit LastKey location.");
        key.SetValue("LastKey", registryPath, RegistryValueKind.String);
    }

    public void SetDwordInLocalMachine(string subKeyPath, string valueName, int value)
    {
        if (string.IsNullOrEmpty(subKeyPath))
            throw new ArgumentException("subKeyPath must be provided", nameof(subKeyPath));

        using var key = Registry.LocalMachine.OpenSubKey(subKeyPath, writable: true) ?? throw new InvalidOperationException($"HKLM\\{subKeyPath} not found");
        key.SetValue(valueName, value, RegistryValueKind.DWord);
    }
}
