using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmStocks : Form
    {
        private DALDoubleM _pdalStock_View;
        DataView dvStocks;
        public frmStocks(object pdalStockArange)
        {
            InitializeComponent();
            _pdalStock_View = (DoubleM.DALDoubleM)pdalStockArange;
        }

        private void frmStocks_Load(object sender, EventArgs e)
        {
            chkActive.Checked = true;
        }
        private void LoadGridDate()
        {
            this.Cursor = Cursors.WaitCursor;
            string s1 = "", s2 = "";
            short ii =(short)(chkActive.Checked ? 0 : -1);
            dvStocks = _pdalStock_View.Stocks(ref s1, ref s2, ii);
            dgvStocks.DataSource = dvStocks;
            ddlStock.DataSource = dvStocks;
            ddlStock.DisplayMember = s1;
            ddlStock.ValueMember = s2;

            ddlSName.DataSource = dvStocks;
            ddlSName.DisplayMember = "ShortName";
            ddlSName.ValueMember = s2;

            ddlYCode.DataSource = dvStocks;
            ddlYCode.DisplayMember = "YFCode";
            ddlYCode.ValueMember = s2;

            ddlBCode.DataSource = dvStocks;
            ddlBCode.DisplayMember = "HDFCCode";
            ddlBCode.ValueMember = s2;

            chkAN.DataBindings.Clear();
            chkAN.DataBindings.Add("Checked", dvStocks, "Active");
            comboBox1.DataBindings.Clear();
            comboBox1.DataBindings.Add("DisplayMember", dvStocks, "Active");
            /* Set up the ErrorProvider */
            this.errorProvider1.DataSource = dvStocks;

            ///* Data type Binding with text controls */
            //txtStock.DataBindings.Clear();
            //txtStock.DataBindings.Add("Text", dvStocks, "StockName", true, DataSourceUpdateMode.OnPropertyChanged);

            this.Cursor = Cursors.Default;
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridDate();
        }
    }
}