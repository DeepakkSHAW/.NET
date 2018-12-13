namespace NewDBTestApp
{
    partial class Form6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form6));
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.treeListView = new BrightIdeasSoftware.TreeListView();
            this.treeColumnName = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnCreated = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnModified = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnSize = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnFileType = new BrightIdeasSoftware.OLVColumn();
            this.treeColumnAttributes = new BrightIdeasSoftware.OLVColumn();
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn2 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn3 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn4 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn5 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn6 = new BrightIdeasSoftware.OLVColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "compass");
            this.imageList1.Images.SetKeyName(1, "down");
            this.imageList1.Images.SetKeyName(2, "user");
            this.imageList1.Images.SetKeyName(3, "find");
            this.imageList1.Images.SetKeyName(4, "folder");
            this.imageList1.Images.SetKeyName(5, "movie");
            this.imageList1.Images.SetKeyName(6, "music");
            this.imageList1.Images.SetKeyName(7, "no");
            this.imageList1.Images.SetKeyName(8, "readonly");
            this.imageList1.Images.SetKeyName(9, "public");
            this.imageList1.Images.SetKeyName(10, "recycle");
            this.imageList1.Images.SetKeyName(11, "spanner");
            this.imageList1.Images.SetKeyName(12, "star");
            this.imageList1.Images.SetKeyName(13, "tick");
            this.imageList1.Images.SetKeyName(14, "archive");
            this.imageList1.Images.SetKeyName(15, "system");
            this.imageList1.Images.SetKeyName(16, "hidden");
            this.imageList1.Images.SetKeyName(17, "temporary");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "user");
            this.imageList2.Images.SetKeyName(1, "compass");
            this.imageList2.Images.SetKeyName(2, "down");
            this.imageList2.Images.SetKeyName(3, "find");
            this.imageList2.Images.SetKeyName(4, "folder");
            this.imageList2.Images.SetKeyName(5, "movie");
            this.imageList2.Images.SetKeyName(6, "music");
            this.imageList2.Images.SetKeyName(7, "no");
            this.imageList2.Images.SetKeyName(8, "readonly");
            this.imageList2.Images.SetKeyName(9, "public");
            this.imageList2.Images.SetKeyName(10, "recycle");
            this.imageList2.Images.SetKeyName(11, "spanner");
            this.imageList2.Images.SetKeyName(12, "star");
            this.imageList2.Images.SetKeyName(13, "tick");
            this.imageList2.Images.SetKeyName(14, "archive");
            this.imageList2.Images.SetKeyName(15, "system");
            this.imageList2.Images.SetKeyName(16, "hidden");
            this.imageList2.Images.SetKeyName(17, "temporary");
            // 
            // treeListView
            // 
            this.treeListView.AllColumns.Add(this.treeColumnName);
            this.treeListView.AllColumns.Add(this.treeColumnCreated);
            this.treeListView.AllColumns.Add(this.treeColumnModified);
            this.treeListView.AllColumns.Add(this.treeColumnSize);
            this.treeListView.AllColumns.Add(this.treeColumnFileType);
            this.treeListView.AllColumns.Add(this.treeColumnAttributes);
            this.treeListView.AllowColumnReorder = true;
            this.treeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView.CheckBoxes = true;
            this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.treeColumnName,
            this.treeColumnCreated,
            this.treeColumnModified,
            this.treeColumnSize,
            this.treeColumnFileType,
            this.treeColumnAttributes});
            this.treeListView.EmptyListMsg = "This folder is completely empty!";
            this.treeListView.EmptyListMsgFont = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.HideSelection = false;
            this.treeListView.ItemRenderer = null;
            this.treeListView.Location = new System.Drawing.Point(12, 0);
            this.treeListView.Name = "treeListView";
            this.treeListView.OwnerDraw = true;
            this.treeListView.ShowGroups = false;
            this.treeListView.Size = new System.Drawing.Size(698, 100);
            this.treeListView.TabIndex = 14;
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.UseHotItem = true;
            this.treeListView.View = System.Windows.Forms.View.Details;
            this.treeListView.VirtualMode = true;
            // 
            // treeColumnName
            // 
            this.treeColumnName.AspectName = "Name";
            this.treeColumnName.IsTileViewColumn = true;
            this.treeColumnName.Text = "Name";
            this.treeColumnName.UseInitialLetterForGroup = true;
            this.treeColumnName.Width = 180;
            // 
            // treeColumnCreated
            // 
            this.treeColumnCreated.AspectName = "CreationTime";
            this.treeColumnCreated.DisplayIndex = 4;
            this.treeColumnCreated.Text = "Created";
            this.treeColumnCreated.Width = 131;
            // 
            // treeColumnModified
            // 
            this.treeColumnModified.AspectName = "LastWriteTime";
            this.treeColumnModified.DisplayIndex = 1;
            this.treeColumnModified.IsTileViewColumn = true;
            this.treeColumnModified.Text = "Modified";
            this.treeColumnModified.Width = 145;
            // 
            // treeColumnSize
            // 
            this.treeColumnSize.AspectName = "Extension";
            this.treeColumnSize.DisplayIndex = 2;
            this.treeColumnSize.Text = "Size";
            this.treeColumnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.treeColumnSize.Width = 80;
            // 
            // treeColumnFileType
            // 
            this.treeColumnFileType.DisplayIndex = 3;
            this.treeColumnFileType.IsTileViewColumn = true;
            this.treeColumnFileType.Text = "File Type";
            this.treeColumnFileType.Width = 148;
            // 
            // treeColumnAttributes
            // 
            this.treeColumnAttributes.FillsFreeSpace = true;
            this.treeColumnAttributes.IsEditable = false;
            this.treeColumnAttributes.MinimumWidth = 20;
            this.treeColumnAttributes.Text = "Attributes";
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumn1);
            this.treeListView1.AllColumns.Add(this.olvColumn2);
            this.treeListView1.AllColumns.Add(this.olvColumn3);
            this.treeListView1.AllColumns.Add(this.olvColumn4);
            this.treeListView1.AllColumns.Add(this.olvColumn5);
            this.treeListView1.AllColumns.Add(this.olvColumn6);
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6});
            this.treeListView1.ItemRenderer = null;
            this.treeListView1.Location = new System.Drawing.Point(12, 116);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(689, 119);
            this.treeListView1.TabIndex = 15;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.Width = 100;
            // 
            // olvColumn2
            // 
            this.olvColumn2.Width = 100;
            // 
            // olvColumn3
            // 
            this.olvColumn3.Width = 100;
            // 
            // olvColumn4
            // 
            this.olvColumn4.Width = 100;
            // 
            // olvColumn5
            // 
            this.olvColumn5.Width = 100;
            // 
            // olvColumn6
            // 
            this.olvColumn6.Width = 100;
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 273);
            this.Controls.Add(this.treeListView1);
            this.Controls.Add(this.treeListView);
            this.Controls.Add(this.button1);
            this.Name = "Form6";
            this.Text = "Form6";
            this.Load += new System.EventHandler(this.Form6_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private BrightIdeasSoftware.TreeListView treeListView;
        private BrightIdeasSoftware.OLVColumn treeColumnName;
        private BrightIdeasSoftware.OLVColumn treeColumnCreated;
        private BrightIdeasSoftware.OLVColumn treeColumnModified;
        private BrightIdeasSoftware.OLVColumn treeColumnSize;
        private BrightIdeasSoftware.OLVColumn treeColumnFileType;
        private BrightIdeasSoftware.OLVColumn treeColumnAttributes;
        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
    }
}