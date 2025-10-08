using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WinServicesTool.Utils;

internal static class ColumnWidthStore
{
    private static readonly string AppFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "WinServicesTool");

    private static readonly string FilePath = Path.Combine(AppFolder, "column_widths.json");

    public static void Save(IDictionary<string, int> widths)
    {
        try
        {
            Directory.CreateDirectory(AppFolder);
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FilePath, JsonSerializer.Serialize(widths, options));
        }
        catch
        {
            // Swallow exceptions — non-critical
        }
    }

    public static Dictionary<string, int>? Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return null;
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<Dictionary<string, int>>(json);
        }
        catch
        {
            return null;
        }
    }
}
