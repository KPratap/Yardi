using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using YardiInterface;

namespace ConsWebSvcTest
{
    class Program
    {

        static YardiInterface.YardiInterface yif;
        static void Main(string[] args)
        {
            //Getproperties();
            string urldev = "https://www.iyardiasp.com/8223thirddev/Webservices/itfcollections.asmx";
            string urlqa = "https://www.iyardiasp.com/8223thirdqa/Webservices/itfcollections.asmx";
            //GetVersion(urldev);
            GetCollectionsLeaseInfo("rentrec1", urlqa);
            GetCollectionsLeaseInfo("rentrec1", urldev);
            GetCollectionsLeaseInfo_iface("rentrec1", urldev, "V100055004.lic");
            //GetVersion(urlqa);
            //GetCollectionsLeaseInfo_Nolic("rentrec1", urlqa);
            //Getproperties_Iface("https://www.iyardiasp.com/8223thirdqa/Webservices/itfcollections.asmx");
           // Getproperties_Iface("https://www.iyardiasp.com/8223thirddev/Webservices/itfcollections.asmx", "V100055004.lic");
            //Console.WriteLine(NeedLicense("15_0", "16_0"));
            //Console.WriteLine(NeedLicense("15_3", "16_0"));
            //Console.WriteLine(NeedLicense("16_0", "15_0"));
            //Console.WriteLine(NeedLicense("16_0", "16_0"));
            
            //Ping();
        }
        private static bool NeedLicense(string cli, string thresh)
        {
            if (string.Compare(cli, thresh) >= 0)
                return true;
            else return false;
        }
        private static void Ping()
        {
            try
            {
                YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
                s.Url = "https://www.iyardiasp.com/8223thirddev/Webservices/itfcollections.asmx";
                string XmlNodeResponse;
                XmlNodeResponse = s.Ping();

                Console.WriteLine(XmlNodeResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void GetVersion(string url, string lic="")
        {
            yif = new YardiInterface.YardiInterface(url, lic);// "V100055004.lic");
            string ver = yif.ClientVersion;
            Console.WriteLine(ver);
        }
        private static void GetVersionOld()
        {
            YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
            s.Url = "https://www.iyardiasp.com/8223thirddev/Webservices/itfcollections.asmx";
            //s.Url = "https://www.iyardiasp.com/8223thirdqa/webservices/itfcollections.asmx";
            string XmlNodeResponse;
            XmlNodeResponse = s.GetVersionNumber();
            Console.WriteLine(XmlNodeResponse);
        }
        static void GetCollectionsLeaseInfo(string prop, string url)
        {
            YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
            //s.CookieContainer = new System.Net.CookieContainer();
            //ServicePointManager.CertificatePolicy = new CertificateValidation();
            s.Url = url;      // "https://www.iyardiasp.com/8223thirddev/Webservices/itfcollections.asmx";
            string lic = GetLicense2("V100055004.lic");
            XmlNode XmlNodeResponse;
            XmlNodeResponse = s.Get_CollectionsLeaseInfo(
                "rentrecoveryws",
                "55004",
                "aspdb04",
                "afqoml_live",
                "SQL Server",
                "Rent Recovery Solutions",
                lic,
                prop
               );
            XElement xE2 = XElement.Parse(XmlNodeResponse.OuterXml);
            xE2.Save(prop + "_" + DateTime.Now.ToString("MMddyyyy_hhmm") + ".xml");
            Console.WriteLine(xE2);
        }
        static void GetCollectionsLeaseInfo_iface(string prop, string url, string lic="")
        {
            try
            {
                yif = new YardiInterface.YardiInterface(url, lic);
                yif.User = "rentrecoveryws";
                yif.Pwd = "55004";
                yif.Database = "aspdb04";
                yif.Server = "afqoml_live";
                yif.Platform = "SQL Server";
                yif.EntityName = "Rent Recovery Solutions";
                yif.YardiPropId = prop;
                //if (lic != "")
                //    yif.LicenseFile = lic;
                XElement xE2 = yif.GetCollectionsLeaseInfo(prop);
                xE2.Save(prop + "_" + DateTime.Now.ToString("MMddyyyy_hhmm") + ".xml");
                Console.WriteLine(xE2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static void GetCollectionsLeaseInfo_Nolic(string prop, string url)
        {
            YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
            //s.CookieContainer = new System.Net.CookieContainer();
            //ServicePointManager.CertificatePolicy = new CertificateValidation();
            s.Url = url; // s.Url url;   //= "https://www.iyardiasp.com/8223thirdqa/Webservices/itfcollections.asmx";
            //string lic = GetLicense2("V100055004.lic");
            XmlNode XmlNodeResponse;
            XmlNodeResponse = s.Get_CollectionsLeaseInfo(
                "rentrecoveryws",
                "55004",
                "aspdb04",
                "afqoml_live",
                "SQL Server",
                "Rent Recovery Solutions",
                prop
               );
            XElement xE2 = XElement.Parse(XmlNodeResponse.OuterXml);
            xE2.Save(prop + "_" + DateTime.Now.ToString("MMddyyyy_hhmm") + ".xml");
            Console.WriteLine(xE2);
        }
        static void Getproperties_Iface(string url, string lic="")
        {
            yif = new YardiInterface.YardiInterface(url, lic);
            yif.User =   "rentrecoveryws";
            yif.Pwd = "55004";
            yif.Database = "aspdb04";
            yif.Server = "afqoml_live";
            yif.Platform = "SQL Server";
            yif.EntityName = "Rent Recovery Solutions";
            if (lic != "")
                yif.LicenseFile = lic;

            XElement xE2 =yif.Getproperties();
            //s.Url = "https://www.iyardiasp.com/8223thirddev/Webservices/itfcollections.asmx";
            //string lic = GetLicense("V100055004.lic");
            //XmlNode XmlNodeResponse;
            //XmlNodeResponse = s.GetPropertyConfigurations(
            //    "rentrecoveryws",
            //    "55004",
            //    "aspdb04",
            //    "afqoml_live",
            //    "SQL Server",
            //    "Rent Recovery Solutions",
            //    lic
            //   );
            //XElement xE2 = XElement.Parse(XmlNodeResponse.OuterXml);
            Console.WriteLine(xE2);
        }
        static void Getproperties()
        {
            YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
            s.CookieContainer = new System.Net.CookieContainer();
            ServicePointManager.CertificatePolicy = new CertificateValidation();
            s.Url = "https://www.iyardiasp.com/8223thirddev/Webservices/itfcollections.asmx";
            string lic = GetLicense("V100055004.lic");
            XmlNode XmlNodeResponse;
            XmlNodeResponse = s.GetPropertyConfigurations(
                "rentrecoveryws",
                "55004",
                "aspdb04",
                "afqoml_live",
                "SQL Server",
                "Rent Recovery Solutions",
                lic
               );
            XElement xE2 = XElement.Parse(XmlNodeResponse.OuterXml);
            Console.WriteLine(xE2);
        }
        static string GetLicense(string FileName)
        {
            string TextLine = string.Empty;

            if (File.Exists(FileName))
            {
                StreamReader objReader = new StreamReader(FileName);
                while (objReader.Peek() != -1)
                    TextLine = TextLine + objReader.ReadLine();

                if (TextLine != string.Empty && TextLine.Trim().Length > 0)
                    return TextLine.Trim();
                else
                    return "";
            }
            else
                return string.Empty;
        }
        static string GetLicense2(string FileName)
        {
            string TextLine = string.Empty;

            if (File.Exists(FileName))
            {
                StreamReader objReader = new StreamReader(FileName);
                TextLine = objReader.ReadToEnd();

                if (TextLine != string.Empty && TextLine.Trim().Length > 0)
                    return TextLine.Trim();
                else
                    return "";
            }
            else
                return string.Empty;
        }
    }
    public class CertificateValidation : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate cert, WebRequest request, int problem)
        {
            return true;
        }
    }
}
