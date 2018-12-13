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
        public frmNewTrad(object pdalSNew)
        {
            InitializeComponent();
            _pdalStock_Trad = (DoubleM.DALDoubleM)pdalSNew;
        }

        private void frmNewTrad_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(this.GetType().Assembly.GetManifestResourceStream("DoubleM.Pics.Colors.ico"));

            string sDM = "", sVM = "";
            Cursor = Cursors.WaitCursor;
            dtpOndate.Value = DateTime.Now.Date;
            ddlStock.DataSource = _pdalStock_Trad.Stocks(ref sDM, ref sVM, 0);
            
            ddlStock.DisplayMember = sDM;
            ddlStock.ValueMember = sVM;
            if (ddlStock.Items.Count > 0) ddlStock.SelectedIndex = 0;
            Databinding();
            Cursor = Cursors.Default;
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
            ddlStock.SelectedIndex = 0;
            chkBS.Checked = true;
            txtQnt.Text = "0";
            txtRate.Text = "0.0";
            txtBrok.Text = "0.0";
            txtTax.Text = "0.0";
            txtNote.Text = "";
            dtpOndate.Value = DateTime.Now.Date;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            int iSQuantity=0;
            int i=0;
            if (MessageBox.Show("Please verify\nStock: " + ddlStock.Text +
                                "\nQuantity: " + txtQnt.Text +
                                "\nPrice: " + txtRate.Text
                , "Confirmation", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                iSQuantity = Convert.ToInt16(chkBS.Checked ? "+" + txtQnt.Text : "-" + txtQnt.Text);
                //iSQuantity = Convert.ToInt16(txtQnt.Text);
                //MessageBox.Show(txtQnt.Text);
                //MessageBox.Show(ddlStock.SelectedValue.ToString());

                i = _pdalStock_Trad.NewTrad((int)ddlStock.SelectedValue, Convert.ToDouble(txtRate.Text),
                            dtpOndate.Value, iSQuantity,
                        Convert.ToDouble(txtBrok.Text), Convert.ToDouble(txtTax.Text),txtNote.Text);
                if (i > 0)
                {
                    MessageBox.Show("Add Trading successfully");
                    Cleanup();
                }
                else
                    MessageBox.Show("Problem: Please try again to add your trading..");
            }
        }
    }
}