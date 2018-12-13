using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewDBTestApp
{
    public partial class Form7 : Form
    {
        //const string ConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Stock.mdb";
        const string ConnString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\Deepak\DoubleM\Includes\NewDBTestApp\Stock.mdb";
        
        OleDbConnection oConn = new OleDbConnection(ConnString);
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter ad = new OleDbDataAdapter();

        public Form7()
        {
            InitializeComponent();
            treeListView1.Columns.Add("Collection", 250);
            treeListView1.Columns.Add("Holding Value", 80);
            treeListView1.Columns.Add("Current Value", 80);
            treeListView1.Columns.Add("Quantity", 60);
            treeListView1.Columns.Add("Price", 60);
            treeListView1.Columns.Add("Holding", 60);
            treeListView1.Columns.Add("Notes", 150);
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "QryAllDetails";
            cmd.Connection = oConn;
            DataTable dtCat=null, dtStocks=null;

            int iQuantity = 0, iTotalQuantity =0, iQuantityHolding=0;
            double dblStockHoldingValue = 0.0, dblCurrentHoldingValue = 0.0, dblLPrice = 0.0;

            try
            {
                ad = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataView dv = new DataView(ds.Tables[0]);

                //Reading all catagories from DB
                dtCat = dv.ToTable(true, "StockCatID", "Category");
                dtStocks = dv.ToTable(true, "StockCatID", "Category", "StockID", "StockName");

                foreach (DataRow drCat in dtCat.Rows)
                {
                    TreeListViewItem itCat = new TreeListViewItem(drCat["Category"].ToString(), 1);
                    //itCat.Expand();
                    itCat.SubItems.Add("Total Holding");
                    itCat.SubItems.Add("Current Value");
                    itCat.Expand();
                    treeListView1.Items.Add(itCat); //1st Node
                    foreach (DataRow drStocks in dtStocks.Select("Category = '" + drCat["Category"].ToString().Replace("'", "''") + "' ", "StockName"))
                    {
                        TreeListViewItem itStocks = new TreeListViewItem(drStocks["StockName"].ToString(), 2);
                        itStocks.Tag = drStocks["StockID"];
                        itStocks.SubItems.Add("");//Item 1 Calculate Holding stocks Value
                        itStocks.SubItems.Add("");//Item 2 Calculate Current Holding stocks Value
                        itStocks.SubItems.Add("");//Item 3 Total Available Stocks
                        itStocks.SubItems.Add("");//Item 4 latest Value
                        itStocks.SubItems.Add("");// Current Profite/Loss %
                        itCat.Items.Add(itStocks); //2nd Node
                        itStocks.Collapse();
                        foreach (DataRow drTrade in dv.Table.Select("Category= '" + drCat["Category"].ToString().Replace("'", "''") + "' and StockName= '" + drStocks["StockName"].ToString().Replace("'", "''") + "'"))
                        {
                            TreeListViewItem itTrade = new TreeListViewItem(((DateTime)drTrade["TradeOn"]).ToString("dd-MMM-yyyy"), 4);
                            iQuantity = (int)drTrade["Quantity"];
                            //iQuantityRemain = drTrade["QuantityRemain"].ToString() == "" ? 0.0 : (double)drTrade["QuantityRemain"];
                            itTrade.SubItems.Add("");//Item 1 Calculate Holding stocks Value
                            itTrade.SubItems.Add("");//Item 2 Currect Value of stocks
                            itTrade.SubItems.Add(iQuantity.ToString());//Item 3
                            itTrade.SubItems.Add(Convert.ToDouble(drTrade["Price"]).ToString("#####0.00"));//Item 4
                            itTrade.SubItems.Add("");//Average Cost Price
                            itTrade.SubItems.Add("");//Item 5 calculate stocks holding 
                            itTrade.SubItems.Add(drTrade["TradeNote"].ToString());//Item 6

                            /*Calculated items values*/
                            if (iQuantity > 0)
                            {
                                if (drTrade["SoldQuantity"].ToString() == "")
                                    iQuantityHolding = iQuantity;
                                else
                                    iQuantityHolding = iQuantity - Convert.ToInt32(drTrade["SoldQuantity"].ToString());

                                //Holding stocks per trading
                                itTrade.SubItems[5].Text = iQuantityHolding.ToString();

                                if (iQuantityHolding > 0)
                                {
                                    iTotalQuantity += iQuantityHolding;
                                    itTrade.SubItems[1].Text = (iQuantityHolding * Convert.ToDouble(drTrade["Price"])).ToString("#####0.00");
                                    dblStockHoldingValue += Convert.ToDouble(itTrade.SubItems[1].Text);

                                    dblLPrice = drTrade["LPrice"] != DBNull.Value? Convert.ToDouble(drTrade["LPrice"]):0;
                                    if (dblLPrice > 0)
                                    {
                                        dblCurrentHoldingValue += dblLPrice * iQuantityHolding;
                                        //Holding Stocks Value
                                        itTrade.SubItems[2].Text = (dblLPrice * iQuantityHolding).ToString("#####0.00");
                                        //Stocks latest Price
                                        //itStocks.SubItems[5].Text = dblLPrice.ToString("#####0.00");
                                        itStocks.ToolTipText = dblLPrice.ToString("#####0.00");
                                    }
                                }
                            }

                            /*Fonts and Colors*/
                            itTrade.SubItems[1].Font = new Font(this.Font, FontStyle.Regular);
                            itTrade.SubItems[3].ForeColor = iQuantity < 0 ? Color.Red : Color.DarkGreen;
                            itTrade.SubItems[6].Tag = drTrade["TradeNoteID"].ToString();
                            itTrade.SubItems[6].Font = new Font(this.Font, FontStyle.Italic);

                            //itTrade.ForeColor = Color.Red;
                            //itTrade.SubItems[3].BackColor = Color.RosyBrown;
                            itTrade.SubItems[4].ForeColor = Color.SeaGreen;
                            itTrade.SubItems[4].Font = new Font(this.Font, FontStyle.Bold);

                            itStocks.Items.Add(itTrade); //leafs: Trading details
                        }
                        itStocks.SubItems[1].Text = dblStockHoldingValue.ToString("#####0.0");
                        itStocks.SubItems[2].Text = dblCurrentHoldingValue.ToString("#####0.0");
                        itStocks.SubItems[3].Text = iTotalQuantity.ToString();
                        itStocks.SubItems[4].Text = iTotalQuantity > 0 ? (dblStockHoldingValue / iTotalQuantity).ToString("#####0.0") : "";
                        itStocks.SubItems[5].Text = iTotalQuantity > 0 ? (100 * ((dblCurrentHoldingValue / dblStockHoldingValue)-1)).ToString("#####0.0")+"%" : "";
                        itStocks.SubItems[1].Font = new Font(this.Font, FontStyle.Bold);
                        itStocks.SubItems[2].Font = new Font(this.Font, FontStyle.Bold);
                        itStocks.SubItems[3].Font = new Font(this.Font, FontStyle.Bold);
                        itStocks.SubItems[4].Font = new Font(this.Font, FontStyle.Bold);
                        itStocks.SubItems[5].Font = new Font(this.Font, FontStyle.Bold);
                        //itStocks.Font = new Font(this.Font, FontStyle.Bold);
                        dblStockHoldingValue = 0.0; dblCurrentHoldingValue = 0.0; iTotalQuantity = 0;
                    }
                }
                dataGridView1.DataSource = dv.Table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (oConn.State != ConnectionState.Closed)
                    oConn.Close();
            }
        }

        private void treeListView1_BeforeExpand(object sender, TreeListViewCancelEventArgs e)
        {
            if (e.Item.ImageIndex == 0) e.Item.ImageIndex = 1;  
            if (e.Item.ImageIndex == 2) e.Item.ImageIndex = 3;  
        }

        private void treeListView1_BeforeCollapse(object sender, TreeListViewCancelEventArgs e)
        {
            if (e.Item.ImageIndex == 1) e.Item.ImageIndex = 0;
            if (e.Item.ImageIndex == 3) e.Item.ImageIndex = 2;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //dataGridView1[1, e.RowIndex+1].Style.BackColor = Color.FromArgb(255, 20, 200);
            dataGridView1.Rows[e.RowIndex].HeaderCell.Value = e.RowIndex + 1;
        }
    }
}
