namespace DoubleM
{
    partial class frmDataImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkActiveStock = new System.Windows.Forms.CheckBox();
            this.dtpPriceDate = new System.Windows.Forms.DateTimePicker();
            this.ddlStock = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.btnValidate = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnFileOpen = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvImport = new System.Windows.Forms.DataGridView();
            this.openFileDlgImport = new System.Windows.Forms.OpenFileDialog();
            this.cmsLoaddata = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.chkActiveStock);
            this.panel1.Controls.Add(this.dtpPriceDate);
            this.panel1.Controls.Add(this.ddlStock);
            this.panel1.Controls.Add(this.lblMode);
            this.panel1.Controls.Add(this.btnValidate);
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.btnFileOpen);
            this.panel1.Controls.Add(this.txtFileName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 81);
            this.panel1.TabIndex = 4;
            // 
            // chkActiveStock
            // 
            this.chkActiveStock.AutoSize = true;
            this.chkActiveStock.Checked = true;
            this.chkActiveStock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActiveStock.Location = new System.Drawing.Point(63, 60);
            this.chkActiveStock.Name = "chkActiveStock";
            this.chkActiveStock.Size = new System.Drawing.Size(15, 14);
            this.chkActiveStock.TabIndex = 14;
            this.chkActiveStock.ThreeState = true;
            this.chkActiveStock.UseVisualStyleBackColor = true;
            this.chkActiveStock.Visible = false;
            this.chkActiveStock.CheckStateChanged += new System.EventHandler(this.chkActiveStock_CheckStateChanged);
            // 
            // dtpPriceDate
            // 
            this.dtpPriceDate.CustomFormat = "dd MMMM yyyy hh:mm:ss tt";
            this.dtpPriceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPriceDate.Location = new System.Drawing.Point(63, 34);
            this.dtpPriceDate.Name = "dtpPriceDate";
            this.dtpPriceDate.Size = new System.Drawing.Size(219, 20);
            this.dtpPriceDate.TabIndex = 13;
            this.dtpPriceDate.Visible = false;
            // 
            // ddlStock
            // 
            this.ddlStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStock.FormattingEnabled = true;
            this.ddlStock.Location = new System.Drawing.Point(63, 35);
            this.ddlStock.MaxDropDownItems = 25;
            this.ddlStock.Name = "ddlStock";
            this.ddlStock.Size = new System.Drawing.Size(219, 21);
            this.ddlStock.TabIndex = 4;
            this.ddlStock.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(3, 38);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(40, 13);
            this.lblMode.TabIndex = 12;
            this.lblMode.Text = "Stocks";
            // 
            // btnValidate
            // 
            this.btnValidate.Image = ((System.Drawing.Image)(resources.GetObject("btnValidate.Image")));
            this.btnValidate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValidate.Location = new System.Drawing.Point(288, 33);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(128, 23);
            this.btnValidate.TabIndex = 5;
            this.btnValidate.Text = "Validate && Import";
            this.btnValidate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnValidate, "Validate & Import");
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(442, 10);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 15);
            this.lblMsg.TabIndex = 6;
            // 
            // btnLoad
            // 
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoad.Location = new System.Drawing.Point(352, 10);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(64, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "&Load";
            this.btnLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnLoad, "Load the selected file");
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnFileOpen.Image")));
            this.btnFileOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFileOpen.Location = new System.Drawing.Point(288, 10);
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(64, 23);
            this.btnFileOpen.TabIndex = 2;
            this.btnFileOpen.Text = "&Open";
            this.btnFileOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnFileOpen, "Browse for file");
            this.btnFileOpen.UseVisualStyleBackColor = true;
            this.btnFileOpen.Click += new System.EventHandler(this.btnFileOpen_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(63, 10);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(219, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Name";
            // 
            // dgvImport
            // 
            this.dgvImport.AllowUserToAddRows = false;
            this.dgvImport.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Thistle;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgvImport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImport.GridColor = System.Drawing.Color.Violet;
            this.dgvImport.Location = new System.Drawing.Point(0, 81);
            this.dgvImport.Name = "dgvImport";
            this.dgvImport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImport.Size = new System.Drawing.Size(726, 183);
            this.dgvImport.TabIndex = 5;
            this.dgvImport.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvImport_CellMouseDown);
            // 
            // cmsLoaddata
            // 
            this.cmsLoaddata.Name = "cmsLoaddata";
            this.cmsLoaddata.ShowImageMargin = false;
            this.cmsLoaddata.Size = new System.Drawing.Size(36, 4);
            this.cmsLoaddata.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsLoaddata_ItemClicked);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmDataImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 264);
            this.Controls.Add(this.dgvImport);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDataImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Data Import";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDataImport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnFileOpen;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvImport;
        private System.Windows.Forms.OpenFileDialog openFileDlgImport;
        private System.Windows.Forms.ContextMenuStrip cmsLoaddata;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox ddlStock;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker dtpPriceDate;
        private System.Windows.Forms.CheckBox chkActiveStock;

    }
}