using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSConfig;
using System.Xml.Linq;

namespace TestNSConfig
{
    class Program
    {
        static cNSConfig cfg = new cNSConfig();
        static void Main(string[] args)
        {
            // File not present
            //FileNotPresent();
            // File present
            //FilePresent();
            //Get element not present
            //GetElementNotPres();
            //Get element present
            //GetEementPres();
            //Get element not present
            //GetElementAttributeNP();
            GetElementbyAttribute("client","keyword","FirstClient");
            GetElementbyAttribute("client", "keyword", "FirstCli");
        }
        static void FileNotPresent()
        {
            try
            {
                XDocument cfgDoc = cfg.GetConfig("test.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static void FilePresent()
        {
            try
            {
                XDocument cfgDoc = cfg.GetConfig("xmltest.xml");
                Console.WriteLine(cfgDoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static void GetElementNotPres()
        {
            try
            {
                XDocument cfgDoc = cfg.GetConfig("xmltest.xml");
                XElement el = cfg.GetElement(cfgDoc, "yardi");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static void GetEementPres()
        {
            try
            {
                XDocument cfgDoc = cfg.GetConfig("xmltest.xml");
                XElement el = cfg.GetElement(cfgDoc, "clients");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static void GetElementAttributeNP()
        {
            try
            {
                XDocument cfgDoc = cfg.GetConfig("xmltest.xml");
                XElement el = cfg.GetElement(cfgDoc, "element1");
                XAttribute xatt = cfg.GetElementAttrib(el, "user");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        static void GetElementAttribute()
        {
            try
            {
                XDocument cfgDoc = cfg.GetConfig("xmltest.xml");
                XElement el = cfg.GetElement(cfgDoc, "client");
                XAttribute xatt = cfg.GetElementAttrib(el, "keyword");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static void GetElementbyAttribute(string elName, string attName, string attVal)
        {
            try
            {
                XDocument cfgDoc = cfg.GetConfig("xmltest.xml");
                XElement el = cfg.GetElementByAttrib(cfgDoc, elName, attName, attVal);
                if (el == null)
                    Console.WriteLine("Element not found");
                else
                Console.WriteLine(el.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
