using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewDBTestApp
{
    public partial class Form4 : Form
    {
        SortOrder[] _soHeaderGlyph;
        DataTable dt = new DataTable();
        DataView dv;
        string _sMultiSort="";
        
        bool blnOnce = false;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
                

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string sFinalSort, sCurCol;
            //dv = dt.DefaultView;

            bool keyCtrlHold = ((ModifierKeys & Keys.Control) == Keys.Control);

            if (keyCtrlHold) //Mouse click without Ctrl pressed
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    sCurCol = dataGridView1.Columns[i].Name;
                    if (e.ColumnIndex == i) //Check which column click holding ctrl key
                    {
                        //Store new value
                        _soHeaderGlyph[i] = dataGridView1.Columns[i].HeaderCell.SortGlyphDirection;
                        //Looking for new value only
                        //keep Priority for Header Sorting
                        if (_sMultiSort.Length==0 || _sMultiSort.IndexOf(sCurCol) < 0)
                        {
                            _sMultiSort = _sMultiSort + sCurCol + " " + (dataGridView1.Columns[i].HeaderCell.SortGlyphDirection == SortOrder.Ascending ? "ASC," : "DESC,");
                        }
                        else
                        {
                            string [] sHeaderCell = _sMultiSort.Split(' ', ',');
                            for (int j = 0; j < sHeaderCell.Length; j++)
                            {
                                if (sHeaderCell[j] == sCurCol)
                                {
                                    // Swaping current sorting order
                                    sHeaderCell[j + 1] = sHeaderCell[j + 1] == "DESC" ? "ASC" : "DESC";
                                    dataGridView1.Columns[i].HeaderCell.SortGlyphDirection = (sHeaderCell[j + 1] == "DESC" ? SortOrder.Descending : SortOrder.Ascending);
                                    _soHeaderGlyph[i] = dataGridView1.Columns[i].HeaderCell.SortGlyphDirection;
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
                        dataGridView1.Columns[i].HeaderCell.SortGlyphDirection = _soHeaderGlyph[i];
                    }
                }
            }
            else //Mouse click without Ctrl press
            {
                _sMultiSort="";
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (e.ColumnIndex == i) //Free click on header
                    {
                        //Store header click
                        _soHeaderGlyph[i] = dataGridView1.Columns[i].HeaderCell.SortGlyphDirection;
                        _sMultiSort = _sMultiSort + dataGridView1.Columns[i].Name + " " + (dataGridView1.Columns[i].HeaderCell.SortGlyphDirection == SortOrder.Ascending ? "ASC," : "DESC,");
                    }
                    else
                    {
                        //Reset glyph with no direction
                        _soHeaderGlyph[i] = 0;
                        dataGridView1.Columns[i].HeaderCell.SortGlyphDirection = _soHeaderGlyph[i];
                    }
                }
            }

            sFinalSort = _sMultiSort.Remove(_sMultiSort.Length - 1);
            dv.Sort = sFinalSort;
            dataGridView1.DataSource = dv;
            label1.Text = sFinalSort;

            //Restoring Glyph on datagrid header
            string []sHeaderCell1 = _sMultiSort.Split(' ', ',');
            for (int j = 0; j < sHeaderCell1.Length - 1; j = j + 2) 
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (sHeaderCell1[j] == dataGridView1.Columns[i].Name)
                    {
                        dataGridView1.Columns[i].HeaderCell.SortGlyphDirection = (sHeaderCell1[j + 1] == "DESC" ? SortOrder.Descending : SortOrder.Ascending);
                        _soHeaderGlyph[i] = dataGridView1.Columns[i].HeaderCell.SortGlyphDirection;
                        
                        break;
                    }
                }
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            // Add rows of data to the DataGridView.

            if (!blnOnce)
            {
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("DOB", typeof(DateTime));
                dt.Columns.Add("PL", typeof(double));
                blnOnce = true;
            }
            dt.Rows.Add(new string[] { "6", "Deepak", "Portland", "1997, 8, 17","-6.6" });
            dt.Rows.Add(new string[] { "3", "Saurav", "Anup", "2009, 8, 17","3.8" });
            dt.Rows.Add(new string[] { "10", "Supam", "Swiss", "1997, 8, 17","-10.5" });
            dt.Rows.Add(new string[] { "9", "Jitendra", "Russia", "2008, 8, 17","9.8" });
            dt.Rows.Add(new string[] { "10", "Ive", "Belgium", "1997, 8, 17","-10.5" });
            dt.Rows.Add(new string[] { "0", "Rupesh", "Russia", "1997, 8, 17","0" });
            dt.Rows.Add(new string[] { "3", "Steev", "Poland", "1997, 8, 17","3.8" });
            dt.Rows.Add(new string[] { "0", "Ravi", "USA", "1997, 8, 17","0" });
            dt.Rows.Add(new string[] { "1", "Jignesh", "USA", "1997, 8, 17","-1.5" });
            dt.Rows.Add(new string[] { "15", "Stain", "Belgium", "1980, 8, 17","15" });
            dt.Rows.Add(new string[] { "15", "Stain", "Poland", "1997, 8, 17","15" });
            dt.Rows.Add(new string[] { "1", "Kristina", "USA", "1997, 8, 17","1" });
            dv = dt.DefaultView;
            dataGridView1.DataSource = dv;
            dataGridView1.AutoResizeColumns();
            _soHeaderGlyph = new SortOrder[dt.Columns.Count];
            

            }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].HeaderCell.Value = e.RowIndex + 1;
            //dataGridView1[3, e.RowIndex].Style.BackColor = Color.FromArgb(200, 100, 50);

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridView1[4, e.RowIndex].Style.BackColor = Color.FromArgb(20, 100, 50);
        }
        }
    }


/*
          private void dgv_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

Or override OnColumnAdded function:

     protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
*/