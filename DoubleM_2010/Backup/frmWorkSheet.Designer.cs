namespace DoubleM
{
    partial class frmWorkSheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkSheet));
            this.lstvWS = new System.Windows.Forms.ListView();
            this.tbctrWS = new System.Windows.Forms.TabControl();
            this.tpView = new System.Windows.Forms.TabPage();
            this.tpBuy = new System.Windows.Forms.TabPage();
            this.tpSell = new System.Windows.Forms.TabPage();
            this.tbctrWS.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstvWS
            // 
            this.lstvWS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvWS.Location = new System.Drawing.Point(2, 1);
            this.lstvWS.Name = "lstvWS";
            this.lstvWS.Size = new System.Drawing.Size(651, 376);
            this.lstvWS.TabIndex = 0;
            this.lstvWS.UseCompatibleStateImageBehavior = false;
            // 
            // tbctrWS
            // 
            this.tbctrWS.Controls.Add(this.tpView);
            this.tbctrWS.Controls.Add(this.tpBuy);
            this.tbctrWS.Controls.Add(this.tpSell);
            this.tbctrWS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbctrWS.Location = new System.Drawing.Point(0, 384);
            this.tbctrWS.Name = "tbctrWS";
            this.tbctrWS.SelectedIndex = 0;
            this.tbctrWS.Size = new System.Drawing.Size(653, 116);
            this.tbctrWS.TabIndex = 1;
            // 
            // tpView
            // 
            this.tpView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpView.Location = new System.Drawing.Point(4, 22);
            this.tpView.Name = "tpView";
            this.tpView.Padding = new System.Windows.Forms.Padding(3);
            this.tpView.Size = new System.Drawing.Size(645, 90);
            this.tpView.TabIndex = 0;
            this.tpView.Text = "View";
            this.tpView.UseVisualStyleBackColor = true;
            // 
            // tpBuy
            // 
            this.tpBuy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpBuy.Location = new System.Drawing.Point(4, 22);
            this.tpBuy.Name = "tpBuy";
            this.tpBuy.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuy.Size = new System.Drawing.Size(645, 90);
            this.tpBuy.TabIndex = 1;
            this.tpBuy.Text = "Buy";
            this.tpBuy.UseVisualStyleBackColor = true;
            // 
            // tpSell
            // 
            this.tpSell.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpSell.Location = new System.Drawing.Point(4, 22);
            this.tpSell.Name = "tpSell";
            this.tpSell.Padding = new System.Windows.Forms.Padding(3);
            this.tpSell.Size = new System.Drawing.Size(645, 90);
            this.tpSell.TabIndex = 2;
            this.tpSell.Text = "Sell";
            this.tpSell.UseVisualStyleBackColor = true;
            // 
            // frmWorkSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 500);
            this.Controls.Add(this.tbctrWS);
            this.Controls.Add(this.lstvWS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWorkSheet";
            this.Text = "DoubleM Wosk Sheet";
            this.Load += new System.EventHandler(this.frmBase_Load);
            this.tbctrWS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstvWS;
        private System.Windows.Forms.TabControl tbctrWS;
        private System.Windows.Forms.TabPage tpView;
        private System.Windows.Forms.TabPage tpBuy;
        private System.Windows.Forms.TabPage tpSell;
    }
}