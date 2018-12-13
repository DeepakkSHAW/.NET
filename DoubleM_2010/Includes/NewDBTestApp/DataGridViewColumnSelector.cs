using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace NewDBTestApp
{
    class DataGridViewColumnSelector
    {
        SortOrder[] _soHeaderGlyph;
        DataTable dt;
        DataView dv;
        string _sMultiSort;

        // the DataGridView to which the DataGridViewColumnSelector is attached
        private DataGridView mDataGridView = null;
        // a CheckedListBox containing the column header text and checkboxes
        private CheckedListBox mCheckedListBox;
        // a ToolStripDropDown object used to show the popup
        private ToolStripDropDown mPopup;

        //Get information of all hidden columns of DataGridView
        private string[] HiddenColumnName;

        /// <summary>
        /// The max height of the popup
        /// </summary>
        public int MaxHeight = 300;
        /// <summary>
        /// The width of the popup
        /// </summary>
        public int Width = 200;

        /// <summary>
        /// Gets or sets the DataGridView to which the DataGridViewColumnSelector is attached
        /// </summary>
        public DataGridView DataGridView
        {
            get { return mDataGridView; }
            set
            {
                // If any, remove handler from current DataGridView 
                if (mDataGridView != null)
                {
                    mDataGridView.CellMouseClick -= new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);
                    mDataGridView.ColumnHeaderMouseClick -= new DataGridViewCellMouseEventHandler(mDataGridView_ColumnHeaderMouseClick);


                }
                // Set the new DataGridView
                mDataGridView = value;
                // Attach CellMouseClick handler to DataGridView
                if (mDataGridView != null)
                {
                    mDataGridView.CellMouseClick += new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);
                    mDataGridView.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(mDataGridView_ColumnHeaderMouseClick);
                }


            }
        }

        //void mDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    this.DataGridView.Columns[0].HeaderCell.SortGlyphDirection = SortOrder.Descending;
        //}
        void mDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string sFinalSort, sCurCol;
            //dv = dt.DefaultView;

            bool keyCtrlHold = ((Keys.Modifiers & Keys.Control) == Keys.Control);

            if (keyCtrlHold) //Mouse click without Ctrl pressed
            {
                for (int i = 0; i < mDataGridView.Columns.Count; i++)
                {
                    sCurCol = mDataGridView.Columns[i].Name;
                    if (e.ColumnIndex == i) //Check which column click holding ctrl key
                    {
                        //Store new value
                        _soHeaderGlyph[i] = mDataGridView.Columns[i].HeaderCell.SortGlyphDirection;
                        //Looking for new value only
                        //keep Priority for Header Sorting
                        if (_sMultiSort.Length == 0 || _sMultiSort.IndexOf(sCurCol) < 0)
                        {
                            _sMultiSort = _sMultiSort + sCurCol + " " + (mDataGridView.Columns[i].HeaderCell.SortGlyphDirection == SortOrder.Ascending ? "ASC," : "DESC,");
                        }
                        else
                        {
                            string[] sHeaderCell = _sMultiSort.Split(' ', ',');
                            for (int j = 0; j < sHeaderCell.Length; j++)
                            {
                                if (sHeaderCell[j] == sCurCol)
                                {
                                    // Swaping current sorting order
                                    sHeaderCell[j + 1] = sHeaderCell[j + 1] == "DESC" ? "ASC" : "DESC";
                                    mDataGridView.Columns[i].HeaderCell.SortGlyphDirection = (sHeaderCell[j + 1] == "DESC" ? SortOrder.Descending : SortOrder.Ascending);
                                    _soHeaderGlyph[i] = mDataGridView.Columns[i].HeaderCell.SortGlyphDirection;
                                    break;
                                }

                            }
                            _sMultiSort = "";
                            for (int k = 0; k < sHeaderCell.Length - 1; k = k + 2)
                            {
                                _sMultiSort = _sMultiSort + sHeaderCell[k] + " " + sHeaderCell[k + 1] + ",";
                            }
                        }

                    }
                    else
                    {
                        //Set glyph with old values
                        mDataGridView.Columns[i].HeaderCell.SortGlyphDirection = _soHeaderGlyph[i];
                    }
                }
            }
            else //Mouse click without Ctrl press
            {
                _sMultiSort = "";
                for (int i = 0; i < mDataGridView.Columns.Count; i++)
                {
                    if (e.ColumnIndex == i) //Free click on header
                    {
                        //Store header click
                        _soHeaderGlyph[i] = mDataGridView.Columns[i].HeaderCell.SortGlyphDirection;
                        _sMultiSort = _sMultiSort + mDataGridView.Columns[i].Name + " " + (mDataGridView.Columns[i].HeaderCell.SortGlyphDirection == SortOrder.Ascending ? "ASC," : "DESC,");
                    }
                    else
                    {
                        //Reset glyph with no direction
                        _soHeaderGlyph[i] = 0;
                        mDataGridView.Columns[i].HeaderCell.SortGlyphDirection = _soHeaderGlyph[i];
                    }
                }
            }

            sFinalSort = _sMultiSort.Remove(_sMultiSort.Length - 1);
            dv.Sort = sFinalSort;
            mDataGridView.DataSource = null;
            mDataGridView.DataSource = dv;
            //label1.Text = sFinalSort;

            //Restoring Glyph on datagrid header
            string[] sHeaderCell1 = _sMultiSort.Split(' ', ',');
            for (int j = 0; j < sHeaderCell1.Length - 1; j = j + 2)
            {
                for (int i = 0; i < mDataGridView.Columns.Count; i++)
                {
                    if (sHeaderCell1[j] == mDataGridView.Columns[i].Name)
                    {
                        mDataGridView.Columns[i].HeaderCell.SortGlyphDirection = (sHeaderCell1[j + 1] == "DESC" ? SortOrder.Descending : SortOrder.Ascending);
                        _soHeaderGlyph[i] = mDataGridView.Columns[i].HeaderCell.SortGlyphDirection;

                        break;
                    }
                }
            }
        }

        // When user right-clicks the cell origin, it clears and fill the CheckedListBox with
        // columns header text. Then it shows the popup. 
        // In this way the CheckedListBox items are always refreshed to reflect changes occurred in 
        // DataGridView columns (column additions or name changes and so on).
        void mDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right && e.RowIndex == -1 && e.ColumnIndex == -1)
            {
                mCheckedListBox.Items.Clear();
                foreach (DataGridViewColumn c in mDataGridView.Columns)
                {
                    if (Array.IndexOf(HiddenColumnName, c.HeaderText) < 0)
                        mCheckedListBox.Items.Add(c.HeaderText, c.Visible);
                    else
                        mCheckedListBox.Items.Add(c.HeaderText, CheckState.Indeterminate);
                }
                int PreferredHeight = (mCheckedListBox.Items.Count * 16) + 7;
                mCheckedListBox.Height = (PreferredHeight < MaxHeight) ? PreferredHeight : MaxHeight;
                mCheckedListBox.Width = this.Width;
                mPopup.Show(mDataGridView.PointToScreen(new Point(e.X, e.Y)));
            }
        }

        // The constructor creates an instance of CheckedListBox and ToolStripDropDown.
        // the CheckedListBox is hosted by ToolStripControlHost, which in turn is
        // added to ToolStripDropDown.
        public DataGridViewColumnSelector()
        {
            mCheckedListBox = new CheckedListBox();
            mCheckedListBox.CheckOnClick = true;
            mCheckedListBox.ItemCheck += new ItemCheckEventHandler(mCheckedListBox_ItemCheck);

            ToolStripControlHost mControlHost = new ToolStripControlHost(mCheckedListBox);
            mControlHost.Padding = Padding.Empty;
            mControlHost.Margin = Padding.Empty;
            mControlHost.AutoSize = false;

            mPopup = new ToolStripDropDown();
            mPopup.Padding = Padding.Empty;
            mPopup.Items.Add(mControlHost);
        }

        public DataGridViewColumnSelector(DataGridView dgv)
            : this()
        {
            int i = 0;
            this.DataGridView = dgv;
            HiddenColumnName = new string[dgv.ColumnCount];
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                if (c.Visible == false)
                    HiddenColumnName[i] = c.HeaderText;
                i++;
            }

            dt = new DataTable();
            _sMultiSort = "";
           dv = new DataView();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("DOB", typeof(DateTime));
            dt.Columns.Add("PL", typeof(double));
            _soHeaderGlyph = new SortOrder[dt.Columns.Count];
            i = 0;
            for (i = 0; i < dgv.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = Convert.ToInt32(dgv[0, i].Value);
                dt.Rows[i][1] = dgv[1, i].Value;
                dt.Rows[i][2] = dgv[2, i].Value;
                dt.Rows[i][3] = Convert.ToDateTime( dgv[3, i].Value);
                dt.Rows[i][4] = Convert.ToDouble(dgv[4, i].Value);
            }
            dv = dt.DefaultView;
            //mDataGridView.DataSource = dv;
        }

        // When user checks / unchecks a checkbox, the related column visibility is 
        // switched.
        void mCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Indeterminate)
                e.NewValue = e.CurrentValue; // no change if CheckState is Indeterminate
            else
                mDataGridView.Columns[e.Index].Visible = (e.NewValue == CheckState.Checked);
        }
    }
}
