namespace DoubleM
{
    partial class frmReadBSE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReadBSE));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wbBSE = new System.Windows.Forms.WebBrowser();
            this.rtbBSE = new System.Windows.Forms.RichTextBox();
            this.tmrBSE = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnScripts = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbtnAllScript = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnActiveScript = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnInactiveScript = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_UpdateAllfromBSE = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_UpdatefromBSE = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_RemoveRate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_AddDB = new System.Windows.Forms.ToolStripButton();
            this.dgViewQ = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewQ)).BeginInit();
            this.SuspendLayout();
            // 
            // wbBSE
            // 
            this.wbBSE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wbBSE.Location = new System.Drawing.Point(12, 333);
            this.wbBSE.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbBSE.Name = "wbBSE";
            this.wbBSE.Size = new System.Drawing.Size(171, 53);
            this.wbBSE.TabIndex = 3;
            this.wbBSE.Visible = false;
            this.wbBSE.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbBSE_DocumentCompleted);
            // 
            // rtbBSE
            // 
            this.rtbBSE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbBSE.Location = new System.Drawing.Point(216, 333);
            this.rtbBSE.Name = "rtbBSE";
            this.rtbBSE.Size = new System.Drawing.Size(171, 56);
            this.rtbBSE.TabIndex = 4;
            this.rtbBSE.Text = "";
            this.rtbBSE.Visible = false;
            // 
            // tmrBSE
            // 
            this.tmrBSE.Interval = 500;
            this.tmrBSE.Tick += new System.EventHandler(this.tmrBSE_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnScripts,
            this.toolStripSeparator2,
            this.toolStripButton_UpdateAllfromBSE,
            this.toolStripButton_UpdatefromBSE,
            this.toolStripButton_RemoveRate,
            this.toolStripSeparator1,
            this.toolStripButton_AddDB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(733, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripBSE_ItemClicked);
            // 
            // tsbtnScripts
            // 
            this.tsbtnScripts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAllScript,
            this.tsbtnActiveScript,
            this.tsbtnInactiveScript});
            this.tsbtnScripts.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnScripts.Image")));
            this.tsbtnScripts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnScripts.Name = "tsbtnScripts";
            this.tsbtnScripts.Size = new System.Drawing.Size(90, 22);
            this.tsbtnScripts.Text = "Script Type";
            // 
            // tsbtnAllScript
            // 
            this.tsbtnAllScript.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAllScript.Image")));
            this.tsbtnAllScript.Name = "tsbtnAllScript";
            this.tsbtnAllScript.Size = new System.Drawing.Size(154, 22);
            this.tsbtnAllScript.Text = "All Script";
            this.tsbtnAllScript.ToolTipText = "Include All Scripts";
            this.tsbtnAllScript.CheckedChanged += new System.EventHandler(this.tsbtnScriptAction_CheckedChanged);
            this.tsbtnAllScript.Click += new System.EventHandler(this.tsbtnScriptAction_Click);
            // 
            // tsbtnActiveScript
            // 
            this.tsbtnActiveScript.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnActiveScript.Image")));
            this.tsbtnActiveScript.Name = "tsbtnActiveScript";
            this.tsbtnActiveScript.Size = new System.Drawing.Size(154, 22);
            this.tsbtnActiveScript.Text = "Active Script";
            this.tsbtnActiveScript.ToolTipText = "Include Active Scripts Only";
            this.tsbtnActiveScript.CheckedChanged += new System.EventHandler(this.tsbtnScriptAction_CheckedChanged);
            this.tsbtnActiveScript.Click += new System.EventHandler(this.tsbtnScriptAction_Click);
            // 
            // tsbtnInactiveScript
            // 
            this.tsbtnInactiveScript.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnInactiveScript.Image")));
            this.tsbtnInactiveScript.Name = "tsbtnInactiveScript";
            this.tsbtnInactiveScript.Size = new System.Drawing.Size(154, 22);
            this.tsbtnInactiveScript.Text = "Inactive Script";
            this.tsbtnInactiveScript.ToolTipText = "Include Inactive Scripts Only";
            this.tsbtnInactiveScript.CheckedChanged += new System.EventHandler(this.tsbtnScriptAction_CheckedChanged);
            this.tsbtnInactiveScript.Click += new System.EventHandler(this.tsbtnScriptAction_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_UpdateAllfromBSE
            // 
            this.toolStripButton_UpdateAllfromBSE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_UpdateAllfromBSE.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_UpdateAllfromBSE.Image")));
            this.toolStripButton_UpdateAllfromBSE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_UpdateAllfromBSE.Name = "toolStripButton_UpdateAllfromBSE";
            this.toolStripButton_UpdateAllfromBSE.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_UpdateAllfromBSE.Text = "Updated from BSE";
            this.toolStripButton_UpdateAllfromBSE.ToolTipText = "Download the latest Rate from BSE";
            // 
            // toolStripButton_UpdatefromBSE
            // 
            this.toolStripButton_UpdatefromBSE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_UpdatefromBSE.Image = global::DoubleM.Properties.Resources.BookmarkNext;
            this.toolStripButton_UpdatefromBSE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_UpdatefromBSE.Name = "toolStripButton_UpdatefromBSE";
            this.toolStripButton_UpdatefromBSE.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_UpdatefromBSE.Text = "Get latest rate";
            this.toolStripButton_UpdatefromBSE.ToolTipText = "Latest Rate of selected Rows from BSE";
            // 
            // toolStripButton_RemoveRate
            // 
            this.toolStripButton_RemoveRate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_RemoveRate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_RemoveRate.Image")));
            this.toolStripButton_RemoveRate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_RemoveRate.Name = "toolStripButton_RemoveRate";
            this.toolStripButton_RemoveRate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_RemoveRate.Text = "RemoveRate";
            this.toolStripButton_RemoveRate.ToolTipText = "Clear the latest rate";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_AddDB
            // 
            this.toolStripButton_AddDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_AddDB.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_AddDB.Image")));
            this.toolStripButton_AddDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_AddDB.Name = "toolStripButton_AddDB";
            this.toolStripButton_AddDB.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_AddDB.Text = "Save to Database";
            this.toolStripButton_AddDB.ToolTipText = "Update database with latest rates";
            // 
            // dgViewQ
            // 
            this.dgViewQ.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Thistle;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgViewQ.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgViewQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgViewQ.GridColor = System.Drawing.Color.Violet;
            this.dgViewQ.Location = new System.Drawing.Point(0, 25);
            this.dgViewQ.Name = "dgViewQ";
            this.dgViewQ.ReadOnly = true;
            this.dgViewQ.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgViewQ.Size = new System.Drawing.Size(733, 388);
            this.dgViewQ.TabIndex = 23;
            this.dgViewQ.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewQ_CellValueChanged);
            this.dgViewQ.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewQ_CellDoubleClick);
            this.dgViewQ.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgViewQ_RowsAdded);
            this.dgViewQ.SelectionChanged += new System.EventHandler(this.dgViewQ_SelectionChanged);
            // 
            // frmReadBSE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 413);
            this.Controls.Add(this.wbBSE);
            this.Controls.Add(this.rtbBSE);
            this.Controls.Add(this.dgViewQ);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReadBSE";
            this.Text = "BSE Direct";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReadBSE_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewQ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbBSE;
        private System.Windows.Forms.RichTextBox rtbBSE;
        private System.Windows.Forms.Timer tmrBSE;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_AddDB;
        private System.Windows.Forms.ToolStripButton toolStripButton_UpdatefromBSE;
        private System.Windows.Forms.ToolStripButton toolStripButton_RemoveRate;
        private System.Windows.Forms.DataGridView dgViewQ;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnScripts;
        private System.Windows.Forms.ToolStripMenuItem tsbtnAllScript;
        private System.Windows.Forms.ToolStripMenuItem tsbtnActiveScript;
        private System.Windows.Forms.ToolStripMenuItem tsbtnInactiveScript;
        private System.Windows.Forms.ToolStripButton toolStripButton_UpdateAllfromBSE;
    }
}