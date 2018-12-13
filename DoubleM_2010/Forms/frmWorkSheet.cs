using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmWorkSheet : Form
    {
        private DALDoubleM _pdalStock_Base;
        private frmMain theParent;

        public frmWorkSheet(object pdalBase)
        {
            InitializeComponent();
            _pdalStock_Base = (DoubleM.DALDoubleM)pdalBase;

            
        }
        private void RowColor(TreeListViewItem item, Color col)
        {
            item.BackColor = col;
            //item.UseItemStyleForSubItems = true;//Doesn't work, alternative way below
            foreach (TreeListViewItem.ListViewSubItem subItem in item.SubItems)
                subItem.BackColor = col;
        }

        private void LoadWorkSheet()
        {
            trlvWorkSheet.Items.Clear();
            DataTable dtCat = null, dtStocks = null;

            int iQuantity = 0, iTotalQuantity = 0, iQuantityHolding = 0;
            double dblStockHoldingValue = 0.0, dblCurrentHoldingValue = 0.0, dblLPrice = 0.0;
            double dblCatInitalHolding = 0.0, dblCatCurrentHolding = 0.0;
            int iTradeRowCol = 0, iCount = 0;

            theParent.lblDMMsg.Text = "Please wait data loading..";
            Cursor = Cursors.WaitCursor;
            try
            {
                DataView dv = _pdalStock_Base.WorkSheet();
                Application.DoEvents();

                //Reading all catagories from DB
                dtCat = dv.ToTable(true, "StockCatID", "Category");
                dtStocks = dv.ToTable(true, "StockCatID", "Category", "StockID", "StockName");

                foreach (DataRow drCat in dtCat.Rows)
                {
                    theParent.pBarDM.Visible = true;
                    theParent.pBarDM.Maximum = dtCat.Rows.Count; //Set Max value in Progress Bar

                    TreeListViewItem itCat = new TreeListViewItem(drCat["Category"].ToString(), 1);
                    //itCat.Expand();
                    itCat.SubItems.Add("Total Holding");
                    itCat.SubItems.Add("Current Value");
                    itCat.SubItems.Add("");
                    itCat.SubItems.Add("");
                    itCat.SubItems.Add("%");
                    itCat.SubItems.Add("");
                    itCat.Expand();
                    RowColor(itCat, Color.FromArgb(200, 200, 255));
                    //1st Node - categorization on sectors 
                    trlvWorkSheet.Items.Add(itCat);

                    foreach (DataRow drStocks in dtStocks.Select("Category = '" + drCat["Category"].ToString().Replace("'", "''") + "' ", "StockName"))
                    {
                        TreeListViewItem itStocks = new TreeListViewItem(drStocks["StockName"].ToString(), 2);
                        itStocks.Tag = drStocks["StockID"];
                        itStocks.SubItems.Add("");//Item 1 Calculate Holding stocks Value
                        itStocks.SubItems.Add("");//Item 2 Calculate Current Holding stocks Value
                        itStocks.SubItems.Add("");//Item 3 Total Available Stocks
                        itStocks.SubItems.Add("");//Item 4 latest Value
                        itStocks.SubItems.Add("");// Current Profite/Loss %
                        itStocks.SubItems.Add("");// Current Profite/Loss %
                        //2nd Node - Add Script per sector
                        RowColor(itStocks, Color.FromArgb(215, 215, 255));
                        itCat.Items.Add(itStocks);
                        itStocks.Collapse();

                        foreach (DataRow drTrade in dv.Table.Select("Category= '" + drCat["Category"].ToString().Replace("'", "''") + "' and StockName= '" + drStocks["StockName"].ToString().Replace("'", "''") + "'"))
                        {
                            TreeListViewItem itTrade = new TreeListViewItem(((DateTime)drTrade["TradeOn"]).ToString("dd-MMM-yyyy"), 4);
                            iQuantity = (int)drTrade["Quantity"];
                            //iQuantityRemain = drTrade["QuantityRemain"].ToString() == "" ? 0.0 : (double)drTrade["QuantityRemain"];

                            //Item 1 - Calculate total value of Holding stocks/script
                            itTrade.SubItems.Add("");
                            //Item 2 - Currect Value of stocks
                            itTrade.SubItems.Add("");
                            //Item 3 - quantity traded
                            itTrade.SubItems.Add(iQuantity.ToString());
                            //Item 4 - Holds Price paid
                            itTrade.SubItems.Add(Convert.ToDouble(drTrade["Price"]).ToString("#####0.00"));
                            //Item 5 - Average Cost Price goes here
                            itTrade.SubItems.Add("");
                            //Item 6 - calculate stocks holding 
                            //itTrade.SubItems.Add("");
                            //Item 7 - Any Notes on particuler trading
                            itTrade.SubItems.Add(drTrade["TradeNote"].ToString());

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

                                    dblLPrice = drTrade["LPrice"] != DBNull.Value ? Convert.ToDouble(drTrade["LPrice"]) : 0;
                                    if (dblLPrice > 0)
                                    {
                                        dblCurrentHoldingValue += dblLPrice * iQuantityHolding;
                                        //Holding Stocks Value
                                        itTrade.SubItems[2].Text = (dblLPrice * iQuantityHolding).ToString("#####0.00");
                                        //Stocks latest Price
                                        itStocks.SubItems[6].Text = dblLPrice.ToString("#####0.00") + " on " + string.Format("{0:dd-MMM-yy HH:mm}", drTrade["MaxOfOndate"]);
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
                            if (iTradeRowCol++ % 2 == 0)
                                RowColor(itTrade, Color.FromArgb(235, 235, 255));
                            else
                                RowColor(itTrade, Color.White);
                            /*{
                                item.BackColor = shaded;
                                item.UseItemStyleForSubItems = true;
                            }*/
                            //itTrade.BackColor = Color.FromArgb(250, 250, 255);

                            itStocks.Items.Add(itTrade); //leafs: Trading details
                        }
                        itStocks.SubItems[1].Text = dblStockHoldingValue.ToString("#####0.0");
                        itStocks.SubItems[2].Text = dblCurrentHoldingValue.ToString("#####0.0");
                        itStocks.SubItems[3].Text = iTotalQuantity.ToString();
                        itStocks.SubItems[4].Text = iTotalQuantity > 0 ? (dblStockHoldingValue / iTotalQuantity).ToString("#####0.0") : "";
                        itStocks.SubItems[5].Text = iTotalQuantity > 0 ? (100 * ((dblCurrentHoldingValue / dblStockHoldingValue) - 1)).ToString("#####0.0") + "%" : "";
                        itStocks.SubItems[1].Font = new Font(this.Font, FontStyle.Bold);
                        itStocks.SubItems[2].Font = new Font(this.Font, FontStyle.Bold);
                        itStocks.SubItems[3].Font = new Font(this.Font, FontStyle.Bold);
                        itStocks.SubItems[4].Font = new Font(this.Font, FontStyle.Bold);
                        itStocks.SubItems[5].Font = new Font(this.Font, FontStyle.Bold);
                        //itStocks.Font = new Font(this.Font, FontStyle.Bold);
                        dblCatInitalHolding += dblStockHoldingValue;
                        dblCatCurrentHolding += dblCurrentHoldingValue;
                        dblStockHoldingValue = 0.0; dblCurrentHoldingValue = 0.0; iTotalQuantity = 0;
                        iTradeRowCol = 0;
                    }
                    itCat.SubItems[1].Text = dblCatInitalHolding.ToString("#####0.0");
                    itCat.SubItems[2].Text = dblCatCurrentHolding.ToString("#####0.0");
                    itCat.SubItems[5].Text = dblCatCurrentHolding > 0 ? (100 * ((dblCatCurrentHolding / dblCatInitalHolding) - 1)).ToString("#####0.0") + "%" : "";

                    dblCatInitalHolding = 0.0; dblCatCurrentHolding = 0.0;

                    itCat.SubItems[0].Font = new Font(this.Font.FontFamily, this.Font.Size + 1, FontStyle.Bold);
                    itCat.SubItems[1].Font = new Font(this.Font.FontFamily, this.Font.Size + 1, FontStyle.Bold);
                    itCat.SubItems[2].Font = new Font(this.Font.FontFamily, this.Font.Size + 1, FontStyle.Bold);
                    itCat.SubItems[5].Font = new Font(this.Font.FontFamily, this.Font.Size + 1, FontStyle.Bold);

                    iCount++;
                    theParent.pBarDM.Value = iCount;
                }
                //dataGridView1.DataSource = dv.Table;
                theParent.lblDMMsg.Text = "Done";
            }
            catch (Exception ex)
            {
                theParent.lblDMMsg.Text = ex.Message;
            }
            finally
            {
                theParent.pBarDM.Visible = false;
            }
            Cursor = Cursors.Default;
        }
        
        private void frmBase_Load(object sender, EventArgs e)
        {
            theParent = (frmMain)this.ParentForm; //To access MDI parent control

            trlvWorkSheet.Columns.Add("Collection", 250);
            trlvWorkSheet.Columns.Add("Holding Value", 80);
            trlvWorkSheet.Columns.Add("Current Value", 80);
            trlvWorkSheet.Columns.Add("Quantity", 60);
            trlvWorkSheet.Columns.Add("Price", 60);
            trlvWorkSheet.Columns.Add("Holding", 60);
            trlvWorkSheet.Columns.Add("Notes", 150);

            LoadWorkSheet();
        }

        private void trlvWorkSheet_BeforeExpand(object sender, TreeListViewCancelEventArgs e)
        {
            if (e.Item.ImageIndex == 0) e.Item.ImageIndex = 1;
            if (e.Item.ImageIndex == 2) e.Item.ImageIndex = 3;  
        }

        private void trlvWorkSheet_BeforeCollapse(object sender, TreeListViewCancelEventArgs e)
        {
            if (e.Item.ImageIndex == 1) e.Item.ImageIndex = 0;
            if (e.Item.ImageIndex == 3) e.Item.ImageIndex = 2;
        }

        private void mnuExpAll_Click(object sender, EventArgs e)
        {
            trlvWorkSheet.ExpandAll();
        }

        private void mnuColAll_Click(object sender, EventArgs e)
        {
            trlvWorkSheet.CollapseAll();
        }

        private void mnutxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show(mnutxtSearch.Text);
                if (mnutxtSearch.Text.Trim() != "")
                    foreach (TreeListViewItem tvitem in trlvWorkSheet.Items)
                        tvitem.Expand();

                ListViewItem item = trlvWorkSheet.FindItemWithText(mnutxtSearch.Text);
                if (item != null)
                {
                    trlvWorkSheet.SelectedIndices.Clear();
                    trlvWorkSheet.Focus();
                    item.Selected = true;
                    item.EnsureVisible();
                    mnutxtSearch.Text = "";
                }
            }
        }

        private void mnuNewTrad_Click(object sender, EventArgs e)
        {
            frmNewTrad pNewTrad = new frmNewTrad(_pdalStock_Base);
            pNewTrad.ShowDialog();
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            LoadWorkSheet();
        }
    }
}