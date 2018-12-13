using System;

using System.IO;
using System.Collections;


using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
//http://www.codeproject.com/KB/list/ObjectListView.aspx?fid=350107&df=90&mpp=25&noise=3&sort=Position&view=Quick&select=2842953&fr=1
//http://objectlistview.sourceforge.net/html/whatsnew.htm

namespace NewDBTestApp
{
    public partial class Form6 : Form
    {
        bool blnOnce = false;
        DataTable dt = new DataTable();
        DataView dv;

        public Form6()
        {
            InitializeComponent();
            InitializeTreeListExample();
            load_data();
            //InitializeTreeListArray();
        }

        void InitializeTreeListExample()
        {
            //TreeListViewItem it = new TreeListViewItem("Deepak");
            //it.SubItems.Add("Rupam");
            //treeListView2.Items.Add(it);

            this.treeListView.CanExpandGetter = delegate(object x)
            {
                return (x is DirectoryInfo);
                
            };
            this.treeListView.ChildrenGetter = delegate(object x)
            {
                DirectoryInfo dir = (DirectoryInfo)x;
                 //dir.GetFileSystemInfos().
                return new ArrayList(dir.GetFileSystemInfos());
            };

            this.treeListView.CheckBoxes = true;

            // You can change the way the connection lines are drawn by changing the pen
            //((TreeListView.TreeRenderer)this.treeListView.TreeColumnRenderer).LinePen = Pens.Firebrick;
           // ((TreeListView.TreeRenderer)this.treeListView.TreeColumnRenderer).LinePen.Color = Color.Black;
            //((TreeListView.TreeRenderer)this.treeListView.TreeColumnRenderer).LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //-------------------------------------------------------------------
            // Eveything after this is the same as the Explorer example tab --
            // nothing specific to the TreeListView. It doesn't have the grouping
            // delegates, since TreeListViews can't show groups.

/*           // Draw the system icon next to the name
#if !MONO
            SysImageListHelper helper = new SysImageListHelper(this.treeListView);
            this.treeColumnName.ImageGetter = delegate(object x)
            {
                return helper.GetImageIndex(((FileSystemInfo)x).FullName);
            };
#endif
  */          // Show the size of files as GB, MB and KBs. Also, group them by
            // some meaningless divisions
            this.treeColumnSize.AspectGetter = delegate(object x)
            {
                if (x is DirectoryInfo)
                    return (long)-1;

                try
                {
                    return ((FileInfo)x).Length;
                }
                catch (System.IO.FileNotFoundException)
                {
                    // Mono 1.2.6 throws this for hidden files
                    return (long)-2;
                }
            };
            this.treeColumnSize.AspectToStringConverter = delegate(object x)
            {
                if ((long)x == -1) // folder
                    return "";
                else
                    //return this.FormatFileSize((long)x);
                    return (string)"Deepak";
            };

            // Show the system description for this object
            this.treeColumnFileType.AspectGetter = delegate(object x)
            {
                //return ShellUtilities.GetFileType(((FileSystemInfo)x).FullName);
                return "Jagruti";
            };

            // Show the file attributes for this object
            this.treeColumnAttributes.AspectGetter = delegate(object x)
            {
                return ((FileSystemInfo)x).Attributes;
            };
            FlagRenderer<FileAttributes> attributesRenderer = new FlagRenderer<FileAttributes>();
            attributesRenderer.Add(FileAttributes.Archive, "archive");
            attributesRenderer.Add(FileAttributes.ReadOnly, "readonly");
            attributesRenderer.Add(FileAttributes.System, "system");
            attributesRenderer.Add(FileAttributes.Hidden, "hidden");
            attributesRenderer.Add(FileAttributes.Temporary, "temporary");
            this.treeColumnAttributes.Renderer = attributesRenderer;

            // List all drives as the roots of the tree
            ArrayList roots = new ArrayList();
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (di.IsReady)
                    roots.Add(new DirectoryInfo(di.Name));
            }
            this.treeListView.Roots = roots;
            this.treeListView.CellEditActivation = ObjectListView.CellEditActivateMode.F2Only;
        }

        void InitializeTreeListData()
        {
            
      /*     this.treeListView.CanExpandGetter = delegate(object x)
            {
                return (x is DirectoryInfo);
                //return (x is DataTable);
                
            };
            this.treeListView.ChildrenGetter = delegate(object x)
            {
                DirectoryInfo dir = (DirectoryInfo)x;
                return new ArrayList(dir.GetFileSystemInfos());
                //return new ArrayList(dt.Rows);
                
            };*/
            this.treeListView1.CanExpandGetter = delegate(object x)
//            { return (x is Array); };
            { return (x is DataTable); };
          /*  this.treeListView1.ChildrenGetter = delegate(object x)
            { return new ArrayList(dt.Rows); };*/


                // this.treeListView.CheckBoxes = true;
            
            // You can change the way the connection lines are drawn by changing the pen
                //((TreeListView.TreeRenderer)this.treeListView.TreeColumnRenderer).LinePen = Pens.Firebrick;
          //  ((TreeListView.TreeRenderer)this.treeListView1.TreeColumnRenderer).LinePen.Color = Color.Black;
          //  ((TreeListView.TreeRenderer)this.treeListView1.TreeColumnRenderer).LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //-------------------------------------------------------------------
            // Eveything after this is the same as the Explorer example tab --
            // nothing specific to the TreeListView. It doesn't have the grouping
            // delegates, since TreeListViews can't show groups.
            
            this.olvColumn1.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[0];
            };
            this.olvColumn2.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[1];
            };
            this.olvColumn3.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[2];
            };
            this.olvColumn4.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[3];
            };
            this.olvColumn5.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[4];
            };
            this.olvColumn6.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[5];
            };
            
            // List all drives as the roots of the tree
            ArrayList roots = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                roots.Add(dr.ItemArray);
            }

            //this.treeListView.Roots = roots;
            this.treeListView1.Roots = roots;
            //this.treeListView1.Roots = dt.Rows;
            
           // this.treeListView.CellEditActivation = ObjectListView.CellEditActivateMode.F2Only;
        }
        
        void InitializeTreeListArray()
        {

            /*     this.treeListView.CanExpandGetter = delegate(object x)
                  {
                      return (x is DirectoryInfo);
                      //return (x is DataTable);
                
                  };
                  this.treeListView.ChildrenGetter = delegate(object x)
                  {
                      DirectoryInfo dir = (DirectoryInfo)x;
                      return new ArrayList(dir.GetFileSystemInfos());
                      //return new ArrayList(dt.Rows);
                
                  };*/
            int[][] arr = new int[3][];

            // Initialize the elements:
            arr[0] = new int[5] { 11, 12, 13, 14, 15 };
            arr[1] = new int[4] { 2, 4, 6, 8 };
            arr[2] = new int[4] { 20, 40, 60, 80 };

            int[] c= new int[4]{1,2,2,4};
            
            
            int[][] baz = new int[2][] { new int[2], new int[3] };

            int[][,] jaggedArray4 = new int[3][,] 
            {
                new int[,] { {1,3}, {5,7} },
                new int[,] { {0,2}, {4,6}, {8,10} },
                new int[,] { {11,22}, {99,88}, {0,9} } 
            };
            //Console.Write(arr[0][0].ToString());

            this.treeListView1.CanExpandGetter = delegate(object x)
                        { return (x is Array); };
            this.treeListView1.ChildrenGetter = delegate(object x)
              { return new ArrayList(c); };


            // this.treeListView.CheckBoxes = true;

            // You can change the way the connection lines are drawn by changing the pen
            //((TreeListView.TreeRenderer)this.treeListView.TreeColumnRenderer).LinePen = Pens.Firebrick;
            //  ((TreeListView.TreeRenderer)this.treeListView1.TreeColumnRenderer).LinePen.Color = Color.Black;
            //  ((TreeListView.TreeRenderer)this.treeListView1.TreeColumnRenderer).LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //-------------------------------------------------------------------
            // Eveything after this is the same as the Explorer example tab --
            // nothing specific to the TreeListView. It doesn't have the grouping
            // delegates, since TreeListViews can't show groups.

            this.olvColumn1.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[0];
                //return (Convert.ToInt32(x))[0];
            };
            this.olvColumn2.AspectGetter = delegate(object x)
            {
                return ((Int32[])(x))[1];
            };
           /* this.olvColumn3.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[2];
            };
            this.olvColumn4.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[3];
            };
            this.olvColumn5.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[4];
            };
            this.olvColumn6.AspectGetter = delegate(object x)
            {
                return ((object[])(x))[5];
            };*/

            // List all drives as the roots of the tree
            ArrayList roots = new ArrayList();

            //foreach (DataRow dr in dt.Rows)
            foreach(Array a in arr)
            {
                roots.Add(a);
            }

            //this.treeListView.Roots = roots;
            this.treeListView1.Roots = roots;
            //this.treeListView1.Roots = dt.Rows;

            // this.treeListView.CellEditActivation = ObjectListView.CellEditActivateMode.F2Only;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load_data();
        }
        private void load_data()
        {
            // Add rows of data to the DataGridView.

            if (!blnOnce)
            {
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("SName", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("DOB", typeof(DateTime));
                dt.Columns.Add("PL", typeof(double));
                blnOnce = true;
            }
            dt.Rows.Add(new string[] { "6", "Deepak","A", "Portland", "1997, 8, 17", "-6.6" });
            dt.Rows.Add(new string[] { "10", "Saurav", "A", "Anup", "2009, 8, 17", "3.8" });
            dt.Rows.Add(new string[] { "-10", "Supam", "A", "Swiss", "1997, 8, 17", "-10.5" });
            dt.Rows.Add(new string[] { "9", "Jitendra", "A", "Russia", "2008, 8, 17", "9.8" });
            dt.Rows.Add(new string[] { "10", "Ive", "A", "Belgium", "1997, 8, 17", "-10.5" });
            //dt.Rows.Add(new string[] { "0", "Rupesh", "A", "Russia", "1997, 8, 17", "0" });
            //dt.Rows.Add(new string[] { "3", "Steev", "A", "Poland", "1997, 8, 17", "3.8" });
            //dt.Rows.Add(new string[] { "0", "Ravi", "A", "USA", "1997, 8, 17", "0" });
            //dt.Rows.Add(new string[] { "1", "Jignesh", "A", "USA", "1997, 8, 17", "-1.5" });
            //dt.Rows.Add(new string[] { "15", "Stain", "A", "Belgium", "1980, 8, 17", "15" });
            //dt.Rows.Add(new string[] { "15", "Stain", "A", "Poland", "1997, 8, 17", "15" });
            //dt.Rows.Add(new string[] { "1", "Kristina", "A", "USA", "1997, 8, 17", "1" });
            dv = dt.DefaultView;
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            // Declare the array of two elements:
            //int[][] arr = new int[3][];

            //// Initialize the elements:
            //arr[0] = new int[5] { 11, 12, 13, 14, 15 };
            //arr[1] = new int[4] { 2, 4, 6, 8 };

            //int[][] baz = new int[2][] {new int[2], new int[3]};

            //int[][,] jaggedArray4 = new int[3][,] 
            //{
            //    new int[,] { {1,3}, {5,7} },
            //    new int[,] { {0,2}, {4,6}, {8,10} },
            //    new int[,] { {11,22}, {99,88}, {0,9} } 
            //};



            //Console.Write(arr[0][0].ToString());
        }
    }
}

