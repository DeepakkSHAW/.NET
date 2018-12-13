namespace DoubleM
{
    partial class frmStocks
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvStocks = new System.Windows.Forms.DataGridView();
            this.StockName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HDFCCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalActiveStocks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAN = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlBCode = new System.Windows.Forms.ComboBox();
            this.ddlYCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlSName = new System.Windows.Forms.ComboBox();
            this.lblSName = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.ddlStock = new System.Windows.Forms.ComboBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStocks
            // 
            this.dgvStocks.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGreen;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.DeepSkyBlue;
            this.dgvStocks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvStocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockName,
            this.ShortName,
            this.YCode,
            this.HDFCCode,
            this.Active,
            this.StockID,
            this.InDate,
            this.TotalActiveStocks});
            this.dgvStocks.Location = new System.Drawing.Point(0, 0);
            this.dgvStocks.MultiSelect = false;
            this.dgvStocks.Name = "dgvStocks";
            this.dgvStocks.ReadOnly = true;
            this.dgvStocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStocks.Size = new System.Drawing.Size(693, 300);
            this.dgvStocks.TabIndex = 0;
            // 
            // StockName
            // 
            this.StockName.DataPropertyName = "StockName";
            this.StockName.HeaderText = "Stock Name";
            this.StockName.Name = "StockName";
            this.StockName.ReadOnly = true;
            this.StockName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StockName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.StockName.Width = 200;
            // 
            // ShortName
            // 
            this.ShortName.DataPropertyName = "ShortName";
            this.ShortName.HeaderText = "Short Name";
            this.ShortName.Name = "ShortName";
            this.ShortName.ReadOnly = true;
            // 
            // YCode
            // 
            this.YCode.DataPropertyName = "YFCode";
            this.YCode.HeaderText = "Yahoo Code";
            this.YCode.Name = "YCode";
            this.YCode.ReadOnly = true;
            this.YCode.Width = 120;
            // 
            // HDFCCode
            // 
            this.HDFCCode.DataPropertyName = "HDFCCode";
            this.HDFCCode.HeaderText = "HDFC Code";
            this.HDFCCode.Name = "HDFCCode";
            this.HDFCCode.ReadOnly = true;
            this.HDFCCode.Width = 110;
            // 
            // Active
            // 
            this.Active.DataPropertyName = "Active";
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            this.Active.Width = 60;
            // 
            // StockID
            // 
            this.StockID.DataPropertyName = "StockID";
            this.StockID.HeaderText = "StockID";
            this.StockID.Name = "StockID";
            this.StockID.ReadOnly = true;
            this.StockID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StockID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockID.Visible = false;
            // 
            // InDate
            // 
            this.InDate.DataPropertyName = "InDate";
            this.InDate.HeaderText = "InDate";
            this.InDate.Name = "InDate";
            this.InDate.ReadOnly = true;
            this.InDate.Visible = false;
            // 
            // TotalActiveStocks
            // 
            this.TotalActiveStocks.DataPropertyName = "TotalActiveStocks";
            this.TotalActiveStocks.HeaderText = "TotalActiveStocks";
            this.TotalActiveStocks.Name = "TotalActiveStocks";
            this.TotalActiveStocks.ReadOnly = true;
            this.TotalActiveStocks.Visible = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAN);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ddlBCode);
            this.groupBox1.Controls.Add(this.ddlYCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ddlSName);
            this.groupBox1.Controls.Add(this.lblSName);
            this.groupBox1.Controls.Add(this.lblStock);
            this.groupBox1.Controls.Add(this.ddlStock);
            this.groupBox1.Controls.Add(this.chkActive);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 300);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 74);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // chkAN
            // 
            this.chkAN.AutoSize = true;
            this.chkAN.Location = new System.Drawing.Point(567, 48);
            this.chkAN.Name = "chkAN";
            this.chkAN.Size = new System.Drawing.Size(80, 17);
            this.chkAN.TabIndex = 11;
            this.chkAN.Text = "checkBox1";
            this.chkAN.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Active",
            "In active"});
            this.comboBox1.Location = new System.Drawing.Point(468, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(79, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "HDFC Code";
            // 
            // ddlBCode
            // 
            this.ddlBCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlBCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlBCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.ddlBCode.FormattingEnabled = true;
            this.ddlBCode.Location = new System.Drawing.Point(306, 47);
            this.ddlBCode.Name = "ddlBCode";
            this.ddlBCode.Size = new System.Drawing.Size(138, 17);
            this.ddlBCode.TabIndex = 8;
            // 
            // ddlYCode
            // 
            this.ddlYCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlYCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlYCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.ddlYCode.FormattingEnabled = true;
            this.ddlYCode.Location = new System.Drawing.Point(73, 48);
            this.ddlYCode.Name = "ddlYCode";
            this.ddlYCode.Size = new System.Drawing.Size(138, 17);
            this.ddlYCode.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Yahoo Code";
            // 
            // ddlSName
            // 
            this.ddlSName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlSName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlSName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.ddlSName.FormattingEnabled = true;
            this.ddlSName.Location = new System.Drawing.Point(306, 16);
            this.ddlSName.MaxLength = 50;
            this.ddlSName.Name = "ddlSName";
            this.ddlSName.Size = new System.Drawing.Size(138, 17);
            this.ddlSName.TabIndex = 5;
            // 
            // lblSName
            // 
            this.lblSName.AutoSize = true;
            this.lblSName.Location = new System.Drawing.Point(237, 19);
            this.lblSName.Name = "lblSName";
            this.lblSName.Size = new System.Drawing.Size(63, 13);
            this.lblSName.TabIndex = 4;
            this.lblSName.Text = "Short Name";
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(32, 19);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(35, 13);
            this.lblStock.TabIndex = 3;
            this.lblStock.Text = "Stock";
            // 
            // ddlStock
            // 
            this.ddlStock.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlStock.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.ddlStock.FormattingEnabled = true;
            this.ddlStock.Location = new System.Drawing.Point(73, 16);
            this.ddlStock.MaxLength = 100;
            this.ddlStock.Name = "ddlStock";
            this.ddlStock.Size = new System.Drawing.Size(138, 17);
            this.ddlStock.TabIndex = 2;
            // 
            // chkActive
            // 
            this.chkActive.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(468, 10);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(113, 23);
            this.chkActive.TabIndex = 0;
            this.chkActive.Text = "Show Active Stocks";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // frmStocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 374);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvStocks);
            this.Name = "frmStocks";
            this.Text = "Arrange Stocks";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmStocks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStocks;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewLinkColumn StockName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn YCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn HDFCCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockID;
        private System.Windows.Forms.DataGridViewTextBoxColumn InDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalActiveStocks;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.ComboBox ddlStock;
        private System.Windows.Forms.ComboBox ddlSName;
        private System.Windows.Forms.Label lblSName;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.ComboBox ddlBCode;
        private System.Windows.Forms.ComboBox ddlYCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkAN;
    }
}