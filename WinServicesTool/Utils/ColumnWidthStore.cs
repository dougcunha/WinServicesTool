using System.Text.Json;

namespace WinServicesTool.Utils;

internal static class ColumnWidthStore
{
    private static readonly string _appFolder = Environment.CurrentDirectory;

    private static readonly string _filePath = Path.Combine(_appFolder, "column_widths.json");

    /// <summary>
    /// Saves the given column widths to a JSON file in the application data folder.
    /// </summary>
    /// <param name="widths">
    /// A dictionary mapping column identifiers to their widths in pixels.
    /// </param>
    public static void Save(IDictionary<string, int> widths)
    {
        try
        {
            Directory.CreateDirectory(_appFolder);
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(widths, options));
        }
        catch
        {
            // Swallow exceptions — non-critical
        }
    }

    /// <summary>
    /// Loads a dictionary of string keys and integer values from the configured file path, if the file exists and can
    /// be deserialized.
    /// </summary>
    /// <remarks>Returns <see langword="null"/> if the file does not exist, cannot be read, or contains
    /// invalid data. The returned dictionary may be empty if the file contains no entries.</remarks>
    /// <returns>A <see cref="Dictionary{string, int}"/> containing the deserialized data if the file exists and is valid;
    /// otherwise, <see langword="null"/>.</returns>
    public static Dictionary<string, int>? Load()
    {
        try
        {
            if (!File.Exists(_filePath)) return null;
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<Dictionary<string, int>>(json);
        }
        catch
        {
            return null;
        }
    }
}
