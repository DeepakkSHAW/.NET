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
    public partial class frmTrendGr : Form
    {
        #region private variables
        private DALDoubleM pDalGraph;
        private frmMain theParent;
        private GraphPane pgSimple;
        private LineItem[] gCurve;
        private Color[] colSymb = {Color.BlueViolet,Color.MediumBlue,Color.LightSeaGreen,Color.CadetBlue, Color.Green, Color.HotPink, Color.Black};
        #endregion

        #region Constructor
        public frmTrendGr(object pdalSNew)
        {
            InitializeComponent();
            pgSimple = zg1.GraphPane;
            pDalGraph = (DoubleM.DALDoubleM)pdalSNew;
            //colSymb = new Color[Color.Wheat, Color.Coral];
        }
        #endregion
        private void frmSimpleGr_Load(object sender, EventArgs e)
        {
            string sDM = "", sVM = "";
            //this.Icon = new Icon(this.GetType().Assembly.GetManifestResourceStream("DoubleM.Pics.GRAPH04.ICO"));
            theParent = (frmMain)this.ParentForm; //To access MDI parent control
            ddlStock.DataSource = pDalGraph.Stocks(ref sDM, ref sVM, 10); //10: all stocks active nonactive
            ddlStock.DisplayMember = sDM;
            ddlStock.ValueMember = sVM;

            if (ddlStock.Items.Count > 0) ddlStock.SelectedIndex = 0;
            gCurve = new LineItem[ddlStock.Items.Count*2];

            GraphPaint();
        }
        private void GraphPaint()
        {
            // Fill the axis background with a color gradient
            pgSimple.Chart.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 270F);

            // Fill the pane background with a color gradient
            pgSimple.Fill = new Fill(Color.Wheat, Color.LightYellow, 45F);
        }
        private void generateSimpleGraph()
        {
            DataTable dtGraph;
            int rndSymb=0;
            int SelectedS = ddlStock.SelectedIndex;

            Cursor = Cursors.WaitCursor;
            try
            {
                //Loading points in a collection//
                PointPairList list = new PointPairList();
                if (gCurve[SelectedS] == null) //Checking if collection already created
                {
                    //Loading Avg. Daily Stock Rates//ddlStock.SelectedIndex
                    dtGraph = pDalGraph.GraphSingle((int)ddlStock.SelectedValue);

                    // Set the titles and axis labels
                    //pgSimple.Title.Text = "DoubleM Simple Graph [ " + ddlStock.Text + " ]";
                    pgSimple.Title.Text = "";
                    pgSimple.XAxis.Title.Text = "";
                    pgSimple.YAxis.Title.Text = "";
                    theParent.lblDMMsg.Text = ddlStock.Text + " data loading..";
                    //CommonDoubleM.LogDM("DoubleM Simple Graph [ " + ddlStock.Text + " ]");
                    theParent.pBarDM.Visible = true;
                    theParent.pBarDM.Maximum = dtGraph.Rows.Count;
                    for (int ii = 0; ii < dtGraph.Rows.Count; ii++)
                    {
                        DateTime dt = Convert.ToDateTime(dtGraph.Rows[ii][1].ToString());
                        //CommonDoubleM.LogDM(dtGraph.Rows[ii][1].ToString() + " - " + dtGraph.Rows[ii][0].ToString());
                        double x = new XDate(dt.Year, dt.Month, dt.Day);
                        list.Add(x, Convert.ToDouble(dtGraph.Rows[ii][0].ToString()));
                        Application.DoEvents();
                        theParent.pBarDM.Value = ii;
                        //ii++;
                    }
                    rndSymb = CommonDoubleM.RandomNumber(0, 255);
                    theParent.pBarDM.Visible = false;

                    gCurve[SelectedS] = pgSimple.AddCurve(ddlStock.Text, list, colSymb[SelectedS % colSymb.Length], SymbolType.Circle);
                    gCurve[SelectedS].Symbol.Size = 3;
                    //gCurve[(int)ddlStock.SelectedValue].Label.FontSpec.Size = 8;

                    // Fill the area under the curve
                    //gCurve[SelectedS].Line.Fill = new Fill(Color.White, Color.LightGray, 45F);

                    // Fill Symbol  opaque by filling them with white 
                    gCurve[SelectedS].Symbol.Fill = new Fill(Color.White, colSymb[rndSymb % colSymb.Length], 180F);

                    //Pointing tradings//
                    DataRow[] drTrads = pDalGraph.TradingValue().Select("StockID=" + ddlStock.SelectedValue.ToString());
                    list = new PointPairList();
                    for (int ii = 0; ii < drTrads.Length; ii++)
                    {
                        DateTime dt = Convert.ToDateTime(drTrads[ii][0].ToString());
                        double x = new XDate(dt.Year, dt.Month, dt.Day);
                        list.Add(x, Convert.ToDouble(drTrads[ii][3].ToString()), Convert.ToDouble(drTrads[ii][4].ToString()));
                        //Application.DoEvents();
                    }

                    gCurve[SelectedS + ddlStock.Items.Count] = pgSimple.AddCurve("", list, Color.Red, SymbolType.Diamond);
                    gCurve[SelectedS + ddlStock.Items.Count].Symbol.Size = 8;
                    gCurve[SelectedS + ddlStock.Items.Count].Symbol.Fill = new Fill(Color.Green, Color.Red);
                    gCurve[SelectedS + ddlStock.Items.Count].Symbol.Border.IsVisible = false;
                    gCurve[SelectedS + ddlStock.Items.Count].Symbol.Fill.Type = FillType.GradientByZ;
                    //gCurve[SelectedS + ddlStock.Items.Count].Symbol.Fill.SecondaryValueGradientColor = Color.Empty;


                    //gCurve[SelectedS].Symbol.Fill.RangeDefault = 0;
                    gCurve[SelectedS + ddlStock.Items.Count].Symbol.Fill.RangeMin = -10;
                    gCurve[SelectedS + ddlStock.Items.Count].Symbol.Fill.RangeMax = 10;
                    gCurve[SelectedS + ddlStock.Items.Count].Line.IsVisible = false;

                    // Set the XAxis to date type
                    pgSimple.XAxis.Type = AxisType.Date;

                    GraphPaint();
                    // Fill the axis background with a color gradient
                    //pgSimple.Chart.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 270F);

                    // Fill the pane background with a color gradient
                    //pgSimple.Fill = new Fill(Color.Wheat, Color.LightYellow, 45F);
                    zg1.AxisChange();
                    zg1.Refresh();
                }
                else
                {
                    gCurve[ddlStock.SelectedIndex].IsVisible = true;
                    gCurve[ddlStock.SelectedIndex].Label.IsVisible = true;
                    gCurve[ddlStock.SelectedIndex + ddlStock.Items.Count].IsVisible = true;
                    gCurve[ddlStock.SelectedIndex + ddlStock.Items.Count].Label.IsVisible = true;
                }
                // Calculate the Axis Scale Ranges
                theParent.lblDMMsg.Text = ddlStock.Text + " data loaded.";
                zg1.ZoomOutAll(pgSimple);
                zg1.AxisChange();
            }
            catch (Exception ex)
            {
                theParent.lblDMMsg.Text = ex.Message;
                CommonDoubleM.LogDM(ex.Message);
            }
                Cursor = Cursors.Default; 
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            generateSimpleGraph();
            //gCurve[ddlStock.SelectedIndex].Line.IsSmooth = true;
            gCurve[ddlStock.SelectedIndex].Line.IsSmooth = true;
            chkSmooth.CheckState = CheckState.Checked;
            chkSym.CheckState = CheckState.Checked;
            zg1.AxisChange();
            zg1.Refresh();
        }

        private void chkSmooth_CheckedChanged(object sender, EventArgs e)
        {
            gCurve[ddlStock.SelectedIndex].Line.IsSmooth = chkSmooth.Checked;
            zg1.Refresh();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (gCurve[ddlStock.SelectedIndex] != null)
            {
                //gCurve[ddlStock.SelectedIndex].Clear();
                gCurve[ddlStock.SelectedIndex].IsVisible = false;
                gCurve[ddlStock.SelectedIndex].Label.IsVisible = false;
                gCurve[ddlStock.SelectedIndex + ddlStock.Items.Count].IsVisible = false;
                gCurve[ddlStock.SelectedIndex + ddlStock.Items.Count].Label.IsVisible = false;
                zg1.ZoomOutAll(pgSimple);
            }
            zg1.Refresh();
        }

        private void chkSym_CheckedChanged(object sender, EventArgs e)
        {
            gCurve[ddlStock.SelectedIndex].Symbol.IsVisible = chkSym.Checked;
            zg1.Refresh();
        }


    }
}