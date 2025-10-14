using System.ComponentModel;
using System.ServiceProcess;
using Microsoft.Extensions.Logging;
using WinServicesTool.Extensions;
using WinServicesTool.Services;
using WinServicesTool.Utils;

namespace WinServicesTool.Forms;

// ReSharper disable AsyncVoidEventHandlerMethod
public sealed partial class FormMain : Form
{
    // App configuration
    private readonly AppConfig _appConfig;

    // Timer used to debounce form resize events when AutoWidthColumns is enabled
    private BindingList<ServiceConfiguration> _servicesList = [];
    private List<ServiceConfiguration> _allServices = [];
    private CancellationTokenSource? _filterCts;
    private string? _sortPropertyName;
    private SortOrder _sortOrder = SortOrder.None;
    private readonly ILogger<FormMain> _logger;
    private readonly IServicePathHelper _serviceHelper;
    private readonly IPrivilegeService _privilegeService;
    private readonly IServiceOperationOrchestrator _orchestrator;
    private readonly IRegistryService _registryService;
    private readonly IRegistryEditor _registryEditor;
    private CancellationTokenSource? _currentOperationCts;
    private readonly bool _shouldSaveOnClose;
    private readonly bool _isRunningAsAdmin;
    private readonly bool _restartingAsAdmin;

    // Store original FillWeight values to detect user modifications
    private readonly Dictionary<string, float> _originalFillWeights = [];

    // Flag to prevent saving column settings during initialization
    private bool _isInitializingColumns;

    public FormMain
    (
        ILogger<FormMain> logger,
        IServicePathHelper serviceHelper,
        IPrivilegeService privilegeService,
        IServiceOperationOrchestrator orchestrator,
        IRegistryService registryService,
        IRegistryEditor registryEditor,
        AppConfig appConfig
    )
    {
        InitializeComponent();

        // Show the app name and version in the title bar
        Text = $"Windows Services Tool v{Application.ProductVersion}";
        _isRunningAsAdmin = privilegeService.IsAdministrator();
        ProgressBar.Visible = false;
        LblStatusServices.Text = "Loading...";
        LblStatusServicesRunning.Text = "Loading...";
        _appConfig = appConfig;
        _logger = logger;
        _serviceHelper = serviceHelper;
        _privilegeService = privilegeService;
        _orchestrator = orchestrator;
        _registryService = registryService;
        _registryEditor = registryEditor;

        // Ensure Cancel button starts disabled
        BtnCancel.Enabled = false;
        _appConfig.PropertyChanged += AppConfigChanged;
        FormClosing += FormPrincipal_FormClosing;
        GridServs.ColumnHeaderMouseClick += GridServs_ColumnHeaderMouseClick;
        GridServs.CellFormatting += GridServs_CellFormatting;
        GridServs.CellPainting += GridServs_CellPainting;
        GridServs.MouseUp += GridServs_MouseUp;
        GridServs.SelectionChanged += GridServs_SelectionChanged;
        GridServs.ColumnWidthChanged += GridServs_ColumnWidthChanged;
        GridServs.ColumnDisplayIndexChanged += GridServs_ColumnDisplayIndexChanged;
        TxtFilter.TextChanged += TxtFilter_TextChanged;
        CbFilterStatus.SelectedIndexChanged += (_, _) => ApplyFilterAndSort();
        CbFilterStartMode.SelectedIndexChanged += (_, _) => ApplyFilterAndSort();
        Load += FormPrincipal_Load;
        Shown += FormPrincipal_Shown;
        BtnChangeStartMode.Enabled = _isRunningAsAdmin;
        BtnRestartAsAdm.Visible = !_isRunningAsAdmin;
        BtnRestartAsAdm.Click += BtnRestartAsAdm_Click;

        ChkStartAsAdm.DataBindings.Add("Checked", _appConfig, nameof(AppConfig.AlwaysStartsAsAdministrator), false, DataSourceUpdateMode.OnPropertyChanged);

        // Make header selection color match header background so headers don't show as "selected" in blue
        var hdrStyle = GridServs.ColumnHeadersDefaultCellStyle;
        hdrStyle.SelectionBackColor = hdrStyle.BackColor;
        hdrStyle.SelectionForeColor = hdrStyle.ForeColor;
        hdrStyle.WrapMode = DataGridViewTriState.True; // Enable word wrap for multi-line headers
        hdrStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center align headers vertically
        GridServs.ColumnHeadersDefaultCellStyle = hdrStyle;

        if (!_isRunningAsAdmin && _appConfig.AlwaysStartsAsAdministrator)
        {
            _restartingAsAdmin = true;
            _privilegeService.AskAndRestartAsAdmin(this, shouldAsk: false);

            return;
        }

        _shouldSaveOnClose = true;

        // Initialize columns before loading services
        InitializeColumns();

        BtnLoad_Click(null, EventArgs.Empty);
    }

    private void BtnRestartAsAdm_Click(object? sender, EventArgs e)
        => _privilegeService.AskAndRestartAsAdmin(this, shouldAsk: false);

    private void AppConfigChanged(object? sender, PropertyChangedEventArgs e)
    {
        _appConfig.Save();

        // Handle dynamic column visibility changes
        if (e.PropertyName == nameof(AppConfig.VisibleColumns))
            ApplyColumnVisibility();
    }

    /// <summary>
    /// Initializes column order, widths, and visibility from saved configuration.
    /// Must be called before loading data to prevent incorrect saves during initialization.
    /// </summary>
    private void InitializeColumns()
    {
        // Set flag to prevent saving during initialization
        _isInitializingColumns = true;

        try
        {
            RestoreColumnOrder();
            RestoreColumnWidths();
            CaptureOriginalColumnWidths();
            ApplyColumnVisibility();
        }
        finally
        {
            // Allow saving after initialization is complete
            _isInitializingColumns = false;
        }
    }

    // Designer-based filter controls are wired in constructor

    private void FormPrincipal_Load(object? sender, EventArgs e)
    {
        if (_restartingAsAdmin)
        {
            Close();

            return;
        }

        // If columns weren't initialized in constructor (e.g., not admin on first run),
        // initialize them now
        if (_originalFillWeights.Count != 0)
            return;

        InitializeColumns();
    }

    private void FormPrincipal_Shown(object? sender, EventArgs e)
    {
        try
        {
            // Restore window position/size/state from config
            if (_appConfig is { WindowWidth: > 0, WindowHeight: > 0 })
            {
                StartPosition = FormStartPosition.Manual;
                Bounds = new Rectangle(_appConfig.WindowLeft, _appConfig.WindowTop, _appConfig.WindowWidth, _appConfig.WindowHeight);
            }

            if (!string.IsNullOrEmpty(_appConfig.WindowState) && Enum.TryParse<FormWindowState>(_appConfig.WindowState, out var ws))
                WindowState = ws;

            // Restore splitter distance if available
            if (_appConfig.SplitterDistance <= 0)
                return;

            try
            {
                SplitMain.SplitterDistance = _appConfig.SplitterDistance;
            }
            catch
            {
                // ignore invalid splitter values
            }
        }
        catch
        {
            // ignore restore failures
        }
    }

    private void UpdateFilterLists()
    {
        try
        {
            // Preserve previous selections
            var prevStatus = CbFilterStatus.SelectedItem as string;
            var prevStart = CbFilterStartMode.SelectedItem as string;

            CbFilterStatus.Items.Clear();
            CbFilterStatus.Items.Add("All");

            foreach (var st in _allServices.Select(s => s.GetStatus()).Distinct().Order())
                CbFilterStatus.Items.Add(st.ToString());

            if (!string.IsNullOrEmpty(prevStatus) && CbFilterStatus.Items.Contains(prevStatus))
                CbFilterStatus.SelectedItem = prevStatus;
            else
                CbFilterStatus.SelectedIndex = 0;

            CbFilterStartMode.Items.Clear();
            CbFilterStartMode.Items.Add("All");

            foreach (var sm in _allServices.Select(static s => s.GetStartMode()).Distinct().Order())
                CbFilterStartMode.Items.Add(sm.ToString());

            if (!string.IsNullOrEmpty(prevStart) && CbFilterStartMode.Items.Contains(prevStart))
                CbFilterStartMode.SelectedItem = prevStart;
            else
                CbFilterStartMode.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update filter lists");
        }
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

            var statuses = sel.Select(s => s.GetStatus()).Distinct().ToList();

            if (statuses.Count != 1)
            {
                BtnStart.Enabled = BtnStop.Enabled = BtnRestart.Enabled = false;

                return;
            }

            var st = statuses[0];
            BtnStart.Enabled = _isRunningAsAdmin && st == ServiceControllerStatus.Stopped;
            BtnStop.Enabled = _isRunningAsAdmin && st is ServiceControllerStatus.Running or ServiceControllerStatus.Paused;
            BtnRestart.Enabled = _isRunningAsAdmin && st is ServiceControllerStatus.Running or ServiceControllerStatus.Paused;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error evaluating selection state");
        }
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_currentOperationCts is null)
                return;

            AppendLog("Cancelling current operation...");
            _currentOperationCts.Cancel();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while cancelling operation");
        }
    }

    private void GridServs_MouseUp(object? sender, MouseEventArgs e)
    {
        try
        {
            if (e.Button != MouseButtons.Right)
                return;

            // Determine the row under the mouse
            var hit = GridServs.HitTest(e.X, e.Y);

            if (hit.RowIndex < 0)
                return;

            // Select the row if not already selected
            if (!GridServs.Rows[hit.RowIndex].Selected)
            {
                GridServs.ClearSelection();
                GridServs.Rows[hit.RowIndex].Selected = true;
            }

            var selected = GetSelectedServices().ToList();

            if (selected.Count == 0)
                return;

            // Build context menu dynamically based on status
            var menu = new ContextMenuStrip();

            // If all selected are stopped, show Start
            if (selected.All(s => s.GetStatus() == ServiceControllerStatus.Stopped))
            {
                var startItem = new ToolStripMenuItem("Start") { Enabled = true };
                startItem.Click += (_, _) => BtnStart_Click(this, EventArgs.Empty);
                startItem.Enabled = _isRunningAsAdmin;
                menu.Items.Add(startItem);
            }

            // If any selected are running or paused, show Stop and Restart
            if (selected.Any(s => s.GetStatus() is ServiceControllerStatus.Running or ServiceControllerStatus.Paused))
            {
                var stopItem = new ToolStripMenuItem("Stop") { Enabled = true };
                stopItem.Click += (_, _) => BtnStop_Click(this, EventArgs.Empty);
                stopItem.Enabled = _isRunningAsAdmin;
                menu.Items.Add(stopItem);

                var restartItem = new ToolStripMenuItem("Restart") { Enabled = true };
                restartItem.Click += (_, _) => BtnRestart_Click(this, EventArgs.Empty);
                restartItem.Enabled = _isRunningAsAdmin;
                menu.Items.Add(restartItem);
            }

            // Separator
            menu.Items.Add(new ToolStripSeparator());

            // Change start mode
            var changeStart = new ToolStripMenuItem("Change Start Mode...") { Enabled = true };
            changeStart.Click += (_, _) => BtnChangeStartMode_Click(this, EventArgs.Empty);
            changeStart.Enabled = _isRunningAsAdmin;
            menu.Items.Add(changeStart);

            // Go to registry (only for single selection)
            if (selected.Count == 1)
            {
                var goToRegistry = new ToolStripMenuItem("Go to Registry") { Enabled = true };
                goToRegistry.Click += (_, _) => OpenServiceInRegistry(selected[0].ServiceName);
                menu.Items.Add(goToRegistry);
            }

            // Show menu at cursor
            menu.Show(GridServs, new Point(e.X, e.Y));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error showing context menu");
        }
    }

    private void GridServs_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
    {
        // Handle right-click for column visibility
        if (e.Button == MouseButtons.Right)
        {
            try
            {
                // Build context menu for column visibility
                var menu = new ContextMenuStrip();

                var chooseColumnsItem = new ToolStripMenuItem("Choose Visible Columns...") { Enabled = true };
                chooseColumnsItem.Click += async (_, _) => await ShowColumnChooserDialogAsync();
                menu.Items.Add(chooseColumnsItem);

                // Show menu at cursor
                var screenPoint = GridServs.PointToScreen(new Point(e.X, e.Y));
                menu.Show(screenPoint);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error showing column header context menu");
            }

            return;
        }

        // Handle left-click for sorting
        if (e.Button is not MouseButtons.Left || e.ColumnIndex < 0 || e.ColumnIndex >= GridServs.Columns.Count)
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
                var _ => SortOrder.None
            };

            if (_sortOrder == SortOrder.None)
                // clear property when cycling back to no-sort
                _sortPropertyName = null;
        }
        else
        {
            _sortPropertyName = propName;
            _sortOrder = SortOrder.Ascending;
        }

        ApplyFilterAndSort();
    }

    private void GridServs_ColumnWidthChanged(object? sender, DataGridViewColumnEventArgs e)
        => BeginInvoke(UpdateColumnHeaderHeight);

    private void GridServs_ColumnDisplayIndexChanged(object? sender, DataGridViewColumnEventArgs e)
        => SaveColumnOrder();

    private async Task ShowColumnChooserDialogAsync()
    {
        try
        {
            // Get currently visible columns
            var visibleColumns = _appConfig.VisibleColumns;

            using var dlg = new FormColumnChooser([.. GridServs.Columns.Cast<DataGridViewColumn>()], visibleColumns);

            if (await dlg.ShowDialogAsync(this) != DialogResult.OK)
                return;

            // Update config with selected columns
            _appConfig.VisibleColumns = dlg.ChosenColumnNames;
            _appConfig.Save();

            // Apply visibility
            ApplyColumnVisibility();

            AppendLog($"Column visibility updated. {dlg.ChosenColumnNames.Count} columns visible.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error showing column chooser dialog");
            AppendLog($"Failed to update column visibility: {ex.Message}", LogLevel.Error);
        }
    }

    private void ApplyColumnVisibility()
    {
        try
        {
            var visibleColumns = _appConfig.VisibleColumns;

            if (visibleColumns.Count == 0)
                return;

            foreach (DataGridViewColumn col in GridServs.Columns)
                col.Visible = visibleColumns.Contains(col.Name);

            UpdateColumnHeaderHeight();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error applying column visibility");
        }
    }

    /// <summary>
    /// Calculates and updates the column header height based on the maximum number of lines
    /// in visible column headers.
    /// </summary>
    private void UpdateColumnHeaderHeight()
    {
        // Skip during initialization to avoid conflicts with auto-resize
        if (_isInitializingColumns)
            return;

        try
        {
            var maxLines = 1;

            using var g = GridServs.CreateGraphics();
            var headerStyle = GridServs.ColumnHeadersDefaultCellStyle;
            var font = headerStyle.Font ?? GridServs.Font;

            foreach (DataGridViewColumn col in GridServs.Columns)
            {
                if (!col.Visible)
                    continue;

                // Measure the header text to determine how many lines it needs
                var headerText = col.HeaderText;

                if (string.IsNullOrEmpty(headerText))
                    continue;

                try
                {
                    // Calculate available width for text (column width minus padding)
                    var availableWidth = col.Width - 10; // 10px padding

                    if (availableWidth <= 0)
                        continue;

                    var textSize = g.MeasureString(headerText, font, availableWidth);
                    var lineHeight = g.MeasureString("A", font).Height;
                    var lines = (int)Math.Ceiling(textSize.Height / lineHeight);

                    if (lines > maxLines)
                        maxLines = lines;
                }
                catch (InvalidOperationException)
                {
                    // Ignore if operation cannot be performed during auto-resize
                    // This can happen during Fill mode adjustments
                }
            }

            // Set height based on maximum number of lines
            try
            {
                GridServs.ColumnHeadersHeight = maxLines switch
                {
                    1 => ColumnHeaderHeightSettings.SINGLE_LINE_HEIGHT,
                    2 => ColumnHeaderHeightSettings.TWO_LINE_HEIGHT,
                    var _ => ColumnHeaderHeightSettings.THREE_LINE_HEIGHT
                };

                _logger.LogDebug("Column header height updated to {Height} for {MaxLines} lines", GridServs.ColumnHeadersHeight, maxLines);
            }
            catch (InvalidOperationException)
            {
                _logger.LogTrace("Column header height update deferred due to auto-resize");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating column header height");
        }
    }


    /// <summary>
    /// Saves the current display order of columns to the configuration.
    /// </summary>
    private void SaveColumnOrder()
    {
        // Don't save during initialization
        if (_isInitializingColumns)
            return;

        try
        {
            // Sort columns by their DisplayIndex to get the actual display order
            var columnOrder = GridServs.Columns
                .Cast<DataGridViewColumn>()
                .OrderBy(col => col.DisplayIndex)
                .Select(col => col.Name)
                .ToList();

            _appConfig.ColumnOrder = columnOrder;
            _appConfig.Save();

            _logger.LogDebug("Saved column order: {ColumnOrder}", string.Join(", ", columnOrder));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving column order");
        }
    }

    /// <summary>
    /// Restores the display order of columns from the configuration.
    /// </summary>
    private void RestoreColumnOrder()
    {
        try
        {
            if (_appConfig.ColumnOrder.Count == 0)
            {
                _logger.LogDebug("No saved column order found");

                return;
            }

            // Create a dictionary to quickly look up columns by name
            var columnDict = GridServs.Columns.Cast<DataGridViewColumn>()
                .ToDictionary(col => col.Name, col => col);

            // Apply the saved order
            var displayIndex = 0;

            foreach (var columnName in _appConfig.ColumnOrder)
            {
                if (!columnDict.TryGetValue(columnName, out var column))
                    continue;

                column.DisplayIndex = displayIndex;
                displayIndex++;
            }

            // Handle any columns that weren't in the saved order (e.g., newly added columns)
            foreach (var col in GridServs.Columns.Cast<DataGridViewColumn>().Where(col => !_appConfig.ColumnOrder.Contains(col.Name)))
            {
                col.DisplayIndex = displayIndex;
                displayIndex++;
            }

            _logger.LogDebug("Restored column order from config");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error restoring column order");
        }
    }

    /// <summary>
    /// Captures the original FillWeight values of all columns.
    /// Should be called once after the form is initialized and before user interactions.
    /// </summary>
    private void CaptureOriginalColumnWidths()
    {
        try
        {
            _originalFillWeights.Clear();

            foreach (DataGridViewColumn col in GridServs.Columns)
                _originalFillWeights[col.Name] = col.FillWeight;

            _logger.LogDebug("Captured original FillWeight values for {Count} columns", _originalFillWeights.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error capturing original column widths");
        }
    }

    /// <summary>
    /// Saves the current FillWeight of columns that have been modified by the user.
    /// Only stores columns with widths different from their original values.
    /// </summary>
    private void SaveColumnWidths()
    {
        // Don't save during initialization
        if (_isInitializingColumns)
            return;

        try
        {
            var modifiedWidths = new Dictionary<string, float>();

            foreach (DataGridViewColumn col in GridServs.Columns)
            {
                // Only save if the FillWeight has changed from the original
                if (!_originalFillWeights.TryGetValue(col.Name, out var originalWeight))
                    continue;

                // Use a small tolerance for floating point comparison
                if (Math.Abs(col.FillWeight - originalWeight) > 0.01f)
                    modifiedWidths[col.Name] = col.FillWeight;
            }

            _appConfig.ColumnFillWeights = modifiedWidths;
            _appConfig.Save();

            if (modifiedWidths.Count > 0)
                _logger.LogDebug("Saved {Count} modified column widths", modifiedWidths.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving column widths");
        }
    }

    /// <summary>
    /// Restores saved FillWeight values for columns that were previously modified.
    /// </summary>
    private void RestoreColumnWidths()
    {
        try
        {
            if (_appConfig.ColumnFillWeights.Count == 0)
            {
                _logger.LogDebug("No saved column widths found");

                return;
            }

            // Temporarily disable auto-size to prevent the grid from adjusting other columns
            var originalAutoSizeMode = GridServs.AutoSizeColumnsMode;
            GridServs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            try
            {
                foreach (var kvp in _appConfig.ColumnFillWeights)
                {
                    var columnName = kvp.Key;
                    var fillWeight = kvp.Value;

                    var column = GridServs.Columns.Cast<DataGridViewColumn>()
                        .FirstOrDefault(col => col.Name == columnName);

                    if (column != null)
                    {
                        column.FillWeight = fillWeight;
                        _logger.LogTrace("Restored FillWeight {FillWeight} for column {ColumnName}", fillWeight, columnName);
                    }
                }

                _logger.LogDebug("Restored {Count} column widths from config", _appConfig.ColumnFillWeights.Count);
            }
            finally
            {
                // Restore auto-size mode
                GridServs.AutoSizeColumnsMode = originalAutoSizeMode;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error restoring column widths");
        }
    }

    internal void OpenServiceInRegistry(string serviceName)
    {
        try
        {
            // Registry path for the service
            var registryPath = $"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\{serviceName}";

            AppendLog($"Attempting to open registry at: {registryPath}");

            try
            {
                _registryService.SetRegeditLastKey(registryPath);
                AppendLog($"Requested Regedit to open at: {registryPath}");
            }
            catch (Exception ex)
            {
                AppendLog($"Failed to open registry for {serviceName}: {ex.Message}", LogLevel.Error);
                _logger.LogError(ex, "Failed to open registry for service {ServiceName}", serviceName);
            }
        }
        catch (Exception ex)
        {
            AppendLog($"Failed to open registry for {serviceName}: {ex.Message}", LogLevel.Error);
            _logger.LogError(ex, "Failed to open registry for service {ServiceName}", serviceName);
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
#pragma warning disable CA2254
                    _logger.LogError(message);
                    break;
                case LogLevel.Warning:
                    _logger.LogWarning(message);
                    break;
                default:
                    _logger.LogInformation(message);
#pragma warning restore CA2254
                    break;
            }

            // Append to TextLog on UI thread and auto-scroll to bottom
            void AppendAndScroll()
            {
                TextLog.AppendText(line);
                // move caret to end and scroll
                TextLog.SelectionStart = TextLog.TextLength;
                TextLog.SelectionLength = 0;
                TextLog.ScrollToCaret();
            }

            TextLog.InvokeIfRequired(AppendAndScroll);
        }
        catch
        {
            // swallow
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
            var graphicsContext = e.Graphics;

            if (graphicsContext == null)
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
            graphicsContext.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphicsContext.FillPolygon(brush, pts);

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
            ProgressBar.Visible = true;
            var servicesNames = _serviceHelper.GetAllServiceNames();
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = servicesNames.Length;
            ProgressBar.Step = 1;

            var allServices = await _serviceHelper.GetServiceConfigurationsAsync
            (
                servicesNames,
                new Progress<int>(completed => this.InvokeIfRequired(() => ProgressBar.Value = completed)),
                CancellationToken.None
            );

            _allServices = [.. allServices.Values.Where(s => s != null).Select(s => s!).OrderBy(s => s.DisplayName)];

            // Update the filter dropdowns to show only values present in the loaded list
            UpdateFilterLists();

            // Now populate the grid with data
            ApplyFilterAndSort();

            // Update column header height after data is loaded and columns have their final widths
            // Use a small delay to ensure all auto-resize operations are complete
            await Task.Delay(50);
            UpdateColumnHeaderHeight();

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
            ProgressBar.Visible = false;
            BtnLoad.Enabled = true;
            Cursor.Current = previousCursor;
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

            if (ct.IsCancellationRequested)
                return;

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

        List<ServiceConfiguration> working;

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

        // Apply column filters if present
        try
        {
            if (CbFilterStatus.SelectedItem is string statusSel && !string.Equals(statusSel, "All", StringComparison.OrdinalIgnoreCase))
                if (Enum.TryParse<ServiceControllerStatus>(statusSel, out var parsedStatus))
                    working = [.. working.Where(s => s.GetStatus() == parsedStatus)];

            if (CbFilterStartMode.SelectedItem is string startSel && !string.Equals(startSel, "All", StringComparison.OrdinalIgnoreCase))
                if (Enum.TryParse<ServiceStartMode>(startSel, out var parsedStart))
                    working = [.. working.Where(s => s.GetStartMode() == parsedStart)];
        }
        catch
        {
            // swallow filter errors — filters are convenience UI only
        }

        // Apply sorting if requested
        if (!string.IsNullOrEmpty(_sortPropertyName) && _sortOrder != SortOrder.None)
        {
            var prop = typeof(ServiceConfiguration).GetProperty(_sortPropertyName!);

            if (prop != null)
                working = _sortOrder == SortOrder.Ascending
                ? [.. working.OrderBy(s => prop.GetValue(s, null))]
                : [.. working.OrderByDescending(s => prop.GetValue(s, null))];
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

        _servicesList = new BindingList<ServiceConfiguration>(working);
        UpdateServiceStatusLabels();
        _servicesList.ListChanged += ServicesList_ListChanged;
        serviceBindingSource.DataSource = _servicesList;
        serviceBindingSource.ResetBindings(false);

        // Ensure the DataGridView repaints so the sort glyph is shown/cleared immediately
        GridServs.Refresh();

        // If we have a sorted column, invalidate its header to ensure glyph is painted
        if (string.IsNullOrEmpty(_sortPropertyName) || _sortOrder == SortOrder.None)
            return;

        var idx = GridServs.Columns.Cast<DataGridViewColumn>()
            .ToList()
            .FindIndex(c => c.DataPropertyName == _sortPropertyName || c.Name == _sortPropertyName);

        if (idx < 0)
            return;

        GridServs.InvalidateColumn(idx);
        // additional aggressive repaints
        GridServs.Invalidate();
        GridServs.Update();
        // clear any selection that might leave header visually selected
        GridServs.ClearSelection();
    }

    private void ServicesList_ListChanged(object? sender, ListChangedEventArgs e)
        => UpdateServiceStatusLabels();

    private void UpdateServiceStatusLabels()
    {
        LblStatusServices.Text = $"Services shown: {_servicesList.Count}";
        LblStatusServicesRunning.Text = $"Running: {_servicesList.Count(s => s.GetStatus() == ServiceControllerStatus.Running)}";
    }

    private void BtnChangeStartMode_Click(object? sender, EventArgs e)
    {
        var selecteds = GetSelectedServices().ToList();

        if (selecteds.Count == 0)
            return;

        // Preselect current start mode from first selected service
        var initial = selecteds[0].GetStartMode() switch
        {
            ServiceStartMode.Automatic => "Automatic",
            ServiceStartMode.Manual => "Manual",
            ServiceStartMode.Disabled => "Disabled",
            var _ => "Manual",
        };

        using var dlg = new FormChangeStartMode(initial);

        if (dlg.ShowDialog(this) != DialogResult.OK)
            return;

        var newMode = dlg.SelectedMode; // 'Automatic' | 'Manual' | 'Disabled'

        AppendLog($"Changing StartType to {newMode} for {selecteds.Count} service(s)...");

        Task.Run(() =>
        {
            var results = new List<string>();

            foreach (var serv in selecteds)
                try
                {
                    var subKey = $"SYSTEM\\CurrentControlSet\\Services\\{serv.ServiceName}";
                    var startValue = StartModeToDword(newMode);

                    _registryEditor.SetDwordInLocalMachine(subKey, "Start", startValue);

                    serv.StartType = newMode switch
                    {
                        "Automatic" => StartType.Automatic,
                        "Manual" => StartType.Manual,
                        "Disabled" => StartType.Disabled,
                        var _ => serv.StartType
                    };

                    var okMsg = $"[{serv.ServiceName}] StartType defined as {newMode} (value {startValue}).";
                    AppendLog(okMsg);
                    results.Add(okMsg);
                }
                catch (Exception ex)
                {
                    var err = $"[{serv.ServiceName}] Failed to change StartType: {ex.Message}";
                    AppendLog(err, LogLevel.Error);
                    results.Add(err);
                }

            // Refresh list on UI thread and show summary
            this.InvokeIfRequired(() =>
            {
                var summary = string.Join(Environment.NewLine, results);
                MessageBox.Show(this, summary, $"Start Type changed to \"{newMode}\".", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        });
    }

    private static int StartModeToDword(string mode)
        => mode switch
        {
            "Automatic" => 2,
            "Manual" => 3,
            "Disabled" => 4,
            var _ => 3,
        };

    private void GridServs_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        try
        {
            var item = _servicesList[e.RowIndex];

            // Color the entire row by status
            var row = GridServs.Rows[e.RowIndex];

            row.DefaultCellStyle.BackColor = item.GetStatus() switch
            {
                ServiceControllerStatus.Running => Color.FromArgb(230, 255, 230), // light green
                ServiceControllerStatus.Stopped => Color.FromArgb(255, 230, 230), // light red
                ServiceControllerStatus.Paused => Color.FromArgb(255, 255, 230),  // light yellow
                var _ => Color.Empty,
            };

            // For the Status column, prefix with an emoji
            var col = GridServs.Columns[e.ColumnIndex];

            if (col.DataPropertyName != "CurrentState" || e.Value == null)
                return;

            var status = (ServiceState)e.Value;

            var prefix = status switch
            {
                ServiceState.Running => "🟢 ",
                ServiceState.Stopped => "🔴 ",
                ServiceState.Paused => "🟡 ",
                var _ => "⚪ "
            };

            e.Value = prefix + status.ToServiceControllerStatus();
            e.FormattingApplied = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error formatting cell");
        }
    }

    private IEnumerable<ServiceConfiguration> GetSelectedServices()
        => GridServs.SelectedRows.Cast<DataGridViewRow>()
            .Select(r => r.DataBoundItem as ServiceConfiguration)
            .Where(s => s != null)
            .Cast<ServiceConfiguration>();

    private async void BtnStart_Click(object? sender, EventArgs e)
    {
        var selectedServices = GetSelectedServices().ToList();

        if (selectedServices.Count == 0)
            return;

        // Only allow starting services that are stopped
        if (selectedServices.Any(s => s.GetStatus() != ServiceControllerStatus.Stopped))
        {
            MessageBox.Show(this, "Please select only services that are stopped to start.", "Start services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            AppendLog("Start aborted: selection contains non-stopped services.", LogLevel.Warning);

            return;
        }

        BtnStart.Enabled = false;
        BtnCancel.Enabled = true;
        AppendLog($"Starting {selectedServices.Count} service(s)...");

        // ReSharper disable once MethodHasAsyncOverload
        _currentOperationCts?.Cancel();
        _currentOperationCts?.Dispose();
        _currentOperationCts = new CancellationTokenSource();

        try
        {
            var results = await _orchestrator.StartServicesAsync(selectedServices, _currentOperationCts.Token);

            foreach (var serv in selectedServices)
            {
                if (results.TryGetValue(serv.ServiceName, out var ok) && ok)
                {
                    serv.CurrentState = ServiceState.Running;
                    AppendLog($"Started: {serv.ServiceName} ({serv.DisplayName})");
                    _logger.LogInformation("Started service {ServiceName}", serv.ServiceName);
                }
                else
                {
                    AppendLog($"Failed to start {serv.ServiceName}", LogLevel.Error);
                    _logger.LogError("Failed to start service {ServiceName}", serv.ServiceName);
                }
            }

            AppendLog($"Start operation completed for {selectedServices.Count} service(s).");
        }
        finally
        {
            BtnStart.Enabled = true;
            BtnCancel.Enabled = false;
            _currentOperationCts?.Dispose();
            _currentOperationCts = null;
        }
    }

    private async void BtnStop_Click(object? sender, EventArgs e)
    {
        var selectedServices = GetSelectedServices().ToList();

        if (selectedServices.Count == 0)
            return;

        // Only allow stopping services that are running or paused
        if (selectedServices.Any(s => s.GetStatus() != ServiceControllerStatus.Running && s.GetStatus() != ServiceControllerStatus.Paused))
        {
            MessageBox.Show(this, "Please select only services that are running or paused to stop.", "Stop services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            AppendLog("Stop aborted: selection contains services not running/paused.", LogLevel.Warning);

            return;
        }

        BtnStop.Enabled = false;
        BtnCancel.Enabled = true;
        AppendLog($"Stopping {selectedServices.Count} service(s)...");

        // ReSharper disable once MethodHasAsyncOverload
        _currentOperationCts?.Cancel();
        _currentOperationCts?.Dispose();
        _currentOperationCts = new CancellationTokenSource();

        try
        {
            var results = await _orchestrator.StopServicesAsync(selectedServices, _currentOperationCts.Token);

            foreach (var serv in selectedServices)
            {
                if (results.TryGetValue(serv.ServiceName, out var ok) && ok)
                {
                    serv.CurrentState = ServiceState.Stopped;
                    AppendLog($"Stopped: {serv.ServiceName} ({serv.DisplayName})");
                    _logger.LogInformation("Stopped service {ServiceName}", serv.ServiceName);
                }
                else
                {
                    AppendLog($"Failed to stop {serv.ServiceName}", LogLevel.Error);
                    _logger.LogError("Failed to stop service {ServiceName}", serv.ServiceName);
                }
            }

            AppendLog($"Stop operation completed for {selectedServices.Count} service(s).");
        }
        finally
        {
            BtnStop.Enabled = true;
            BtnCancel.Enabled = false;
            _currentOperationCts?.Dispose();
            _currentOperationCts = null;
        }
    }

    private async void BtnRestart_Click(object? sender, EventArgs e)
    {
        var sel = GetSelectedServices().ToList();

        if (sel.Count == 0)
            return;

        // For restart, require services to be running or paused
        if (sel.Any(s => s.GetStatus() != ServiceControllerStatus.Running && s.GetStatus() != ServiceControllerStatus.Paused))
        {
            MessageBox.Show(this, "Please select only services that are running or paused to restart.", "Restart services", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return;
        }

        BtnRestart.Enabled = false;
        BtnCancel.Enabled = true;
        AppendLog($"Restarting {sel.Count} service(s)...");

        // ReSharper disable once MethodHasAsyncOverload
        _currentOperationCts?.Cancel();
        _currentOperationCts?.Dispose();
        _currentOperationCts = new CancellationTokenSource();

        try
        {
            var results = await _orchestrator.RestartServicesAsync(sel, _currentOperationCts.Token);

            foreach (var s in sel)
            {
                if (results.TryGetValue(s.ServiceName, out var ok) && ok)
                {
                    AppendLog($"Restarted: {s.ServiceName} ({s.DisplayName})");
                    _logger.LogInformation("Restarted service {ServiceName}", s.ServiceName);
                }
                else
                {
                    AppendLog($"Failed to restart {s.ServiceName}", LogLevel.Error);
                    _logger.LogError("Failed to restart service {ServiceName}", s.ServiceName);
                }
            }

            AppendLog($"Restart operation completed for {sel.Count} service(s).");
        }
        finally
        {
            BtnRestart.Enabled = true;
            BtnCancel.Enabled = false;
            _currentOperationCts?.Dispose();
            _currentOperationCts = null;
        }
    }

    private void FormPrincipal_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (!_shouldSaveOnClose)
            return;

        try
        {
            // Save current window bounds/state to config
            var rect = (WindowState == FormWindowState.Normal) ? Bounds : RestoreBounds;

            _appConfig.WindowLeft = rect.Left;
            _appConfig.WindowTop = rect.Top;
            _appConfig.WindowWidth = rect.Width;
            _appConfig.WindowHeight = rect.Height;
            _appConfig.WindowState = WindowState.ToString();
            _appConfig.SplitterDistance = SplitMain.SplitterDistance;

            // Save column widths before closing
            SaveColumnWidths();

            _appConfig.Save();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save window bounds");
        }
    }

    private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e)
        {
            case { Control: true, KeyCode: Keys.K }:
                TxtFilter.Focus();
                e.Handled = true;
                break;

            case { KeyCode: Keys.Escape }:
                if (TxtFilter.Focused)
                    TxtFilter.Clear();
                else
                    BtnCancel_Click(sender, e);

                e.Handled = true;
                break;

            case { KeyCode: Keys.F5 }:
                BtnLoad_Click(sender, e);
                e.Handled = true;
                break;

            case { KeyCode: Keys.F9 }:
                BtnStart_Click(sender, e);
                e.Handled = true;
                break;

            case { KeyCode: Keys.F7 }:
                BtnRestart_Click(sender, e);
                e.Handled = true;
                break;

            case { KeyCode: Keys.F2 }:
                BtnStop_Click(sender, e);
                e.Handled = true;
                break;

            case { KeyCode: Keys.F6 }:
                BtnChangeStartMode_Click(sender, e);
                e.Handled = true;
                break;
        }
    }

    private async void BtnColumns_Click(object sender, EventArgs e)
        => await ShowColumnChooserDialogAsync();

    private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        => TextLog.Clear();
}
