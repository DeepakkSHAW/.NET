using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
namespace DoubleM
{

    public partial class frmCatGraph : Form
    {
        private DALDoubleM pDalGraph;
        private frmMain theParent;

        public frmCatGraph(object pdalSNew)
        {
            InitializeComponent();
            pDalGraph = (DoubleM.DALDoubleM)pdalSNew;
        }

        private void frmCatGraph_Load(object sender, EventArgs e)
        {
            // Setup the graph
            theParent = (frmMain)this.ParentForm; //To access MDI parent control

            CreateGraph(zg1);
            // Size the control to fill the form with a margin
            SetSize();
        }
        private void CreateGraph(ZedGraphControl zgc)
        {

            GraphPane myPane = zgc.GraphPane;
            double total = 0;

            Random rnd = new Random();
            // Set the GraphPane title
            myPane.Title.Text = "Investment Distribution Sector";
            myPane.Title.FontSpec.IsItalic = true;
            myPane.Title.FontSpec.Size = 20f;
            //myPane.Title.FontSpec.Family = "Times New Roman";

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.White, Color.Goldenrod, 45.0f);
            // No fill for the chart background
            myPane.Chart.Fill.Type = FillType.None;
            Cursor = Cursors.WaitCursor;
            try
            {
                DataView dvCatGraph = pDalGraph.GraphCatStocks(DateTime.Today.Date);
                //MessageBox.Show(dvCatGraph.Count.ToString());
                if (dvCatGraph.Count > 0) // Categories count
                {
                    PieItem[] PieItems;
                    PieItems = new PieItem[dvCatGraph.Count];

                    for (int i = 0; i < dvCatGraph.Count; i++)
                    {
                        // Add some pie slices
                        PieItems[i] = myPane.AddPieSlice(Convert.ToDouble(dvCatGraph.Table.Rows[i][2]),
                            Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)),Color.White, 0.25f,
                            0.05f,
                            //(Convert.ToDouble(dvCatGraph.Table.Rows[i][2]) > 75000.0 ? 0.05f : 0.01f),
                            dvCatGraph.Table.Rows[i][1].ToString());
                        // Sum up the pie values 
                        total += Convert.ToDouble(dvCatGraph.Table.Rows[i][2]);
                    }
                    
                    //PieItems[5].Label.Text = PieItems[5].Label.Text + Convert.ToString(((PieItems[5].Value)/total)*100);

                    // Make a text label to highlight the total value
                    TextObj text = new TextObj("Total Investment\n" + "Rs. " + total.ToString() + " /-",
                                   0.12F, 0.12F, CoordType.PaneFraction);
                    text.Location.AlignH = AlignH.Center;
                    text.Location.AlignV = AlignV.Bottom;
                    text.FontSpec.Border.IsVisible = false;
                    text.FontSpec.Fill = new Fill(Color.White, Color.FromArgb(255, 100, 100), 45F);
                    text.FontSpec.StringAlignment = StringAlignment.Center;
                    myPane.GraphObjList.Add(text);

                    // Create a drop shadow for the total value text item
                    TextObj text2 = new TextObj(text);
                    text2.FontSpec.Fill = new Fill(Color.Black);
                    text2.Location.X += 0.008f;
                    text2.Location.Y += 0.01f;
                    myPane.GraphObjList.Add(text2);
                }
            }
            catch (Exception ex)
            {
                theParent.lblDMMsg.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }
            Cursor = Cursors.Default; 

            // Set the legend to an arbitrary location
            myPane.Legend.Position = LegendPos.Float;
            myPane.Legend.Location = new Location(0.97f, 0.03f, CoordType.PaneFraction,
                           AlignH.Right, AlignV.Top);
            myPane.Legend.FontSpec.Size = 10f;
            myPane.Legend.IsHStack = false;

            zgc.AxisChange();
        }
        private void SetSize()
        {
            //zg1.Location = new Point(10, 10);
            //// Leave a small margin around the outside of the control
            //zg1.Size = new Size(ClientRectangle.Width - 20,
            //                        ClientRectangle.Height - 20);
        }
        private void frmCatGraph_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

    }
}