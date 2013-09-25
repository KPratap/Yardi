using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NSConfig
{
    public class cNSConfig
    {
        public cNSConfig()
        {
        }
        public XDocument GetConfig(string fname)
        {
            XDocument doc = null;
            try
            {
                doc = XDocument.Load(fname);
                return doc;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        public XElement GetElement(XDocument doc, string ele)
        {
            XElement el = null;
            try
            {

                el = doc.Descendants(ele).First();
                return el;
            }
            catch (Exception ex)
            {
                return el;
                //throw ex;
            }
        }
        public XElement GetElement(XElement node, string ele)
        {
            XElement el = null;
            try
            {

                el = node.Descendants(ele).First();
                return el;
            }
            catch (Exception ex)
            {
                return el;
                //throw ex;
            }
        }

        //public XElement GetElementbyAttribName(XDocument doc, string attName, string attValue)
        //{
        //    XElement el = null;
        //    try
        //    {
        //        //el = doc.Descendants().Where(e => (string) e.Attributes(attName).First().Value == attValue);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public XElement GetElementByAttrib(XDocument doc, string eleName, string attName, string attVal)
        {
            try
            {
                XElement temp= null;
                IEnumerable<XElement> t = doc.Descendants(eleName);
                foreach (XElement elem in t)
                {
                    if (elem.Attribute(attName).Value == attVal)
                    {
                        temp = elem;
                        break;
                    }
                }
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<XElement> GetElements(XDocument doc, string ele)
        {
            List<XElement> el = null;
            try
            {

                el = doc.Descendants(ele).ToList();
                return el;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public XAttribute GetElementAttrib(XElement xel, string attrName)
        {
            XAttribute xatt = null;
            if (!xel.HasAttributes) return xatt;
            return xel.Attributes(attrName).First();
        }

        public bool SetElementAttrib(XElement xel, string attrName, string attValue)
        {
            if (!xel.HasAttributes) return  false;
            xel.Attribute(attrName).Value = attValue;
            return true;
        }
        public void
           // List<string> 
            ExtractElements(XElement root)
        {
            List<string> elements = new List<string>();
            StringBuilder line;
            foreach (var el in root.Descendants())
            {

                Console.WriteLine(el.Name);
                foreach (var attrib in el.Attributes())
                {
                    Console.WriteLine("> " + attrib.Name + " = " + attrib.Value);
                }
            }
        }
    }
}
