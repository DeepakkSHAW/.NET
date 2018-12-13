using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmReadBSE : Form
    {
        private DALDoubleM _pDalBSE;
        private frmMain _theParent;
        private DataView _dtWatchList;
        private int iCount = 0;
        private int iMaxTry = 0;
        private Boolean blnBbaseRow;
        private Boolean blnOnce = false;
        private Boolean blnSelectedUpdate = false;
        public frmReadBSE(object pdalSNew)
        {
            InitializeComponent();
            _pDalBSE = (DoubleM.DALDoubleM)pdalSNew;
        }

        private void frmReadBSE_Load(object sender, EventArgs e)
        {
            _theParent = (frmMain)this.ParentForm; //To access MDI parent control
            
            WatchList_Design();
            _theParent.lblDMMsg.Text = "Script loading..";
            tsbtnActiveScript.Checked = true;

            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(dgViewQ);
            cs.MaxHeight = 1000;
            cs.Width = 130;

            //Automatic push for loading from BSE 
            //toolStripButton_UpdateAllfromBSE.PerformClick();
        }
        
        private void fillGrid()
        {
            int iPBValue = 0;
            _theParent.pBarDM.Visible = true;
            Application.DoEvents();
            Cursor = Cursors.WaitCursor;
            try
            {
                _theParent.pBarDM.Maximum = _dtWatchList.Table.Rows.Count;
                blnBbaseRow = false;
                dgViewQ.Rows.Clear();
                //for (int ii = 0; ii < dtWatchList.Table.Rows.Count; ii++)
                foreach (DataRow dtRow in _dtWatchList.Table.Rows)
                {
                    dgViewQ.Rows.Add();
                    dgViewQ[10, iPBValue].Value = dtRow[0].ToString(); //SID
                    dgViewQ[11, iPBValue].Value = dtRow[1].ToString(); //BSEID
                    dgViewQ[0, iPBValue].Value = dtRow[2].ToString(); // Stock Name
                    dgViewQ[1, iPBValue].Value = Convert.ToInt32(dtRow[3].ToString()); // Quantity
                    dgViewQ[7, iPBValue].Value = Convert.ToDouble(dtRow[4].ToString()); //T Paid
                    dgViewQ[3, iPBValue].Value = Convert.ToDouble(dtRow[5].ToString()); //Avg
                    dgViewQ[9, iPBValue].Value = (dtRow[6].ToString() == "True") ? "Y" : "N"; // Acitive or Inactive

                    _theParent.pBarDM.Value = iPBValue;
                    iPBValue++;
                    //if (iPBValue == 5) break; //Just for testing only 2 rows
                }
                _theParent.lblDMMsg.Text = iPBValue.ToString() + " Script loaded.";
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }
            finally
            {
                _theParent.pBarDM.Visible = false;
                Cursor = Cursors.Default;
            }
        }
        
        private void WatchList_Design()
        {
            try
            {
                dgViewQ.Columns.Add("Stock Name", "Stock Name");
                dgViewQ.Columns.Add("Shrs", "Shrs");
                dgViewQ.Columns.Add("Current", "Current");
                dgViewQ.Columns.Add("Avg", "Avg");
                dgViewQ.Columns.Add("%", "%");
                dgViewQ.Columns.Add("C%", "% Chg");
                dgViewQ.Columns.Add("T Gain", "T Gain");
                dgViewQ.Columns.Add("Paid", "Paid");
                dgViewQ.Columns.Add("Last update", "Last update");
                dgViewQ.Columns.Add("Active", "Active");
                dgViewQ.Columns.Add("SID", "SID");
                dgViewQ.Columns.Add("BSEID", "BSEID");

                /* Data grid format display*/

                dgViewQ.Columns[0].Width = 180; //Name
                dgViewQ.Columns[1].Width = 50; //Shrs - Total Qualtity
                dgViewQ.Columns[2].Width = 50; //Current Rate - Should be coming from BSE
                dgViewQ.Columns[3].Width = 60; //Avg Cost Price
                dgViewQ.Columns[4].Width = 50; // % Gain
                dgViewQ.Columns[5].Width = 60; // % Change

                dgViewQ.Columns[6].Width = 60; //T Gain need to be calculated
                dgViewQ.Columns[7].Width = 60; //Ammount Paid for the currect quantity
                dgViewQ.Columns[8].Width = 125; // Time on BSE
                dgViewQ.Columns[9].Width = 30; // Active

                dgViewQ.RowHeadersWidth = 60;

                dgViewQ.Columns[9].Visible = false; // Active
                dgViewQ.Columns[10].Visible = false; // SID
                dgViewQ.Columns[11].Visible = false; // BSE

                dgViewQ.Columns[8].DefaultCellStyle.Format = CommonDoubleM.lngDate;
                dgViewQ.Columns[2].DefaultCellStyle.Format = "0.00";
                dgViewQ.Columns[3].DefaultCellStyle.Format = "0.00";
                dgViewQ.Columns[4].DefaultCellStyle.Format = "0.00";
                dgViewQ.Columns[5].DefaultCellStyle.Format = "0.00";
                dgViewQ.Columns[6].DefaultCellStyle.Format = "0.00";
                dgViewQ.Columns[7].DefaultCellStyle.Format = "0.00";
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }

        }

        private void tmrBSE_Tick(object sender, EventArgs e)
        {
            tmrBSE.Enabled = false;
            readBSE(-1);
        }
        
        private void readBSE(int iOnce)
        {
            try
            {
                var document = wbBSE.Document;
                string sBSECode = "";
                DateTime latestDate = DateTime.Today;
                double latestPrice = 0.0, PercentChange = 0.0;

                EnableForm(false);
                //Single row update
                if (iOnce >= 0)
                    iCount = iOnce;

                    //Reading BSE code from Database
                    if (iCount >= dgViewQ.Rows.Count - 1)
                    {
                        iCount = 0;
                        tmrBSE.Enabled = false;
                        blnSelectedUpdate = false;
                        EnableForm(true);
                        return;
                    }
                    else
                    {
                        //sBSECode = _dtWatchList[iCount][6].ToString(); 
                        if (blnSelectedUpdate)
                        {
                            while (dgViewQ.Rows[iCount].Selected == false)
                            {
                                //Reading BSE code from Database
                                if (iCount >= dgViewQ.Rows.Count - 1) //Checking last recored
                                {
                                    iCount = 0;
                                    tmrBSE.Enabled = false;
                                    blnSelectedUpdate = false;
                                    EnableForm(true);
                                    return;
                                }
                                iCount++;
                            }
                        }
                        if (dgViewQ[11, iCount].Value != null)
                            sBSECode = dgViewQ[11, iCount].Value.ToString();
                        else
                        {
                            _theParent.lblDMMsg.Text = "Stock " + dgViewQ[0, iCount].Value+" does not have BSE code";
                            if (blnOnce)
                            { iCount = 0; blnOnce = false; tmrBSE.Enabled = false; EnableForm(true); return; }
                            else { iCount++; tmrBSE.Enabled = true; return; }
                        }

                    }

                //No URL for very fast call, initialization for WebBroswer
                if (wbBSE.Url == null)
                {
                    wbBSE.Url = new Uri("http://www.bseindia.com/stockreach/stockreach.htm?scripcd=" + sBSECode);
                    return;
                }

                string updateInfo = document.Body.InnerText.ToString();
                rtbBSE.Text = updateInfo;
                if (rtbBSE.Find(sBSECode) > 0)
                {
                    iMaxTry = 0; tmrBSE.Interval = 500;
                    _theParent.lblDMMsg.Text = "Loading latest data for " + dgViewQ[0, iCount].Value;
                    if (FilterBSE(rtbBSE.Lines, out latestDate, out latestPrice, out PercentChange))
                    {
                        //Auto Scroll to Row updating
                        if(!dgViewQ.Rows[iCount+1].Displayed) 
                            dgViewQ.FirstDisplayedScrollingRowIndex = iCount;
                        //unselect the corrent row
                        dgViewQ.Rows[iCount].Selected = false; 

                        dgViewQ[2, iCount].Value = latestPrice; // Latest Price
                        dgViewQ[5, iCount].Value = PercentChange; // Latest % Change
                        dgViewQ[8, iCount].Value = latestDate.ToString(CommonDoubleM.lngDate); // Latest Price Date
                        _theParent.lblDMMsg.Text += "  [" + latestPrice + "] Change: [" + PercentChange.ToString() + "%]";
                    }
                    //rtbBSE.Text = "found" + rtbBSE.Text;
                    if (blnOnce)
                    {
                        iCount = 0; blnOnce = false;
                        tmrBSE.Enabled = false;
                        EnableForm(true);
                    }
                    else
                    {
                        iCount++;
                        tmrBSE.Enabled = true;
                    }
                }
                else
                {
                    wbBSE.Url = new Uri("http://www.bseindia.com/stockreach/stockreach.htm?scripcd=" + sBSECode);
                    
                    iMaxTry++;

                    _theParent.lblDMMsg.Text = string.Format("Attempting [{0}] times to download latest data for {1}",
                        iMaxTry, dgViewQ[0, iCount].Value);
                    CommonDoubleM.LogDM(_theParent.lblDMMsg.Text);

                    //Maximum attempt to download rates from BSE
                    if (iMaxTry >= CommonDoubleM.MaxFailedAttemptAllowed) 
                    {
                        //unselect the corrent row
                        dgViewQ.Rows[iCount].Selected = false;
                        //each failed attempt increase loading interval by 0.5 sec
                        tmrBSE.Interval = tmrBSE.Interval + 500; 
                        iMaxTry = 0; iCount++; 
                    }
                    //wbBSE.Refresh();
                }
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }

        }

        private void wbBSE_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (wbBSE.Document.Body.InnerText != null)
                tmrBSE.Enabled = true;
        }

        /*
         "The out keyword causes arguments to be passed by reference. 
         This is similar to the ref keyword, except that ref requires that the variable 
         be initialized before being passed." 
         Here is the detailed explaination:
         http://geekswithblogs.net/ftom/archive/2008/09/10/c-and-the-difference-between-out-and-ref.aspx
         */
        private bool FilterBSE(string[] sBSE, out DateTime dtLastupdate, out double dblPrice, out double dblPrivouseClosing)
        {
            bool blnFn = false;
            int i=0, Notnull=0;
            string sSearch;
            dtLastupdate = System.DateTime.Now;
            dblPrice=0;
            dblPrivouseClosing = 0;

            try
            {
                for (i = 0; i < sBSE.Length; i++)
                {
                    sSearch = sBSE[i].Trim();

                    if (sSearch.Contains("Last update"))//Looking for Last update
                    {
                        string tmp = sSearch;
                        tmp = tmp.Remove(0, "Last update".Length + 3);
                        tmp = tmp.Replace("'", "");
                        dtLastupdate = System.DateTime.ParseExact(tmp, "HH:mm, dd MMM yy", null);
                        //Debug.WriteLine(dtLastupdate);
                    }
                    else if (sSearch.Contains("LTP"))//Looking for Last price
                    {
                        string[] sPrice = sBSE[i + 1].Trim().Split('/',' ');
                        //Debug.WriteLine(sPrice.Length);
                        if (sPrice.Length > 2)
                        {
                            for (int j = 0; j < sPrice.Length; j++)
                            {
                               if(sPrice[j] != "")
                               {
                                   Notnull++;
                                if (System.Text.RegularExpressions.Regex.IsMatch(sPrice[j].Trim(), @"^-?(?:0|[1-9][0-9]*)(?:\.[0-9]+)?$"))
                                {
                                    if(Notnull ==1) //Latest price
                                        dblPrice = Convert.ToDouble(sPrice[j].Trim());
                                    if (Notnull == 4) //Percent Change
                                    {
                                        dblPrivouseClosing = Convert.ToDouble(sPrice[j].Trim());
                                        break;
                                    }

                                }
                                   
                               }
                            }
                            break;
                            //dblPrivouseClosing = Convert.ToDouble(sPrice[1].Trim());
                            //System.Text.RegularExpressions.Regex.IsMatch(inputString, "^[0-9]+$"); 
                        }

                    }
                }
                blnFn = true;
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
                blnFn = false;
            }
            return blnFn;
        }

        private void dgViewQ_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == dgViewQ.RowCount-1) return;

                if (e.ColumnIndex == 2)
                {
                    //Only got cleaning
                    if (dgViewQ[2, e.RowIndex].Value.ToString() == string.Empty)
                    {
                        dgViewQ[4, e.RowIndex].Value = "";
                        dgViewQ[6, e.RowIndex].Value = "";
                        dgViewQ[4, e.RowIndex].Style.BackColor = dgViewQ[3, e.RowIndex].Style.BackColor;
                        return;
                    }
                    double SP = 0.0, CP = 0.0, dicPerct=0.0, Profit=0.0;
                    //System.Diagnostics.Debug.WriteLine(e.RowIndex);
                    //Total Gain
                    SP = Convert.ToDouble(dgViewQ[1, e.RowIndex].Value) * Convert.ToDouble(dgViewQ[2, e.RowIndex].Value);
                    CP = Convert.ToDouble(dgViewQ[7, e.RowIndex].Value);
                    Profit = SP - CP;
                    dgViewQ[6, e.RowIndex].Value = Profit;
                    if (CP > 0)
                    {
                        //Calc %
                        dicPerct = (Profit / CP)  * 100;
                        dgViewQ[4, e.RowIndex].Value = dicPerct;
                        dgViewQ[4, e.RowIndex].Style.BackColor = Color.FromArgb(CommonDoubleM.GetColorcode((double)dicPerct));
                    }
                    else
                        dgViewQ[4, e.RowIndex].Value = 0.0;
                }
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }
        }
        
        private void dgViewQ_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int i = 0;

            if (!blnBbaseRow)
            {
                dgViewQ.Rows[e.RowIndex].HeaderCell.Value = "##";
                blnBbaseRow = true;
            }
            else
            {
                dgViewQ.Rows[e.RowIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                i = e.RowIndex + 1;
                dgViewQ.Rows[e.RowIndex].HeaderCell.Value = i.ToString();
            }
        }
        
        private void dgViewQ_SelectionChanged(object sender, EventArgs e)
        {
            int iLastRow = 0;
            double dblSelTotal = 0, dblSelGain = 0, dblSelValue = 0;

            iLastRow = dgViewQ.RowCount - 1;

            if (iLastRow > 2)
                if (dgViewQ.SelectedRows.Count > 1)
                {

                    foreach (DataGridViewRow dtRowSelected in dgViewQ.SelectedRows)
                    {
                        if (iLastRow != dtRowSelected.Index)
                        {
                            dblSelTotal = dblSelTotal + Convert.ToDouble(dgViewQ[7, dtRowSelected.Index].Value);
                            dblSelValue = dblSelValue + Convert.ToDouble(dgViewQ[1, dtRowSelected.Index].Value) * Convert.ToDouble(dgViewQ[2, dtRowSelected.Index].Value);
                            dblSelGain = dblSelGain + Convert.ToDouble(dgViewQ[6, dtRowSelected.Index].Value);
                        }
                    }

                    dgViewQ[0, iLastRow].Value = "Summary of selected rows";
                    dgViewQ[2, iLastRow].Value = dblSelValue;
                    if(dblSelTotal != 0)
                    {
                    dgViewQ[4, iLastRow].Value = decimal.Round(((decimal)(dblSelGain / dblSelTotal) * 100), 2);
                    }
                    dgViewQ[6, iLastRow].Value = dblSelGain;
                    dgViewQ[7, iLastRow].Value = dblSelTotal;
                }
                else
                {
                    dgViewQ[0, iLastRow].Value = "Summary";
                    dgViewQ[2, iLastRow].Value = 0;//dblValue;
                    dgViewQ[4, iLastRow].Value = 0;//decimal.Round(((decimal)((dblValue / dblTotal) - 1) * 100), 2);
                    dgViewQ[6, iLastRow].Value = 0;//dblGain;
                    dgViewQ[7, iLastRow].Value = 0;//dblTotal;

                }
        }

        private void dgViewQ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dgViewQ.RowCount - 1)
            {
                CommonDoubleM.LogDM("Downloading rates for " + dgViewQ[0, e.RowIndex].Value.ToString());
                blnOnce = true;
                EnableForm(false);
                readBSE(e.RowIndex);
            }
            else
                _theParent.lblDMMsg.Text = "Please double click on valid rows..";
        }

        private void toolStripBSE_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {
            int iDgvRow = 0;


            switch (e.ClickedItem.Name.Trim('&'))
            {
                case "toolStripButton_UpdateAllfromBSE":
                    /******************************/
                    /* Loadind from BSE           */
                    /******************************/
                    if (MessageBox.Show("Loading data direct from BSE may take longer time...\n" +
                        "The minimum estimated time would be around  \n[" + TimeSpan.FromSeconds(dgViewQ.RowCount * 1.25).ToString() + "] H." +
                        "\nDo you want to Continue", "Delay Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _theParent.lblDMMsg.Text = "Loading from BSE..";
                        Application.DoEvents();
                        tmrBSE.Enabled = true; //disable for testing 
                    }
                    break;
                case "toolStripButton_AddDB":

                    TimeSpan duration;
                    DateTime dbDateTime;
                    ArrayList notUpdated = new ArrayList();

                    if (dgViewQ.Rows.Count < 1)
                    {
                        _theParent.lblDMMsg.Text = "No Stock listed..";
                        return;
                    }
                    if (MessageBox.Show("Please confirm to update DoubleM database with latest stock price.", "Save Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                    _theParent.lblDMMsg.Text = "Saving the latest rates to DoubleM database..Please wait";
                    try
                    {
                        DataTable dtLatestRates = new DataTable("LatestRate");
                        dtLatestRates.Columns.Add("StockID", typeof(int));
                        dtLatestRates.Columns.Add("Price", typeof(double));
                        dtLatestRates.Columns.Add("Ondate", typeof(DateTime));

                        foreach (DataGridViewRow dr in dgViewQ.Rows)
                        {
                            if (dr.Cells["Last update"].Value != null)
                                if (dr.Cells["Last update"].Value.ToString() != string.Empty)
                                {
                                    dbDateTime = System.DateTime.ParseExact(dr.Cells["Last update"].Value.ToString(),
                                        CommonDoubleM.lngDate,
                                        System.Globalization.CultureInfo.InvariantCulture);

                                    duration = DateTime.Now - dbDateTime;
                                    if (duration.Days == 0 && duration.Hours > -Convert.ToInt32(CommonDoubleM._sLatestPriceNotOldtThanHour) && duration.Hours < Convert.ToInt32(CommonDoubleM._sLatestPriceNotOldtThanHour))
                                    {
                                        dtLatestRates.Rows.Add(new string[] { dr.Cells["SID"].Value.ToString(),
                                        dr.Cells["Current"].Value.ToString(),
                                        dbDateTime.AddSeconds(DateTime.Now.Second).ToString()});
                                    }
                                    else
                                    {
                                        string s = "Not latest rate - " +
                                            dr.Cells["Stock Name"].Value.ToString() + " " +
                                            dr.Cells["Current"].Value.ToString() + " " +
                                            dbDateTime.ToString();
                                        CommonDoubleM.LogDM(s);

                                        notUpdated.Add(s);
                                    }
                                }
                        }
                        if (dtLatestRates.Rows.Count > 0)
                        {
                            bool blnReturn = _pDalBSE.BulkUpdateStockprice(dtLatestRates);
                            if (blnReturn)
                            {

                                if (notUpdated.Count > 0)
                                {
                                    string msg = "";
                                    for (int i = 0; i < notUpdated.Count; i++)
                                        msg += notUpdated[i].ToString() + ".\n";

                                    msg = "Not all latest Rates have been saved into database successfully.\n" +
                                    "The following stocks are not updated becuse of rates are not in range of "+
                                    CommonDoubleM._sLatestPriceNotOldtThanHour+ " H.\n\n" +
                                    msg;

                                    MessageBox.Show(msg, "Update successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    _theParent.lblDMMsg.Text = "Saving the latest rates to DoubleM database successfully completed";
                                    MessageBox.Show("Latest Rates have been saved into database successfully.", "Update successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Stocks latest Rates have not been updated.\nDownloaded rates are not within " +
                                CommonDoubleM._sLatestPriceNotOldtThanHour +
                                " Hours\n\n Double click on any stock for latest price..",
                                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _theParent.lblDMMsg.Text = "Rates are not latest.";
                        }
                    }
                    catch (Exception ex)
                    {
                        _theParent.lblDMMsg.Text = ex.Message;
                    }
                    break;
                case "toolStripButton_UpdatefromBSE":
                    _theParent.lblDMMsg.Text = "Downlaoding latest rates of selected stocks..";
                    blnSelectedUpdate = true;
                    tmrBSE.Enabled = true;
                    EnableForm(false);
                    break;
                case "toolStripButton_RemoveRate":
                    for (int i = 0; i<dgViewQ.SelectedRows.Count; i++)
                    {
                        iDgvRow = dgViewQ.SelectedRows[i].Index;
                        _theParent.lblDMMsg.Text = "Cleaning " + dgViewQ[0, iDgvRow].Value.ToString();

                        dgViewQ[2, iDgvRow].Value = null; // Latest Price
                        //dgViewQ[4, iDgvRow].Value = ; // Latest Price
                        dgViewQ[5, iDgvRow].Value = null; // Latest Price
                        //dgViewQ[6, iDgvRow].Value = ""; // Latest Price
                        dgViewQ[8, iDgvRow].Value = null; // Latest Price
                        _theParent.lblDMMsg.Text = "Cleaning completed";
                    }


                    break;

                default:
                    break;

            }

        }
        private void EnableForm(Boolean Action)
        {
            toolStrip1.Enabled = Action;
            dgViewQ.Enabled = Action;
        }

        private void tsbtnScriptAction_Click(object sender, EventArgs e)
        {
           ToolStripMenuItem obj = (ToolStripMenuItem)sender;

           tsbtnAllScript.Checked = false;
           tsbtnActiveScript.Checked = false;
           tsbtnInactiveScript.Checked = false;

           obj.Checked = true;
        }

        private void tsbtnScriptAction_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;

            if (obj.Checked)
            {
                _dtWatchList = null;
                
                switch (obj.Name)
                {
                    case "tsbtnAllScript":
                        _dtWatchList = _pDalBSE.Stocks_AvailAvg_BSE(DateTime.Today.AddDays(1.0), (short)CommonDoubleM.StockType.AllStocks);
                        break;
                    case "tsbtnActiveScript":
                        _dtWatchList = _pDalBSE.Stocks_AvailAvg_BSE(DateTime.Today.AddDays(1.0), (short)CommonDoubleM.StockType.ActiveStocks);
                        break;
                    case "tsbtnInactiveScript":
                        _dtWatchList = _pDalBSE.Stocks_AvailAvg_BSE(DateTime.Today.AddDays(1.0), (short)CommonDoubleM.StockType.InactiveStocks);
                        break;
                }
                fillGrid();
            }
        }
    }
}
