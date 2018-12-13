using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmlAvgView : Form
    {
        private DALDoubleM _pdalStock_CA;
        private int _Caller = 0;
        private string _sTitle="";
        private double dblTotal = 0, dblGain = 0, dblValue = 0;
        Boolean blnBbaseRow;

        public frmlAvgView()
        {
            InitializeComponent();
        }

        public frmlAvgView(object pdalSQV, string sTitle)

        {
            InitializeComponent();
            _pdalStock_CA = (DoubleM.DALDoubleM)pdalSQV;
            _sTitle = sTitle;
            if (_sTitle.Contains("Cumulative"))
                _Caller = 2;
            else
                _Caller = 1;

            this.Text = _sTitle;
        }

        private void frmCumulAvg_Load(object sender, EventArgs e)
        {
            
            frmMain theParent = (frmMain)this.ParentForm; //To access MDI parent control
            theParent.lblDMMsg.Text = "Please wait data loading..";

            try
            {
                dblTotal = 0;
                dblGain = 0;
                dblValue = 0;
                decimal dicAvgPrice = 0, dicPerct = 0;

                DataTable dtQuickView = new DataTable();
                theParent.pBarDM.Visible = true;
                Application.DoEvents();

                if (_Caller == 1) //Avg
                {
                    dtQuickView = _pdalStock_CA.StockAvgAfterSale();

                    /*Adding Columns Header to Datagrid*/
                    /*foreach (DataColumn dtcol in dtQuickView.Columns)
                        dgViewQ.Columns.Add(dtcol.ColumnName, dtcol.ColumnName);*/

                    dgViewQ.Columns.Add("Stock Name", "Stock Name");
                    dgViewQ.Columns.Add("Shrs", "Shrs");
                    dgViewQ.Columns.Add("Current", "Current");
                    dgViewQ.Columns.Add("SID", "SID");
                    dgViewQ.Columns.Add("AvgPrice", "Avg");
                    dgViewQ.Columns.Add("Percentage", "%");
                    dgViewQ.Columns.Add("Gain", "T Gain");
                    dgViewQ.Columns.Add("PricePaid", "Paid");
                }

                else
                {
                    dtQuickView = _pdalStock_CA.LatestValue();
                    /*Adding Columns Header to Datagrid*/
                    foreach (DataColumn dtcol in dtQuickView.Columns)
                        dgViewQ.Columns.Add(dtcol.ColumnName, dtcol.ColumnName);

                    dgViewQ.Columns.Add("AvgPrice", "Avg");
                    dgViewQ.Columns.Add("Percentage", "%");
                    dgViewQ.Columns.Add("Gain", "T Gain");
                    dgViewQ.Columns.Add("PricePaid", "Paid");
                }

                /* Data grid format display*/

                dgViewQ.Columns[0].Width = 180; //Name
                dgViewQ.Columns[1].Width = 40; //Total Stock
                dgViewQ.Columns[2].Width = 60; //Current Price
                dgViewQ.Columns[3].Visible = false; // StockID

                dgViewQ.Columns[4].Width = 60; //Avg. Price
                dgViewQ.Columns[5].Width = 45; //%
                dgViewQ.Columns[6].Width = 50; //Gain
                dgViewQ.Columns[7].Width = 60; // TCost
                //dgViewQ.Columns[7].Visible = false; //%
                dgViewQ.RowHeadersWidth = 60;
                    int iCount = 0;

                    theParent.pBarDM.Maximum = dtQuickView.Rows.Count;

                    foreach (DataRow dtrow in dtQuickView.Rows)
                    {
                        dgViewQ.Rows.Add();

                        //Noticed No Effect, Hance Commented
                        /*dgViewQ.Columns[1].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgViewQ.Columns[2].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgViewQ.Columns[3].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleRight;*/
                        if (_Caller == 2)
                        {
                            dgViewQ[0, iCount].Value = dtrow[0];
                            dgViewQ[1, iCount].Value = dtrow[1];
                            dgViewQ[2, iCount].Value = dtrow[2];
                            
                            dicAvgPrice = _pdalStock_CA.StockAvg((int)dtrow[3]);
                            dgViewQ[4, iCount].Value = dicAvgPrice;

                            dgViewQ[6, iCount].Value = ((decimal)(dgViewQ[2, iCount].Value) - dicAvgPrice) * (decimal)(double)(dgViewQ[1, iCount].Value);
                            dgViewQ[7, iCount].Value = dicAvgPrice * (decimal)(double)dgViewQ[1, iCount].Value;

                            //dgViewQ[0, 0].Style.BackColor = Color.RoyalBlue;
                        }
                        else if (_Caller == 1)
                        {
                            dgViewQ[0, iCount].Value = dtrow[1];
                            dgViewQ[1, iCount].Value = dtrow[2];
                            dgViewQ[2, iCount].Value = dtrow[4];
                            dgViewQ[3, iCount].Value = dtrow[0];
                            dgViewQ[4, iCount].Value = dtrow[3];
                            dgViewQ[5, iCount].Value = dtrow[5];
                            dgViewQ[6, iCount].Value = dtrow[6];
                            dgViewQ[7, iCount].Value = dtrow[7];
                        }
                        //dgViewQ[3, iCount].Value = dtrow[3];

                        //dgViewQ[4, iCount].Value = dtrow[4];
                        //double tmpValue = Convert.ToDouble(dgViewQ[4, iCount].Value);
                        dicPerct = (decimal)((Convert.ToDouble(dgViewQ[2, iCount].Value) / Convert.ToDouble(dgViewQ[4, iCount].Value)) - 1) * 100;
                        dicPerct = decimal.Round(dicPerct, 2);
                        dgViewQ[5, iCount].Value = dicPerct;

                       /* int iR = 255, iG = 255, iB = 255;
                        int iMax = 0, iMin = 0;

                        //http://www.flounder.com/csharp_color_table.htm (for color coding)
                        if (dicPerct < 0)
                        {
                            iMin = 255 - (int)(dicPerct * -4);
                            if ((iMin < 0) || (iMin > 255)) iMin = 0;

                            iG = iMin;
                            iB = iMin;
                        }
                        else
                        {
                            iMax = 255 - (int)(dicPerct * 4);
                            if ((iMax < 0) || (iMax > 255)) iMax = 0;
                            iR = iMax;
                            iB = iMax;
                        }
                        dgViewQ[5, iCount].Style.BackColor = Color.FromArgb(iR, iG, iB);*/
                        dgViewQ[5, iCount].Style.BackColor = Color.FromArgb(CommonDoubleM.GetColorcode((double)dicPerct));
                        dblValue = dblValue +
                                Convert.ToDouble(dgViewQ[1, iCount].Value) *
                                Convert.ToDouble(dgViewQ[2, iCount].Value);

                        dblGain = dblGain + Convert.ToDouble(dgViewQ[6, iCount].Value);
                        dblTotal = dblTotal + Convert.ToDouble(dgViewQ[7, iCount].Value);


                        iCount++;
                        theParent.pBarDM.Value = iCount;
                    }
                    //dgViewQ.Rows[iCount].Selected = true;
                    /*Summary at bottom*/
                    dgViewQ[0, iCount].Value = "Summary";
                    dgViewQ[2, iCount].Value = dblValue;
                    dgViewQ[5, iCount].Value = decimal.Round(((decimal)((dblValue / dblTotal) - 1) * 100), 2);
                    dgViewQ[6, iCount].Value = dblGain;
                    dgViewQ[7, iCount].Value = dblTotal;

                    DataGridViewColumnSelector cs = new DataGridViewColumnSelector(dgViewQ);
                    cs.MaxHeight = 1000;
                    cs.Width = 130;

                    theParent.lblDMMsg.Text = "Done..";
                }
            catch (Exception ex)
            {
                theParent.lblDMMsg.Text = ex.Message;
            }
            finally
            {
                theParent.pBarDM.Visible = false;
            }
        }


        //private void frmCumulAvg_Load(object sender, EventArgs e)
        //{
        //    this.Text = _sTitle;
        //    frmMain theParent = (frmMain)this.ParentForm; //To access MDI parent control
        //    theParent.lblDMMsg.Text = "Please wait data loading..";

        //    try
        //    {
        //        dblTotal = 0;
        //        dblGain = 0;
        //        dblValue = 0;
        //        DataTable dtQuickView = new DataTable();

        //        if (_sTitle.Contains("Cumulative"))
        //        {
        //    Application.DoEvents();


        //    decimal dicAvgPrice = 0, dicPerct = 0;

        //        theParent.pBarDM.Visible = true;
        //        dtQuickView = _pdalStock_CA.LatestValue();
        //        //dtQuickView.con
        //        /*Adding Columns Header to Datagrid*/
        //        foreach (DataColumn dtcol in dtQuickView.Columns)
        //            dgViewQ.Columns.Add(dtcol.ColumnName, dtcol.ColumnName);

        //        dgViewQ.Columns.Add("AvgPrice", "Avg");
        //        dgViewQ.Columns.Add("Percentage", "%");
        //        dgViewQ.Columns.Add("Gain", "T Gain");
        //        dgViewQ.Columns.Add("PricePaid", "Paid");

        //        /* Data grid format display*/

        //        dgViewQ.Columns[0].Width = 180; //Name
        //        dgViewQ.Columns[1].Width = 40; //Total Stock
        //        dgViewQ.Columns[2].Width = 60; //Current Price
        //        dgViewQ.Columns[3].Visible = false; // StockID

        //        dgViewQ.Columns[4].Width = 60; //Avg. Price
        //        dgViewQ.Columns[5].Width = 45; //%
        //        dgViewQ.Columns[6].Width = 50; //Gain
        //        dgViewQ.Columns[7].Width = 60; // TCost

        //        int iCount = 0;

        //        theParent.pBarDM.Maximum = dtQuickView.Rows.Count;

        //        foreach (DataRow dtrow in dtQuickView.Rows)
        //        {
        //            dgViewQ.Rows.Add();
        //            dgViewQ.Columns[1].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //            dgViewQ.Columns[2].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //            dgViewQ.Columns[3].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //            //            _pdalStock_QuickView.StockAvg(2);

        //            dgViewQ[0, iCount].Value = dtrow[0];
        //            dgViewQ[1, iCount].Value = dtrow[1];
        //            dgViewQ[2, iCount].Value = dtrow[2];
        //            dblValue = dblValue +
        //                Convert.ToDouble(dgViewQ[1, iCount].Value) *
        //                Convert.ToDouble(dgViewQ[2, iCount].Value);

        //            dicAvgPrice = _pdalStock_CA.StockAvg((int)dtrow[3]);
        //            dgViewQ[4, iCount].Value = dicAvgPrice;
        //            //dgViewQ[3, iCount].Value = dtrow[3];

        //            //dgViewQ[4, iCount].Value = dtrow[4];
        //            //double tmpValue = Convert.ToDouble(dgViewQ[4, iCount].Value);
        //            dicPerct = (decimal)((Convert.ToDouble(dgViewQ[2, iCount].Value) / Convert.ToDouble(dgViewQ[4, iCount].Value)) - 1) * 100;
        //            dicPerct = decimal.Round(dicPerct, 2);
        //            dgViewQ[5, iCount].Value = dicPerct;

        //            int iR = 255, iG = 255, iB = 255;
        //            int iMax = 0, iMin = 0;

        //            //http://www.flounder.com/csharp_color_table.htm (for color coding)
        //            if (dicPerct < 0)
        //            {
        //                iMin = 255 - (int)(dicPerct * -4);
        //                if ((iMin < 0) || (iMin > 255)) iMin = 0;

        //                iG = iMin;
        //                iB = iMin;
        //            }
        //            else
        //            {
        //                iMax = 255 - (int)(dicPerct * 4);
        //                if ((iMax < 0) || (iMax > 255)) iMax = 0;
        //                iR = iMax;
        //                iB = iMax;
        //            }
        //            dgViewQ[5, iCount].Style.BackColor = Color.FromArgb(iR, iG, iB);

        //            dgViewQ[6, iCount].Value = ((decimal)(dgViewQ[2, iCount].Value) - dicAvgPrice) * (decimal)(double)(dgViewQ[1, iCount].Value);
        //            dblGain = dblGain + Convert.ToDouble(dgViewQ[6, iCount].Value);
        //            dgViewQ[7, iCount].Value = dicAvgPrice * (decimal)(double)dgViewQ[1, iCount].Value;
        //            dblTotal = dblTotal + Convert.ToDouble(dgViewQ[7, iCount].Value);
        //            //dgViewQ[0, 0].Style.BackColor = Color.RoyalBlue;

        //            iCount++;
        //            theParent.pBarDM.Value = iCount;
        //        }
        //        //dgViewQ.Rows[iCount].Selected = true;
        //        /*Summary at bottom*/
        //        dgViewQ[0, iCount].Value = "Summary";
        //        dgViewQ[2, iCount].Value = dblValue;
        //        dgViewQ[5, iCount].Value = decimal.Round(((decimal)((dblValue / dblTotal) - 1) * 100), 2);
        //        dgViewQ[6, iCount].Value = dblGain;
        //        dgViewQ[7, iCount].Value = dblTotal;

        //        theParent.lblDMMsg.Text = "Done..";
        //    }
        //        else
        //        {
        //            MessageBox.Show("Avg");
        //            theParent.pBarDM.Visible = true;
        //            dtQuickView = _pdalStock_CA.StockAvgAfterSale();

        //            /*Adding Columns Header to Datagrid*/
        //            /*foreach (DataColumn dtcol in dtQuickView.Columns)
        //                dgViewQ.Columns.Add(dtcol.ColumnName, dtcol.ColumnName);*/
        //            dgViewQ.Columns.Add("Stock Name", "Stock Name");
        //            dgViewQ.Columns.Add("Shrs", "Shrs");
        //            dgViewQ.Columns.Add("Current", "Current");
        //            dgViewQ.Columns.Add("AvgPrice", "Avg");
        //            dgViewQ.Columns.Add("Percentage", "%");
        //            dgViewQ.Columns.Add("Gain", "T Gain");
        //            dgViewQ.Columns.Add("PricePaid", "Paid");

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        theParent.lblDMMsg.Text = ex.Message;
        //    }
        //    finally
        //    {
        //        theParent.pBarDM.Visible = false;
        //    }
        //}

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
                    dgViewQ[5, iLastRow].Value = decimal.Round(((decimal)(dblSelGain / dblSelTotal) * 100), 2);
                    dgViewQ[6, iLastRow].Value = dblSelGain;
                    dgViewQ[7, iLastRow].Value = dblSelTotal;
                }
                else
                {
                    dgViewQ[0, iLastRow].Value = "Summary";
                    dgViewQ[2, iLastRow].Value = dblValue;
                    dgViewQ[5, iLastRow].Value = decimal.Round(((decimal)((dblValue / dblTotal) - 1) * 100), 2);
                    dgViewQ[6, iLastRow].Value = dblGain;
                    dgViewQ[7, iLastRow].Value = dblTotal;

                }
        }

        private void dgViewQ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //MessageBox.Show(dgViewQ[0, e.RowIndex].Value.ToString());
                //tltipQuickView
                ////tltipQuickView.SetToolTip(dgViewQ, "tipText");
                ////tltipQuickView.Active = true;
                //http://www.codeproject.com/csharp/testgridzip.asp
            }
        }
    }
}