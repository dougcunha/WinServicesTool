using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceProcess;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using WinServicesTool.Models;
using WinServicesTool.Utils;

namespace WinServicesTool.Forms;

// ReSharper disable AsyncVoidEventHandlerMethod
public partial class FormPrincipal : Form
{
    private BindingList<Service> _servicesList = [];
    private List<Service> _allServices = [];
    private CancellationTokenSource? _filterCts;
    private string? _sortPropertyName;
    private SortOrder _sortOrder = SortOrder.None;
    private readonly ILogger<FormPrincipal> _logger;

    public FormPrincipal(ILogger<FormPrincipal> logger)
    {
        InitializeComponent();
        _logger = logger;
        FormClosing += FormPrincipal_FormClosing;
        GridServs.ColumnWidthChanged += GridServs_ColumnWidthChanged;
        GridServs.ColumnHeaderMouseClick += GridServs_ColumnHeaderMouseClick;
        GridServs.CellFormatting += GridServs_CellFormatting;
        GridServs.CellPainting += GridServs_CellPainting;
        GridServs.SelectionChanged += GridServs_SelectionChanged;
        TxtFilter.TextChanged += TxtFilter_TextChanged;

        // Make header selection color match header background so headers don't show as "selected" in blue
        var hdrStyle = GridServs.ColumnHeadersDefaultCellStyle;
        hdrStyle.SelectionBackColor = hdrStyle.BackColor;
        hdrStyle.SelectionForeColor = hdrStyle.ForeColor;
        GridServs.ColumnHeadersDefaultCellStyle = hdrStyle;

        // On startup, if we are not running elevated, ask the user to restart as admin
        if (IsAdministrator())
        {
            BtnLoad_Click(null, EventArgs.Empty);

            return;
        }

        AppendLog("Application not running as administrator. Prompting for elevation...");
        AskAndRestartAsAdmin();
    }

    private void GridServs_SelectionChanged(object? sender, EventArgs e)
    {
        try
        {
            var sel = GetSelectedServices().ToList();

            if (sel.Count == 0)
            {
                BtnStart.Enabled = BtnStop.Enabled = BtnRestart.Enabled = false;

                return;
            }

            var statuses = sel.Select(s => s.Status).Distinct().ToList();

            if (statuses.Count != 1)
            {
                BtnStart.Enabled = BtnStop.Enabled = BtnRestart.Enabled = false;

                return;
            }

            var st = statuses[0];
            BtnStart.Enabled = st == ServiceControllerStatus.Stopped;
            BtnStop.Enabled = st is ServiceControllerStatus.Running or ServiceControllerStatus.Paused;
            BtnRestart.Enabled = st is ServiceControllerStatus.Running or ServiceControllerStatus.Paused;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error evaluating selection state");
        }
    }

    private void AppendLog(string message, LogLevel level = LogLevel.Information)
    {
        try
        {
            var ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var line = $"[{ts}] {message}{Environment.NewLine}";

            // Log to injected logger
            switch (level)
            {
                case LogLevel.Error:
                    _logger.LogError(message);
                    break;
                case LogLevel.Warning:
                    _logger.LogWarning(message);
                    break;
                default:
                    _logger.LogInformation(message);
                    break;
            }

            // Append to TextLog on UI thread and auto-scroll to bottom
            void appendAndScroll()
            {
                TextLog.AppendText(line);
                // move caret to end and scroll
                TextLog.SelectionStart = TextLog.TextLength;
                TextLog.SelectionLength = 0;
                TextLog.ScrollToCaret();
            }

            if (TextLog.InvokeRequired)
                TextLog.Invoke(appendAndScroll);
            else
                appendAndScroll();
        }
        catch
        {
            // swallow
        }
    }

    private static bool IsAdministrator()
    {
        try
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch
        {
            return false;
        }
    }

    private bool AskAndRestartAsAdmin()
    {
        var res = MessageBox.Show(this, "This application requires administrator privileges to perform this action. Restart as administrator?", "Elevation required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
            MessageBox.Show(this, "Unable to start the application with elevated privileges.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }
    }

    // Draw a custom sort glyph (triangle) in the header so it's visible across themes
    private void GridServs_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        try
        {
            // Only care about header cells
            if (e.RowIndex != -1 || e.ColumnIndex < 0)
                return;

            // Let default paint run first
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);

            if (string.IsNullOrEmpty(_sortPropertyName) || _sortOrder == SortOrder.None)
            {
                e.Handled = true;

                return;
            }

            var col = GridServs.Columns[e.ColumnIndex];

            var propName = !string.IsNullOrEmpty(col.DataPropertyName)
                ? col.DataPropertyName
                : col.Name;

            if (propName != _sortPropertyName)
            {
                e.Handled = true;

                return;
            }

            // Draw a small triangle on the right side of the header
            var g = e.Graphics;

            if (g == null)
            {
                e.Handled = true;

                return;
            }

            var rect = e.CellBounds;
            var size = Math.Min(12, rect.Height - 8);
            var cx = rect.Right - size - 6;
            var cy = rect.Top + (rect.Height / 2);

            Point[] pts = _sortOrder == SortOrder.Ascending
                ? [new Point(cx, cy + (size / 2)), new Point(cx + size, cy + (size / 2)), new Point(cx + (size / 2), cy - (size / 2))]
                : [new Point(cx, cy - (size / 2)), new Point(cx + size, cy - (size / 2)), new Point(cx + (size / 2), cy + (size / 2))];

            using var brush = new SolidBrush(Color.FromArgb(80, 80, 80));
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillPolygon(brush, pts);

            e.Handled = true;
        }
        catch
        {
            // swallow painting errors
        }
    }

    private async void BtnLoad_Click(object? sender, EventArgs e)
    {
        BtnLoad.Enabled = false;
        var previousCursor = Cursor.Current;
        Cursor.Current = Cursors.WaitCursor;
        AppendLog("Loading services...");

        try
        {
            _allServices = await Task.Run(() =>
            {
                return ServiceController.GetServices()
                    .Select(serv => new Service
                    {
                        DisplayName = serv.DisplayName,
                        ServiceName = serv.ServiceName,
                        Status = serv.Status,
                        StartMode = GetStartTypeSafe(serv),
                        ServiceType = serv.ServiceType,
                        CanPauseAndContinue = serv.CanPauseAndContinue,
                        CanShutdown = serv.CanShutdown,
                        CanStop = serv.CanStop
                    })
                    .OrderBy(s => s.DisplayName)
                    .ToList();
            });

            ApplyFilterAndSort();
            ApplyColumnSizing();
            LoadColumnWidths();
            AppendLog($"Loaded {_allServices.Count} services.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Failed to load services: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            AppendLog($"Failed to load services: {ex.Message}", LogLevel.Error);
            _logger.LogError(ex, "Failed to load services");
        }
        finally
        {
            BtnLoad.Enabled = true;
            Cursor.Current = previousCursor;
        }
    }

    private static ServiceStartMode GetStartTypeSafe(ServiceController serv)
    {
        try
        {
            return serv.StartType;
        }
        catch
        {
            return ServiceStartMode.System;
        }
    }

    private void ApplyColumnSizing()
    {
        // Prefer DisplayName and ServiceName to fill available space
        foreach (DataGridViewColumn col in GridServs.Columns)
        {
            col.Resizable = DataGridViewTriState.True;
            // We'll manage the sort glyph programmatically
            col.SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        // Make the main text columns fill remaining space
        var displayCol = GridServs.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.DataPropertyName == "DisplayName");
        var serviceCol = GridServs.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.DataPropertyName == "ServiceName");

        displayCol?.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        serviceCol?.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        // Other columns: size to content
        foreach (var col in GridServs.Columns.Cast<DataGridViewColumn>())
        {
            if (col.AutoSizeMode == DataGridViewAutoSizeColumnMode.NotSet)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
    }

    private void TxtFilter_TextChanged(object? sender, EventArgs e)
    {
        // Debounce filter input
        _filterCts?.Cancel();
        _filterCts = new CancellationTokenSource();
        var ct = _filterCts.Token;
        _ = ApplyFilterWithDelay(ct);
    }

    private async Task ApplyFilterWithDelay(CancellationToken ct)
    {
        try
        {
            // Wait 400ms after last keystroke
            await Task.Delay(400, ct);
            if (ct.IsCancellationRequested) return;
            ApplyFilterImmediately();
        }
        catch (TaskCanceledException)
        {
            // expected on rapid typing
        }
    }

    private void ApplyFilterImmediately()
        => ApplyFilterAndSort();

    private void ApplyFilterAndSort()
    {
        var text = TxtFilter.Text.Trim();

        List<Service> working;
        if (string.IsNullOrEmpty(text))
        {
            working = [.. _allServices];
        }
        else
        {
            var lower = text.ToLowerInvariant();

            working = [.. _allServices.Where(s =>
                (!string.IsNullOrEmpty(s.DisplayName) && s.DisplayName.Contains(lower, StringComparison.InvariantCultureIgnoreCase)) ||
                (!string.IsNullOrEmpty(s.ServiceName) && s.ServiceName.Contains(lower, StringComparison.InvariantCultureIgnoreCase))
            )];
        }

        // Apply sorting if requested
        if (!string.IsNullOrEmpty(_sortPropertyName) && _sortOrder != SortOrder.None)
        {
            var prop = typeof(Service).GetProperty(_sortPropertyName!);

            if (prop != null)
            {
                working = _sortOrder == SortOrder.Ascending
                    ? working.OrderBy(s => prop.GetValue(s, null)).ToList()
                    : working.OrderByDescending(s => prop.GetValue(s, null)).ToList();
            }
        }

        // ChangeStartMode helper methods are defined after this method

        // Update header glyphs and font: reset all, then apply only when there's an active sort (Asc/Desc)
        foreach (DataGridViewColumn c in GridServs.Columns)
        {
            c.HeaderCell.SortGlyphDirection = SortOrder.None;
            // Reset header font
            c.HeaderCell.Style.Font = new Font(GridServs.Font, FontStyle.Regular);
        }

        if (!string.IsNullOrEmpty(_sortPropertyName) && _sortOrder != SortOrder.None)
        {
            var col = GridServs.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.DataPropertyName == _sortPropertyName || c.Name == _sortPropertyName);
            if (col != null)
            {
                col.HeaderCell.SortGlyphDirection = _sortOrder;
                col.HeaderCell.Style.Font = new Font(GridServs.Font, FontStyle.Bold);
            }
        }

        _servicesList = new BindingList<Service>(working);
        serviceBindingSource.DataSource = _servicesList;
        serviceBindingSource.ResetBindings(false);

        // Ensure the DataGridView repaints so the sort glyph is shown/cleared immediately
        GridServs.Refresh();

        // If we have a sorted column, invalidate its header to ensure glyph is painted
        if (string.IsNullOrEmpty(_sortPropertyName) || _sortOrder == SortOrder.None)
            return;

        var idx = GridServs.Columns.Cast<DataGridViewColumn>().ToList().FindIndex(c => c.DataPropertyName == _sortPropertyName || c.Name == _sortPropertyName);

        if (idx < 0)
            return;

        GridServs.InvalidateColumn(idx);
        // additional aggressive repaints
        GridServs.Invalidate();
        GridServs.Update();
        // clear any selection that might leave header visually selected
        GridServs.ClearSelection();
    }

    private void BtnChangeStartMode_Click(object? sender, EventArgs e)
    {
        var sel = GetSelectedServices().ToList();
        if (!sel.Any()) return;

        // Preselect current start mode from first selected service
        var initial = sel.First().StartMode switch
        {
            ServiceStartMode.Automatic => "Automatic",
            ServiceStartMode.Manual => "Manual",
            ServiceStartMode.Disabled => "Disabled",
            _ => "Manual",
        };

        using var dlg = new FormChangeStartMode(initial);

        if (dlg.ShowDialog(this) != DialogResult.OK)
            return;

        var newMode = dlg.SelectedMode; // 'Automatic' | 'Manual' | 'Disabled'

        AppendLog($"Changing StartType to {newMode} for {sel.Count} service(s)...");

        Task.Run(() =>
        {
            var results = new List<string>();
            foreach (var s in sel)
            {
                try
                {
                    using var key = Registry.LocalMachine.OpenSubKey($"SYSTEM\\CurrentControlSet\\Services\\{s.ServiceName}", writable: true);
                    if (key == null)
                    {
                        var msg = $"[{s.ServiceName}] Registry key not found.";
                        AppendLog(msg, LogLevel.Warning);
                        results.Add(msg);
                        continue;
                    }

                    var startValue = StartModeToDword(newMode);
                    key.SetValue("Start", startValue, RegistryValueKind.DWord);

                    var okMsg = $"[{s.ServiceName}] StartType defined as {newMode} (value {startValue}).";
                    AppendLog(okMsg, LogLevel.Information);
                    results.Add(okMsg);
                }
                catch (Exception ex)
                {
                        var err = $"[{s.ServiceName}] Failed to change StartType: {ex.Message}";
                    AppendLog(err, LogLevel.Error);
                    results.Add(err);
                }
            }

            // Refresh list on UI thread and show summary
            Invoke(() =>
            {
                var summary = string.Join(Environment.NewLine, results);
                MessageBox.Show(this, summary, $"Start Type changed to \"{newMode}\".", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnLoad_Click(this, EventArgs.Empty);
            });
        });
    }

    private static int StartModeToDword(string mode)
    {
        // Registry Start values:
        // 2 = Automatic
        // 3 = Manual
        // 4 = Disabled
        return mode switch
        {
            "Automatic" => 2,
            "Manual" => 3,
            "Disabled" => 4,
            _ => 3,
        };
    }

    private void GridServs_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex < 0 || e.ColumnIndex >= GridServs.Columns.Count)
            return;

        var col = GridServs.Columns[e.ColumnIndex];

        var propName = !string.IsNullOrEmpty(col.DataPropertyName)
            ? col.DataPropertyName
            : col.Name;

        if (_sortPropertyName == propName)
        {
            _sortOrder = _sortOrder switch
            {
                // cycle: None -> Asc -> Desc -> None
                SortOrder.None => SortOrder.Ascending,
                SortOrder.Ascending => SortOrder.Descending,
                _ => SortOrder.None
            };
            if (_sortOrder == SortOrder.None)
            {
                // clear property when cycling back to no-sort
                _sortPropertyName = null;
            }
        }
        else
        {
            _sortPropertyName = propName;
        }

        ApplyFilterAndSort();
    }

    private void GridServs_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        try
        {
            var item = _servicesList[e.RowIndex];

            // Color the entire row by status
            var row = GridServs.Rows[e.RowIndex];

            switch (item.Status)
            {
                case ServiceControllerStatus.Running:
                    row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230); // light green
                    break;
                case ServiceControllerStatus.Stopped:
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230); // light red
                    break;
                case ServiceControllerStatus.Paused:
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 230); // light yellow
                    break;
                default:
                    row.DefaultCellStyle.BackColor = Color.Empty;
                    break;
            }

            // For the Status column, prefix with an emoji
            var col = GridServs.Columns[e.ColumnIndex];

            if (col.DataPropertyName == "Status" && e.Value != null)
            {
                var status = (ServiceControllerStatus)e.Value;
                var prefix = status switch
                {
                    ServiceControllerStatus.Running => "🟢 ",
                    ServiceControllerStatus.Stopped => "🔴 ",
                    ServiceControllerStatus.Paused => "🟡 ",
                    _ => "⚪ "
                };

                e.Value = prefix + e.Value;
                e.FormattingApplied = true;
            }
        }
        catch
        {
            // ignore formatting errors
        }
    }

    private IEnumerable<Service> GetSelectedServices()
    {
        return GridServs.SelectedRows.Cast<DataGridViewRow>()
            .Select(r => r.DataBoundItem as Service)
            .Where(s => s != null)!
            .Cast<Service>();
    }

    private async void BtnStart_Click(object? sender, EventArgs e)
    {
        if (!IsAdministrator())
        {
            AppendLog("Operation requires administrator. Asking to restart elevated...");

            if (AskAndRestartAsAdmin())
                return;
        }

        var sel = GetSelectedServices().ToList();

        if (sel.Count == 0)
            return;

        // Only allow starting services that are stopped
        if (sel.Any(s => s.Status != ServiceControllerStatus.Stopped))
        {
            MessageBox.Show(this, "Please select only services that are stopped to start.", "Start services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            AppendLog("Start aborted: selection contains non-stopped services.", LogLevel.Warning);

            return;
        }

        if (MessageBox.Show(this, $"Start {sel.Count} service(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        BtnStart.Enabled = false;
        AppendLog($"Starting {sel.Count} service(s)...");

        try
        {
            await Task.Run(() =>
            {
                foreach (var s in sel)
                {
                    try
                    {
                        using var sc = new ServiceController(s.ServiceName);

                        if (sc.Status == ServiceControllerStatus.Stopped)
                        {
                            sc.Start();
                            sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                            AppendLog($"Started: {s.ServiceName} ({s.DisplayName})");
                            _logger.LogInformation("Started service {ServiceName}", s.ServiceName);
                        }
                    }
                    catch (Exception ex)
                    {
                        AppendLog($"Failed to start {s.ServiceName}: {ex.Message}", LogLevel.Error);
                        _logger.LogError(ex, "Failed to start service {ServiceName}", s.ServiceName);
                    }
                }
            });

            AppendLog($"Start operation completed for {sel.Count} service(s).");
        }
        finally
        {
            BtnStart.Enabled = true;
            // Refresh list to show updated statuses
            AppendLog("Refreshing services list after start operation...");
            BtnLoad_Click(this, EventArgs.Empty);
        }
    }

    private async void BtnStop_Click(object? sender, EventArgs e)
    {
        if (!IsAdministrator())
        {
            AppendLog("Operation requires administrator. Asking to restart elevated...");

            if (AskAndRestartAsAdmin())
                return;
        }

        var sel = GetSelectedServices().ToList();

        if (sel.Count == 0)
            return;

        // Only allow stopping services that are running or paused
        if (sel.Any(s => s.Status != ServiceControllerStatus.Running && s.Status != ServiceControllerStatus.Paused))
        {
            MessageBox.Show(this, "Please select only services that are running or paused to stop.", "Stop services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            AppendLog("Stop aborted: selection contains services not running/paused.", LogLevel.Warning);

            return;
        }

        if (MessageBox.Show(this, $"Stop {sel.Count} service(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        BtnStop.Enabled = false;
        AppendLog($"Stopping {sel.Count} service(s)...");
        try
        {
            await Task.Run(() =>
            {
                foreach (var s in sel)
                {
                    try
                    {
                        using var sc = new ServiceController(s.ServiceName);

                        if (sc.Status is ServiceControllerStatus.Running or ServiceControllerStatus.Paused)
                        {
                            sc.Stop();
                            sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                            AppendLog($"Stopped: {s.ServiceName} ({s.DisplayName})");
                            _logger.LogInformation("Stopped service {ServiceName}", s.ServiceName);
                        }
                    }
                    catch (Exception ex)
                    {
                        AppendLog($"Failed to stop {s.ServiceName}: {ex.Message}", LogLevel.Error);
                        _logger.LogError(ex, "Failed to stop service {ServiceName}", s.ServiceName);
                    }
                }
            });

            AppendLog($"Stop operation completed for {sel.Count} service(s).");
        }
        finally
        {
            BtnStop.Enabled = true;
            AppendLog("Refreshing services list after stop operation...");
            BtnLoad_Click(this, EventArgs.Empty);
        }
    }

    private async void BtnRestart_Click(object? sender, EventArgs e)
    {
        if (!IsAdministrator())
        {
            AppendLog("Operation requires administrator. Asking to restart elevated...");

            if (AskAndRestartAsAdmin())
                return;
        }

        var sel = GetSelectedServices().ToList();

        if (sel.Count == 0)
            return;

        // For restart, require services to be running or paused
        if (sel.Any(s => s.Status != ServiceControllerStatus.Running && s.Status != ServiceControllerStatus.Paused))
        {
            MessageBox.Show(this, "Please select only services that are running or paused to restart.", "Restart services", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return;
        }

        if (MessageBox.Show(this, $"Restart {sel.Count} service(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        BtnRestart.Enabled = false;
        AppendLog($"Restarting {sel.Count} service(s)...");

        try
        {
            await Task.Run(() =>
            {
                foreach (var s in sel)
                {
                    try
                    {
                        using var sc = new ServiceController(s.ServiceName);

                        if (sc.Status is ServiceControllerStatus.Running or ServiceControllerStatus.Paused)
                        {
                            sc.Stop();
                            sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                            sc.Start();
                            sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                            AppendLog($"Restarted: {s.ServiceName} ({s.DisplayName})");
                            _logger.LogInformation("Restarted service {ServiceName}", s.ServiceName);
                        }
                    }
                    catch (Exception ex)
                    {
                        AppendLog($"Failed to restart {s.ServiceName}: {ex.Message}", LogLevel.Error);
                        _logger.LogError(ex, "Failed to restart service {ServiceName}", s.ServiceName);
                    }
                }
            });
            AppendLog($"Restart operation completed for {sel.Count} service(s).");
        }
        finally
        {
            BtnRestart.Enabled = true;
            AppendLog("Refreshing services list after restart operation...");
            BtnLoad_Click(this, EventArgs.Empty);
        }

        // duplicate WMI-based method removed; registry-based implementation exists earlier
        }

    private void LoadColumnWidths()
    {
        try
        {
            var map = ColumnWidthStore.Load();
            if (map == null) return;

            foreach (DataGridViewColumn col in GridServs.Columns)
            {
                if (!map.TryGetValue(col.Name, out var w) || w <= 0)
                    continue;

                col.Width = w;
                // When width explicitly set, change autosize to None so it persists
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
        }
        catch
        {
            // ignore
        }
    }

    private void SaveColumnWidths()
    {
        try
        {
            var map = GridServs.Columns.Cast<DataGridViewColumn>().ToDictionary(c => c.Name, c => c.Width);
            ColumnWidthStore.Save(map);
        }
        catch
        {
            // ignore
        }
    }

    private void GridServs_ColumnWidthChanged(object? sender, DataGridViewColumnEventArgs e)
        => SaveColumnWidths();

    private void FormPrincipal_FormClosing(object? sender, FormClosingEventArgs e)
        => SaveColumnWidths();

    private void BtnBestFitColumns_Click(object sender, EventArgs e)
        => ApplyColumnSizing();

    private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
    {
        // Se apertar ctrl + k , foca no filtro
        if (e is { Control: true, KeyCode: Keys.K })
        {

            TxtFilter.Focus();
            e.Handled = true;
        }

        // Se apertar ESC no filtro, limpa o filtro
        if (e is { KeyCode: Keys.Escape })
        {
            TxtFilter.Clear();
            e.Handled = true;
        }

        // Se apertar F5, recarrega a lista
        if (e is { KeyCode: Keys.F5 })
        {
            BtnLoad_Click(sender, e);
            e.Handled = true;
        }

        // Se apertas F3 ajusta as colunas
        if (e is { KeyCode: Keys.F3 })
        {
            ApplyColumnSizing();
            e.Handled = true;
        }
    }
}