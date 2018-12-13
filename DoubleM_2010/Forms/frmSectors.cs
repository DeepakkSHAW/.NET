using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoubleM
{
    public partial class frmSectors : Form
    {
        private DALDoubleM _pdalSectors;
        private string _ddlSectorsName = string.Empty, _ddlSectorsValue = string.Empty;
        DataView _dvSectors;
        private int  _SectorsID = -1;
        public frmSectors(object pdalSector, int SectorID)
        {
            InitializeComponent();
            _pdalSectors = (DoubleM.DALDoubleM)pdalSector;
            _dvSectors = _pdalSectors.getSectors(ref _ddlSectorsName, ref _ddlSectorsValue);
            _SectorsID = SectorID;
        }

        private void frmSectors_Load(object sender, EventArgs e)
        {
            ddlSectors.DataSource = _dvSectors.ToTable();
            ddlSectors.DisplayMember = _ddlSectorsName;
            ddlSectors.ValueMember = _ddlSectorsValue;

            if (_SectorsID > 0)
            {
                this.Text += " [Update]";
                ddlSectors.SelectedValue = _SectorsID;
            }
            else
            {
                this.Text += " [Add New]";
                ddlSectors.Text = "";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
            if (_SectorsID == 0) //Add new
            {
                //Check for Stock name. It can't be empty
                if (ddlSectors.Text.Trim().Length < 1)
                {
                    MessageBox.Show("Sectors name is required.", "Add new Sector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ddlSectors.Focus();
                    return;
                }
                //Check for Sector name already exist. Duplicate Sector name is not allowed
                if (ddlSectors.SelectedValue != null)//Within the list
                {
                    MessageBox.Show("Sector already available in your DoubleM", "Add new Sector", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                InsertSector();
            }
            else if (_SectorsID > 0) //Update
            {
                //Check for Stock name. It can't be empty
                if (ddlSectors.Text.Trim().Length < 1)
                {
                    MessageBox.Show("Sectors name Can't be Empty", "Update Sector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ddlSectors.Focus();
                    return;
                }
                //Check for Sector name already exist. Duplicate Sector name is not allowed
                if (ddlSectors.SelectedValue == null)//Within the list
                    UpdateSector();
                else if ((int)ddlSectors.SelectedValue == _SectorsID)
                    UpdateSector();
                else
                {
                    MessageBox.Show("Sector already available in your DoubleM", "Add new Sector", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    ddlSectors.Focus();
                    return;
                } 
            }


        }

 
        private void InsertSector()
        {
            if (_pdalSectors.NewSector(ddlSectors.Text.Trim()) == 1)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }
        private void UpdateSector()
        {
            if (_pdalSectors.UpdateSector(ddlSectors.Text.Trim(),_SectorsID) == 1)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}