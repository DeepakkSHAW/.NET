using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
     
    
    public partial class frmAc : Form
    {
        private DALDoubleM _pdalStock_Ac;
        private frmMain _theParent; //To access MDI parent control

        public frmAc(object pdalStockAc)
        {
            InitializeComponent();
            _pdalStock_Ac = (DoubleM.DALDoubleM)pdalStockAc;
        }
        internal void FillAc(DataTable dt)
        {
            dgvAc.DataSource = null;
            /*Set PK for seaching before data flushing into datagridview*/
            DataColumn[] dcPk = new DataColumn[1];

            // Set Primary Key
            dcPk[0] = dt.Columns["ACID"];
            dt.PrimaryKey = dcPk;

            dgvAc.DataSource = dt;

            if (dt != null)
            {
                dgvAc.Columns[0].Visible = false;
                dgvAc.Columns[1].Visible = false;
                dgvAc.Columns[2].Visible = false;
                dgvAc.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[3].DefaultCellStyle.Format = "ddd, dd-MMM-yyyy hh:mm";
                dgvAc.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[4].Frozen = true;
                dgvAc.Columns[5].Width = 40;
                dgvAc.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvAc.Columns[13].DefaultCellStyle.Format = "0.00";
                //Column selector
                DataGridViewColumnSelector cs = new DataGridViewColumnSelector(dgvAc);
                cs.MaxHeight = 1000;
                cs.Width = 130;

            }

        }
        private void frmAc_Load(object sender, EventArgs e)
        {
            //lblMsg.Text = "Total Quantity " + _iSellQuantity.ToString();
            dgvAc.RowHeadersWidth = 25;
            _theParent = (frmMain)this.ParentForm;
        }

        private void frmAc_Activated(object sender, EventArgs e)
        {
           // lblMsg.Text = "Total Quantity " + _iSellQuantity.ToString();
        }

        private void frmAc_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }


        private void dgvAc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvAc.DataSource != null && dgvAc[0, e.RowIndex].Value != null)
                {
                    if (dgvAc[13, e.RowIndex].Value != DBNull.Value && dgvAc[13, e.RowIndex].Value.ToString() != "")
                        dgvAc[13, e.RowIndex].Style.BackColor = Color.FromArgb(
                            CommonDoubleM.GetColorcode(Convert.ToDouble(dgvAc[13, e.RowIndex].Value)));
                }
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
            }
        }

        private void dgvAc_SelectionChanged(object sender, EventArgs e)
        {
            int iLastRow = 0;
            double dblCPTotal = 0, dblSPTotal = 0, dblTotal = 0;

            iLastRow = dgvAc.RowCount - 1;
            try
            {
                if (iLastRow > 2) //More than 2 Rows, Footer + Single Row from DB
                    if (dgvAc.SelectedRows.Count > 1)
                    {

                        foreach (DataGridViewRow dtRowSelected in dgvAc.SelectedRows)
                        {
                            if (iLastRow != dtRowSelected.Index) //Do not include the last row
                            {
                                dblCPTotal += Convert.ToDouble(dgvAc[8, dtRowSelected.Index].Value);
                                dblTotal = dblTotal + Convert.ToDouble(dgvAc[1, dtRowSelected.Index].Value) * Convert.ToDouble(dgvAc[2, dtRowSelected.Index].Value);
                                dblSPTotal = dblSPTotal + Convert.ToDouble(dgvAc[11, dtRowSelected.Index].Value);
                            }
                        }
                        //dgvAc[3, iLastRow].Style.BackColor = Color.Red;
                        dgvAc[3, iLastRow].Value = "Summary :";
                        dgvAc[8, iLastRow].Value = dblCPTotal;
                        dgvAc[11, iLastRow].Value = dblSPTotal;
                        dgvAc[12, iLastRow].Value = decimal.Round((decimal)(dblSPTotal - dblCPTotal), 2);
                        dgvAc[13, iLastRow].Value = ((dblSPTotal / dblCPTotal) - 1) * 100;
                    }
                    else
                    {
                        dgvAc[3, iLastRow].Value = "Summary";
                        dgvAc[8, iLastRow].Value = "";
                        dgvAc[11, iLastRow].Value = "";
                        dgvAc[12, iLastRow].Value = "";
                        dgvAc[13, iLastRow].Value = "";
                    }
            }
            catch (Exception ex)
            {
            _theParent.lblDMMsg.Text = ex.Message;
            }

        }

        private void dgvAc_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           //MessageBox.Show(e.RowIndex.ToString() + " " +e.ColumnIndex.ToString());
           if (e.ColumnIndex == 13)
           {
               dgvAc[3, e.RowIndex].Value = "Esc to unselect";
               dgvAc[13, e.RowIndex].Value = "0";
           }
            e.Cancel=false;
        }

    }
}