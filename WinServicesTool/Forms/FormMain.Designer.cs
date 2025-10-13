namespace WinServicesTool.Forms
{
    sealed partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            ChkStartAsAdm = new CheckBox();
            BtnLoad = new Button();
            Imgs = new ImageList(components);
            BtnStart = new Button();
            BtnStop = new Button();
            BtnRestart = new Button();
            BtnChangeStartMode = new Button();
            BtnCancel = new Button();
            tableLayoutPanelFilter = new TableLayoutPanel();
            CbFilterStatus = new ComboBox();
            CbFilterStartMode = new ComboBox();
            TxtFilter = new TextBox();
            LblFilterStatus = new Label();
            LblStartMode = new Label();
            GridServs = new DataGridView();
            ColDisplayName = new DataGridViewTextBoxColumn();
            ColServiceName = new DataGridViewTextBoxColumn();
            ColStatus = new DataGridViewTextBoxColumn();
            ColStartMode = new DataGridViewTextBoxColumn();
            ColServiceType = new DataGridViewTextBoxColumn();
            ColCanPauseAndContinue = new DataGridViewCheckBoxColumn();
            ColCanShutdown = new DataGridViewCheckBoxColumn();
            ColCanStop = new DataGridViewCheckBoxColumn();
            ColPath = new DataGridViewTextBoxColumn();
            serviceBindingSource = new BindingSource(components);
            TextLog = new RichTextBox();
            SplitMain = new SplitContainer();
            StatusBar = new StatusStrip();
            LblStatusServices = new ToolStripStatusLabel();
            LblStatusSeparator = new ToolStripStatusLabel();
            LblStatusServicesRunning = new ToolStripStatusLabel();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GridServs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)serviceBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SplitMain).BeginInit();
            SplitMain.Panel1.SuspendLayout();
            SplitMain.Panel2.SuspendLayout();
            SplitMain.SuspendLayout();
            StatusBar.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanelFilter, 0, 1);
            tableLayoutPanel2.Controls.Add(GridServs, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1252, 561);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 8;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel3.Controls.Add(ChkStartAsAdm, 7, 0);
            tableLayoutPanel3.Controls.Add(BtnLoad, 0, 0);
            tableLayoutPanel3.Controls.Add(BtnStart, 1, 0);
            tableLayoutPanel3.Controls.Add(BtnStop, 2, 0);
            tableLayoutPanel3.Controls.Add(BtnRestart, 3, 0);
            tableLayoutPanel3.Controls.Add(BtnChangeStartMode, 4, 0);
            tableLayoutPanel3.Controls.Add(BtnCancel, 5, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1246, 44);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // ChkStartAsAdm
            // 
            ChkStartAsAdm.AutoSize = true;
            ChkStartAsAdm.Dock = DockStyle.Fill;
            ChkStartAsAdm.Location = new Point(1129, 3);
            ChkStartAsAdm.Name = "ChkStartAsAdm";
            ChkStartAsAdm.Size = new Size(114, 38);
            ChkStartAsAdm.TabIndex = 8;
            ChkStartAsAdm.Text = "Always start as administrator";
            ChkStartAsAdm.UseVisualStyleBackColor = true;
            // 
            // BtnLoad
            // 
            BtnLoad.Dock = DockStyle.Fill;
            BtnLoad.ImageKey = "refresh-cw.png";
            BtnLoad.ImageList = Imgs;
            BtnLoad.Location = new Point(3, 3);
            BtnLoad.Name = "BtnLoad";
            BtnLoad.Size = new Size(94, 38);
            BtnLoad.TabIndex = 0;
            BtnLoad.Text = "Load (F5)";
            BtnLoad.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnLoad.UseVisualStyleBackColor = true;
            BtnLoad.Click += BtnLoad_Click;
            // 
            // Imgs
            // 
            Imgs.ColorDepth = ColorDepth.Depth32Bit;
            Imgs.ImageStream = (ImageListStreamer)resources.GetObject("Imgs.ImageStream");
            Imgs.TransparentColor = Color.Transparent;
            Imgs.Images.SetKeyName(0, "refresh-cw.png");
            Imgs.Images.SetKeyName(1, "columns-3-cog.png");
            Imgs.Images.SetKeyName(2, "play.png");
            Imgs.Images.SetKeyName(3, "square.png");
            Imgs.Images.SetKeyName(4, "rotate-ccw.png");
            Imgs.Images.SetKeyName(5, "trending-up.png");
            Imgs.Images.SetKeyName(6, "ban.png");
            // 
            // BtnStart
            // 
            BtnStart.Dock = DockStyle.Fill;
            BtnStart.ImageKey = "play.png";
            BtnStart.ImageList = Imgs;
            BtnStart.Location = new Point(103, 3);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(94, 38);
            BtnStart.TabIndex = 3;
            BtnStart.Text = "Start (F9)";
            BtnStart.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // BtnStop
            // 
            BtnStop.Dock = DockStyle.Fill;
            BtnStop.ImageKey = "square.png";
            BtnStop.ImageList = Imgs;
            BtnStop.Location = new Point(203, 3);
            BtnStop.Name = "BtnStop";
            BtnStop.Size = new Size(94, 38);
            BtnStop.TabIndex = 4;
            BtnStop.Text = "Stop (F2)";
            BtnStop.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnStop.UseVisualStyleBackColor = true;
            BtnStop.Click += BtnStop_Click;
            // 
            // BtnRestart
            // 
            BtnRestart.Dock = DockStyle.Fill;
            BtnRestart.ImageKey = "rotate-ccw.png";
            BtnRestart.ImageList = Imgs;
            BtnRestart.Location = new Point(303, 3);
            BtnRestart.Name = "BtnRestart";
            BtnRestart.Size = new Size(94, 38);
            BtnRestart.TabIndex = 5;
            BtnRestart.Text = "Restart (F7)";
            BtnRestart.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnRestart.UseVisualStyleBackColor = true;
            BtnRestart.Click += BtnRestart_Click;
            // 
            // BtnChangeStartMode
            // 
            BtnChangeStartMode.Dock = DockStyle.Fill;
            BtnChangeStartMode.ImageKey = "trending-up.png";
            BtnChangeStartMode.ImageList = Imgs;
            BtnChangeStartMode.Location = new Point(403, 3);
            BtnChangeStartMode.Name = "BtnChangeStartMode";
            BtnChangeStartMode.Size = new Size(94, 38);
            BtnChangeStartMode.TabIndex = 6;
            BtnChangeStartMode.Text = "Start mode (F6)";
            BtnChangeStartMode.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnChangeStartMode.UseVisualStyleBackColor = true;
            BtnChangeStartMode.Click += BtnChangeStartMode_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Dock = DockStyle.Fill;
            BtnCancel.Font = new Font("Segoe UI", 9F);
            BtnCancel.ImageKey = "ban.png";
            BtnCancel.ImageList = Imgs;
            BtnCancel.Location = new Point(503, 3);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(94, 38);
            BtnCancel.TabIndex = 7;
            BtnCancel.Text = "Cancel";
            BtnCancel.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // tableLayoutPanelFilter
            // 
            tableLayoutPanelFilter.ColumnCount = 5;
            tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelFilter.Controls.Add(CbFilterStatus, 1, 0);
            tableLayoutPanelFilter.Controls.Add(CbFilterStartMode, 3, 0);
            tableLayoutPanelFilter.Controls.Add(TxtFilter, 4, 0);
            tableLayoutPanelFilter.Controls.Add(LblFilterStatus, 0, 0);
            tableLayoutPanelFilter.Controls.Add(LblStartMode, 2, 0);
            tableLayoutPanelFilter.Dock = DockStyle.Fill;
            tableLayoutPanelFilter.Location = new Point(3, 53);
            tableLayoutPanelFilter.Name = "tableLayoutPanelFilter";
            tableLayoutPanelFilter.RowCount = 1;
            tableLayoutPanelFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelFilter.Size = new Size(1246, 34);
            tableLayoutPanelFilter.TabIndex = 2;
            // 
            // CbFilterStatus
            // 
            CbFilterStatus.Dock = DockStyle.Fill;
            CbFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            CbFilterStatus.FlatStyle = FlatStyle.Flat;
            CbFilterStatus.Location = new Point(73, 5);
            CbFilterStatus.Margin = new Padding(3, 5, 3, 3);
            CbFilterStatus.Name = "CbFilterStatus";
            CbFilterStatus.Size = new Size(194, 23);
            CbFilterStatus.TabIndex = 0;
            // 
            // CbFilterStartMode
            // 
            CbFilterStartMode.Dock = DockStyle.Fill;
            CbFilterStartMode.DropDownStyle = ComboBoxStyle.DropDownList;
            CbFilterStartMode.FlatStyle = FlatStyle.Flat;
            CbFilterStartMode.Location = new Point(323, 5);
            CbFilterStartMode.Margin = new Padding(3, 5, 3, 3);
            CbFilterStartMode.Name = "CbFilterStartMode";
            CbFilterStartMode.Size = new Size(194, 23);
            CbFilterStartMode.TabIndex = 1;
            // 
            // TxtFilter
            // 
            TxtFilter.BorderStyle = BorderStyle.FixedSingle;
            TxtFilter.Dock = DockStyle.Fill;
            TxtFilter.Font = new Font("Segoe UI", 10F);
            TxtFilter.Location = new Point(525, 5);
            TxtFilter.Margin = new Padding(5, 5, 5, 3);
            TxtFilter.Name = "TxtFilter";
            TxtFilter.PlaceholderText = "Filter text... (ctrl + k)";
            TxtFilter.Size = new Size(716, 25);
            TxtFilter.TabIndex = 2;
            // 
            // LblFilterStatus
            // 
            LblFilterStatus.AutoSize = true;
            LblFilterStatus.Dock = DockStyle.Fill;
            LblFilterStatus.Image = (Image)resources.GetObject("LblFilterStatus.Image");
            LblFilterStatus.ImageAlign = ContentAlignment.MiddleLeft;
            LblFilterStatus.Location = new Point(0, 0);
            LblFilterStatus.Margin = new Padding(0);
            LblFilterStatus.Name = "LblFilterStatus";
            LblFilterStatus.Size = new Size(70, 34);
            LblFilterStatus.TabIndex = 3;
            LblFilterStatus.Text = "Status";
            LblFilterStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LblStartMode
            // 
            LblStartMode.AutoSize = true;
            LblStartMode.Location = new Point(273, 0);
            LblStartMode.Name = "LblStartMode";
            LblStartMode.Size = new Size(38, 30);
            LblStartMode.TabIndex = 4;
            LblStartMode.Text = "Start mode";
            LblStartMode.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridServs
            // 
            GridServs.AllowUserToAddRows = false;
            GridServs.AllowUserToDeleteRows = false;
            GridServs.AllowUserToOrderColumns = true;
            GridServs.AllowUserToResizeRows = false;
            GridServs.AutoGenerateColumns = false;
            GridServs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridServs.BorderStyle = BorderStyle.None;
            GridServs.CellBorderStyle = DataGridViewCellBorderStyle.None;
            GridServs.ColumnHeadersHeight = 48;
            GridServs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            GridServs.Columns.AddRange(new DataGridViewColumn[] { ColDisplayName, ColServiceName, ColStatus, ColStartMode, ColServiceType, ColCanPauseAndContinue, ColCanShutdown, ColCanStop, ColPath });
            GridServs.DataSource = serviceBindingSource;
            GridServs.Dock = DockStyle.Fill;
            GridServs.Location = new Point(3, 93);
            GridServs.Name = "GridServs";
            GridServs.ReadOnly = true;
            GridServs.RowHeadersVisible = false;
            GridServs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridServs.Size = new Size(1246, 465);
            GridServs.TabIndex = 0;
            // 
            // ColDisplayName
            // 
            ColDisplayName.DataPropertyName = "DisplayName";
            ColDisplayName.HeaderText = "Display Name";
            ColDisplayName.Name = "ColDisplayName";
            ColDisplayName.ReadOnly = true;
            // 
            // ColServiceName
            // 
            ColServiceName.DataPropertyName = "ServiceName";
            ColServiceName.FillWeight = 80F;
            ColServiceName.HeaderText = "Service Name";
            ColServiceName.Name = "ColServiceName";
            ColServiceName.ReadOnly = true;
            // 
            // ColStatus
            // 
            ColStatus.DataPropertyName = "Status";
            ColStatus.FillWeight = 50F;
            ColStatus.HeaderText = "Status";
            ColStatus.Name = "ColStatus";
            ColStatus.ReadOnly = true;
            // 
            // ColStartMode
            // 
            ColStartMode.DataPropertyName = "StartMode";
            ColStartMode.FillWeight = 50F;
            ColStartMode.HeaderText = "Start Mode";
            ColStartMode.Name = "ColStartMode";
            ColStartMode.ReadOnly = true;
            // 
            // ColServiceType
            // 
            ColServiceType.DataPropertyName = "ServiceType";
            ColServiceType.HeaderText = "Service Type";
            ColServiceType.Name = "ColServiceType";
            ColServiceType.ReadOnly = true;
            // 
            // ColCanPauseAndContinue
            // 
            ColCanPauseAndContinue.DataPropertyName = "CanPauseAndContinue";
            ColCanPauseAndContinue.FillWeight = 40F;
            ColCanPauseAndContinue.HeaderText = "Can Pause\n& Continue";
            ColCanPauseAndContinue.Name = "ColCanPauseAndContinue";
            ColCanPauseAndContinue.ReadOnly = true;
            // 
            // ColCanShutdown
            // 
            ColCanShutdown.DataPropertyName = "CanShutdown";
            ColCanShutdown.FillWeight = 40F;
            ColCanShutdown.HeaderText = "Can\nShutdown";
            ColCanShutdown.Name = "ColCanShutdown";
            ColCanShutdown.ReadOnly = true;
            // 
            // ColCanStop
            // 
            ColCanStop.DataPropertyName = "CanStop";
            ColCanStop.FillWeight = 40F;
            ColCanStop.HeaderText = "Can\nStop";
            ColCanStop.Name = "ColCanStop";
            ColCanStop.ReadOnly = true;
            // 
            // ColPath
            // 
            ColPath.DataPropertyName = "Path";
            ColPath.FillWeight = 150F;
            ColPath.HeaderText = "Path";
            ColPath.Name = "ColPath";
            ColPath.ReadOnly = true;
            // 
            // serviceBindingSource
            // 
            serviceBindingSource.DataSource = typeof(Models.Service);
            // 
            // TextLog
            // 
            TextLog.BackColor = Color.White;
            TextLog.BorderStyle = BorderStyle.None;
            TextLog.Dock = DockStyle.Fill;
            TextLog.Location = new Point(0, 0);
            TextLog.Margin = new Padding(10);
            TextLog.Name = "TextLog";
            TextLog.ReadOnly = true;
            TextLog.Size = new Size(1252, 104);
            TextLog.TabIndex = 1;
            TextLog.Text = "";
            // 
            // SplitMain
            // 
            SplitMain.Dock = DockStyle.Fill;
            SplitMain.Location = new Point(0, 0);
            SplitMain.Name = "SplitMain";
            SplitMain.Orientation = Orientation.Horizontal;
            // 
            // SplitMain.Panel1
            // 
            SplitMain.Panel1.Controls.Add(tableLayoutPanel2);
            // 
            // SplitMain.Panel2
            // 
            SplitMain.Panel2.Controls.Add(TextLog);
            SplitMain.Panel2.Controls.Add(StatusBar);
            SplitMain.Size = new Size(1252, 691);
            SplitMain.SplitterDistance = 561;
            SplitMain.TabIndex = 2;
            // 
            // StatusBar
            // 
            StatusBar.Items.AddRange(new ToolStripItem[] { LblStatusServices, LblStatusSeparator, LblStatusServicesRunning });
            StatusBar.Location = new Point(0, 104);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new Size(1252, 22);
            StatusBar.TabIndex = 9;
            StatusBar.Text = "statusStrip1";
            // 
            // LblStatusServices
            // 
            LblStatusServices.Image = (Image)resources.GetObject("LblStatusServices.Image");
            LblStatusServices.Name = "LblStatusServices";
            LblStatusServices.Size = new Size(120, 17);
            LblStatusServices.Text = "{0} services loaded";
            // 
            // LblStatusSeparator
            // 
            LblStatusSeparator.DisplayStyle = ToolStripItemDisplayStyle.Image;
            LblStatusSeparator.Image = (Image)resources.GetObject("LblStatusSeparator.Image");
            LblStatusSeparator.Name = "LblStatusSeparator";
            LblStatusSeparator.Size = new Size(16, 17);
            LblStatusSeparator.Text = " - ";
            // 
            // LblStatusServicesRunning
            // 
            LblStatusServicesRunning.Image = (Image)resources.GetObject("LblStatusServicesRunning.Image");
            LblStatusServicesRunning.Name = "LblStatusServicesRunning";
            LblStatusServicesRunning.Size = new Size(126, 17);
            LblStatusServicesRunning.Text = "{0} services running";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1252, 691);
            Controls.Add(SplitMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "FormMain";
            Text = "WinServicesTool";
            KeyDown += FormPrincipal_KeyDown;
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanelFilter.ResumeLayout(false);
            tableLayoutPanelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GridServs).EndInit();
            ((System.ComponentModel.ISupportInitialize)serviceBindingSource).EndInit();
            SplitMain.Panel1.ResumeLayout(false);
            SplitMain.Panel2.ResumeLayout(false);
            SplitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SplitMain).EndInit();
            SplitMain.ResumeLayout(false);
            StatusBar.ResumeLayout(false);
            StatusBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel2;
        private DataGridView GridServs;
        private BindingSource serviceBindingSource;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanelFilter;
        private ComboBox CbFilterStatus;
        private ComboBox CbFilterStartMode;
        private Button BtnStart;
        private Button BtnStop;
        private Button BtnRestart;
        private Button BtnChangeStartMode;
        private Button BtnLoad;
        private TextBox TxtFilter;
        private ImageList Imgs;
        private RichTextBox TextLog;
        private SplitContainer SplitMain;
        private Label LblFilterStatus;
        private Label LblStartMode;
        private DataGridViewTextBoxColumn ColDisplayName;
        private DataGridViewTextBoxColumn ColServiceName;
        private DataGridViewTextBoxColumn ColStatus;
        private DataGridViewTextBoxColumn ColStartMode;
        private DataGridViewTextBoxColumn ColServiceType;
        private DataGridViewCheckBoxColumn ColCanPauseAndContinue;
        private DataGridViewCheckBoxColumn ColCanShutdown;
        private DataGridViewCheckBoxColumn ColCanStop;
        private DataGridViewTextBoxColumn ColPath;
        private Button BtnCancel;
        private CheckBox ChkStartAsAdm;
        private StatusStrip StatusBar;
        private ToolStripStatusLabel LblStatusServices;
        private ToolStripStatusLabel LblStatusServicesRunning;
        private ToolStripStatusLabel LblStatusSeparator;
    }
}
