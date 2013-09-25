using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml;
namespace YardiInterface
{
    public class YardiInterface
    {
        public string LicenseFile { get; set; }
        public string URL { get; set; }
        public string User{ get; set;}
        public string Pwd { get; set; }
        public string Database { get; set; }
        public string Server { get; set; }
        public string Platform { get; set; }
        public string License { get; set; }
        public string EntityName { get; set; }
        public string YardiPropId { get; set; }
        

        public string ClientVersion{get;set;}
        private string licStr;
        private const string verThresh = "16_0";
        private const string _entname = "Rent Recovery Solutions";
        private bool needLic = false;

        public YardiInterface(string url, string licFile)
        {
            URL = url;
            EntityName = _entname;
            if (licFile == string.Empty)
                return;
            LicenseFile = licFile;
            try
            {
                licStr = GetLicense(licFile);
                ClientVersion = this.GetVersion(url);
                needLic = this.NeedLicense(ClientVersion, verThresh);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetVersion(string url)
        {
            YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
            try
            {
                s.Url = url;
                string XmlNodeResponse;
                XmlNodeResponse = s.GetVersionNumber();
                return XmlNodeResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private string GetLicense(string FileName)
        //{
        //    string TextLine = string.Empty;
        //    if (!File.Exists(FileName))
        //        return string.Empty;
        //    StreamReader objReader = new StreamReader(FileName);
        //    while (objReader.Peek() != -1)
        //        TextLine = TextLine + objReader.ReadLine();

        //    if (TextLine != string.Empty && TextLine.Trim().Length > 0)
        //        return TextLine.Trim();
        //    else
        //        return "";
        //}
        //public Xelement  GetCollectionsLeaseInfo(string prop)
        //{
        //    YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
        //    s.Url = URL;
        //    string lic = GetLicense2("V100055004.lic");
        //    XmlNode XmlNodeResponse;
        //    XmlNodeResponse = s.Get_CollectionsLeaseInfo(
        //        "rentrecoveryws",
        //        "55004",
        //        "aspdb04",
        //        "afqoml_live",
        //        "SQL Server",
        //        "Rent Recovery Solutions",
        //        lic,
        //        prop
        //       );
        //    XElement xE2 = XElement.Parse(XmlNodeResponse.OuterXml);
        //    xE2.Save(prop + "_" + DateTime.Now.ToString("MMddyyyy_hhmm") + ".xml");
        //    Console.WriteLine(xE2);
        //}
        //static void Getproperties()
        //{
        //    YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
        //    s.CookieContainer = new System.Net.CookieContainer();
        //    ServicePointManager.CertificatePolicy = new CertificateValidation();
        //    s.Url = "https://www.iyardiasp.com/8223thirddev/Webservices/itfcollections.asmx";
        //    string lic = GetLicense("V100055004.lic");
        //    XmlNode XmlNodeResponse;
        //    XmlNodeResponse = s.GetPropertyConfigurations(
        //        "rentrecoveryws",
        //        "55004",
        //        "aspdb04",
        //        "afqoml_live",
        //        "SQL Server",
        //        "Rent Recovery Solutions",
        //        lic
        //       );
        //    XElement xE2 = XElement.Parse(XmlNodeResponse.OuterXml);
        //    Console.WriteLine(xE2);
        //}
        public XElement GetCollectionsLeaseInfo(string prop)
        {
            XElement xE2 = null;
            YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
            XmlNode XmlNodeResponse;

            s.Url = this.URL;
            try
            {
                //if (!needLic)
                //{
                //    XmlNodeResponse = s.Get_CollectionsLeaseInfo(
                //       this.User,
                //       this.Pwd,
                //       this.Database,
                //       this.Server,
                //       this.Platform,
                //       this.EntityName,
                //       prop
                //       );
                //}
                //else
                //{
                XmlNodeResponse = s.Get_CollectionsLeaseInfo(
                   this.User,
                   this.Pwd,
                   this.Server,
                   this.Database,
                   this.Platform,
                   this.EntityName,
                    licStr,
                    prop
                   );
                //}

                    if (xE2 != null)
                    {
                        xE2 = XElement.Parse(XmlNodeResponse.OuterXml);
                        return xE2;
                    }
                    else
                    {
                        throw new Exception("Response is null");
                    }

            }
            catch (Exception ex)
            {
                throw ex;
            }
           // xE2.Save(prop + "_" + DateTime.Now.ToString("MMddyyyy_hhmm") + ".xml");
        }
        public XElement Getproperties()
        {
         
            YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
            XmlNode XmlNodeResponse;
            try
            {
                //s.CookieContainer = new System.Net.CookieContainer();
                s.Url = this.URL;

                //ServicePointManager.CertificatePolicy = new CertificateValidation();
                //if (!needLic)
                //{
                //    XmlNodeResponse = s.GetPropertyConfigurations(
                //       this.User,
                //       this.Pwd,
                //       this.Database,
                //       this.Server,
                //       this.Platform,
                //       this.EntityName
                //       );
                //}
                //else
                //{
                XmlNodeResponse = s.GetPropertyConfigurations(
                   this.User,
                   this.Pwd,
                   this.Server,
                   this.Database,
                   this.Platform,
                   this.EntityName,
                    licStr
                   );
                // }
                if (XmlNodeResponse != null)
                {
                    XElement xE2 = XElement.Parse(XmlNodeResponse.OuterXml);
                    return xE2;
                }
                else
                {
                    throw new Exception("Response is null");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool NeedLicense(string cli, string thresh)
        {
            if (string.Compare(cli, thresh) >= 0)
                return true;
            else return false;
        }
        static string GetLicense(string FileName)
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
}
