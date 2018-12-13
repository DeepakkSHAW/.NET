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
    public partial class frmMngStocks : Form
    {
        private DALDoubleM _pdalMngStocks;
        private frmMain _theParent; //To access MDI parent control
        
        private DataView _dvSectors;
        private DataView _dvScripts;

        //private DataTable _dtScipts;
        //private DataTable _dtAfterfilter;
        private Boolean blnBbaseRow;
        //private string _sSelectActive = "[Active] = TRUE";
        private int _ScriptID = -1;
        private string _ScriptName = string.Empty;
        private string _ddlSectorsName = string.Empty, _ddlSectorsValue = string.Empty;
        private string _ddlScriptName = string.Empty, _ddlScriptValue = string.Empty;
        private string _scriptType = string.Empty, _sFilter = string.Empty;

        public frmMngStocks(object pdalMngStockPrice)
        {
            InitializeComponent();
            _pdalMngStocks = (DoubleM.DALDoubleM)pdalMngStockPrice;

            loadFromDB();
        }


#region Control Events
        private void frmMngStocks_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            _theParent = (frmMain)this.ParentForm;

            dgvLayout();
            databindingFormControls();

            //programmatically click Active stocks button
            tsbtnActiveScript.PerformClick();
            clearInputs();
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(dgvScripts);
            cs.MaxHeight = 1000;
            cs.Width = 130;
        }
        private void tsbtnScriptAction_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;

            tsbtnAllScript.Checked = false;
            tsbtnActiveScript.Checked = false;
            tsbtnInactiveScript.Checked = false;

            obj.Checked = true;

            if (obj.Name == "tsbtnAllScript")
            {
                _scriptType = "[Active] <> 10";
                tsbtnScripts.ToolTipText = "Stocks Type [All Stocks selected]";
            }
            else if (obj.Name == "tsbtnActiveScript")
            {
                _scriptType = "[Active] = TRUE";
                tsbtnScripts.ToolTipText = "Stocks Type [Only Active Stocks selected]";
            }
            else
            {
                _scriptType = "[Active] = FALSE";
                tsbtnScripts.ToolTipText = "Stocks Type [Only Inactive Stocks selected]";
            }

            _sFilter = "All";
            fillData();
        }
        private void tscboSectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscboSectors.SelectedIndex > 0)
            {
                _sFilter = "All";
                fillData();
            }
        }
        private void tsScript_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            _sFilter = e.ClickedItem.Text;
            fillData();
        }
        private void dgvScripts_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int i = 0;

            if (!blnBbaseRow)
            {
                dgvScripts.Rows[e.RowIndex].HeaderCell.Value = "##";
                blnBbaseRow = true;
            }
            else
            {
                dgvScripts.Rows[e.RowIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                i = e.RowIndex + 1;
                dgvScripts.Rows[e.RowIndex].HeaderCell.Value = i.ToString();
            }
        }
        private void dgvScripts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.RowIndex < dgvScripts.RowCount - 1 && tsbtnClipboard.CheckState == CheckState.Checked)
                    //_theParent.lblDMMsg.Text = dgvScripts["Tr. Code", e.RowIndex].Value.ToString();
                    Clipboard.SetText(dgvScripts["Tr. Code", e.RowIndex].Value.ToString());

            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
            }
        }
        private void dgvScripts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dgvScripts.RowCount - 1)
            {
                _ScriptID = Convert.ToInt32(dgvScripts[7, e.RowIndex].Value);
                DataRow[] drStocks = _dvScripts.Table.Select("StockID=" + _ScriptID.ToString());
                if (drStocks.Length == 1)
                {
                    tsbtnUpdate.CheckState = CheckState.Checked;
                    ddlSectors.SelectedValue = drStocks[0][1]; //Convert.ToInt64(dgvScripts[8, e.RowIndex].Value);
                    ddlStock.Text = drStocks[0][2].ToString();
                    chkActive.Checked = drStocks[0][8].ToString() == "True" ? true : false;
                    if (!tsbtnAuto.Checked) //When Autofind disable
                    {
                        ddlSName.Text = drStocks[0][3].ToString();
                        ddlYahoo.Text = drStocks[0][4].ToString();
                        ddlRediff.Text = drStocks[0][5].ToString();
                        ddlBSE.Text = drStocks[0][6].ToString();
                        ddlBCode.Text = drStocks[0][7].ToString();
                    }

                    _ScriptName = ddlStock.Text;
                    tsbtnUpdate.ToolTipText = "Update stocks details [" + _ScriptName + "]";
                    _theParent.lblDMMsg.Text = "You have selected  [" + _ScriptName + "].";
                    ddlStock.Focus();
                }
                else
                {
                    _theParent.lblDMMsg.Text = "Stock or Stocks details not found, you may need to refresh or restart the application.";
                }
            }
            else
            {
                _theParent.lblDMMsg.Text = "Please double click on valid rows..";
                tsbtnUpdate.ToolTipText = "Update stocks details";
            }
        }
        private void tsScriptMng_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string sOldScriptName = string.Empty;
            int iSectorID = -1, result;
            frmSectors pfrmSectors;

            switch (e.ClickedItem.Name)
            {
            ///////////// Insert new script//////////////////
            case "tsbtnNew":
                    //Check for Stock name. It can't be empty
                    if (ddlStock.Text.Trim().Length < 1)
                    {
                        MessageBox.Show("Stock name required.", "Add new Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ddlStock.Focus();
                        break;
                    }
                    //Check for stock name already exist. Duplicate stock name not allowed
                    if (ddlStock.SelectedValue != null)//Within the list
                    {
                        MessageBox.Show("Script already available in your DoubleM", "Add new Stock", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                        else
                        InsertStock(); 
                    
                                break;

            ///////////// Update Script//////////////////
            case "tsbtnUpdate":
             dgvScripts.Focus();

             //Check for record selection for update
             if (!tsbtnUpdate.Checked || _ScriptID < 1)
              {
               MessageBox.Show("No script has been selected for update.\nDouble click to select the script to updated.", "Script Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
              }

             //Check for Stock name. It can't be empty
             if (ddlStock.Text.Trim().Length < 1)
             {
                 MessageBox.Show("Stock name required.", "Stock update failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 ddlStock.Focus();
                 break;
             }
             //Check for stock name already exist. Duplicate stock name not allowed
             if (ddlStock.SelectedValue != null)//Within the list
             {
                 if ((int)ddlStock.SelectedValue != _ScriptID) //Already in the list - not allowed
                 {
                     MessageBox.Show("Script already available in your DoubleM", "Stock update failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                     break;
                 }
                 else
                     UpdateStock();
             }
             else
                     UpdateStock();

                    break;
            ///////////// Delete Script /////////////
            case "tsbtnDeleteStock":
                    dgvScripts.Focus();

                    //Check for record selection for update
                    if (_ScriptID < 1)
                    {
                        MessageBox.Show("No script has been selected for update.\nDouble click to select the script to updated.", "Script Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    DeleteStock();
                    break;
            ///////////// Control databinding /////////////
            case "tsbtnAuto":
                    tsbtnAuto.Checked = !tsbtnAuto.Checked;
                    //this.tsbtnAuto.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnScripts.Image")));
                    //this.tsbtnAuto.Image = Properties.Resources.der;
                    AutoFind();
                    break;
            ///////////// Refresh Script loading from DB again /////////////
            case "tsbtnRefesh":
                    loadFromDB();
                    databindingFormControls();
                    //programmatically click Active stocks button
                    tsbtnActiveScript.PerformClick();
                    clearInputs();
                    break;
            /**************************************************************/
            /////////////           Handeling sectors          /////////////
            /**************************************************************/


            case "tsbtnNewSector":
                    _theParent.lblDMMsg.Text = "Adding a new sector";
                    pfrmSectors = new frmSectors(_pdalMngStocks,0);

                    if (pfrmSectors.ShowDialog() == DialogResult.OK)
                    {
                        _theParent.lblDMMsg.Text = "Added new Sector seccessfully";
                        //refresh dropdown lists of sectors
                        _dvSectors = _pdalMngStocks.getSectors(ref _ddlSectorsName, ref _ddlSectorsValue);
                        databindingFormControls();
                    }
                    break;
            case "tsbtnEditSector":
                    
                    
                    if (tscboSectors.SelectedIndex > -1)
                        if (int.TryParse(((ComboBox)tscboSectors.Control).SelectedValue.ToString(), out result))
                            iSectorID = Convert.ToInt32(((ComboBox)tscboSectors.Control).SelectedValue);

                    if (iSectorID < 1)
                    {
                        MessageBox.Show("Please select a valid sector to update", "Sector update", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;
                    }

                    _theParent.lblDMMsg.Text = "Updating Sector";
                    pfrmSectors = new frmSectors(_pdalMngStocks, iSectorID);

                    if (pfrmSectors.ShowDialog() == DialogResult.OK)
                    {
                        //refresh dropdown lists of sectors
                        _theParent.lblDMMsg.Text = "Sector seccessfully updated";
                        _dvSectors = _pdalMngStocks.getSectors(ref _ddlSectorsName, ref _ddlSectorsValue);
                        databindingFormControls();
                    }

                    break;
            case "tsbtnDeleteSector":
                    if (tscboSectors.SelectedIndex > -1)
                        if (int.TryParse(((ComboBox)tscboSectors.Control).SelectedValue.ToString(), out result))
                            iSectorID = Convert.ToInt32(((ComboBox)tscboSectors.Control).SelectedValue);

                    if (iSectorID < 1)
                    {
                        MessageBox.Show("Please select a valid sector to Delete", "Sector Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }


                    DataRow[] dr = _dvScripts.ToTable().Select(" StockCatID = " + iSectorID.ToString());
                    if(dr.Length != 0)    
                    {
                        MessageBox.Show("Select sector has been associated  with "+dr.Length+" scripts", "Failed to Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;

                    }
                    //final confirmation
                    if (MessageBox.Show("You are going to delete " + ((ComboBox)tscboSectors.Control).Text 
                        + " sector!!", "Sector Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    break;

                    _theParent.lblDMMsg.Text = "Deleting Sector";

                    if (_pdalMngStocks.DeleteSector(iSectorID) == 1)
                    {
                        _dvSectors = _pdalMngStocks.getSectors(ref _ddlSectorsName, ref _ddlSectorsValue);
                        databindingFormControls();
                    }
                    break;

            default:
                    break;

            }

        }


#endregion

#region Private Methods
        private void loadFromDB()
        {
            //Initialize sectors and stocks
            _dvSectors = _pdalMngStocks.getSectors(ref _ddlSectorsName, ref _ddlSectorsValue);
            _dvScripts = _pdalMngStocks.Stocks(ref _ddlScriptName, ref _ddlScriptValue, (short)CommonDoubleM.StockType.AllStocks);
        }

        private void dgvLayout()
        {
            try
            {
                dgvScripts.Columns.Add("Stock Name", "Stock Name");
                dgvScripts.Columns.Add("Called", "Called");
                dgvScripts.Columns.Add("Y! Code", "Y! Code");
                dgvScripts.Columns.Add("Rediff Code", "Rediff Code");
                dgvScripts.Columns.Add("BSE Code", "BSE Code");
                dgvScripts.Columns.Add("Tr. Code", "Tr. Code");
                dgvScripts.Columns.Add("Hist. Date", "Hist. Date");
                dgvScripts.Columns.Add("SID", "SID");
                dgvScripts.Columns.Add("CATID", "CATID");
                dgvScripts.Columns.Add("IsActive", "IsActive");

                /* Data grid format display*/

                dgvScripts.Columns[0].Width = 180; //Name of Script
                dgvScripts.Columns[1].Width = 90; //Short Name of Script
                dgvScripts.Columns[2].Width = 80; //Yahoo Code
                dgvScripts.Columns[3].Width = 80; //Rediff Code
                dgvScripts.Columns[4].Width = 80; // BSE Code
                dgvScripts.Columns[5].Width = 80; // Trading Code

                dgvScripts.Columns[6].Width = 125;// Stock in Date
                dgvScripts.Columns[7].Width = 0; //Stock ID
                dgvScripts.Columns[8].Width = 0; //Sector ID
                dgvScripts.Columns[9].Width = 30; //Active or inAtive

                dgvScripts.RowHeadersWidth = 60;

                dgvScripts.Columns[7].Visible = false; // Stock ID
                dgvScripts.Columns[8].Visible = false; // Sector ID
                dgvScripts.Columns[9].Visible = false; // Active / Inactive

                dgvScripts.Columns[6].DefaultCellStyle.Format = CommonDoubleM.srtDate;
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }
        }
        private void databindingFormControls()
        {
            _theParent.lblDMMsg.Text = "Loading sectors..";
            Cursor = Cursors.WaitCursor;
            try
            {
                /*1st Control*/
                ddlSectors.DataSource = _dvSectors.ToTable();
                ddlSectors.DisplayMember = _ddlSectorsName;
                ddlSectors.ValueMember = _ddlSectorsValue;

                //Adding virtual row is bound dataview
                _dvSectors.Table.Rows.Add(new string[] { "-10", "All Sectors" });

                /*2nd Control*/
                //An alternative ways to load datasource to toolstripcombobox 
                ComboBox cb = (ComboBox)tscboSectors.Control;
                /*filling 1st fropdown with modified dataview*/
                cb.DataSource = _dvSectors;
                cb.DisplayMember = _ddlSectorsName;
                cb.ValueMember = _ddlSectorsValue;
                cb.Text = "All Sectors";

                /*3rd Control*/
                ddlStock.DataSource = _dvScripts;
                ddlStock.DisplayMember = _dvScripts.Table.Columns[2].ToString();
                ddlStock.ValueMember = "StockID";

                AutoFind();
            }
            catch (Exception ex)
            { _theParent.lblDMMsg.Text = ex.Message; }
            finally
            { Cursor = Cursors.Default; }
            
        }
        private void AutoFind()
        {
                //DataBinding Enabled
                /*4th Control*/
                ddlSName.DataSource = tsbtnAuto.Checked?_dvScripts:null;
                ddlSName.DisplayMember = _dvScripts.Table.Columns[3].ToString();
                ddlSName.ValueMember = "StockID";

                /*5th Control*/
                ddlYahoo.DataSource = tsbtnAuto.Checked ? _dvScripts : null;
                ddlYahoo.DisplayMember = _dvScripts.Table.Columns[4].ToString();
                ddlYahoo.ValueMember = "StockID";

                /*6th Control*/
                ddlRediff.DataSource = tsbtnAuto.Checked ? _dvScripts : null;
                ddlRediff.DisplayMember = _dvScripts.Table.Columns[5].ToString();
                ddlRediff.ValueMember = "StockID";

                /*7th Control*/
                ddlBSE.DataSource = tsbtnAuto.Checked ? _dvScripts : null;
                ddlBSE.DisplayMember = _dvScripts.Table.Columns[6].ToString();
                ddlBSE.ValueMember = "StockID";

                /*8th Control*/
                ddlBCode.DataSource = tsbtnAuto.Checked ? _dvScripts : null;
                ddlBCode.DisplayMember = _dvScripts.Table.Columns[7].ToString();
                ddlBCode.ValueMember = "StockID";
        }
        private void clearInputs()
        {
            //ddlSectors.SelectedValue = Convert.ToInt64(dgvScripts[8, e.RowIndex].Value);
            ddlSectors.SelectedIndex = 0;
            ddlStock.Text = "";

            if (tsbtnAuto.Checked)
            {
                ddlStock.SelectedIndex = -1;
                ddlSName.SelectedIndex = -1;
                ddlYahoo.SelectedIndex = -1;
                ddlRediff.SelectedIndex = -1;
                ddlBSE.SelectedIndex = -1;
                ddlBCode.SelectedIndex = -1;
            }
            else
            {
                ddlSName.Text = "";
                ddlYahoo.Text = "";
                ddlRediff.Text = "";
                ddlBSE.Text = "";
                ddlBCode.Text = "";
            }
            chkActive.Checked = true;

        }
        private void fillData()
        {
            string sCriteria = string.Empty;
            int result;
            DataTable dtAfterfilter;

            try
            {
                if (_sFilter == "All")
                    sCriteria = _scriptType;
                else
                    sCriteria = _scriptType + " AND [StockName] like '" + _sFilter + "%'";

                if (tscboSectors.SelectedIndex > -1)
                    if (int.TryParse(((ComboBox)tscboSectors.Control).SelectedValue.ToString(), out result))
                    {
                        int iSectorID = Convert.ToInt32(((ComboBox)tscboSectors.Control).SelectedValue);
                        if (iSectorID > 0)
                            sCriteria = sCriteria + " AND StockCatID = " + iSectorID.ToString();
                    }
                if (sCriteria == "") return;
                _dvScripts.RowFilter = sCriteria;
                dtAfterfilter = _dvScripts.ToTable();
                _dvScripts.RowFilter = "";

                int iPBValue = 0;
                _theParent.pBarDM.Visible = true;
                dgvScripts.ClearSelection();

                Cursor = Cursors.WaitCursor;
                    _theParent.pBarDM.Maximum = dtAfterfilter.Rows.Count;
                    blnBbaseRow = false;
                    dgvScripts.Rows.Clear();

                    foreach (DataRow dtRow in dtAfterfilter.Rows)
                    {
                        dgvScripts.Rows.Add();
                        dgvScripts[0, iPBValue].Value = dtRow[2].ToString(); // Stock Name
                        dgvScripts[1, iPBValue].Value = dtRow[3].ToString(); // Short Name
                        dgvScripts[2, iPBValue].Value = dtRow[4].ToString(); // Short Name
                        dgvScripts[3, iPBValue].Value = dtRow[5].ToString(); // Rediff Code
                        dgvScripts[4, iPBValue].Value = dtRow[6].ToString(); // BSE Code
                        dgvScripts[5, iPBValue].Value = dtRow[7].ToString(); // Trading/Bank Code
                        dgvScripts[6, iPBValue].Value = Convert.ToDateTime(dtRow[9]); // inDate

                        dgvScripts[7, iPBValue].Value = dtRow[0].ToString(); // SID
                        dgvScripts[8, iPBValue].Value = dtRow[1].ToString(); // CATID
                        dgvScripts[9, iPBValue].Value = dtRow[8].ToString(); // Trading/Bank Code

                        _theParent.pBarDM.Value = iPBValue;
                        iPBValue++;
                        //if (iPBValue == 5) break; //Just for testing only 2 rows
                    }
                    _theParent.lblDMMsg.Text = iPBValue.ToString() + " Script loaded.";
                }
                catch (Exception ex)
                {
                    _theParent.lblDMMsg.Text = ex.Message;
                    CommonDoubleM.LogDM(ex.Message);
                }
                finally
                {
                    _theParent.pBarDM.Visible = false;
                    Cursor = Cursors.Default;
                }
            }

        private void InsertStock()
        {
            if (DialogResult.Yes != MessageBox.Show("You ready to add new script [" + ddlStock.Text + "] in your portfolio", "New Script Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;
            try
            {
                _theParent.lblDMMsg.Text = "Adding new the script " + ddlStock.Text.Trim().ToUpper();

                int iResult = _pdalMngStocks.NewStock((int)ddlSectors.SelectedValue,
                    ddlStock.Text.Trim().ToUpper(), ddlSName.Text.Trim(),
                    ddlYahoo.Text.Trim().ToUpper(), ddlRediff.Text.Trim().ToUpper(),
                    ddlBSE.Text.Trim().ToUpper(), ddlBCode.Text.Trim().ToUpper(),
                    chkActive.Checked);
                if (iResult > 0)
                {
                    //updated dataview as dataview as been created earlier.
                    //Console.Write(_dvScripts.Count);
                    DataRow drStocks = _dvScripts.Table.NewRow();
                    drStocks[0] = iResult;
                    drStocks[1] = (int)ddlSectors.SelectedValue;
                    drStocks[2] = ddlStock.Text.Trim().ToUpper();
                    drStocks[3] = ddlSName.Text.Trim();
                    drStocks[4] = ddlYahoo.Text.Trim().ToUpper();
                    drStocks[5] = ddlRediff.Text.Trim().ToUpper();
                    drStocks[6] = ddlBSE.Text.Trim().ToUpper();
                    drStocks[7] = ddlBCode.Text.Trim().ToUpper();
                    drStocks[8] = chkActive.Checked;
                    _dvScripts.Table.Rows.Add(drStocks);

                    dgvScripts.Rows.Add();
                    dgvScripts[0, dgvScripts.RowCount - 2].Value = drStocks[2]; // Stock Name
                    dgvScripts[1, dgvScripts.RowCount - 2].Value = drStocks[3]; // Short Name
                    dgvScripts[2, dgvScripts.RowCount - 2].Value = drStocks[4]; // Yahoo Code
                    dgvScripts[3, dgvScripts.RowCount - 2].Value = drStocks[5]; // Rediff Code
                    dgvScripts[4, dgvScripts.RowCount - 2].Value = drStocks[6]; // BSE Code
                    dgvScripts[5, dgvScripts.RowCount - 2].Value = drStocks[7]; // Trading/Bank Code
                    dgvScripts[6, dgvScripts.RowCount - 2].Value = DateTime.Now.ToString(CommonDoubleM.srtDate); // inDate

                    dgvScripts[7, dgvScripts.RowCount - 2].Value = drStocks[0]; // SID
                    dgvScripts[8, dgvScripts.RowCount - 2].Value = drStocks[1]; // CATID

                    dgvScripts.ClearSelection();
                    //Auto Scroll to Row new row and select it
                    dgvScripts.Rows[dgvScripts.RowCount - 2].Selected = true;
                    if (!dgvScripts.Rows[dgvScripts.RowCount-2].Displayed)
                        dgvScripts.FirstDisplayedScrollingRowIndex = dgvScripts.RowCount - 2;

                    clearInputs();
                }
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
            }
            finally
            {
                
            }
        }
        private void UpdateStock()
        {
            if (DialogResult.Yes != MessageBox.Show("You updating script [" + _ScriptName + "]", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            //Update Script: update the details 
            _theParent.lblDMMsg.Text = "Updating the script details..";
            try
            {
                int iResult = _pdalMngStocks.UpdateStock(_ScriptID,
                    (int)ddlSectors.SelectedValue, ddlStock.Text.Trim().ToUpper(),
                    ddlSName.Text.Trim(),ddlYahoo.Text.Trim().ToUpper(),
                    ddlRediff.Text.Trim().ToUpper(), ddlBSE.Text.Trim().ToUpper(),
                    ddlBCode.Text.Trim().ToUpper(), chkActive.Checked);
                if (iResult == 1)
                {
                    tsbtnUpdate.ToolTipText = "Update stocks details";
                    //updated dataview as dataview as been created earlier.
                    DataRow[] drStocks = _dvScripts.Table.Select("StockID =" + _ScriptID);
                    if (drStocks.Length == 1)
                    {
                        drStocks[0].BeginEdit();
                        drStocks[0][1] = (int)ddlSectors.SelectedValue;
                        drStocks[0][2] = ddlStock.Text.Trim().ToUpper();
                        drStocks[0][3] = ddlSName.Text.Trim();
                        drStocks[0][4] = ddlYahoo.Text.Trim().ToUpper();
                        drStocks[0][5] = ddlRediff.Text.Trim().ToUpper();
                        drStocks[0][6] = ddlBSE.Text.Trim().ToUpper();
                        drStocks[0][7] = ddlBCode.Text.Trim().ToUpper();
                        drStocks[0][8] = chkActive.Checked;
                        drStocks[0].EndEdit();
                    }
                    //fillGrid();
                    dgvScripts.ClearSelection();
                    foreach (DataGridViewRow dr in dgvScripts.Rows)
                    {
                        if (dr.Cells[7].Value.ToString() == _ScriptID.ToString())
                        {
                            dr.Cells[8].Value = ddlSectors.SelectedValue;
                            dr.Cells[0].Value = ddlStock.Text.Trim().ToUpper();
                            dr.Cells[1].Value = ddlSName.Text.Trim();
                            dr.Cells[2].Value = ddlYahoo.Text.Trim().ToUpper();
                            dr.Cells[3].Value = ddlRediff.Text.Trim().ToUpper();
                            dr.Cells[4].Value = ddlBSE.Text.Trim().ToUpper();
                            dr.Cells[5].Value = ddlBCode.Text.Trim().ToUpper();
                            dr.Cells[9].Value = chkActive.Checked;

                            dr.Selected = true;
                            break;
                        }
                    }
                    clearInputs();
                    _theParent.lblDMMsg.Text = _ScriptName + " has been updated successfully.";
                }
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
            }
            finally
            {
                _ScriptID = -1;
                _ScriptName = "";
                tsbtnUpdate.Checked = false;
            }
        }
        private void DeleteStock()
        {
            if (DialogResult.Yes != MessageBox.Show("You are going to delete script [" + _ScriptName + "]", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            try
            {
                if (_pdalMngStocks.getRates_Count(_ScriptID) != 0)
                {
                    MessageBox.Show("You are not able to delete script [" + _ScriptName + "]\nAll associated Tradings & Rates should be deleted before.", "Delete Confirmation",MessageBoxButtons.OK,  MessageBoxIcon.Stop);
                    return;
                }
            int iResult = _pdalMngStocks.DeleteStock(_ScriptID);

            //Update Script: update the details 
            _theParent.lblDMMsg.Text = "Deleting stock";

            if (iResult == 1)
            {
                tsbtnUpdate.ToolTipText = "Update stocks details";
                //updated dataview as dataview as been created earlier.
                DataRow[] drStocks = _dvScripts.Table.Select("StockID =" + _ScriptID);

                if (drStocks.Length == 1)
                {
                    drStocks[0].Delete();
                }
                //fillGrid();
                dgvScripts.ClearSelection();
                foreach (DataGridViewRow dr in dgvScripts.Rows)
                {
                    if (dr.Cells[7].Value.ToString() == _ScriptID.ToString())
                    {
                        dr.Visible = false;
                        break;
                    }
                }
                clearInputs();
                _theParent.lblDMMsg.Text = _ScriptName + " has been deleted successfully.";
            }
            }
            catch (Exception ex)
            {
                _theParent.lblDMMsg.Text = ex.Message;
            }
            finally
            {
                _ScriptID = -1;
                _ScriptName = "";
                tsbtnUpdate.Checked = false;
            }

            
        }
        private void llblBSE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ddlStock.Text.Trim() != "")
                System.Diagnostics.Process.Start("IExplore", " http://www.bseindia.com/scripsearch/scrips.aspx?myScrip=" + ddlStock.Text.Trim());
            else
            {
                MessageBox.Show("Online auto seaech for BSE code needs Stock Name");
                ddlStock.Focus();
            }
        }
        private void llblRCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (ddlStock.Text.Trim() != "")
                System.Diagnostics.Process.Start("IExplore", " http://money.rediff.com/companies/" + ddlStock.Text.Trim());
            else
            {
                MessageBox.Show("Online auto seaech for Rediff code needs Stock Name");
                ddlStock.Focus();
            }
        }
        private void llblYF_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ddlStock.Text.Trim() != "")
                System.Diagnostics.Process.Start("IExplore", " http://finance.yahoo.com/lookup?s=" + ddlStock.Text.Trim());
            else
            {
                MessageBox.Show("Online auto seaech for Yahoo code needs Stock Name");
                ddlStock.Focus();
            }
        }
#endregion



    }
}
