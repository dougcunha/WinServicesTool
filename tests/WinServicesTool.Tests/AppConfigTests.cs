using System.Diagnostics.CodeAnalysis;
using Shouldly;
using WinServicesTool.Utils;
using Xunit;

namespace WinServicesTool.Tests;

[ExcludeFromCodeCoverage]
public sealed class AppConfigTests
{
    [Fact]
    public void Save_WhenCalled_PersistedAndLoadedValuesMatch()
    {
        // Arrange
        var tmpDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tmpDir);
        var prevDir = Environment.CurrentDirectory;

        try
        {
            Environment.CurrentDirectory = tmpDir;

            var cfg = new AppConfig { VisibleColumns = ["Path"], AlwaysStartsAsAdministrator = true };

            // Act
            cfg.Save();

            // Assert
            var loaded = AppConfig.Load();
            loaded.VisibleColumns.ShouldBe(["Path"]);
            loaded.AlwaysStartsAsAdministrator.ShouldBe(true);
        }
        finally
        {
            Environment.CurrentDirectory = prevDir;
            var f = Path.Combine(tmpDir, "app_config.json");
            if (File.Exists(f)) File.Delete(f);

            try
            {
                Directory.Delete(tmpDir);
            }
            catch
            {
                // ignored
            }
        }
    }
}
