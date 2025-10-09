using System.Diagnostics;
using System.Security.Principal;
using Microsoft.Extensions.Logging;

namespace WinServicesTool.Services;

public sealed class PrivilegeService : IPrivilegeService
{
    private readonly ILogger<PrivilegeService> _logger;

    public PrivilegeService(ILogger<PrivilegeService> logger)
        => _logger = logger;

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
            _logger.LogWarning(ex, "Failed to determine administrator status");
            return false;
        }
    }

    public bool AskAndRestartAsAdmin(Form? owner)
    {
        var res = MessageBox.Show
        (
            owner,
            "This application requires administrator privileges to perform this action. Restart as administrator?",
            "Elevation required",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (res != DialogResult.Yes)
            return false;

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

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to relaunch elevated");
            MessageBox.Show(owner, "Unable to start the application with elevated privileges.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            return false;
        }
    }
}
