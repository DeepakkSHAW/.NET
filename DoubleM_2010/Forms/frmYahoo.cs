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
    public partial class frmYahoo : Form
    {
        private System.Windows.Forms.ToolStripStatusLabel _lblMsgDoubleM;
        private System.Windows.Forms.ToolStripProgressBar _pBar;
        private DALDoubleM _pdalYahoo;

        private DataView _dvStocks;
        private string _mode = string.Empty;
        private const string URLYahoo = 
        "http://ichart.finance.yahoo.com/table.csv?s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g={7}&ignore=.csv";
        /*
        http://ichart.finance.yahoo.com/table.csv?s=WIPRO.NS&a=10&b=25&c=2005&d=11&e=26&f=2009&g=d&ignore=.csv
        http://ichart.finance.yahoo.com/table.csv?s=WIPRO.NS&a=07&b=12&c=2002&d=09&e=21&f=2009&g=w&ignore=.csv
        http://ichart.finance.yahoo.com/table.csv?s=WIPRO.NS&a=07&b=12&c=2002&d=09&e=21&f=2009&g=m&ignore=.csv
        http://ichart.finance.yahoo.com/table.csv?s=WIPRO.NS&a=07&b=12&c=2002&d=09&e=21&f=2009&g=v&ignore=.csv
        */

        public frmYahoo(object pdalStockAc,
            System.Windows.Forms.ToolStripStatusLabel plbl,
            System.Windows.Forms.ToolStripProgressBar pBar)
        {
            InitializeComponent();
            _pdalYahoo = (DoubleM.DALDoubleM)pdalStockAc;
            _lblMsgDoubleM = plbl;
            _pBar = pBar;
        }

        private void frmYahoo_Load(object sender, EventArgs e)
        {
            rdoDaily.Checked = true;
            dtpStart.Value = DateTime.Now.AddYears(-1);
            dtpEnd.Value = DateTime.Now;

            string sDM = "", sVM = "";
            Cursor = Cursors.WaitCursor;
            //All stocks
            _dvStocks = _pdalYahoo.Stocks(ref sDM, ref sVM, 10);
            ddlStock.DataSource = _dvStocks;

            ddlStock.DisplayMember = sDM;
            ddlStock.ValueMember = sVM;
            if (ddlStock.Items.Count > 0) ddlStock.SelectedIndex = 0;
            Cursor = Cursors.Default;
        }

        private void rdoButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton obj = (RadioButton)sender;
            if(obj.Checked)

                switch (obj.Name)
                {
                    case "rdoDaily":
                        _mode = "d";
                        break;
                    case "rdoWeekly":
                        _mode = "w";
                        break;
                    case "rdoMonthly":
                        _mode = "m";
                        break;
                    case "rdoDividends":
                        _mode = "v";
                        break;

                }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string sURL = string.Empty;
            string sSName = string.Empty;
            long DownloadBytes = 0;

            try
            {
                sSName = _dvStocks[(int)ddlStock.SelectedIndex]["YFCode"].ToString();
                sURL = string.Format(URLYahoo, sSName,
                                    dtpStart.Value.ToString("MM"),
                                    dtpStart.Value.ToString("dd"),
                                    dtpStart.Value.ToString("yyyy"),
                                    dtpEnd.Value.ToString("MM"),
                                    dtpEnd.Value.ToString("dd"),
                                    dtpEnd.Value.ToString("yyyy"),
                                    _mode);
                //Console.WriteLine(sURL);

                SaveFileDialog saveFileDlgExport = new SaveFileDialog();
                saveFileDlgExport.Title = "Save file [Historical Data from Yahoo]";
                saveFileDlgExport.FileName = sSName = _dvStocks[(int)ddlStock.SelectedIndex]["ShortName"].ToString();
                saveFileDlgExport.Filter = "CSV (*.csv)|*.csv";
                saveFileDlgExport.InitialDirectory = CommonDoubleM._DoubleMPath;
                if (saveFileDlgExport.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    DownloadBytes = CommonDoubleM.DownloadFile(sURL, saveFileDlgExport.FileName, _pBar);
                    
                    if (DownloadBytes > -1)
                        _lblMsgDoubleM.Text = "Download " + CommonDoubleM.FormatBytes(DownloadBytes);
                    else
                        _lblMsgDoubleM.Text = "No data available for selected date, please change the date & try again..";
                    
                    Cursor = Cursors.Default;
                }

            }
            catch (Exception ex)
            {
                _lblMsgDoubleM.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }
        }
    }
}


