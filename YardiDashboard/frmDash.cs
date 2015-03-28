using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Xml.Linq;
using System.Xml.Schema;
using NSConfig;
using System.IO;
using YardiDashboard.RPXCollections;
using YardiData;
using System.Xml;
namespace YardiDashboard
{
    public partial class frmDash : Form
    {
        private const string _FileLocationsTool = "YardiFileLocator.exe";
        private const string _YardiClientsTool = "YardiClients.exe";
        private string _entname = "Rent Recovery Solutions";
        string locConfig = string.Empty;
        string cliConfig = string.Empty;
        string licConfig = string.Empty;
        string strLic = string.Empty;
        XDocument locFile = null;
        XDocument cliFile = null;
        cNSConfig ccfg = new cNSConfig();
        cNSConfig ccfgRP = new cNSConfig();
        // Realpage appsettings
        private string rpxusername = string.Empty;
        private string rpxpassword = string.Empty;
        private string rpxlicensekey = string.Empty;
        private string rpxsystemname = string.Empty;
        //private string rpxpmcid = string.Empty;
        string rpcliConfig = string.Empty;
        XDocument rpcliFile = null;

        private enum VendorName
        {
            Yardi,
            Realpage,
            VendorUnknown=-1
        };


        public frmDash()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void LaunchTool(string _FileLocationsTool)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = _FileLocationsTool;
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to run " + _FileLocationsTool + "\n" +  ex.ToString(), "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
        
            }
        }
        private void Launch(string folder)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "explorer.exe";
                startInfo.Arguments = folder;
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void mnuYardiFileLocator_Click(object sender, EventArgs e)
        {
            LaunchTool(_FileLocationsTool);
        }

        private void mnuYardiClients_Click(object sender, EventArgs e)
        {
            LaunchTool(_YardiClientsTool);
        }

        private void frmDash_Load(object sender, EventArgs e)
        {
    //        SetRunMode(runUnattendedToolStripMenuItem.Checked);
            tabCtl.SelectTab("tabRetrieval");
            AddMessage("Application started..");
            try
            {
                if (!CheckFileLocations())
                {
                    MessageBox.Show("Error Loading File Locations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                // Yardi
                if (!CheckClients())
                {
                    MessageBox.Show("Error Loading Clients", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                if (!CheckLicense())
                {
                    MessageBox.Show("Error Loading License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                if (!CheckEntityName())
                {
                    MessageBox.Show("Error Loading Entity Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                if (txtColl.Text == string.Empty || txtCollFalse.Text == string.Empty || txtRawXML.Text == string.Empty)
                {
                    MessageBox.Show("One or more file locations is not set. please choose folders and restart", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LaunchTool(_FileLocationsTool);
                    this.Close();
                }
                // RealPage appsettings verification
                if (!CheckRealPageSettings())
                {
                    MessageBox.Show("One or more RealPage appsettings keys missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }


                if (!CheckClients_RP())
                {
                    MessageBox.Show("Error Loading Clients", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private bool CheckClients_RP()
        {
            bool ret = false;
            rpcliConfig = ConfigurationManager.AppSettings["rpclientconfiguration"];
            if (rpcliConfig == null)
            {
                MessageBox.Show("Unable to find 'clientconfiguration' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
                this.Close();
            }
            rpcliFile = ccfg.GetConfig(rpcliConfig);
            if (rpcliConfig == null)
            {
                return false;
            }
            LoadClients_RP(rpcliFile);
            ret = true;
            return ret;
        }

        private bool CheckRealPageSettings()
        {
            rpxusername = ConfigurationManager.AppSettings["rpxusername"];
            rpxpassword = ConfigurationManager.AppSettings["rpxpassword"];
            rpxlicensekey = ConfigurationManager.AppSettings["rpxlicensekey"];
            rpxsystemname = ConfigurationManager.AppSettings["rpxsystemname"];
          //  rpxpmcid = ConfigurationManager.AppSettings["rpxpmcid"];
            if (string.IsNullOrEmpty(rpxusername)
                || string.IsNullOrEmpty(rpxpassword)
                || string.IsNullOrEmpty(rpxlicensekey)
                || string.IsNullOrEmpty(rpxsystemname)
                )
                return false;
            return true;
        }

        private bool CheckLicense()
        {
            bool ret = false;
            licConfig = ConfigurationManager.AppSettings["licensefile"];
            if (licConfig == null)
            {
                MessageBox.Show("Unable to find 'licensefile' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
                this.Close();
            }
            strLic = GetLicense(licConfig);
            if (strLic != string.Empty)
                txtLicFile.Text = licConfig;
            ret = true;
            return ret;
        }

        private bool CheckEntityName()
        {
            bool ret = false;
            _entname = ConfigurationManager.AppSettings["entityname"];
            if (String.IsNullOrEmpty(_entname))
            {
                MessageBox.Show("Unable to find 'entityname' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
                this.Close();
            }
            ret = true;
            return ret;
        }
        //static string GetLicense(string FileName)
        //{
        //    string TextLine = string.Empty;

        //    if (File.Exists(FileName))
        //    {
        //        StreamReader objReader = new StreamReader(FileName);
        //        while (objReader.Peek() != -1)
        //            TextLine = TextLine + objReader.ReadLine();

        //        if (TextLine != string.Empty && TextLine.Trim().Length > 0)
        //            return TextLine.Trim();
        //        else
        //            return "";
        //    }
        //    else
        //        return string.Empty;
        //}
        private void LoadClients(XDocument cliFile)
        {
            List<XElement> clients = ccfg.GetElements(cliFile, "client");
            lvClients.Items.Clear();

            foreach (XElement el in clients)
            {
                if (ccfg.GetElement(el, "enabled").Value.ToLower() == "true")
                    lvClients.Items.Add(new ListViewItem(new string[] 
                                      {ccfg.GetElementAttrib(el,"keyword").Value
                                      ,ccfg.GetElement(el,"yardipropid").Value
                                      ,ccfg.GetElement(el,"name").Value
                                      ,ccfg.GetElement(el,"enabled").Value
                                      ,ccfg.GetElement(el,"url").Value
                                     
                                        }));

            }
        }
        private void LoadClients_RP(XDocument cliFile)
        {
            cboSiteList.DataSource = null;

            List<XElement> clients = ccfg.GetElements(cliFile, "client");
            lvRealPageSites.Columns.Clear();
            lvRealPageSites.Items.Clear();
            lvRealPageSites.Columns.Add("RRS Id", 50);
            lvRealPageSites.Columns.Add("Site Id", 60);
            lvRealPageSites.Columns.Add("Site Name", 160);
            lvRealPageSites.Columns.Add("PMC Name", 160);
            lvRealPageSites.Columns.Add("Enabled", 60);
            lvRealPageSites.Columns.Add("Address", 300);
            lvRealPageSites.Columns.Add("Email", 300);
            lvRealPageSites.Columns.Add("FirstDate", 100);
            lvRealPageSites.Columns.Add("PMC Id", 60);
            lvRealPageSites.Columns.Add("Ekey", 150);
            lvRealPageSites.Columns.Add("Days After Moveout", 50);
            lvRealPageSites.Columns.Add("Minimum Bal", 100);

            Dictionary<string, string> cboDict = new Dictionary<string, string>();
            cboDict.Add("_Select Site_",string.Empty);
            cboSiteList.Items.Clear();
            var kwd = string.Empty;
            foreach (XElement el in clients)
            {
                var enabledFlag = ccfg.GetElement(el, "enabled").Value;
                if (enabledFlag == "false")
                    continue;
                lvRealPageSites.Items.Add(new ListViewItem(new string[] 
                                      {ccfg.GetElementAttrib(el,"keyword").Value
                                      ,ccfg.GetElement(el,"siteid").Value
                                      ,ccfg.GetElement(el,"sitename").Value
                                      ,ccfg.GetElement(el,"name").Value
                                      ,ccfg.GetElement(el,"enabled").Value
                                      ,ccfg.GetElement(el,"siteaddress").Value
                                      ,ccfg.GetElement(el,"email").Value
                                      ,ccfg.GetElement(el,"firstdate").Value
                                      ,ccfg.GetElement(el,"pmcid").Value
                                      ,ccfg.GetElement(el,"ekey").Value
                                      ,ccfg.GetElement(el,"aftermoveout").Value
                                      ,ccfg.GetElement(el,"balanceowed").Value
                                     
                                        }));
                kwd = ccfg.GetElementAttrib(el, "keyword").Value.Replace(" ", "_");
                if (!string.IsNullOrEmpty(kwd))
                {
                    cboDict.Add(kwd + "_" + ccfg.GetElement(el, "siteid").Value, kwd);
                }
            }
            var cboSorted  = from pair in cboDict
                        orderby pair.Key ascending
                        select pair;

            cboSiteList.DisplayMember = "Key";
            cboSiteList.ValueMember = "Value";
            cboSiteList.DataSource = new BindingSource(cboSorted, null); 
        }
        private bool CheckClients()
        {
            bool ret = false;
            cliConfig = ConfigurationManager.AppSettings["clientconfiguration"];
            if (cliConfig == null)
            {
                MessageBox.Show("Unable to find 'clientconfiguration' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
                this.Close();
            }
            cliFile = ccfg.GetConfig(cliConfig);
            if (cliConfig == null) return false;
            LoadClients(cliFile);
            ret = true;
            return ret;
        }

        private bool CheckFileLocations()
        {
            bool ret = false;
            locConfig = ConfigurationManager.AppSettings["filelocation"];
            if (locConfig == null)
            {
                MessageBox.Show("Unable to find 'filelocation' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
                this.Close();
            }
            locFile = ccfg.GetConfig(locConfig);
            if (locConfig == null) return false;
            txtRawXML.Text = GetFileLoc("rawxml",locFile);
            txtColl.Text = GetFileLoc("collections", locFile);
            txtCollFalse.Text = GetFileLoc("collectionsfalse", locFile);
            ret = true;
            return ret;
        }
        private string GetFileLoc(string ele, XDocument fil)
        {
            string ret = string.Empty;
            XElement temp = ccfg.GetElement(locFile, ele);
            if (temp != null)
            {
                if (temp.Attribute("location") != null)
                {
                    ret = temp.Attribute("location").Value;
                }
            }
            return ret;
        }
        private void OpenFolder(string folder)
        {
            if (folder == string.Empty) 
                OpenFolder(@"c:\");
            else
            Launch(folder);
        }
        private void btnOpenRaw_Click(object sender, EventArgs e)
        {
            OpenFolder(txtRawXML.Text);
        }

        private void btnOpenColl_Click(object sender, EventArgs e)
        {
            OpenFolder(txtColl.Text);
        }

        private void btnOpenCollFalse_Click(object sender, EventArgs e)
        {
            OpenFolder(txtCollFalse.Text);

        }

        private void mnuGetCollections_Click(object sender, EventArgs e)
        {
            if (lvClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a client", "Error", MessageBoxButtons.OK);
                return;
            }
            string keyword = lvClients.SelectedItems[0].SubItems[0].Text;
            GetCollections_Local(keyword);

        }

        private void GetCollections(string keyword)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                XElement el = ccfg.GetElementByAttrib(cliFile, "client", "keyword", keyword);
                YardiInterface.YardiInterface iface = new YardiInterface.YardiInterface(ccfg.GetElement(el, "url").Value, txtLicFile.Text);
                iface.URL = ccfg.GetElement(el, "url").Value;
                iface.User = ccfg.GetElement(el, "user").Value;
                iface.Pwd = ccfg.GetElement(el, "password").Value;
                iface.Database = ccfg.GetElement(el, "database").Value;
                iface.Server = ccfg.GetElement(el, "server").Value;
                iface.Platform = ccfg.GetElement(el, "platform").Value;
                iface.YardiPropId = ccfg.GetElement(el, "yardipropid").Value;
                XElement collData = iface.GetCollectionsLeaseInfo(ccfg.GetElement(el, "yardipropid").Value);
                string shortName = ccfg.GetElement(el, "name").Value.Replace(" ", "_");
                string dir = txtRawXML.Text + @"\" + shortName;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string fname = dir + @"\" + iface.YardiPropId + "_" + keyword + "_" + DateTime.Now.ToString("MMddyyyy_hhmm") + ".xml";
                SaveRawFile(collData, fname);
                AddMessage(" XML File saved as " + fname);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void GetCollections_Local(string keyword)
        {
            try
            {
                YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
                XElement el = ccfg.GetElementByAttrib(cliFile, "client", "keyword", keyword);
                s.Url = ccfg.GetElement(el, "url").Value;
                string lic = GetLicense(txtLicFile.Text);
                XmlNode XmlNodeResponse;
                XmlNodeResponse = s.Get_CollectionsLeaseInfo(
                    ccfg.GetElement(el, "user").Value,
                    ccfg.GetElement(el, "password").Value,
                    ccfg.GetElement(el, "server").Value,
                    ccfg.GetElement(el, "database").Value,
                    ccfg.GetElement(el, "platform").Value,
                    _entname,
                    strLic,
                    ccfg.GetElement(el, "yardipropid").Value
                   );


                XElement xE2 = XElement.Parse(XmlNodeResponse.OuterXml);

                string res = ErrorMessage(xE2);
                if (!String.IsNullOrEmpty(res))
                {
                    AddMessage(res);
                    return;
                }
                if (LeaseFileCount(xE2) <= 0)
                {
                    AddMessage("No lease files found in " + keyword + ", file not created");
                    return;
                }
                string shortName = ccfg.GetElement(el, "name").Value.Replace(" ", "_");
                string dir = txtRawXML.Text + @"\" + shortName;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string fname = dir + @"\" + ccfg.GetElement(el, "yardipropid").Value + "_" + keyword + "_" + DateTime.Now.ToString("MMddyyyy_hhmm") + ".xml";
                SaveRawFile(xE2, fname);
                AddMessage(" XML File saved as " + fname);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private string GetLicense(string FileName)
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
        private XElement GetPropertyConfiguration(string keyword)
        {
            XElement collData = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                XElement el = ccfg.GetElementByAttrib(cliFile, "client", "keyword", keyword);
                YardiInterface.YardiInterface iface = new YardiInterface.YardiInterface(ccfg.GetElement(el, "url").Value, txtLicFile.Text);
                iface.User = ccfg.GetElement(el, "user").Value;
                iface.Pwd = ccfg.GetElement(el, "password").Value;
                iface.Database = ccfg.GetElement(el, "database").Value;
                iface.Server = ccfg.GetElement(el, "server").Value;
                iface.Platform = ccfg.GetElement(el, "platform").Value;
                collData = iface.Getproperties();
                return collData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return collData;
        }
        private XElement GetPropertyConfiguration_Local(string keyword)
        {
            XElement collData = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                YardiWebRef.ItfCollections s = new YardiWebRef.ItfCollections();
                XElement el = ccfg.GetElementByAttrib(cliFile, "client", "keyword", keyword);
                s.Url = ccfg.GetElement(el, "url").Value;
                string lic = GetLicense(txtLicFile.Text);
                XmlNode XmlNodeResponse;
                XmlNodeResponse = s.GetPropertyConfigurations(
                    ccfg.GetElement(el, "user").Value,
                    ccfg.GetElement(el, "password").Value,
                    //ccfg.GetElement(el, "database").Value,
                    ccfg.GetElement(el, "server").Value, 
                    ccfg.GetElement(el, "database").Value,
                    ccfg.GetElement(el, "platform").Value,
                    _entname,
                    strLic
                   );
                if (XmlNodeResponse != null)
                    collData = XElement.Parse(XmlNodeResponse.OuterXml);

                else
                    MessageBox.Show("Null response returned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return collData;
        }
        private void GetVersion(string keyword)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                XElement el = ccfg.GetElementByAttrib(cliFile, "client", "keyword", keyword);
                YardiInterface.YardiInterface iface = new YardiInterface.YardiInterface(ccfg.GetElement(el, "url").Value, txtLicFile.Text);
                string version = iface.ClientVersion;
                AddMessage("Interface Version for " + keyword + " is " + version);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void SaveRawFile(XElement collData, string fname)
        {
            collData.Save(fname);

        }

        private void btnOpenLic_Click(object sender, EventArgs e)
        {
            OpenFolder(txtLicFile.Text);
        }

        private void retrieveCollectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string keyword;
            //string propCode;
            //string propName;
            if (lvClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a client", "Error", MessageBoxButtons.OK);
                return;
            }
            tabCtl.SelectTab("tabRetrieval");
            foreach (ListViewItem row in lvClients.SelectedItems)
            {
                keyword = row.SubItems[0].Text;

                AddMessage("Retrieving data for " + keyword);
                GetCollections_Local(keyword);

                AddMessage("Retrieval complete " + keyword);
            }
        }
        private void ClearMessage()
        {
            lvMsg.Items.Clear();
        }
        private void AddMessage(string msg)
        {
            ListViewItem lvi = new ListViewItem(DateTime.Now.ToString("MM/dd/yyyy hh:mm ") + msg);
            lvMsg.Items.Add(lvi);
        }

        private void DoExtract(bool outputHeader, bool dbg = false)
        {
            string fn = GetFileToExtract(txtRawXML.Text);
            if (fn == string.Empty)
            {
                return;
            }
            var vendor = IdentifyVendor(fn);
            if (vendor == VendorName.VendorUnknown)
            {
                MessageBox.Show("Unable to identify the Vendor of this file", "Error", MessageBoxButtons.OK);
                return;
            }


            tabCtl.SelectTab("tabRetrieval");
            AddMessage("Extracting " + vendor + " file " + fn);
            switch (vendor)
            {
                case VendorName.Yardi:
                    ExtractYardiFile(fn, outputHeader, dbg);
                    break;
                case VendorName.Realpage:
                    ExtractRealPageFile(fn, outputHeader, dbg);
                    break;
            }
        }

        private void ExtractYardiFile(string fn, bool outputHeader, bool dbg)
        {
            try
            {
                var yd = new YardiData.YardiData(txtRawXML.Text, txtColl.Text, txtCollFalse.Text);
                yd.OutputHeader = outputHeader;
                yd.Extract(fn, dbg);
                AddMessage("File Extracted to " + yd.foutColl);
                if (yd.CollStatusFalseCount > 0)
                    AddMessage("File Extracted to " + yd.foutCollFalse);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Extract: \n" + ex, "Error", MessageBoxButtons.OK);
            }
        }

        private void ExtractRealPageFile(string fn, bool outputHeader, bool dbg)
        {
            try
            {
                var siteFromXml = GetSiteIdFromRawXml(fn);
                if (siteFromXml == string.Empty)
                {
                    MessageBox.Show("Unable to find sideId in the raw xml file", "Error", MessageBoxButtons.OK);
                    return;
                }
                var cle = GetClientEntry(siteFromXml);
                if (cle == null)
                {
                    AddMessage("Extract failure; Unable to locate siteid entry "+ siteFromXml + " in the client list; ");
                    return;
                }
                string dKey = cle.Ekey;   //   GetDecryptKeyForSiteTd(siteFromXml);
                if (string.IsNullOrEmpty(dKey))
                {
                    AddMessage("Decrypt key is null or empty; Will not decrypt Encrypted data");
                }
                var outputDirColl = GetOutputFolder(txtColl.Text, cle);
                var rp = new RealpageData.RealpageData(txtRawXML.Text, outputDirColl, txtCollFalse.Text);
                rp.OutputHeader = outputHeader;
                rp.DecryptKey = dKey;
                rp.Extract(fn, dbg);
                AddMessage("File Extracted to " + rp.foutColl);
                if (rp.CollStatusFalseCount > 0)
                    AddMessage("File Extracted to " + rp.foutCollFalse);
                var tenants = rp.TenantList;
                DownloadTenantDocuments(tenants, rp.foutColl, cle);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Extract: \n" + ex, "Error", MessageBoxButtons.OK);
            }
        }

        private string GetSiteIdFromRawXml(string fn)
        {
            string site = string.Empty;
            XDocument doc = XDocument.Load(fn);
            XElement sites = doc.Descendants("SiteList").FirstOrDefault();
            if (sites == null)
            {
                return site;
            }

            var firstSite = sites.Descendants("Site").FirstOrDefault();
            if (firstSite == null)
            {
                return site;
            }

            var firstSiteIdNode = firstSite.Descendants("SiteID").FirstOrDefault();
            if (firstSiteIdNode == null)
            {
                return site;
            }
            site = firstSite.Descendants("SiteID").FirstOrDefault().Value;
            return site;
        }

        private void DownloadTenantDocuments(List<RealpageData.Resident> tenants, string fn, ClientEntry cle)
        {
            AddMessage("Downloading tenant Documents..");
            string path = Path.GetDirectoryName(fn);
            foreach (var res in tenants)
            {
                BuildAndDownLoadDocument2(res, path, cle);
            }
            AddMessage("Download Complete..");

        }

        private void BuildAndDownLoadDocument(RealpageData.Resident res, string folder, ClientEntry cle)
        {
            AuthDTO auth = GetAuth(cle);
            var client = new RPXServiceClient();
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 00);
            try
            {
                var req = new BuildCollectionDocumentsRequest() { reshid = res.ReshID };
                var resp = client.buildcollectiondocuments(auth, req);
                txtResponse.Text = resp.ToString();
                var fileSizeNode = resp.Descendants("filesize").FirstOrDefault();
                int fileSizeVal = 0;
                if (fileSizeNode != null)
                {
                    fileSizeVal = Convert.ToInt32(fileSizeNode.Value);
                }
                if (fileSizeVal == 0)
                {
      //              AddMessage("No files to download for site " + res.SiteID + " Tenant Reshid " + res.ReshID + " Name: " + res.FirstName + " " + res.LastName );
                    return;
                }

                var reqDL = new DownloadCollectionDocumentRequest() { reshid = res.ReshID };

                string returnDoc = client.downloadcollectiondocument(auth, reqDL).Value;

                var filename = folder 
                    + @"\"
                    + cle.RrsId + "_"
                    + cle.SiteId + "_"
                    + res.ReshID + "_"
                    + StripNumber(res.LastName) + "_" + res.FirstName + "_" + DateTime.Now.ToString("MMddyyyy_HHmm") + ".pdf";
                
                FileStream wFile = new FileStream(filename, FileMode.Create);
                byte[] buffer;
                buffer = Convert.FromBase64String(returnDoc);
                wFile.Write(buffer, 0, buffer.Length);
                wFile.Close();
                AddMessage("File " + filename + "  Has been created ; filesize " + fileSizeVal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Creating File\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BuildAndDownLoadDocument2(RealpageData.Resident res, string folder, ClientEntry cle)
        {
            AuthDTO auth = GetAuth(cle);
            var client = new RPXServiceClient();
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 00);
            try
            {
                var req = new BuildCollectionDocumentsRequest() { reshid = res.ReshID };
                
                var resp = client.buildcollectiondocumentsAsync(auth, req);
                var respXml =  resp.Result.Body.buildcollectiondocumentsResult;
                txtResponse.Text = respXml.ToString();
                var fileSizeNode = respXml.Descendants("filesize").FirstOrDefault();
                int fileSizeVal = 0;
                if (fileSizeNode != null)
                {
                    fileSizeVal = Convert.ToInt32(fileSizeNode.Value);
                }
                if (fileSizeVal == 0)
                {
                    //              AddMessage("No files to download for site " + res.SiteID + " Tenant Reshid " + res.ReshID + " Name: " + res.FirstName + " " + res.LastName );
                    return;
                }

                var filename = folder
                    + @"\"
                    + cle.RrsId + "_"
                    + cle.SiteId + "_"
                    + res.ReshID + "_"
                    + StripNumber(res.LastName) + "_" + res.FirstName + "_" + DateTime.Now.ToString("MMddyyyy_HHmm") + ".pdf";


                var reqDL = new DownloadCollectionDocumentRequest() { reshid = res.ReshID };

                var returnDoc =  client.downloadcollectiondocumentAsync(auth, reqDL).Result.Body.downloadcollectiondocumentResult;
                var returnResult = returnDoc.Descendants("Result").FirstOrDefault();
                if (returnResult == null)
                {
                    AddMessage("Result is empty for file pull " + filename);
                    return;
                }
                var retData = returnDoc.Descendants("Result").FirstOrDefault().Value;

                FileStream wFile = new FileStream(filename, FileMode.Create);
                byte[] buffer;
                buffer = Convert.FromBase64String(retData);
                wFile.Write(buffer, 0, buffer.Length);
                wFile.Close();
                AddMessage("File " + filename + "  Has been created ; filesize " + fileSizeVal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Creating File\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string StripNumber(string val)
        {
            // LAstname seems to be of the format Holmes (2), need to return Holmes
            string output = Regex.Replace(val, @"\(\d\)", string.Empty);
            return output.Trim();

        }

        private VendorName IdentifyVendor(string fn)
        {
            var doc = XDocument.Load(fn);
            if (doc.Descendants("MITS-Collections").FirstOrDefault() != null)
            {
                return VendorName.Yardi;
            }
            if (doc.Descendants("Response").FirstOrDefault() != null)
            {
                return VendorName.Realpage;
            }
            return VendorName.VendorUnknown;
        }


        private string GetFileToExtract(string path)
        {
            string fname = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose file to extract";
            ofd.InitialDirectory = path;
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                fname = ofd.FileName;

            }
            return fname;
        }

        private void getVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a client", "Error", MessageBoxButtons.OK);
                return;
            }
            tabCtl.SelectTab("tabRetrieval");
            string keyword = lvClients.SelectedItems[0].SubItems[0].Text;
            GetVersion(keyword);
        }

        private void excludeHeadersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoExtract(false);
        }

        private void includeHeadersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoExtract(true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearMessage();
        }

        private void getPropertyConfigurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a client", "Error", MessageBoxButtons.OK);
                return;
            }
            tabCtl.SelectTab("tabRetrieval");
            string keyword = lvClients.SelectedItems[0].SubItems[0].Text;
            string propCode = lvClients.SelectedItems[0].SubItems[1].Text;
            string propName = lvClients.SelectedItems[0].SubItems[2].Text;
            //XElement props = GetPropertyConfiguration(keyword);
            XElement props = GetPropertyConfiguration_Local(keyword);
            if (props == null)
            {
                AddMessage("There are no properties");
                return;
            }
            string res = ErrorMessage(props);
            if (!String.IsNullOrEmpty(res))
            {
                AddMessage(res);
                return;
            }
            AddMessage(String.Format("---- Property Configurations for {0} properties for {1} - {2}: {3} ----",props.Descendants("Property").Count(), keyword,propCode,propName));
            foreach ( XElement prop in props.Descendants("Property"))
            {
                    AddMessage(GetPropInfo(prop));
            }
        }

        private string ErrorMessage(XElement props)
        {
            string rc = string.Empty;
            foreach (XElement prop in props.Descendants("Message"))
            {
                rc = prop.Value;
            }
            return rc;
        }
        private int LeaseFileCount(XElement props)
        {
            int rc = 0;
            foreach (XElement prop in props.Descendants("TotalLeaseFiles"))
            {
                int.TryParse(prop.Value, out rc); ;
            }
            return rc;
        }
        private string GetPropInfo(XElement prop)
        {
            StringBuilder s= new StringBuilder();
            if (prop.Descendants("Code").FirstOrDefault()  != null)
                s.Append("Property " + prop.Element("Code").Value);
            if (prop.Descendants("Code").FirstOrDefault() != null)
                s.Append("-" + prop.Element("MarketingName").Value);
            if (prop.Descendants("AddressLine1").FirstOrDefault() != null)
                s.Append("-" + prop.Element("AddressLine1").Value);
            if (prop.Descendants("City").FirstOrDefault() != null)
                s.Append("-" + prop.Element("City").Value);
            if (prop.Descendants("State").FirstOrDefault() != null)
                s.Append("-" + prop.Element("State").Value);
            if (prop.Descendants("PostalCode").FirstOrDefault() != null)
                s.Append("-" + prop.Element("PostalCode").Value);
            return s.ToString();
            
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void retrieveAllCollectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabCtl.SelectTab("tabRetrieval");
            GetAllCollections();

        }

        private void GetAllCollections()
        {
            string keyword; string propCode; string propName; bool enabled;

            foreach (ListViewItem row in lvClients.Items)
            {
                keyword = row.SubItems[0].Text;
                propCode = row.SubItems[1].Text;
                propName = row.SubItems[2].Text;
                if (!bool.TryParse(row.SubItems[3].Text, out enabled))
                {
                    AddMessage(String.Format("Invalid Enabled column {3} for ID - {0}-{1} : {2} (skipped)", keyword, propCode, propName, row.SubItems[3].Text));
                    continue;
                }
                if (!enabled)
                {
                    AddMessage(String.Format("Skipped Disabled entry for ID - {0}-{1} : {2}", keyword, propCode, propName, row.SubItems[3].Text));
                    continue;
                }
                AddMessage(String.Format("Retrieving data for ID {0}-{1} : {2}", keyword, propCode, propName));
                GetCollections_Local(keyword);

                //AddMessage("Retrieval complete " + keyword);
            }
        }

        private void chToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchTool(_YardiClientsTool);
        }

        private void setFileLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchTool(_FileLocationsTool);

        }

        private void mnuExtract_Click(object sender, EventArgs e)
        {

        }

        private void unattendedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void runUnattendedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetRunMode(runUnattendedToolStripMenuItem.Checked);
        }

        private void SetRunMode(bool unatt)
        {
            tabCtl.SelectTab("tabRetrieval");
            if (unatt)
            {
                lblRunMode.Text = "Running Unattended";
                lblRunMode.BackColor = Color.Red;
                AddMessage("..started Unattended Run mode ..");
            }
            else
            {
                lblRunMode.Text = string.Empty;
                lblRunMode.BackColor = Control.DefaultBackColor;
                AddMessage("..stopped Unattended Run mode ..");
            }
        }

        private void debugFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoExtract(true, true);
        }


        private void PullSitesForClient(ClientEntry cle)
        {
            AuthDTO auth = GetAuth(cle);

            var client = new RPXServiceClient();
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 00);
            try
            {
                var resp = client.sitelist(auth, new SiteListRequest());
                txtResponse.Text = resp.ToString();
                UpdateClients_RP(resp, cle);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Sitelist Call \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void GetRealPageSites(XElement resp)
        {
            lvRealPageSites.Items.Clear();

            var respNode = resp.Descendants("Response");

            foreach (var site in respNode.Descendants("Site"))
            {
                lvRealPageSites.Items.Add(new ListViewItem(new string[] 
                                      {
                                       String.Empty // "RRS" +site.Descendants("SiteID").FirstOrDefault().Value
                                      ,site.Descendants("SiteID").FirstOrDefault().Value
                                      ,site.Descendants("PMCName").FirstOrDefault().Value
                                      ,site.Descendants("SiteName").FirstOrDefault().Value
                                      ,"true"
                                      ,GetSiteAddress(site)
                                      ,site.Descendants("Email").FirstOrDefault().Value
                                      
                                        }));
            }
        }

        private void UpdateClients_RP(XElement resp, ClientEntry cle)
        {
            var respNode = resp.Descendants("Response");
            XDocument doc = ccfg.GetConfig(rpcliConfig);
            var clients = ccfg.GetElement(doc, "clients");


            foreach (var site in respNode.Descendants("Site"))
            {
                var siteId = site.Descendants("SiteID").FirstOrDefault().Value;
                bool found = false;
                foreach (var member in clients.Descendants("client"))
                {
                    var memberSite = member.Descendants("siteid").FirstOrDefault();
                    if (memberSite == null)
                    {
                        continue;
                    }
                    if (siteId == memberSite.Value)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {

                    var email = site.Descendants("Email").FirstOrDefault() == null
                        ? string.Empty
                        : site.Descendants("Email").FirstOrDefault().Value;
                    XElement newClient = new XElement("client",
                          new XAttribute("keyword", string.Empty)
                        , new XElement("name", site.Descendants("PMCName").FirstOrDefault().Value)
                        , new XElement("enabled", "false")
                        , new XElement("email", email)
                        , new XElement("siteid", site.Descendants("SiteID").FirstOrDefault().Value)
                        , new XElement("sitename", site.Descendants("SiteName").FirstOrDefault().Value)
                        , new XElement("siteaddress", GetSiteAddress(site))
                        , new XElement("firstdate", DateTime.Now.ToString("yyyy-MM-dd"))
                        , new XElement("aftermoveout", cle.AfterMoveout)
                        , new XElement("balanceowed", cle.BalanceOwed)
                        , new XElement("ekey", cle.Ekey)
                        , new XElement("pmcid", cle.PmcId)
                        , new XElement("phone1",  site.Descendants("Phone1").FirstOrDefault() != null ? site.Descendants("Phone1").FirstOrDefault().Value : string.Empty)
                        );
                    clients.Add(newClient);
                    AddMessage("New Client Entry created for Site: "
                        + site.Descendants("SiteName").FirstOrDefault().Value
                        + ", PMC : " + cle.PmcId);
                    doc.Save(rpcliConfig); 
                }
            }
            LoadClients_RP(doc);
        }

        private string GetSiteAddress(XElement site)
        {
            var retStr =
                  (site.Descendants("Adr1").FirstOrDefault() != null ? site.Descendants("Adr1").FirstOrDefault().Value : "No Address") + ", "
                + (site.Descendants("City").FirstOrDefault() != null ? site.Descendants("City").FirstOrDefault().Value : "No City") + ", "
                + (site.Descendants("State").FirstOrDefault() != null ? site.Descendants("State").FirstOrDefault().Value : "No State") + " "
                + (site.Descendants("Zip").FirstOrDefault() != null ? site.Descendants("Zip").FirstOrDefault().Value : "No Zip");
            return retStr;
        }

        private void retrieveCollectionsRpx_Click(object sender, EventArgs e)
        {
            ClientEntry clEntry;
            bool enabled;
            if (lvRealPageSites.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a site", "Error", MessageBoxButtons.OK);
                return;
            }
            AddMessage("Reteieving selected item(s)");
            foreach (ListViewItem row in lvRealPageSites.SelectedItems)
            {
                clEntry = GetClientEntry(row);
                if (string.IsNullOrEmpty(clEntry.RrsId))
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " has not been configured for retrieval! \n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (!clEntry.Enabled)
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " has not been enabled for retrieval! \n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(clEntry.AfterMoveout) 
                    || string.IsNullOrEmpty(clEntry.BalanceOwed)
                    || string.IsNullOrEmpty(clEntry.Ekey))
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " is missing EncryptionKey, Aftermoveout or BalanceOwed values\n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                tabCtl.SelectTab("tabRetrieval");
                AddMessage(string.Format("Retrieving data for RRSId {0} - {1} ", clEntry.RrsId, clEntry.SiteName));
                RetrievePlacementsByDate(clEntry);
            }
            AddMessage("Reteieving selected item(s) complete");

        }

        private ClientEntry GetClientEntry(ListViewItem row)
        {
            var cl = new ClientEntry();
            cl.RrsId = row.SubItems[0].Text;
            cl.SiteId = row.SubItems[1].Text;
            cl.SiteName = row.SubItems[2].Text;
            cl.Enabled = Convert.ToBoolean(row.SubItems[4].Text);
            cl.FirstDate = Convert.ToDateTime(row.SubItems[7].Text);
            cl.PmcId = row.SubItems[8].Text;
            cl.Ekey = row.SubItems[9].Text;
            cl.AfterMoveout = row.SubItems[10].Text;
            cl.BalanceOwed = row.SubItems[11].Text;
            return cl;
        }
        private ClientEntry GetClientEntry(string site)
        {
            XDocument doc = ccfg.GetConfig(rpcliConfig);
            XElement clientElement = ccfg.GetClientElementForSiteId(doc, site);
            if (clientElement == null)
            {
                return null;
            }
            var cl = new ClientEntry();
            cl.RrsId = clientElement.Attribute("keyword") != null ? clientElement.Attribute("keyword") .Value :string.Empty;
            cl.SiteId =  clientElement.Descendants("siteid").FirstOrDefault().Value;
            cl.SiteName = clientElement.Descendants("sitename").FirstOrDefault().Value;
            cl.Enabled = Convert.ToBoolean(clientElement.Descendants("enabled").FirstOrDefault().Value);
            cl.FirstDate = Convert.ToDateTime(clientElement.Descendants("firstdate").FirstOrDefault().Value);
            cl.PmcId = clientElement.Descendants("pmcid").FirstOrDefault().Value;
            cl.Ekey = clientElement.Descendants("ekey").FirstOrDefault().Value;
            cl.AfterMoveout = clientElement.Descendants("aftermoveout").FirstOrDefault().Value;
            cl.BalanceOwed = clientElement.Descendants("balanceowed").FirstOrDefault().Value;
            return cl;
        }
        private void RetrievePlacementsByDate(ClientEntry clEntry)
        {

            Cursor.Current = Cursors.WaitCursor;
       //     rpcliConfig = ConfigurationManager.AppSettings["rpclientconfiguration"];
            ccfgRP = new cNSConfig();
            rpcliFile = ccfgRP.GetConfig(rpcliConfig);

            XElement el = ccfgRP.GetElementByAttrib(rpcliFile, "client", "keyword", clEntry.RrsId);

            AuthDTO auth = GetAuth(clEntry);

            var client = new RPXServiceClient();
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 00);
            var request = new RetrievePlacementsByDateRequest();
            request.aftermoveout = clEntry.AfterMoveout;
            request.balanceowed = clEntry.BalanceOwed;
            request.subid = "ALL";
            request.extractfrom = string.Empty;//new DateTime(2014, 9, 1);
            request.extractto = string.Empty;//new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            request.paramstartdate = Convert.ToDateTime(clEntry.FirstDate.ToString("yyyy-MM-dd"));
            AddMessage(
                string.Format(
                    "RetrievePlacementsByDateRequest aftermoveout {0} balanceowed {1} extractfrom {2} extractto {3} paramstartdate {4}",
                    request.aftermoveout, request.balanceowed, request.extractfrom, request.extractto,
                    request.paramstartdate.ToString("yyyy-MM-dd")));
            try
            {
                var resp = client.retrieveplacementsbydate(auth, request);
                txtResponse.Text = resp.ToString();
                var respNode = resp.Descendants("Response").FirstOrDefault();
                if (RpxLeaseFileCount(respNode) == 0)
                {
                    AddMessage("No lease files found in site " + clEntry.SiteId + ", file not created");
                    return;
                }

                string dir = GetOutputFolder(txtRawXML.Text, clEntry);

                string fullfname = dir + @"\" + GetOutputSubdir(clEntry) + "_" + DateTime.Now.ToString("MMddyyyy_HHmm") + ".xml";
                string fname = GetOutputSubdir(clEntry) + "_" + DateTime.Now.ToString("MMddyyyy_HHmm") + ".xml";
                SaveRawFile(respNode, fullfname);
                AddMessage("XML File saved as " + fname);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on retrieveplacementsbydate Call \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private string GetOutputFolder(string folderBase , ClientEntry clEntry)
        {
            string subDir = (clEntry.RrsId + "_" + clEntry.SiteName).Replace(" ", "_").Replace(",", "");
            string dir = folderBase + @"\" + subDir;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
        private string GetOutputSubdir(ClientEntry clEntry)
        {
            string subDir = (clEntry.RrsId + "_" + clEntry.SiteName).Replace(" ", "_").Replace(",", "");
            return subDir;
        }

        private AuthDTO GetAuth(ClientEntry clEntry)
        {
            var auth = new AuthDTO();
            auth.system = rpxsystemname;

            auth.username = rpxusername;
            auth.password = rpxpassword;
            auth.licensekey = rpxlicensekey;
            auth.pmcid = clEntry.PmcId;
            auth.siteid = clEntry.SiteId;
            return auth;
        }
        private int RpxLeaseFileCount(XElement respNode)
        {
            int rc = 0;
            XElement leases = respNode.Descendants("Collections").FirstOrDefault().Descendants("LeaseList").FirstOrDefault();
            var leaselist = leases.Descendants("Lease").ToList();
            if (leaselist == null )
            {
                return rc;
            }
            return leaselist.Count;
        }

        private void btnDlCollectionDocument_Click(object sender, EventArgs e)
        {
            KeyValuePair<string, string> selectedPair = (KeyValuePair<string,string>) cboSiteList.SelectedItem;

            var folder = txtColl.Text;
            var siteId = selectedPair.Key.Split('_')[1];
            var cboRrsId = selectedPair.Value;
            int resId = 0;
            int.TryParse(txtReshid.Text.Trim(), out resId);
            if (resId == 0 || string.IsNullOrEmpty(cboRrsId))
            {
                MessageBox.Show("Please select a siteId and  enter an ReshId", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            tabCtl.SelectTab("tabRetrieval");
            ClientEntry cle = GetClientEntry(siteId);
            AuthDTO auth = GetAuth(cle);
            var client = new RPXServiceClient();
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 00);
            try
            {
                var req = new BuildCollectionDocumentsRequest() { reshid = resId };
                var resp = client.buildcollectiondocuments(auth, req);
                txtResponse.Text = resp.ToString();
                var fileSizeNode = resp.Descendants("filesize").FirstOrDefault();
                int fileSizeVal = 0;
                if (fileSizeNode != null)
                {
                    fileSizeVal = Convert.ToInt32(fileSizeNode.Value);
                }
                if (fileSizeVal == 0)
                {
                    AddMessage("No files to download");
                    return;
                }
                var dirOut = GetOutputFolder(folder, cle);

                var dlFile = dirOut + @"\" + cle.RrsId + "_" + siteId + "_" + resId + "_AdHoc_" + DateTime.Now.ToString("MMddyyyy_HHmm")  + ".pdf";

                var reqDL = new DownloadCollectionDocumentRequest() { reshid = resId };
                string returnDoc = client.downloadcollectiondocument(auth, reqDL).Value;

                FileStream wFile = new FileStream(dlFile, FileMode.Create);
                byte[] buffer;
                buffer = Convert.FromBase64String(returnDoc);
                wFile.Write(buffer, 0, buffer.Length);
                wFile.Close();
                AddMessage("File has been created as " + dlFile + "; FileSize is " + fileSizeVal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Creating File\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshClients_Click(object sender, EventArgs e)
        {
            if (!CheckClients_RP())
            {
                MessageBox.Show("Error Loading Client List", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AddMessage("Realpage Clients Refreshed");
        }

        private void cboSiteList_DisplayMemberChanged(object sender, EventArgs e)
        {
            txtReshid.Focus();
        }

        private void updateSitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientEntry clEntry;
            bool enabled;
            if (lvRealPageSites.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a site", "Error", MessageBoxButtons.OK);
                return;
            }
            foreach (ListViewItem row in lvRealPageSites.SelectedItems)
            {
                clEntry = GetClientEntry(row);
                if (string.IsNullOrEmpty(clEntry.RrsId))
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " has not been configured for retrieval! \n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (!clEntry.Enabled)
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " has not been enabled for retrieval! \n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(clEntry.PmcId))
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " does not have a PmcId! \n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(clEntry.AfterMoveout)
                    || string.IsNullOrEmpty(clEntry.BalanceOwed)
                    || string.IsNullOrEmpty(clEntry.Ekey))
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " is missing EncryptionKey, Aftermoveout or BalanceOwed values\n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                tabCtl.SelectTab("tabRetrieval");
                AddMessage(String.Format("Updating Sites data for rrsId {0} - {1} (SiteId {2}) ",clEntry.RrsId, clEntry.SiteName, clEntry.SiteId));

                PullSitesForClient(clEntry);

                AddMessage("Site Import complete");
            }

        }

        private void showHideResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtResponse.Text.Trim() == string.Empty)
            {
                AddMessage("Response is empty");
                return;
            }
            Clipboard.Clear();
            Clipboard.SetText(txtResponse.Text);
            AddMessage("Response copied to clipboard");
        }

        private void copyResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabCtl.SelectTab("tabRetrieval");
            if (txtResponse.Text.Trim() == string.Empty)
            {
                AddMessage("Response is empty");
                return;
            }
            Clipboard.Clear();
            Clipboard.SetText(txtResponse.Text);
            AddMessage("Response copied to clipboard");
        }

        private void retrieveAllCollectionsRpx_Click(object sender, EventArgs e)
        {
            ClientEntry clEntry;
            bool enabled;
            AddMessage("Retrieve all collections");
            foreach (ListViewItem row in lvRealPageSites.Items)
            {
                clEntry = GetClientEntry(row);
                if (string.IsNullOrEmpty(clEntry.RrsId))
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " has not been configured for retrieval! \n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (!clEntry.Enabled)
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " has not been enabled for retrieval! \n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(clEntry.AfterMoveout)
                    || string.IsNullOrEmpty(clEntry.BalanceOwed)
                    || string.IsNullOrEmpty(clEntry.Ekey))
                {
                    MessageBox.Show("Site " + clEntry.SiteName + " is missing EncryptionKey, Aftermoveout or BalanceOwed values\n Please configure via client utility and refresh this list", "Site not configured", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                tabCtl.SelectTab("tabRetrieval");
                AddMessage(string.Format("Retrieving data for RRSId {0} - {1}", clEntry.RrsId, clEntry.SiteName));
                RetrievePlacementsByDate(clEntry);
            }
            AddMessage("Retrievals complete");

        }

        private void filesNeverExtractedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
