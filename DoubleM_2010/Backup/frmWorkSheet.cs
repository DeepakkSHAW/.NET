using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmWorkSheet : Form
    {
        private DALDoubleM _pdalStock_Base;
        public frmWorkSheet(object pdalBase)
        {
            InitializeComponent();
            _pdalStock_Base = (DoubleM.DALDoubleM)pdalBase;
        }

        private void frmBase_Load(object sender, EventArgs e)
        {

        }


    }
}