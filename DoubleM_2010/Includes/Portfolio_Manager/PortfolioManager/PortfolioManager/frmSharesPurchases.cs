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
    public partial class frmSharesPurchases : Form
    {
        public frmSharesPurchases()
        {
            InitializeComponent();
        }


        OleDbConnection Conn;
        OleDbDataAdapter da;
        OleDbCommand cmd;
         string strConnectionString; 
        private void frmSharesPurchases_Load(object sender, EventArgs e)
        {
            cboTransactiontype.Items.Clear();
            cboTransactiontype.Items.Add("Buy");
            cboTransactiontype.Items.Add("Bonus");
            cboTransactiontype.Items.Add("Split");
            cboTransactiontype.Items.Add("Sell");
            cboTransactiontype.Items.Add("Dividend");
            try
            {
                Conn = new OleDbConnection();
               
                strConnectionString = "provider=microsoft.jet.oledb.4.0;data source=c:\\temp\\ShareReport.mdb;";
                Conn.ConnectionString = strConnectionString;
                da = new OleDbDataAdapter("select scriptcode,scriptname + ' ' + market + ' ' + series as ScriptName1 from scriptmaster order by scriptname",Conn );
                DataSet ds;
                ds = new DataSet();
                da.Fill(ds, "ScriptMaster");
                cboScript.DataSource = ds.Tables["scriptmaster"];
                cboScript.DisplayMember = "scriptname1";
                cboScript.ValueMember = "scriptcode";
                cboScript.Text = "";
                da.SelectCommand.CommandText = "select distinct holder from stockpurchase";
                DataSet ds1;
                ds1 = new DataSet();
                da.Fill(ds1, "Holder");
                cboHolder.DataSource = ds1.Tables["Holder"];
                cboHolder.DisplayMember = "holder";
                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open connection " + ex.Message );
            }

            finally
            {
                MainDataset();

                Conn.Close();
            }

        }

        DataView dv;
        public DataSet MainDataset()
        { 
            try
            {
                Conn.Open ();
                da = new OleDbDataAdapter("select TrnID,DateOfPurchase as Dated,stockpurchase.ScriptCode,ScriptName ,scriptmaster.Market,TypeofTransaction,Qty,Price,Amount,Holder from StockPurchase , scriptmaster   where scriptmaster.scriptcode=stockpurchase.scriptcode union all select TrnID,DateOfsale ,stocksold.ScriptCode,  ScriptName,scriptmaster.Market,TypeofTransaction,Qty,Price,Amount,Holder from StockSold , scriptmaster   where scriptmaster.scriptcode=StockSold.scriptcode order by trnid,dated", Conn);
                DataSet ds;
                ds = new DataSet();
                da.Fill(ds,"MainTable");
                
                dv = new DataView();
                dv.Table = ds.Tables["MainTable"];
                dv.Sort = "dated,scriptname";
                dataGridView1.DataSource = dv;

                return ds;
            }
            catch(Exception ex)
            {
                MessageBox.Show (ex.Message + " " + ex.Data);
                return null;
            }

            finally
            {
                Conn.Close ();
                
            }
        }
        public void ClearControls()
        {
             

            lblTransactionId.Text = "0";
            txtQty.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtAmount.Text = string.Empty;
           // cboHolder.Text = string.Empty;
           // cboScript.Text = string.Empty;
            cboTransactiontype.Text = string.Empty;
           // dtDated.Value = DateTime.Parse(DateTime.Now.ToShortDateString());
            cboTransactiontype.Text = string.Empty;

            

        }

        public void InsertSoldEntry()
        {
            try{
            Conn.Open();
                cmd=new OleDbCommand();
                cmd.Connection=Conn;
                
            string InsertQuery;
            ClosingStock ClsStock;
            ClsStock = new ClosingStock();
            cmd.CommandText = "delete from stocksold where trnid=" + long.Parse(lblTransactionId.Text);
            cmd.ExecuteNonQuery();

                float ClosingQty=ClsStock.CalculateClosingStock(cboScript.SelectedValue.ToString(),Conn,cboHolder.Text.ToString());

                if (float.Parse("0" + txtQty.Text) > ClosingQty)
                {
                    MessageBox.Show("Sold Qty should not be greater than " + ClosingQty + " (Qty in hand", "Error Saving", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;

                }
                float[,] st;
                st=  ClsStock.ReturnSellingNumbers(cboScript.SelectedValue.ToString(), Conn, float.Parse(txtQty.Text));
                int i;
                for (i=0;i<10;i++)
                {
                    if (st[i, 1] > 0)
                    {
                        InsertQuery = "insert into stockSold (scriptcode,dateofsale,qty,price,amount,holder,typeoftransaction,purchasetrnid) values('" + cboScript.SelectedValue.ToString() + "','" + dtDated.Value.ToShortDateString() + "'," + float.Parse(st[i, 1].ToString()) + "," + float.Parse(txtPrice.Text) + "," + (float.Parse(txtPrice.Text) * float.Parse(st[i, 1].ToString())) + ",'" + cboHolder.Text + "','" + cboTransactiontype.Text + "'," + st[i, 0] + ")";
                        cmd.CommandText = InsertQuery;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show ("" + ex.Message);
            }
            finally
            {
                ClearControls();
                Conn.Close();
                cboScript.Focus();
                MainDataset();
            }






        }
        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            if (cboTransactiontype.Text.Equals(string.Empty))
            {
                MessageBox.Show("Please Select Transaction type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;

            }

            if (cboTransactiontype.Text == "Sell")
            {
                InsertSoldEntry();
                return;
            }
          

            try
            {
               int scrollposition;
               scrollposition = dataGridView1.CurrentRow.Index     ;
               Conn.Open();
               string InsertQuery;
               if (long.Parse("" + lblTransactionId.Text) > 0)
                {
                InsertQuery = "update stockpurchase set scriptcode='" +
                        cboScript.SelectedValue.ToString()  + "',dateofpurchase='" + 
                        dtDated.Value.ToShortDateString() + "',qty=" + 
                        float.Parse(txtQty.Text) + ",price=" + 
                        float.Parse(txtPrice.Text)
                        + ",amount=" + float.Parse( txtAmount.Text ) + 
                        ",Holder='" +  cboHolder.Text + "',typeoftransaction='" 
                        + cboTransactiontype.Text + "' where trnid=" + 
                        long.Parse(lblTransactionId.Text);

                }
                    else
                {
                    InsertQuery = "insert into stockpurchase (scriptcode,dateofpurchase,qty,price,amount,holder,typeoftransaction) values('" + cboScript.SelectedValue.ToString() + "','" + dtDated.Value.ToShortDateString() + "'," + float.Parse(txtQty.Text) + "," + float.Parse(txtPrice.Text) + "," + float.Parse(txtAmount.Text) + ",'" + cboHolder.Text + "','" + cboTransactiontype.Text + "')";
                }
                
                cmd = new OleDbCommand();
                da = new OleDbDataAdapter();
                cmd.Connection = Conn;
                cmd.CommandText = InsertQuery;
                cmd.ExecuteNonQuery();

                
                Conn.Close();
                MainDataset();
               // dataGridView1.FirstDisplayedScrollingRowIndex   = scrollposition;
                //MessageBox.Show("" + scrollposition);
                dataGridView1.FirstDisplayedScrollingRowIndex = scrollposition ;
                dataGridView1.Rows[scrollposition].Selected = true;

                dataGridView1.Focus();
                ClearControls();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conn.Close();
                cboScript.Focus();
       
       

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {


                contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
            }
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

       
        private void cmdEdit_Click_1(object sender, EventArgs e)
        {
            lblTransactionId.Text = dataGridView1.CurrentRow.Cells["TrnId"].Value.ToString();
            cboTransactiontype.Text = dataGridView1.CurrentRow.Cells["typeoftransaction"].Value.ToString();
            cboScript.Text = dataGridView1.CurrentRow.Cells["scriptName"].Value.ToString();
            cboScript.SelectedValue = dataGridView1.CurrentRow.Cells["scriptcode"].Value.ToString();
            cboHolder.Text = dataGridView1.CurrentRow.Cells["holder"].Value.ToString();
            txtPrice.Text = "" + dataGridView1.CurrentRow.Cells["price"].Value.ToString();
            txtAmount.Text = "" + dataGridView1.CurrentRow.Cells["amount"].Value.ToString();
            txtQty.Text = "" + dataGridView1.CurrentRow.Cells["qty"].Value.ToString();
            dtDated.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells["dated"].Value.ToString());
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (cboTransactiontype.Items.Count < 4)
            cboTransactiontype.Items.Add("Sell");
            ClearControls();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cboTransactiontype.Items.Remove("Sell");

            cmdEdit_Click_1(sender, e);
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete it", "Delete It", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Conn.Open();
                    cmd = new OleDbCommand();
                    cmd.Connection = Conn;
                    if (dataGridView1.CurrentRow.Cells["typeoftransaction"].Value.ToString() == "Sell")
                    {
                        cmd.CommandText = "delete from stocksold where trnid=" + long.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    }
                    else
                    {
                        cmd.CommandText = "delete from stockpurchase where trnid=" + long.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Deleted");
                    ClearControls();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Delete " + ex.Message );
                }
                finally
                {
                    Conn.Close();
                    MainDataset();
                }

            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmdDelete_Click(sender, e);
        }
        
       

        private void txtQty_Leave(object sender, EventArgs e)
        {

            txtAmount.Text = "" + float.Parse (0+txtPrice.Text) * float.Parse(0 + txtQty.Text);
        }

       

        private void cboScript_Leave(object sender, EventArgs e)
        {
            try
            {
                Conn.Open();
                ClosingStock cst;
                cst = new ClosingStock();
                label8.Text = "Current Balance : " + cst.CalculateClosingStock(cboScript.SelectedValue.ToString(), Conn, cboHolder.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            finally { Conn.Close(); }

        }

       
        private void cboHolder_Leave(object sender, EventArgs e)
        {
            cboScript_Leave(sender, e);
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
           // txtPrice.Text = float.Parse(0+txtPrice.Text.ToString( )).ToString("0.00");
        }

        private void moreInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
            if (dataGridView1.CurrentRow.Cells["market"].Value.ToString()=="BSE")
                {
                    OpenSite("http://bseindia.com/price_finder/stockreach.asp?scripcd=" + dataGridView1.CurrentRow.Cells["scriptcode"].Value.ToString());
                }
            else
            
                {
                    OpenSite("http://nseindia.com/marketinfo/equities/quotesearch.jsp?companyname=" + dataGridView1.CurrentRow.Cells["scriptcode"].Value.ToString() + "&submit1=go&series=EQ&flag=0");
         
                }


   

        }
        public void OpenSite(string siteid)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            process.StartInfo.UseShellExecute = true;
            
                process.StartInfo.FileName = siteid;
             
               // process.StartInfo.FileName = "http://nseindia.com/marketinfo/equities/quotesearch.jsp?companyname=" + dataGridView1.CurrentRow.Cells["scriptcode"].Value.ToString() + "&submit1=go&series=EQ&flag=0";

          
            process.Start();
        }

        private void technicalAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSite("http://www.moneycontrol.com/stocks/cptmarket/charting.php?stockcode=" +  dataGridView1.CurrentRow.Cells["scriptcode"].Value.ToString() + "&compname=&x=9&y=5");
        }

        
         

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dv.RowFilter = "scriptname='" + dataGridView1.CurrentRow.Cells["scriptname"].Value.ToString() +"'";

        }

       

        

        
         

       

       

        
       

        

     
    



        
    }
}