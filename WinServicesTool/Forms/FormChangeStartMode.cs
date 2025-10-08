namespace WinServicesTool.Forms;

public sealed class FormChangeStartMode : Form
{
    public string SelectedMode { get; private set; } = "Manual";

    public FormChangeStartMode()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "Change Start Type";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(320, 120);

        var lbl = new Label { Left = 12, Top = 12, Width = 296, Text = "Select new start type:" };
            var cmb = new ComboBox { Left = 12, Top = 36, Width = 296, DropDownStyle = ComboBoxStyle.DropDownList };
            cmb.Items.AddRange(["Automatic", "Manual", "Disabled"]);
            cmb.SelectedIndex = 1; // Manual

            var btnOk = new Button { Text = "OK", Left = 148, Width = 80, Top = 76, DialogResult = DialogResult.OK };
        var btnCancel = new Button { Text = "Cancel", Left = 236, Width = 80, Top = 76, DialogResult = DialogResult.Cancel };

        btnOk.Click += (_, _) =>
        {
            SelectedMode = cmb.SelectedItem?.ToString() ?? "Manual";
            DialogResult = DialogResult.OK;
            Close();
        };

        btnCancel.Click += (_, _) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        Controls.Add(lbl);
        Controls.Add(cmb);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
    }
}