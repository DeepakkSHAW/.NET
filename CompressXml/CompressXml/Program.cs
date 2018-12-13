using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CompressXml
{
    class Program
    {
        static void Main(string[] args)
        {
            string sXML = string.Empty;
            sXML = @"<?xml version=""1.0""?> 
<myXML>
                <Header>
                    <Name>Deepak</Name>
                    <SurName>Shaw</SurName>

            </Header>
            <Body>
                <To>Pune</To>
                <From>Melbourne</From>
            </Body>                  </myXML>";
            XmlDocument xdoc = new XmlDocument();
            xdoc.PreserveWhitespace = false;
            try
            {
                xdoc.LoadXml(sXML);
                Console.WriteLine("Orignal XML:\n" + sXML);
                Console.WriteLine("Compress XML:\n" + xdoc.InnerXml);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Please any key to close..");
            Console.ReadKey();

        }
    }
}
