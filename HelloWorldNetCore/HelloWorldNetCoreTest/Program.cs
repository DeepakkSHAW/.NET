using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace HelloWorldNetCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["URLREST"].tostr);
            Console.ReadKey();




            //string url = string.Format("{0}/name?PrimaryName={1}", System.Configuration.ConfigurationManager.AppSettings["URLREST"], "yournmae");
            //string details = CallRestMethod(url);
        }

        //https://stackoverflow.com/questions/22627296/how-to-call-rest-api-from-a-console-application
        public static string CallRestMethod(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Headers.Add("Username", "xyz");
            webrequest.Headers.Add("Password", "abc");
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }
    }
}
