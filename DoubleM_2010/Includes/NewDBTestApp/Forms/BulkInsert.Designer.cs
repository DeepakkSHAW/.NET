namespace NewDBTestApp
{
    partial class BulkInsert
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
            this.button1 = new System.Windows.Forms.Button();
            this.stockDataSet1 = new NewDBTestApp.StockDataSet();
            this.qryCatStocksTableAdapter1 = new NewDBTestApp.StockDataSetTableAdapters.QryCatStocksTableAdapter();
            this.tableAdapterManager1 = new NewDBTestApp.StockDataSetTableAdapters.TableAdapterManager();
            this.stockDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Bulk Insert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // stockDataSet1
            // 
            this.stockDataSet1.DataSetName = "StockDataSet";
            this.stockDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qryCatStocksTableAdapter1
            // 
            this.qryCatStocksTableAdapter1.ClearBeforeFill = true;
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.Connection = null;
            this.tableAdapterManager1.UpdateOrder = NewDBTestApp.StockDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // stockDataSet1BindingSource
            // 
            this.stockDataSet1BindingSource.DataSource = this.stockDataSet1;
            this.stockDataSet1BindingSource.Position = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(439, 222);
            this.dataGridView1.TabIndex = 1;
            // 
            // BulkInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 273);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "BulkInsert";
            this.Text = "BulkInsert";
            this.Load += new System.EventHandler(this.BulkInsert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource stockDataSet1BindingSource;
        private StockDataSet stockDataSet1;
        private NewDBTestApp.StockDataSetTableAdapters.QryCatStocksTableAdapter qryCatStocksTableAdapter1;
        private NewDBTestApp.StockDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}