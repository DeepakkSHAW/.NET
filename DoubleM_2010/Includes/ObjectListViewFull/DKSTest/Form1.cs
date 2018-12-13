using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DKSTest
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        bool blnOnce = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!blnOnce)
            {
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("DOB", typeof(DateTime));
                dt.Columns.Add("PL", typeof(double));
                blnOnce = true;
            }
            dt.Rows.Add(new string[] { "6", "Deepak", "Portland", "1997, 8, 17", "-6.6" });
            dt.Rows.Add(new string[] { "3", "Saurav", "Anup", "2009, 8, 17", "3.8" });
            dt.Rows.Add(new string[] { "10", "Supam", "Swiss", "1997, 8, 17", "-10.5" });
            dt.Rows.Add(new string[] { "9", "Jitendra", "Russia", "2008, 8, 17", "9.8" });
            dt.Rows.Add(new string[] { "10", "Ive", "Belgium", "1997, 8, 17", "-10.5" });
            dt.Rows.Add(new string[] { "0", "Rupesh", "Russia", "1997, 8, 17", "0" });
            dt.Rows.Add(new string[] { "3", "Steev", "Poland", "1997, 8, 17", "3.8" });
            dt.Rows.Add(new string[] { "0", "Ravi", "USA", "1997, 8, 17", "0" });
            dt.Rows.Add(new string[] { "1", "Jignesh", "USA", "1997, 8, 17", "-1.5" });
            dt.Rows.Add(new string[] { "15", "Stain", "Belgium", "1980, 8, 17", "15" });
            dt.Rows.Add(new string[] { "15", "Stain", "Poland", "1997, 8, 17", "15" });
            dt.Rows.Add(new string[] { "1", "Kristina", "USA", "1997, 8, 17", "1" });

            // Test with DataTable
            //DataTable personTable = ds.Tables["Person"];
            //this.listViewDataSet.DataSource = personTable;
            this.treeListView1.DataSource = dt.DataSet;

        }
    }
}
