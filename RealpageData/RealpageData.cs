using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using log4net.Config;
using NSConfig;

namespace RealpageData
{
    public class RealpageData
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
        public string DecryptKey { get; set; }

        public List<Resident> TenantList { get; set; }
        private int cntFalseCount = 0;
        private decimal AssignedAmount = 0;
        public int CollStatusFalseCount
        {
            get { return cntFalseCount; }
        }
        private int ftransMaxCount = 0;
        private int tenantMaxCnt = 0;
        private bool debugFormat = false;
        private string dataElementFile = "rpdataelements.xml";

        private string _NTVOnDate;
        private string _NTVForDate;


        //IEnumerable<XElement> dateElements;
        public RealpageData(string rawfolder, string collFolder, string collFalseFolder)
        {
            RawFolder = rawfolder;
            CollFolder = collFolder;
            CollFalseFolder = collFalseFolder;
            OutputHeader = false;
            try
            {
                _cli = ccfg.GetConfig(dataElementFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Extract(string fname, bool dbg = false)
        {
            debugFormat = dbg;
            FileName = fname;
            foutColl = Path.GetFileName(fname);
            foutColl = Path.ChangeExtension(foutColl, "csv");
            foutColl = Path.Combine(CollFolder, foutColl);
            foutCollFalse = Path.GetFileName(fname);
            foutCollFalse = Path.ChangeExtension(foutCollFalse, "csv");
            foutCollFalse = Path.Combine(CollFalseFolder, foutCollFalse);
            Log.Info("begin Extract");
            try
            {
                doc = XDocument.Load(fname);
                writer = new StreamWriter(foutColl);
                writerFalse = new StreamWriter(foutCollFalse);
                ExtractData(doc);
                ArchiveRawFile(fname);

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

        private void ArchiveRawFile(string fname)
        {
            var fullpath = Path.GetDirectoryName(fname);
            var fname_only = Path.GetFileName(fname);
            string new_name = GetArchiveFilename(fname_only);
            new_name = Path.Combine(fullpath, new_name);
            try
            {
                File.Move(fname, new_name);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Unable to rename raw file {0} to {1}\n {2}",fname, new_name, ex));
            }
    }

        private string GetArchiveFilename(string fname)
        {
            string fout = string.Empty;
            var fname_noext = Path.GetFileNameWithoutExtension(fname);
            var fname_ext = Path.GetExtension(fname);
            Match match = Regex.Match(fname, @"([_A-Za-z0-9\-]+)(_Archive_)([0-9]+)(\.xml)$", RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                fout = fname_noext + "_archive_1" + fname_ext;
                return fout;
            }
            var aval = Convert.ToInt32(match.Groups[3].Value);
            aval++;
            fout = string.Concat(match.Groups[1], match.Groups[2], aval.ToString(), match.Groups[4]);
            return fout;
        }
        private void DisposeFile(StreamWriter writer)
        {
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }
        private void ExtractData(XDocument cli)
        {

            int tenantCnt = 0;
            int ftransCnt = 0;
            XElement leases = cli.Descendants("Collections").FirstOrDefault().Descendants("LeaseList").FirstOrDefault();
            if (leases == null)
            {
                throw new Exception("FileContents/Collections/LeaseList element not found");
            }
            var leaseList = DeserializeNode<LeaseList>(leases);


            // Validation
            XElement summ = cli.Descendants("Collections").FirstOrDefault().Descendants("Validation").FirstOrDefault();
            if (summ == null)
            {
                throw new Exception("FileContents/Collections/Validation element not found");
            }
            dictSumm = ExtractSummary(summ, leaseList.Leases);

            XElement sites = cli.Descendants("SiteList").FirstOrDefault();
            if (sites == null)
            {
                throw new Exception("FileContents/Collections/SiteList element not found");
            }

            var siteList = DeserializeNode<SiteList>(sites);

            XElement units = cli.Descendants("UnitList").FirstOrDefault();
            //if (units == null)
            //{
            //    throw new Exception("FileContents/Collections/UnitList element not found");
            //}

            var unitList = DeserializeNode<UnitList>(units).Units;


            var thisSite = siteList.Sites[0].SiteID;
            
            dictProp = ExtractPropertyInfo(siteList.Sites[0]);

            // Residents
            XElement residents = cli.Descendants("Collections").FirstOrDefault().Descendants("ResidentList").FirstOrDefault();
            if (residents == null)
            {
                throw new Exception("FileContents/Collections/ResidentList element not found");
            }
            var residentList = DeserializeNode<ResidentList>(residents);

            XElement opencharges = cli.Descendants("Collections").FirstOrDefault().Descendants("OpenChargesList").FirstOrDefault();
            if (opencharges == null)
            {
                throw new Exception("FileContents/Collections/OpenChargesList element not found");
            }
            var openchargesList = DeserializeNode<OpenChargesList>(opencharges);
            
            WriteSummary(writer);

            //if (dictFiletrans.Count < ftransMaxCount)
            //    AddBlankTransactions(dictFiletrans, ftransMaxCount);



            bool collStatus = false;
            cntFalseCount = 0;

            IEnumerable<Resident> leasetenants = null;
            IEnumerable<OpenCharges> leasecharges = null;
            /*
            TenantList =
                from lea in leaseList.Leases
                from res in residentList.Residents
                where lea.LeaID == res.LeaID &&
                    lea.SiteID == res.SiteID &&
                    lea.UnitID == res.UnitID &&
                    lea.ReshID == res.ReshID
                select res;
            */
            TenantList = new List<Resident>();
            foreach (var lea in leaseList.Leases)
            {
                // Get Residents for the lease
                leasetenants =
                    from res in residentList.Residents
                    where lea.LeaID == res.LeaID &&
                          lea.SiteID == res.SiteID &&
                          lea.UnitID == res.UnitID &&
                          lea.ReshID == res.ReshID
                    select res;

                tenantMaxCnt = GetElementsMaxCount("Tenants");
                dictTenants = new List<Dictionary<string, string>>(tenantMaxCnt);
                foreach (var lt in leasetenants)
                {
                    AssignedAmount = 0;
                    var unitNo = GetUnitNo(lea.UnitID, lea.SiteID, unitList);
                    dictLease = ExtractLeaseInfo(lea, unitNo);
                    var leaseresident = leasetenants.Where(i => i.LeaID == lea.LeaID).FirstOrDefault();
                    if (leaseresident != null)
                    {
                        var collstat = leaseresident.InColl.Equals("Y") ? "true" : "false";
                        collStatus = Convert.ToBoolean(collstat);
                        dictLease["CollectionStatus"] = collstat;

                        if (!collStatus)
                        {
                            cntFalseCount++;
                        }
                    }


                    dictTenant = ExtractTenantInfo(lt);
                    if (dictTenants.Count < dictTenants.Capacity)
                    {
                        dictTenants.Add(dictTenant);
                        TenantList.Add(lt);
                    }

                    foreach (var ten in lt.ResidentMembers)
                    {
                        dictTenant = ExtractTenantInfo(ten);
                        if (dictTenant.Count > 0)
                        {
                            if (dictTenants.Count < dictTenants.Capacity)
                            {
                                dictTenants.Add(dictTenant);
                               // TenantList.Add(new Resident(){ReshID = ten.});
                            }
                        }
                    }   
                } 

                // Get open charges for the lease
                leasecharges =
                    from chg in openchargesList.openChgs
                    where lea.LeaID == chg.LeaID &&
                        lea.SiteID == chg.SiteID &&
                        lea.UnitID == chg.UnitID &&
                        lea.ReshID == chg.ReshID
                    select chg;

                ftransMaxCount = GetElementsMaxCount("FileTransactions");
                dictFiletrans = ExtractFileTransInfo(leasecharges);
                if (dictFiletrans.Count < ftransMaxCount)
                    AddBlankTransactions(dictFiletrans, ftransMaxCount);

                if (dictTenants.Count < tenantMaxCnt)
                    AddBlankTenants(dictTenants, tenantMaxCnt);

                dictTenants[tenantMaxCnt - 1]["Other3"] = _NTVOnDate;
                dictTenants[tenantMaxCnt - 1]["Other4"] = _NTVForDate;


                if (collStatus)
                    WriteLease(writer);
                else
                    WriteLease(writerFalse);
            }
        }

        private string GetUnitNo(int unitId, int siteId, List<Unit> units  )
        {
            string val = string.Empty;
            var  unit = units.FirstOrDefault(i => i.UnitID == unitId && i.SiteID == siteId);
            if (unit != null)
            {
                val = unit.UnitNo;
            }
            return val;
        }

        private List<Dictionary<string, string>> ExtractFileTransInfo(IEnumerable<OpenCharges> leasecharges)
        {
            var chgs = new List<Dictionary<string, string>>(ftransMaxCount);
            var temp = GetElements("FileTransactions");
            if (temp == null)
                return chgs;
            int cnt = 0;
            foreach (var charges in leasecharges)
            {
                var key = string.Empty;
                var val = string.Empty;

                var st = new Dictionary<string, string>();
                cnt++;
                if (cnt == 1)
                {
                    st.Add("AssignedAmount", AssignedAmount.ToString());
                }
                foreach (XElement de in temp)
                {
                    key = de.Attribute("location").Value;
                    if (typeof (OpenCharges).GetProperties().Any(x => x.Name == key))
                    {
                        val = charges.GetType().GetProperty(key).GetValue(charges, null).ToString();
                        st.Add(de.Attribute("outputname").Value, val);
                    }
                }
                if (chgs.Count < chgs.Capacity)
                {
                    chgs.Add(st);
                }
            }
            return chgs;
        }
     


        private Dictionary<string, string> ExtractTenantInfo<T>(T tenant)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("Tenants");
            if (temp == null)
                return st;
            if (tenant is Resident)
            {
                var res = tenant as Resident;
                ExtractResident(res, st);
            }
            else if (tenant is ResidentMember)
            {
                var res = tenant as ResidentMember;
                if (res.Guarantor || res.Signer)
                {
                    ExtractResidentMember(res, st);
                }
            }

            return st;


        }

        private void ExtractResidentMember(ResidentMember tenant, Dictionary<string, string> st)
        {
            var temp = GetElements("Tenants");
            if (temp == null)
                return;
            RegularAddress comboAddress = CreateAddressObject(tenant);
            var key = string.Empty;
            var val = string.Empty;
            foreach (XElement de in temp)
            {
                key = de.Attribute("location").Value;

                if (key == "Other1" || key == "Other2" || key == "Other3" || key == "Other4")
                {
                    st.Add(key, string.Empty);
                    continue;
                }
                if (key == "LastName")
                {
                    continue;
                }
                if (key == "FirstName")
                {
                    key = "Name";
                }

                if (typeof(ResidentMember).GetProperties().Any(x => x.Name == key))
                {
                    val = tenant.GetType().GetProperty(key).GetValue(tenant, null).ToString();
                    if (key == "SSNo")
                    {
                        val = DecryptValue(val);
                    }
                    if (key == "Name")
                    {
                        var spl = val.Split(',');
                        switch (spl.Length)
                        {
                            case 0:
                                val = string.Empty;
                                st.Add("FirstName", val);
                                st.Add("LastName", val);
                                break;
                            case 1:
                                val = spl[0];
                                st.Add("FirstName", string.Empty);
                                st.Add("LastName", StripNumber(val));
                                break;
                            case 2:
                                st.Add("FirstName", spl[1].Trim());
                                st.Add("LastName", StripNumber(spl[0]));
                                break;
                            default:
                                val = string.Empty;
                                st.Add("FirstName", val);
                                st.Add("LastName", val);
                                break;
                        }
                        continue;
                    }
                    if (de.Attribute("date") != null && de.Attribute("date").Value == "1")
                    {
                        val = FormatDate(val);
                    }
                    if (de.Attribute("phone") != null && de.Attribute("phone").Value == "1")
                    {
                        val = UnformatPhoneNumber(val);
                    }
                    st.Add(de.Attribute("outputname").Value, val);
                }
                else if (typeof(RegularAddress).GetProperties().Any(x => x.Name == key))
                {
                    val = comboAddress.GetType().GetProperty(key).GetValue(comboAddress, null).ToString();
                    if (de.Attribute("phone") != null && de.Attribute("phone").Value == "1")
                    {
                        val = UnformatPhoneNumber(val);
                    }
                    st.Add(de.Attribute("outputname").Value, val);
                }
            }

        }

        private string UnformatPhoneNumber(string val)
        {
            return 
                        val.Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace("-", string.Empty)
                .Replace(" ", string.Empty);
        }

        private string DecryptValue(string val)
        {
            string output = string.Empty;
            if (string.IsNullOrEmpty(val))
                return string.Empty;
            if (string.IsNullOrEmpty(DecryptKey))
            {
                return val;
            }
            Engine myEngine = new Engine(DecryptKey);
            try
            {
                output = myEngine.Decrypt(val);
            }
            catch (Exception ex)
            {
                output =  val + "\nError Decrypting.." + ex;
            }
            return output;
        }
        

        private void ExtractResident(Resident tenant, Dictionary<string, string> st)
        {
            var temp = GetElements("Tenants");
            if (temp == null)
                return ;
            RegularAddress comboAddress = CreateAddressObject(tenant);
            EmergAddress emAddr = null;
            if (tenant.EmergAddress != null)
                emAddr = tenant.EmergAddress;

            var key = string.Empty;
            var val = string.Empty;
            foreach (XElement de in temp)
            {
                key = de.Attribute("location").Value;
                if (key == "EmergFirstName" || key == "EmergLastName" || key == "EmergPhone")
                {
                    if (emAddr != null)
                    {
                        val =string.Empty;
                        if (key == "EmergFirstName")
                            val = emAddr.EmergFirstName;
                        if (key == "EmergLastName")
                            val = emAddr.EmergLastName;
                        if (key == "EmergPhone")
                            val = UnformatPhoneNumber(emAddr.Cell);
                    }
                    st.Add(key, val);
                    continue;
                }
                if (key == "Other1" || key == "Other2" || key == "Other3" || key == "Other4")
                {
                    st.Add(key,string.Empty);
                    continue;
                }
                if (typeof (Resident).GetProperties().Any(x => x.Name == key))
                {
                    val = tenant.GetType().GetProperty(key).GetValue(tenant, null).ToString();
                    if (key == "LastName")
                    {
                        val = StripNumber(val);
                    }
                    if (key == "SSNo")
                    {
                        val = DecryptValue(val);
                    }
                    if (de.Attribute("date") != null && de.Attribute("date").Value == "1")
                    {
                        val = FormatDate(val);
                    }
                    st.Add(de.Attribute("outputname").Value, val);
                }
                else if (typeof(RegularAddress).GetProperties().Any(x => x.Name == key))
                {
                    val = comboAddress.GetType().GetProperty(key).GetValue(comboAddress, null).ToString();

                    if (de.Attribute("phone") != null && de.Attribute("phone").Value == "1")
                    {
                        val = UnformatPhoneNumber(val);
                    }
                    st.Add(de.Attribute("outputname").Value, val);
                }
            }
        }

        private string FormatDate(string val)
        {
            var retVal = string.Empty;
            if (!string.IsNullOrEmpty(val))
            {
                var Dt = Convert.ToDateTime(val);
                retVal = Dt.ToString("yyyyMMdd");
            }
            return retVal;
        }

        private string StripNumber(string val)
        {
            // LAstname seems to be of the format Holmes (2), need to return Holmes
            string output = Regex.Replace(val, @"\(\d\)", string.Empty);
            return output.Trim();

        }

        private RegularAddress CreateAddressObject(Resident tenant)
        {
            RegularAddress ra = new RegularAddress();
            if (tenant.ForwardingAddress != null)
            {
                ra.Adr1 = tenant.ForwardingAddress.Adr1;
                if (!string.IsNullOrEmpty(tenant.ForwardingAddress.Adr2))
                {
                    ra.Adr1 = tenant.ForwardingAddress.Adr1 + " " + tenant.ForwardingAddress.Adr2;
                }
                ra.City = tenant.ForwardingAddress.City;
                ra.St = tenant.ForwardingAddress.St;
                ra.Zip = tenant.ForwardingAddress.Zip;
                ra.Country = tenant.ForwardingAddress.Country;
                ra.Email = tenant.ForwardingAddress.Email;
                ra.Phone1 = tenant.ForwardingAddress.Phone1;
                ra.Phone2 = tenant.ForwardingAddress.Phone2;
                ra.Cell = tenant.ForwardingAddress.Cell;
            }
            else if (tenant.RegularAddress != null)
            {
                ra.Adr1 = tenant.RegularAddress.Adr1;
                if (!string.IsNullOrEmpty(tenant.RegularAddress.Adr2))
                {
                    ra.Adr1 = tenant.RegularAddress.Adr1 + " " + tenant.RegularAddress.Adr2;
                }
                ra.City = tenant.RegularAddress.City;
                ra.St = tenant.RegularAddress.St;
                ra.Zip = tenant.RegularAddress.Zip;
                ra.Country = tenant.RegularAddress.Country;
                ra.Email = tenant.RegularAddress.Email;
                ra.Phone1 = tenant.RegularAddress.Phone1;
                ra.Phone2 = tenant.RegularAddress.Phone2;
                ra.Cell = tenant.RegularAddress.Cell;
            }
            if (tenant.RegularAddress != null && tenant.ForwardingAddress != null)
            {
                if (string.IsNullOrEmpty(ra.Phone1))
                {
                    ra.Phone1 = string.IsNullOrEmpty(tenant.ForwardingAddress.Phone1)
                        ? tenant.RegularAddress.Phone1
                        : tenant.ForwardingAddress.Phone1;
                }
                if (string.IsNullOrEmpty(ra.Phone2))
                {
                    ra.Phone2 = string.IsNullOrEmpty(tenant.ForwardingAddress.Phone2)
                        ? tenant.RegularAddress.Phone2
                        : tenant.ForwardingAddress.Phone2;
                }
                if (string.IsNullOrEmpty(ra.Cell))
                {
                    ra.Cell = string.IsNullOrEmpty(tenant.ForwardingAddress.Cell)
                        ? tenant.RegularAddress.Cell
                        : tenant.ForwardingAddress.Cell;
                }
            }
            return ra;
        }

        private RegularAddress CreateAddressObject(ResidentMember tenant)
        {
            RegularAddress ra = new RegularAddress();
            if (tenant.RMRegularAddresses.RMRegularAddressesesList.Count == 0)
                return ra;

            if (tenant.RMRegularAddresses.RMRegularAddressesesList.Count == 1)
            {
                var addrSrc = tenant.RMRegularAddresses.RMRegularAddressesesList.FirstOrDefault();
                ra.Adr1 = addrSrc.Adr1;
                if (!string.IsNullOrEmpty(addrSrc.Adr2))
                {
                    ra.Adr1 = addrSrc.Adr1 + " " + addrSrc.Adr2;
                }
                ra.City = addrSrc.City;
                ra.St = addrSrc.St;
                ra.Zip = addrSrc.Zip;
                ra.Country = addrSrc.Country;
                ra.Email = addrSrc.Email;
                ra.Phone1 = addrSrc.Phone1;
                ra.Phone2 = addrSrc.Phone2;
                ra.Cell = addrSrc.Cell;
                return ra;
            }
            if (tenant.RMRegularAddresses.RMRegularAddressesesList.Count == 2)
            {
                var addrSrc = tenant.RMRegularAddresses.RMRegularAddressesesList.FirstOrDefault();
                var addrSrc2 = tenant.RMRegularAddresses.RMRegularAddressesesList[1];
                ra.Adr1 = addrSrc.Adr1;
                if (!string.IsNullOrEmpty(addrSrc.Adr2))
                {
                    ra.Adr1 = addrSrc.Adr1 + " " + addrSrc.Adr2;
                }
                ra.City = addrSrc.City;
                ra.St = addrSrc.St;
                ra.Zip = addrSrc.Zip;
                ra.Country = addrSrc.Country;
                ra.Email = addrSrc.Email;
                ra.Phone1 = addrSrc.Phone1;
                ra.Phone2 = addrSrc.Phone2;
                ra.Cell = addrSrc.Cell;
                if (string.IsNullOrEmpty(ra.Phone1))
                {
                    ra.Phone1 = addrSrc2.Phone1;
                }
                if (string.IsNullOrEmpty(ra.Phone2))
                {
                    ra.Phone2 = addrSrc2.Phone2;
                }
                if (string.IsNullOrEmpty(ra.Cell))
                {
                    ra.Cell = addrSrc2.Cell;
                }
            }
            return ra;
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
        private void AddBlankTenants(List<Dictionary<string, string>> dictTenants, int max)
        {
            //if (dictTenants.Count == 0) return;
            Dictionary<string, string> dict;
            int toAdd = (max - dictTenants.Count);

            for (int x = 0; x < toAdd; x++)
            {
                dict = new Dictionary<string, string>();
                if (dictTenants != null && dictTenants.Count > 0)
                {
                    foreach (var itm in dictTenants[0].Keys)
                        if (!(itm.StartsWith("Emerg")))
                            dict[itm] = string.Empty;
                }
                dictTenants.Add(dict);
            }
        }

        private void AddBlankTransactions(List<Dictionary<string, string>> dictFiletrans, int max)
        {
            //if (dictFiletrans.Count == 0) return;
            Dictionary<string, string> dict;
            int toAdd = (max - dictFiletrans.Count);

            for (int x = 0; x < toAdd; x++)
            {
                dict = new Dictionary<string, string>();
                if (dictFiletrans != null && dictFiletrans.Count > 0)
                {
                    foreach (var itm in dictFiletrans[0].Keys)
                        if (itm != "AssignedAmount")
                            dict[itm] = string.Empty;
                }
                dictFiletrans.Add(dict);
            }
        }

        private void WriteMessage(string p)
        {
            Console.WriteLine(p);
        }

        private void  WriteLease(StreamWriter writer)
        {
            string tempStr = string.Empty;
            string tempHdr = string.Empty;
            Dictionary<string, string>[] v = new Dictionary<string, string>[tenantMaxCnt+ftransMaxCount+1]; // tenants+transactions+lease
            v[0] = dictLease;
            if (dictTenants != null && dictTenants.Count > 0)
            {
                for (int ix = 1; ix <= tenantMaxCnt; ix++)
                {
                    v[ix] = dictTenants[ix - 1];
                }
            }
             if (dictFiletrans != null && dictFiletrans.Count > 0) 
             {
                 for (int ix = tenantMaxCnt + 1; ix <= tenantMaxCnt + ftransMaxCount; ix++ )
                 {
                     v[ix] = dictFiletrans[ix - tenantMaxCnt - 1];
                 }
             }
            tempHdr = GetString(true, v);
            tempStr = GetString(false, v);

            if (OutputHeader)
                writer.WriteLine(tempHdr);
            writer.WriteLine(tempStr);
            if (debugFormat)
                writer.WriteLine(GetStringDebug(tempHdr, tempStr));
        }

        private void WriteSummary(StreamWriter writer)
        {
            string tempStr = string.Empty;
            string tempHdr = string.Empty;
            // Write Summary + prop combined
            tempHdr = GetString(true, new Dictionary<string, string>[] { dictProp, dictSumm });
            tempStr = GetString(false, new Dictionary<string, string>[] { dictProp, dictSumm });

            if (OutputHeader)
                writer.WriteLine(tempHdr);
            writer.WriteLine(tempStr);
            if (debugFormat)
                writer.WriteLine(GetStringDebug(tempHdr, tempStr));
        }

        private string GetStringDebug(string tempHdr, string tempStr)
        {
            StringBuilder sline = new StringBuilder();
            sline.Append("------------- Start Record Details --------------\r\n");
            if (string.IsNullOrEmpty(tempHdr) || string.IsNullOrEmpty(tempStr))
                return "Error: input strings missing";
 
            var hdr = tempHdr.Split('|');
            var data = tempStr.Split('|');
            if (hdr.Length != data.Length)
                return "Error: Unequal length arrays";

            for (int i = 0; i < hdr.Length; i++)
            {
                if (hdr[i].StartsWith("TenantFirstName"))
                    sline.Append("\r\n--- Tenant Info ---\r\n");
                if (hdr[i].StartsWith("AssignedAmount"))
                    sline.Append("\r\n--- File Transactions Info ---\r\n");
                sline.Append("p" + string.Format("{0,3:###}",i + 1) + " : " + hdr[i] + " = " + (!string.IsNullOrEmpty(data[i]) ? data[i] : "<empty>") + "\r\n");
            }
            sline.Append("------------- End  Record Details --------------\r\n");

            return sline.ToString();
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
                    { 
                        if (dict[ix] != null) st.Add(dict[ix][x]); 
                    }
                    else st.Add(x);
                }
            }
            return string.Join("|", st);
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
        private int GetElementsMaxCount(string section)
        {
            int max = 0;
            XDocument decfg = XDocument.Load(dataElementFile);

            XElement deSec = decfg.Descendants(section).FirstOrDefault();
            if  (deSec == null) 
                return -1;
            if (deSec.HasAttributes && deSec.Attribute("maxcount") != null)
                if (!int.TryParse(deSec.Attribute("maxcount").Value, out max))
                    return -2;
            return max;
        }
        private IEnumerable<XElement> GetElements(string section)
        {
            XDocument decfg = XDocument.Load(dataElementFile);

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
            XDocument decfg = XDocument.Load(dataElementFile);

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
            XDocument decfg = XDocument.Load(dataElementFile);

            XElement deSumm = decfg.Descendants(section).FirstOrDefault();
            if (deSumm == null)
                return null;
            var temp = from a in deSumm.Descendants("dataelement")
                       where a.Attribute("enabled").Value == "1" && a.Attribute("location").Value.Contains("PersonDetails/Address/")
                                && (! a.Attribute("location").Value.Contains("PersonDetails/Address/Email"))
                       select a;
            return temp;
        }

        private Dictionary<string, string> ExtractSummary(XElement summ, List<Lease> leaselist )
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("Summary");
            if (temp == null)
                return st;
            var validationNodes = DeserializeNode<Validation>(summ);
            var key = string.Empty;
            foreach (XElement de in temp)
            {
                if (de.Attribute("value") != null)
                {
                    key = de.Attribute("value").Value;
                    var val = validationNodes.Audits.Where(i => i.Node == key).FirstOrDefault();
                    if (val != null)
                    {
                        st.Add(de.Attribute("outputname").Value,val.Count.ToString());
                    }
                }
            }
            var curBal = leaselist.Sum(i => i.CurBal);
            st.Add("TotalOpenAmount",curBal.ToString());
            return st;
        }

        private T  DeserializeNode<T>(XElement summ)
        {

            var deserializer = new XmlSerializer(typeof(T));
            TextReader reader = new StringReader(summ.ToString());
            object val = deserializer.Deserialize(reader);
            reader.Close();
            return (T) val;
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


        private Dictionary<string, string> ExtractPropertyInfo(Site site)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("Property");
            if (temp == null)
                return st;
            string key;
            string val;
            foreach (XElement de in temp)
            {
                key = de.Attribute("location").Value;
                if (typeof (Site).GetProperties().Any(x => x.Name == key))
                {
                    val =  site.GetType().GetProperty(key).GetValue(site,null).ToString();
                    st.Add(de.Attribute("outputname").Value, val);
                }
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

        private Dictionary<string, string> ExtractLeaseInfo(Lease lease, string unitNo)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            _NTVOnDate = string.Empty;
            _NTVForDate = string.Empty;


            var temp = GetElements("LeaseFile");
            if (temp == null)
                return st;

            var key = string.Empty;
            var val = string.Empty;
            foreach (XElement de in temp)
            {
                key = de.Attribute("location").Value;
                if (key == "InColl")
                {
                    st.Add(de.Attribute("outputname").Value, string.Empty);
                    continue;
                }
                if (key == "UnitNo")
                {
                    val = unitNo;
                    st.Add(de.Attribute("outputname").Value, val);
                    continue;
                }
                if (typeof (Lease).GetProperties().Any(x => x.Name == key))
                {
                    val = lease.GetType().GetProperty(key).GetValue(lease, null).ToString();

                    if (key == "CurBal")
                    {
                        AssignedAmount = Convert.ToDecimal(val);
                        continue;
                    }
                    if (de.Attribute("date") != null && de.Attribute("date").Value == "1")
                    {
                        if (!string.IsNullOrEmpty(val))
                        {
                            var Dt = Convert.ToDateTime(val);
                            val = Dt.ToString("yyyyMMdd");
                        }
                        else
                        {
                            val = string.Empty;
                        }
                        if (key == "NTVOnDate")
                        {
                            _NTVOnDate = val;
                            continue;
                        }
                        if (key == "NTVForDate")
                        {
                            _NTVForDate = val;
                            continue;
                        }
                    }
                    st.Add(de.Attribute("outputname").Value, val);
                }
            }
            return st;
        }

        /*
        private Dictionary<string, string> ExtractFtrans(RPFileTransactions fTrans, string transid, bool firstTran = false)
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            var temp = GetElements("FileTransactions");
            var secMaxCount = GetElementsMaxCount("FileTransactions");
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
        */
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
