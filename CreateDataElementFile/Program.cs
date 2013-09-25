using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NSConfig;

namespace CreateDataElementFile
{
    class Program
    {
        static cNSConfig ccfg = new cNSConfig();
        static XDocument doc = new XDocument();
        static XDocument _cli;
        static void Main(string[] args)
        {
            _cli= XDocument.Load("Collections Sample.xml");
            ccfg.ExtractElements(_cli.Descendants("PropertyFiles").FirstOrDefault());
        }
    }
}
