using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmMngStockPrice : Form
    {
        private DALDoubleM _pdalMngSPrice;
        private frmMain _theParent; //To access MDI parent control
        private DataSet _dsRates;
        private DataRow _drRate;
        private long _RateID = -1;
        private int _AddedRowIndex = -1;

        public frmMngStockPrice(object pdalMngStockPrice)
        {
            InitializeComponent();
            _pdalMngSPrice = (DoubleM.DALDoubleM)pdalMngStockPrice;
        }
#region Control Events
        private void frmMngStockPrice_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            _theParent = (frmMain)this.ParentForm;
            clean();
            LoadStocks();
        }
        private void frmMngStockPrice_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void txtRate_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtRate.Text))
            {
                txtRate.SelectionStart = 0;
                txtRate.SelectionLength = txtRate.Text.Length;
            }
        }
        private void txtRate_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            double result;

            if (!double.TryParse(txtRate.Text, out result))
            {
                error = "Please enter a valid price";
                e.Cancel = true;
            }
            if (result < 0)
            {
                error = "Price can't be negative";
                e.Cancel = true;
            }
            errorProvider1.SetError((Control)sender, error);
        }
        private void dtpOndate_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (DateTime.Now.CompareTo(dtpOndate.Value) == -1)
            {
                error = "Can't be a future date";
                e.Cancel = true;
            }
            errorProvider1.SetError((Control)sender, error);
        }
        private void tsSPrice_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                ///////////// Insert Rate//////////////////
                case "tsbtnNew": //Printing Gridview
                    double result;
                    _RateID = -1; _drRate = null;
                    if (double.TryParse(txtRate.Text, out result))
                    {
                        if (_dsRates != null)
                        {
                            AddNewRate(result);
                            clean();
                        }
                        else
                            MessageBox.Show("Please select a stock from dropdown list");
                    }
                    else
                        MessageBox.Show("Entered rate is not in valid formate");
                    break;
                ///////////// Update Rate//////////////////
                case "tsbtnUpdate": //Printing Gridview
                    if (double.TryParse(txtRate.Text, out result))
                    {
                        if (_dsRates != null)
                        {
                            UpdateRate(result);
                            _RateID = -1; _drRate = null;
                            clean();
                        }
                        else
                            MessageBox.Show("Please select a stock from dropdown list");
                    }
                    else
                        MessageBox.Show("Entered rate is not in valid formate");
                    break;
                    ///////////// Delete Rate//////////////////
                case "tsbtnDeletePrice": //Printing Gridview
                    if (dgvSPrice.SelectedRows.Count < 1)
                        MessageBox.Show("Only select Rows can be deleted\nPlease select Rates", "Delete Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure, want to delete selected rates", "Deleted confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        foreach (DataGridViewRow dr in dgvSPrice.SelectedRows)
                        {
                            if(_dsRates.Tables[0].Rows.Count <= 0) return;
                            _drRate = _dsRates.Tables[0].Rows[dr.Index];
                            DeleteRate(Convert.ToInt64(dr.Cells[0].Value));
                            _RateID = -1; _drRate = null;
                        }
                        _dsRates.AcceptChanges();
                        dgvSPrice.ClearSelection();
                    }
                    break;
                ///////////// Refresh Rates//////////////////
                case "tsbtnRefesh":
                    clean();
                    LoadStocks();
                    fillDatagrid();
                    break;
                ///////////// Clean form and veriables for Rates//////////////////
                case "tsbtnClean": 
                    clean();
                    _RateID = -1; _drRate = null;
                    break;
                ///////////// Plot the rates on Chat//////////////////
                case "tsbtnGraph":
                    PlotGraph();
                    break;
                case "tsbtnSelectStock":
                    fillDatagrid();
                    break;
                default:
                    break;
                    
            }
        }
        private void dgvSPrice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dgvSPrice.RowCount - 1)
            {
                CommonDoubleM.LogDM("Editing Rates with ID " + dgvSPrice[0, e.RowIndex].Value.ToString());
                
                _RateID = Convert.ToInt64(dgvSPrice[0, e.RowIndex].Value);
                txtRate.Text = dgvSPrice[2, e.RowIndex].Value.ToString();
                dtpOndate.Value = (DateTime)dgvSPrice[3, e.RowIndex].Value;
                _theParent.lblDMMsg.Text = "After modification - Press Edit to button to update or New Button to Insert new rate";
                _drRate = _dsRates.Tables[0].Rows[e.RowIndex];
                txtRate.Focus();
            }
            else
                _theParent.lblDMMsg.Text = "Please double click on valid rows..";
        }

        private void dgvSPrice_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            _AddedRowIndex = e.RowIndex;
        }
#endregion

#region Private Methods
        private void clean()
        {
            txtRate.Text = "0.00";
            txtRate.Focus();
            dtpOndate.Value = DateTime.Now;
        }
        private void LoadStocks()
        {

            string sDMs = "", sVMs = "";
            DataView dvStocks = null;

            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            //Loading All available stock (both active and inactive)
            dvStocks = _pdalMngSPrice.Stocks(ref sDMs, ref sVMs, 8);
            //An alternative ways to load datasource to toolstripcombobox 
            ComboBox cb = (ComboBox)tscboStocks.Control; 
            cb.DataSource = dvStocks;
            cb.DisplayMember =sDMs;
            cb.ValueMember = sVMs;

            Cursor = Cursors.Default;
            dgvSPrice.Focus();

        }
        private void fillDatagrid()
        {
            //int iStockID = (int)((ComboBox)tscboStocks.Control).SelectedValue;
            int result;
            //if ((((ComboBox)tscboStocks.Control).SelectedValue).GetType().ToString() == "System.Int32") // an alternative way
            if (tscboStocks.SelectedIndex > -1)
            if (int.TryParse(((ComboBox)tscboStocks.Control).SelectedValue.ToString(), out result))
            {

                int iStockID = Convert.ToInt32(((ComboBox)tscboStocks.Control).SelectedValue);
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                _dsRates = _pdalMngSPrice.GetRateList(iStockID);

                dgvSPrice.DataSource = _dsRates.Tables[0];
                if (dgvSPrice.ColumnCount > 2)
                {
                    //Ids Columns are invisible
                    dgvSPrice.Columns[0].Visible = false;
                    dgvSPrice.Columns[1].Visible = false;
                    dgvSPrice.Columns[2].Width = 60;
                    dgvSPrice.Columns[3].Width = 150;
                    dgvSPrice.Columns[3].DefaultCellStyle.Format = "ddd, dd-MMM-yyyy hh:mm";
                    
                    _theParent.lblDMMsg.Text = "Total " + _dsRates.Tables[0].Rows.Count.ToString() + " rows in the list";
                }
                Cursor = Cursors.Default;
                dgvSPrice.Focus();
            }
        }
        private void AddNewRate(double sPrice)
        {
            int i = 0;
            int iStockID = Convert.ToInt32(((ComboBox)tscboStocks.Control).SelectedValue);
            i = _pdalMngSPrice.NewRate(iStockID, sPrice, dtpOndate.Value);
            if(i>0)
            {
            _drRate = _dsRates.Tables[0].NewRow();
            _drRate[0] = i;
            _drRate[1] = iStockID;
            _drRate[2] = sPrice;
            _drRate[3] = dtpOndate.Value;
            _dsRates.Tables[0].Rows.Add(_drRate);
            //dgvSPrice.Refresh();

            //Auto Scroll to Row new row and select it
                dgvSPrice.Rows[_AddedRowIndex].Selected = true;
            if (!dgvSPrice.Rows[_AddedRowIndex].Displayed) 
                dgvSPrice.FirstDisplayedScrollingRowIndex = _AddedRowIndex;

            }
        }
        private void UpdateRate(double sPrice)
        {
            int i = -1;
            //int iStockID = Convert.ToInt32(((ComboBox)tscboStocks.Control).SelectedValue);
            if (_drRate == null)
            {
                _theParent.lblDMMsg.Text = "Please double Click on item to be updated";
                return;
            }
                _drRate.BeginEdit();
                _drRate[2] = sPrice;
                _drRate[3] = dtpOndate.Value;
                i = _pdalMngSPrice.UpdateRate(_RateID, sPrice, dtpOndate.Value);

                if (i == 1)
                    _drRate.EndEdit();
                else
                {
                    _drRate.CancelEdit();
                    MessageBox.Show("Latest price has not been updated","Update failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
        }
        private void DeleteRate(long RateID)
        {
            int i=-1;
            
            i = _pdalMngSPrice.DeleteRate(RateID);
            if (i == 1)
            {
                _drRate.Delete();
            }
            else
                MessageBox.Show("Rate was not deleted", "Deletion failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
        private void PlotGraph()
        {
            if (_dsRates != null)
            {
                frmCatGraph pCatGr = new frmCatGraph(_pdalMngSPrice, _dsRates, "HighLowClose");
                pCatGr.MdiParent = _theParent;
                pCatGr.Show();
            }
            else
                MessageBox.Show("No data has been found to plot the graph");
        }
#endregion





















    }
}
