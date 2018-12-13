using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb ;
using System.Globalization;
 
namespace PortfolioManager
{
    public partial class DataUpdate : Form
    {
        public DataUpdate()
        {
            InitializeComponent();
        }
        OleDbConnection conn1;
        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            //folderBrowserDialog1.SelectedPath = Environment.SpecialFolder.Desktop();
            folderBrowserDialog1.ShowDialog();
            txtTargetFolder.Text = folderBrowserDialog1.SelectedPath;
            chkBSE.Checked = false;
            chkNSE.Checked = false;
            CheckForFile();
        }

        void CheckForFile()
        {
            if (txtTargetFolder.Text.Equals(string.Empty))
            {
         
                return ;
            }

            if (File.Exists(txtTargetFolder.Text + "\\cm" + dateTimePicker1.Value.ToString("dd") + dateTimePicker1.Value.ToString("MMM").ToUpper() + dateTimePicker1.Value.Year + "bhav.csv"))
            {
                chkNSE.Checked = true;

            }
            if (File.Exists(txtTargetFolder.Text + "\\EQ" + dateTimePicker1.Value.ToString("dd") + dateTimePicker1.Value.ToString("MM") + dateTimePicker1.Value.ToString("yy") + ".csv"))
            {

                chkBSE.Checked = true;

            }
        }


      
        
           
        void DnFile_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            lblDownload.Text = "NSE File Downloaded";
            if (chkNSE.Checked==true)
            UpdateNSEData("NSE");
        }

        void DnFileBSE_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            lblDownload.Text = "BSE File Downloaded";
            UnzipFile(txtTargetFolder.Text + "\\EQ" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.ToString("MMM") + dateTimePicker1.Value.ToString("yyyy") + "bhav.zip");
            if (chkBSE.Checked==true)
            UpdateBSEData("BSE");
        }
        void DnMFFile_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            lblDownload.Text = "MF File Downloaded";
            if (chkMF .Checked == true)
            UpdateMFData("MF");

        }
        void DnFileBSE_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
           // pbBSE.Value = e.ProgressPercentage;
            label4.Text = e.ProgressPercentage.ToString() + " %";

            lblDownload.Text = "Downloading BSE Data File " + e.ProgressPercentage.ToString() + " Bytes Downloaded";
        }

       

        void DnFile_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
           // throw new Exception("The method or operation is not implemented.");
            label3.Text = e.ProgressPercentage.ToString() + " %";

            // pbNSE.Value = e.ProgressPercentage;
            lblDownload.Text = "Downloading NSE Data File " + e.ProgressPercentage.ToString() + " Bytes Downloaded";

        }
        void DnMFFile_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
           // throw new Exception("The method or operation is not implemented.");
           // pbNSE.Value = e.ProgressPercentage;
            label5.Text = e.ProgressPercentage.ToString () + " %";

            lblDownload.Text = "Downloading MF Data File " + e.ProgressPercentage.ToString() + " Bytes Downloaded";

        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            
            if (System.IO.Directory.Exists(txtTargetFolder.Text) == false)
            {
                MessageBox.Show("Directory Does not exists");
                return;
            }
            if (chkBSE.Checked == false && chkNSE.Checked == false && chkMF.Checked == false )
            {
                MessageBox.Show("Nothing to Process");
                return;
               
            }

            if (chkDownload.Checked==true)
            {
                if (txtTargetFolder.Text == string.Empty)
                {
                    MessageBox.Show("Please Select Target Folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                System.Net.WebClient DnFile;
                System.Net.WebClient DnFileBSE;
                System.Net.WebClient DnMFFile;

                DnFile = new System.Net.WebClient();
                DnFileBSE = new System.Net.WebClient();
                DnMFFile = new System.Net.WebClient();

                DnFile.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(DnFile_DownloadProgressChanged);
                DnFileBSE.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(DnFileBSE_DownloadProgressChanged);
                DnMFFile.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(DnMFFile_DownloadProgressChanged);
                
                DnFile.DownloadFileCompleted +=new AsyncCompletedEventHandler(DnFile_DownloadFileCompleted);
                DnFileBSE.DownloadFileCompleted+=new AsyncCompletedEventHandler(DnFileBSE_DownloadFileCompleted);
                DnMFFile.DownloadFileCompleted +=new AsyncCompletedEventHandler(DnMFFile_DownloadFileCompleted);

                //  DnFileBSE.DownloadFileCompleted += new AsyncCompletedEventHandler(DnFileBSE_DownloadFileCompleted);
                // DnFile.DownloadFileCompleted += new AsyncCompletedEventHandler(DnFile_DownloadFileCompleted);

                Uri siteuri;
              
                
                 
               
                try
                {
                    if (chkNSE.Checked == true )
                    {
                        siteuri = new Uri(@"http://www.nseindia.com/content/historical/EQUITIES/" + dateTimePicker1.Value.Year + "/" + dateTimePicker1.Value.ToString("MMM").ToUpper() + "/cm" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.ToString("MMM").ToUpper() + dateTimePicker1.Value.Year.ToString("0000") + "bhav.csv");
                        DnFile.DownloadFileAsync(siteuri, txtTargetFolder.Text + "\\cm" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.ToString("MMM").ToUpper() + dateTimePicker1.Value.Year.ToString("0000") + "bhav.csv");
                        //DnFile.DownloadFile(a, txtTargetFolder.Text + "\\cm" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.ToString("MMM").ToUpper() + dateTimePicker1.Value.Year.ToString("0000") + "bhav.csv");
                    }                        
                        if (chkBSE.Checked == true)
                        {
                            siteuri = new Uri(@"http://www.bseindia.com/bhavcopy/eq" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.ToString("MM") + dateTimePicker1.Value.ToString("yy") + "_csv.zip");
                            DnFileBSE.DownloadFileAsync(siteuri, txtTargetFolder.Text + "\\eq" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.ToString("MMM") + dateTimePicker1.Value.ToString("yyyy") + "bhav.zip");
                        }
                        //DnFileBSE.DownloadFile(b, txtTargetFolder.Text + "\\eq" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.Month.ToString("00") + dateTimePicker1.Value.ToString("yy") + ".zip");

                        if (chkMF.Checked == true)
                        {
                            siteuri = new Uri(@"http://www.amfiindia.com/portal/upload/downloadnav.txt");
                            DnMFFile.DownloadFileAsync(siteuri, txtTargetFolder.Text + "\\NAV.csv");
                        }
                }
                catch
                {

                }
                finally
                {

                }

            }



        }
         
            public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
{
    Double result;
    return Double.TryParse(val,NumberStyle,System.Globalization.CultureInfo.CurrentCulture,out result);
}
 public void UpdateNSEData (string UpdateFor)
 {
            OleDbConnection conn;
            conn = new OleDbConnection();
            OleDbCommand cmd;
            cmd = new OleDbCommand();
            OleDbDataReader dr;
            conn1 = new OleDbConnection();
            conn1.ConnectionString = "provider=microsoft.jet.oledb.4.0;data source=c:\\temp\\sharereport.mdb";
  
                button2.Enabled = false;
                conn1.Open();
                conn.ConnectionString = "provider=microsoft.jet.oledb.4.0;data source=" + txtTargetFolder.Text  + "\\;Extended Properties='text;HDR=Yes'";
                conn.Open();
                if (UpdateFor == "NSE")
                {
                     
                    cmd.CommandText = "select * from " + "cm" + dateTimePicker1.Value.ToString("dd") + dateTimePicker1.Value.ToString("MMM").ToUpper() + dateTimePicker1.Value.Year + "bhav.csv order by symbol";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    dr = cmd.ExecuteReader();
                    OleDbCommand cmd1;
                    OleDbDataReader drCount;
                    cmd1 = new OleDbCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandText = "select count(*) as ct from " + "cm" + dateTimePicker1.Value.ToString("dd") + dateTimePicker1.Value.ToString("MMM").ToUpper() + dateTimePicker1.Value.Year + "bhav.csv";
                    
                    
                    drCount = cmd1.ExecuteReader();
                    while (drCount.Read())
                    {
                        progressBar1.Maximum = Int32.Parse( drCount["ct"].ToString());
                    }
                    long i=0;
                    while (dr.Read())
                    {
                        
                        
                        i++;
                        lblStatus.Text = "Processing NSE For " + DateTime.Parse(dr["timestamp"].ToString()).ToLongDateString();
                        lblStatus1.Text = dr["symbol"].ToString() ;
                        lblStatus.Refresh();
                        lblStatus1.Refresh();
                        Application.DoEvents();
                        CheckForScriptcode(dr["symbol"].ToString(),dr["symbol"].ToString(),dr["series"].ToString(), float.Parse(dr["Close"].ToString()), dr["timestamp"].ToString(), "NSE", conn1);
                        progressBar1.Value = int.Parse(i.ToString());
                    }

                dr.Close();
                }
            }
  

      public void UpdateBSEData (string UpdateFor)
      {
          OleDbConnection conn;
          conn = new OleDbConnection();
          OleDbCommand cmd;
          cmd = new OleDbCommand();
          OleDbDataReader dr;
          conn1 = new OleDbConnection();
          conn1.ConnectionString = "provider=microsoft.jet.oledb.4.0;data source=c:\\temp\\sharereport.mdb";
         
              button2.Enabled = false;
              conn1.Open();
          
              conn.ConnectionString = "provider=microsoft.jet.oledb.4.0;data source=" + txtTargetFolder.Text + "\\;Extended Properties='text;HDR=Yes'";
              conn.Open();
              try
              {
                  if (UpdateFor == "BSE")
                  {
                      string filename;
                      filename = "EQ" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.Month.ToString("00") + dateTimePicker1.Value.Year.ToString().Replace("20", "") + ".CSV";
                      cmd.CommandText = "Select * from " + filename + " order by sc_code";
                      cmd.CommandType = CommandType.Text;
                      cmd.Connection = conn;
                      dr = cmd.ExecuteReader();
                      OleDbCommand cmd1;
                      OleDbDataReader drCount;
                      cmd1 = new OleDbCommand();
                      cmd1.Connection = conn;
                      cmd1.CommandText = "select count(*) as ct from " + filename  ;
                      drCount = cmd1.ExecuteReader();
                      while (drCount.Read())
                      {
                          progressBar2.Maximum = Int32.Parse(drCount["ct"].ToString());
                      }
                      long i = 0;
                      while (dr.Read())
                      {
                          i++;
                          //lblProgress.Text = "Processing BSE " + dr["sc_name"].ToString() + " For " + dateTimePicker1.Value.ToLongDateString () ;
                          lblStatus.Text = "Processing BSE " + dateTimePicker1.Value.ToLongDateString();
                          lblStatus1.Text = dr["sc_name"].ToString();
                          lblStatus.Refresh();
                          lblStatus1.Refresh();
                          Application.DoEvents();
                          CheckForScriptcode(dr["sc_code"].ToString(), dr["sc_name"].ToString(), dr["sc_group"].ToString(), float.Parse(dr["Close"].ToString()), dateTimePicker1.Value.ToShortDateString(), "BSE", conn1);
                          progressBar2.Value = int.Parse(i.ToString());
                      }
                      drCount.Close();
                      dr.Close();
                  }
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message.ToString());
              }
              finally
              {

              }
          }

      public void UpdateMFData (string UpdateFor)
      {
          OleDbConnection conn;
          conn = new OleDbConnection();
          OleDbCommand cmd;
          cmd = new OleDbCommand();
          OleDbDataReader dr;
          conn1 = new OleDbConnection();
          conn1.ConnectionString = "provider=microsoft.jet.oledb.4.0;data source=c:\\temp\\sharereport.mdb";
         try
         {
              button2.Enabled = false;
              conn1.Open();
              conn.ConnectionString = "provider=microsoft.jet.oledb.4.0;data source=" + txtTargetFolder.Text + "\\;Extended Properties='text;HDR=Yes'";
              conn.Open();
                if (UpdateFor=="MF")
                {

                    OleDbCommand cmd2;
                    OleDbDataReader drCount;
                    cmd2 = new OleDbCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "select count(*) as ct from " + "NAV"  + ".csv";
                    drCount = cmd2.ExecuteReader();
                    while (drCount.Read())
                    {
                        progressBar3.Maximum = Int32.Parse(drCount["ct"].ToString());
                    }
                    drCount.Close();
                    cmd2.Dispose();
                    cmd2.CommandText = "Select * from NAV.csv";
                    cmd2.Connection = conn;
                    dr = cmd2.ExecuteReader();
                    string strTemp;
                    progressBar3.Value = 0;
                    while (dr.Read())
                    {
                        char [] Splitter = { ';' };
                        strTemp = dr[0].ToString(); 
                        string [] SpString = new string[strTemp.Length];
                        SpString = strTemp.Split(Splitter);
                        string strseries="";
                        Application.DoEvents();
                        if (SpString.Length == 1)
                        {
                            strseries = SpString[0];
                            progressBar3.Value = progressBar3.Value + 1;
                        }
                        if (SpString.Length > 5)
                        {
                               float Price=0;
                            if (isNumeric(SpString[2].ToString(),NumberStyles.Float ))
                            {
                                Price = float.Parse(SpString[2].ToString());
                            }
                            else
                            {
                                Price = 0;
                            }
                            string ScName = SpString[1].Replace("'", "`") ;
                            if (SpString[1].Replace("'", "`").LastIndexOf("-") > 0)
                            {
                                ScName = SpString[1].Replace("'", "`").Substring(SpString[1].Replace("'", "`").LastIndexOf("-"), SpString[1].Replace("'", "`").Length - SpString[1].Replace("'", "`").LastIndexOf("-"));
                                ScName = ScName.Replace("-", "");
                            }
                            Application.DoEvents();

                            lblStatus.Text = "Processing MF For " + DateTime.Parse(SpString[5]).ToLongDateString();
                            lblStatus.Refresh();
                            lblStatus1.Text = SpString[1].ToString();
                            lblStatus1 .Refresh();
                            CheckForScriptcode(SpString[0], SpString[1].Replace("'", "`"),  ScName  , Price, DateTime.Parse(SpString[5]).ToShortDateString(), "Mutual Fund", conn1);
                            progressBar3.Value = progressBar3.Value + 1;
                        }


                           
                           
                    }

                }

              //MessageBox.Show("Processing Complete", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {

                            }
 }

        void CheckForScriptcode(string scriptcode,string scriptname,string series,float price,string Dt, string market,OleDbConnection  connPass)
        {

            try
            {
                OleDbCommand cmd1;

                cmd1 = new OleDbCommand();
                cmd1.Connection = connPass;
                if (Dt.Equals(string.Empty))
                {
                    Dt= dateTimePicker1.Value.ToShortTimeString();
                }
                int a;
                cmd1.CommandText = "select count(*)  from scriptmaster where scriptcode='" + scriptcode + "'";
                a = int.Parse("" + cmd1.ExecuteScalar());

                if (a > 0)
                {
                    cmd1.CommandText = "update scriptmaster set currentPrice=" + float.Parse (price.ToString()) + ",PortFolioDate='" + DateTime.Parse(Dt).ToShortDateString() + "',series='" + series + "',market='" + market + "' where scriptcode='" + scriptcode + "'";
                    cmd1.ExecuteNonQuery();
                }
                else
                {
                    cmd1.CommandText = "insert into scriptmaster (scriptcode,scriptname,series,market,currentprice,portfoliodate) values('" + scriptcode + "','" + scriptname + "','" + series + "','" + market + "'," + float.Parse(price.ToString()) + ",'" + DateTime.Parse(Dt).ToShortDateString() + "')";
                    cmd1.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show (ex.Message.ToString());
            }

             
            
          

        }
        private void UnzipFile(string zipFileName)
        {
            try
            {
                string fileName;
                fileName = "EQ" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.Month.ToString  ("00") + dateTimePicker1.Value.Year.ToString().Replace("20","") + ".CSV"; ;
                sbyte[] buf = new sbyte[1024];
                int len;
                //filename = "EQ" + dateTimePicker1.Value.Day.ToString("00") + dateTimePicker1.Value.Month.ToString("00") + dateTimePicker1.Value.Year.ToString("00") + ".csv" ;
                //filename = filename.Replace("zip", "CSV");
                java.io.FileInputStream fis = new java.io.FileInputStream(zipFileName);
                java.util.zip.ZipInputStream zis = new java.util.zip.ZipInputStream(fis);
                java.util.zip.ZipEntry ze;
                while ((ze = zis.getNextEntry()) != null)
                {
                    if (fileName  == ze.getName())
                    {
                        // File name format in zip file is:
                        // folder/subfolder/filename
                        // Let's check...
                        int index = fileName.LastIndexOf('/');
                        if (index > 1)
                        {
                            string folder = fileName.Substring(0, index);
                            DirectoryInfo di = new DirectoryInfo(folder);
                            // Create directory if not exists
                            if (!di.Exists)
                                di.Create();
                        }
                        java.io.FileOutputStream fos = new java.io.FileOutputStream(fileName);
                        while ((len = zis.read(buf)) >= 0)
                        {
                            fos.write(buf, 0, len);
                        }
                        fos.close();
                 
                    }
                }
                zis.close();
                fis.close();
               
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show ("Error" + ex.Message.ToString() );

            }
            finally 
            {
                // Close everything 
                
            }

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            CheckForFile();
        }

        private void DataUpdate_Load(object sender, EventArgs e)
        {
            txtTargetFolder.Text = Application.StartupPath;
        }
    }
}