﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NSConfig;
using System.IO;
namespace YardiData
{
    public class YardiData
    {
        private string FileName { get; set; }
        private string RawFolder { get; set; }
        private string CollFolder { get; set; }
        private string CollFalseFolder { get; set; }
        private cNSConfig ccfg = new cNSConfig();
        private XDocument doc = new XDocument();
        private XDocument _cli;


        private Dictionary<string, string> dictSumm = null;
        private Dictionary<string, string> dictProp = null;
        private List<Dictionary<string, string>> dictLeases = null;
        private List<Dictionary<string, string>> dictFiletrans = null;
        private List<Dictionary<string, string>> dictTenants = null;

        private Dictionary<string, string> dictLease = null;
        private Dictionary<string, string> dictFiletran = null;
        private Dictionary<string, string> dictTenant = null;

        private StreamWriter writer;
        private StreamWriter writerFalse;
        public string foutColl { get; set; }
        public string foutCollFalse { get; set; }
        public bool OutputHeader { get; set; }
        private int cntFalseCount = 0;
        public int CollStatusFalseCount
        {
            get { return cntFalseCount; }
        }

        IEnumerable<XElement> dateElements;
        public YardiData(string rawfolder, string collFolder, string collFalseFolder)
        {
            RawFolder = rawfolder;
            CollFolder = collFolder;
            CollFalseFolder = collFalseFolder;
            OutputHeader = false;
            try
            {
                _cli = ccfg.GetConfig("DataElements.xml");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Extract(string fname)
        {
            FileName = fname;

            foutColl = Path.GetFileName(fname);
            foutColl = Path.ChangeExtension(foutColl, "csv");
            foutColl = Path.Combine(CollFolder, foutColl);
            foutCollFalse = Path.GetFileName(fname);
            foutCollFalse = Path.ChangeExtension(foutCollFalse, "csv");
            foutCollFalse = Path.Combine(CollFalseFolder, foutCollFalse);

            try
            {
                doc = XDocument.Load(fname);
                writer = new StreamWriter(foutColl);
                writerFalse = new StreamWriter(foutCollFalse);
                ExtractData(doc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    DisposeFile(writer);
                if (writerFalse != null)
                    DisposeFile(writerFalse);
                if (cntFalseCount == 0)
                    File.Delete(foutCollFalse);
            }

        }
        private void DisposeFile(StreamWriter writer)
        {
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }
        private void ExtractData(XDocument cli)
        {
            string tempStr;

            int tenantCnt = 0;
            int ftransCnt = 0;
            XElement summ = cli.Descendants("Summary").FirstOrDefault();
            if (summ == null)
            {
                throw new Exception("Summary element not found");
            }

            dictSumm = ExtractSummary(summ);
            XElement propfiles = cli.Descendants("PropertyFiles").FirstOrDefault();
            foreach (XElement prop in propfiles.Descendants("PropertyFile").Descendants("Property"))
            {
                dictProp = ExtractPropertyInfo(prop);

            }

            WriteSummary(writer);

            tempStr = string.Empty;
            cntFalseCount = 0;
            bool collStatusFalse = false;
            foreach (XElement lease in propfiles.Descendants("LeaseFile"))
            {
                collStatusFalse = false;
                //Console.WriteLine(ExtractLeaseInfo(lease));
                dictLease = ExtractLeaseInfo(lease);
                //dictLeases.Add(dictLease);
                if (dictLease["CollectionStatus"] == "false")
                {
                    cntFalseCount++;
                    collStatusFalse = true;
                }
                ftransCnt = lease.Descendants("FileTransactions").Count();
                dictFiletrans = new List<Dictionary<string, string>>();

                if (ftransCnt > 0)
                {
                    int cnt = 0;
                    //if (ftransCnt > 10)
                    //    WriteMessage("File Transactions count > 10; only importing 10");
                    Dictionary<string, string> dictItem = null;
                    FileTransactions fTrans = new FileTransactions();
                    fTrans.GetIdValues(lease);
                    if (fTrans != null && fTrans.IdValues.Count > 0)
                    {
                        foreach (var x in fTrans.IdValues)
                        {
                            cnt++;
                            if (cnt > 10) break;
                            dictItem = new Dictionary<string, string>();
                            if (cnt == 1)
                                dictItem = ExtractFtrans(fTrans, x.Key, true);
                            else
                                dictItem = ExtractFtrans(fTrans, x.Key);
                            dictFiletrans.Add(dictItem);
                        }
                    }
                }

                if (dictFiletrans.Count < 10)
                    AddBlankTransactions(dictFiletrans);

                tenantCnt = lease.Descendants("Tenants").Count();
                dictTenants = new List<Dictionary<string, string>>();
                if (tenantCnt > 0)
                {
                    int cnt = 0;
                    //if (tenantCnt > 3)
                    //    WriteMessage("Tenant count > 3; only importing 3");
                    foreach (XElement tenant in lease.Descendants("Tenants"))
                    {
                        cnt++;
                        if (cnt > 3) break;
                        if (cnt == 1)
                            dictTenants.Add(ExtractTenantInfo(tenant, true));  // primary tenant
                        else 
                            dictTenants.Add(ExtractTenantInfo(tenant));
                    }

                    if (dictTenants.Count > 1)
                        ReplicateAddress();
                }
                if (dictTenants.Count < 3)
                    AddBlankTenants(dictTenants);

                //if (tenantCnt > 0 && ftransCnt > 0)
                //{
                    if (!collStatusFalse)
                        WriteLease(writer);
                    else
                        WriteLease(writerFalse);
                //}
            }

        }

        private void ReplicateAddress()
        {
            var firstItem = dictTenants.First();
            IEnumerable<XElement> addrFields = GetAddressElements("Tenants");
            for (int ix = 1; ix < dictTenants.Count; ix++)
            {
                foreach (var x in addrFields)
                {
                    dictTenants[ix][x.Attribute("outputname").Value] = firstItem[x.Attribute("outputname").Value];
                }
            }
        }
        private void AddBlankTenants(List<Dictionary<string, string>> dictTenants)
        {
            if (dictTenants.Count == 0) return;
            Dictionary<string, string> dict;
            int toAdd = (3 - dictTenants.Count);

            for (int x = 0; x < toAdd; x++)
            {
                dict = new Dictionary<string, string>();
                foreach (var itm in dictTenants[0].Keys)
                    if (!(itm.StartsWith("Contact") || (itm.StartsWith("Other"))))
                        dict[itm] = string.Empty;
                dictTenants.Add(dict);
            }
        }

        private void AddBlankTransactions(List<Dictionary<string, string>> dictFiletrans)
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

        private void WriteMessage(string p)
        {
            Console.WriteLine(p);
        }

        private void WriteLease(StreamWriter writer)
        {
            string tempStr = string.Empty;
            Dictionary<string, string>[] v = new Dictionary<string, string>[14];
            for (int ix = 0; ix < v.Count(); ix++)
            {
                if (ix == 0) 
                    v[ix] = dictLease;
                else if (ix >= 1 && ix <= 3 && dictTenants.Count > 0)
                    {
                        if (dictTenants != null) v[ix] = dictTenants[ix - 1];
                    }
                    else if (ix > 3 && dictFiletrans.Count > 0)
                    {
                        if (dictFiletrans != null) v[ix] = dictFiletrans[ix - 4];
                    }
            }
            if (OutputHeader)
            {
                tempStr = GetString(true, v);
                writer.WriteLine(tempStr);
            }
            tempStr = GetString(false, v);
            writer.WriteLine(tempStr);
        }
        private void WriteSummary(StreamWriter writer)
        {
            string tempStr;
            // Write Summary + prop combined
            tempStr = string.Empty;
            if (OutputHeader)
            {
                tempStr = GetString(true, new Dictionary<string, string>[] { dictProp, dictSumm });
                writer.WriteLine(tempStr);
            }
            tempStr = GetString(false, new Dictionary<string, string>[] { dictProp, dictSumm });
            writer.WriteLine(tempStr);
        }

        private string GetString(bool hdr, Dictionary<string, string>[] dict)
        {
            List<string> st = new List<string>();
            string ret = string.Empty;
            for (int ix = 0; ix < dict.Count(); ix++)
            {
                if (dict[ix] == null) break;
                foreach (var x in dict[ix].Keys)
                {
                    if (!hdr)
                    { if (dict[ix] != null) st.Add(dict[ix][x]); }
                    else st.Add(x);
                }
            }
            return string.Join("|", st);
        }

        private Dictionary<string, string> ExtractTenantInfo(XElement tenant, bool primaryTenant = false)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("Tenants");
            if (temp == null)
                return st;
            // gather up phone numbers
            PhoneNumbers ph = new PhoneNumbers();
            if (tenant.Descendants("PersonDetails") != null)
                ph.GetIdValues(tenant.Descendants("PersonDetails").FirstOrDefault());
            // gather up addresses
            Addresses addr = new Addresses();
            if (tenant.Descendants("PersonDetails") != null)
                addr.GetIdValues(tenant.Descendants("PersonDetails").FirstOrDefault());

            TenantCustoms tc = new TenantCustoms();
            if (tenant.Descendants("CustomRecords") != null)
                tc.GetIdValues(tenant);
            bool isDate = false;
            foreach (XElement de in temp)
            {
                if (de.Attribute("date") != null && de.Attribute("date").Value == "1")
                    isDate = true;
                else isDate = false;

                if (de.Attribute("phonetype") != null)
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
                    { if (!isDate) st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, tenant));
                    else st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, tenant).Replace("-",""));
                    }
                    else if (!primaryTenant)
                    {
                        if (!de.Attribute("location").Value.StartsWith("Contact/"))
                            st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, tenant));
                    }
            }
            return st;
        }
        private string ExtractTenantInfoHdr()
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

        private IEnumerable<XElement> GetElements(string section)
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

        private IEnumerable<XElement> GetDateElements(string section)
        {
            XDocument decfg = XDocument.Load("dataelements.xml");

            XElement deSumm = decfg.Descendants(section).FirstOrDefault();
            if (deSumm == null)
                return null;
            var temp = from a in deSumm.Descendants("dataelement")
                       where a.Attribute("enabled").Value == "1" && a.Attribute("date").Value == "1"
                       select a;
            return temp;
        }
        private IEnumerable<XElement> GetAddressElements(string section)
        {
            XDocument decfg = XDocument.Load("dataelements.xml");

            XElement deSumm = decfg.Descendants(section).FirstOrDefault();
            if (deSumm == null)
                return null;
            var temp = from a in deSumm.Descendants("dataelement")
                       where a.Attribute("enabled").Value == "1" && a.Attribute("location").Value.Contains("PersonDetails/Address/")
                                && (! a.Attribute("location").Value.Contains("PersonDetails/Address/Email"))
                       select a;
            return temp;
        }
        private Dictionary<string, string> ExtractSummary(XElement summ)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("Summary");
            if (temp == null)
                return st;
            foreach (XElement de in temp)
            {

                if (de.Attribute("date") != null && de.Attribute("date").Value == "1")
                {
                    st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, summ).Replace("-",""));
                }
                else
                    st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, summ));
            }
            return st;
        }
        private string ExtractSummaryHdr()
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

        private Dictionary<string, string> ExtractPropertyInfo(XElement prop)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("Property");
            if (temp == null)
                return st;
            foreach (XElement de in temp)
            {
                if (de.Attribute("date") != null && de.Attribute("date").Value == "1")
                {
                    st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value.Replace("-", ""), prop));
                }
                else
                    st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, prop));
            }
            return st;
        }
        private string ExtractPropertyInfoHdr()
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

        private Dictionary<string, string> ExtractLeaseInfo(XElement lease)
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
                else
                {
                    if (de.Attribute("date") != null && de.Attribute("date").Value == "1")
                        st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, lease).Replace("-", ""));
                    else
                        st.Add(de.Attribute("outputname").Value, GetItem(de.Attribute("location").Value, lease));
                }
            }
            return st;
        }

        private Dictionary<string, string> ExtractFtrans(FileTransactions fTrans, string transid, bool firstTran = false)
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
                    if (firstTran) st.Add(f.Attribute("location").Value, fTrans.AssignedAmount);
                if (f.Attribute("location").Value == "Identification")
                    st.Add(f.Attribute("location").Value, x.TransId);
                if (f.Attribute("location").Value == "TransDate")
                    st.Add(f.Attribute("location").Value, x.TransDate);
                if (f.Attribute("location").Value == "TransType")
                    st.Add(f.Attribute("location").Value, x.TransType);
                if (f.Attribute("location").Value == "TransTypeDesc")
                    st.Add(f.Attribute("location").Value, x.TransTypeDesc);
                if (f.Attribute("location").Value == "TransAmount")
                    st.Add(f.Attribute("location").Value, x.TransAmount);
                if (f.Attribute("location").Value == "OpenAmount")
                    st.Add(f.Attribute("location").Value, x.OpenAmount);
                if (f.Attribute("location").Value == "CustChargeCode")
                    st.Add(f.Attribute("location").Value, x.CustChargeCode);
                if (f.Attribute("location").Value == "CustChargeCodeDesc")
                    st.Add(f.Attribute("location").Value, x.CustChargeCodeDesc);
            }
            return st;
        }

        private string ExtractFtransHdr(XElement lease)
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
        private string ExtractLeaseInfoHdr()
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
    }
}