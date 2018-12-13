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
    public partial class frmCapitalGainLoss : Form
    {
        public frmCapitalGainLoss()
        {
            InitializeComponent();
        }
        
OleDbConnection conn;
        private void button1_Click(object sender, EventArgs e)
        {
            
            ClosingStock Cs;
            Cs = new ClosingStock();
            conn = Cs.GetConnection();
            try
            {
                conn.Open();
                
                OleDbDataAdapter da;
                string strSQL;

                if (cboHolder.Text=="All")
                {
                    strSQL = "select stocksold.ScriptCode,stocksold.Holder,scriptmaster.ScriptName,DateOfSale,Qty,Price,Qty*price as Amount,(select top 1 price from stockpurchase where trnid=stocksold.purchasetrnid) as PurchaseRate,(select top 1 dateofpurchase from stockpurchase where trnid=stocksold.purchasetrnid) as PurDate ,(select InflationIndex from InflationIndex where enddate>=(select  dateofpurchase from stockpurchase where trnid=stocksold.purchasetrnid) and startdate <=(select top 1 dateofpurchase from stockpurchase where trnid=stocksold.purchasetrnid)) as PurIndex,(select InflationIndex from InflationIndex where enddate>=dateofsale and startdate <=dateofsale) as SaleIndex,(select financialyear from InflationIndex where enddate>=dateofsale and startdate <=dateofsale )as FinancialYear, Purchaserate*qty as PurAmount from stocksold,scriptmaster where stocksold.scriptcode=scriptmaster.scriptcode and dateofsale>=#" + dtStart.Value.ToShortDateString() + "# and dateofsale<=#" + DtEnd.Value.ToShortDateString() + "#";
                }
                else
                {
                    strSQL = "select stocksold.ScriptCode,StockSold.Holder,scriptmaster.ScriptName,DateOfSale,Qty,Price,Qty*price as Amount,(select top 1 price from stockpurchase where trnid=stocksold.purchasetrnid) as PurchaseRate,(select top 1 dateofpurchase from stockpurchase where trnid=stocksold.purchasetrnid) as PurDate ,(select InflationIndex from InflationIndex where enddate>=(select  dateofpurchase from stockpurchase where trnid=stocksold.purchasetrnid) and startdate <=(select top 1 dateofpurchase from stockpurchase where trnid=stocksold.purchasetrnid)) as PurIndex,(select InflationIndex from InflationIndex where enddate>=dateofsale and startdate <=dateofsale) as SaleIndex,(select financialyear from InflationIndex where enddate>=dateofsale and startdate <=dateofsale) as FinancialYear, Purchaserate*qty as PurAmount from stocksold,scriptmaster where stocksold.scriptcode=scriptmaster.scriptcode and dateofsale>=#" + dtStart.Value.ToShortDateString() + "# and dateofsale<=#" + DtEnd.Value.ToShortDateString() + "# and holder='" + cboHolder.Text + "'";
                }

                da = new OleDbDataAdapter(strSQL, conn);
                
                DataSet ds;

                ds = new DataSet();

                da.Fill(ds, "Temp");

                dataGridView1.DataSource = ds.Tables[0];
                CapitalGain cr = new CapitalGain();
                cr.SetDataSource(ds.Tables[0]);
                cr.Refresh() ;
                crystalReportViewer1.ReportSource = cr;
                crystalReportViewer1.ShowRefreshButton = true;
                
                crystalReportViewer1.DisplayToolbar = true;


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }

            finally
            {

            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void frmCapitalGainLoss_Load(object sender, EventArgs e)
        {
            try
            {
                ClosingStock cs;

                cs = new ClosingStock();
                conn = cs.GetConnection();
                conn.Open();
                OleDbCommand cmd1;
                cmd1 = new OleDbCommand();
                cmd1.CommandText = "select distinct holder from stockpurchase order by holder";
                cmd1.Connection = conn;

             OleDbDataReader dr;
                dr = cmd1.ExecuteReader();

                 
                 
              
                cboHolder.Items.Add("All");
                while (dr.Read())
                {
                    cboHolder.Items.Add(dr["holder"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
               
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}