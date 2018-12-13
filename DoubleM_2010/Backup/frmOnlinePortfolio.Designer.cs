namespace DoubleM
{
    partial class frmOnlinePortfolio
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
            this.dgOnlinePF = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgOnlinePF)).BeginInit();
            this.SuspendLayout();
            // 
            // dgOnlinePF
            // 
            this.dgOnlinePF.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Thistle;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgOnlinePF.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgOnlinePF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOnlinePF.GridColor = System.Drawing.Color.Violet;
            this.dgOnlinePF.Location = new System.Drawing.Point(0, 0);
            this.dgOnlinePF.Name = "dgOnlinePF";
            this.dgOnlinePF.ReadOnly = true;
            this.dgOnlinePF.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOnlinePF.Size = new System.Drawing.Size(636, 391);
            this.dgOnlinePF.TabIndex = 2;
            this.dgOnlinePF.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgOnlinePF_RowsAdded);
            this.dgOnlinePF.SelectionChanged += new System.EventHandler(this.dgOnlinePF_SelectionChanged);
            // 
            // frmOnlinePortfolio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 391);
            this.Controls.Add(this.dgOnlinePF);
            this.Name = "frmOnlinePortfolio";
            this.Text = "Online Portfolio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOnlinePortfolio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgOnlinePF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgOnlinePF;



    }
}