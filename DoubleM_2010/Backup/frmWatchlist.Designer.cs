namespace DoubleM
{
    partial class frmWatchlist
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgWList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgWList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgWList
            // 
            this.dgWList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgWList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgWList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgWList.GridColor = System.Drawing.Color.DarkOrange;
            this.dgWList.Location = new System.Drawing.Point(0, 0);
            this.dgWList.Name = "dgWList";
            this.dgWList.ReadOnly = true;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Gold;
            this.dgWList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgWList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgWList.Size = new System.Drawing.Size(601, 432);
            this.dgWList.TabIndex = 2;
            this.dgWList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgWList_RowsAdded);
            // 
            // frmWatchlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 432);
            this.Controls.Add(this.dgWList);
            this.Name = "frmWatchlist";
            this.Text = "Stock Watch List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmWatchlist_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgWList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgWList;



    }
}