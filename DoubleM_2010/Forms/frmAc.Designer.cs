namespace DoubleM
{
    partial class frmAc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAc));
            this.dgvAc = new System.Windows.Forms.DataGridView();
            this.tltpAC = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAc)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAc
            // 
            this.dgvAc.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PowderBlue;
            this.dgvAc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAc.GridColor = System.Drawing.SystemColors.HotTrack;
            this.dgvAc.Location = new System.Drawing.Point(0, 0);
            this.dgvAc.Name = "dgvAc";
            this.dgvAc.ReadOnly = true;
            this.dgvAc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAc.Size = new System.Drawing.Size(525, 321);
            this.dgvAc.TabIndex = 3;
            this.dgvAc.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAc_CellFormatting);
            this.dgvAc.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvAc_DataError);
            this.dgvAc.SelectionChanged += new System.EventHandler(this.dgvAc_SelectionChanged);
            // 
            // frmAc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 321);
            this.Controls.Add(this.dgvAc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Accounting - Profit & Loss";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAc_Load);
            this.Activated += new System.EventHandler(this.frmAc_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAc_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAc;
        private System.Windows.Forms.ToolTip tltpAC;




    }
}