using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Xml.Linq;
using NSConfig;
using System.IO;
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
            try
            {

                if (!CheckFileLocations())
                {
                    MessageBox.Show("Error Loading File Locations", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
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
                XElement el = ccfg.GetElementByAttrib(cliFile, "client", "keyword",keyword);
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
            lvMsg.Items.Add(DateTime.Now.ToString("MM/dd/yyyy hh:mm ")  + msg);
        }

        private void DoExtract(bool outputHeader)
        {
            string fn = GetFileToExtract(txtRawXML.Text);
            if (fn == string.Empty)
                return;
            tabCtl.SelectTab("tabRetrieval");
            try
            {
                AddMessage("Extracting file " + fn);
                YardiData.YardiData yd = new YardiData.YardiData(txtRawXML.Text, txtColl.Text, txtCollFalse.Text);
                yd.OutputHeader = outputHeader;
                yd.Extract(fn);
                AddMessage("File Extracted to " + yd.foutColl);
                if (yd.CollStatusFalseCount > 0)
                    AddMessage("File Extracted to " + yd.foutCollFalse);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Extract: \n" + ex.ToString(), "Error", MessageBoxButtons.OK);
                return;
            }
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
            //XElement props = GetPropertyConfiguration(keyword);
            XElement props = GetPropertyConfiguration_Local(keyword);
            if (props == null)
            {
                AddMessage("There are no properties");
                return;
            }
            foreach ( XElement prop in props.Descendants("Property"))
            {
                    AddMessage(GetPropInfo(prop));
            }
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
    }
}
