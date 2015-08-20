using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PSData
{
    public class Identification
    {
        public Identification()
        {
            IdValues = new Dictionary<string, string>();
        }
        public Dictionary<string, string> IdValues;
        public void GetIdValues(XElement src)
        {
            foreach (XElement sub in src.Descendants("Identification"))
            {
                //Console.WriteLine(sub);
                if (sub.Attribute("IDType") != null)
                {
                    if (sub.Attribute("IDType").Value == "Transaction ID" || sub.Attribute("IDType").Value == "Customer ID")
                        continue;
                    IdValues.Add(sub.Attribute("IDType").Value, sub.Element("IDValue").Value);
                }
            }
        }
    }
}
