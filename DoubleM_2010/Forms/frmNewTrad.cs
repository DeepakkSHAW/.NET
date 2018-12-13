using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmNewTrad : Form
    {
        private DALDoubleM _pdalStock_Trad;
        private bool _sellValid = false;
        private const string _sTitle = "New Trading";

        private const int _iInterval = 5;
        private const int _iHeight = 125;
        private int _curHeight = 125;
        private Boolean _blnCollapse = true;
        
        public frmNewTrad(object pdalSNew)
        {
            InitializeComponent();
            _pdalStock_Trad = (DoubleM.DALDoubleM)pdalSNew;
        }


        private void frmNewTrad_Load(object sender, EventArgs e)
        {
            //this.Icon = new Icon(this.GetType().Assembly.GetManifestResourceStream("DoubleM.Pics.Colors.ico"));
            this.Icon = Properties.Resources.transfer;
            string sDM = "", sVM = "";
            Cursor = Cursors.WaitCursor;
            dtpOndate.Value = DateTime.Now.Date;
            ddlStock.DataSource = _pdalStock_Trad.Stocks(ref sDM, ref sVM, 0);
            
            ddlStock.DisplayMember = sDM;
            ddlStock.ValueMember = sVM;
            if (ddlStock.Items.Count > 0) ddlStock.SelectedIndex = 0;
            Databinding();
            //pfrmAc = new frmAc(_pdalStock_Trad, 1, 1, null);
            //Default buy

            /*Datagridview Columns*/

            dgvTrading.Columns.Add("SID", "SID");
            dgvTrading.Columns.Add("TDate", "Date");
            dgvTrading.Columns.Add("Quantity", "Quant");
            dgvTrading.Columns.Add("Price", "Price");
            dgvTrading.Columns.Add("RQuantity", "Rem. Quant");
            dgvTrading.Columns.Add("SQuantity", "Sell Quant");
            dgvTrading.Columns.Add("TID", "TradeID");

            dgvTrading.Columns[0].Visible = false;  //StockID
            dgvTrading.Columns[1].Width = 80;       //Date
            dgvTrading.Columns[2].Width = 40;       //Quantity
            dgvTrading.Columns[3].Width = 50;       //Price
            dgvTrading.Columns[4].Width = 40;       // Remaining Quantity
            dgvTrading.Columns[5].Width = 40;       // Selling Quantity
            dgvTrading.Columns[6].Visible = false;  // TradeID
            
            dgvTrading.Columns[1].ReadOnly = true;
            dgvTrading.Columns[2].ReadOnly = true;
            dgvTrading.Columns[3].ReadOnly = true;
            dgvTrading.Columns[4].ReadOnly = true;

            //dgvTrading.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            
            Cursor = Cursors.Default;
            RBtnBuy.Checked = true;
        }

        private void Databinding()
        {
            /* Creating Virtual Databale */
            DataTable _dt;
            _dt = new DataTable("Numbers");

            _dt.Columns.Add("ID", typeof(uint));
            _dt.Columns.Add("Name", typeof(string));
            _dt.Columns.Add("Cost", typeof(decimal));

            /*Values for 1st Rows*/
            _dt.Rows.Add(1, "Two", 1.00M);

            /* Set up the ErrorProvider */
            this.errorProvider1.DataSource = _dt;
            /* Data type Binding with text controls */
            txtRate.DataBindings.Add("Text", _dt, "Cost", true, DataSourceUpdateMode.OnPropertyChanged);
            txtBrok.DataBindings.Add("Text", _dt, "Cost", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTax.DataBindings.Add("Text", _dt, "Cost", true, DataSourceUpdateMode.OnPropertyChanged);
            txtQnt.DataBindings.Add("Text", _dt, "ID", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        private void Cleanup()
        {
            RBtnBuy.Checked = true;
            txtQnt.Text = "0";
            txtRate.Text = "0.0";
            txtBrok.Text = "0.0";
            txtTax.Text = "0.0";
            txtNote.Text = "";
            dtpOndate.Value = DateTime.Now.Date;
            this.Text = _sTitle;
            ddlStock.SelectedIndex = 0;
            ddlStock.Focus();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            int iSQuantity=0;
            int i=0;
            
                iSQuantity = Convert.ToInt16(RBtnBuy.Checked ? "+" + txtQnt.Text : "-" + txtQnt.Text);

                //if (chkBS.Checked) //buy
                if(RBtnBuy.Checked)//buy
                {
                    if (MessageBox.Show("Please verify your buy\nStock: " + ddlStock.Text +
                    "\nQuantity: " + txtQnt.Text +
                    "\nPrice: " + txtRate.Text,
                    "Buy Confirmation",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        i = _pdalStock_Trad.NewTrad((int)ddlStock.SelectedValue,
                                Convert.ToDouble(txtRate.Text),
                                dtpOndate.Value, iSQuantity,
                                Convert.ToDouble(txtBrok.Text),
                                Convert.ToDouble(txtTax.Text),
                                txtNote.Text,null);
                        SuccessMesg(i);
                    }
                }
                else //Sell
                {
                    if (_sellValid)
                    {
                        if (FinalValidation())
                        {
                            if (MessageBox.Show("Please verify your sell\nStock: " + ddlStock.Text +
                                "\nQuantity: " + txtQnt.Text +
                                "\nPrice: " + txtRate.Text,
                                "Sell Confirmation",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                i = _pdalStock_Trad.NewTrad((int)ddlStock.SelectedValue,
                                    Convert.ToDouble(txtRate.Text),
                                    dtpOndate.Value, iSQuantity,
                                    Convert.ToDouble(txtBrok.Text),
                                    Convert.ToDouble(txtTax.Text),
                                    txtNote.Text, AccoutingData());
                                SuccessMesg(i);
                            }
                        }
                    }

                }

        }

        /*Generating virtual Datatable*/
        private DataTable AccoutingData()
        {
            DataTable dtAc = new DataTable("jagruti22032005");
            dtAc.Columns.Add("BuyID", typeof(Int16));
            dtAc.Columns.Add("Quantity", typeof(Int16));

            foreach (DataGridViewRow dr in dgvTrading.Rows)
            {
               if (dr.Cells[5].Value != null)
                   if(dr.Cells[5].Value.ToString().Trim() != String.Empty)
                       if(Convert.ToUInt16(dr.Cells[5].Value.ToString().Trim()) > 0)
                dtAc.Rows.Add(new object[] { dr.Cells[6].Value, dr.Cells[5].Value});
            }
            return dtAc;
        }
        private void SuccessMesg(int i)
        {
            if (i > 0)
            {
                MessageBox.Show("Your trad Added successfully", "Save confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cleanup();
            }
            else
                MessageBox.Show("Problem: Please try again to add your trading..", "Try again..", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void fillDatagrid()
        {
            DataView dv = _pdalStock_Trad.AllTradAccount((short)CommonDoubleM.StockType.AllStocks);
            int iCount = 0, iRQuantity=0;
            dgvTrading.Rows.Clear();
            dgvTrading.RowHeadersWidth = 25;
            //theParent.pBarDM.Visible = true;
            foreach (DataRow drTradeAc in dv.Table.Select("StockID= " + (int)ddlStock.SelectedValue))
            {
                //MessageBox.Show(drTradeAc["StockName"].ToString());

                iRQuantity = (int)drTradeAc["Quantity"] -Convert.ToInt16(drTradeAc["SoldQuantity"] != DBNull.Value ? drTradeAc["SoldQuantity"] : 0);
                if (iRQuantity > 0)
                {
                dgvTrading.Rows.Add();

                dgvTrading[0, iCount].Value = drTradeAc["StockID"].ToString();
                dgvTrading[1, iCount].Value = ((DateTime)drTradeAc["TradeOn"]).ToString("dd-MMM-yyyy");//drTradeAc["TradeOn"].ToString();
                dgvTrading[2, iCount].Value = drTradeAc["Quantity"].ToString();
                dgvTrading[3, iCount].Value = drTradeAc["Price"].ToString();
                dgvTrading[4, iCount].Value = iRQuantity;//drTradeAc["SoldQuantity"].ToString();
                dgvTrading[6, iCount].Value = drTradeAc["TradeID"].ToString();
                iCount++;
                }
                //theParent.pBarDM.Value = iCount;
            }
            //theParent.pBarDM.Visible = false;
            //dataGridView1.DataSource = dv;
        }

        private void ddlStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStock.SelectedIndex > 0 && dgvTrading.Visible)
               
            fillDatagrid();
        }

        private void dgvTrading_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgvTrading_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Validate the CompanyName entry by disallowing empty strings.
            ushort result;
            _sellValid = false;

            /*Validation 1 Row level: Positive Integer Input only*/
            if (!String.IsNullOrEmpty(e.FormattedValue.ToString())&&
                dgvTrading[e.ColumnIndex, e.RowIndex].IsInEditMode)
            {
                //Checking for possitive nos.
                if (!UInt16.TryParse(e.FormattedValue.ToString(), out result))
                {
                    dgvTrading["Quantity", e.RowIndex].ErrorText = "Please enter valid Quantity as Number";
                    e.Cancel = true;
                }
                else
                {
                    //dgvTrading["Quantity", e.RowIndex].ErrorText = "";
                    /*Validation 2 each row: Sell quantity can't be more then remaining quantity*/
                    //Make sure only sell quantity taken into consideration for this validation
                    if (e.ColumnIndex == 5)
                        if (Convert.ToInt16(e.FormattedValue) > Convert.ToInt16(dgvTrading["RQuantity", e.RowIndex].Value))
                        {
                            e.Cancel = true;
                            dgvTrading["RQuantity", e.RowIndex].ErrorText = "Sell quantity can't be more than remaning quantity";
                        }
                }
            }
        }

        private void dgvTrading_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            int iAvaileble = 0, iSelling = 0;
            double dblAmount = 0;
            dgvTrading["Quantity", e.RowIndex].ErrorText = "";
            dgvTrading["RQuantity", e.RowIndex].ErrorText = "";
            _sellValid = true;

            foreach (DataGridViewRow dr in dgvTrading.Rows)
            {
                iAvaileble += Convert.ToUInt16(dr.Cells["RQuantity"].Value);
                iSelling += Convert.ToUInt16(dr.Cells["SQuantity"].Value);
                dblAmount += Convert.ToDouble(dr.Cells["Price"].Value) * Convert.ToUInt16(dr.Cells["SQuantity"].Value);
            }
            //MessageBox.Show("Avg. Price of Sell " + dblAmount/iSelling);
            if(iSelling > 0)
            this.Text = _sTitle +" [Avg. " + (dblAmount / iSelling).ToString("##.##") + " ]";
        }

        private bool FinalValidation()
        {
            int iAvaileble = 0, iSelling = 0;

            /*Validation 3: Total Sell quantity can't be more than availeble quantity - should be checked autometically
             Check the Quantity in txtQnt*/

            foreach (DataGridViewRow dr in dgvTrading.Rows)
            {
                iAvaileble += Convert.ToUInt16(dr.Cells["RQuantity"].Value);
                iSelling += Convert.ToUInt16(dr.Cells["SQuantity"].Value);
            }
            if (iAvaileble > 0)
            {
                if (Convert.ToUInt16(txtQnt.Text) != iSelling &&
                    Convert.ToUInt16(txtQnt.Text) <= iAvaileble)
                {
                    MessageBox.Show("Stock sell quantity mismatch", "Recheck - Quantity mismatch", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }

        private void tmrCollapse_Tick(object sender, EventArgs e)
        {
            if (_blnCollapse)
            {
                if (_curHeight > 0)
                {
                    dgvTrading.ScrollBars = ScrollBars.None;
                    this.Height -= _iInterval;
                    //panel1.Height -= _iInterval;
                    dgvTrading.Height -= _iInterval;
                    _curHeight -= _iInterval;
                }
                else
                {
                    tmrCollapse.Enabled = false;
                    dgvTrading.ScrollBars = ScrollBars.Vertical;
                }
            }
            else
            {
                if (_curHeight < _iHeight)
                {
                    dgvTrading.ScrollBars = ScrollBars.None;
                    this.Height += _iInterval;
                    dgvTrading.Height += _iInterval;
                    _curHeight += _iInterval;
                }
                else
                {
                    dgvTrading.ScrollBars = ScrollBars.Vertical;
                    tmrCollapse.Enabled = false;
                }
            }
            
        }

        private void RBtnBuy_CheckedChanged(object sender, EventArgs e)
        {
            if (RBtnBuy.Checked)
            {
                _blnCollapse = true;
                _sellValid = true;
                this.Text = _sTitle;
            }
            else
            {
                _blnCollapse = false;
                _sellValid = false;
                dgvTrading.Visible = true;
                fillDatagrid();
            }
            tmrCollapse.Enabled = true;
        }

    }
}