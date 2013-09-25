using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YardiDataTest
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
                if (sub.Element("IDValue") != null)
                {
                    if (sub.Element("OrganizationName") != null)
                        IdValues.Add(sub.Element("OrganizationName").Value.ToLower(),sub.Element("IDValue").Value);
                }
            }
        }
    }
}
