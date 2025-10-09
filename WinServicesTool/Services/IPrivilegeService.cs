namespace WinServicesTool.Services;

/// <summary>
/// Abstraction for privilege and elevation related operations.
/// </summary>
public interface IPrivilegeService
{
    /// <summary>
    /// Checks if the current process is running as administrator.
    /// </summary>
    bool IsAdministrator();

    /// <summary>
    /// Prompt the user to restart the application as administrator and attempt to relaunch.
    /// Returns true if the relaunch was initiated.
    /// </summary>
    void AskAndRestartAsAdmin(Form? owner, bool shouldAsk);
}
