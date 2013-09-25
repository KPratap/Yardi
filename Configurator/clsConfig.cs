using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Configurator
{
    public class clsConfig
    {
        private XmlDocument _doc;
        
        public clsConfig()
        { }
        public XmlDocument LoadDoc(string fullname)
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(fullname);
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No Config File found.", e);
            }
        }
        public string ReadSetting(string key)
        {
            string val = string.Empty;
            XmlNode node = _doc.SelectSingleNode("//appSettings");
            XmlElement elem = (XmlElement) node.SelectSingleNode(String.Format("//add[@key='{0}']",key));
            if (elem != null)
            {
                val = elem.GetAttribute("value");
            }
            return val;
        }

    }
    
}
