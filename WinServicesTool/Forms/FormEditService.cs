using Microsoft.Extensions.Logging;
using WinServicesTool.Services;

namespace WinServicesTool.Forms;

/// <summary>
/// Form for editing service display name and description.
/// </summary>
public sealed class FormEditService : Form
{
    private readonly ILogger<FormEditService> _logger;
    private readonly IRegistryService _registryService;
    private readonly string _serviceName;

    private TextBox _txtDisplayName = null!;
    private TextBox _txtDescription = null!;
    private Button _btnOk = null!;
    private Button _btnCancel = null!;
    private Label _lblDisplayName = null!;
    private Label _lblDescription = null!;
    private Label _lblServiceName = null!;

    /// <summary>
    /// Gets the updated display name entered by the user.
    /// </summary>
    public string DisplayName
        => _txtDisplayName.Text;

    /// <summary>
    /// Gets the updated description entered by the user.
    /// </summary>
    public string Description
        => _txtDescription.Text;

    public FormEditService
    (
        ILogger<FormEditService> logger,
        IRegistryService registryService,
        string serviceName,
        string currentDisplayName,
        string currentDescription
    )
    {
        _logger = logger;
        _registryService = registryService;
        _serviceName = serviceName;

        InitializeComponent();
        InitializeControls(serviceName, currentDisplayName, currentDescription);
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        // Form properties
        Text = "Edit Service";
        ClientSize = new Size(500, 290);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        AcceptButton = _btnOk;
        CancelButton = _btnCancel;

        // Service Name Label
        _lblServiceName = new Label
        {
            Text = "Service:",
            Location = new Point(12, 12),
            Size = new Size(476, 20),
            Font = new Font(Font, FontStyle.Bold)
        };

        // Display Name Label
        _lblDisplayName = new Label
        {
            Text = "Display Name:",
            Location = new Point(12, 42),
            AutoSize = true
        };

        // Display Name TextBox
        _txtDisplayName = new TextBox
        {
            Location = new Point(12, 65),
            Size = new Size(476, 23),
            MaxLength = 256
        };

        // Description Label
        _lblDescription = new Label
        {
            Text = "Description:",
            Location = new Point(12, 98),
            AutoSize = true
        };

        // Description TextBox
        _txtDescription = new TextBox
        {
            Location = new Point(12, 121),
            Size = new Size(476, 115),
            MaxLength = 1024,
            Multiline = true
        };

        // OK Button
        _btnOk = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Location = new Point(332, 243),
            Size = new Size(75, 38)
        };

        _btnOk.Click += BtnOk_Click;

        // Cancel Button
        _btnCancel = new Button
        {
            Text = "Cancel",
            DialogResult = DialogResult.Cancel,
            Location = new Point(413, 243),
            Size = new Size(75, 38)
        };

        // Add controls to form
        Controls.Add(_lblServiceName);
        Controls.Add(_lblDisplayName);
        Controls.Add(_txtDisplayName);
        Controls.Add(_lblDescription);
        Controls.Add(_txtDescription);
        Controls.Add(_btnOk);
        Controls.Add(_btnCancel);

        ResumeLayout(false);
        PerformLayout();
    }

    private void InitializeControls(string serviceName, string currentDisplayName, string currentDescription)
    {
        _lblServiceName.Text = $"Service: {serviceName}";
        _txtDisplayName.Text = currentDisplayName;
        _txtDescription.Text = currentDescription;
    }

    private async void BtnOk_Click(object? sender, EventArgs e)
    {
        try
        {
            _btnOk.Enabled = false;
            _btnCancel.Enabled = false;

            var displayName = _txtDisplayName.Text.Trim();
            var description = _txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(displayName))
            {
                MessageBox.Show
                (
                    "Display name cannot be empty.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                _btnOk.Enabled = true;
                _btnCancel.Enabled = true;
                DialogResult = DialogResult.None;

                return;
            }

            await Task.Run(() => _registryService.UpdateServiceInfo(_serviceName, displayName, description));

            MessageBox.Show
            (
                "Service information updated successfully.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update service information for {ServiceName}", _serviceName);

            MessageBox.Show
            (
                $"Failed to update service information: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            DialogResult = DialogResult.None;
        }
        finally
        {
            _btnOk.Enabled = true;
            _btnCancel.Enabled = true;
        }
    }
}
