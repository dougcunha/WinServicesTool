namespace WinServicesTool.Forms
{
    partial class FormMain
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
            BtnBestFitColumns = new Button();
            Imgs = new ImageList(components);
            BtnLoad = new Button();
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
            ColDisplayName = new DataGridViewTextBoxColumn();
            serviceNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            statusDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            startModeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            serviceTypeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            canPauseAndContinueDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            canShutdownDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            canStopDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            serviceBindingSource = new BindingSource(components);
            TabSettings = new TabPage();
            TextLog = new RichTextBox();
            SplitMain = new SplitContainer();
            TabCtrl.SuspendLayout();
            TabMain.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GridServs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)serviceBindingSource).BeginInit();
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
            tableLayoutPanel3.ColumnCount = 7;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(BtnBestFitColumns, 1, 0);
            tableLayoutPanel3.Controls.Add(BtnLoad, 0, 0);
            tableLayoutPanel3.Controls.Add(BtnStart, 2, 0);
            tableLayoutPanel3.Controls.Add(BtnStop, 3, 0);
            tableLayoutPanel3.Controls.Add(BtnRestart, 4, 0);
            tableLayoutPanel3.Controls.Add(BtnChangeStartMode, 5, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1068, 44);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // BtnBestFitColumns
            // 
            BtnBestFitColumns.Dock = DockStyle.Fill;
            BtnBestFitColumns.ImageKey = "columns-3-cog.png";
            BtnBestFitColumns.ImageList = Imgs;
            BtnBestFitColumns.Location = new Point(103, 3);
            BtnBestFitColumns.Name = "BtnBestFitColumns";
            BtnBestFitColumns.Size = new Size(94, 38);
            BtnBestFitColumns.TabIndex = 1;
            BtnBestFitColumns.Text = "Best fit (F3)";
            BtnBestFitColumns.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnBestFitColumns.UseVisualStyleBackColor = true;
            BtnBestFitColumns.Click += BtnBestFitColumns_Click;
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
            // BtnStart
            // 
            BtnStart.Dock = DockStyle.Fill;
            BtnStart.ImageKey = "play.png";
            BtnStart.ImageList = Imgs;
            BtnStart.Location = new Point(203, 3);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(74, 38);
            BtnStart.TabIndex = 3;
            BtnStart.Text = "Start";
            BtnStart.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // BtnStop
            // 
            BtnStop.Dock = DockStyle.Fill;
            BtnStop.ImageKey = "square.png";
            BtnStop.ImageList = Imgs;
            BtnStop.Location = new Point(283, 3);
            BtnStop.Name = "BtnStop";
            BtnStop.Size = new Size(74, 38);
            BtnStop.TabIndex = 4;
            BtnStop.Text = "Stop";
            BtnStop.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnStop.UseVisualStyleBackColor = true;
            BtnStop.Click += BtnStop_Click;
            // 
            // BtnRestart
            // 
            BtnRestart.Dock = DockStyle.Fill;
            BtnRestart.ImageKey = "rotate-ccw.png";
            BtnRestart.ImageList = Imgs;
            BtnRestart.Location = new Point(363, 3);
            BtnRestart.Name = "BtnRestart";
            BtnRestart.Size = new Size(74, 38);
            BtnRestart.TabIndex = 5;
            BtnRestart.Text = "Restart";
            BtnRestart.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnRestart.UseVisualStyleBackColor = true;
            BtnRestart.Click += BtnRestart_Click;
            // 
            // BtnChangeStartMode
            // 
            BtnChangeStartMode.Dock = DockStyle.Fill;
            BtnChangeStartMode.ImageKey = "trending-up.png";
            BtnChangeStartMode.ImageList = Imgs;
            BtnChangeStartMode.Location = new Point(443, 3);
            BtnChangeStartMode.Name = "BtnChangeStartMode";
            BtnChangeStartMode.Size = new Size(74, 38);
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
            GridServs.Columns.AddRange(new DataGridViewColumn[] { ColDisplayName, serviceNameDataGridViewTextBoxColumn, statusDataGridViewTextBoxColumn, startModeDataGridViewTextBoxColumn, serviceTypeDataGridViewTextBoxColumn, canPauseAndContinueDataGridViewCheckBoxColumn, canShutdownDataGridViewCheckBoxColumn, canStopDataGridViewCheckBoxColumn });
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
            // ColDisplayName
            // 
            ColDisplayName.DataPropertyName = "DisplayName";
            ColDisplayName.HeaderText = "DisplayName";
            ColDisplayName.Name = "ColDisplayName";
            ColDisplayName.ReadOnly = true;
            // 
            // serviceNameDataGridViewTextBoxColumn
            // 
            serviceNameDataGridViewTextBoxColumn.DataPropertyName = "ServiceName";
            serviceNameDataGridViewTextBoxColumn.HeaderText = "ServiceName";
            serviceNameDataGridViewTextBoxColumn.Name = "serviceNameDataGridViewTextBoxColumn";
            serviceNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            statusDataGridViewTextBoxColumn.HeaderText = "Status";
            statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // startModeDataGridViewTextBoxColumn
            // 
            startModeDataGridViewTextBoxColumn.DataPropertyName = "StartMode";
            startModeDataGridViewTextBoxColumn.HeaderText = "StartMode";
            startModeDataGridViewTextBoxColumn.Name = "startModeDataGridViewTextBoxColumn";
            startModeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serviceTypeDataGridViewTextBoxColumn
            // 
            serviceTypeDataGridViewTextBoxColumn.DataPropertyName = "ServiceType";
            serviceTypeDataGridViewTextBoxColumn.HeaderText = "ServiceType";
            serviceTypeDataGridViewTextBoxColumn.Name = "serviceTypeDataGridViewTextBoxColumn";
            serviceTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // canPauseAndContinueDataGridViewCheckBoxColumn
            // 
            canPauseAndContinueDataGridViewCheckBoxColumn.DataPropertyName = "CanPauseAndContinue";
            canPauseAndContinueDataGridViewCheckBoxColumn.HeaderText = "CanPauseAndContinue";
            canPauseAndContinueDataGridViewCheckBoxColumn.Name = "canPauseAndContinueDataGridViewCheckBoxColumn";
            canPauseAndContinueDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // canShutdownDataGridViewCheckBoxColumn
            // 
            canShutdownDataGridViewCheckBoxColumn.DataPropertyName = "CanShutdown";
            canShutdownDataGridViewCheckBoxColumn.HeaderText = "CanShutdown";
            canShutdownDataGridViewCheckBoxColumn.Name = "canShutdownDataGridViewCheckBoxColumn";
            canShutdownDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // canStopDataGridViewCheckBoxColumn
            // 
            canStopDataGridViewCheckBoxColumn.DataPropertyName = "CanStop";
            canStopDataGridViewCheckBoxColumn.HeaderText = "CanStop";
            canStopDataGridViewCheckBoxColumn.Name = "canStopDataGridViewCheckBoxColumn";
            canStopDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // serviceBindingSource
            // 
            serviceBindingSource.DataSource = typeof(Models.Service);
            // 
            // TabSettings
            // 
            TabSettings.Location = new Point(4, 24);
            TabSettings.Name = "TabSettings";
            TabSettings.Padding = new Padding(3);
            TabSettings.Size = new Size(1080, 533);
            TabSettings.TabIndex = 1;
            TabSettings.Text = "Settings";
            TabSettings.UseVisualStyleBackColor = true;
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
        private Button BtnBestFitColumns;
        private TextBox TxtFilter;
        private DataGridViewTextBoxColumn ColDisplayName;
        private DataGridViewTextBoxColumn serviceNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn startModeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn serviceTypeDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn canPauseAndContinueDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn canShutdownDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn canStopDataGridViewCheckBoxColumn;
        private ImageList Imgs;
    private RichTextBox TextLog;
        private SplitContainer SplitMain;
        private Label LblFilterStatus;
        private Label LblStartMode;
    }
}
