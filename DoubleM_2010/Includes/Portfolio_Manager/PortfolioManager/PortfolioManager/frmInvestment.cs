using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;

namespace WindowsApplication1
{

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string q, cstr = "provider=microsoft.jet.oledb.4.0;data source=c:/temp/ShareReport.mdb";
        OleDbDataAdapter ad;
        DataSet ds;
        DataSet ds1;
        ReportDocument rep;

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "All")
           {
                q = "select  * from StockPurchase where DateOfPurchase>=#" + dateTimePicker1.Value.ToShortDateString() + "# and DateOfPurchase<=#" + dateTimePicker2.Value.ToShortDateString() + "# and TypeOfTransaction='Buy'";
            }
            else
            {
                q = "select  * from StockPurchase where DateOfPurchase>=#" + dateTimePicker1.Value.ToShortDateString() + "# and DateOfPurchase<=#" + dateTimePicker2.Value.ToShortDateString() + "# and TypeOfTransaction='Buy' and holder='" + comboBox1.Text + "'"; 
            }
            rep = new ReportDocument();
            ds1 = new DataSet();
            ds= new DataSet ();
            ad = new OleDbDataAdapter(q, cstr);
            ad.Fill(ds1, "StockPurchase");
            rep.Load(@"d:/vivek/WindowsApplication1/WindowsApplication1/CrystalReport2.rpt");
            rep.SetDataSource(ds1);
            crystalReportViewer1.ReportSource = rep;
            ad.Fill(ds, "StockPurchase");
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {         
            q = "select distinct holder from StockPurchase";
            OleDbConnection cn;
            OleDbCommand cd;
            cn = new OleDbConnection(cstr);
            cn.Open();
            cd = new OleDbCommand(q, cn);
            OleDbDataReader dr = cd.ExecuteReader();
            comboBox1.Items.Add("All");
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}