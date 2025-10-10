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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            TabCtrl = new TabControl();
            TabMain = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            BtnLoad = new Button();
            Imgs = new ImageList(components);
            BtnStart = new Button();
            BtnStop = new Button();
            BtnRestart = new Button();
            BtnChangeStartMode = new Button();
            tableLayoutPanelFilter = new TableLayoutPanel();
            CbFilterStatus = new ComboBox();
            CbFilterStartMode = new ComboBox();
            TxtFilter = new TextBox();
            LblFilterStatus = new Label();
            LblStartMode = new Label();
            GridServs = new DataGridView();
            serviceBindingSource = new BindingSource(components);
            TabSettings = new TabPage();
            PnlSettings = new TableLayoutPanel();
            GrpStarting = new GroupBox();
            ChkStartAsAdm = new CheckBox();
            GrpSettingsGrid = new GroupBox();
            ChkAutoWidth = new CheckBox();
            TextLog = new RichTextBox();
            SplitMain = new SplitContainer();
            ColDisplayName = new DataGridViewTextBoxColumn();
            ColServiceName = new DataGridViewTextBoxColumn();
            ColStatus = new DataGridViewTextBoxColumn();
            ColStartMode = new DataGridViewTextBoxColumn();
            ColServiceType = new DataGridViewTextBoxColumn();
            ColCanPauseAndContinue = new DataGridViewCheckBoxColumn();
            ColCanShutdown = new DataGridViewCheckBoxColumn();
            ColCanStop = new DataGridViewCheckBoxColumn();
            Path = new DataGridViewTextBoxColumn();
            TabCtrl.SuspendLayout();
            TabMain.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GridServs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)serviceBindingSource).BeginInit();
            TabSettings.SuspendLayout();
            PnlSettings.SuspendLayout();
            GrpStarting.SuspendLayout();
            GrpSettingsGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitMain).BeginInit();
            SplitMain.Panel1.SuspendLayout();
            SplitMain.Panel2.SuspendLayout();
            SplitMain.SuspendLayout();
            SuspendLayout();
            // 
            // TabCtrl
            // 
            TabCtrl.Controls.Add(TabMain);
            TabCtrl.Controls.Add(TabSettings);
            TabCtrl.Dock = DockStyle.Fill;
            TabCtrl.Location = new Point(0, 0);
            TabCtrl.Name = "TabCtrl";
            TabCtrl.SelectedIndex = 0;
            TabCtrl.Size = new Size(1088, 561);
            TabCtrl.TabIndex = 0;
            // 
            // TabMain
            // 
            TabMain.Controls.Add(tableLayoutPanel2);
            TabMain.Location = new Point(4, 24);
            TabMain.Name = "TabMain";
            TabMain.Padding = new Padding(3);
            TabMain.Size = new Size(1080, 533);
            TabMain.TabIndex = 0;
            TabMain.Text = "Services";
            TabMain.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanelFilter, 0, 1);
            tableLayoutPanel2.Controls.Add(GridServs, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1074, 527);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 6;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(BtnLoad, 0, 0);
            tableLayoutPanel3.Controls.Add(BtnStart, 1, 0);
            tableLayoutPanel3.Controls.Add(BtnStop, 2, 0);
            tableLayoutPanel3.Controls.Add(BtnRestart, 3, 0);
            tableLayoutPanel3.Controls.Add(BtnChangeStartMode, 4, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1068, 44);
            tableLayoutPanel3.TabIndex = 1;
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
            BtnChangeStartMode.Text = "Start mode";
            BtnChangeStartMode.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnChangeStartMode.UseVisualStyleBackColor = true;
            BtnChangeStartMode.Click += BtnChangeStartMode_Click;
            // 
            // tableLayoutPanelFilter
            // 
            tableLayoutPanelFilter.ColumnCount = 5;
            tableLayoutPanelFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
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
            tableLayoutPanelFilter.Size = new Size(1068, 34);
            tableLayoutPanelFilter.TabIndex = 2;
            // 
            // CbFilterStatus
            // 
            CbFilterStatus.Dock = DockStyle.Fill;
            CbFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            CbFilterStatus.FlatStyle = FlatStyle.Flat;
            CbFilterStatus.Location = new Point(53, 5);
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
            CbFilterStartMode.Location = new Point(303, 5);
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
            TxtFilter.Location = new Point(505, 5);
            TxtFilter.Margin = new Padding(5, 5, 5, 3);
            TxtFilter.Name = "TxtFilter";
            TxtFilter.PlaceholderText = "Filter text... (ctrl + k)";
            TxtFilter.Size = new Size(558, 25);
            TxtFilter.TabIndex = 2;
            // 
            // LblFilterStatus
            // 
            LblFilterStatus.AutoSize = true;
            LblFilterStatus.Dock = DockStyle.Fill;
            LblFilterStatus.Location = new Point(0, 0);
            LblFilterStatus.Margin = new Padding(0);
            LblFilterStatus.Name = "LblFilterStatus";
            LblFilterStatus.Size = new Size(50, 34);
            LblFilterStatus.TabIndex = 3;
            LblFilterStatus.Text = "Status";
            LblFilterStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LblStartMode
            // 
            LblStartMode.AutoSize = true;
            LblStartMode.Location = new Point(253, 0);
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
            GridServs.ColumnHeadersHeight = 25;
            GridServs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            GridServs.Columns.AddRange(new DataGridViewColumn[] { ColDisplayName, ColServiceName, ColStatus, ColStartMode, ColServiceType, ColCanPauseAndContinue, ColCanShutdown, ColCanStop, Path });
            GridServs.DataSource = serviceBindingSource;
            GridServs.Dock = DockStyle.Fill;
            GridServs.Location = new Point(3, 93);
            GridServs.Name = "GridServs";
            GridServs.ReadOnly = true;
            GridServs.RowHeadersVisible = false;
            GridServs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridServs.Size = new Size(1068, 431);
            GridServs.TabIndex = 0;
            // 
            // serviceBindingSource
            // 
            serviceBindingSource.DataSource = typeof(Models.Service);
            // 
            // TabSettings
            // 
            TabSettings.Controls.Add(PnlSettings);
            TabSettings.Location = new Point(4, 24);
            TabSettings.Name = "TabSettings";
            TabSettings.Padding = new Padding(3);
            TabSettings.Size = new Size(1080, 533);
            TabSettings.TabIndex = 1;
            TabSettings.Text = "Settings";
            TabSettings.UseVisualStyleBackColor = true;
            // 
            // PnlSettings
            // 
            PnlSettings.ColumnCount = 3;
            PnlSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            PnlSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            PnlSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            PnlSettings.Controls.Add(GrpStarting, 1, 0);
            PnlSettings.Controls.Add(GrpSettingsGrid, 0, 0);
            PnlSettings.Dock = DockStyle.Fill;
            PnlSettings.Location = new Point(3, 3);
            PnlSettings.Name = "PnlSettings";
            PnlSettings.RowCount = 3;
            PnlSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 182F));
            PnlSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            PnlSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            PnlSettings.Size = new Size(1074, 527);
            PnlSettings.TabIndex = 0;
            // 
            // GrpStarting
            // 
            GrpStarting.Controls.Add(ChkStartAsAdm);
            GrpStarting.FlatStyle = FlatStyle.Flat;
            GrpStarting.Location = new Point(203, 3);
            GrpStarting.Name = "GrpStarting";
            GrpStarting.Padding = new Padding(5);
            GrpStarting.Size = new Size(194, 176);
            GrpStarting.TabIndex = 1;
            GrpStarting.TabStop = false;
            GrpStarting.Text = "Starting";
            // 
            // ChkStartAsAdm
            // 
            ChkStartAsAdm.AutoSize = true;
            ChkStartAsAdm.Dock = DockStyle.Top;
            ChkStartAsAdm.Location = new Point(5, 21);
            ChkStartAsAdm.Name = "ChkStartAsAdm";
            ChkStartAsAdm.Size = new Size(184, 19);
            ChkStartAsAdm.TabIndex = 0;
            ChkStartAsAdm.Text = "Always start as administrator";
            ChkStartAsAdm.UseVisualStyleBackColor = true;
            // 
            // GrpSettingsGrid
            // 
            GrpSettingsGrid.Controls.Add(ChkAutoWidth);
            GrpSettingsGrid.FlatStyle = FlatStyle.Flat;
            GrpSettingsGrid.Location = new Point(3, 3);
            GrpSettingsGrid.Name = "GrpSettingsGrid";
            GrpSettingsGrid.Padding = new Padding(5);
            GrpSettingsGrid.Size = new Size(194, 176);
            GrpSettingsGrid.TabIndex = 0;
            GrpSettingsGrid.TabStop = false;
            GrpSettingsGrid.Text = "Grid";
            // 
            // ChkAutoWidth
            // 
            ChkAutoWidth.AutoSize = true;
            ChkAutoWidth.Dock = DockStyle.Top;
            ChkAutoWidth.Location = new Point(5, 21);
            ChkAutoWidth.Name = "ChkAutoWidth";
            ChkAutoWidth.Size = new Size(184, 19);
            ChkAutoWidth.TabIndex = 0;
            ChkAutoWidth.Text = "Columns auto width ";
            ChkAutoWidth.UseVisualStyleBackColor = true;
            ChkAutoWidth.CheckedChanged += ChkAutoWidth_CheckedChanged;
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
            TextLog.Size = new Size(1088, 126);
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
            SplitMain.Panel1.Controls.Add(TabCtrl);
            // 
            // SplitMain.Panel2
            // 
            SplitMain.Panel2.Controls.Add(TextLog);
            SplitMain.Size = new Size(1088, 691);
            SplitMain.SplitterDistance = 561;
            SplitMain.TabIndex = 2;
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
            ColServiceName.HeaderText = "Service Name";
            ColServiceName.Name = "ColServiceName";
            ColServiceName.ReadOnly = true;
            // 
            // ColStatus
            // 
            ColStatus.DataPropertyName = "Status";
            ColStatus.HeaderText = "Status";
            ColStatus.Name = "ColStatus";
            ColStatus.ReadOnly = true;
            // 
            // ColStartMode
            // 
            ColStartMode.DataPropertyName = "StartMode";
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
            ColCanPauseAndContinue.HeaderText = "Can Pause & Continue";
            ColCanPauseAndContinue.Name = "ColCanPauseAndContinue";
            ColCanPauseAndContinue.ReadOnly = true;
            // 
            // ColCanShutdown
            // 
            ColCanShutdown.DataPropertyName = "CanShutdown";
            ColCanShutdown.HeaderText = "Can Shutdown";
            ColCanShutdown.Name = "ColCanShutdown";
            ColCanShutdown.ReadOnly = true;
            // 
            // ColCanStop
            // 
            ColCanStop.DataPropertyName = "CanStop";
            ColCanStop.HeaderText = "Can Stop";
            ColCanStop.Name = "ColCanStop";
            ColCanStop.ReadOnly = true;
            // 
            // Path
            // 
            Path.DataPropertyName = "Path";
            Path.HeaderText = "Path";
            Path.Name = "Path";
            Path.ReadOnly = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1088, 691);
            Controls.Add(SplitMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "FormMain";
            Text = "WinServicesTool";
            KeyDown += FormPrincipal_KeyDown;
            TabCtrl.ResumeLayout(false);
            TabMain.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanelFilter.ResumeLayout(false);
            tableLayoutPanelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GridServs).EndInit();
            ((System.ComponentModel.ISupportInitialize)serviceBindingSource).EndInit();
            TabSettings.ResumeLayout(false);
            PnlSettings.ResumeLayout(false);
            GrpStarting.ResumeLayout(false);
            GrpStarting.PerformLayout();
            GrpSettingsGrid.ResumeLayout(false);
            GrpSettingsGrid.PerformLayout();
            SplitMain.Panel1.ResumeLayout(false);
            SplitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitMain).EndInit();
            SplitMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabControl TabCtrl;
        private TabPage TabMain;
        private TabPage TabSettings;
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
        private TableLayoutPanel PnlSettings;
        private GroupBox GrpSettingsGrid;
        private CheckBox ChkAutoWidth;
        private GroupBox GrpStarting;
        private CheckBox ChkStartAsAdm;
        private DataGridViewTextBoxColumn ColDisplayName;
        private DataGridViewTextBoxColumn ColServiceName;
        private DataGridViewTextBoxColumn ColStatus;
        private DataGridViewTextBoxColumn ColStartMode;
        private DataGridViewTextBoxColumn ColServiceType;
        private DataGridViewCheckBoxColumn ColCanPauseAndContinue;
        private DataGridViewCheckBoxColumn ColCanShutdown;
        private DataGridViewCheckBoxColumn ColCanStop;
        private DataGridViewTextBoxColumn Path;
    }
}
