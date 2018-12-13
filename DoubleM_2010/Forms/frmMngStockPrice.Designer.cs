namespace DoubleM
{
    partial class frmMngStockPrice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMngStockPrice));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tpSPrice = new System.Windows.Forms.ToolTip(this.components);
            this.tsSPrice = new System.Windows.Forms.ToolStrip();
            this.tsbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDeletePrice = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnRefesh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClean = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnGraph = new System.Windows.Forms.ToolStripButton();
            this.tscboStocks = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnSelectStock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.dtpOndate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.dgvSPrice = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tsSPrice.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tsSPrice
            // 
            this.tsSPrice.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNew,
            this.tsbtnUpdate,
            this.tsbtnDeletePrice,
            this.toolStripSeparator2,
            this.tsbtnRefesh,
            this.tsbtnClean,
            this.toolStripSeparator1,
            this.tsbtnGraph,
            this.tscboStocks,
            this.tsbtnSelectStock,
            this.toolStripSeparator3});
            this.tsSPrice.Location = new System.Drawing.Point(0, 0);
            this.tsSPrice.Name = "tsSPrice";
            this.tsSPrice.Size = new System.Drawing.Size(728, 25);
            this.tsSPrice.TabIndex = 0;
            this.tsSPrice.Text = "toolStrip1";
            this.tsSPrice.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsSPrice_ItemClicked);
            // 
            // tsbtnNew
            // 
            this.tsbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNew.Image")));
            this.tsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNew.Name = "tsbtnNew";
            this.tsbtnNew.Size = new System.Drawing.Size(23, 22);
            this.tsbtnNew.Text = "New Rate";
            this.tsbtnNew.ToolTipText = "Add new rate of stock";
            // 
            // tsbtnUpdate
            // 
            this.tsbtnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUpdate.Image")));
            this.tsbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUpdate.Name = "tsbtnUpdate";
            this.tsbtnUpdate.Size = new System.Drawing.Size(23, 22);
            this.tsbtnUpdate.Text = "Price update";
            this.tsbtnUpdate.ToolTipText = "Update stock price";
            // 
            // tsbtnDeletePrice
            // 
            this.tsbtnDeletePrice.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDeletePrice.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDeletePrice.Image")));
            this.tsbtnDeletePrice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeletePrice.Name = "tsbtnDeletePrice";
            this.tsbtnDeletePrice.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDeletePrice.Text = "Delete Stock Price";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnRefesh
            // 
            this.tsbtnRefesh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefesh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefesh.Image")));
            this.tsbtnRefesh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefesh.Name = "tsbtnRefesh";
            this.tsbtnRefesh.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefesh.Text = "Rate refresh";
            // 
            // tsbtnClean
            // 
            this.tsbtnClean.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClean.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClean.Image")));
            this.tsbtnClean.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClean.Name = "tsbtnClean";
            this.tsbtnClean.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClean.Text = "Clear";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnGraph
            // 
            this.tsbtnGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGraph.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGraph.Image")));
            this.tsbtnGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGraph.Name = "tsbtnGraph";
            this.tsbtnGraph.Size = new System.Drawing.Size(23, 22);
            this.tsbtnGraph.Text = "Draw Graph";
            // 
            // tscboStocks
            // 
            this.tscboStocks.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tscboStocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscboStocks.DropDownWidth = 121;
            this.tscboStocks.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tscboStocks.MaxDropDownItems = 10;
            this.tscboStocks.Name = "tscboStocks";
            this.tscboStocks.Size = new System.Drawing.Size(175, 25);
            this.tscboStocks.ToolTipText = "Available stocks";
            // 
            // tsbtnSelectStock
            // 
            this.tsbtnSelectStock.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnSelectStock.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSelectStock.Image")));
            this.tsbtnSelectStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbtnSelectStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSelectStock.Name = "tsbtnSelectStock";
            this.tsbtnSelectStock.Size = new System.Drawing.Size(85, 22);
            this.tsbtnSelectStock.Text = "Select Stock";
            this.tsbtnSelectStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbtnSelectStock.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbtnSelectStock.ToolTipText = "Select Stock from the drop down";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtRate);
            this.panel1.Controls.Add(this.dtpOndate);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.lblPrice);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 316);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 46);
            this.panel1.TabIndex = 1;
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(41, 15);
            this.txtRate.MaxLength = 10;
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(54, 20);
            this.txtRate.TabIndex = 9;
            this.txtRate.Enter += new System.EventHandler(this.txtRate_Enter);
            this.txtRate.Validating += new System.ComponentModel.CancelEventHandler(this.txtRate_Validating);
            // 
            // dtpOndate
            // 
            this.dtpOndate.CustomFormat = "dd/MM/yyyy   hh:mm";
            this.dtpOndate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOndate.Location = new System.Drawing.Point(179, 14);
            this.dtpOndate.Name = "dtpOndate";
            this.dtpOndate.Size = new System.Drawing.Size(219, 20);
            this.dtpOndate.TabIndex = 11;
            this.dtpOndate.Validating += new System.ComponentModel.CancelEventHandler(this.dtpOndate_Validating);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(117, 17);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(56, 13);
            this.lblDate.TabIndex = 10;
            this.lblDate.Text = "Date Time";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(5, 18);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(31, 13);
            this.lblPrice.TabIndex = 8;
            this.lblPrice.Text = "Price";
            // 
            // dgvSPrice
            // 
            this.dgvSPrice.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Thistle;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgvSPrice.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSPrice.GridColor = System.Drawing.Color.Violet;
            this.dgvSPrice.Location = new System.Drawing.Point(0, 25);
            this.dgvSPrice.Name = "dgvSPrice";
            this.dgvSPrice.ReadOnly = true;
            this.dgvSPrice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSPrice.Size = new System.Drawing.Size(728, 291);
            this.dgvSPrice.TabIndex = 2;
            this.dgvSPrice.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSPrice_CellDoubleClick);
            this.dgvSPrice.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvSPrice_RowsAdded);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmMngStockPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 362);
            this.Controls.Add(this.dgvSPrice);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tsSPrice);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMngStockPrice";
            this.Text = "Stock Price Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMngStockPrice_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMngStockPrice_FormClosing);
            this.tsSPrice.ResumeLayout(false);
            this.tsSPrice.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip tpSPrice;
        private System.Windows.Forms.ToolStrip tsSPrice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvSPrice;
        private System.Windows.Forms.ToolStripButton tsbtnNew;
        private System.Windows.Forms.ToolStripButton tsbtnUpdate;
        private System.Windows.Forms.ToolStripButton tsbtnDeletePrice;
        private System.Windows.Forms.ToolStripButton tsbtnClean;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnGraph;
        private System.Windows.Forms.ToolStripComboBox tscboStocks;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.DateTimePicker dtpOndate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripButton tsbtnRefesh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbtnSelectStock;
    }
}