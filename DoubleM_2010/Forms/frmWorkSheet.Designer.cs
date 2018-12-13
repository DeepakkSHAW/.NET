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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer1 = new System.Windows.Forms.TreeListViewItemCollection.TreeListViewItemCollectionComparer();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkSheet));
            this.trlvWorkSheet = new System.Windows.Forms.TreeListView();
            this.contextMenuStrip_WorkSheet = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuExpAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuColAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnutxtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNewTrad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_WorkSheet.SuspendLayout();
            this.SuspendLayout();
            // 
            // trlvWorkSheet
            // 
            treeListViewItemCollectionComparer1.Column = 0;
            treeListViewItemCollectionComparer1.SortOrder = System.Windows.Forms.SortOrder.None;
            this.trlvWorkSheet.Comparer = treeListViewItemCollectionComparer1;
            this.trlvWorkSheet.ContextMenuStrip = this.contextMenuStrip_WorkSheet;
            this.trlvWorkSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trlvWorkSheet.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.trlvWorkSheet.HideSelection = false;
            this.trlvWorkSheet.Location = new System.Drawing.Point(0, 0);
            this.trlvWorkSheet.Name = "trlvWorkSheet";
            this.trlvWorkSheet.Size = new System.Drawing.Size(333, 326);
            this.trlvWorkSheet.SmallImageList = this.imageList1;
            this.trlvWorkSheet.Sorting = System.Windows.Forms.SortOrder.None;
            this.trlvWorkSheet.TabIndex = 2;
            this.trlvWorkSheet.UseCompatibleStateImageBehavior = false;
            this.trlvWorkSheet.BeforeExpand += new System.Windows.Forms.TreeListViewCancelEventHandler(this.trlvWorkSheet_BeforeExpand);
            this.trlvWorkSheet.BeforeCollapse += new System.Windows.Forms.TreeListViewCancelEventHandler(this.trlvWorkSheet_BeforeCollapse);
            // 
            // contextMenuStrip_WorkSheet
            // 
            this.contextMenuStrip_WorkSheet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExpAll,
            this.mnuColAll,
            this.mnutxtSearch,
            this.toolStripMenuItem3,
            this.mnuNewTrad,
            this.toolStripSeparator1,
            this.mnuRefresh});
            this.contextMenuStrip_WorkSheet.Name = "contextMenuStrip1";
            this.contextMenuStrip_WorkSheet.ShowImageMargin = false;
            this.contextMenuStrip_WorkSheet.Size = new System.Drawing.Size(136, 149);
            this.contextMenuStrip_WorkSheet.Text = "Work Sheet contextMenu";
            // 
            // mnuExpAll
            // 
            this.mnuExpAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuExpAll.Name = "mnuExpAll";
            this.mnuExpAll.Size = new System.Drawing.Size(135, 22);
            this.mnuExpAll.Text = "Expand All";
            this.mnuExpAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mnuExpAll.Click += new System.EventHandler(this.mnuExpAll_Click);
            // 
            // mnuColAll
            // 
            this.mnuColAll.Name = "mnuColAll";
            this.mnuColAll.Size = new System.Drawing.Size(135, 22);
            this.mnuColAll.Text = "Collapse All";
            this.mnuColAll.Click += new System.EventHandler(this.mnuColAll_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(132, 6);
            // 
            // mnutxtSearch
            // 
            this.mnutxtSearch.AutoToolTip = true;
            this.mnutxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mnutxtSearch.Name = "mnutxtSearch";
            this.mnutxtSearch.Size = new System.Drawing.Size(100, 21);
            this.mnutxtSearch.ToolTipText = "Type to Search";
            this.mnutxtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mnutxtSearch_KeyUp);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "4.ico");
            this.imageList1.Images.SetKeyName(1, "briefcase.ico");
            this.imageList1.Images.SetKeyName(2, "12-Application.ico");
            this.imageList1.Images.SetKeyName(3, "1499.ico");
            this.imageList1.Images.SetKeyName(4, "Be Script.ico");
            this.imageList1.Images.SetKeyName(5, "183.ICO");
            this.imageList1.Images.SetKeyName(6, "Net Service.ico");
            this.imageList1.Images.SetKeyName(7, "WAV.ico");
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 10;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
            // 
            // mnuNewTrad
            // 
            this.mnuNewTrad.Name = "mnuNewTrad";
            this.mnuNewTrad.Size = new System.Drawing.Size(135, 22);
            this.mnuNewTrad.Text = "New Trad";
            this.mnuNewTrad.Click += new System.EventHandler(this.mnuNewTrad_Click);
            // 
            // mnuRefresh
            // 
            this.mnuRefresh.Name = "mnuRefresh";
            this.mnuRefresh.Size = new System.Drawing.Size(135, 22);
            this.mnuRefresh.Text = "Refresh";
            this.mnuRefresh.ToolTipText = "Refresh Work sheet";
            this.mnuRefresh.Click += new System.EventHandler(this.mnuRefresh_Click);
            // 
            // frmWorkSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 326);
            this.Controls.Add(this.trlvWorkSheet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWorkSheet";
            this.Text = "DoubleM Wosk Sheet";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBase_Load);
            this.contextMenuStrip_WorkSheet.ResumeLayout(false);
            this.contextMenuStrip_WorkSheet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeListView trlvWorkSheet;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_WorkSheet;
        private System.Windows.Forms.ToolStripMenuItem mnuExpAll;
        private System.Windows.Forms.ToolStripMenuItem mnuColAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripTextBox mnutxtSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuNewTrad;
        private System.Windows.Forms.ToolStripMenuItem mnuRefresh;
    }
}