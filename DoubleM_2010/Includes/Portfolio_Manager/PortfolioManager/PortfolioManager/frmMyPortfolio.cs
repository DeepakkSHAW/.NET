using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

 
namespace PortfolioManager
{
    public partial class frmMyPortfolio : Form
    {
        public frmMyPortfolio()
        {
            InitializeComponent();
        }
OleDbConnection conn1;
        private void frmMyPortfolio_Load(object sender, EventArgs e)
        {
            ClosingStock cm;
            cm = new ClosingStock();
            conn1 = cm.GetConnection();

            //conn1 = new OleDbConnection();
            string txtconn = "provider=microsoft.jet.oledb.4.0;data source=c:\\temp\\sharereport.mdb";
            conn1.ConnectionString = txtconn;

            try
            {
                conn1.Open();
 OleDbCommand cmd;
                cmd = new OleDbCommand();
                cmd.CommandText = "select distinct holder from stockpurchase order by holder";
                cmd.Connection = conn1;
                OleDbDataReader dr;
                dr = cmd.ExecuteReader();

                cboHolder1.Items.Add("All");
                cboHolder1.Text = "All";
                while (dr.Read())
                {
                    cboHolder1.Items.Add(dr["holder"].ToString());
                }
                cboHolder1.Text = "All";
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }
        DataSet Ds;
        DataView dv1;
        
  private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn1.Open();
                OleDbDataAdapter da;
                string sqlString;
               // string txtHolder;
                


                if (cboHolder1.Text == "All")
                {
                    sqlString = "SELECT ScriptMaster.ScriptCode, ScriptMaster.ScriptName, Market, ScriptMaster.Sector, typeofscript,  ((select sum(puramt) from purvalue where scriptcode = scriptmaster.scriptcode)/(select sum( qty1) from purvalue where scriptcode = scriptmaster.scriptcode)) as PurPrice, (select IIF(ISNULL(sum( StockPurchase.Qty)),0,sum( StockPurchase.Qty)) from stockpurchase where scriptcode=scriptmaster.scriptcode )-(select IIF(ISNULL(sum(  Qty)),0,sum( Qty)) from stocksold where scriptcode=scriptmaster.scriptcode ) AS QtyInHand,(select sum(puramt) from purvalue where scriptcode = scriptmaster.scriptcode) as Investment, ScriptMaster.CurrentPrice, ScriptMaster.PortFolioDate, QtyInHand * currentprice AS CurrentValue FROM ScriptMaster where (select IIF(ISNULL(sum(  Qty)),0,sum( Qty)) from stockpurchase where scriptcode=scriptmaster.scriptcode)-(select IIF(ISNULL(sum(  Qty)),0,sum( Qty)) from stocksold where scriptcode=scriptmaster.scriptcode) <> 0";
                    //sqlString = "select * from purvalue";
                }
                else
                    sqlString = "SELECT ScriptMaster.ScriptCode,  ScriptMaster.ScriptName, Market, ScriptMaster.Sector,typeofscript, (select sum(puramt) from purvalue where scriptcode=scriptmaster.scriptcode and holder='" + cboHolder1.Text + "')/(select sum(qty1) from purvalue where scriptcode = scriptmaster.scriptcode and holder='" + cboHolder1.Text + "') as PurPrice,  (select IIF(ISNULL(sum( StockPurchase.Qty)),0,sum( StockPurchase.Qty)) from stockpurchase where scriptcode=scriptmaster.scriptcode and holder='" + cboHolder1.Text + "' )-(select IIF(ISNULL(sum(  Qty)),0,sum( Qty)) from stocksold where scriptcode=scriptmaster.scriptcode and holder='" + cboHolder1.Text + "' ) AS QtyInHand,purprice*qtyinhand as Investment, ScriptMaster.CurrentPrice, ScriptMaster.PortFolioDate,  qtyinhand * currentprice AS CurrentValue FROM ScriptMaster where (select IIF(ISNULL(sum(  Qty)),0,sum( Qty)) from stockpurchase where scriptcode=scriptmaster.scriptcode and holder='" + cboHolder1.Text + "')-(select IIF(ISNULL(sum(  Qty)),0,sum( Qty)) from stocksold where scriptcode=scriptmaster.scriptcode and holder='" + cboHolder1.Text + "') <> 0";
                 //   sqlString = "SELECT ScriptMaster.ScriptCode, ScriptMaster.ScriptName, Market, ScriptMaster.Sector, ScriptMaster.CurrentPrice, ScriptMaster.PortFolioDate, (select IIF(ISNULL(sum( StockPurchase.Qty)),0,sum( StockPurchase.Qty)) from stockpurchase where scriptcode=scriptmaster.scriptcode and holder='" + cboHolder.Text + "')-(select IIF(ISNULL(sum(  Qty)),0,sum( Qty)) from stocksold where scriptcode=scriptmaster.scriptcode and holder='" + cboHolder.Text + "') AS Balance, balance * currentprice AS CurentValue FROM ScriptMaster where (select IIF(ISNULL(sum(  Qty)),0,sum( Qty)) from stockpurchase where scriptcode=scriptmaster.scriptcode and holder='" + cboHolder.Text + "')-(select IIF(ISNULL(sum(  Qty)),0,sum( Qty)) from stocksold where scriptcode=scriptmaster.scriptcode and holder='" + cboHolder.Text + "') <> 0";

                da = new OleDbDataAdapter(sqlString , conn1);
                Ds = new DataSet();
               //dv1 = new DataView() ;
                
                da.Fill(Ds, "ClosingStock");
               // dv1.Table = Ds.Tables["ClosingStock"];
               // dv1.Sort = "scriptname";
                dataGridView1.DataSource = Ds.Tables[0];
                this.dataGridView1.Columns["PurPrice"].DefaultCellStyle.Format = "0.00";
                this.dataGridView1.Columns["CurrentPrice"].DefaultCellStyle.Format = "0.00";
                this.dataGridView1.Columns["CurrentValue"].DefaultCellStyle.Format = "0.00";
                this.dataGridView1.Columns["PurPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns["qtyinhand"].DefaultCellStyle.Format = "0.000";
                this.dataGridView1.Columns["portfoliodate"].DefaultCellStyle.Format = "dd-MMM";
                this.dataGridView1.Columns["Investment"].DefaultCellStyle.Format = "0.00";

                this.dataGridView1.Columns["Investment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns["CurrentPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns["CurrentValue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dataGridView1.Columns["QtyinHand"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                
                //this.dataGridView1.Rows.Add();
                //this.dataGridView1.Rows[dataGridView1.Rows.Count].Cells["currentValue"].Value = Ds.Tables[0].Compute("sum(currentvalue}",string.Empty ).ToString();


                Folio MyFl = new Folio();
                MyFl.SetDataSource(Ds.Tables[0]);
                //MyFl.Load("myfolio.cs");
                crystalReportViewer1.RefreshReport();
                //MyFl.ReportDefinition.Sections[1].ReportObjects["ReportTitle1"].ObjectFormat = "sdfsdf";

              

                 
                crystalReportViewer1.ReportSource = MyFl;
                crystalReportViewer1.ShowRefreshButton = true;
                crystalReportViewer1.DisplayToolbar = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn1.Close();
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }



       
       

    }
}