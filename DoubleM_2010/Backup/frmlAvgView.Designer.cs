namespace DoubleM
{
    partial class frmlAvgView
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
            this.dgViewQ = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewQ)).BeginInit();
            this.SuspendLayout();
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
            this.dgViewQ.Location = new System.Drawing.Point(0, 0);
            this.dgViewQ.Name = "dgViewQ";
            this.dgViewQ.ReadOnly = true;
            this.dgViewQ.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgViewQ.Size = new System.Drawing.Size(528, 318);
            this.dgViewQ.TabIndex = 1;
            this.dgViewQ.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewQ_CellDoubleClick);
            this.dgViewQ.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgViewQ_RowsAdded);
            this.dgViewQ.SelectionChanged += new System.EventHandler(this.dgViewQ_SelectionChanged);
            // 
            // frmCumulAvg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 318);
            this.Controls.Add(this.dgViewQ);
            this.Name = "frmCumulAvg";
            this.Text = "Cumulative Average";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCumulAvg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewQ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgViewQ;
    }
}