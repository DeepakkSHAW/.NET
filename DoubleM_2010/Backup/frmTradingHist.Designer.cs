namespace DoubleM
{
    partial class frmTradingHist
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTrading = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrading)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTrading
            // 
            this.dgvTrading.AllowUserToAddRows = false;
            this.dgvTrading.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PowderBlue;
            this.dgvTrading.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTrading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTrading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrading.GridColor = System.Drawing.SystemColors.HotTrack;
            this.dgvTrading.Location = new System.Drawing.Point(0, 0);
            this.dgvTrading.Name = "dgvTrading";
            this.dgvTrading.ReadOnly = true;
            this.dgvTrading.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrading.Size = new System.Drawing.Size(834, 340);
            this.dgvTrading.TabIndex = 2;
            // 
            // frmTradings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 340);
            this.Controls.Add(this.dgvTrading);
            this.Name = "frmTradings";
            this.Text = "Trading";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTradings_FormClosing);
            this.Load += new System.EventHandler(this.frmTradings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTrading;
    }
}