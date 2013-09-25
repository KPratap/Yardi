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
            



            //summ = doc.Descendants("PropertyFiles").First();
            //foreach (DateElement de in delist)
            //{
            //     if (de.Enabled == "1")
            //        Console.WriteLine("{0}={1}",de.OutName,GetItem(de.Location, summ));
            //}


            
            //summ =  doc.Descendants("Summary").First();
            //if (summ != null)
            //    GetSummary(summ);
            //else Console.WriteLine("No Summary");
            //summ = null;
            //summ = doc.Descendants("PropertyFiles").First();
            ////GetProperties(summ);
            //GetDataElements("DataElements.xml");

            //GetSpecificItem("PropertyFile/Property/Address", summ);
        }

        private static void ExtractData(XDocument cli)
        {
            XElement summ = cli.Descendants("Summary").FirstOrDefault();
            if (summ == null)
            {
                Console.WriteLine("Summary element not found");
            }

            //Console.WriteLine(ExtractSummary(summ));
            writer.WriteLine(ExtractSummaryHdr());
            writer.WriteLine(ExtractSummary(summ));
            XElement propfiles = cli.Descendants("PropertyFiles").FirstOrDefault();
           // Console.WriteLine("Properties {0}", propfiles.Descendants("PropertyFile").Count());
            foreach (XElement prop in propfiles.Descendants("PropertyFile").Descendants("Property"))
            {
                //Console.WriteLine(ExtractPropertyInfo(prop));
                writer.WriteLine(ExtractPropertyInfoHdr());
                writer.WriteLine(ExtractPropertyInfo(prop));

            }
            foreach (XElement lease in propfiles.Descendants("LeaseFile"))
            {
                //Console.WriteLine(ExtractLeaseInfo(lease));
                writer.WriteLine(ExtractLeaseInfoHdr());
                writer.WriteLine(ExtractLeaseInfo(lease));

                if (lease.Descendants("FileTransactions").Count() > 0)
                    writer.WriteLine(ExtractFtransHdr(lease));

                FileTransactions fTrans = new FileTransactions();
                fTrans.GetIdValues(lease);
                if (fTrans != null && fTrans.IdValues.Count > 0)
                {
                    foreach (var x in fTrans.IdValues)
                    {
                        writer.WriteLine(ExtractFtrans(fTrans,x.Key));
                    }
                }

                if (lease.Descendants("Tenants").Count() > 0)
                    writer.WriteLine(ExtractTenantInfoHdr());

                foreach (XElement tenant in lease.Descendants("Tenants"))
                {
                    //Console.WriteLine(ExtractTenantInfo(tenant));
                    writer.WriteLine(ExtractTenantInfo(tenant));
                }
            }
        }
        private static string ExtractTenantInfo(XElement tenant)
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("Tenants");
            if (temp == null)
                return ret;
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

            st.Add("TENANT");

            foreach (XElement de in temp)
            {
                if (de.Attribute("phonetype") != null )
                {
                    if (ph.IdValues.ContainsKey(de.Attribute("phonetype").Value))
                        st.Add(ph.IdValues[de.Attribute("phonetype").Value]);
                    else
                        st.Add(string.Empty);
                }
                else
                    if (de.Attribute("addresstype") != null)
                    {
                        if (addr.IdValues.ContainsKey(de.Attribute("addresstype").Value))
                        {
                            if (de.Attribute("location").Value.Contains("Address/Address"))
                                st.Add(addr.IdValues[de.Attribute("addresstype").Value].Street);
                            if (de.Attribute("location").Value.Contains("Address/City"))
                                st.Add(addr.IdValues[de.Attribute("addresstype").Value].City);
                            if (de.Attribute("location").Value.Contains("Address/State"))
                                st.Add(addr.IdValues[de.Attribute("addresstype").Value].State);
                            if (de.Attribute("location").Value.Contains("Address/PostalCode"))
                                st.Add(addr.IdValues[de.Attribute("addresstype").Value].PostalCode);
                            if (de.Attribute("location").Value.Contains("Address/Email"))
                                st.Add(addr.IdValues[de.Attribute("addresstype").Value].Email);
                        }
                        else
                        {
                            if (de.Attribute("location").Value.Contains("Address/Address"))
                                st.Add(string.Empty);
                            if (de.Attribute("location").Value.Contains("Address/City"))
                                st.Add(string.Empty);
                            if (de.Attribute("location").Value.Contains("Address/State"))
                                st.Add(string.Empty);
                            if (de.Attribute("location").Value.Contains("Address/PostalCode"))
                                st.Add(string.Empty);
                            if (de.Attribute("location").Value.Contains("Address/Email"))
                                st.Add(string.Empty);
                        }
                    }
                    else if (de.Attribute("location").Value.Contains("CustomRecords/Other"))
                    {
                        if (de.Attribute("location").Value == "CustomRecords/Other1")
                            st.Add(tc.IdValues.Other1Val);
                        if (de.Attribute("location").Value == "CustomRecords/Other2")
                            st.Add(tc.IdValues.Other2Val);
                        if (de.Attribute("location").Value == "CustomRecords/Other3")
                            st.Add(tc.IdValues.Other3Val);
                        if (de.Attribute("location").Value == "CustomRecords/Other4")
                            st.Add(tc.IdValues.Other4Val);
                    }
                    else
                        st.Add(GetItem(de.Attribute("location").Value, tenant));
            }
            return string.Join("|", st);
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

        private static string ExtractSummary(XElement summ)
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("Summary");
            if (temp == null)
                return ret;
            st.Add("SUMMARY");
            foreach (XElement de in temp)
            {
                st.Add(GetItem(de.Attribute("location").Value,summ));
            }
            return string.Join("|", st);
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
        private static string ExtractPropertyInfo(XElement prop)
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("Property");
            if (temp == null)
                return ret;
            st.Add("PROPERTY");
            foreach (XElement de in temp)
            {
               st.Add(GetItem(de.Attribute("location").Value, prop));
            }
            return string.Join("|", st);
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

        private static string ExtractLeaseInfo(XElement lease)
        {
            List<string> st = new List<string>();
            List<string> ftlist = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("LeaseFile");
            if (temp == null)
                return ret;
            st.Add("LEASE");

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
                            st.Add(idvals.IdValues[de.Attribute("orgval").Value]);
                    }
                }
                else st.Add(GetItem(de.Attribute("location").Value, lease));
            }
            return String.Join("|", st);
        }

        private static string ExtractFtrans(FileTransactions fTrans, string transid)
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            var temp = GetElements("FileTransactions");
            if (temp == null)
                return ret;
            st.Add("FTRANS");

            var x = fTrans.IdValues[transid];
            foreach (var f in temp)
            {
                if (f.Attribute("location") == null) continue;

                if (f.Attribute("location").Value == "AssignedAmount")
                    st.Add(fTrans.AssignedAmount);
                if (f.Attribute("location").Value  == "Identification")
                    st.Add(x.TransId);
                if (f.Attribute("location").Value  == "TransDate")
                    st.Add(x.TransDate);
                if (f.Attribute("location").Value  == "TransType")
                    st.Add(x.TransType);
                if (f.Attribute("location").Value  == "TransTypeDesc")
                    st.Add(x.TransTypeDesc);
                if (f.Attribute("location").Value  == "TransAmount")
                    st.Add(x.TransAmount);
                if (f.Attribute("location").Value  == "OpenAmount")
                    st.Add(x.OpenAmount);
                if (f.Attribute("location").Value  == "CustChargeCode")
                    st.Add(x.CustChargeCode);
                if (f.Attribute("location").Value  == "CustChargeCodeDesc")
                    st.Add(x.CustChargeCodeDesc);
            }
            return String.Join("|", st);
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
/*         
    public class DateElement
    {
        public string Location {get;set;}
        public string OutName {get;set;}
        public string Enabled {get;set;}
        public DateElement(string loc, string oname, string enabled)
        {
            Location = loc;
            OutName = oname;
            Enabled = enabled;
        }

    }
 * */
}
