using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WinServicesTool.Utils;

public sealed partial class AppConfig : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the list of visible column names in the services grid.
    /// </summary>
    public List<string> VisibleColumns { get; set; } = [];

    /// <summary>
    /// Gets or sets the ordered list of column names representing their display order in the grid.
    /// Column names are stored in the order they should appear from left to right.
    /// </summary>
    public List<string> ColumnOrder { get; set; } = [];

    /// <summary>
    /// Gets or sets a dictionary mapping column names to their custom FillWeight values.
    /// Only columns with user-modified widths are stored. Empty dictionary means all columns use default weights.
    /// </summary>
    public Dictionary<string, float> ColumnFillWeights { get; set; } = [];

    /// <summary>
    /// Gets a value indicating whether the Path column is currently visible in the view.
    /// </summary>
    [JsonIgnore]
    public bool IsPathColumnVisible
        => VisibleColumns.Count is 0 || VisibleColumns.Contains("ColPath");

    /// <summary>
    /// Gets or sets a value indicating whether the application should always start with administrator privileges.
    /// </summary>
    public bool AlwaysStartsAsAdministrator { get; set; }

    /// <summary>
    /// Saved left position of the main window.
    /// </summary>
    public int WindowLeft { get; set; }

    /// <summary>
    /// Saved top position of the main window.
    /// </summary>
    public int WindowTop { get; set; }

    /// <summary>
    /// Saved width of the main window.
    /// </summary>
    public int WindowWidth { get; set; }

    /// <summary>
    /// Saved height of the main window.
    /// </summary>
    public int WindowHeight { get; set; }

    /// <summary>
    /// Saved window state (Normal, Minimized, Maximized).
    /// </summary>
    public string? WindowState { get; set; }

    /// <summary>
    /// Saved SplitContainer.SplitterDistance for the main split (log/grid).
    /// </summary>
    public int SplitterDistance { get; set; }

    // Use the application's base directory (where the executable lives) so load/save
    // happen from the same location regardless of current working directory.
    private static readonly string _appFolder = AppContext.BaseDirectory;

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
