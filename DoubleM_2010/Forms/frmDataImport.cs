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
    public partial class frmDataImport : Form
    {
        public frmDataImport()
        {
            InitializeComponent();
        }

        DataGridViewCheckBoxColumn c1;
        CheckBox ckBox;

        private DALDoubleM _pdalImportData;
        private frmMain _theParent;
        private DataView _dvStocks;

        private const string initMsg = "After loading of stock rates from external file\nRight click of header of sheet below.\nAnd Choose {0} and {1} Columns..";
        private short _ImportWhat = -1;
        private string _MsgSelect = string.Empty;
        private int _ClickColumn = -1;
        private string[] _fields;
        private string[] _menuItems;

        internal enum ImportWhat : short { SingleRates = 0, MultipleRates= 1, Nothing = 2 };

        public frmDataImport(object pdalImportData, short vImportwhat)
        {
            InitializeComponent();
            _pdalImportData = (DoubleM.DALDoubleM)pdalImportData;
            _ImportWhat = vImportwhat;
        }

        #region Control Events
        private void frmDataImport_Load(object sender, EventArgs e)
        {
            _theParent = (frmMain)this.ParentForm; //To access MDI parent control
            /*Add Checkbox dynamically to 1st column of each row*/
            c1 = new DataGridViewCheckBoxColumn();
            c1.Width = 30;
            c1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvImport.Columns.Add(c1);
            dgvImport.Columns[0].Resizable = DataGridViewTriState.False;
            dgvImport.Columns[0].Frozen = true;
            dgvImport.Columns[0].ReadOnly = false;

            dgvImport.RowHeadersWidth = 60;
            dgvImport.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvImport.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            /*Add Checkbox to header*/
            ckBox = new CheckBox();
            //Get the column header cell bounds
            Rectangle rect = dgvImport.GetCellDisplayRectangle(0, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position correctly.
            rect.X = rect.Location.X + (rect.Width / 4);

            ckBox.Size = new Size(18, 18);
            //Change the location of the CheckBox to make it stay on the header
            ckBox.Location = rect.Location;
            ckBox.CheckedChanged += new EventHandler(ckBox_CheckedChanged);
            //Add the CheckBox into the DataGridView
            dgvImport.Controls.Add(ckBox);
            ckBox.Visible = false;

            GetStocks();

            switch (_ImportWhat)
            {
                case (short)ImportWhat.SingleRates:
                    this.Text = "Data Import of Single Stock";
                    lblMsg.Text = string.Format(initMsg, "Date", "Rate");
                    lblMode.Text = "Stocks";
                    ddlStock.Visible = true;

                    _MsgSelect = "Selected fields are \n[Dates] = {0}\n[Rates] = {1}";
                    _fields = new string[2];
                    _menuItems = new string[] { "Click Column is: Dates", "Clicked Column is: Rates" };
                    cmsLoaddata.Items.Add(_menuItems[0]);
                    cmsLoaddata.Items.Add(_menuItems[1]);
                    break;

                case (short)ImportWhat.MultipleRates:
                    this.Text = "Data Import of Multiple Stock";
                    lblMsg.Text = string.Format(initMsg, "Stocks Code", "Rate");
                    lblMode.Text = "Date\\Time";
                    dtpPriceDate.Visible = true; dtpPriceDate.Value = DateTime.Now;
                    chkActiveStock.Visible = true; chkActiveStock.CheckState = CheckState.Indeterminate;
                    _MsgSelect = "Selected fields are \n[Stocks] = {0}\n[Rates] = {1}";
                    _fields = new string[2];
                    _menuItems = new string[] { "Click Column is: Stocks", "Clicked Column is: Rates" };
                    cmsLoaddata.Items.Add(_menuItems[0]);
                    cmsLoaddata.Items.Add(_menuItems[1]);
                    break;
                default:
                    break;

            }
        }

        void ckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < this.dgvImport.RowCount; j++)
            {
                this.dgvImport[0, j].Value = this.ckBox.Checked;
            }
            this.dgvImport.EndEdit();
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            _theParent.lblDMMsg.Text = "";
            openFileDlgImport.Filter = "CSV (*.csv)|*.csv|TEXT (*.txt)|*.txt|ASCII (*.asc)|*.asc|TAB (*.tab)|*.tab";
            openFileDlgImport.InitialDirectory = CommonDoubleM._DoubleMPath;
            openFileDlgImport.FileName = string.Empty;
            if (openFileDlgImport.ShowDialog() == DialogResult.OK)
                txtFileName.Text = openFileDlgImport.FileName;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            long iCount = 0;

            switch (_ImportWhat)
            {
                case (short)ImportWhat.SingleRates:
                    lblMsg.Text = string.Format(initMsg, "Date", "Rate");
                    break;

                case (short)ImportWhat.MultipleRates:
                    lblMsg.Text = string.Format(initMsg, "Stocks Code", "Rate");
                    break;
            }

            _theParent.lblDMMsg.Text = "Loading file: " + txtFileName.Text;

            if (System.IO.File.Exists(txtFileName.Text))
            {
                DataTable dtImport = ExportExcel.ReadCSV(txtFileName.Text);
                dgvImport.DataSource = null;
                //Column's SortMode cannot be set to Automatic while the DataGridView control's SelectionMode is set to ColumnHeaderSelect.
                dgvImport.DataSource = dtImport;

                ckBox.Visible = dgvImport.RowCount > 0 ? true : false;
                ckBox.Checked = false;

                //1st column 'holding checkbox' is not read-only 
                foreach (DataGridViewColumn dc in dgvImport.Columns)
                {
                    if (dc.Index > 0)
                        dc.ReadOnly = true;
                }
                //row header count
                foreach (DataGridViewRow dr in dgvImport.Rows)
                {
                    iCount++;
                    dr.HeaderCell.Value = iCount.ToString();
                }
            }
            else
            { _theParent.lblDMMsg.Text = "File [" + txtFileName.Text + "] doesn't exist"; }
        }
        private void dgvImport_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex > 0 && e.RowIndex < 0)
                {
                    _ClickColumn = e.ColumnIndex;
                    dgvImport.ContextMenuStrip = cmsLoaddata;
                }
                else
                    dgvImport.ContextMenuStrip = null;
                //MessageBox.Show("Col " + e.ColumnIndex.ToString() + "\nRow " + e.RowIndex.ToString());               
            }
            else
                dgvImport.ContextMenuStrip = null;
        }

        private void cmsLoaddata_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Application.DoEvents();
            StringBuilder sbLable = new StringBuilder();
            for (int i = 0; i < _menuItems.Length; i++)
            {
                if (_menuItems[i] == e.ClickedItem.Text)
                {
                    //Clicked field name collection
                    _fields[i] = dgvImport.Columns[_ClickColumn].Name;
                    //Display the msg on lable
                    lblMsg.Text = sbLable.AppendFormat(_MsgSelect,
                        _fields[0] == null ? "??" : _fields[0],
                        _fields[1] == null ? "??" : _fields[1]
                        ).ToString();
                    break;
                }
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            _theParent.lblDMMsg.Text = "";
            DataTable dtInsert;
            string msg = string.Empty;
            bool validated = false;

            switch (_ImportWhat)
            {
                case (short)ImportWhat.SingleRates:
                    validated = CheckBeforeImport_SingleRates();
                    msg = "Are you ready to import [{0}] items \nfor stock -" + ddlStock.Text+" ?";
                    break;

                case (short)ImportWhat.MultipleRates:
                   validated= CheckBeforeImport_MultipleRates();
                   msg = "Are you ready to import " + chkActiveStock.Tag.ToString() + " [{0}] Stocks. \nRates would be saved against " + dtpPriceDate.Value.ToString("dd-MMM-yyyy HH:mm:ss tt")+" ?";
                   break;
                default:
                   break;
            }
            if (validated)
            {
                dtInsert=getRatesToImport();
                if (dtInsert.Rows.Count <= 0)
                    MessageBox.Show("No item checked to import","Please select items",MessageBoxButtons.OK,MessageBoxIcon.Hand);

                else if (MessageBox.Show(string.Format(msg, dtInsert.Rows.Count.ToString()),"Import confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    bool blnReturn =  _pdalImportData.BulkUpdateStockprice(dtInsert);
                    if (blnReturn) _theParent.lblDMMsg.Text = "Successfully imported rates of " + ddlStock.Text + ". Total import was " + dtInsert.Rows.Count.ToString();
                }

            }
        }

        private void chkActiveStock_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkActiveStock.CheckState == CheckState.Checked)
            {
                chkActiveStock.Text = "Include Only Active Stocks";
                chkActiveStock.Tag = "Active";
            }
            else if (chkActiveStock.CheckState == CheckState.Unchecked)
            {
                chkActiveStock.Text = "Include Only Inactive Stocks";
                chkActiveStock.Tag = "Inactive";
            }
            else
            {
                chkActiveStock.Text = "Include All Stocks";
                chkActiveStock.Tag = "All";
        }
        }


        #endregion
        #region Private Methods
        private void GetStocks()
        {
            
            string sDM = "", sVM = "";
            _dvStocks= _pdalImportData.Stocks(ref sDM, ref sVM, 10); //10: all stocks active nonactive
            ddlStock.DataSource = _dvStocks;
            ddlStock.DisplayMember = sDM;
            ddlStock.ValueMember = sVM;

            if (ddlStock.Items.Count > 0) ddlStock.SelectedIndex = 0;
        }
        private bool CheckBeforeImport_SingleRates()
        {
            bool hasTime = true;
            //Looking for date
            try
            {
                if (_fields[0] == null) { lblMsg.Text = "Date not selected\n"; return false; }
                if (_fields[1] == null) { lblMsg.Text = "Price not selected\n"; return false; }
                if (dgvImport.Columns[_fields[0]].ValueType.Name != "DateTime") { lblMsg.Text = "Column [" + _fields[0] + "] is not Date type\n"; return false; }
                if (dgvImport.Columns[_fields[1]].ValueType.Name != "Double") { lblMsg.Text = "Column [" + _fields[1] + "] is not Price type\n"; return false; }

                foreach (DataGridViewRow dr in dgvImport.Rows)
                {
                    if (dr.Cells[_fields[0]].Value.ToString().Substring(dr.Cells[_fields[0]].Value.ToString().IndexOf(" ") + 1).Contains("00:00"))
                    {
                        dr.Selected = true;
                        hasTime = false;
                    }
                }

                if (!hasTime)
                {
                    lblMsg.Text = "Time is missing in selected items.\nPlease add default time";
                    frmDateTime pfrmDateTime = new frmDateTime("Default Time", true,false);
                    if (pfrmDateTime.ShowDialog() == DialogResult.OK)
                    {

                        foreach (DataGridViewRow dr in dgvImport.Rows)
                        {
                            dr.Cells[_fields[0]].Value = DateTime.Parse(dr.Cells[_fields[0]].Value.ToString()).ToShortDateString() +
                                " " + CommonDoubleM._SelectedDateTime.ToShortTimeString();
                            dr.Selected = false;
                        }
                        this.dgvImport.EndEdit();
                        return true;
                    }
                    pfrmDateTime.Dispose();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Validation not completed successfully, please try again..","Validation failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                _theParent.lblDMMsg.Text = "Validation not completed successfully, please try again..["+ex.Message+"]";
                CommonDoubleM.LogDM("CheckBeforeImport_SingleRates "+ex.Message);
                return false;
            }
        }

        private bool CheckBeforeImport_MultipleRates()
        {
            //Looking for date
            int iRow = -1;
            dgvImport.ClearSelection();
            _theParent.pBarDM.Maximum = 32000;
            _theParent.pBarDM.Value = 0;
            if (_fields[0] == null) { lblMsg.Text = "Stocks code not selected\n"; return false; }
            if (_fields[1] == null) { lblMsg.Text = "Price not selected\n"; return false; }
            if (dtpPriceDate.Value.CompareTo(DateTime.Now) > 0) { lblMsg.Text = "Future date is not valid."; return false; }
            if (dgvImport.Columns[_fields[0]].ValueType.Name != "Int32") { lblMsg.Text = "Column [" + _fields[0] + "] doesn't contain stock code\n"; return false; }
            if (dgvImport.Columns[_fields[1]].ValueType.Name != "Double") { lblMsg.Text = "Column [" + _fields[1] + "] is not Price type\n"; return false; }

            _dvStocks.Sort = "BSECode";

            //selecting stocks in your portfolio
            
            foreach (DataGridViewRow dr in dgvImport.Rows)
            {
                iRow= _dvStocks.Find(dr.Cells[_fields[0]].Value);
                if (iRow >= 0)
                {
                    if (chkActiveStock.CheckState == CheckState.Indeterminate)
                    {
                        dr.Selected = true;
                        dr.Cells[0].Value = true;
                        _theParent.pBarDM.Value++;
                    }
                    else if ((bool)_dvStocks[iRow].Row["Active"] == chkActiveStock.Checked)
                    {
                        dr.Selected = true;
                        dr.Cells[0].Value = true;
                        _theParent.pBarDM.Value++;
                    }
                    else
                        dr.Cells[0].Value = false;
                }
                else
                {
                    dr.Cells[0].Value = false;
                }
            }
            this.dgvImport.EndEdit();
            _theParent.lblDMMsg.Text = "[" + _theParent.pBarDM.Value.ToString()+ "] found stocks in your portfolio.";
            return true;
        }
        private DataTable getRatesToImport()
        {
            DataTable dtLatestRates = new DataTable("LatestRate");
            Int32 iCount = 0;
            string sImportDate=string.Empty;

            try
            {
                dtLatestRates.Columns.Add("StockID", typeof(int));
                dtLatestRates.Columns.Add("Price", typeof(double));
                dtLatestRates.Columns.Add("Ondate", typeof(DateTime));

                _theParent.pBarDM.Visible = true;
                _theParent.pBarDM.Maximum = dgvImport.Rows.Count;
                foreach (DataGridViewRow dr in dgvImport.Rows)
                {
                    if (dr.Cells[0].Value != null)
                        if ((bool)dr.Cells[0].Value)
                        {
                            if (_ImportWhat == (short)ImportWhat.SingleRates)
                                sImportDate = dr.Cells[_fields[0]].Value.ToString();
                            else if (_ImportWhat == (short)ImportWhat.MultipleRates)
                                sImportDate = dtpPriceDate.Value.ToString();

                            dtLatestRates.Rows.Add(new string[] {ddlStock.SelectedValue.ToString(),
                            dr.Cells[_fields[1]].Value.ToString(),
                            sImportDate});
                        }
                    _theParent.pBarDM.Value = iCount++;
                }
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
            }
            finally { _theParent.pBarDM.Visible = false; }
           return dtLatestRates; 
        }

        #endregion










    }
}
