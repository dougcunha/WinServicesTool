namespace WinServicesTool.Forms;

/// <summary>
/// Dialog for selecting visible columns in the services grid.
/// </summary>
public sealed class FormColumnChooser : Form
{
    private CheckedListBox? _lstColumns;

    /// <summary>
    /// Gets the list of selected column names after dialog is closed with OK.
    /// </summary>
    public List<string> ChosenColumnNames { get; private set; } = [];

    /// <summary>
    /// Creates a new column chooser dialog.
    /// </summary>
    /// <param name="allColumns">All available column names.</param>
    /// <param name="visibleColumns">Currently visible column names.</param>
    public FormColumnChooser(List<DataGridViewColumn> allColumns, List<string> visibleColumns)
        => InitializeComponent(allColumns, visibleColumns);

    private void InitializeComponent(List<DataGridViewColumn> allColumns, List<string> visibleColumnsName)
    {
        Text = "Choose Visible Columns";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(380, 400);

        var columns = allColumns.ConvertAll(col => new { col.Name, Caption = col.HeaderText.Replace('\n', ' ') });

        var lbl = new Label
        {
            Left = 12,
            Top = 12,
            Width = 356,
            Height = 20,
            Text = "Select the columns you want to display:"
        };

        _lstColumns = new CheckedListBox
        {
            Left = 12,
            Top = 36,
            Width = 356,
            Height = 300,
            CheckOnClick = true
        };

        // Add all columns to the list
        foreach (var col in columns)
        {
            var isChecked = visibleColumnsName.Count is 0 || visibleColumnsName.Contains(col.Name);
            _lstColumns.Items.Add(col.Caption, isChecked);
        }

        var btnSelectAll = new Button
        {
            Text = "Select All",
            Left = 12,
            Width = 90,
            Height = 32,
            Top = 344
        };

        var btnDeselectAll = new Button
        {
            Text = "Deselect All",
            Left = 110,
            Width = 90,
            Height = 32,
            Top = 344
        };

        var btnOk = new Button
        {
            Text = "OK",
            Left = 206,
            Width = 80,
            Height = 32,
            Top = 344,
            DialogResult = DialogResult.OK
        };

        var btnCancel = new Button
        {
            Text = "Cancel",
            Left = 292,
            Width = 80,
            Height = 32,
            Top = 344,
            DialogResult = DialogResult.Cancel
        };

        btnSelectAll.Click += (_, _) =>
        {
            if (_lstColumns == null)
                return;

            for (var i = 0; i < _lstColumns.Items.Count; i++)
                _lstColumns.SetItemChecked(i, true);
        };

        btnDeselectAll.Click += (_, _) =>
        {
            if (_lstColumns == null)
                return;

            for (var i = 0; i < _lstColumns.Items.Count; i++)
                _lstColumns.SetItemChecked(i, false);
        };

        btnOk.Click += (_, _) =>
        {
            if (_lstColumns == null)
                return;

            // Map checked captions back to column names
            ChosenColumnNames = _lstColumns.CheckedIndices.Cast<int>()
                .Select(i => columns[i].Name)
                .ToList();

            DialogResult = DialogResult.OK;
            Close();
        };

        btnCancel.Click += (_, _) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        Controls.Add(lbl);
        Controls.Add(_lstColumns);
        Controls.Add(btnSelectAll);
        Controls.Add(btnDeselectAll);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
    }
}
