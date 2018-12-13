namespace DoubleM
{
    partial class frmTrendGr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrendGr));
            this.grBoxSimple = new System.Windows.Forms.GroupBox();
            this.chkSym = new System.Windows.Forms.CheckBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.ddlStock = new System.Windows.Forms.ComboBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.chkSmooth = new System.Windows.Forms.CheckBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.grBoxSimple.SuspendLayout();
            this.SuspendLayout();
            // 
            // grBoxSimple
            // 
            this.grBoxSimple.Controls.Add(this.chkSym);
            this.grBoxSimple.Controls.Add(this.btnRemove);
            this.grBoxSimple.Controls.Add(this.ddlStock);
            this.grBoxSimple.Controls.Add(this.lblStock);
            this.grBoxSimple.Controls.Add(this.chkSmooth);
            this.grBoxSimple.Controls.Add(this.btnShow);
            this.grBoxSimple.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grBoxSimple.Location = new System.Drawing.Point(0, 576);
            this.grBoxSimple.Name = "grBoxSimple";
            this.grBoxSimple.Size = new System.Drawing.Size(810, 68);
            this.grBoxSimple.TabIndex = 0;
            this.grBoxSimple.TabStop = false;
            // 
            // chkSym
            // 
            this.chkSym.AutoSize = true;
            this.chkSym.Location = new System.Drawing.Point(118, 46);
            this.chkSym.Name = "chkSym";
            this.chkSym.Size = new System.Drawing.Size(90, 17);
            this.chkSym.TabIndex = 4;
            this.chkSym.Text = "Show Symbol";
            this.chkSym.UseVisualStyleBackColor = true;
            this.chkSym.CheckedChanged += new System.EventHandler(this.chkSym_CheckedChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(354, 40);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(119, 21);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "&Remove Graph";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // ddlStock
            // 
            this.ddlStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStock.FormattingEnabled = true;
            this.ddlStock.Location = new System.Drawing.Point(118, 19);
            this.ddlStock.MaxDropDownItems = 25;
            this.ddlStock.Name = "ddlStock";
            this.ddlStock.Size = new System.Drawing.Size(219, 21);
            this.ddlStock.TabIndex = 0;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(10, 25);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(102, 13);
            this.lblStock.TabIndex = 5;
            this.lblStock.Text = "Stock Add/Remove";
            // 
            // chkSmooth
            // 
            this.chkSmooth.AutoSize = true;
            this.chkSmooth.Location = new System.Drawing.Point(13, 46);
            this.chkSmooth.Name = "chkSmooth";
            this.chkSmooth.Size = new System.Drawing.Size(94, 17);
            this.chkSmooth.TabIndex = 3;
            this.chkSmooth.Text = "Smooth Graph";
            this.chkSmooth.UseVisualStyleBackColor = true;
            this.chkSmooth.CheckedChanged += new System.EventHandler(this.chkSmooth_CheckedChanged);
            // 
            // btnShow
            // 
            this.btnShow.Image = ((System.Drawing.Image)(resources.GetObject("btnShow.Image")));
            this.btnShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShow.Location = new System.Drawing.Point(354, 19);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(119, 21);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "&Show Graph";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // zg1
            // 
            this.zg1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.zg1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.zg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zg1.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg1.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.zg1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.zg1.IsAutoScrollRange = false;
            this.zg1.IsEnableHEdit = false;
            this.zg1.IsEnableHPan = true;
            this.zg1.IsEnableHZoom = true;
            this.zg1.IsEnableVEdit = false;
            this.zg1.IsEnableVPan = true;
            this.zg1.IsEnableVZoom = true;
            this.zg1.IsPrintFillPage = true;
            this.zg1.IsPrintKeepAspectRatio = true;
            this.zg1.IsScrollY2 = false;
            this.zg1.IsShowContextMenu = true;
            this.zg1.IsShowCopyMessage = true;
            this.zg1.IsShowCursorValues = false;
            this.zg1.IsShowHScrollBar = false;
            this.zg1.IsShowPointValues = false;
            this.zg1.IsShowVScrollBar = false;
            this.zg1.IsSynchronizeXAxes = false;
            this.zg1.IsSynchronizeYAxes = false;
            this.zg1.IsZoomOnMouseCenter = false;
            this.zg1.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg1.LinkModifierKeys = System.Windows.Forms.Keys.None;
            this.zg1.Location = new System.Drawing.Point(0, 0);
            this.zg1.Name = "zg1";
            this.zg1.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg1.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zg1.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zg1.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zg1.PointDateFormat = "g";
            this.zg1.PointValueFormat = "G";
            this.zg1.ScrollMaxX = 0;
            this.zg1.ScrollMaxY = 0;
            this.zg1.ScrollMaxY2 = 0;
            this.zg1.ScrollMinX = 0;
            this.zg1.ScrollMinY = 0;
            this.zg1.ScrollMinY2 = 0;
            this.zg1.Size = new System.Drawing.Size(810, 576);
            this.zg1.TabIndex = 1;
            this.zg1.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zg1.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zg1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zg1.ZoomStepFraction = 0.1;
            // 
            // frmSimpleGr
            // 
            this.AcceptButton = this.btnShow;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnRemove;
            this.ClientSize = new System.Drawing.Size(810, 644);
            this.Controls.Add(this.zg1);
            this.Controls.Add(this.grBoxSimple);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSimpleGr";
            this.Text = "Simple Graph";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSimpleGr_Load);
            this.grBoxSimple.ResumeLayout(false);
            this.grBoxSimple.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grBoxSimple;
        private ZedGraph.ZedGraphControl zg1;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.CheckBox chkSmooth;
        private System.Windows.Forms.ComboBox ddlStock;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.CheckBox chkSym;

    }
}