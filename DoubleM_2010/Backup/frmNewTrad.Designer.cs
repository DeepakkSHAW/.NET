namespace DoubleM
{
    partial class frmNewTrad
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
            this.lblStock = new System.Windows.Forms.Label();
            this.ddlStock = new System.Windows.Forms.ComboBox();
            this.lblQnt = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpOndate = new System.Windows.Forms.DateTimePicker();
            this.lblBrok = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtQnt = new System.Windows.Forms.TextBox();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.txtBrok = new System.Windows.Forms.TextBox();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.chkBS = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.ttipBuySell = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(3, 15);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(35, 13);
            this.lblStock.TabIndex = 0;
            this.lblStock.Text = "Stock";
            // 
            // ddlStock
            // 
            this.ddlStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStock.FormattingEnabled = true;
            this.ddlStock.Location = new System.Drawing.Point(66, 12);
            this.ddlStock.Name = "ddlStock";
            this.ddlStock.Size = new System.Drawing.Size(219, 21);
            this.ddlStock.TabIndex = 1;
            this.ttipBuySell.SetToolTip(this.ddlStock, "Please choose the stock for trading");
            // 
            // lblQnt
            // 
            this.lblQnt.AutoSize = true;
            this.lblQnt.Location = new System.Drawing.Point(4, 66);
            this.lblQnt.Name = "lblQnt";
            this.lblQnt.Size = new System.Drawing.Size(46, 13);
            this.lblQnt.TabIndex = 2;
            this.lblQnt.Text = "Quantity";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(195, 62);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(31, 13);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "Price";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(4, 128);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(56, 13);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date Time";
            // 
            // dtpOndate
            // 
            this.dtpOndate.CustomFormat = "dd/MM/yyyy   hh:mm";
            this.dtpOndate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOndate.Location = new System.Drawing.Point(66, 125);
            this.dtpOndate.Name = "dtpOndate";
            this.dtpOndate.Size = new System.Drawing.Size(219, 20);
            this.dtpOndate.TabIndex = 7;
            this.ttipBuySell.SetToolTip(this.dtpOndate, "Date time of trading");
            // 
            // lblBrok
            // 
            this.lblBrok.AutoSize = true;
            this.lblBrok.Location = new System.Drawing.Point(4, 98);
            this.lblBrok.Name = "lblBrok";
            this.lblBrok.Size = new System.Drawing.Size(56, 13);
            this.lblBrok.TabIndex = 8;
            this.lblBrok.Text = "Brokerage";
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Location = new System.Drawing.Point(198, 97);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(28, 13);
            this.lblTax.TabIndex = 10;
            this.lblTax.Text = "Tax.";
            // 
            // btnOk
            // 
            this.btnOk.Image = global::DoubleM.Properties.Resources.Check1;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(106, 212);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(87, 31);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "&Done";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtQnt
            // 
            this.txtQnt.Location = new System.Drawing.Point(66, 60);
            this.txtQnt.MaxLength = 10;
            this.txtQnt.Name = "txtQnt";
            this.txtQnt.Size = new System.Drawing.Size(54, 20);
            this.txtQnt.TabIndex = 3;
            this.ttipBuySell.SetToolTip(this.txtQnt, "Stock Quantity, Can\'t be negative");
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(231, 59);
            this.txtRate.MaxLength = 10;
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(54, 20);
            this.txtRate.TabIndex = 4;
            this.ttipBuySell.SetToolTip(this.txtRate, "Price per stock, Can\'t be negative");
            // 
            // txtBrok
            // 
            this.txtBrok.Location = new System.Drawing.Point(66, 95);
            this.txtBrok.MaxLength = 10;
            this.txtBrok.Name = "txtBrok";
            this.txtBrok.Size = new System.Drawing.Size(54, 20);
            this.txtBrok.TabIndex = 5;
            this.ttipBuySell.SetToolTip(this.txtBrok, "Brokerage Paid, Can\'t be negative");
            // 
            // txtTax
            // 
            this.txtTax.Location = new System.Drawing.Point(232, 92);
            this.txtTax.MaxLength = 10;
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(54, 20);
            this.txtTax.TabIndex = 6;
            this.ttipBuySell.SetToolTip(this.txtTax, "Tax paid, Can\'t be negative");
            // 
            // chkBS
            // 
            this.chkBS.AutoSize = true;
            this.chkBS.Checked = true;
            this.chkBS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBS.Location = new System.Drawing.Point(66, 39);
            this.chkBS.Name = "chkBS";
            this.chkBS.Size = new System.Drawing.Size(64, 17);
            this.chkBS.TabIndex = 2;
            this.chkBS.Text = "Buy/sell";
            this.ttipBuySell.SetToolTip(this.chkBS, "Check - Buy\r\nUncheck - Sell");
            this.chkBS.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(3, 162);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(35, 13);
            this.lblNote.TabIndex = 11;
            this.lblNote.Text = "Notes";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(66, 155);
            this.txtNote.MaxLength = 255;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNote.Size = new System.Drawing.Size(219, 47);
            this.txtNote.TabIndex = 8;
            this.ttipBuySell.SetToolTip(this.txtNote, "Special notes if any.\r\nCtrl+Enter for New line.\r\nOnly 255 letters allowed.");
            // 
            // frmNewTrad
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 250);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.chkBS);
            this.Controls.Add(this.txtTax);
            this.Controls.Add(this.txtBrok);
            this.Controls.Add(this.txtRate);
            this.Controls.Add(this.txtQnt);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblTax);
            this.Controls.Add(this.lblBrok);
            this.Controls.Add(this.dtpOndate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblQnt);
            this.Controls.Add(this.ddlStock);
            this.Controls.Add(this.lblStock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmNewTrad";
            this.Opacity = 0.8;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Trading";
            this.Load += new System.EventHandler(this.frmNewTrad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.ComboBox ddlStock;
        private System.Windows.Forms.Label lblQnt;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpOndate;
        private System.Windows.Forms.Label lblBrok;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtQnt;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.TextBox txtBrok;
        private System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.CheckBox chkBS;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.ToolTip ttipBuySell;
    }
}