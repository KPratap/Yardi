using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Xml.Linq;
using log4net;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSConfig;
using System.IO;
using YardiDashboard.RPXCollections;
using YardiData;
using System.Xml;
namespace YardiDashboard
{
    public partial class frmDash : Form
    {
        private Thread worker; 
       // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
        DateTime filenotextractedcutoffdate;
        XDocument rpcliFile = null;

        private string pscliconfig = string.Empty;
        private XDocument pscliFile = null;

        private static string _apiurl = "https://sync.propertysolutions.com/api/oauth";
        private static string _leaseUrlSuffix = ".propertysolutions.com/api/leases";
        private static string _clientId = "80aa0055c39ea084d2bd.rrs_mult";
        //"dde8541e13ec4ed241f9.rent_rec";  // Supplied by property Solutions
        private static string _clientSecret = "57102b11720fd1c7cb9b08cd50e6374e"; // Supplied by property Solutions
        private enum VendorName
        {
            Yardi,
            Realpage,
            PropertySolutions,
            VendorUnknown=-1
        };

        private BackgroundWorker bw = null;

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
                Log.Error("Unable to run " + _FileLocationsTool + "\n" + ex);
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
                Log.Error(ex);
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
            SetRunMode(runUnattendedToolStripMenuItem.Checked);
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
                    MessageBox.Show("Error Loading Realpage Clients", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                // Property Solutions appsettings verification
                if (!CheckClients_PS())
                {
                    MessageBox.Show("Error Loading Property Solutions Clients", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                this.Close();
            }
        }

        private bool CheckClients_RP()
        {
            bool ret = false;
            rpcliConfig = ConfigurationManager.AppSettings["rpclientconfiguration"];
            if (ConfigurationManager.AppSettings["filenotextractedcutoffdate"] != null)
            {
                filenotextractedcutoffdate =
                    Convert.ToDateTime(ConfigurationManager.AppSettings["filenotextractedcutoffdate"]);
            }
            else
            {
                filenotextractedcutoffdate = new DateTime(2015,3,1);
            }
            dpCutoffDate.Value = filenotextractedcutoffdate;
            if (rpcliConfig == null)
            {
                MessageBox.Show("Unable to find 'rpclientconfiguration' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
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

        private bool CheckClients_PS()
        {
            bool ret = false;
            pscliconfig = ConfigurationManager.AppSettings["psclientconfiguration"];

            if (pscliconfig == null)
            {
                MessageBox.Show("Unable to find 'psclientconfiguration' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
                this.Close();
            }
            pscliFile = ccfg.GetConfig(pscliconfig);
            if (pscliconfig == null)
            {
                return false;
            }
            LoadClients_PS(pscliFile);
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
        //    lvClients.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
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

        private void LoadClients_PS(XDocument cliFile)
        {
            cboSiteListPS.DataSource = null;

            List<XElement> clients = ccfg.GetElements(cliFile, "client");
            lvPSSites.Columns.Clear();
            lvPSSites.Items.Clear();
            lvPSSites.Columns.Add("RRS Id", 50);
            lvPSSites.Columns.Add("PS Propid", 60);
            lvPSSites.Columns.Add("Site Name", 160);
            lvPSSites.Columns.Add("Address", 300);
            lvPSSites.Columns.Add("Enabled", 60);
            lvPSSites.Columns.Add("Url", 160);
            lvPSSites.Columns.Add("Subdomain", 160);
            lvPSSites.Columns.Add("Token", 160);
            lvPSSites.Columns.Add("FirstDate", 160);

            Dictionary<string, string> cboDict = new Dictionary<string, string>();
            cboDict.Add("_Select Site_", string.Empty);
            cboSiteListPS.Items.Clear();
            var kwd = string.Empty;
            foreach (XElement el in clients)
            {
                var enabledFlag = ccfg.GetElement(el, "enabled").Value;
                if (enabledFlag == "false")
                    continue;
                if (enabledFlag == "true")
                    lvPSSites.Items.Add(new ListViewItem(new string[] 
                                      {ccfg.GetElementAttrib(el,"keyword").Value
                                      ,ccfg.GetElement(el,"siteid").Value
                                      ,ccfg.GetElement(el,"name").Value
                                      ,ccfg.GetElement(el,"address").Value
                                      ,ccfg.GetElement(el,"enabled").Value
                                      ,ccfg.GetElement(el,"url").Value
                                      ,ccfg.GetElement(el,"subdomain").Value
                                      ,ccfg.GetElement(el,"token").Value
                                      ,ccfg.GetElement(el,"firstdate").Value
                                        }));
                kwd = ccfg.GetElementAttrib(el, "keyword").Value.Replace(" ", "_");
                if (!string.IsNullOrEmpty(kwd))
                {
                    cboDict.Add(kwd + "_" + ccfg.GetElement(el, "siteid").Value, kwd);
                }
            }
            var cboSorted = from pair in cboDict
                            orderby pair.Key ascending
                            select pair;

            cboSiteListPS.DisplayMember = "Key";
            cboSiteListPS.ValueMember = "Value";
            cboSiteListPS.DataSource = new BindingSource(cboSorted, null);
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
            txtEntrataLogins.Text = GetFileLoc("entratalogins", locFile);
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
                Log.Error("Error retrieving data\n" + ex);
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
                Log.Error("Error retrieving data\n" + ex);
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
                Log.Error("Error retrieving data\n" + ex);
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
                Log.Error("Error retrieving data\n" + ex);
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
                Log.Error("Error retrieving data\n" + ex);
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
            Log.Debug(msg);
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
                case VendorName.PropertySolutions:
                    ExtractPropertySolutionsFile(fn, outputHeader, dbg);
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
                Log.Error("Error in Extract: \n" + ex);
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
                var fullstr = ex.ToString();
                var dispStr = fullstr.Length > 500 ? fullstr.Substring(0,500) + "...." : fullstr;
                Log.ErrorFmt(ex.ToString());
                MessageBox.Show("Error in Extract. Please refer to the log for details: \n" + ex.InnerException + "\n" +  dispStr, "Error", MessageBoxButtons.OK);
            }
        }

        private void ExtractPropertySolutionsFile(string fn, bool outputHeader, bool dbg)
        {
            try
            {
                var siteFromXml = GetSiteIdFromRawXmlPS(fn);
                if (siteFromXml == string.Empty)
                {
                    MessageBox.Show("Unable to find sideId in the raw xml file", "Error", MessageBoxButtons.OK);
                    return;
                }
                var cle = GetClientEntryPS(siteFromXml);
                if (cle == null)
                {
                    AddMessage("Extract failure; Unable to locate siteid entry " + siteFromXml + " in the client list; ");
                    return;
                }
                var outputDirColl = GetOutputFolderPS(txtColl.Text, cle);
                var yd = new PSData.PSData(txtRawXML.Text, outputDirColl, txtCollFalse.Text);
                yd.OutputHeader = outputHeader;
                yd.Extract(fn, dbg);
                AddMessage("File Extracted to " + yd.foutColl);
                if (yd.CollStatusFalseCount > 0)
                    AddMessage("File Extracted to " + yd.foutCollFalse);
                var leases = yd.LeaseIdentifiers;
                DownloadLeaseDocumentsPS(leases, yd.foutColl, cle);
            }
            catch (Exception ex)
            {
                Log.Error("Error in Extract: \n" + ex);
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

        private string GetSiteIdFromRawXmlPS(string fn)
        {

        //          <PropertyFiles>
        //<PropertyFile>
        //  <Property>
            //    <PropertyFile IDType="Property ID" IDRank="primary" IDScopeType="sender">
        //      <IDValue>162914</IDValue>


            string site = string.Empty;
            XDocument doc = XDocument.Load(fn);
            XElement sites = doc.Descendants("PropertyFiles").FirstOrDefault();
            if (sites == null)
            {
                return site;
            }

            var firstSite = sites.Descendants("PropertyFile").FirstOrDefault();
            if (firstSite == null)
            {
                return site;
            }

            var firstSiteIdNode = firstSite.Descendants("Property").FirstOrDefault();
            if (firstSiteIdNode == null)
            {
                return site;
            }
            site = firstSite.Descendants("IDValue").FirstOrDefault().Value;
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
        private void DownloadLeaseDocumentsPS(List<string> leases, string fn, PSClientEntry cle)
        {
            AddMessage("Downloading lease Documents..");
            string path = Path.GetDirectoryName(fn);
            foreach (var leas in leases)
            {
                try
                {
                    DownloadDocumentsPS(cle, leas, path);
                }
                catch (Exception ex)
                {
                    AddMessage("Error downloading documents for " + cle.SiteName + "-" + cle.SiteId + "- LeaseId " + leas);
                }
            }
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
                Log.Error("Error Creating File\n" + ex);
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
                Log.Error("Error Creating File\n" + ex);
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
            var mitsColl = doc.Descendants("MITS-Collections").FirstOrDefault();
            if (mitsColl != null)
            {
                var srcOrg = mitsColl.Descendants("SourceOrganization").FirstOrDefault();
                if (srcOrg != null)
                {
                    if (srcOrg.Value.Contains("Property Solutions") || srcOrg.Value.Contains("Entrata"))
                        return VendorName.PropertySolutions;
                }
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
            ofd.Title = "Choose file";
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
                StartWorkerThread();
                AddMessage("..started Unattended Run mode ..");

            }
            else
            {
                lblRunMode.Text = string.Empty;
                lblRunMode.BackColor = Control.DefaultBackColor;
                //while (worker.IsAlive)
                //{
                //    worker.Abort();
                //}
                //if (!worker.IsAlive)
                //{
                //    AddMessage("..stopped Unattended Run mode ..");
                //}
            }
        }

        private void StartWorkerThread()
        {
            worker = new Thread(DummyExtract);
            worker.Start();
        }

        private void DummyExtract()
        {
            try
            {
                while (true)
                {
                    Log.Debug("..sleep ..");
                    Thread.Sleep(5000);
                    Log.Debug("..work ..");
                }
            }
            catch (Exception ex)
            {
                Log.Debug("Stopped Worker");
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
                Log.Info("Sitelist Response \n" + resp);
            }
            catch (Exception ex)
            {
                Log.Info("Error on Sitelist Call \n" + ex);
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

        private PSClientEntry GetClientEntryPS(string site)
        {
            XDocument doc = ccfg.GetConfig(pscliconfig);
            XElement clientElement = ccfg.GetClientElementForSiteId(doc, site);
            if (clientElement == null)
            {
                return null;
            }
            var cl = new PSClientEntry();
            cl.RrsId = clientElement.Attribute("keyword") != null ? clientElement.Attribute("keyword").Value : string.Empty;
            cl.SiteId = clientElement.Descendants("siteid").FirstOrDefault().Value;
            cl.SiteName = clientElement.Descendants("name").FirstOrDefault().Value;
            cl.Enabled = Convert.ToBoolean(clientElement.Descendants("enabled").FirstOrDefault().Value);
            cl.Token = clientElement.Descendants("token").FirstOrDefault().Value;
            cl.Subdomain = clientElement.Descendants("subdomain").FirstOrDefault().Value;
            var work =clientElement.Descendants("firstdate").FirstOrDefault(); //.Value;
            cl.FirstDate = work != null
                ? cl.FirstDate.Replace("-", "/")
                : DateTime.Today.AddDays(-30).ToString("MM-dd-yyyy");
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
                Log.Error("Error on retrieveplacementsbydate Call \n" + ex);
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

        private string GetOutputFolderPS(string folderBase, PSClientEntry clEntry)
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

        private string GetOutputSubdirPS(PSClientEntry clEntry)
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
                Log.Error("Error Creating File\n" + ex);
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
            PrepareReport();
        }

        private void PrepareReport()
        {
            FileReport fr = new FileReport();
            lblRaw.Text = txtRawXML.Text;
            lblDesc.Text = "Raw Unprocessed Files Report";
            var allFiles = fr.GetFiles(txtRawXML.Text, "*.xml");
            LoadFile(allFiles, dpCutoffDate.Value);
        }
        private void LoadFile(IEnumerable<FileInfo> allFiles, DateTime cutoff)
        {
            //foreach (var itm in allFiles)
            //{
            //    Log.DebugFmt("{0} Created {1}", Path.Combine(itm.DirectoryName, itm.Name), itm.CreationTime);
            //}
            
            lvReport.Items.Clear();
            if (lvReport.Columns.Count == 0)
            {
                LoadReportColumns();
            }
            var items = allFiles.Where(x => !x.Name.Contains("_archive_") && x.CreationTime > cutoff);
            ShowReportLines(items);
            tabCtl.SelectTab("tabReports");
        }

        private void ShowReportLines(IEnumerable<FileInfo> allFiles)
        {
            foreach (var itm in allFiles.OrderBy(i=>i.DirectoryName).ThenByDescending(i=>i.CreationTime))
            {
                var ix = itm.DirectoryName.LastIndexOf("\\");
                var subFolder = itm.DirectoryName.Substring(ix);
                var lvi = new ListViewItem(new string[]
                {
                     subFolder
                    , itm.Name
                    , itm.CreationTime.ToString("MM-dd-yyyy hh:mm.ss")
                    , itm.Length.ToString()
                });
                if (itm.Name.Contains("_archive_"))
                    lvi.ForeColor = Color.Green;
                else lvi.ForeColor = Color.DarkRed;
                lvReport.Items.Add(lvi);
            }
            lvReport.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lblItemCount.Text = "Item Count: " + lvReport.Items.Count;
        }

        private void LoadReportColumns()
        {
            lvReport.Columns.Clear();
            lvReport.Items.Clear();
            lvReport.Columns.Add("SubFolder", 300);
            lvReport.Columns.Add("FileName", 400);
            lvReport.Columns.Add("CreateDate", 120);
            lvReport.Columns.Add("FileSize", 50);

        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {
            PrepareReport();
        }

        private void releaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("readme.txt");
        }

        private void excludeHeadersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           // DoExtract();
        }

        private void includeHeadersToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void debugFormatToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void btnRefreshClientEntrata_Click(object sender, EventArgs e)
        {
            if (!CheckClients_PS())
            {
                MessageBox.Show("Error Loading Client List", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AddMessage("Entrata Clients Refreshed");
        }

        private void btnOpenEntrataLogins_Click(object sender, EventArgs e)
        {
            OpenFolder(txtEntrataLogins.Text);

        }

        private void updateSitesFromLoginFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportSitesFromLoginFile();
        }

        private void ImportSitesFromLoginFile()
        {
            string fn = GetFileToExtract(txtEntrataLogins.Text);
            if (string.IsNullOrEmpty(fn))
            {
                return;
            }
            AddMessage("Begin Updating sites from " + fn);
            UpdatePSClients(fn);
            AddMessage("End Update");
        }

        private void UpdatePSClients(string fn)
        {
            JsonClientEntry entry = null;

            try
            {
                using (var sr = new StreamReader(fn))
                {
                    var ln = string.Empty;
                    while (sr.Peek() > -1)
                    {
                        ln = sr.ReadLine();
                        entry = JsonConvert.DeserializeObject<JsonClientEntry>(ln);
                        AddUpdatePSEntry(entry);
                    }
                }
                XDocument doc = ccfg.GetConfig(pscliconfig);
                LoadClients_PS(doc);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void AddUpdatePSEntry(JsonClientEntry entry)
        {

            XDocument doc = ccfg.GetConfig(pscliconfig);
            var clients = ccfg.GetElement(doc, "clients");
            var srchRslt = clients.Descendants("siteid").Where(i => i.Value == entry.property.id.ToString());
            if (!srchRslt.Any())
            {
                XElement newClient = new XElement("client",
                    new XAttribute("keyword", string.Empty)
                    , new XElement("companyname", entry.company_name)
                    , new XElement("name", entry.property.name)
                    , new XElement("enabled", "false")
                    , new XElement("address", entry.property.address)
                    , new XElement("subdomain", entry.subdomain)
                    , new XElement("siteid", entry.property.id)
                    , new XElement("token", entry.token)
                    , new XElement("url", "https://" + entry.subdomain + ".propertysolutions.com/api/leases")
                    , new XElement("firstdate", DateTime.Today.ToString("yyyy-MM-dd"))
                    );
                clients.Add(newClient);
                AddMessage("New Client Entry created for Site: " + entry.property.name + ", PopertyId : " +
                           entry.property.id);
                doc.Save(pscliconfig);
            }
            else
            {
                foreach (var member in clients.Descendants("client"))
                {
                    var site = member.Descendants("siteid").FirstOrDefault();
                    if (site != null && site.Value == entry.property.id.ToString())
                    {
                        AddUpdateChild(member, "token", entry.token);
                        AddMessage("Client Entry token updated for Site: " + entry.property.name + ", PopertyId : " + entry.property.id);
                       // AddUpdateChild(member, "firstdate", );
                        clients.Save(pscliconfig);
                        break;
                    }
                }
            }

        }
        private void AddUpdateChild(XElement xml, string name, string val)
        {
            if (xml.Descendants(name).FirstOrDefault() == null)
            {
                var child = new XElement(name, val);
                xml.Add(child);
            }
            else
                xml.Descendants(name).First().Value = val.Trim();
        }



        private void retrieveCollectionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PSClientEntry clEntry;
            bool enabled;
            if (lvPSSites.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a site", "Error", MessageBoxButtons.OK);
                return;
            }
            AddMessage("Reteieving selected item(s)");
            foreach (ListViewItem row in lvPSSites.SelectedItems)
            {
                clEntry = GetClientEntryPS(row);
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

                tabCtl.SelectTab("tabRetrieval");
                AddMessage(string.Format("Retrieving data for RRSId {0} - {1} ", clEntry.RrsId, clEntry.SiteName));
                UpdateTokenForClientPS(clEntry);
                RetrieveLeasePS(clEntry);
            }
            AddMessage("Reteieving selected item(s) complete");

        }

        private void RetrieveLeasePS(PSClientEntry clEntry)
        {

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                var resp = GetLeasePS(clEntry.Subdomain, clEntry.Token, clEntry.SiteId, clEntry.FirstDate);
                var respXml = XDocument.Parse(resp);
                var errMsg = ParseErrorResponse(respXml);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    AddMessage(errMsg);
                    return;
                }
                var succMsg = ParseSuccessResponse(respXml);
                if (!string.IsNullOrEmpty(succMsg))
                {
                    AddMessage(succMsg);
                    return;
                }
                var respNode = respXml.Descendants("response").FirstOrDefault();
                if (LeaseFileCount(respNode) == 0)
                {
                    AddMessage("No lease files found in site " + clEntry.SiteId + ", file not created");
                    return;
                }

                string dir = GetOutputFolderPS(txtRawXML.Text, clEntry);

                string fullfname = dir + @"\" + GetOutputSubdirPS(clEntry) + "_" + DateTime.Now.ToString("MMddyyyy_HHmm") + ".xml";
                string fname = GetOutputSubdirPS(clEntry) + "_" + DateTime.Now.ToString("MMddyyyy_HHmm") + ".xml";
                SaveRawFile(respNode, fullfname);
                AddMessage("XML File saved as " + fname);
            }
            catch (Exception ex)
            {
                Log.Error("Error on getMitsCollections Call \n" + ex);
                MessageBox.Show("Error on getMitsCollections Call \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void RetrieveLeaseDocumentsPS(PSClientEntry clEntry)
        {

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                var apiResp = string.Empty;
                var resp = GetLeasePS(clEntry.Subdomain, clEntry.Token, clEntry.SiteId, clEntry.FirstDate);
                var respXml = XDocument.Parse(resp);
                var errMsg = ParseErrorResponse(respXml);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    AddMessage(errMsg);
                    return;
                }
                var succMsg = ParseSuccessResponse(respXml);
                if (!string.IsNullOrEmpty(succMsg))
                {
                    AddMessage(succMsg);
                    return;
                }
                var respNode = respXml.Descendants("response").FirstOrDefault();
                if (LeaseFileCount(respNode) == 0)
                {
                    AddMessage("No lease files found in site " + clEntry.SiteId + ", file not created");
                    return;
                }

                string dir = GetOutputFolderPS(txtRawXML.Text, clEntry);

                string fullfname = dir + @"\" + GetOutputSubdirPS(clEntry) + "_" + DateTime.Now.ToString("MMddyyyy_HHmm") + ".xml";
                string fname = GetOutputSubdirPS(clEntry) + "_" + DateTime.Now.ToString("MMddyyyy_HHmm") + ".xml";
                SaveRawFile(respNode, fullfname);
                AddMessage("XML File saved as " + fname);
            }
            catch (Exception ex)
            {
                Log.Error("Error on getMitsCollections Call \n" + ex);
                MessageBox.Show("Error on getMitsCollections Call \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }



        /*
         * Exampl Success Response
<?xml version="1.0" encoding="UTF-8"?>
<response>
  <result>
    <success>No Records Found.</success>
  </result>
</response>
         */
        private string ParseSuccessResponse(XDocument respXml)
        {
            string ret = string.Empty;
            if (respXml.Descendants("response") != null)
            {
                if (respXml.Descendants("result") != null)
                {
                    var sucNode = respXml.Descendants("success").FirstOrDefault();
                    if (sucNode != null)
                    {
                        ret = sucNode.Value;
                    }
                }
            }
            return ret;
        }

        /* Example 
         <?xml version="1.0" encoding="UTF-8"?>
    <response>
      <error>
        <code>311</code>
        <message>App doesn't have permission to the property.</message>
      </error>
    </response>
         */

        private string ParseErrorResponse(XDocument respXml)
        {
            string ret = string.Empty;
            if (respXml.Descendants("response").FirstOrDefault() != null)
            {
                if (respXml.Descendants("error").FirstOrDefault() != null)
                {
                    var errNode = respXml.Descendants("error").FirstOrDefault();
                    if (errNode.Descendants("code").FirstOrDefault() != null)
                    {
                        ret = "Error Code " + errNode.Descendants("code").FirstOrDefault().Value + " - " +
                              errNode.Descendants("message").FirstOrDefault().Value;
                    }
                }
            }
            return ret;
        }


        public string GetLeasePS(string subDomain, string token, string propId, string firstDate)
        {
            
            string ret = string.Empty;
            //string body = BuildJson_GetLease(propId);
            string body = BuildXml_GetLease(propId, firstDate);
            var apiUrl = "https://" + subDomain + _leaseUrlSuffix;
            try
            {
                //ret = APIRequest(apiUrl, body, token);
                ret = APIRequestXML(apiUrl, body, token);

            }
            catch
            {
                throw new Exception("Get user info token " + token);
            }
            return ret;
        }

        public string GetLeaseDocumentsPS(string subDomain, string token, string propId, string leaseId)
        {
            string ret = string.Empty;
            string body = BuildXml_DownloadDocuments(propId, leaseId);
            var apiUrl = "https://" + subDomain + _leaseUrlSuffix;
            try
            {
                //ret = APIRequest(apiUrl, body, token);
                ret = APIRequestXML(apiUrl, body, token);

            }
            catch( Exception ex)
            {
                Log.Error("Error Getting document, see log");
                throw ex;
            }
            return ret;
        }


        private  string BuildJson_GetLease(string propId)
        {

            string ret = 
                  "{" +
                  "\"auth\":"  +
                  "{\"type\": \"oauth\"},\"method\": {\"name\": \"getMitsCollections\"," +
                  "\"params\": {\"propertyId\" : " + "\"" + propId + "\"" + " }" +
                  "}" +
                "}";

            return ret;
        }

        private string BuildXml_GetLease(string propId, string dtFrom = "")
        {
            dtFrom = String.IsNullOrEmpty(dtFrom) ? DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy") : DateTime.Parse(dtFrom).ToString("MM/dd/yyyy");
            string xmlRequest = 
				"<request>" +
					"<auth>" +
						"<type>oauth</type>" +
					"</auth>" +
					"<method>" +
						"<name>getMitsCollections</name>" +
						"<params>" +
                        	"<propertyId>" + propId + "</propertyId>" + 
			                "<fromDate>" + dtFrom + "</fromDate>" + 
						"</params>" +
					"</method>" +
				"</request>";
            return xmlRequest;
        }

        private string BuildXml_DownloadDocuments(string propId, string leaseId)
        {
            string xmlRequest = 
                "<request>" +
					"<auth>" +
						"<type>oauth</type>" +
					"</auth>" + 
                    "<method>" +
		                    "<name>getLeaseDocuments</name>" +
		                    "<params>" +
			                    "<propertyId>" + propId + "</propertyId>" + 
			                    "<leaseId>" + leaseId + "</leaseId>" +
			                    "<fileTypesCode></fileTypesCode>" +
                                "<showDeletedFile>1</showDeletedFile>" +
		                    "</params>" +
	                    "</method>" +
                    "</request>";
            return xmlRequest;
        }

        // JSON Request not used here
        private string APIRequest(string url, string body, string token = "")
        {
            string resp = string.Empty;
            byte[] bodyByte = System.Text.Encoding.UTF8.GetBytes(body);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentLength = bodyByte.Length;
            req.ContentType = "application/json";
            if (token != string.Empty)
                req.Headers.Add("Authorization", "Bearer " + token);
            if (bodyByte.Length > 0)
            {
                using (Stream stream = req.GetRequestStream())
                    stream.Write(bodyByte, 0, bodyByte.Length);

                try
                {
                    HttpWebResponse webResp = (HttpWebResponse)req.GetResponse();
                    Stream respStream = webResp.GetResponseStream();
                    StreamReader sr = new StreamReader(respStream);
                    resp = sr.ReadToEnd();
                    webResp.Close();
                    //Log.Write("Req:" + url);
                 //   Log.Write("Resp:" + resp);
                    return resp;

                }
                catch (WebException ex)
                {
                    HttpStatusCode scode = ((HttpWebResponse)ex.Response).StatusCode;
                    resp = "Status Code " + scode + "\r\n" + ex.ToString();
                    Log.Error(resp);
                    return resp;
                }
            }
          //  Log.Write("Body is empty " + url);
            return resp;
        }

        private string APIRequestXML(string url, string body, string token = "")
        {

            Log.Debug("Entrata API Request:" + url + " Token: " + token);
            Log.Debug("Body:" + body);
            string resp = string.Empty;
            byte[] bodyByte = System.Text.Encoding.UTF8.GetBytes(body);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Timeout = 60000;
            req.Method = "POST";
            req.ContentLength = bodyByte.Length;
            req.ContentType = "APPLICATION/XML; CHARSET=UTF-8";
            if (string.IsNullOrEmpty(token))
            {
                Log.Error("Token is not present ; request not sent");
                return resp;
            }
            req.Headers.Add("Authorization", "Bearer " + token);
            if (bodyByte.Length > 0)
            {
                using (Stream stream = req.GetRequestStream())
                    stream.Write(bodyByte, 0, bodyByte.Length);

                try
                {
                    HttpWebResponse webResp = (HttpWebResponse)req.GetResponse();
                    Stream respStream = webResp.GetResponseStream();
                    StreamReader sr = new StreamReader(respStream);
                    resp = sr.ReadToEnd();

                    webResp.Close();
                    //Log.Debug("Response: " + resp);
                    return resp;
                }
                catch (WebException ex)
                {
                    HttpStatusCode scode = ((HttpWebResponse)ex.Response).StatusCode;
                    resp = "Status Code " + scode + "\r\n" + ex.ToString();
                    Log.Error(url);
                    Log.Error(body);
                    Log.Error(resp);
                    return resp;
                }
                catch (Exception ex)
                {
                    Log.Error(url);
                    Log.Error(body);
                    Log.Error(resp);
                    Log.Error(ex.ToString());
                    return resp;
                }
            }
            return resp;
        }

        private PSClientEntry GetClientEntryPS(ListViewItem row)
        {
            var cl = new PSClientEntry();
            cl.RrsId = row.SubItems[0].Text;
            cl.SiteId = row.SubItems[1].Text;
            cl.SiteName = row.SubItems[2].Text;
            cl.Enabled = Convert.ToBoolean(row.SubItems[4].Text);
            cl.Subdomain = row.SubItems[6].Text;
            cl.Token = row.SubItems[7].Text;
            cl.FirstDate = row.SubItems[8].Text.Replace("-", "/");
            return cl;
        }

        private void retrieveAllColllectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PSClientEntry clEntry;
            bool enabled;
            AddMessage("Retrieve all collections");
            foreach (ListViewItem row in lvPSSites.Items)
            {
                clEntry = GetClientEntryPS(row);
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

                tabCtl.SelectTab("tabRetrieval");
                AddMessage(string.Format("Retrieving data for RRSId {0} - {1}", clEntry.RrsId, clEntry.SiteName));
                RetrieveLeasePS(clEntry);
            }
            AddMessage("Retrievals complete");
        }

        private void btnDownloadDocPS_Click(object sender, EventArgs e)
        {

            KeyValuePair<string, string> selectedPair = (KeyValuePair<string, string>)cboSiteListPS.SelectedItem;

            var folder = txtColl.Text;
            var siteId = selectedPair.Key.Split('_')[1];
            var cboRrsId = selectedPair.Value;
            int leaseId = 0;
            int.TryParse(txtPSLeaseId.Text.Trim(), out leaseId);
            if (leaseId == 0 || string.IsNullOrEmpty(cboRrsId))
            {
                MessageBox.Show("Please select a siteId and  enter a LeaseId", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            tabCtl.SelectTab("tabRetrieval");
            PSClientEntry clEntry = GetClientEntryPS(siteId);


            Cursor.Current = Cursors.WaitCursor;
            try
            {
                var resp = GetLeaseDocumentsPS(clEntry.Subdomain, clEntry.Token, clEntry.SiteId, leaseId.ToString());
                if (string.IsNullOrEmpty(resp))
                {
                    AddMessage("Error: Empty repsonse from Doc request for client " + clEntry.SiteName + " - " + clEntry.SiteId + " - lease " + leaseId);
                    return;
                }
                var respXml = XDocument.Parse(resp);
                var errMsg = ParseErrorResponse(respXml);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    AddMessage(errMsg);
                    return;
                }
                var succMsg = ParseSuccessResponse(respXml);
                if (!string.IsNullOrEmpty(succMsg))
                {
                    AddMessage(succMsg);
                    return;
                }
                var dirOut = GetOutputFolderPS(folder, clEntry);
                AddMessage("Downloading documents for ");
                var dlFile = dirOut + @"\" + clEntry.RrsId + "_" + siteId + "_" + leaseId  + "_" + DateTime.Now.ToString("MMddyyyy_HHmm") + "_";

                SaveRawDocumentPS(respXml, dlFile);
            }
            catch (Exception ex)
            {
                Log.Error("Error Downloading Documents \n" + ex);
                MessageBox.Show("Error on API Call \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }
        /*
      <LeaseDocument Id="4851222">
        <AppliesTo>All</AppliesTo>
        <Type>Upload (Upload - STC)</Type>
        <Title>Sent_to_collections_07-06-2015_13.55.pdf</Title>
        <Name>Sent_to_collections_07-06-2015_13.55.pdf</Name>
        <Path>sent_to_collections_statements/10486947/</Path>
        <FileData>JVBERi0xLjMKMyAwIG9iago8PC9UeXBlIC9QYWdlC</FileData>
        <AddedOn>07/06/2015 13:55:46</AddedOn>
      </LeaseDocument>
         * 
         */

        private void SaveRawDocumentPS(XDocument respXml, string partName)
        {
            string name = string.Empty;
            string fileData = string.Empty;
            var ldNodes = respXml.Descendants("LeaseDocument");
            if (ldNodes.Count() == 0)
            {
                AddMessage("No LeaseDocument nodes in response");
                return;
            }

            foreach (var doc in ldNodes)
            {
                name = partName + doc.Descendants("Name").FirstOrDefault().Value;
                if (!name.EndsWith("pdf"))
                {
                    continue;
                }
                fileData = doc.Descendants("FileData").FirstOrDefault().Value;
                File.WriteAllBytes(name, Convert.FromBase64String(fileData));
                AddMessage("Document File saved as " + name);
            }
        }

        private void WriteDocumentToToFile(string stringToDecode, string fn)
        {
            stringToDecode =
                "JVBERi0xLjMKMyAwIG9iago8PC9UeXBlIC9QYWdlCi9QYXJlbnQgMSAwIFIKL1Jlc291cmNlcyAyIDAgUgovQ29udGVudHMgNCAwIFI+PgplbmRvYmoKNCAwIG9iago8PC9GaWx0ZXIgL0ZsYXRlRGVjb2RlIC9MZW5ndGggOTE3Pj4Kc3RyZWFtCnic1VhLc9MwEL73V+yBAzAZYfkh272FFjrDq0AzMAzDQbGVWGBbQVYa8u9ZySl1IO0QYk9LDp7okd1vP+1+WseHF0ceiWJYHT2dwJPnFCglngeTGTybHNmvHsyPPn8BD3LcaIeTnVu/g0doEGw99Rxwp5+SOIbECwiNYJLDw1NuxONHMPmKv4N3G6tnv5xt4FxNowPdPnZ43QD7Hd8hQOM4JAl1QM/rtVj1CtQ6dz+2/lhC/KjrD659HcQ4S0mSdg2/lNVwYaQB8ba8wU2fG8Lr7+gPsrRZSf9YOMR2fzn7i2/KIuInEDOPsMAR/pSXvM4EnC7F8TXH++V+wEiado3CgzDyfs+ayWqIct0LKcWyoT7EISMhc0gH1JL7aGlvwmLMl6BL2HvRyFzUBsZ5rkXT9EvgVrIGlFBMK4pn1orDqeC6L52LGGGsa3xgwU4je4V1/N1zRdsWtu6Gvxab9vxY4q5GG/KkkA3UykjUG/xmFMh6pnQFa7UEU3CDDwHTjSKpmZ3XwLNMLWszws1unVd2iKNcZtgN5MCn6lKMYCXLEqYCmuW0kgYXbpWzfaPAQohSFwXCzlRZisxIVTfA6xwWJc8QiKpbxJkWuTSgxUJpQ2CHEPYAiFES+g7QSQeNI0HV5doyoUWFzOQgHZN2iKgWfF3Z+sUwLJvXoQCfC0uz0pBLjXNoBDedaSHqUvBZr3SGse1fLPrXvEbHDtKrVyc72drXeIBdRZtxpy6Qq5gbyHhtiam4LJEXo44HbSX/Ei5qQuo5uH6MS28waQrBGwPPfiysxK74+j7A9EISt9f7xVIaASfszzv+DnBFKbpqU2lsUDkMH8HZGAIvCKPbcumur9b2jSGKGQkSh/4jSpvGguRlKeu5K86Ko45pW4WtEuKodqW6kqZwO0yhlvPCXKvnVXlXfA0Fv0Q5xfIFlAFdKvVN5OSQ+tpgDhPihQ7zJwtroVW1MK28GCfdV1LMF5jAmbQq3YdfmhLWavCFRDdalOvRP5i97TzClJKgjW1cTYVeKZXDBwwHNeoukn031jbzwzgmtNU5mnjwvMQUuCgULxsYXw6I9q5rJsT8S/xuxQ/7ns26Hkd9NaCxT9Ity2fjQd+zse3sOOsrihQvhahrGHWXsuECoZgCUXLTgey4HsIgJKy9Hs5nM9t5vi1ULY7/j/a7r78m/O5fE24m6M5si20SkSSxpsKNuDyht/Z+PwGAe4vPCmVuZHN0cmVhbQplbmRvYmoKMSAwIG9iago8PC9UeXBlIC9QYWdlcwovS2lkcyBbMyAwIFIgXQovQ291bnQgMQovTWVkaWFCb3ggWzAgMCA1OTUuMjggODQxLjg5XQo+PgplbmRvYmoKNSAwIG9iago8PC9UeXBlIC9Gb250Ci9CYXNlRm9udCAvSGVsdmV0aWNhCi9TdWJ0eXBlIC9UeXBlMQovRW5jb2RpbmcgL1dpbkFuc2lFbmNvZGluZwo+PgplbmRvYmoKNiAwIG9iago8PC9UeXBlIC9Gb250Ci9CYXNlRm9udCAvSGVsdmV0aWNhLUJvbGQKL1N1YnR5cGUgL1R5cGUxCi9FbmNvZGluZyAvV2luQW5zaUVuY29kaW5nCj4+CmVuZG9iago3IDAgb2JqCjw8L1R5cGUgL0ZvbnQKL0Jhc2VGb250IC9IZWx2ZXRpY2EtT2JsaXF1ZQovU3VidHlwZSAvVHlwZTEKL0VuY29kaW5nIC9XaW5BbnNpRW5jb2RpbmcKPj4KZW5kb2JqCjIgMCBvYmoKPDwKL1Byb2NTZXQgWy9QREYgL1RleHQgL0ltYWdlQiAvSW1hZ2VDIC9JbWFnZUldCi9Gb250IDw8Ci9GMSA1IDAgUgovRjIgNiAwIFIKL0YzIDcgMCBSCj4+Ci9YT2JqZWN0IDw8Cj4+Cj4+CmVuZG9iago4IDAgb2JqCjw8Ci9Qcm9kdWNlciAoRlBERiAxLjcpCi9DcmVhdG9yIChIVE1MMkZQREYgPj4gaHR0cDovL2h0bWwyZnBkZi5zZi5uZXQpCi9DcmVhdGlvbkRhdGUgKEQ6MjAxNTA3MDYxMzU1NDYpCj4+CmVuZG9iago5IDAgb2JqCjw8Ci9UeXBlIC9DYXRhbG9nCi9QYWdlcyAxIDAgUgo+PgplbmRvYmoKeHJlZgowIDEwCjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMTA3NCAwMDAwMCBuIAowMDAwMDAxNDYyIDAwMDAwIG4gCjAwMDAwMDAwMDkgMDAwMDAgbiAKMDAwMDAwMDA4NyAwMDAwMCBuIAowMDAwMDAxMTYxIDAwMDAwIG4gCjAwMDAwMDEyNTcgMDAwMDAgbiAKMDAwMDAwMTM1OCAwMDAwMCBuIAowMDAwMDAxNTg2IDAwMDAwIG4gCjAwMDAwMDE3MDkgMDAwMDAgbiAKdHJhaWxlcgo8PAovU2l6ZSAxMAovUm9vdCA5IDAgUgovSW5mbyA4IDAgUgo+PgpzdGFydHhyZWYKMTc1OAolJUVPRgo=";
            // string s = Encoding.UTF8.GetString(Convert.FromBase64String(stringToDecode));
      //      StreamWriter sw = new StreamWriter(@"c:\Docs\test.pdf");
            File.WriteAllBytes(@"c:\Docs\test.pdf", Convert.FromBase64String(stringToDecode));
            //sw.WriteLine(s);
            //sw.Flush();
            //sw.Close();

        }


        private void DownloadDocumentsPS(PSClientEntry clEntry, string leaseId, string folder )
        {
            try
            {
                var apiResp = string.Empty;
                AddMessage("Downloading documents for" + clEntry.SiteName + "-" + clEntry.SiteId + "- LeaseId " + leaseId);
                var resp = GetLeaseDocumentsPS(clEntry.Subdomain, clEntry.Token, clEntry.SiteId, leaseId);
                if (string.IsNullOrEmpty(resp))
                {
                    AddMessage("Error: Empty repsonse from Doc request for client " + clEntry.SiteName + " - " + clEntry.SiteId + "+ lease " + leaseId );
                    return;
                }
                var respXml = XDocument.Parse(resp);

                var errMsg = ParseErrorResponse(respXml);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    AddMessage(errMsg);
                    return;
                }
                var succMsg = ParseSuccessResponse(respXml);
                if (!string.IsNullOrEmpty(succMsg))
                {
                    AddMessage(succMsg);
                    return;
                }

                var dlFile = folder + @"\" + clEntry.RrsId + "_" + clEntry.SiteId + "_" + leaseId + "_" +
                             DateTime.Now.ToString("MMddyyyy_HHmm") + "_";

                var dCnt = respXml.Descendants("LeaseDocument").Count();
                AddMessage("Document count for client " + clEntry.SiteName + " - " + clEntry.SiteId + " - lease " + leaseId + " : " + dCnt  );
                SaveRawDocumentPS(respXml, dlFile);
                AddMessage("Download complete");
            }
            catch (Exception ex)
            {
                Log.Error("Error Downloading Documents \n" + ex);
                MessageBox.Show("Error on API Call \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateTokenFromLoginFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PSClientEntry clEntry;
            bool enabled;
            if (lvPSSites.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a site", "Error", MessageBoxButtons.OK);
                return;
            }
            foreach (ListViewItem row in lvPSSites.SelectedItems)
            {
                clEntry = GetClientEntryPS(row);
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

                tabCtl.SelectTab("tabRetrieval");
                AddMessage(String.Format("Updating Token for rrsId {0} - {1} (SiteId {2}) ",clEntry.RrsId, clEntry.SiteName, clEntry.SiteId));

                UpdateTokenForClientPS(clEntry);
            }
            AddMessage("Token Update complete");

        }

        private void UpdateTokenForClientPS(PSClientEntry clEntry)
        {
            var loginFolder = txtEntrataLogins.Text.Trim();
            if (string.IsNullOrEmpty(loginFolder))
            {
                AddMessage("Entrata login folder has not been configured; Please use folder location utility");
                return;
            }
            var dirInfo = new DirectoryInfo(loginFolder);
            var fileList = dirInfo.GetFiles().OrderByDescending(i => i.CreationTime);
            foreach (var fitm in fileList)
            {
                if (ProcessLoginFilePS(fitm, clEntry))
                {
                    break;
                }
            }
        }

        private bool ProcessLoginFilePS(FileInfo fitm, PSClientEntry clEntry)
        {
            JsonClientEntry entry = null;
            bool foundSite = false;
            try
            {
                using (var sr = new StreamReader(fitm.FullName))
                {
                    var ln = string.Empty;
                    while (sr.Peek() > -1)
                    {
                        ln = sr.ReadLine();
                        entry = JsonConvert.DeserializeObject<JsonClientEntry>(ln);
                        //AddUpdatePSEntry(entry);
                        var sid = entry.property.id;
                        if (sid.ToString() == clEntry.SiteId)
                        {
                            Log.Debug(string.Format("File {0} SiteId {1}, Token {2} {3} {4} ", fitm.Name, sid, entry.token, entry.token == clEntry.Token ? " = " : " <>", clEntry.Token));
                            if (entry.token != clEntry.Token)
                            {
                                UpdatePSEntryToken(entry);
                                XDocument doc = ccfg.GetConfig(pscliconfig);
                                LoadClients_PS(doc);
                            }
                            else
                            {
                                AddMessage("Client Entry token is UP-TO-DATE for Site: " + entry.property.name + ", PopertyId : " + entry.property.id);
                            }
                            foundSite = true;
                            break;
                        }
                    }
                }
                return foundSite;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void UpdatePSEntryToken(JsonClientEntry entry)
        {

            XDocument doc = ccfg.GetConfig(pscliconfig);
            var clients = ccfg.GetElement(doc, "clients");
            var srchRslt = clients.Descendants("siteid").Where(i => i.Value == entry.property.id.ToString());
            if (srchRslt.Any())
            {
                foreach (var member in clients.Descendants("client"))
                {
                    var site = member.Descendants("siteid").FirstOrDefault();
                    if (site != null && site.Value == entry.property.id.ToString())
                    {
                        AddUpdateChild(member, "token", entry.token);
                        AddMessage("Client Entry token updated for Site: " + entry.property.name + ", PopertyId : " + entry.property.id);
                        Log.DebugFmt("New Token is {0}",entry.token);
                        clients.Save(pscliconfig);
                        break;
                    }
                }
            }

        }

        // Backgroundworker methods
        //private void BwInit(BackgroundWorker bw)
        //{
        //    bw.WorkerReportsProgress = true;
        //    bw.WorkerSupportsCancellation = true;
        //    bw.DoWork += new DoWorkEventHandler(BwDoWork);
        //    bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
        //    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        //}

        //private void BwDoWork(object sender, DoWorkEventArgs e)
        //{
        //    BackgroundWorker worker = sender as BackgroundWorker;
        //worker.ReportProgress()
        //}

    }
}
