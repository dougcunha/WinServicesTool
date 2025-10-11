using System.Diagnostics;

namespace WinServicesTool.Services;

/// <summary>
/// Abstraction for launching processes so calls can be mocked in tests.
/// </summary>
public interface IProcessLauncher
{
    /// <summary>
    /// Starts a process given a filename and optional start info.
    /// </summary>
    Process? Start(string fileName);
}
