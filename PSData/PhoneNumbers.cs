using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PSData
{
    public class PhoneNumbers
    {
        public PhoneNumbers()
        {
            IdValues = new Dictionary<string, string>();
        }
        public Dictionary<string, string> IdValues;
        public void GetIdValues(XElement src)
        {
            if (src == null) return;
            foreach (XElement sub in src.Descendants("Phone"))
            {
                if (sub.Attribute("PhoneType") != null)
                {
                    string type = sub.Attribute("PhoneType").Value;
                    if (sub.Element("PhoneNumber") != null)
                        if (!IdValues.ContainsKey(type))
                            IdValues.Add(type, sub.Element("PhoneNumber").Value);
                }
            }
        }
    }
}
