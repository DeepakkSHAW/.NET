using System;
using System.Collections.Generic;
using System.Text;
//using System.ComponentModel;
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
        internal static DALDoubleM _pdalStock1;
        internal static bool _blnFirewll;
        internal static string _sProxy, _sUID, _sPWD;

        internal static string MarqueeString = "";
        internal static string[] Basics = { "Work Sheet", "Stock", "Stock Edit", "New Trad", "OO", "Trade Book", "Profit && Loss" };
        internal static string[] Online = { "Online Update", "Update Traker", "Online Portfolio","Watch List" };
        internal static string[] Analysis = { "Quick View - Avg", "Cumulative Average", "Trend Graph", "Investment Category Graph" };
        internal static string[] Views = { "Cascade", "Horizontal", "Vertical", "Arrange" };
        internal static string[] Configuration = { "Settings", "Proxy" };
        //http://www.example-code.com/csharp/aes_string_encryption.asp
        internal static string[] About = { "About", "Credit", "Licence" };

                    /*Loading Application configuration*/
        internal static void LoadAppConfig()
        {
                        /*Loading Application configuration*/
            udlDoubleM = AppWRDoubleM.ReadSetting("DoubleM_UDL").Trim();
            _blnFirewll = Convert.ToBoolean( AppWRDoubleM.ReadSetting("Firewall"));
            _sProxy = AppWRDoubleM.ReadSetting("Proxy").Trim();
            _sUID = AppWRDoubleM.ReadSetting("UID").Trim();
            _sPWD = CryptorEngine.Decrypt(AppWRDoubleM.ReadSetting("PWD"), true);
        }

        //to store the details in log file.
        internal static void LogDM(string sLog)
        {
            string sDate = DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Year.ToString();
            System.IO.StreamWriter sw = System.IO.File.AppendText(
                Environment.CurrentDirectory + "\\DoubleM_" + sDate + ".log");
            try
            {
                sw.WriteLine(sLog);
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
                WebRequest myWebRequest = WebRequest.Create("http://finance.yahoo.com/d/quotes.csv?s=" + sStockSyb.ToUpper().Trim() + "&f=sl1d1t1c1ohgv&e");

                /*
                 * sl1d1t1c1ohgv -> s l1 d1 t1 c1 o h g v 
                 * Stock Name,Last Trade Rate, Date, Time, Change, Open, Prev Close, Bid
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
                readStream.Close();
                myWebResponse.Close();
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
                readStream.Close();
                myWebResponse.Close();
            }
            catch (Exception ex)
            {
                LogDM(ex.Message);
                return blnTest;
            }
        }

        internal static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
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
                //result = System.Text.RegularExpressions.Regex.Replace(result, 
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty, 
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

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
                         @"<[^>]*>", string.Empty,
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
   
    }

}
