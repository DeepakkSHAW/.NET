namespace DoubleM
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabPage1 = new System.Windows.Forms.ToolTip(this.components);
            this.dtPKStart = new System.Windows.Forms.DateTimePicker();
            this.lstBStock = new System.Windows.Forms.ListBox();
            this.cntMnuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnImport = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbtnImportSingle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnImportMultiple = new System.Windows.Forms.ToolStripMenuItem();
            this.txtProxySrvName = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lstBStocks = new System.Windows.Forms.ListBox();
            this.dtpSince = new System.Windows.Forms.DateTimePicker();
            this.chkAllStocks = new System.Windows.Forms.CheckBox();
            this.rdobPeriod = new System.Windows.Forms.RadioButton();
            this.rdobDateStarts = new System.Windows.Forms.RadioButton();
            this.rdobDateIn = new System.Windows.Forms.RadioButton();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.chkFromTo = new System.Windows.Forms.CheckBox();
            this.SStripMain = new System.Windows.Forms.StatusStrip();
            this.SStripMainlblDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDMMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pBarDM = new System.Windows.Forms.ToolStripProgressBar();
            this.SStripMainlblAbout = new System.Windows.Forms.ToolStripStatusLabel();
            this.imglstMain = new System.Windows.Forms.ImageList(this.components);
            this.scrtxtStock = new ScrollingTextControl.ScrollingText();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsbtnExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDownload = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbtnDownloadBSE = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnDownloadNSE = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnDownloadY = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sptContMain = new System.Windows.Forms.SplitContainer();
            this._groupPaneBar = new BarTender.GroupPaneBar();
            this.tbMain = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ddlPeriod = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkAfterBefore = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlProxy = new System.Windows.Forms.Panel();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.lblPWD = new System.Windows.Forms.Label();
            this.txtUID = new System.Windows.Forms.TextBox();
            this.lblUID = new System.Windows.Forms.Label();
            this.lblProxySrvName = new System.Windows.Forms.Label();
            this.chkProxy = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.prtDlgMain = new System.Windows.Forms.PrintDialog();
            this.prtDocMain = new System.Drawing.Printing.PrintDocument();
            this.saveFileDlgExport = new System.Windows.Forms.SaveFileDialog();
            this.cntMnuMain.SuspendLayout();
            this.SStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.sptContMain.Panel1.SuspendLayout();
            this.sptContMain.Panel2.SuspendLayout();
            this.sptContMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._groupPaneBar)).BeginInit();
            this.tbMain.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.pnlProxy.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtPKStart
            // 
            this.dtPKStart.CustomFormat = "ddd dd, MMM, yyyy";
            this.dtPKStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPKStart.Location = new System.Drawing.Point(26, 148);
            this.dtPKStart.Name = "dtPKStart";
            this.dtPKStart.Size = new System.Drawing.Size(118, 20);
            this.dtPKStart.TabIndex = 1;
            this.tabPage1.SetToolTip(this.dtPKStart, "Trading after date");
            this.dtPKStart.Value = new System.DateTime(2007, 3, 26, 0, 0, 0, 0);
            // 
            // lstBStock
            // 
            this.lstBStock.ContextMenuStrip = this.cntMnuMain;
            this.lstBStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lstBStock.FormattingEnabled = true;
            this.lstBStock.Location = new System.Drawing.Point(6, 6);
            this.lstBStock.Name = "lstBStock";
            this.lstBStock.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstBStock.Size = new System.Drawing.Size(174, 121);
            this.lstBStock.TabIndex = 0;
            this.tabPage1.SetToolTip(this.lstBStock, "choose stocks");
            // 
            // cntMnuMain
            // 
            this.cntMnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.cntMnuMain.Name = "cntMnuMain";
            this.cntMnuMain.OwnerItem = this.tsbtnImport;
            this.cntMnuMain.Size = new System.Drawing.Size(129, 48);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Checked = true;
            this.selectAllToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // tsbtnImport
            // 
            this.tsbtnImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnImportSingle,
            this.tsbtnImportMultiple});
            this.tsbtnImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImport.Image")));
            this.tsbtnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnImport.Name = "tsbtnImport";
            this.tsbtnImport.Size = new System.Drawing.Size(68, 22);
            this.tsbtnImport.Text = "Import";
            this.tsbtnImport.ToolTipText = "Import stock price";
            // 
            // tsbtnImportSingle
            // 
            this.tsbtnImportSingle.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImportSingle.Image")));
            this.tsbtnImportSingle.Name = "tsbtnImportSingle";
            this.tsbtnImportSingle.Size = new System.Drawing.Size(156, 22);
            this.tsbtnImportSingle.Text = "Import &Single";
            this.tsbtnImportSingle.ToolTipText = "Import single stocks price";
            this.tsbtnImportSingle.Click += new System.EventHandler(this.tsbtnImports_Click);
            // 
            // tsbtnImportMultiple
            // 
            this.tsbtnImportMultiple.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImportMultiple.Image")));
            this.tsbtnImportMultiple.Name = "tsbtnImportMultiple";
            this.tsbtnImportMultiple.Size = new System.Drawing.Size(156, 22);
            this.tsbtnImportMultiple.Text = "Import &Multiple";
            this.tsbtnImportMultiple.ToolTipText = "Import multiple stocks price";
            this.tsbtnImportMultiple.Click += new System.EventHandler(this.tsbtnImports_Click);
            // 
            // txtProxySrvName
            // 
            this.txtProxySrvName.Location = new System.Drawing.Point(11, 34);
            this.txtProxySrvName.Name = "txtProxySrvName";
            this.txtProxySrvName.Size = new System.Drawing.Size(157, 20);
            this.txtProxySrvName.TabIndex = 1;
            this.tabPage1.SetToolTip(this.txtProxySrvName, "[PROXY Server Name]:[PORT No]");
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Image = global::DoubleM.Properties.Resources.Exclaimation;
            this.btnStart.Location = new System.Drawing.Point(147, 148);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(33, 20);
            this.btnStart.TabIndex = 2;
            this.tabPage1.SetToolTip(this.btnStart, "Click to load Tradings");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lstBStocks
            // 
            this.lstBStocks.FormattingEnabled = true;
            this.lstBStocks.Location = new System.Drawing.Point(6, 31);
            this.lstBStocks.Name = "lstBStocks";
            this.lstBStocks.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstBStocks.Size = new System.Drawing.Size(174, 69);
            this.lstBStocks.TabIndex = 1;
            this.tabPage1.SetToolTip(this.lstBStocks, "choose stocks");
            // 
            // dtpSince
            // 
            this.dtpSince.CustomFormat = "ddd dd, MMM, yyyy";
            this.dtpSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSince.Location = new System.Drawing.Point(6, 112);
            this.dtpSince.Name = "dtpSince";
            this.dtpSince.Size = new System.Drawing.Size(174, 20);
            this.dtpSince.TabIndex = 3;
            this.tabPage1.SetToolTip(this.dtpSince, "Starting date");
            // 
            // chkAllStocks
            // 
            this.chkAllStocks.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAllStocks.Image = global::DoubleM.Properties.Resources.TASKL;
            this.chkAllStocks.Location = new System.Drawing.Point(6, 3);
            this.chkAllStocks.Name = "chkAllStocks";
            this.chkAllStocks.Size = new System.Drawing.Size(24, 24);
            this.chkAllStocks.TabIndex = 0;
            this.tabPage1.SetToolTip(this.chkAllStocks, "All Stocks");
            this.chkAllStocks.UseVisualStyleBackColor = true;
            this.chkAllStocks.CheckedChanged += new System.EventHandler(this.chkAllStocks_CheckedChanged);
            // 
            // rdobPeriod
            // 
            this.rdobPeriod.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdobPeriod.Image = global::DoubleM.Properties.Resources.Elenchi_materiale;
            this.rdobPeriod.Location = new System.Drawing.Point(96, 3);
            this.rdobPeriod.Name = "rdobPeriod";
            this.rdobPeriod.Size = new System.Drawing.Size(24, 24);
            this.rdobPeriod.TabIndex = 5;
            this.rdobPeriod.TabStop = true;
            this.tabPage1.SetToolTip(this.rdobPeriod, "Select for Period");
            this.rdobPeriod.UseVisualStyleBackColor = true;
            this.rdobPeriod.CheckedChanged += new System.EventHandler(this.rdobPeriod_CheckedChanged);
            // 
            // rdobDateStarts
            // 
            this.rdobDateStarts.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdobDateStarts.Image = global::DoubleM.Properties.Resources.tempario;
            this.rdobDateStarts.Location = new System.Drawing.Point(126, 3);
            this.rdobDateStarts.Name = "rdobDateStarts";
            this.rdobDateStarts.Size = new System.Drawing.Size(24, 24);
            this.rdobDateStarts.TabIndex = 6;
            this.rdobDateStarts.TabStop = true;
            this.tabPage1.SetToolTip(this.rdobDateStarts, "Select form Date");
            this.rdobDateStarts.UseVisualStyleBackColor = true;
            this.rdobDateStarts.CheckedChanged += new System.EventHandler(this.rdobPeriod_CheckedChanged);
            // 
            // rdobDateIn
            // 
            this.rdobDateIn.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdobDateIn.Image = global::DoubleM.Properties.Resources.Inserimento_iva;
            this.rdobDateIn.Location = new System.Drawing.Point(156, 3);
            this.rdobDateIn.Name = "rdobDateIn";
            this.rdobDateIn.Size = new System.Drawing.Size(24, 24);
            this.rdobDateIn.TabIndex = 7;
            this.rdobDateIn.TabStop = true;
            this.tabPage1.SetToolTip(this.rdobDateIn, "Select within Dates");
            this.rdobDateIn.UseVisualStyleBackColor = true;
            this.rdobDateIn.CheckedChanged += new System.EventHandler(this.rdobPeriod_CheckedChanged);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "ddd dd, MMM, yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(6, 138);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(174, 20);
            this.dtpTo.TabIndex = 8;
            this.tabPage1.SetToolTip(this.dtpTo, "Ending date");
            // 
            // chkFromTo
            // 
            this.chkFromTo.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkFromTo.Image = global::DoubleM.Properties.Resources.FastForward;
            this.chkFromTo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkFromTo.Location = new System.Drawing.Point(6, 164);
            this.chkFromTo.Name = "chkFromTo";
            this.chkFromTo.Size = new System.Drawing.Size(51, 25);
            this.chkFromTo.TabIndex = 4;
            this.chkFromTo.Text = "AB";
            this.chkFromTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tabPage1.SetToolTip(this.chkFromTo, "Before or After the date");
            this.chkFromTo.UseVisualStyleBackColor = true;
            this.chkFromTo.CheckedChanged += new System.EventHandler(this.chkFromTo_CheckedChanged);
            // 
            // SStripMain
            // 
            this.SStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SStripMainlblDate,
            this.lblDMMsg,
            this.toolStripStatusLabel4,
            this.pBarDM,
            this.SStripMainlblAbout});
            this.SStripMain.Location = new System.Drawing.Point(0, 664);
            this.SStripMain.Name = "SStripMain";
            this.SStripMain.Size = new System.Drawing.Size(820, 22);
            this.SStripMain.TabIndex = 6;
            // 
            // SStripMainlblDate
            // 
            this.SStripMainlblDate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.SStripMainlblDate.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.SStripMainlblDate.Name = "SStripMainlblDate";
            this.SStripMainlblDate.Size = new System.Drawing.Size(34, 17);
            this.SStripMainlblDate.Text = "Date";
            this.SStripMainlblDate.ToolTipText = "Current Date";
            // 
            // lblDMMsg
            // 
            this.lblDMMsg.AutoSize = false;
            this.lblDMMsg.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblDMMsg.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblDMMsg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblDMMsg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDMMsg.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.lblDMMsg.Name = "lblDMMsg";
            this.lblDMMsg.Size = new System.Drawing.Size(644, 17);
            this.lblDMMsg.Spring = true;
            this.lblDMMsg.Text = "Welcome to Market Manager - Double\"M\"";
            this.lblDMMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDMMsg.ToolTipText = "Message for your referance";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            // 
            // pBarDM
            // 
            this.pBarDM.Name = "pBarDM";
            this.pBarDM.Size = new System.Drawing.Size(109, 16);
            this.pBarDM.ToolTipText = "Action in progress";
            // 
            // SStripMainlblAbout
            // 
            this.SStripMainlblAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SStripMainlblAbout.Image = ((System.Drawing.Image)(resources.GetObject("SStripMainlblAbout.Image")));
            this.SStripMainlblAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SStripMainlblAbout.Name = "SStripMainlblAbout";
            this.SStripMainlblAbout.Size = new System.Drawing.Size(16, 17);
            this.SStripMainlblAbout.Click += new System.EventHandler(this.SStripMainlblAbout_Click);
            // 
            // imglstMain
            // 
            this.imglstMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstMain.ImageStream")));
            this.imglstMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstMain.Images.SetKeyName(0, "28-Desktop.ico");
            this.imglstMain.Images.SetKeyName(1, "19-default internet 00.ico");
            this.imglstMain.Images.SetKeyName(2, "Graph.gif");
            this.imglstMain.Images.SetKeyName(3, "Prosetup.ico");
            this.imglstMain.Images.SetKeyName(4, "1440.ico");
            this.imglstMain.Images.SetKeyName(5, "favicon.ico");
            this.imglstMain.Images.SetKeyName(6, "print.gif");
            // 
            // scrtxtStock
            // 
            this.scrtxtStock.BackColor = System.Drawing.SystemColors.ControlText;
            this.scrtxtStock.BorderColor = System.Drawing.Color.Black;
            this.scrtxtStock.Cursor = System.Windows.Forms.Cursors.Default;
            this.scrtxtStock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scrtxtStock.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrtxtStock.ForeColor = System.Drawing.Color.White;
            this.scrtxtStock.ForegroundBrush = null;
            this.scrtxtStock.Location = new System.Drawing.Point(0, 646);
            this.scrtxtStock.Name = "scrtxtStock";
            this.scrtxtStock.ScrollDirection = ScrollingTextControl.ScrollDirection.RightToLeft;
            this.scrtxtStock.ScrollText = "";
            this.scrtxtStock.ShowBorder = true;
            this.scrtxtStock.Size = new System.Drawing.Size(820, 18);
            this.scrtxtStock.StopScrollOnMouseOver = true;
            this.scrtxtStock.TabIndex = 2;
            this.scrtxtStock.TextScrollDistance = 2;
            this.scrtxtStock.TextScrollSpeed = 25;
            this.scrtxtStock.VerticleTextPosition = ScrollingTextControl.VerticleTextPosition.Center;
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnExcel,
            this.tsbtnPrint,
            this.toolStripSeparator1,
            this.toolStripButton6,
            this.tsbtnDownload,
            this.tsbtnImport,
            this.toolStripSeparator2});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(820, 25);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            this.toolStripMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMain_ItemClicked);
            // 
            // tsbtnExcel
            // 
            this.tsbtnExcel.Image = global::DoubleM.Properties.Resources.Excel_16;
            this.tsbtnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExcel.Name = "tsbtnExcel";
            this.tsbtnExcel.Size = new System.Drawing.Size(52, 22);
            this.tsbtnExcel.Text = "&Excel";
            this.tsbtnExcel.ToolTipText = "Export Active Grid to Excel";
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.Image = global::DoubleM.Properties.Resources.Elenchi_materiale;
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(49, 22);
            this.tsbtnPrint.Text = "&Print";
            this.tsbtnPrint.ToolTipText = "Print Active Grid";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::DoubleM.Properties.Resources.Exclaimation;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.Visible = false;
            // 
            // tsbtnDownload
            // 
            this.tsbtnDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDownload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnDownloadBSE,
            this.tsbtnDownloadNSE,
            this.tsbtnDownloadY});
            this.tsbtnDownload.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDownload.Image")));
            this.tsbtnDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDownload.Name = "tsbtnDownload";
            this.tsbtnDownload.Size = new System.Drawing.Size(29, 22);
            this.tsbtnDownload.Text = "Download Historical data";
            // 
            // tsbtnDownloadBSE
            // 
            this.tsbtnDownloadBSE.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDownloadBSE.Image")));
            this.tsbtnDownloadBSE.Name = "tsbtnDownloadBSE";
            this.tsbtnDownloadBSE.ShortcutKeyDisplayString = "Ctrl+B";
            this.tsbtnDownloadBSE.Size = new System.Drawing.Size(210, 22);
            this.tsbtnDownloadBSE.Text = "Dowload from BSE";
            this.tsbtnDownloadBSE.ToolTipText = "Download historical data from BSE";
            this.tsbtnDownloadBSE.Click += new System.EventHandler(this.tsbtnDownload_Click);
            // 
            // tsbtnDownloadNSE
            // 
            this.tsbtnDownloadNSE.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDownloadNSE.Image")));
            this.tsbtnDownloadNSE.Name = "tsbtnDownloadNSE";
            this.tsbtnDownloadNSE.Size = new System.Drawing.Size(210, 22);
            this.tsbtnDownloadNSE.Text = "Dowload from NSE";
            this.tsbtnDownloadNSE.ToolTipText = "Download historical data from NSE";
            this.tsbtnDownloadNSE.Click += new System.EventHandler(this.tsbtnDownload_Click);
            // 
            // tsbtnDownloadY
            // 
            this.tsbtnDownloadY.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDownloadY.Image")));
            this.tsbtnDownloadY.Name = "tsbtnDownloadY";
            this.tsbtnDownloadY.Size = new System.Drawing.Size(210, 22);
            this.tsbtnDownloadY.Text = "Dowload from Yahoo";
            this.tsbtnDownloadY.ToolTipText = "Download historical data from Yahoo";
            this.tsbtnDownloadY.Click += new System.EventHandler(this.tsbtnDownload_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // sptContMain
            // 
            this.sptContMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sptContMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.sptContMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sptContMain.IsSplitterFixed = true;
            this.sptContMain.Location = new System.Drawing.Point(0, 25);
            this.sptContMain.Name = "sptContMain";
            this.sptContMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sptContMain.Panel1
            // 
            this.sptContMain.Panel1.Controls.Add(this._groupPaneBar);
            this.sptContMain.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // sptContMain.Panel2
            // 
            this.sptContMain.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.sptContMain.Panel2.Controls.Add(this.tbMain);
            this.sptContMain.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sptContMain.Size = new System.Drawing.Size(217, 621);
            this.sptContMain.SplitterDistance = 400;
            this.sptContMain.TabIndex = 23;
            // 
            // _groupPaneBar
            // 
            this._groupPaneBar.AutoScroll = true;
            this._groupPaneBar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._groupPaneBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupPaneBar.HeaderColor1 = System.Drawing.Color.Gray;
            this._groupPaneBar.HeaderColor2 = System.Drawing.Color.Snow;
            this._groupPaneBar.Location = new System.Drawing.Point(0, 0);
            this._groupPaneBar.MinimumExpandedHeight = 30;
            this._groupPaneBar.Name = "_groupPaneBar";
            this._groupPaneBar.Padding = new System.Windows.Forms.Padding(1);
            this._groupPaneBar.Size = new System.Drawing.Size(213, 396);
            this._groupPaneBar.TabIndex = 6;
            this._groupPaneBar.Text = "groupPaneBar1";
            // 
            // tbMain
            // 
            this.tbMain.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tbMain.Controls.Add(this.tpMain);
            this.tbMain.Controls.Add(this.tabPage3);
            this.tbMain.Controls.Add(this.tabPage4);
            this.tbMain.Controls.Add(this.tabPage2);
            this.tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Margin = new System.Windows.Forms.Padding(0);
            this.tbMain.Multiline = true;
            this.tbMain.Name = "tbMain";
            this.tbMain.SelectedIndex = 0;
            this.tbMain.Size = new System.Drawing.Size(213, 213);
            this.tbMain.TabIndex = 4;
            // 
            // tpMain
            // 
            this.tpMain.Controls.Add(this.btnSearch);
            this.tpMain.Controls.Add(this.rdobDateIn);
            this.tpMain.Controls.Add(this.rdobDateStarts);
            this.tpMain.Controls.Add(this.rdobPeriod);
            this.tpMain.Controls.Add(this.chkFromTo);
            this.tpMain.Controls.Add(this.lstBStocks);
            this.tpMain.Controls.Add(this.chkAllStocks);
            this.tpMain.Controls.Add(this.dtpTo);
            this.tpMain.Controls.Add(this.dtpSince);
            this.tpMain.Controls.Add(this.ddlPeriod);
            this.tpMain.Location = new System.Drawing.Point(4, 4);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(186, 205);
            this.tpMain.TabIndex = 0;
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::DoubleM.Properties.Resources.Exclaimation;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(129, 164);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(51, 25);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Load";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ddlPeriod
            // 
            this.ddlPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPeriod.FormattingEnabled = true;
            this.ddlPeriod.Location = new System.Drawing.Point(6, 111);
            this.ddlPeriod.Name = "ddlPeriod";
            this.ddlPeriod.Size = new System.Drawing.Size(174, 21);
            this.ddlPeriod.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chkAfterBefore);
            this.tabPage3.Controls.Add(this.dtPKStart);
            this.tabPage3.Controls.Add(this.btnStart);
            this.tabPage3.Controls.Add(this.lstBStock);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(186, 205);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chkAfterBefore
            // 
            this.chkAfterBefore.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAfterBefore.Font = new System.Drawing.Font("Symbol", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAfterBefore.Location = new System.Drawing.Point(6, 148);
            this.chkAfterBefore.Name = "chkAfterBefore";
            this.chkAfterBefore.Size = new System.Drawing.Size(17, 18);
            this.chkAfterBefore.TabIndex = 3;
            this.chkAfterBefore.Text = ">";
            this.chkAfterBefore.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAfterBefore.UseVisualStyleBackColor = true;
            this.chkAfterBefore.CheckedChanged += new System.EventHandler(this.chkAfterBefore_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnSave);
            this.tabPage4.Controls.Add(this.pnlProxy);
            this.tabPage4.Controls.Add(this.chkProxy);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(186, 205);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(44, 175);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 24);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlProxy
            // 
            this.pnlProxy.AutoScroll = true;
            this.pnlProxy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlProxy.Controls.Add(this.txtPWD);
            this.pnlProxy.Controls.Add(this.lblPWD);
            this.pnlProxy.Controls.Add(this.txtUID);
            this.pnlProxy.Controls.Add(this.lblUID);
            this.pnlProxy.Controls.Add(this.txtProxySrvName);
            this.pnlProxy.Controls.Add(this.lblProxySrvName);
            this.pnlProxy.Location = new System.Drawing.Point(3, 30);
            this.pnlProxy.Name = "pnlProxy";
            this.pnlProxy.Size = new System.Drawing.Size(177, 139);
            this.pnlProxy.TabIndex = 2;
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(50, 92);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '●';
            this.txtPWD.Size = new System.Drawing.Size(117, 20);
            this.txtPWD.TabIndex = 5;
            // 
            // lblPWD
            // 
            this.lblPWD.AutoSize = true;
            this.lblPWD.Location = new System.Drawing.Point(8, 87);
            this.lblPWD.Name = "lblPWD";
            this.lblPWD.Size = new System.Drawing.Size(33, 13);
            this.lblPWD.TabIndex = 4;
            this.lblPWD.Text = "PWD";
            // 
            // txtUID
            // 
            this.txtUID.Location = new System.Drawing.Point(50, 64);
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(117, 20);
            this.txtUID.TabIndex = 3;
            // 
            // lblUID
            // 
            this.lblUID.AutoSize = true;
            this.lblUID.Location = new System.Drawing.Point(9, 67);
            this.lblUID.Name = "lblUID";
            this.lblUID.Size = new System.Drawing.Size(43, 13);
            this.lblUID.TabIndex = 2;
            this.lblUID.Text = "User ID";
            // 
            // lblProxySrvName
            // 
            this.lblProxySrvName.AutoSize = true;
            this.lblProxySrvName.Location = new System.Drawing.Point(9, 14);
            this.lblProxySrvName.Name = "lblProxySrvName";
            this.lblProxySrvName.Size = new System.Drawing.Size(98, 13);
            this.lblProxySrvName.TabIndex = 0;
            this.lblProxySrvName.Text = "Proxy Server Name";
            // 
            // chkProxy
            // 
            this.chkProxy.AutoSize = true;
            this.chkProxy.Location = new System.Drawing.Point(22, 7);
            this.chkProxy.Name = "chkProxy";
            this.chkProxy.Size = new System.Drawing.Size(94, 17);
            this.chkProxy.TabIndex = 1;
            this.chkProxy.Text = "Proxy Enabled";
            this.chkProxy.UseVisualStyleBackColor = true;
            this.chkProxy.CheckedChanged += new System.EventHandler(this.chkProxy_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(186, 205);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "s";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // prtDlgMain
            // 
            this.prtDlgMain.UseEXDialog = true;
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackgroundImage = global::DoubleM.Properties.Resources.Main2M;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(820, 686);
            this.Controls.Add(this.sptContMain);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.scrtxtStock);
            this.Controls.Add(this.SStripMain);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DoubleM: Market Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.cntMnuMain.ResumeLayout(false);
            this.SStripMain.ResumeLayout(false);
            this.SStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.sptContMain.Panel1.ResumeLayout(false);
            this.sptContMain.Panel2.ResumeLayout(false);
            this.sptContMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._groupPaneBar)).EndInit();
            this.tbMain.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.pnlProxy.ResumeLayout(false);
            this.pnlProxy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip tabPage1;
        private System.Windows.Forms.StatusStrip SStripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel SStripMainlblAbout;
        internal System.Windows.Forms.ToolStripProgressBar pBarDM;
        internal System.Windows.Forms.ToolStripStatusLabel SStripMainlblDate;
        internal System.Windows.Forms.ToolStripStatusLabel lblDMMsg;
        private System.Windows.Forms.ImageList imglstMain;
        private ScrollingTextControl.ScrollingText scrtxtStock;
        private System.Windows.Forms.ContextMenuStrip cntMnuMain;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.SplitContainer sptContMain;
        private BarTender.GroupPaneBar _groupPaneBar;
        private System.Windows.Forms.TabControl tbMain;
        private System.Windows.Forms.TabPage tpMain;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chkAfterBefore;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DateTimePicker dtPKStart;
        private System.Windows.Forms.ListBox lstBStock;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsbtnExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.CheckBox chkProxy;
        private System.Windows.Forms.Panel pnlProxy;
        private System.Windows.Forms.Label lblUID;
        private System.Windows.Forms.TextBox txtProxySrvName;
        private System.Windows.Forms.Label lblProxySrvName;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label lblPWD;
        private System.Windows.Forms.TextBox txtUID;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PrintDialog prtDlgMain;
        private System.Drawing.Printing.PrintDocument prtDocMain;
        private System.Windows.Forms.CheckBox chkAllStocks;
        private System.Windows.Forms.DateTimePicker dtpSince;
        private System.Windows.Forms.ComboBox ddlPeriod;
        private System.Windows.Forms.ListBox lstBStocks;
        private System.Windows.Forms.CheckBox chkFromTo;
        private System.Windows.Forms.RadioButton rdobDateIn;
        private System.Windows.Forms.RadioButton rdobDateStarts;
        private System.Windows.Forms.RadioButton rdobPeriod;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.SaveFileDialog saveFileDlgExport;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnImport;
        private System.Windows.Forms.ToolStripMenuItem tsbtnImportSingle;
        private System.Windows.Forms.ToolStripMenuItem tsbtnImportMultiple;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnDownload;
        private System.Windows.Forms.ToolStripMenuItem tsbtnDownloadBSE;
        private System.Windows.Forms.ToolStripMenuItem tsbtnDownloadNSE;
        private System.Windows.Forms.ToolStripMenuItem tsbtnDownloadY;
    }
}

