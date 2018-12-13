using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewDBTestApp
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // Add columns to the DataGridView.

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "City";
            dataGridView1.Columns[3].Name = "DOB";
            dataGridView1.Columns[4].Name = "PL";

            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["Name"].HeaderText = "Name";
            dataGridView1.Columns["City"].HeaderText = "City";
            dataGridView1.Columns["DOB"].HeaderText = "DOB";
            dataGridView1.Columns["PL"].HeaderText = "PL";

            // Add rows of data to the DataGridView.

            dataGridView1.Rows.Add(new string[] { "6", "Deepak", "Portland", "1997, 8, 17", "-6.6" });
            dataGridView1.Rows.Add(new string[] { "3", "Saurav", "Anup", "2009, 8, 17", "3.8" });
            dataGridView1.Rows.Add(new string[] { "10", "Supam", "Swiss", "1997, 8, 17", "-10.5" });
            dataGridView1.Rows.Add(new string[] { "9", "Jitendra", "Russia", "2008, 8, 17", "9.8" });
            dataGridView1.Rows.Add(new string[] { "10", "Ive", "Belgium", "1997, 8, 17", "-10.5" });
            dataGridView1.Rows.Add(new string[] { "0", "Rupesh", "Russia", "1997, 8, 17", "0" });

            dataGridView1.Rows.Add(new string[] { "3", "Steev", "Poland", "1997, 8, 17","3.8" });
            dataGridView1.Rows.Add(new string[] { "0", "Ravi", "USA", "1997, 8, 17", "0" });
            dataGridView1.Rows.Add(new string[] { "1", "Jignesh", "USA", "1997, 8, 17", "-1.5" });
            dataGridView1.Rows.Add(new string[] { "15", "Stain", "Belgium", "1980, 8, 17", "15" });
            dataGridView1.Rows.Add(new string[] { "15", "Stain", "Poland", "1997, 8, 17", "15" });
            dataGridView1.Rows.Add(new string[] { "1", "Kristina", "USA", "1997, 8, 17", "1" });

            dataGridView1.AutoResizeColumns();

            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(dataGridView1);
            cs.MaxHeight = 1000;
            cs.Width = 130;
        }
    }
}
