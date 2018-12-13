using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace DoubleM
{
    //public string udlDoubleM = "";


    internal sealed class CommonDoubleM // to use like Module  
    {
        internal static string udlDoubleM = "DoubleM.udl";
        internal const string lngDate = "ddd dd-MMM-yyyy HH:mm";
        internal const string srtDate = "ddd dd-MMM-yyyy";
        internal const int MaxFailedAttemptAllowed = 10;
        internal static DateTime _SelectedDateTime = DateTime.Now;
        internal static DALDoubleM _pdalStock1;
        internal static bool _blnFirewll;
        internal static string _sProxy, _sUID, _sPWD;
        internal static string _sLatestPriceNotOldtThanHour = "12";
        internal static string _sDefaultExcelFile = "DoubleM.xls";
        internal static string _sBatchKey = string.Empty;
        internal static string _DoubleMPath = string.Empty;
        internal enum StockType : short {ActiveStocks=0, AllStocks=1, InactiveStocks=-1};
        internal enum StockExchange : short { BSE = 0, NSE = 1 };
        internal static string MarqueeString = "";
        internal static string[] Period = { "Last Week", "Two Weeks", "This Month", "Last Month", "This quater", "Last 6 Months", "This Year", "Last year" };

        internal static string[] Basics = { "Work Sheet", "Profit && Loss", "Manage Scripts", "Manage Stock Rates", "New Trad", "Manage Tradings", "Trade Book" };
        internal static string[] Online = { "BSE direct", "Yahoo direct", "Rediff direct", "Online DB Update", "Update Traker" };//, "Online Portfolio","Watch List" };
        internal static string[] Analysis = { "Quick View - Avg", "Cumulative Average", "Trend Graph", "Investment Category Graph" };
        internal static string[] Views = { "Cascade", "Horizontal", "Vertical", "Arrange" };
        internal static string[] Configuration = { "Settings", "Proxy" };
        //http://www.example-code.com/csharp/aes_string_encryption.asp
        internal static string[] About = { "About", "Credit", "Licence" };
        internal static string getColumnType(object dc)
        {
            if (dc == null) return null;
            
            string columnType = "TEXT";
            switch (dc.ToString())
            {
                case "System.Int64":
                case "System.Double":
                case "System.Int32":
                case "System.Decimal":
                    columnType = "NUMERIC";
                    break;
                default:
                    columnType = "TEXT";
                    break;
            }
            return columnType;
        }

                    /*Loading Application configuration*/
        internal static void LoadAppConfig()
        {
                        /*Loading Application configuration*/
            //string aName = System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            //string aPath = System.IO.Path.GetDirectoryName(aName);
            //Console.WriteLine("App.Path and App.ExeName Plus Extension Name is " + aPath);

            _DoubleMPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            udlDoubleM = AppWRDoubleM.ReadSetting("DoubleM_UDL").Trim();
            _sDefaultExcelFile = AppWRDoubleM.ReadSetting("ExcelExport");
            _blnFirewll = Convert.ToBoolean( AppWRDoubleM.ReadSetting("Firewall"));
            _sProxy = AppWRDoubleM.ReadSetting("Proxy").Trim();
            _sUID = AppWRDoubleM.ReadSetting("UID").Trim();
            _sPWD = CryptorEngine.Decrypt(AppWRDoubleM.ReadSetting("PWD"), true);
            _sLatestPriceNotOldtThanHour = AppWRDoubleM.ReadSetting("LatestPriceNotOldtThanHour").Trim();
            _sBatchKey = AppWRDoubleM.ReadSetting("Batchkey").Trim();
        }

        //to store the details in log file.
        internal static void LogDM(string sLog)
        {
            string sDate = DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Year.ToString();
            System.IO.StreamWriter sw = System.IO.File.AppendText(
                _DoubleMPath + "\\DoubleM_" + sDate + ".log");
            //Environment.CurrentDirectory -> doesn't working with Control like open/Save file
            try
            {
                string CurrentTime = "[" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff") + "] ";
                sw.WriteLine(CurrentTime + sLog);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false,
                   "A Creator as raised an exception !!!\n" + ex.Message);
            }
            finally
            { sw.Close(); }
        }
        //to get the authentication
        private static WebProxy wpInternetAccessAuthentication()
        {
            WebProxy proxyDoubleM;
                proxyDoubleM = new WebProxy(_sProxy.Remove(_sProxy.IndexOf(":")), 
                    Convert.ToInt32(_sProxy.Remove(0, _sProxy.IndexOf(":")+1)));
                proxyDoubleM.Credentials = new NetworkCredential(_sUID, _sPWD);
                System.Net.GlobalProxySelection.Select = proxyDoubleM;
            return proxyDoubleM;
        }
        internal static bool YahooStockLatestUpdates(string sStockSyb, ref string sLatest)
        {
            bool blnTest = false;
            try
            {
                WebRequest myWebRequest = WebRequest.Create("http://finance.yahoo.com/d/quotes.csv?s=" + sStockSyb.ToUpper().Trim() + "&f=sl1d1t1cohgv&e");

                /*
                 * sl1d1t1c1ohgv -> s l1 d1 t1 c1 o h g v c
                 * Stock Name,Last Trade Rate, Date, Time, Change, Open, Prev Close, Bid, change with percent change
                 * w - 52wk Range
                 * x - stock market
                 * t - graph
                 * m - monthly range
                 * 
                 * Alternative download: http://www.nseindia.com/content/historical/EQUITIES/2007/SEP/cm21SEP2007bhav.csv (NSE)
                 * http://www.bseindia.com/bhavcopy/eq210907_csv.zip (for BSE)
                 * http://www.bseindia.com/images/zoomchart.gif (for BSE chat access)
                 * http://www.bseindia.com/charting/newchart.asp (for BSE chat access)
                 */
                myWebRequest.Timeout = 90000;
                if (_blnFirewll)
                    myWebRequest.Proxy = wpInternetAccessAuthentication();
                WebResponse myWebResponse = myWebRequest.GetResponse();

                Stream ReceiveStream = myWebResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                string strResponse = readStream.ReadToEnd();
                LogDM(strResponse);

                sLatest = strResponse;
                blnTest = true;
                return blnTest;
                //readStream.Close();
                //myWebResponse.Close();
            }
            catch (Exception ex)
            {
                LogDM(ex.Message);
                return blnTest;
            }
        }

        //to get the updated values from Internet:finance.yahoo.com
        //internal static bool YahooStockLatestUpdates(string sStockSyb, ref string sLatest)
        //{
        //    bool blnTest = false;
        //    try
        //    {
        //        WebProxy proxy = new WebProxy("kolproxy.wipro.com", 8080);
        //        proxy.Credentials = new NetworkCredential(@"shawd", "Nestle");
        //        //proxy.Credentials = new NetworkCredential();
        //        System.Net.GlobalProxySelection.Select = proxy;
        //        WebRequest myWebRequest = WebRequest.Create("http://finance.yahoo.com/d/quotes.csv?s=" + sStockSyb.ToUpper().Trim() + "&f=sl1d1t1c1ohgv&e");

        //        /*
        //         * sl1d1t1c1ohgv -> s l1 d1 t1 c1 o h g v 
        //         * Stock Name,Last Trade Rate, Date, Time, Change, Open, Prev Close, Bid
        //         * w - 52wk Range
        //         * x - stock market
        //         * t - graph
        //         * m - monthly range
        //         * 
        //         * Alternative download: http://www.nseindia.com/content/historical/EQUITIES/2007/SEP/cm21SEP2007bhav.csv (NSE)
        //         * http://www.bseindia.com/bhavcopy/eq210907_csv.zip (for BSE)
        //         * http://www.bseindia.com/images/zoomchart.gif (for BSE chat access)
        //         * http://www.bseindia.com/charting/newchart.asp (for BSE chat access)
        //         */
        //        myWebRequest.Timeout = 90000;
        //        myWebRequest.Proxy = proxy;
        //        WebResponse myWebResponse = myWebRequest.GetResponse();

        //        Stream ReceiveStream = myWebResponse.GetResponseStream();
        //        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        //        StreamReader readStream = new StreamReader(ReceiveStream, encode);
        //        string strResponse = readStream.ReadToEnd();
        //        LogDM(strResponse);
                
        //        sLatest = strResponse;
        //        blnTest = true;
        //        return blnTest;
        //        readStream.Close();
        //        myWebResponse.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogDM(ex.Message);
        //        return blnTest;
        //    }
        //}
        internal static bool RediffStockLatestUpdates(string sStockSyb, ref string sLatest)
        {
            bool blnTest = false;
            try
            {
                /*WebProxy proxy = new WebProxy("kolproxy.wipro.com", 8080);
                proxy.Credentials = new NetworkCredential(@"shawd", "Nestle");
                //proxy.Credentials = new NetworkCredential();
                System.Net.GlobalProxySelection.Select = proxy;*/
                WebRequest myWebRequest = WebRequest.Create("http://money.rediff.com/money/jsp/current_stat.jsp?companyCode=" + sStockSyb.ToUpper().Trim());

                /*Reading Stock latest Values from http://money.rediff.com/money/jsp/current_stat.jsp?companyCode=STOCKCODE
                 * After we need to filter the pure HTLM code
                 */
                myWebRequest.Timeout = 90000;
                if(_blnFirewll)
                myWebRequest.Proxy = wpInternetAccessAuthentication();
                WebResponse myWebResponse = myWebRequest.GetResponse();

                Stream ReceiveStream = myWebResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                string strResponse = readStream.ReadToEnd();
                //LogDM(strResponse);

                sLatest = strResponse;
                blnTest = true;
                return blnTest;
                //readStream.Close();
                //myWebResponse.Close();
            }
            catch (Exception ex)
            {
                LogDM(ex.Message);
                return blnTest;
            }
        }
        internal static long DownloadFile(String remoteFilename, String localFilename, System.Windows.Forms.ToolStripProgressBar pBarDoubleM)
        {
          //Code taken from
          //http://www.codeguru.com/csharp/csharp/cs_network/internetweb/article.php/c7005/

          // Function will return the number of bytes processed
          // to the caller. Initialize to 0 here.
          long bytesProcessed = 0;
          // Assign values to these objects here so that they can
          // be referenced in the finally block
          Stream remoteStream  = null;
          Stream localStream   = null;
          WebResponse response = null;
          
          // Use a try/catch/finally block as both the WebRequest and Stream
          // classes throw exceptions upon error
          try
          {
            // Create a request for the specified remote file name
              WebRequest request = null;
              if (_blnFirewll)
                  request.Proxy = wpInternetAccessAuthentication();
              request = WebRequest.Create(remoteFilename);
              
            if (request != null)
            {
              // Send the request to the server and retrieve the
              // WebResponse object
              response = request.GetResponse();
              if (response != null)
              {
                // Once the WebResponse object has been retrieved,
                // get the stream object associated with the response's data
                remoteStream = response.GetResponseStream();
                
                // Create the local file
                localStream = File.Create(localFilename);
                pBarDoubleM.Visible = true;
                pBarDoubleM.Maximum = 1024*1024;
                // Allocate a 1k buffer
                byte[] buffer = new byte[1024];
                int bytesRead;
                LogDM("Downloading " + remoteFilename);

                // Simple do/while loop to read from stream until
                // no bytes are returned
                do
                {

                  // Read data (up to 1k) from the stream
                  bytesRead = remoteStream.Read (buffer, 0, buffer.Length);

                  // Write the data to the local file
                  localStream.Write (buffer, 0, bytesRead);

                  // Increment total bytes processed
                  bytesProcessed += bytesRead;
                    if(bytesRead < pBarDoubleM.Maximum)
                      pBarDoubleM.Value += bytesRead;
                } while (bytesRead > 0);
              }
            }
          }
          catch(Exception ex)
          {
            LogDM("Function DownloadFile: " + remoteFilename +" "+ ex.Message);
              if(ex.Message.Contains("404")) bytesProcessed = -1;
          }
          finally
          {
            // Close the response and streams objects here
            // to make sure they're closed even if an exception
            // is thrown at some point
            if (response     != null) response.Close();
            if (remoteStream != null) remoteStream.Close();
            if (localStream  != null) localStream.Close();
            pBarDoubleM.Visible = false;
          }

          // Return total bytes processed to caller.
          return bytesProcessed;
        }
        internal static long getHitoricalData(short sExchange, string FileName,System.Windows.Forms.ToolStripProgressBar pBarDM)
        {
/*      
 * http://ichart.finance.yahoo.com/table.csv?s=BHEL.NS&d=9&e=13&f=2009&g=d&a=7&b=12&c=2002&ignore=.csv
 * http://ichart.finance.yahoo.com/table.csv?s=BHEL.NS&a=08&b=12&c=2002&d=09&e=13&f=2009&g=d&ignore=.csv
 * http://in.rd.yahoo.com/finance/quotes/internal/historical/download/*http://ichart.finance.yahoo.com/table.csv?s=BHEL.NS&a=04&b=12&c=2002&d=09&e=13&f=2009&g=d&ignore=.csv
*/
            const string sNSE = "http://www.nseindia.com/content/historical/EQUITIES/{0}/{1}/cm{2}bhav.csv"; //"http://www.nseindia.com/content/historical/EQUITIES/2009/SEP/cm16SEP2009bhav.csv";
            const string sBSE = "http://www.bseindia.com/bhavcopy/eq{0}{1}{2}_csv.zip";//"http://www.bseindia.com/bhavcopy/eq160909_csv.zip";

            string sURL = string.Empty;
            switch (sExchange) 
            {
                case (short)StockExchange.NSE:
                    sURL = string.Format(sNSE, _SelectedDateTime.ToString("yyyy"), _SelectedDateTime.ToString("MMM").ToUpper(), _SelectedDateTime.ToString("ddMMMyyyy").ToUpper()); 
                    break;

                case (short)StockExchange.BSE:
                    sURL = string.Format(sBSE, _SelectedDateTime.ToString("dd"), _SelectedDateTime.ToString("MM"), _SelectedDateTime.ToString("yy"));
                    break;
                default:
                    break;
            }
                return (DownloadFile(sURL, FileName, pBarDM));
        }
        internal static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        internal static string FormatBytes(long Bytes)
        {
          string filesize;
          if (Bytes >= 1073741824)
          {
            decimal size = decimal.Divide(Bytes, 1073741824);
            filesize = string.Format("{0:##.##} GB", size);
          }
          else if (Bytes >= 1048576)
          {
            decimal size = decimal.Divide(Bytes, 1048576);
            filesize = string.Format("{0:##.##} MB", size);
          }
          else if (Bytes >= 1024)
          {
            decimal size = decimal.Divide(Bytes, 1024);
            filesize = string.Format("{0:##.##} KB", size);
          }
          else if (Bytes > 0 & Bytes < 1024)
          {
            decimal size = Bytes;
            filesize = string.Format("{0:##.##} Bytes", size);
          }
          else
          {
            filesize = "0 Bytes";
          }
          return filesize;
        }
        internal static string StripHTML(string source)
        {
            /*To convert raw HTML code to text
            http://www.codeproject.com/KB/dotnet/HTML_to_Plain_Text.aspx
             */
            try
            {

                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating speces becuase browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                         string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>).*(</script>)", string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything thats enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testng
                //System.Text.RegularExpressions.Regex.Replace(result, 
                //       this.txtRegex.Text,string.Empty, 
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4. 
                // Prepare first to remove any whitespaces inbetween
                // the escaped characters and remove redundant tabs inbetween linebreaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multible tabs followind a linebreak with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for linebreaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace("  ", " ");
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // Thats it.
                return result;

            }
            catch (Exception ex)
            {
                LogDM(ex.Message);
                return source;
            }
        }
        internal static Int32 GetColorcode(double sead)
        {
            int iR = 255, iG = 255, iB = 255;
            int iMax = 0, iMin = 0;
            //http://www.flounder.com/csharp_color_table.htm (for color coding)
            if (sead < 0)
            {
                iMin = 255 - (int)(sead * -4);
                if ((iMin < 0) || (iMin > 255)) iMin = 0;

                iG = iMin;
                iB = iMin;
            }
            else
            {
                iMax = 255 - (int)(sead * 4);
                if ((iMax < 0) || (iMax > 255)) iMax = 0;
                iR = iMax;
                iB = iMax;
            }
            return System.Drawing.Color.FromArgb(iR, iG, iB).ToArgb();
        }
        internal static DataTable getDataTable(System.Windows.Forms.DataGridView dgv)
        {
            DataTable dtReturn = null;
            try
            {
               dtReturn = new DataTable("Return");
                if (dgv.ColumnCount == 0) return dtReturn;

                foreach (System.Windows.Forms.DataGridViewColumn dc in dgv.Columns)
                {
                    string dType = null;
                    
                    if (dc.Visible)
                    {
                        dType = getColumnType(dc.ValueType);
                        if (dType == null)
                        {
                            if (dgv.RowCount > 0)
                                dType = getColumnType(dgv[dc.Name, 0].Value.GetType());
                            else
                                dType = "TEXT"; //default data type
                        }
                        //DataTable Schema generation
                        switch (dType) 
                        {
                            case "TEXT":
                                dtReturn.Columns.Add(dc.Name,typeof(string));
                                break;
                            case "NUMERIC":
                                dtReturn.Columns.Add(dc.Name, typeof(double));
                                break;
                            case "DATETIME":
                                dtReturn.Columns.Add(dc.Name, typeof(DateTime));
                                break;
                        }
                    }
                }
                //Insert values to DataTable
                foreach (System.Windows.Forms.DataGridViewRow row in dgv.Rows)
                {
                    DataRow drVirtual;
                    drVirtual = dtReturn.NewRow();
                    foreach (System.Windows.Forms.DataGridViewColumn col in dgv.Columns)
                    {
                        if (col.Visible)
                            drVirtual[col.Name] = dgv[col.Name, row.Index].Value == null ? DBNull.Value : dgv[col.Name, row.Index].Value;
                    }
                    dtReturn.Rows.Add(drVirtual);
                }
            }
            catch (Exception ex) { LogDM(ex.Message); }
            return dtReturn;

        }
        internal static string[] FilterOnlineValues(string sInterNetUpdates)
        {
            try
            {
                string[] sFilter = sInterNetUpdates.Split(',');
                Console.WriteLine(sFilter[2]);
                return sFilter;
            }
            catch (Exception ex)
            {
                LogDM(ex.Message);
                return null;
            }
        }
    }

}
