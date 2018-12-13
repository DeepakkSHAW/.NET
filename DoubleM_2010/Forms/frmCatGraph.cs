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
        private string _PlotTitle = string.Empty;
        private DataSet _dsPlot = null;

        public frmCatGraph(object pdalSNew)
        {
            InitializeComponent();
            pDalGraph = (DoubleM.DALDoubleM)pdalSNew;
        }
        public frmCatGraph(object pdalSNew, DataSet dsPlot, string PlotTitle)
        {
            InitializeComponent();
            pDalGraph = (DoubleM.DALDoubleM)pdalSNew;
            _dsPlot = dsPlot;
            _PlotTitle = PlotTitle;
        }
        private void frmCatGraph_Load(object sender, EventArgs e)
        {
            // Setup the graph
            theParent = (frmMain)this.ParentForm; //To access MDI parent control
            if (_PlotTitle == "HighLowClose")
                CreateHighLow(zg1);
            else
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
            myPane.Title.Text = "Investment distribution sector wise";
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
        private void CreateHighLow(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;
            // Set the title and axis labels
            myPane.Title.Text = "Hi-Low-Close Daily Stock Chart";
            myPane.XAxis.Title.Text = "";
            myPane.YAxis.Title.Text = "Trading Price, Rs(INR)";

            // Set the title font characteristics
            myPane.Title.FontSpec.Family = "Arial";
            myPane.Title.FontSpec.IsItalic = true;
            myPane.Title.FontSpec.Size = 14;

            // Generate some random stock price data
            PointPairList hList = new PointPairList();
            PointPairList cList = new PointPairList();
            Random rand = new Random();
            // initialize the starting close price

            string sOnlyOneday = string.Empty;
            DateTime tKickoff = DateTime.MinValue;
            DataTable dtVirtual = new DataTable();
            string selectInDates = "Ondate>=#{0}# AND Ondate<#{1}#";
            DataTable dt = _dsPlot.Tables[0];
            dtVirtual = dt.Clone();

            foreach (DataRow dr in dt.Rows)
            {
                if (tKickoff.ToShortDateString().CompareTo(
                    Convert.ToDateTime(dr[3]).ToShortDateString()) != 0)
                {
                    // another date
                    tKickoff = Convert.ToDateTime(dr[3]);
                    dtVirtual.Rows.Clear();

                    sOnlyOneday = string.Format(selectInDates, tKickoff.ToShortDateString(), tKickoff.AddDays(1).ToShortDateString());
                    foreach (DataRow drStocks in dt.Select(sOnlyOneday))
                        dtVirtual.ImportRow(drStocks);

                    DataRow[] maxPrice = dtVirtual.Select("Price=Max(Price)");
                    DataRow[] minPrice = dtVirtual.Select("Price=Min(Price)");
                    DataRow[] clsPrice = dtVirtual.Select("Ondate=Max(Ondate)");
                   /* Console.WriteLine(maxPrice[0].ItemArray[2].ToString());
                    Console.WriteLine(minPrice[0].ItemArray[2].ToString());
                    Console.WriteLine(clsPrice[0].ItemArray[2].ToString());*/

                    //filling the data to plot the Graph
                    double x = (double)new XDate(tKickoff.Year, tKickoff.Month, tKickoff.Day);
                    double hi = Convert.ToDouble(maxPrice[0].ItemArray[2]);
                    double low = Convert.ToDouble(minPrice[0].ItemArray[2]);
                    double close = Convert.ToDouble(clsPrice[0].ItemArray[2]);
                    hList.Add(x, hi, low);
                    cList.Add(x, close);
                }
            }

            // Make a new curve with a "Closing Price" label
            LineItem curve = myPane.AddCurve("Closing Price", cList, Color.Black,
               SymbolType.Diamond);
            // Turn off the line display, symbols only
            curve.Line.IsVisible = false;
            // Fill the symbols with solid red color
            curve.Symbol.Fill = new Fill(Color.Red);
            curve.Symbol.Size = (float)4.5;

            // Add a blue error bar to the graph
            ErrorBarItem myCurve = myPane.AddErrorBar("Price Range", hList,
               Color.Blue);
            myCurve.Bar.PenWidth = 3;
            myCurve.Bar.Symbol.IsVisible = false;

            // Set the XAxis to date type
            myPane.XAxis.Type = AxisType.Date;
            // X axis step size is 1 day
            myPane.XAxis.Scale.MajorStep = 1;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Day;
            // tilt the x axis labels to an angle of 65 degrees
            myPane.XAxis.Scale.FontSpec.Angle = 65;
            myPane.XAxis.Scale.FontSpec.IsBold = true;
            myPane.XAxis.Scale.FontSpec.Size = 12;
            myPane.XAxis.Scale.Format = "d MMM";
            // make the x axis scale minimum 1 step less than the minimum data value
            myPane.XAxis.Scale.Min = hList[0].X - 1;

            // Display the Y axis grid
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.Scale.MinorStep = 0.5;

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White,
               Color.FromArgb(255, 255, 166), 90F);

            // Calculate the Axis Scale Ranges
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