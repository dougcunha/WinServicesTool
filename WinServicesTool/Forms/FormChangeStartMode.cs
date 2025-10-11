namespace WinServicesTool.Forms;

public sealed class FormChangeStartMode : Form
{
    private ComboBox? _cmb;

    public string SelectedMode { get; private set; } = "Manual";

    public FormChangeStartMode(string initialMode = "Manual")
    {
        InitializeComponent(initialMode);
    }

    private void InitializeComponent(string initialMode)
    {
        Text = "Change Start Type";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(320, 120);

        var lbl = new Label { Left = 12, Top = 12, Width = 296, Text = "Select new start type:" };
        _cmb = new ComboBox { Left = 12, Top = 36, Width = 296, DropDownStyle = ComboBoxStyle.DropDownList };
        _cmb.Items.AddRange(["Automatic", "Manual", "Disabled"]);

        // Try to select the initial mode if present
        var idx = _cmb.Items.IndexOf(initialMode);
        if (idx >= 0)
            _cmb.SelectedIndex = idx;
        else
            _cmb.SelectedIndex = 1; // default Manual

        var btnOk = new Button { Text = "OK", Left = 148, Width = 80, Top = 76, DialogResult = DialogResult.OK };
        var btnCancel = new Button { Text = "Cancel", Left = 236, Width = 80, Top = 76, DialogResult = DialogResult.Cancel };

        btnOk.Click += (s, e) =>
        {
            SelectedMode = _cmb?.SelectedItem?.ToString() ?? "Manual";
            DialogResult = DialogResult.OK;
            Close();
        };

        btnCancel.Click += (s, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        Controls.Add(lbl);
        Controls.Add(_cmb);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
    }
}
