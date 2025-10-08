namespace WinServicesTool.Forms
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            tableLayoutPanel1 = new TableLayoutPanel();
            TabCtrl = new TabControl();
            TabMain = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            GridServs = new DataGridView();
            displayNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            serviceNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            statusDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            startModeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            serviceTypeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            canPauseAndContinueDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            canShutdownDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            canStopDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            serviceBindingSource = new BindingSource(components);
            tableLayoutPanel3 = new TableLayoutPanel();
            BtnBestFitColumns = new Button();
            Imgs = new ImageList(components);
            BtnLoad = new Button();
            BtnStart = new Button();
            BtnStop = new Button();
            BtnRestart = new Button();
            TxtFilter = new TextBox();
            tabPage2 = new TabPage();
            TextLog = new RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            TabCtrl.SuspendLayout();
            TabMain.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GridServs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)serviceBindingSource).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(TabCtrl, 0, 0);
            tableLayoutPanel1.Controls.Add(TextLog, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.Size = new Size(1088, 691);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // TabCtrl
            // 
            TabCtrl.Controls.Add(TabMain);
            TabCtrl.Controls.Add(tabPage2);
            TabCtrl.Dock = DockStyle.Fill;
            TabCtrl.Location = new Point(3, 3);
            TabCtrl.Name = "TabCtrl";
            TabCtrl.SelectedIndex = 0;
            TabCtrl.Size = new Size(1082, 565);
            TabCtrl.TabIndex = 0;
            // 
            // TabMain
            // 
            TabMain.Controls.Add(tableLayoutPanel2);
            TabMain.Location = new Point(4, 24);
            TabMain.Name = "TabMain";
            TabMain.Padding = new Padding(3);
            TabMain.Size = new Size(1074, 537);
            TabMain.TabIndex = 0;
            TabMain.Text = "Services";
            TabMain.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(GridServs, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1068, 531);
            tableLayoutPanel2.TabIndex = 0;
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
            GridServs.Columns.AddRange(new DataGridViewColumn[] { displayNameDataGridViewTextBoxColumn, serviceNameDataGridViewTextBoxColumn, statusDataGridViewTextBoxColumn, startModeDataGridViewTextBoxColumn, serviceTypeDataGridViewTextBoxColumn, canPauseAndContinueDataGridViewCheckBoxColumn, canShutdownDataGridViewCheckBoxColumn, canStopDataGridViewCheckBoxColumn });
            GridServs.DataSource = serviceBindingSource;
            GridServs.Dock = DockStyle.Fill;
            GridServs.Location = new Point(3, 53);
            GridServs.Name = "GridServs";
            GridServs.ReadOnly = true;
            GridServs.RowHeadersVisible = false;
            GridServs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridServs.Size = new Size(1062, 475);
            GridServs.TabIndex = 0;
            // 
            // displayNameDataGridViewTextBoxColumn
            // 
            displayNameDataGridViewTextBoxColumn.DataPropertyName = "DisplayName";
            displayNameDataGridViewTextBoxColumn.HeaderText = "DisplayName";
            displayNameDataGridViewTextBoxColumn.Name = "displayNameDataGridViewTextBoxColumn";
            displayNameDataGridViewTextBoxColumn.ReadOnly = true;
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
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 6;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(BtnBestFitColumns, 1, 0);
            tableLayoutPanel3.Controls.Add(BtnLoad, 0, 0);
            tableLayoutPanel3.Controls.Add(BtnStart, 2, 0);
            tableLayoutPanel3.Controls.Add(BtnStop, 3, 0);
            tableLayoutPanel3.Controls.Add(BtnRestart, 4, 0);
            tableLayoutPanel3.Controls.Add(TxtFilter, 5, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1062, 44);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // BtnBestFitColumns
            // 
            BtnBestFitColumns.Dock = DockStyle.Fill;
            BtnBestFitColumns.FlatStyle = FlatStyle.Flat;
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
            // 
            // BtnLoad
            // 
            BtnLoad.Dock = DockStyle.Fill;
            BtnLoad.FlatStyle = FlatStyle.Flat;
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
            BtnStart.FlatStyle = FlatStyle.Flat;
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
            BtnStop.FlatStyle = FlatStyle.Flat;
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
            BtnRestart.FlatStyle = FlatStyle.Flat;
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
            // TxtFilter
            // 
            TxtFilter.BorderStyle = BorderStyle.None;
            TxtFilter.Dock = DockStyle.Fill;
            TxtFilter.Font = new Font("Segoe UI", 10F);
            TxtFilter.Location = new Point(454, 14);
            TxtFilter.Margin = new Padding(14);
            TxtFilter.Name = "TxtFilter";
            TxtFilter.PlaceholderText = "Filter text... (ctrl + k)";
            TxtFilter.Size = new Size(594, 18);
            TxtFilter.TabIndex = 2;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1074, 537);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Settings";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // TextLog
            // 
            TextLog.BorderStyle = BorderStyle.None;
            TextLog.Dock = DockStyle.Fill;
            TextLog.Location = new Point(10, 581);
            TextLog.Margin = new Padding(10);
            TextLog.Name = "TextLog";
            TextLog.ReadOnly = true;
            TextLog.Size = new Size(1068, 100);
            TextLog.TabIndex = 1;
            TextLog.Text = "";
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1088, 691);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "FormPrincipal";
            Text = "WinServicesTool";
            KeyDown += FormPrincipal_KeyDown;
            tableLayoutPanel1.ResumeLayout(false);
            TabCtrl.ResumeLayout(false);
            TabMain.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GridServs).EndInit();
            ((System.ComponentModel.ISupportInitialize)serviceBindingSource).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TabControl TabCtrl;
        private TabPage TabMain;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel2;
        private DataGridView GridServs;
        private BindingSource serviceBindingSource;
        private TableLayoutPanel tableLayoutPanel3;
    private Button BtnStart;
    private Button BtnStop;
    private Button BtnRestart;
        private Button BtnLoad;
        private Button BtnBestFitColumns;
        private TextBox TxtFilter;
        private DataGridViewTextBoxColumn displayNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn serviceNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn startModeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn serviceTypeDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn canPauseAndContinueDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn canShutdownDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn canStopDataGridViewCheckBoxColumn;
        private ImageList Imgs;
    private RichTextBox TextLog;
    }
}
