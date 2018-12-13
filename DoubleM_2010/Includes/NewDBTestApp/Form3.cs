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
    public partial class Form3 : Form
    {
        DataGridComparer _columnSorter;
        DataTable dt = new DataTable();
        DataView dv;
        string _sMultiSort;
        bool blnOnce = false;
        SortOrder [] _soHeaderGlyph;
        //List<Person> programmers;

        /*public DataGridComparer SortedDataGridView()
        {
            _columnSorter = new DataGridComparer(this);
        }*/
        public Form3()
        {
            InitializeComponent();
            _columnSorter = new DataGridComparer(dataGridView1);

            /*InitialiseProgrammers();
            PopulateGrid();*/
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            // Add columns to the DataGridView.

            dataGridView1.ColumnCount = 3;
            _soHeaderGlyph = new SortOrder[dataGridView1.ColumnCount];
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "City";
            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["Name"].HeaderText = "Name";
            dataGridView1.Columns["City"].HeaderText = "City";

            // Add rows of data to the DataGridView.

            dataGridView1.Rows.Add(new string[] { "1", "Parker", "Seattle" });
            dataGridView1.Rows.Add(new string[] { "-3", "Parker", "New York" });
            dataGridView1.Rows.Add(new string[] { "3", "Watson", "Seattle" });
            dataGridView1.Rows.Add(new string[] { "4.9", "Jameson", "New Jersey" });
            dataGridView1.Rows.Add(new string[] { "5.5", "Brock", "New York" });
            dataGridView1.Rows.Add(new string[] { "-6.5", "Conner", "Portland" });

            dataGridView1.Rows.Add(new string[] { "5", "Parker", "Seattle" });
            dataGridView1.Rows.Add(new string[] { "-2", "Parker", "New York" });
            dataGridView1.Rows.Add(new string[] { "3", "Watson", "Seattle" });
            dataGridView1.Rows.Add(new string[] { "4", "Jameson", "New Jersey" });
            dataGridView1.Rows.Add(new string[] { "-6.6", "Brock", "New York" });
            dataGridView1.Rows.Add(new string[] { "6", "Aban", "Portland" });

            dataGridView1.Rows.Add(new string[] { "5", "Parker", "Seattle" });
            dataGridView1.Rows.Add(new string[] { "-2", "Parker", "New York" });
            dataGridView1.Rows.Add(new string[] { "3", "Watson", "Seattle" });
            dataGridView1.Rows.Add(new string[] { "-4.44", "Jameson", "New Jersey" });
            dataGridView1.Rows.Add(new string[] { "-6.6", "Brock", "New York" });
            dataGridView1.Rows.Add(new string[] { "-6.6", "Aban", "Portland" });
            // Autosize the columns.

            //dataGridView1.AutoResizeColumns();
            
        }


        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!blnOnce)
            {
                dt.Columns.Add("C1", typeof(double));
                dt.Columns.Add("C2", typeof(string));
                dt.Columns.Add("C3", typeof(string));

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i][0] = Convert.ToDouble(dataGridView1[0, i].Value);
                    dt.Rows[i][1] = dataGridView1[1, i].Value;
                    dt.Rows[i][2] = dataGridView1[2, i].Value;
                }
                blnOnce = true;
                dataGridView2.DataSource = dt;
            }
            /*foreach(DataRow dr in dataGridView1.Rows)
            {
                dt.Columns["C1"].va
            }*/

            bool keepSamePriority = ((ModifierKeys & Keys.Control) == Keys.Control);
            //MessageBox.Show(keepSamePriority.ToString());
            if (keepSamePriority) //Mouse click without Ctrl press
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++) 
                {
                    if (e.ColumnIndex == i) //Check which column click holding ctrl key
                        //Store new value
                        _soHeaderGlyph[i] = dataGridView1.Columns[i].HeaderCell.SortGlyphDirection;
                    else
                        //Set glyph with old values
                        dataGridView1.Columns[i].HeaderCell.SortGlyphDirection = _soHeaderGlyph[i];
                }
            }
            else //Mouse click without Ctrl press
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (e.ColumnIndex == i) //Free click on header
                        //Store header click
                        _soHeaderGlyph[i] = dataGridView1.Columns[i].HeaderCell.SortGlyphDirection;
                    else
                    {
                        //Reset glyph with no direction
                        _soHeaderGlyph[i] = 0;
                        dataGridView1.Columns[i].HeaderCell.SortGlyphDirection = (SortOrder)_soHeaderGlyph[i];
                    }
                }
            }
                dv = dt.DefaultView;
                _sMultiSort = "";
                for(int i=0;i<dataGridView1.Columns.Count;i++)
                {
                    if (dataGridView1.Columns[i].HeaderCell.SortGlyphDirection != SortOrder.None)
                        _sMultiSort = _sMultiSort + dataGridView2.Columns[i].Name + " " + (dataGridView1.Columns[i].HeaderCell.SortGlyphDirection == SortOrder.Ascending ? "ASC," : "DESC,");
                        
                }
                _sMultiSort = _sMultiSort.Remove(_sMultiSort.Length - 1);
                dv.Sort = _sMultiSort;
                label1.Text = _sMultiSort;
                dataGridView2.DataSource = dv;
            

            /*_columnSorter.SetSortColumn(e.ColumnIndex, ModifierKeys);

             dataGridView1.Sort(_columnSorter);
            
            //Sort(_columnSorter);

            //Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;

            //base.OnColumnHeaderMouseClick(e);
            label1.Text = _columnSorter.SortOrderDescription.ToString();*/
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            double i, j;

            if (double.TryParse(e.CellValue1.ToString(), out i) == false) return;
            if (double.TryParse(e.CellValue2.ToString(), out j) == false) return;

            if (i == j)
            {
                e.SortResult = 0;
            }
            else if (j > i)
            {
                e.SortResult = -1;
            }
            else
                e.SortResult = 1;

            e.Handled = true;

        }
    }
        /*
        //////////////////////
        private void PopulateGrid()
        {
            foreach (Person programmer in programmers)
            {
                int rowNum = dataGridView1.Rows.Add(new object[] { programmer.ID,
					programmer.FirstName,programmer.LastName, programmer.Date,
					programmer.Region,
					programmer.Level, programmer.Level.ToString()});
                dataGridView1.Rows[rowNum].Tag = programmer;
                
            }
        }

        private void InitialiseProgrammers()
        {
            programmers = new List<Person>();

            AddPerson(1924, "Dennis", "Ritchie", new DateTime(1971, 11, 3), OriginRegion.US, Level.Guru);
            AddPerson(1025, "Dennis", "Menace", new DateTime(1938, 7, 26), OriginRegion.EU, Level.Beginner);
            AddPerson(2335, "Bjarne", "Stroustrup", new DateTime(1997, 11, 14), OriginRegion.EU, Level.Guru);
            AddPerson(3378, "Don", "Box", new DateTime(1997, 8, 7), OriginRegion.US, Level.Guru);
            AddPerson(7045, "Paul", "Anderson", new DateTime(2007, 3, 16), OriginRegion.EU, Level.Intermediate);
            AddPerson(8972, "Kevin", "Hoover", new DateTime(2002, 4, 23), OriginRegion.EU, Level.Intermediate);
            AddPerson(6241, "Melinda", "Box", new DateTime(1979, 12, 20), OriginRegion.EU, Level.Advanced);
            AddPerson(1234, "David", "Ritchie", new DateTime(1983, 9, 26), OriginRegion.US, Level.Advanced);
            AddPerson(4321, "Bjarne", "Menace", new DateTime(1997, 8, 7), OriginRegion.EU, Level.Advanced);
            AddPerson(5801, "Don", "Menace", new DateTime(1997, 8, 7), OriginRegion.EU, Level.Beginner);
            AddPerson(7801, "Jonquil", "Menace", new DateTime(1997, 8, 17), OriginRegion.EU, Level.Beginner);
            AddPerson(5801, "Dennis", "Dodger", new DateTime(1997, 8, 27), OriginRegion.EU, Level.Beginner);
            AddPerson(7802, "Nick", "Menace", new DateTime(1997, 8, 7), OriginRegion.EU, Level.Advanced);
            AddPerson(5423, "Bud", "Box", new DateTime(1997, 8, 7), OriginRegion.US, Level.Beginner);
            AddPerson(8423, "Brian", "Hoover", new DateTime(1987, 8, 17), OriginRegion.US, Level.Intermediate);
        }

        private void AddPerson(int id, string firstName, string lastName, DateTime date, OriginRegion region, Level level)
        {
            programmers.Add(new Person(id, firstName, lastName, date, region, level));
        }

        //////////////////////
    }

    class Person
    {
        public int ID;
        public string FirstName;
        public string LastName;
        public DateTime Date;
        public OriginRegion Region;
        public Level Level;

        public Person(int id, string firstName, string lastName, DateTime date, OriginRegion region, Level level)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Date = date;
            Region = region;
            Level = level;
        }
    }
    public enum Level
    {
        Beginner,
        Intermediate,
        Advanced,
        Guru,
    }

    public enum OriginRegion
    {
        EU,
        US,
    }*/
    
}
