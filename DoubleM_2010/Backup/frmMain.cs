using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmMain : Form
    {
        private static DALDoubleM _pdalStock;
        private frmTradingHist pTradings;
        //private DataGridViewPrinter _DataGridViewPrinter;

        public frmMain()
        {
            InitializeComponent();
            _pdalStock = new DALDoubleM(lblDMMsg, pBarDM);
            _pdalStock.MarqueeUpdate();
            CommonDoubleM._pdalStock1 = new DALDoubleM(lblDMMsg, pBarDM);
            //////////////////////////
            _groupPaneBar.Add(CreateButton("Basics", CommonDoubleM.Basics.Length), "Basics  ", imglstMain.Images[0], true);
            _groupPaneBar.Add(CreateButton("Online", CommonDoubleM.Online.Length), "Online  ", imglstMain.Images[1], true);
            _groupPaneBar.Add(CreateButton("Analysis", CommonDoubleM.Analysis.Length), "Analysis  ", imglstMain.Images[2], true);
            _groupPaneBar.Add(CreateButton("Views", CommonDoubleM.Views.Length), "Views  ", imglstMain.Images[3], true);
            _groupPaneBar.Add(CreateButton("Configuration", CommonDoubleM.Configuration.Length), "Configuration  ", imglstMain.Images[3], true);
            _groupPaneBar.Add(CreateButton("About", CommonDoubleM.About.Length), " About  ", imglstMain.Images[4], true);


            _groupPaneBar.CollapseAll(false);
            _groupPaneBar.CanResize = false;
            _groupPaneBar[0].Expanded = true;
            _groupPaneBar.ShowExpandCollapseButton = false;
            selectAllToolStripMenuItem.Checked = false;
            /*Winform FAQ*/
            //http://www.syncfusion.com/faq/windowsforms/default.aspx

            /*Hiding Tab Pages on tab control*/
            //http://www.experts-exchange.com/Programming/Programming_Languages/C_Sharp/Q_21549892.html
            tbMain.Top = tbMain.Top - tbMain.ItemSize.Height;
            tbMain.Height = tbMain.Height + tbMain.ItemSize.Height;
            tbMain.Region = new Region(new RectangleF(tpMain.Left, tpMain.Top, tpMain.Width, tpMain.Height + tbMain.ItemSize.Height));

            LoadStocks();
            pTradings = new frmTradingHist(_pdalStock);
        }
        private Panel CreateButton(string baseText, int count)
        {
            Panel result = new Panel();
            string[] BtnNames = null;
            switch (baseText)
            {
                case "Basics":
                    BtnNames = CommonDoubleM.Basics;
                    break;
                case "Online":
                    BtnNames = CommonDoubleM.Online;
                    break;
                case "Analysis":
                    BtnNames = CommonDoubleM.Analysis;
                    break;
                case "Views":
                    BtnNames = CommonDoubleM.Views;
                    break;
                case "Configuration":
                    BtnNames = CommonDoubleM.Configuration;
                    break;
                case "About":
                    BtnNames = CommonDoubleM.About;
                    break;
            }
            for (int i = count; i >= 1; i--)
            {
                Button Btn = new Button();
                Btn.Dock = DockStyle.Top;
                Btn.TextAlign = ContentAlignment.MiddleRight;
                Btn.Text = BtnNames[i - 1]; //baseText + i;
                result.Controls.Add(Btn);
                Btn.Height = 27;
                Btn.BackColor = Color.DimGray;
                Btn.FlatStyle = FlatStyle.Flat;
                Btn.FlatAppearance.BorderSize = 0;
                Btn.FlatAppearance.MouseOverBackColor = Color.LightGray;
                Btn.FlatAppearance.MouseDownBackColor = Color.DarkSeaGreen;

                Btn.Click += new EventHandler(MenuBtn_Click);
            }
            result.Height = result.Controls[0].Bottom;
            return result;
        }
        internal void MenuBtn_Click(object sender, EventArgs e)
        {
            string s = ((Button)sender).Text + ((Button)sender).Name;

            switch (s)
            {
                case "Work Sheet":
                    frmWorkSheet pWS = new frmWorkSheet(_pdalStock);
                    pWS.MdiParent = this;
                    pWS.Show();
                    break;
                case "Stock":
                    Form1 pf2 = new Form1(_pdalStock);
                    pf2.ShowDialog();
                    break;
                case "OO":
                    //Do nothing for the moment
                    break;
                case "Trade Book":
                    ((Button)sender).Enabled = false;
                    tbMain.SelectedIndex = 1;
                    ((Button)sender).Enabled = true;
                    break;
                case "Profit && Loss":
                    tbMain.SelectedIndex = 2;
                    break;
                case "Trend Graph":
                    frmTrendGr pSimpleGr = new frmTrendGr(_pdalStock);
                    pSimpleGr.MdiParent = this;
                    pSimpleGr.Show();
                    break;
                case "Investment Category Graph":
                    frmCatGraph pCatGr = new frmCatGraph(_pdalStock);
                    pCatGr.MdiParent = this;
                    pCatGr.Show();
                    break;
                case "Online Update":
                    ((Button)sender).Enabled = false;
                    UpdateOnline();
                    scrtxtStock.ScrollText = CommonDoubleM.MarqueeString;
                    ((Button)sender).Enabled = true;
                    break;
                case "Update Traker":
                    ((Button)sender).Enabled = false;
                    _pdalStock.MarqueeUpdate();
                    scrtxtStock.ScrollText = CommonDoubleM.MarqueeString;
                    ((Button)sender).Enabled = true;
                    break;
                case "Online Portfolio":
                    frmOnlinePortfolio pOnlinePF = new frmOnlinePortfolio(_pdalStock);
                    pOnlinePF.MdiParent = this;
                    pOnlinePF.Show();
                    //frmWatchlist
                    break;
                    
                case "Watch List":
                    frmWatchlist pWatchList = new frmWatchlist(_pdalStock);
                    pWatchList.MdiParent = this;
                    pWatchList.Show();
                    //frmWatchlist
                    break;
                case "New Trad":
                    frmNewTrad pNewTrad = new frmNewTrad(_pdalStock);
                    //pNewTrad.MdiParent = this;
                    pNewTrad.ShowDialog();
                    break;
                case "Quick View - Avg":
                    frmlAvgView pQuickView = new frmlAvgView(_pdalStock, "Quick View - Average");
                    pQuickView.MdiParent = this;
                    pQuickView.Show();
                    break;
                case "Cumulative Average":
                    frmlAvgView pCumulAvg = new frmlAvgView(_pdalStock, "Quick View - Cumulative Average");
                    pCumulAvg.MdiParent = this;
                    pCumulAvg.Show();
                    break;
                case "Stock Edit":
                    frmStocks pStocks = new frmStocks(_pdalStock);
                    pStocks.MdiParent = this;
                    pStocks.Show();
                    break;

                /*Views panel*/
                case "Cascade":
                    this.LayoutMdi(MdiLayout.Cascade);
                    break;
                case "Horizontal":
                    this.LayoutMdi(MdiLayout.TileHorizontal);
                    break;
                case "Vertical":
                    this.LayoutMdi(MdiLayout.TileVertical); 
                    break;
                case "Arrange":
                    this.LayoutMdi(MdiLayout.ArrangeIcons);
                    break;

                /*Configuration panel*/
                case "Settings":
                    //Do nothing for the moment
                    break;
                case "Proxy":
                    tbMain.SelectedIndex = 2;
                    /*string s1 = AppWRDoubleM.ReadSetting("Setting1");
MessageBox.Show(s1);
AppWRDoubleM.WriteSetting("Setting1", "abc");*/

                    //int n = Int32.Parse(dks,System.Globalization.StringInfo.ParseCombiningCharacters);
                   /* string dks = CryptorEngine.Encrypt("Nestle", true);
                    string dk = CryptorEngine.Decrypt(dks, true);*/
                    break;


                default:
                    break;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //scrtxtStock.BackgroundBrush =
            //    new LinearGradientBrush(this.scrtxtStock.ClientRectangle,
            //    Color.Red, Color.Blue,
            //    LinearGradientMode.Horizontal);
            this.Icon = new Icon(this.GetType().Assembly.GetManifestResourceStream("DoubleM.Pics.der.ico"));
            CommonDoubleM.LoadAppConfig();
            
                

            pBarDM.Visible = false;
            dtPKStart.Value = DateTime.Now.Date;
            scrtxtStock.BackgroundBrush =
                    new LinearGradientBrush(this.scrtxtStock.ClientRectangle,
                    Color.Black, Color.DarkSlateGray,
                    LinearGradientMode.Horizontal);
            scrtxtStock.ScrollText = CommonDoubleM.MarqueeString;

            chkAfterBefore.Checked = true;

            /*Loading proxy Settings*/
            chkProxy.Checked = CommonDoubleM._blnFirewll;
            txtProxySrvName.Text = CommonDoubleM._sProxy;
            txtUID.Text = CommonDoubleM._sUID;
            txtPWD.Text = CommonDoubleM._sPWD;
            ProxySettingInit();
            //////////////////////////////////////

        }

        private void UpdateOnline()
        {
            Cursor = Cursors.WaitCursor;
            if (MessageBox.Show("Are you sure to update stock latest price.."
    , "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _pdalStock.UpdateStockprice();
                _pdalStock.MarqueeUpdate();
                scrtxtStock.ScrollText = CommonDoubleM.MarqueeString;
            }
            Cursor = Cursors.Default;
        }
        private void LoadStocks()
        {
            //lstBStock
            //_pdalStock.Stocks(

            string sDM = "", sVM = "";
            Cursor = Cursors.WaitCursor;
            //dtpOndate.Value = DateTime.Now.Date;
            
            lstBStock.DataSource = _pdalStock.Stocks(ref sDM, ref sVM, 8);
            lstBStock.DisplayMember = sDM;
            lstBStock.ValueMember = sVM;
            lstBStock.SetSelected(lstBStock.SelectedIndex, false); //Unselect default selected item in list box
            Cursor = Cursors.Arrow;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Select All items in the Stock ListBox
            selectAllToolStripMenuItem.Checked = selectAllToolStripMenuItem.Checked ? false : true;
            for(int i=0;i<lstBStock.Items.Count;i++)
               lstBStock.SetSelected(i, selectAllToolStripMenuItem.Checked);           
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Reload Stocks name from database
            lstBStock.DataSource = null;
            lstBStock.Items.Clear();
            LoadStocks();
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            
            string sStockIDs = "", sStocksDates="", sWhere = "", sSQL = "";

            if (lstBStock.SelectedItems.Count <= 0) return; //Do nothing if no stock selected

            btnStart.Enabled = false;
            Cursor = Cursors.WaitCursor;

            //sStocksDates = " AND (TTrade.TradeOn" + chkAfterBefore.Text + "=#" + dtPKStart.Value.Date.ToString() + "#)";
            sStocksDates = " AND (TTrade.TradeOn" + chkAfterBefore.Text + "=#" + dtPKStart.Value.AddHours(23).ToString() + "#)";
            
            for (int i = 0; i <= lstBStock.SelectedItems.Count - 1; i++)
            {
                DataRowView ln = lstBStock.SelectedItems[i] as DataRowView;
                //MessageBox.Show(ln[0].ToString());
                sStockIDs = "TStockName.StockID=" + ln[0].ToString();
                sWhere += "((" + sStockIDs + ")" + sStocksDates + ") OR ";
            }
            sWhere = sWhere.Substring(0, sWhere.Length - 4);
            sSQL = "SELECT TStockName.StockName, TTrade.Quantity, TRates.Price, TTrade.Brokerage, TTrade.Tax, TTrade.TradeOn, TTradeNotes.TradeNote " +
                   "FROM ((TStockName INNER JOIN TRates ON TStockName.StockID = TRates.StockID) INNER JOIN TTrade ON TRates.RateID = TTrade.RateID) LEFT JOIN TTradeNotes ON TTrade.TradeID = TTradeNotes.TradeID " +
                   "Where " + sWhere + " ORDER BY TStockName.StockName, TTrade.TradeOn";

            //MessageBox.Show(sSQL);

            if (pTradings == null) pTradings = new frmTradingHist(_pdalStock);
            pTradings.MdiParent = this;
            pTradings.Show();
            pTradings.Select();
            pTradings.FillTradeBook(sSQL);
            btnStart.Enabled = true;
            Cursor = Cursors.Arrow;
        }

        private void chkAfterBefore_CheckedChanged(object sender, EventArgs e)
        {
            chkAfterBefore.Text = (string)(chkAfterBefore.Checked ? "<" : ">");
        }

        private void chkProxy_CheckedChanged(object sender, EventArgs e)
        {
            ProxySettingInit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CommonDoubleM._blnFirewll = chkProxy.Checked;
            AppWRDoubleM.WriteSetting("Firewall", CommonDoubleM._blnFirewll.ToString());
            try
            {
                if (chkProxy.Checked)
                {
                    CommonDoubleM._sProxy = txtProxySrvName.Text.Trim();
                    AppWRDoubleM.WriteSetting("Proxy", CommonDoubleM._sProxy);
                    CommonDoubleM._sUID = txtUID.Text.Trim();
                    AppWRDoubleM.WriteSetting("UID", txtUID.Text.Trim());
                    CommonDoubleM._sPWD = txtPWD.Text.Trim();
                    AppWRDoubleM.WriteSetting("PWD", CryptorEngine.Encrypt(CommonDoubleM._sPWD, true));
                }
               // CommonDoubleM.LoadAppConfig();
            }
            catch (Exception ex)
            {
                CommonDoubleM.LogDM(ex.Message);
            }
        }

        private void ProxySettingInit()
        {
            if (chkProxy.Checked)
            {
                pnlProxy.BorderStyle = BorderStyle.Fixed3D;
                pnlProxy.Enabled = true;
            }
            else
            {
                pnlProxy.BorderStyle = BorderStyle.FixedSingle;
                pnlProxy.Enabled = false;
            }
        }

        private void toolStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string s;
            switch (e.ClickedItem.Text.Trim('&'))
            {
                case "Print": //Printing Gridview
                    
                    if(this.MdiChildren.Length >0)
                        foreach (Control c in this.ActiveMdiChild.Controls)
                        {
                            if (c is DataGridView)
                                PrintDGV.Print_DataGridView((DataGridView)c);
                        }

                    break;
                case "Excel": //Export to Excel

                    //ExcelDoubleM.WriteToExcel( ds,"aaa",true);   
                    MessageBox.Show("Open Excel");
                    

                    break;
            }
        }

        /*Printer Setup*/
        //private bool SetupThePrinting(string sTitle, DataGridView dgv)
        //{
        //    PrintDialog MyPrintDialog = new PrintDialog();
        //    MyPrintDialog.AllowCurrentPage = false;
        //    MyPrintDialog.AllowPrintToFile = false;
        //    MyPrintDialog.AllowSelection = false;
        //    MyPrintDialog.AllowSomePages = false;
        //    MyPrintDialog.PrintToFile = false;
        //    MyPrintDialog.ShowHelp = false;
        //    MyPrintDialog.ShowNetwork = false;

        //    if (MyPrintDialog.ShowDialog() != DialogResult.OK)
        //        return false;

        //    prtDocMain.DocumentName = sTitle;
        //    prtDocMain.PrinterSettings = MyPrintDialog.PrinterSettings;
        //    prtDocMain.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
        //    prtDocMain.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(20, 20, 40, 40);

        //    if (MessageBox.Show("Do you want the report to be centered on the page", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        _DataGridViewPrinter = new DataGridViewPrinter(dgv, prtDocMain, true, true, "Customers", new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
        //    else
        //        _DataGridViewPrinter = new DataGridViewPrinter(dgv, prtDocMain, false, true, "Customers", new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

        //    return true;
        //}

        //http://www.codeproject.com/cs/miscctrl/dynformpartii.asp
        //http://www.csharphelp.com/archives2/archive408.html
        //http://www.syncfusion.com/FAQ/winforms/default.aspx#87

    }
}

/*
 *     //**************************************
    //     
    // Name: PrevInstance for C#
    // Description:A easy class for checking
    //     previous instance with C# and then attat
    //     ch to the previous instance just like pr
    //     evious vb applikations.
    // By: Patrik Johansson
    //
    // Inputs:Application name
    //
    // Returns:true or false
    //
    //This code is copyrighted and has    // limited warranties.Please see http://
    //     www.Planet-Source-Code.com/vb/scripts/Sh
    //     owCode.asp?txtCodeId=5203&lngWId=10    //for details.    //**************************************
    //     
    
    using System;
    using System.Runtime.InteropServices;
    namespace Previnstance
    {
    	public class PrevInstance
    	{
    		[DllImport("user32.dll")]
    		[return: MarshalAs(UnmanagedType.Bool)]
    		private static extern bool SetForegroundWindow(IntPtr hWnd);
    		public PrevInstance(){}
    		public static bool PreviousInstance(string name){
    			System.Diagnostics.Process[] processes=System.Diagnostics.Process.GetProcessesByName(name);
    			foreach(System.Diagnostics.Process proc in processes){
    				if(proc.MainWindowHandle!=(IntPtr)0){
    					SetForegroundWindow(proc.MainWindowHandle);
    					return true;
    				}
    			}
    			return false;
    		}
    	}
    }

 */

