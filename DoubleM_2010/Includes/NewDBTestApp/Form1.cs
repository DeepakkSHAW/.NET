using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewDBTestApp
{
    public partial class Form1 : Form
    {
        // The ListView Sorter
        private ListViewItemComparer _lvwItemComparer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stockDataSet.QryCatStocks' table. You can move, or remove it, as needed.
            this.qryCatStocksTableAdapter.Fill(this.stockDataSet.QryCatStocks);

            /*ListViewGroup group1 = new ListViewGroup("Group 01--------- test");

            ListViewGroup group2 = new ListViewGroup("Group 02");
            listView1.Groups.AddRange(new ListViewGroup[] { group1, group2 });*/


            /*Adding dataset in Listview*/
            listView1.Columns.Add("Name", 180, HorizontalAlignment.Left);
            listView1.Columns.Add("Category", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("SName", 60, HorizontalAlignment.Right);
            listView1.Columns.Add("Active", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Date", 100, HorizontalAlignment.Left);
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
            listView1.Sorting = SortOrder.Ascending;
            
            _lvwItemComparer = new ListViewItemComparer();
            //this.listView1.ListViewItemSorter = _lvwItemComparer;

            DataTable dt = stockDataSet.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow drow = dt.Rows[i];

                // Only row that have not been deleted
                if (drow.RowState != DataRowState.Deleted)
                {
                    // Define the list items
                    //ListViewItem lvi = new ListViewItem(drow[2].ToString(), i % 2 == 0 ? group1 : group2);
                    ListViewItem lvi = new ListViewItem(drow[2].ToString());
                    lvi.SubItems.Add(drow[3].ToString());
                    lvi.SubItems.Add(drow[4].ToString());
                    lvi.SubItems.Add(drow[3].ToString());
                    lvi.SubItems.Add(listView1.Items.Count.ToString());
                    //lvi.BackColor = (listView1.Items.Count % 2 == 0 ? Color.LightYellow : Color.LightBlue);

                    // Add the list items to the ListView
                    listView1.Items.Add(lvi);
                }

            }

        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            dataGridView1.VirtualMode = false;
        }
    }

    // This class is an implementation of the 'IComparer' interface.
    public class ListViewItemComparer : IComparer
    {
        // Specifies the column to be sorted
        private int ColumnToSort;

        // Specifies the order in which to sort (i.e. 'Ascending').
        private SortOrder OrderOfSort;

        // Case insensitive comparer object
        private CaseInsensitiveComparer ObjectCompare;

        // Class constructor, initializes various elements
        public ListViewItemComparer()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        // This method is inherited from the IComparer interface.
        // 
        // x: First object to be compared
        // y: Second object to be compared
        //
        // The result of the comparison. "0" if equal, 
        // negative if 'x' is less than 'y' and 
        // positive if 'x' is greater than 'y'
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Determine the type being compared
            try
            {
                compareResult = CompareDateTime(listviewX, listviewY);
            }
            catch
            {
                try
                {
                    compareResult = CompareDecimal(listviewX, listviewY);
                }
                catch
                {
                    compareResult = CompareString(listviewX, listviewY);
                }
            }

            // Simple String Compare
            // compareResult = String.Compare (
            // 	listviewX.SubItems[ColumnToSort].Text,
            // 	listviewY.SubItems[ColumnToSort].Text
            // );

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        public int CompareDateTime(ListViewItem listviewX, ListViewItem listviewY)
        {
            // Parse the two objects passed as a parameter as a DateTime.
            System.DateTime firstDate =
                DateTime.Parse(listviewX.SubItems[ColumnToSort].Text);
            System.DateTime secondDate =
                DateTime.Parse(listviewY.SubItems[ColumnToSort].Text);

            // Compare the two dates.
            int compareResult = DateTime.Compare(firstDate, secondDate);
            return compareResult;
        }

        public int CompareDecimal(ListViewItem listviewX, ListViewItem listviewY)
        {
            // Parse the two objects passed as a parameter as a DateTime.
            System.Decimal firstValue =
                Decimal.Parse(listviewX.SubItems[ColumnToSort].Text);
            System.Decimal secondValue =
                Decimal.Parse(listviewY.SubItems[ColumnToSort].Text);

            // Compare the two dates.
            int compareResult = Decimal.Compare(firstValue, secondValue);
            return compareResult;
        }


        public int CompareString(ListViewItem listviewX, ListViewItem listviewY)
        {
            // Case Insensitive Compare
            int compareResult = ObjectCompare.Compare(
                listviewX.SubItems[ColumnToSort].Text,
                listviewY.SubItems[ColumnToSort].Text
            );
            return compareResult;
        }

        // Gets or sets the number of the column to which to
        // apply the sorting operation (Defaults to '0').
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        // Gets or sets the order of sorting to apply
        // (for example, 'Ascending' or 'Descending').
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }

}
