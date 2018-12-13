using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace PortfolioManager
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
             Form  frmShares;

            //Form frmShares;
            frmShares = new frmSharesPurchases();
            this.Cursor = Cursors.WaitCursor;
            frmShares.MdiParent  = this;
            frmShares.WindowState = FormWindowState.Maximized;
            frmShares.Show ();
            this.Cursor = Cursors.Default;
        }

        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fr1;
            fr1 = new DataUpdate();
            //fr1.MdiParent = this;
            //fr1.WindowState = FormWindowState.Maximized;
            fr1.Show ();
            //fr1.Show();

        }

        private void bonusSplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        /*    Form ff;
            ff = new   Form1();
            ff.MdiParent = this;
            ff.WindowState = FormWindowState.Maximized;
            ff.Show ();*/
        }

        private void myCurrentPortfolioToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

           

        }

       
        private void stockMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Form sm;
            sm = new frmStockMaster();
            sm.MdiParent = this;
            sm.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.WaitCursor;
            sm.Show();
            
           
            this.Cursor = Cursors.Default;
        }

        private void purchaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          /*  Form ff;
            ff = new Form1();
            ff.Show();*/


        }

        private void capitalGainLossFIFOToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form cgl;
            cgl = new frmCapitalGainLoss();
            cgl.MdiParent = this;
            cgl.WindowState = FormWindowState.Maximized;
            cgl.Show();
        }

        private void myCurrentPortfolioToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            Form CS;
            CS = new frmMyPortfolio();
            CS.MdiParent = this;
            CS.WindowState = FormWindowState.Maximized;
            CS.Show();
       
        }
    }
}