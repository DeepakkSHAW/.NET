using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmTradingHist : Form
    {
        public frmTradingHist()
        {
            InitializeComponent();
        }

        private DALDoubleM _pdalStock_View;
        DataView dvStocks;
        public frmTradingHist(object pdalStockArange)
        {
            InitializeComponent();
            _pdalStock_View = (DoubleM.DALDoubleM)pdalStockArange;
        }
        private void frmTradings_Load(object sender, EventArgs e)
        {
            //dgvTrading.DataSource = _pdalStock_View.TradingValue();
        }

        internal void FillTradeBook(string sSQL)
        {
            //dgvTrading.DataSource = _pdalStock_View.TradingValue(sSQL);
            //dgvTrading.DataSource = _pdalStock_View.TradingValue(sSQL);
            dgvTrading.DataSource = _pdalStock_View.TradingValue(sSQL);
        }

        private void frmTradings_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}