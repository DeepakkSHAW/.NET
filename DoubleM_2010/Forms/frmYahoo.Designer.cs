namespace DoubleM
{
    partial class frmYahoo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmYahoo));
            this.rdoDaily = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rdoWeekly = new System.Windows.Forms.RadioButton();
            this.rdoDividends = new System.Windows.Forms.RadioButton();
            this.rdoMonthly = new System.Windows.Forms.RadioButton();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlStock = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // rdoDaily
            // 
            this.rdoDaily.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoDaily.Image = ((System.Drawing.Image)(resources.GetObject("rdoDaily.Image")));
            this.rdoDaily.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdoDaily.Location = new System.Drawing.Point(11, 107);
            this.rdoDaily.Name = "rdoDaily";
            this.rdoDaily.Size = new System.Drawing.Size(72, 54);
            this.rdoDaily.TabIndex = 1;
            this.rdoDaily.TabStop = true;
            this.rdoDaily.Text = "Daily";
            this.rdoDaily.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rdoDaily.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.rdoDaily, "Daily");
            this.rdoDaily.UseVisualStyleBackColor = true;
            this.rdoDaily.CheckedChanged += new System.EventHandler(this.rdoButton_CheckedChanged);
            // 
            // rdoWeekly
            // 
            this.rdoWeekly.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoWeekly.Image = ((System.Drawing.Image)(resources.GetObject("rdoWeekly.Image")));
            this.rdoWeekly.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdoWeekly.Location = new System.Drawing.Point(83, 107);
            this.rdoWeekly.Name = "rdoWeekly";
            this.rdoWeekly.Size = new System.Drawing.Size(72, 54);
            this.rdoWeekly.TabIndex = 2;
            this.rdoWeekly.TabStop = true;
            this.rdoWeekly.Text = "Weekly";
            this.rdoWeekly.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rdoWeekly.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.rdoWeekly, "Weekly");
            this.rdoWeekly.UseVisualStyleBackColor = true;
            this.rdoWeekly.CheckedChanged += new System.EventHandler(this.rdoButton_CheckedChanged);
            // 
            // rdoDividends
            // 
            this.rdoDividends.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoDividends.Image = ((System.Drawing.Image)(resources.GetObject("rdoDividends.Image")));
            this.rdoDividends.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdoDividends.Location = new System.Drawing.Point(227, 107);
            this.rdoDividends.Name = "rdoDividends";
            this.rdoDividends.Size = new System.Drawing.Size(72, 54);
            this.rdoDividends.TabIndex = 4;
            this.rdoDividends.TabStop = true;
            this.rdoDividends.Text = "Dividends";
            this.rdoDividends.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rdoDividends.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.rdoDividends, "Dividends");
            this.rdoDividends.UseVisualStyleBackColor = true;
            this.rdoDividends.CheckedChanged += new System.EventHandler(this.rdoButton_CheckedChanged);
            // 
            // rdoMonthly
            // 
            this.rdoMonthly.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoMonthly.Image = ((System.Drawing.Image)(resources.GetObject("rdoMonthly.Image")));
            this.rdoMonthly.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdoMonthly.Location = new System.Drawing.Point(155, 107);
            this.rdoMonthly.Name = "rdoMonthly";
            this.rdoMonthly.Size = new System.Drawing.Size(72, 54);
            this.rdoMonthly.TabIndex = 3;
            this.rdoMonthly.TabStop = true;
            this.rdoMonthly.Text = "Monthly";
            this.rdoMonthly.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rdoMonthly.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.rdoMonthly, "Monthly");
            this.rdoMonthly.UseVisualStyleBackColor = true;
            this.rdoMonthly.CheckedChanged += new System.EventHandler(this.rdoButton_CheckedChanged);
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "dddd, MMMM dd, yyyy";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(89, 36);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(288, 20);
            this.dtpStart.TabIndex = 7;
            this.toolTip1.SetToolTip(this.dtpStart, "Start date");
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "dddd, MMMM dd, yyyy";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(89, 67);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(288, 20);
            this.dtpEnd.TabIndex = 8;
            this.toolTip1.SetToolTip(this.dtpEnd, "End date");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Start Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "End Date";
            // 
            // btnOk
            // 
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOk.Location = new System.Drawing.Point(305, 107);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 54);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "\r\n&Download";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Stock";
            // 
            // ddlStock
            // 
            this.ddlStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStock.FormattingEnabled = true;
            this.ddlStock.Location = new System.Drawing.Point(89, 6);
            this.ddlStock.Name = "ddlStock";
            this.ddlStock.Size = new System.Drawing.Size(288, 21);
            this.ddlStock.TabIndex = 11;
            // 
            // frmYahoo
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 172);
            this.Controls.Add(this.ddlStock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rdoDividends);
            this.Controls.Add(this.rdoMonthly);
            this.Controls.Add(this.rdoWeekly);
            this.Controls.Add(this.rdoDaily);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmYahoo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historical data from Yahoo";
            this.Load += new System.EventHandler(this.frmYahoo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoDaily;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton rdoWeekly;
        private System.Windows.Forms.RadioButton rdoDividends;
        private System.Windows.Forms.RadioButton rdoMonthly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ddlStock;
    }
}