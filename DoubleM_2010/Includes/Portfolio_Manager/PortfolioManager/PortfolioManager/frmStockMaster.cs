using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PortfolioManager
{
    public partial class frmStockMaster : Form
    {
        public frmStockMaster()
        {
            InitializeComponent();
        }
        OleDbConnection conn;
        OleDbCommand cmd;

        private void frmStockMaster_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection();
             ClosingStock cs;

                cs = new ClosingStock();
                conn = cs.GetConnection();
                conn.Open();
                OleDbDataAdapter da;
                da = new OleDbDataAdapter("select * from scriptmaster", conn);
                DataSet ds;
                ds = new DataSet();
                da.Fill(ds, "Tab1");
                dataGridView1.DataSource = ds.Tables[0];
                
                OleDbDataAdapter Da1;
                Da1 = new OleDbDataAdapter("select distinct sector from scriptmaster", conn);
                Da1.Fill(ds,"Script");
                DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();

                cboSector.DataSource = ds.Tables["script"];
                cboSector.ValueMember = "sector";
                cboSector.DisplayMember = "sector";
            


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();

           cmd.Connection = conn;
           
           if (lblScriptId.ToString().Length > 0)
           {
               cmd.CommandText = "update scriptmaster set sector='" + cboSector.Text + "' where scriptcode='" + lblScriptId.Text + "'";
           }
           cmd.ExecuteNonQuery();
           MessageBox.Show("Record has been updated successfully","Done",MessageBoxButtons.OK);
           groupBox2.Enabled = false   ;
           dataGridView1.CurrentRow.Cells[1].Value = txtScriptName.Text;
           dataGridView1.CurrentRow.Cells[2].Value = cboSector.Text;
           txtScriptName.Text = string.Empty ;
           cboSector.Text = string.Empty;
           

            

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cboSector.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtScriptName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            lblScriptId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            groupBox2.Enabled = true;
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridView1, e.X, e.Y);

            }
        }
    }
}