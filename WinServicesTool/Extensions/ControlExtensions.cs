namespace WinServicesTool.Extensions;

/// <summary>
/// Helper extension methods for Control to simplify thread-safe UI actions.
/// </summary>
internal static class ControlExtensions
{
    /// <summary>
    /// Invokes the given action on the control's thread if required.
    /// </summary>
    public static void InvokeIfRequired(this Control? control, Action action)
    {
        if (control == null)
            return;

        if (control.InvokeRequired)
            control.Invoke(action);
        else
            action();
    }
}
