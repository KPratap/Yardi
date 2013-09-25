using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NSConfig;
using System.IO;

namespace YardiDataTest
{
    class Program
    {
        static cNSConfig ccfg = new cNSConfig(); 
        static XDocument doc = new XDocument();
        static XDocument _cli;
        static StreamWriter writer;

        static Dictionary<string, string> dictSumm = null;
        static Dictionary<string, string> dictProp = null;
        static List<Dictionary<string, string>> dictLeases = null;
        static List<Dictionary<string, string>> dictFiletrans = null;
        static List<Dictionary<string, string>> dictTenants = null;

        static Dictionary<string, string> dictLease = null;
        static Dictionary<string, string> dictFiletran = null;
        static Dictionary<string, string> dictTenant = null;
        static string FileName;
        static string RawFolder;
        static string CollFolder;
        static string CollFalseFolder;
        static void Main(string[] args)
        {

            _cli = ccfg.GetConfig("DataElements.xml");
           
            //List<DateElement> delist = LoadDataElements();
            string fname = "rentrec1_11232012_0815.xml"; // "rentrec1_11112012_0651.xml";
            //string fname = "Collections Sample.xml";
            
            doc = XDocument.Load(fname);//;("Collections Sample.xml");
            using (writer = new StreamWriter((fname+ ".csv")))
            {
                ExtractData(doc);
            }
            
       }
        private static void Initialize(string fname, string rawfolder, string collFolder, string collFalseFolder)
        {
            FileName = fname;
            RawFolder = rawfolder;
            CollFolder = collFolder;
            CollFalseFolder = collFalseFolder;
        }
        private static void ExtractData(XDocument cli)
        {
            string tempStr;
            XElement summ = cli.Descendants("Summary").FirstOrDefault();
            if (summ == null)
            {
                Console.WriteLine("Summary element not found");
                return;
            }

            dictSumm = ExtractSummary(summ);
            XElement propfiles = cli.Descendants("PropertyFiles").FirstOrDefault();
            foreach (XElement prop in propfiles.Descendants("PropertyFile").Descendants("Property"))
            {
                dictProp = ExtractPropertyInfo(prop);

            }

            WriteSummary(writer);

            tempStr = string.Empty;
            foreach (XElement lease in propfiles.Descendants("LeaseFile"))
            {
                //Console.WriteLine(ExtractLeaseInfo(lease));
                dictLease = ExtractLeaseInfo(lease);
                //dictLeases.Add(dictLease);
                int ftransCnt = lease.Descendants("FileTransactions").Count();
                if (ftransCnt > 0)
                {
                    int cnt = 0;
                    if (ftransCnt > 10)
                        WriteMessage("File Transactions count > 10; only importing 10");
                    Dictionary<string, string> dictItem = null;
                    FileTransactions fTrans = new FileTransactions();
                    fTrans.GetIdValues(lease);
                    if (fTrans != null && fTrans.IdValues.Count > 0)
                    {
                        dictFiletrans = new List<Dictionary<string, string>>();
                        foreach (var x in fTrans.IdValues)
                        {
                            cnt++;
                            if (cnt > 10) break;
                            dictItem = new Dictionary<string, string>();
                            if (cnt == 1)
                                dictItem = ExtractFtrans(fTrans, x.Key,true);
                            else
                                dictItem = ExtractFtrans(fTrans, x.Key);
                            dictFiletrans.Add(dictItem);
                        }
                    }
                    if (dictFiletrans.Count < 10)
                        AddBlankTransactions(dictFiletrans);
                }
                int tenantCnt = lease.Descendants("Tenants").Count();
                if (tenantCnt > 0)
                {
                    int cnt = 0;
                    if (tenantCnt > 3)
                        WriteMessage("Tenant count > 3; only importing 3");
                    dictTenants = new List<Dictionary<string, string>>();
                    foreach (XElement tenant in lease.Descendants("Tenants"))
                    {
                        cnt++;
                        if (cnt > 3) break;
                        if (cnt == 1)
                            dictTenants.Add( ExtractTenantInfo(tenant,true));
                        else dictTenants.Add(ExtractTenantInfo(tenant));
                    }
                    if (dictTenants.Count < 3)
                        AddBlankTenants(dictTenants);
                }
            }
            WriteLease(writer);
        }

        private static void AddBlankTenants(List<Dictionary<string, string>> dictTenants)
        {
            if (dictTenants.Count == 0) return;
            Dictionary<string, string> dict;
            int toAdd = (3 - dictTenants.Count);

            for (int x = 0; x < toAdd; x++)
            {
                dict = new Dictionary<string,string>();
                foreach (var itm in dictTenants[0].Keys)
                    if (!(itm.StartsWith("Contact") || (itm.StartsWith("Other"))) )
                        dict[itm] = string.Empty;
                dictTenants.Add(dict);
            }
        }

        private static void AddBlankTransactions(List<Dictionary<string, string>> dictFiletrans)
        {
            if (dictFiletrans.Count == 0) return;
            Dictionary<string, string> dict;
            int toAdd = (10 - dictFiletrans.Count);

            for (int x = 0; x < toAdd; x++)
            {
                dict = new Dictionary<string, string>();
                foreach (var itm in dictFiletrans[0].Keys) 
                    if (itm != "AssignedAmount") 
                        dict[itm] = string.Empty;
                dictFiletrans.Add(dict);
            }
        }

        private static void WriteMessage(string p)
        {
            Console.WriteLine(p);
        }

        private static void WriteLease(StreamWriter writer)
        {
            string tempStr = string.Empty;
            Dictionary<string, string>[] v = new Dictionary<string, string>[14];
            for (int ix = 0; ix < v.Count(); ix++)
            {
                if (ix == 0) v[ix] = dictLease;
                else if (ix >=1 && ix <= 3)
                    v[ix] = dictTenants[ix-1];
                else if (ix > 3)
                    v[ix] = dictFiletrans[ix-4];
            }
            tempStr = GetString(true, v);
            writer.WriteLine(tempStr);
            tempStr = GetString(false, v);
            writer.WriteLine(tempStr);
        }
        private static void WriteSummary(StreamWriter writer)
        {
            string tempStr;
            // Write Summary + prop combined
            tempStr = string.Empty;
            tempStr = GetString(true, new Dictionary<string, string>[] { dictProp, dictSumm });
            writer.WriteLine(tempStr);
            tempStr = GetString(false, new Dictionary<string, string>[] { dictProp, dictSumm });
            writer.WriteLine(tempStr);
        }
        private static string GetString(Dictionary<string, string> dict, bool hdr)
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            foreach (var x in dict.Keys)
            {
                if (! hdr)
                    st.Add(dict[x]);
                else st.Add(x);
            }
            return string.Join("|", st);
        }

        private static string GetString(bool hdr, Dictionary<string, string>[] dict)
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            for (int ix=0; ix < dict.Count(); ix++)
            {
                foreach (var x in dict[ix].Keys)
                {
                    if (!hdr)
                        st.Add(dict[ix][x]);
                    else st.Add(x);
                }
            }
            return string.Join("|", st);
        }

        private static Dictionary<string, string> ExtractTenantInfo(XElement tenant, bool primaryTenant=false)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("Tenants");
            if (temp == null)
                return st;
            // gather up phone numbers
            PhoneNumbers ph = new PhoneNumbers();
            if (tenant.Descendants("PersonDetails")!= null)
                ph.GetIdValues(tenant.Descendants("PersonDetails").FirstOrDefault());
            // gather up addresses
            Addresses addr = new Addresses();
            if (tenant.Descendants("PersonDetails") != null)
                addr.GetIdValues(tenant.Descendants("PersonDetails").FirstOrDefault());

            TenantCustoms tc = new TenantCustoms();
            if (tenant.Descendants("CustomRecords") != null)
                tc.GetIdValues(tenant);

            foreach (XElement de in temp)
            {
                if (de.Attribute("phonetype") != null )
                {
                    if (ph.IdValues.ContainsKey(de.Attribute("phonetype").Value))
                        st.Add(de.Attribute("outputname").Value, ph.IdValues[de.Attribute("phonetype").Value]);
                    else
                        st.Add(de.Attribute("outputname").Value, string.Empty);
                }
                else
                    if (de.Attribute("addresstype") != null)
                    {
                        if (addr.IdValues.ContainsKey(de.Attribute("addresstype").Value))
                        {
                            if (de.Attribute("location").Value.Contains("Address/Address"))
                                st.Add(de.Attribute("outputname").Value, addr.IdValues[de.Attribute("addresstype").Value].Street);
                            if (de.Attribute("location").Value.Contains("Address/City"))
                                st.Add(de.Attribute("outputname").Value, addr.IdValues[de.Attribute("addresstype").Value].City);
                            if (de.Attribute("location").Value.Contains("Address/State"))
                                st.Add(de.Attribute("outputname").Value, addr.IdValues[de.Attribute("addresstype").Value].State);
                            if (de.Attribute("location").Value.Contains("Address/PostalCode"))
                                st.Add(de.Attribute("outputname").Value, addr.IdValues[de.Attribute("addresstype").Value].PostalCode);
                            if (de.Attribute("location").Value.Contains("Address/Email"))
                                st.Add(de.Attribute("outputname").Value, addr.IdValues[de.Attribute("addresstype").Value].Email);
                        }
                        else
                        {
                            if (de.Attribute("location").Value.Contains("Address/Address"))
                                st.Add(de.Attribute("outputname").Value, string.Empty);
                            if (de.Attribute("location").Value.Contains("Address/City"))
                                st.Add(de.Attribute("outputname").Value, string.Empty);
                            if (de.Attribute("location").Value.Contains("Address/State"))
                                st.Add(de.Attribute("outputname").Value, string.Empty);
                            if (de.Attribute("location").Value.Contains("Address/PostalCode"))
                                st.Add(de.Attribute("outputname").Value, string.Empty);
                            if (de.Attribute("location").Value.Contains("Address/Email"))
                                st.Add(de.Attribute("outputname").Value, string.Empty);
                        }
                    }
                    else if (de.Attribute("location").Value.Contains("CustomRecords/Other"))
                    {
                        if (de.Attribute("location").Value == "CustomRecords/Other1")
                            if (primaryTenant) st.Add(de.Attribute("outputname").Value, tc.IdValues.Other1Val);
                        if (de.Attribute("location").Value == "CustomRecords/Other2")
                            if (primaryTenant) st.Add(de.Attribute("outputname").Value, tc.IdValues.Other2Val);
                        if (de.Attribute("location").Value == "CustomRecords/Other3")
                            if (primaryTenant) st.Add(de.Attribute("outputname").Value, tc.IdValues.Other3Val);
                        if (de.Attribute("location").Value == "CustomRecords/Other4")
                            if (primaryTenant) st.Add(de.Attribute("outputname").Value, tc.IdValues.Other4Val);
                    }
                    else if (primaryTenant)
                            st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, tenant));
                    else if (!primaryTenant)
                    {
                        if (!de.Attribute("location").Value.StartsWith("Contact/"))
                            st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, tenant));
                    }
            }
            return st;
        }
        private static string ExtractTenantInfoHdr()
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("Tenants");
            if (temp == null)
                return ret;
            st.Add("TENANT");
            foreach (XElement de in temp)
            {
                st.Add(de.Attribute("outputname").Value);
            }
            return string.Join("|", st);
        }

        private static IEnumerable<XElement>  GetElements(string section)
        {
            XDocument decfg = XDocument.Load("dataelements.xml");

            XElement deSumm = decfg.Descendants(section).FirstOrDefault();
            if (deSumm == null)
                return null;
            var temp = from a in deSumm.Descendants("dataelement")
                       where a.Attribute("enabled").Value == "1"
                       select a;
            return temp;
        }

        private static Dictionary<string,string> ExtractSummary(XElement summ)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("Summary");
            if (temp == null)
                return st;
            foreach (XElement de in temp)
            {
                st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, summ));
            }
            return st;
        }
        private static string ExtractSummaryHdr()
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("Summary");
            if (temp == null)
                return ret;
            st.Add("SUMMARY");
            foreach (XElement de in temp)
            {
                st.Add(de.Attribute("outputname").Value);
            }
            return string.Join("|", st);
        }
        //private static void ExtractPropertyInfo(XElement prop)
        //{
        //    Console.WriteLine(">>Property");
        //    XDocument decfg = XDocument.Load("dataelements.xml");

        //    XElement deSumm = decfg.Descendants("Property").FirstOrDefault();
        //    if (deSumm == null)
        //        return;
        //    foreach (XElement de in deSumm.Descendants("dataelement"))
        //    {
        //        Console.WriteLine("{0}={1}", de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, prop));
        //    }
        //}

        private static Dictionary<string,string> ExtractPropertyInfo(XElement prop)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("Property");
            if (temp == null)
                return st;
            foreach (XElement de in temp)
            {
                st.Add(de.Attribute("outputname").Value,GetItem(de.Attribute("location").Value, prop));
            }
            return st;
        }
        private static string ExtractPropertyInfoHdr()
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("Property");
            if (temp == null)
                return ret;
            st.Add("PROPERTY");
            foreach (XElement de in temp)
            {
                st.Add(de.Attribute("outputname").Value);
            }
            return string.Join("|", st);
        }

        private static Dictionary<string,string> ExtractLeaseInfo(XElement lease)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            //List<string> ftlist = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("LeaseFile");
            if (temp == null)
                return st;

            FileTransactions fTrans = new FileTransactions();
            fTrans.GetIdValues(lease);

            foreach (XElement de in temp)
            {
                Identification idvals = new Identification();
                if (de.Attribute("location").Value == "Identification")
                {
                    idvals.GetIdValues(lease);
                    if (de.Attribute("orgval").Value != string.Empty)
                    {
                        if (idvals.IdValues.ContainsKey(de.Attribute("orgval").Value))
                            st.Add(de.Attribute("outputname").Value, idvals.IdValues[de.Attribute("orgval").Value]);
                    }
                }
                else st.Add(de.Attribute("outputname").Value,GetItem(de.Attribute("location").Value, lease));
            }
            return st;
        }

        private static Dictionary<string,string> ExtractFtrans(FileTransactions fTrans, string transid, bool firstTran=false)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("FileTransactions");
            if (temp == null)
                return st;
            int cnt = 0;
            var x = fTrans.IdValues[transid];
            foreach (var f in temp)
            {
                cnt++;
                if (f.Attribute("location") == null) continue;

                if (f.Attribute("location").Value == "AssignedAmount")
                    if (firstTran) st.Add(f.Attribute("location").Value,fTrans.AssignedAmount);
                if (f.Attribute("location").Value  == "Identification")
                    st.Add(f.Attribute("location").Value, x.TransId);
                if (f.Attribute("location").Value  == "TransDate")
                    st.Add(f.Attribute("location").Value, x.TransDate);
                if (f.Attribute("location").Value  == "TransType")
                    st.Add(f.Attribute("location").Value, x.TransType);
                if (f.Attribute("location").Value  == "TransTypeDesc")
                    st.Add(f.Attribute("location").Value, x.TransTypeDesc);
                if (f.Attribute("location").Value  == "TransAmount")
                    st.Add(f.Attribute("location").Value, x.TransAmount);
                if (f.Attribute("location").Value  == "OpenAmount")
                    st.Add(f.Attribute("location").Value, x.OpenAmount);
                if (f.Attribute("location").Value  == "CustChargeCode")
                    st.Add(f.Attribute("location").Value, x.CustChargeCode);
                if (f.Attribute("location").Value  == "CustChargeCodeDesc")
                    st.Add(f.Attribute("location").Value, x.CustChargeCodeDesc);
            }
            return st;
        }

        private static string ExtractFtransHdr(XElement lease)
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("FileTransactions");
            if (temp == null)
                return ret;
            st.Add("FTRANS");
            foreach (var f in temp)
            {
                if (f.Attribute("location") == null) continue;

                if (f.Attribute("location").Value == "AssignedAmount")
                    st.Add(f.Attribute("outputname").Value);
                if (f.Attribute("location").Value == "Identification")
                    st.Add(f.Attribute("outputname").Value);
                if (f.Attribute("location").Value == "TransDate")
                    st.Add(f.Attribute("outputname").Value);
                if (f.Attribute("location").Value == "TransType")
                    st.Add(f.Attribute("outputname").Value);
                if (f.Attribute("location").Value == "TransTypeDesc")
                    st.Add(f.Attribute("outputname").Value);
                if (f.Attribute("location").Value == "TransAmount")
                    st.Add(f.Attribute("outputname").Value);
                if (f.Attribute("location").Value == "OpenAmount")
                    st.Add(f.Attribute("outputname").Value);
                if (f.Attribute("location").Value == "CustChargeCode")
                    st.Add(f.Attribute("outputname").Value);
                if (f.Attribute("location").Value == "CustChargeCodeDesc")
                    st.Add(f.Attribute("outputname").Value);
            }
            return String.Join("|", st);
        }
        private static string ExtractLeaseInfoHdr()
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("LeaseFile");
            if (temp == null)
                return ret;
            st.Add("LEASE");

            foreach (XElement de in temp)
            {
                st.Add(de.Attribute("outputname").Value);
            }
            return String.Join("|", st);
        }
        /*
        private static List<DateElement> LoadDataElements()
        {
            List<XElement> clients = ccfg.GetElements(_cli, "dataelement");
            List<DateElement> lst = new List<DateElement>();
            foreach (XElement el in clients)
            {

                //Console.WriteLine("{0} {1} {2}"
                //                      , ccfg.GetElementAttrib(el, "location").Value
                //                      , ccfg.GetElementAttrib(el, "outputname").Value
                //                      , ccfg.GetElementAttrib(el, "enabled").Value
                //                      );
                lst.Add(new DateElement(ccfg.GetElementAttrib(el, "location").Value
                                                      , ccfg.GetElementAttrib(el, "outputname").Value
                                                      , ccfg.GetElementAttrib(el, "enabled").Value));

            }
            return lst;
        }
        
        private static List<DateElement> LoadDataElements(string sect)
        {
            List<XElement> clients = ccfg.GetElements(_cli, sect);
            List<DateElement> lst = new List<DateElement>();
            foreach (XElement el in clients)
            {

                //Console.WriteLine("{0} {1} {2}"
                //                      , ccfg.GetElementAttrib(el, "location").Value
                //                      , ccfg.GetElementAttrib(el, "outputname").Value
                //                      , ccfg.GetElementAttrib(el, "enabled").Value
                //                      );
                lst.Add(new DateElement(ccfg.GetElementAttrib(el, "location").Value
                                                      , ccfg.GetElementAttrib(el, "outputname").Value
                                                      , ccfg.GetElementAttrib(el, "enabled").Value));

            }
            return lst;
        }
         
        private static void GetDataElements(string fil)
        {
            XDocument doc;
            doc = XDocument.Load(fil);
            XElement ele = doc.Descendants("dataelement").FirstOrDefault();


        }
        private static void GetDataElements(string fil, string node)
        {
            XDocument doc;
            doc = XDocument.Load(fil);
            XElement ele = doc.Descendants(node).FirstOrDefault();
            //XElement dele = ele.Descendants("dataelement");
        }
         
        static void GetProperties(XElement props)
        {
            foreach (XElement el in props.Descendants("PropertyFile"))
            {
                foreach( XElement elprop in el.Descendants())
                    Console.WriteLine("{0} = {1}", elprop.Name, elprop.Value);
            }
        }
        */
        static void GetSpecificItem(string loc, XElement src)
        {
            string[] items = loc.Split('/');
            XElement e = src.Descendants(items[0]).Descendants(items[1]).Descendants(items[2]).First();

        }
        static string GetItem(string loc, XElement src)
        {
            string[] items = loc.Split('/');
            int level = items.Length;
            string returnstr = string.Empty;
            try
            {
                switch (level)
                {
                    case 1:
                        if (src.Element(items[0]) != null)
                            returnstr = src.Element(items[0]).Value;
                        break;
                    case 2:
                        if (src.Element(items[0]).Element(items[1]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Value;
                        break;
                    case 3:
                        if (src.Element(items[0]).Element(items[1]).Element(items[2]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Element(items[2]).Value;
                        break;
                    case 4:
                        if (src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Value;
                        break;
                    case 5:
                        if (src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Element(items[4]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Element(items[4]).Value;
                        break;
                    case 6:
                        if (src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Element(items[4]).Element(items[5]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Element(items[4]).Element(items[5]).Value;
                        break;
                    default:
                        returnstr = "Nested too Deep";
                        break;
                }
                return returnstr;
            }
            catch
            {
                return returnstr;
            }
        }

        /*
        static string GetItem(string loc, XElement src, string orgname)
        {
            string org = string.Empty;
            if (orgname != string.Empty)
            {
                org = GetOrgname(loc.Replace("IDValue", "OrganizationName"), src);
            }
            string[] items = loc.Split('/');
            int level = items.Length;
            string returnstr = string.Empty;
            try
            {
                switch (level)
                {
                    case 1:
                        if (src.Element(items[0]) != null)
                            returnstr = src.Element(items[0]).Value;
                        break;
                    case 2:
                        if (src.Element(items[0]).Element(items[1]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Value;
                        break;
                    case 3:
                        if (src.Element(items[0]).Element(items[1]).Element(items[2]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Element(items[2]).Value;
                        break;
                    case 4:
                        if (src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Value;
                        break;
                    case 5:
                        if (src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Element(items[4]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Element(items[4]).Value;
                        break;
                    case 6:
                        if (src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Element(items[4]).Element(items[5]) != null)
                            returnstr = src.Element(items[0]).Element(items[1]).Element(items[2]).Element(items[3]).Element(items[4]).Element(items[5]).Value;
                        break;
                    default:
                        returnstr = "Nested too Deep";
                        break;
                }
                if (loc.EndsWith("IDValue") && orgname != string.Empty)
                {
                    if (org == orgname.ToLower())
                        return returnstr;
                    else return string.Empty;
                }
                return returnstr;
            }
            catch
            {
                return returnstr;
            }
        }

        private static string GetOrgname(string p, XElement src)
        {
            return GetItem(p, src).ToLower();
        }
        static void GetSummary(XElement summ)
        {
            foreach (XElement el in summ.Descendants())
            {
                Console.WriteLine("{0} = {1}", el.Name, el.Value);
            }
        }
         * */
    }

}
