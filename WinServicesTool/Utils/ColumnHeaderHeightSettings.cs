namespace WinServicesTool.Utils;

/// <summary>
/// Contains configuration for automatic column header height calculation based on text wrapping.
/// </summary>
public static class ColumnHeaderHeightSettings
{
    /// <summary>
    /// Height for column headers displaying a single line of text.
    /// </summary>
    public const int SINGLE_LINE_HEIGHT = 35;

    /// <summary>
    /// Height for column headers displaying two lines of text.
    /// </summary>
    public const int TWO_LINE_HEIGHT = 45;

    /// <summary>
    /// Height for column headers displaying three or more lines of text.
    /// </summary>
    public const int THREE_LINE_HEIGHT = 60;
}
