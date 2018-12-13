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
     
    
    public partial class frmOnlinePortfolio : Form
    {
        private DALDoubleM _pdalStock_OnlinePF;
        private string _sTitle = "";
        private double dblTotal = 0, dblGain = 0, dblValue = 0;
        Boolean blnBbaseRow;

        public frmOnlinePortfolio(object pdalSNew)
        {
            InitializeComponent();
            _pdalStock_OnlinePF = (DoubleM.DALDoubleM)pdalSNew;
        }

        private void frmOnlinePortfolio_Load(object sender, EventArgs e)
        {
            frmMain theParent = (frmMain)this.ParentForm; //To access MDI parent control
            theParent.lblDMMsg.Text = "Please wait data loading..";
            
            try
            {
                dblTotal = 0;
                dblGain = 0;
                dblValue = 0;
                decimal dicAvgPrice = 0, dicPerct = 0;

                DataTable dtOnlinePF = new DataTable();
                theParent.pBarDM.Visible = true;
                Application.DoEvents();

                    dtOnlinePF = _pdalStock_OnlinePF.StockAvgAfterSale();

                    /*Adding Columns Header to Datagrid*/
                    /*foreach (DataColumn dtcol in dtOnlinePF.Columns)
                        dgOnlinePF.Columns.Add(dtcol.ColumnName, dtcol.ColumnName);*/
                    
                    dgOnlinePF.Columns.Add("Stock Name", "Stock Name");
                    dgOnlinePF.Columns.Add("Shrs", "Shrs");
                    dgOnlinePF.Columns.Add("Current", "Current");
                    dgOnlinePF.Columns.Add("SID", "SID");
                    dgOnlinePF.Columns.Add("AvgPrice", "Avg");
                    dgOnlinePF.Columns.Add("Percentage", "%");
                    dgOnlinePF.Columns.Add("Gain", "T Gain");
                    dgOnlinePF.Columns.Add("PricePaid", "Paid");

                /* Data grid format display*/

                dgOnlinePF.Columns[0].Width = 180; //Name
                dgOnlinePF.Columns[1].Width = 40; //Total Stock
                dgOnlinePF.Columns[2].Width = 60; //Current Price
                dgOnlinePF.Columns[3].Visible = false; // StockID

                dgOnlinePF.Columns[4].Width = 60; //Avg. Price
                dgOnlinePF.Columns[5].Width = 45; //%
                dgOnlinePF.Columns[6].Width = 50; //Gain
                dgOnlinePF.Columns[7].Width = 60; // TCost

                int iCount = 0;

                theParent.pBarDM.Maximum = dtOnlinePF.Rows.Count;

                foreach (DataRow dtrow in dtOnlinePF.Rows)
                {
                    dgOnlinePF.Rows.Add();
                    dgOnlinePF.Columns[1].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgOnlinePF.Columns[2].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgOnlinePF.Columns[3].InheritedStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgOnlinePF[0, iCount].Value = dtrow[1];
                    dgOnlinePF[1, iCount].Value = dtrow[2];
                    dgOnlinePF[2, iCount].Value = dtrow[4];
                    dgOnlinePF[3, iCount].Value = dtrow[0];
                    dgOnlinePF[4, iCount].Value = dtrow[3];
                    dgOnlinePF[5, iCount].Value = dtrow[5];
                    dgOnlinePF[6, iCount].Value = dtrow[6];
                    dgOnlinePF[7, iCount].Value = dtrow[7];

                    dicPerct = (decimal)((Convert.ToDouble(dgOnlinePF[2, iCount].Value) / Convert.ToDouble(dgOnlinePF[4, iCount].Value)) - 1) * 100;
                    dicPerct = decimal.Round(dicPerct, 2);
                    dgOnlinePF[5, iCount].Value = dicPerct;


                    int iR = 255, iG = 255, iB = 255;
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
                    dgOnlinePF[5, iCount].Style.BackColor = Color.FromArgb(iR, iG, iB);

                    dblValue = dblValue +
                            Convert.ToDouble(dgOnlinePF[1, iCount].Value) *
                            Convert.ToDouble(dgOnlinePF[2, iCount].Value);

                    dblGain = dblGain + Convert.ToDouble(dgOnlinePF[6, iCount].Value);
                    dblTotal = dblTotal + Convert.ToDouble(dgOnlinePF[7, iCount].Value);


                    iCount++;
                    theParent.pBarDM.Value = iCount;
                }
                //dgOnlinePF.Rows[iCount].Selected = true;
                /*Summary at bottom*/
                dgOnlinePF[0, iCount].Value = "Summary";
                dgOnlinePF[2, iCount].Value = dblValue;
                dgOnlinePF[5, iCount].Value = decimal.Round(((decimal)((dblValue / dblTotal) - 1) * 100), 2);
                dgOnlinePF[6, iCount].Value = dblGain;
                dgOnlinePF[7, iCount].Value = dblTotal;

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

        private void dgOnlinePF_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int i = 0;

            if (!blnBbaseRow)
            {
                dgOnlinePF.Rows[e.RowIndex].HeaderCell.Value = "##";
                blnBbaseRow = true;
            }
            else
            {
                dgOnlinePF.Rows[e.RowIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                i = e.RowIndex + 1;
                dgOnlinePF.Rows[e.RowIndex].HeaderCell.Value = i.ToString();
            }
        }

        private void dgOnlinePF_SelectionChanged(object sender, EventArgs e)
        {
            int iLastRow = 0;
            double dblSelTotal = 0, dblSelGain = 0, dblSelValue = 0;

            iLastRow = dgOnlinePF.RowCount - 1;

            if (iLastRow > 2)
                if (dgOnlinePF.SelectedRows.Count > 1)
                {

                    foreach (DataGridViewRow dtRowSelected in dgOnlinePF.SelectedRows)
                    {
                        dblSelTotal = dblSelTotal + Convert.ToDouble(dgOnlinePF[7, dtRowSelected.Index].Value);
                        dblSelValue = dblSelValue + Convert.ToDouble(dgOnlinePF[1, dtRowSelected.Index].Value) * Convert.ToDouble(dgOnlinePF[2, dtRowSelected.Index].Value);
                        dblSelGain = dblSelGain + Convert.ToDouble(dgOnlinePF[6, dtRowSelected.Index].Value);
                    }

                    dgOnlinePF[0, iLastRow].Value = "Summary of selected rows";
                    dgOnlinePF[2, iLastRow].Value = dblSelValue;
                    dgOnlinePF[5, iLastRow].Value = decimal.Round(((decimal)(dblSelGain / dblSelTotal) * 100), 2);
                    dgOnlinePF[6, iLastRow].Value = dblSelGain;
                    dgOnlinePF[7, iLastRow].Value = dblSelTotal;
                }
                else
                {
                    dgOnlinePF[0, iLastRow].Value = "Summary";
                    dgOnlinePF[2, iLastRow].Value = dblValue;
                    dgOnlinePF[5, iLastRow].Value = decimal.Round(((decimal)((dblValue / dblTotal) - 1) * 100), 2);
                    dgOnlinePF[6, iLastRow].Value = dblGain;
                    dgOnlinePF[7, iLastRow].Value = dblTotal;

                }

        }

    }
}