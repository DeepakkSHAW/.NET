using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           //webBrowser1.Url = new Uri("http://yahoo.co.in");
            webBrowser1.Url = new Uri("http://www.bseindia.com/stockreach/stockreach.htm?scripcd=500002");
           // callMe();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if(webBrowser1.Document.Body.InnerText != null)
            timer1.Enabled = true;
            //System.Threading.Thread.Sleep(5000);
            //callMe();
            /*var document = webBrowser1.Document;
            document.Body.Focus();
            string updateInfo = document.Body.InnerText.ToString();
            richTextBox1.Text = updateInfo;*/
        }
        private void callMe()
        {
            var document = webBrowser1.Document;
            document.Body.Focus();
            string updateInfo = document.Body.InnerText.ToString();
            richTextBox1.Text = updateInfo;
            if (richTextBox1.Find("500002") > 0)
            {
                richTextBox1.Text = "found" + richTextBox1.Text;
            }
            else
            {
                webBrowser1.Url = new Uri("http://www.bseindia.com/stockreach/stockreach.htm?scripcd=500002");
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            callMe();
            timer1.Enabled = false;
        }
    }
}
