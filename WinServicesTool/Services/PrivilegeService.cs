using System.Diagnostics;
using System.Security.Principal;
using Microsoft.Extensions.Logging;

namespace WinServicesTool.Services;

public sealed class PrivilegeService(ILogger<PrivilegeService> logger) : IPrivilegeService
{
    public bool IsAdministrator()
    {
        try
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to determine administrator status");

            return false;
        }
    }

    public void AskAndRestartAsAdmin(Form? owner, bool shouldAsk)
    {
        if (shouldAsk)
        {
            var resp = MessageBox.Show
            (
                owner,
                "This application requires administrator privileges to perform this action. Restart as administrator?",
                "Elevation required",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) == DialogResult.No;

            if (resp)
                return;
        }

        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = Process.GetCurrentProcess().MainModule?.FileName ?? Application.ExecutablePath,
                UseShellExecute = true,
                Verb = "runas",
            };

            Process.Start(psi);
            Application.Exit();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to relaunch elevated");
            MessageBox.Show(owner, "Unable to start the application with elevated privileges.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
