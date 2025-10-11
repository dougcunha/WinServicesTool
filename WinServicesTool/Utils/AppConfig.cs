using System.ComponentModel;
using System.Text.Json;

namespace WinServicesTool.Utils;

public sealed partial class AppConfig : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets a value indicating whether the main grid should automatically adjust column widths.
    /// </summary>
    public bool AutoWidthColumns { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Path column should be shown in the services grid.
    /// </summary>
    public bool ShowPathColumn { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the application should always start with administrator privileges.
    /// </summary>
    public bool AlwaysStartsAsAdministrator { get; set; }

    private static readonly string _appFolder = Environment.CurrentDirectory;

    private static readonly string _filePath = Path.Combine(_appFolder, "app_config.json");

    public static AppConfig Load()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new AppConfig();

            var json = File.ReadAllText(_filePath);
            var cfg = JsonSerializer.Deserialize<AppConfig>(json);
            return cfg ?? new AppConfig();
        }
        catch
        {
            return new AppConfig();
        }
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory(_appFolder);
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(this, options));
        }
        catch
        {
            // non-critical
        }
    }
}
