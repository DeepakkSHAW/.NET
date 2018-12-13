using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
namespace DoubleM
{
    public partial class frmWatchlist : Form
    {   
//        #region private variables
        private DALDoubleM pDalWatchLest;
        private frmMain theParent;
        Boolean blnBbaseRow;
  //      #region endregion

        public frmWatchlist(object pdalSNew)
        {
            InitializeComponent();
            pDalWatchLest = (DoubleM.DALDoubleM)pdalSNew;
        }

        private void frmWatchlist_Load(object sender, EventArgs e)
        {
            //dgWList.DataSource = pDalTest.LatestValue();
            WatchList_Design();
            string sDM = "", sVM = "", sHTML = "";
            char[] cfilter = { '\r', '\t'};
            string[] sValues;
            int iPBValue = 0;

            DataView dtWatchList;
            theParent = (frmMain)this.ParentForm; //To access MDI parent control
            theParent.lblDMMsg.Text = "All script loading..";
            theParent.pBarDM.Visible = true;

            Cursor = Cursors.WaitCursor;
            try
            {
                dtWatchList = pDalWatchLest.Stocks(ref sDM, ref sVM, 1);

                theParent.pBarDM.Maximum = dtWatchList.Table.Rows.Count;
                //for (int ii = 0; ii < dtWatchList.Table.Rows.Count; ii++)
                foreach (DataRow dtRow in dtWatchList.Table.Rows)
                {
                    Application.DoEvents();
                    dgWList.Rows.Add();
                    dgWList[0, iPBValue].Value = dtRow[2].ToString();
                    theParent.lblDMMsg.Text = "Reading " + dgWList[0, iPBValue].Value;

                    if (dtRow[5].ToString().Trim() != "")
                    {
                        //Reading from Rediff returns true once the reading successfully
                        if (CommonDoubleM.RediffStockLatestUpdates(dtRow[5].ToString(), ref sHTML)) 
                        {
                            sHTML = CommonDoubleM.StripHTML(sHTML);
                            sValues = sHTML.Split(cfilter);
                            for (int i = 0; i < sValues.Length; i++)
                            {
                                switch (sValues[i].Trim())
                                {
                                    case "Last traded":
                                        dgWList[1, iPBValue].Value = sValues[i+1].Trim();
                                        break;

                                    case "Change":
                                        dgWList[2, iPBValue].Value = sValues[i+1].Trim();
                                        break;

                                    case "Time":
                                        dgWList[3, iPBValue].Value = sValues[i+1].Trim();
                                        break;

                                    case "Prev Close":
                                        dgWList[4, iPBValue].Value = sValues[i + 1].Trim();
                                        break;

                                    case "DaysH/L(Rs)":
                                        dgWList[5, iPBValue].Value = sValues[i + 1].Trim();
                                        break;

                                    case "52wkH/L(Rs)":
                                        dgWList[6, iPBValue].Value = sValues[i + 1].Trim();
                                        break;

                                    case "Volume":
                                        dgWList[7, iPBValue].Value = sValues[i + 1].Trim();
                                        break;

                                    case "MktCap(RsCr)":
                                        dgWList[8, iPBValue].Value = sValues[i + 1].Trim();
                                        break;

                                }
                            }
                           // dgWList[2, iPBValue].Value = sHTML;
                        }

                    }
                    else
                        dgWList[1, iPBValue].Value = "--";

                    theParent.pBarDM.Value = iPBValue;
                    iPBValue++;
                   // if (iPBValue == 2) break; //Just for testing only 2 rows
                }
                theParent.pBarDM.Visible = false;
            }
            catch (Exception ex)
            {
                theParent.lblDMMsg.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }
            Cursor = Cursors.Default; 

            //string s=null, abc=null;
            //if (CommonDoubleM.RediffStockLatestUpdates("10510001", ref s))
            //{
            //    s = s.Substring(s.LastIndexOf("Last traded"), s.LastIndexOf("<img") - s.LastIndexOf("Last traded"));
            //    abc = s;
            //    char[] sp = { '\r', '\t' };
            //    s = CommonDoubleM.StripHTML(s);
            //    CommonDoubleM.LogDM(s);
            //    string[] a = s.Split(sp);
                
            //}

        }
        private void WatchList_Design()
        {
            dgWList.Columns.Add("Stock Name", "Stock Name");
            dgWList.Columns.Add("CRate", "Rate");
            dgWList.Columns.Add("Change", "Change");
            dgWList.Columns.Add("DTime", "Date Time");

            dgWList.Columns.Add("PClose", "Prev Close");
            dgWList.Columns.Add("DaysHL", "Days H/L");
            dgWList.Columns.Add("YearHL", "52wk H/L");
            dgWList.Columns.Add("Volume", "Volume");
            dgWList.Columns.Add("MCap", "Mkt Cap");

            /* Data grid format display*/

            dgWList.Columns[0].Width = 180; //Name
            dgWList.Columns[1].Width = 50; //Rate
            dgWList.Columns[2].Width = 60; //Change
            dgWList.Columns[3].Width = 90; // Date Time

            dgWList.Columns[4].Width = 80; //Prev Close
            dgWList.Columns[5].Width = 110; //Days H/L
            dgWList.Columns[6].Width = 110; //52wk H/L
            dgWList.Columns[7].Width = 60; // Volume
            dgWList.Columns[8].Width = 55; // Mkt Cap

        }

        private void dgWList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int i = 0;
            if (!blnBbaseRow)
            {
                dgWList.Rows[e.RowIndex].HeaderCell.Value = "##";
                blnBbaseRow = true;
            }
            else
            {
                dgWList.Rows[e.RowIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                i = e.RowIndex + 1;
                dgWList.Rows[e.RowIndex].HeaderCell.Value = i.ToString();
            }
        }

    }
}