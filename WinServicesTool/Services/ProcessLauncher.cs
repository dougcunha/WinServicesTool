using System.Diagnostics;

namespace WinServicesTool.Services;

public sealed class ProcessLauncher : IProcessLauncher
{
    public Process? Start(string fileName)
    {
        var psi = new ProcessStartInfo
        {
            FileName = fileName,
            UseShellExecute = true
        };

        return Process.Start(psi);
    }
}
