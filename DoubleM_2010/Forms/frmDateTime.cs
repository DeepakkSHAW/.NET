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
    public partial class frmDateTime : Form
    {
        public frmDateTime(string Title,bool timeRequired, bool dateRequired)
        {
            InitializeComponent();
            this.Text = Title;
            dtpTime.Checked = timeRequired;
            mcDate.Enabled = dateRequired;
            this.Width = mcDate.Width +7;
            this.Height = mcDate.Height + 60;
            
        }

        private void frmDateTime_Load(object sender, EventArgs e)
        {
            mcDate.SelectionStart = CommonDoubleM._SelectedDateTime;
            if(dtpTime.Checked)
            dtpTime.Value = CommonDoubleM._SelectedDateTime;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.MinValue;
            dt = mcDate.SelectionStart;
           
            if (dtpTime.Checked)
                CommonDoubleM._SelectedDateTime = 
                    DateTime.Parse(mcDate.SelectionStart.ToShortDateString() + " " + dtpTime.Value.ToLongTimeString());
            else
                CommonDoubleM._SelectedDateTime = 
                    DateTime.Parse(mcDate.SelectionStart.ToShortDateString());

            this.DialogResult = DialogResult.OK;
        }
    }
}
