using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace CsharpXmlReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("111");
            XmlDocument doc = new XmlDocument();
            string generalCfgDir = "D:\\Config\\ConfigCV\\System\\General\\";
            string xmlFilename = "PatientOrientation.xml";
            doc.Load(generalCfgDir + xmlFilename);
            XmlNodeList nodeList = doc.SelectSingleNode("root").ChildNodes;
            Console.WriteLine("I've got " + nodeList.Count + " Patient_orientation:");

            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                Console.WriteLine(xe.Name);
                string orientation = xe.GetAttribute("value");  //headfirst
                string defaultFlag = xe.GetAttribute("default"); //1
                if(defaultFlag == String.Empty)
                {
                    defaultFlag = "Not default";
                }
                Console.WriteLine("\t"+orientation+"\t"+defaultFlag);

                XmlNodeList subnodeList = xe.ChildNodes;
                Console.WriteLine("\tGot " + subnodeList.Count + " in " + orientation);
                foreach (XmlNode cfg in subnodeList)
                {
                    XmlElement subXe = (XmlElement)cfg;
                    string angleValue = subXe.GetAttribute("value"); //FF-DL
                    string angleDefaultFlag = subXe.GetAttribute("default");  //1
                    if (angleDefaultFlag == String.Empty)
                    {
                        angleDefaultFlag = "Not default";
                    }
                    string imgPath = subXe.InnerText;  //FF-DLicon.png
                    Console.WriteLine("\t\t" + angleValue + "\t" + angleDefaultFlag + "\t" + imgPath);
                }
            }            
        }
    }
}
