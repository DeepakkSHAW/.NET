namespace PortfolioManager
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.transactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mutualFundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myCurrentPortfolioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capitalGainLossFIFOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transactionToolStripMenuItem,
            this.mutualFundToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(613, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // transactionToolStripMenuItem
            // 
            this.transactionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.purchaseToolStripMenuItem,
            this.saleToolStripMenuItem,
            this.stockMasterToolStripMenuItem});
            this.transactionToolStripMenuItem.Name = "transactionToolStripMenuItem";
            this.transactionToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.transactionToolStripMenuItem.Text = "Shares";
            // 
            // purchaseToolStripMenuItem
            // 
            this.purchaseToolStripMenuItem.Name = "purchaseToolStripMenuItem";
            this.purchaseToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.purchaseToolStripMenuItem.Text = "Transactions";
            this.purchaseToolStripMenuItem.Click += new System.EventHandler(this.purchaseToolStripMenuItem_Click);
            // 
            // saleToolStripMenuItem
            // 
            this.saleToolStripMenuItem.Name = "saleToolStripMenuItem";
            this.saleToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saleToolStripMenuItem.Text = "Update NSE/BSE Prices";
            this.saleToolStripMenuItem.Click += new System.EventHandler(this.saleToolStripMenuItem_Click);
            // 
            // stockMasterToolStripMenuItem
            // 
            this.stockMasterToolStripMenuItem.Name = "stockMasterToolStripMenuItem";
            this.stockMasterToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.stockMasterToolStripMenuItem.Text = "Stock Master";
            this.stockMasterToolStripMenuItem.Click += new System.EventHandler(this.stockMasterToolStripMenuItem_Click);
            // 
            // mutualFundToolStripMenuItem
            // 
            this.mutualFundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myCurrentPortfolioToolStripMenuItem,
            this.capitalGainLossFIFOToolStripMenuItem});
            this.mutualFundToolStripMenuItem.Name = "mutualFundToolStripMenuItem";
            this.mutualFundToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.mutualFundToolStripMenuItem.Text = "Reports";
            // 
            // myCurrentPortfolioToolStripMenuItem
            // 
            this.myCurrentPortfolioToolStripMenuItem.Name = "myCurrentPortfolioToolStripMenuItem";
            this.myCurrentPortfolioToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.myCurrentPortfolioToolStripMenuItem.Text = "My Current Portfolio";
            this.myCurrentPortfolioToolStripMenuItem.Click += new System.EventHandler(this.myCurrentPortfolioToolStripMenuItem_Click_1);
            // 
            // capitalGainLossFIFOToolStripMenuItem
            // 
            this.capitalGainLossFIFOToolStripMenuItem.Name = "capitalGainLossFIFOToolStripMenuItem";
            this.capitalGainLossFIFOToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.capitalGainLossFIFOToolStripMenuItem.Text = "Capital Gain/Loss FIFO";
            this.capitalGainLossFIFOToolStripMenuItem.Click += new System.EventHandler(this.capitalGainLossFIFOToolStripMenuItem_Click_1);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 445);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainForm";
            this.Text = "Portfolio Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem transactionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mutualFundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myCurrentPortfolioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capitalGainLossFIFOToolStripMenuItem;
    }
}