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
        private frmMngStockPrice pMngSPrice;
        private frmAc pAccount;
        //private DataGridViewPrinter _DataGridViewPrinter;

        public frmMain()
        {
            InitializeComponent();
            CommonDoubleM.LoadAppConfig();
            _pdalStock = new DALDoubleM(lblDMMsg, pBarDM);
            _pdalStock.MarqueeUpdate();
            CommonDoubleM._pdalStock1 = new DALDoubleM(lblDMMsg, pBarDM);
            //////////////////////////
            _groupPaneBar.Add(CreateButton("Basics", CommonDoubleM.Basics.Length), "Basics  ", imglstMain.Images[0], true);
            _groupPaneBar.Add(CreateButton("Online", CommonDoubleM.Online.Length), "Online  ", imglstMain.Images[1], true);
            _groupPaneBar.Add(CreateButton("Analysis", CommonDoubleM.Analysis.Length), "Analysis  ", imglstMain.Images[2], true);
            _groupPaneBar.Add(CreateButton("Views", CommonDoubleM.Views.Length), "Views  ", imglstMain.Images[3], true);
            _groupPaneBar.Add(CreateButton("Configuration", CommonDoubleM.Configuration.Length), "Configuration  ", imglstMain.Images[4], true);
            _groupPaneBar.Add(CreateButton("About", CommonDoubleM.About.Length), " About  ", imglstMain.Images[5], true);


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
            LoadStock();
            ddlPeriod.DataSource = DoubleM.CommonDoubleM.Period;
            chkAllStocks.Checked = true;
            rdobPeriod.Checked = true;

            pTradings = new frmTradingHist(_pdalStock);
            pAccount = new frmAc(_pdalStock);
        }

        #region "Form Events"

        private void frmMain_Load(object sender, EventArgs e)
        {
            //scrtxtStock.BackgroundBrush =
            //    new LinearGradientBrush(this.scrtxtStock.ClientRectangle,
            //    Color.Red, Color.Blue,
            //    LinearGradientMode.Horizontal);
            //this.Icon = new Icon(this.GetType().Assembly.GetManifestResourceStream("DoubleM.Pics.der.ico")); //Also working
            this.Icon = Properties.Resources.der;
            SStripMainlblDate.Text = DateTime.Now.ToLongDateString();
            SStripMainlblDate.ToolTipText = "Started @ "+DateTime.Now.ToLongTimeString();
            
            pBarDM.Visible = false;
            dtPKStart.Value = DateTime.Now.Date;
            dtpSince.Value = DateTime.Now.Date;

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

        internal void MenuBtn_Click(object sender, EventArgs e)
        {
            string s = ((Button)sender).Text + ((Button)sender).Name;

            switch (s)
            {
                case "Work Sheet":
                    frmWorkSheet pWS = new frmWorkSheet(_pdalStock);
                    pWS.MdiParent = this;
                    pWS.Show();
                    //frmWorkSheet.ActiveForm.Activate();
                    break;
                case "Manage Stock Rates":
                    if (pMngSPrice == null) pMngSPrice = new frmMngStockPrice(_pdalStock);
                    pMngSPrice.MdiParent = this;
                    pMngSPrice.Show();
                    pMngSPrice.Select();
                    break;
                case "Manage Tradings":
                    MessageBox.Show("Features - 'Manage Tradings' will be included in next version of DoubleM","Coming with next version",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    break;
                case "Trade Book":
                    ((Button)sender).Enabled = false;
                    tbMain.SelectedIndex = 1;
                    ((Button)sender).Enabled = true;
                    break;
                case "Profit && Loss":
                    tbMain.SelectedIndex = 0;
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
                case "Online DB Update":
                    ((Button)sender).Enabled = false;
                    UpdateOnline();
                    scrtxtStock.ScrollText = CommonDoubleM.MarqueeString;
                    ((Button)sender).Enabled = true;
                    break;
                case "BSE direct":
                    frmReadBSE pBSE = new frmReadBSE(_pdalStock);
                    pBSE.MdiParent = this;
                    pBSE.Show();
                    break;
                case "Yahoo direct":
                    frmReadYF_MR pYF = new frmReadYF_MR(_pdalStock, "Yahoo");
                    pYF.MdiParent = this;
                    pYF.Show();
                    break;
                case "Rediff direct":
                    frmReadYF_MR pMR = new frmReadYF_MR(_pdalStock, "Rediff");
                    pMR.MdiParent = this;
                    pMR.Show();
                    break;
                case "Update Traker":
                    ((Button)sender).Enabled = false;
                    _pdalStock.MarqueeUpdate();
                    scrtxtStock.ScrollText = CommonDoubleM.MarqueeString;
                    ((Button)sender).Enabled = true;
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
                case "Manage Scripts":
                    //frmStocks pStocks = new frmStocks(_pdalStock);
                    frmMngStocks pStocks = new frmMngStocks(_pdalStock);
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
                case "About":
                    ShowAbout();
                    break;
                case "Credit":
                    MessageBox.Show(".:" + Application.ProductName +
                    ":. - Market Manager\n\t\t Ver " +
                    Application.ProductVersion +
                    " \n\nAll rights reserved by OM Soft Pvt. Ltd." +
                    "\nContact:deepak.shaw@gmail.com for further details.",
                    "Rights Reserved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                case "Licence":
                    MessageBox.Show(".:" + Application.ProductName +
                        ":. - Market Manager\n\t\t Ver " +
                        Application.ProductVersion +
                        " \n\nIt's Beta release." +
                        "\nPlease let us know your feedback:-\rdeepak.shaw@gmail.com",
                        "Free Licence", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                default:
                    break;
            }
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
            LoadStock();
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            
            string sStockIDs = "", sStocksDates="", sWhere = "", sSQL = "";

            btnStart.Enabled = false;
            Cursor = Cursors.WaitCursor;

            //Incase no selection assume all stocks selected
            if (lstBStock.SelectedItems.Count == 0) 
            {
                sWhere = " (TTrade.TradeOn" + chkAfterBefore.Text + "=#" + dtPKStart.Value.AddHours(23).ToString("dd/MMM/yyyy") + "#)";
            }
            else if (lstBStock.SelectedItems.Count > 0)
            {
                sStocksDates = " AND (TTrade.TradeOn" + chkAfterBefore.Text + "=#" + dtPKStart.Value.AddHours(23).ToString() + "#)";

                for (int i = 0; i <= lstBStock.SelectedItems.Count - 1; i++)
                {
                    DataRowView ln = lstBStock.SelectedItems[i] as DataRowView;
                    //MessageBox.Show(ln[0].ToString());
                    sStockIDs = "TStockName.StockID=" + ln[0].ToString();
                    sWhere += "((" + sStockIDs + ")" + sStocksDates + ") OR ";
                }

                sWhere = sWhere.Substring(0, sWhere.Length - 4);
            }
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
            Cursor = Cursors.Default;
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


        private void toolStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = null;
            Form activeChildForm = this.ActiveMdiChild;

            switch (e.ClickedItem.Name)
            {
                case "tsbtnPrint": //Printing Gridview
                    dgv = getDGVCOntrol();
                    if (dgv != null)
                        PrintDGV.Print_DataGridView(dgv);
                    else
                        lblDMMsg.Text = "There is no grid found on Active form";
                    break;

                case "tsbtnExcel": //Export to Excel
                    dgv = getDGVCOntrol();
                    if (dgv != null)
                     {
                         saveFileDlgExport.Title = "Save file [Export to Excel]";
                        saveFileDlgExport.Filter = "Excel (*.xls)|*.xls|XLSX (*.xlsx)|*.xlsx|XLSB (*.xlsb)|*.xlsb|XLSM (*.xlsm)|*.xlsm";
                        saveFileDlgExport.FileName = CommonDoubleM._sDefaultExcelFile;
                        
                        if (saveFileDlgExport.ShowDialog() == DialogResult.OK)
                        {
                                System.IO.FileInfo f = new System.IO.FileInfo(saveFileDlgExport.FileName);
                                switch (f.Extension.ToLower())
                                {
                                    case ".xls":
                                    case "xlsx":
                                    case "xlsb":
                                    case "xlsm":
                                        DataTable dt = new DataTable();
                                        dt = CommonDoubleM.getDataTable(dgv);
                                        DoubleM.ExportExcel.Export(f.ToString(),dt , dt.TableName);
                                        break;
                                    default:
                                        MessageBox.Show("Unable to Export - Invalid file type");
                                        break;
                                }
                        }
                    }
                    else
                        lblDMMsg.Text = "There is no grid found on Active form";

                        break;
                case "tsbtnBSE":
                        break;
                case "tsbtnNSE":
                    break;
                        


                                       
            }
        }
        private void tsbtnDownload_Click(object sender, EventArgs e)
        {
            frmDateTime pfrmDT;
            long DownloadBytes = 0;

            ToolStripMenuItem obj = (ToolStripMenuItem)sender;
            //Console.WriteLine(obj.Name);
            switch (obj.Name)
            {
                case "tsbtnDownloadBSE":
                    pfrmDT = new frmDateTime("Historical date for BSE", false, true);
                    if (pfrmDT.ShowDialog() != DialogResult.OK)
                    {
                        pfrmDT.Dispose();
                        break;
                    }
                    pfrmDT.Dispose();

                    saveFileDlgExport.Title = "Save file [Historical Data from BSE]";
                    saveFileDlgExport.FileName = string.Empty;
                    saveFileDlgExport.Filter = "Zip (*.zip)|*.zip";
                    saveFileDlgExport.InitialDirectory = CommonDoubleM._DoubleMPath;
                    if (saveFileDlgExport.ShowDialog() == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;
                        DownloadBytes = CommonDoubleM.getHitoricalData((short)CommonDoubleM.StockExchange.BSE,
                            saveFileDlgExport.FileName, pBarDM);
                        if (DownloadBytes > -1)
                            lblDMMsg.Text = "Download " + CommonDoubleM.FormatBytes(DownloadBytes);
                        else
                            lblDMMsg.Text = "No data available for selected date, please change the date & try again..";
                        Cursor = Cursors.Default;
                    }
                    break;

                case "tsbtnDownloadNSE":
                    pfrmDT = new frmDateTime("Historical date for NSE", false, true);
                    if (pfrmDT.ShowDialog() != DialogResult.OK)
                    {
                        pfrmDT.Dispose();
                        break;
                    }
                    pfrmDT.Dispose();

                    saveFileDlgExport.Title = "Save file [Historical Data from NSE]";
                    saveFileDlgExport.FileName = string.Empty;
                    saveFileDlgExport.Filter = "CSV (*.csv)|*.csv";
                    saveFileDlgExport.InitialDirectory = CommonDoubleM._DoubleMPath;
                    if (saveFileDlgExport.ShowDialog() == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;
                        DownloadBytes = CommonDoubleM.getHitoricalData((short)CommonDoubleM.StockExchange.NSE,
                                saveFileDlgExport.FileName, pBarDM);
                        if (DownloadBytes > -1)
                            lblDMMsg.Text = "Download " + CommonDoubleM.FormatBytes(DownloadBytes);
                        else
                            lblDMMsg.Text = "No data available for selected date, please change the date & try again..";
                        Cursor = Cursors.Default;
                    }
                    break;
                case "tsbtnDownloadY":
                    frmYahoo pYahoo = new frmYahoo(_pdalStock,lblDMMsg,pBarDM);
                    pYahoo.ShowDialog();
                    break;
            }

        }
        private void tsbtnImports_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;

            ///////////// Import Rate from external file//////////////////
            frmDataImport pDataImport = null;

            switch (obj.Name)
            {
                case "tsbtnImportSingle":
                    pDataImport = new frmDataImport(_pdalStock, (short)frmDataImport.ImportWhat.SingleRates);
                    break;
                case "tsbtnImportMultiple":
                    pDataImport = new frmDataImport(_pdalStock, (short)frmDataImport.ImportWhat.MultipleRates);
                    break;
            }
            pDataImport.MdiParent = this;
            pDataImport.Show();
        }

        private void chkAllStocks_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAllStocks.Checked)
            {
                lstBStocks.Enabled = true;
                LoadStocks();
            }
            else
            {
                lstBStocks.Enabled = false;
            }
        }

        private void rdobPeriod_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            //MessageBox.Show(rd.Name);
            /*Make all Switchable control inviible*/
            ddlPeriod.Visible = false;
            chkFromTo.Visible = false;
            dtpSince.Visible = false;
            dtpTo.Visible = false;

            /*Make the visible again on user choise*/
            switch (rd.Name)
            {
                case "rdobPeriod":
                    if (rdobPeriod.Checked)
                        ddlPeriod.Visible = true;
                    break;
                case "rdobDateStarts":
                    if (rdobDateStarts.Checked)
                    {
                        dtpSince.Visible = true;
                        chkFromTo.Visible = true;
                    }
                    break;
                case "rdobDateIn":
                    if (rdobDateIn.Checked)
                    {
                        dtpSince.Visible = true;
                        dtpTo.Visible = true;
                    }
                    break;
                default :
                    break;

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sSID="", sTime="";
            DataTable dtAccount = null;
            DataView dvAC = _pdalStock.Accouting_ProfitLoss(DateTime.MinValue);
            
            

            if (dvAC != null)
            {
                if (!chkAllStocks.Checked)
                {
                    if (lstBStocks.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i <= lstBStocks.SelectedItems.Count - 1; i++)
                        {
                            DataRowView ln = lstBStocks.SelectedItems[i] as DataRowView;
                            sSID += ln[0].ToString() + ",";
                        }
                        sSID = "StockID IN (" + sSID.Substring(0, sSID.Length - 1) + ")";
                    }
                }
                if (ddlPeriod.Visible)
                {
                    if(ddlPeriod.SelectedIndex==0) //Last week
                    sTime = "(TradeOn >=#" + DateTime.Now.AddDays(-7).ToShortDateString()+"#)";
                    else if(ddlPeriod.SelectedIndex==1) //Last 2 Weeks
                        sTime = "(TradeOn >=#" + DateTime.Now.AddDays(-15).ToShortDateString() + "#)";
                    else if (ddlPeriod.SelectedIndex == 2) //This month
                        sTime = "(TradeOn >=#" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString() + "#)";
                    else if (ddlPeriod.SelectedIndex == 3) //Last month
                        sTime = "(TradeOn >=#" + new DateTime(DateTime.Now.Year, DateTime.Now.Month-1, 1).ToShortDateString() + "#)";
                    else if (ddlPeriod.SelectedIndex == 4) //This quater
                        sTime = "(TradeOn >=#" + new DateTime(DateTime.Now.Year,((3*(DateTime.Now.Month - 1) / 3 + 1)-1), 1).ToShortDateString() + "#)";
                    else if (ddlPeriod.SelectedIndex == 5) //6 Months
                        sTime = "(TradeOn >=#" + DateTime.Now.AddMonths(-6).ToShortDateString() + "#)";
                    else if (ddlPeriod.SelectedIndex ==6) //This year
                        //sTime = "(TradeOn >=#" + DateTime.Now.AddYears(-1).ToShortDateString() + "#)";
                        sTime = "(TradeOn >=#" + new DateTime(DateTime.Now.Year, 1, 1).ToShortDateString() + "#)";
                    else if (ddlPeriod.SelectedIndex == 7) //last year
                        sTime = "(TradeOn >=#" + DateTime.Now.AddYears(-1).ToShortDateString() + "#)";
                    
                }
                else if (chkFromTo.Visible)
                {
                    sTime = "(TradeOn " + (string)(chkFromTo.Checked==true ? "<=#" : ">=#") + dtpSince.Value.ToShortDateString() + "#)";
                }
                else if (dtpTo.Visible)
                {
                    sTime = "(TradeOn >= #"+ dtpSince.Value.ToShortDateString()+" #) AND (TradeOn <= #"+dtpTo.Value.ToShortDateString()+"#)";
                }
                //dvAC.RowFilter = "(TradeOn >= #12/31/2008#) AND (TradeOn <= #3/1/2009#)";
                dvAC.RowFilter = (sSID=="")?sTime: sSID + " AND " + sTime;
                dtAccount = dvAC.ToTable();
            }
            btnSearch.Enabled = false;
            Cursor = Cursors.WaitCursor;
            if (pAccount == null) pAccount = new frmAc(_pdalStock);
            pAccount.MdiParent = this;
            pAccount.Show();
            pAccount.Select();
            pAccount.FillAc(dtAccount);
            btnSearch.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void chkFromTo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFromTo.Checked)
                chkFromTo.Image = Properties.Resources.Reverse;
            else
                chkFromTo.Image = global::DoubleM.Properties.Resources.FastForward;
        }
        private void SStripMainlblAbout_Click(object sender, EventArgs e)
        {
            ShowAbout(); 
        }

        #endregion
        #region "Private Methods"
        /*Making a Array control event*/
        private void ShowAbout()
        {
                    MessageBox.Show(".:"+Application.ProductName+
            ":. - Market Manager\n\t\t Ver " +
            Application.ProductVersion +
            " \n\nDeveloped and designed by OM Soft Pvt. Ltd."+
            "\nPlease let us know your feedback:-\rdeepak.shaw@gmail.com",
            "About", MessageBoxButtons.OK, MessageBoxIcon.None);

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
        private void UpdateOnline()
        {
            Cursor = Cursors.WaitCursor;
            if (MessageBox.Show("Are you sure to update stock latest price.."
    , "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _pdalStock.UpdateStockprice(false);
                _pdalStock.MarqueeUpdate();
                scrtxtStock.ScrollText = CommonDoubleM.MarqueeString;
            }
            Cursor = Cursors.Default;
        }
        private void LoadStock()
        {
            string sDM = "", sVM = "";
            DataView dvStock = null;
            Cursor = Cursors.WaitCursor;
            dvStock = _pdalStock.Stocks(ref sDM, ref sVM, (short)CommonDoubleM.StockType.AllStocks);
            /*List box to go for menu item Trade Books*/
            lstBStock.DataSource = dvStock;
            lstBStock.DisplayMember = sDM;
            lstBStock.ValueMember = sVM;
            if (lstBStock.Items.Count > 0)
                lstBStock.SetSelected(lstBStock.SelectedIndex, false); //Unselect default selected item in list box
            Cursor = Cursors.Default;
        }
        private void LoadStocks()
        {

            string sDMs = "", sVMs = "";
            DataView dvStocks = null;

            Cursor = Cursors.WaitCursor;
            //dtpOndate.Value = DateTime.Now.Date;

            dvStocks = _pdalStock.Stocks(ref sDMs, ref sVMs, 8);
            /*List box to go for menu item Profit && Loss*/
            lstBStocks.DataSource = dvStocks;
            lstBStocks.DisplayMember = sDMs;
            lstBStocks.ValueMember = sVMs;
            if (lstBStocks.Items.Count > 0)
                lstBStocks.SetSelected(lstBStocks.SelectedIndex, false); //Unselect default selected item in list box

            Cursor = Cursors.Default;
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
        private DataGridView getDGVCOntrol()
        {
            //Control c = null;
            Form activeChildForm = this.ActiveMdiChild;

            if (activeChildForm != null)
                foreach (Control c in activeChildForm.Controls)
                {
                    if (c is DataGridView)
                        return (DataGridView)c;
                }
            return null;
        }
        #endregion








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

