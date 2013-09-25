using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using NSConfig;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using YardiInterface;
namespace YardiClients
{
    public partial class frmClients : Form
    {
        enum EditMode { None, Add, Edit };
        EditMode currMode = EditMode.None;

        string _cfgFile = string.Empty;
        cNSConfig ccfg = new cNSConfig();
        bool isLoading = false;
        private XDocument _cli = null;
        public frmClients()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmClients_Load(object sender, EventArgs e)
        {
            currMode = EditMode.None;
            ClearFields();
            pnlDetl.Enabled = false;
            _cfgFile = ConfigurationManager.AppSettings["configfilelocation"];
            if (_cfgFile == null)
            {
                MessageBox.Show("Unable to find \'configfilelocation\' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
                this.Close();
            }
            txtClientConfig.Text = _cfgFile;
            this.Text += " (" + _cfgFile + ")";
            try
            {
                _cli = ccfg.GetConfig(_cfgFile);
                LoadClients();
            }
            catch (Exception ex)
            {
                ShowError(ex);
                this.Close();
            }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
            
        }

        private void LoadClients2()
        {
            List<XElement> clients = ccfg.GetElements(_cli, "client");
            lvClients.Items.Clear();
            isLoading = true;
            foreach (XElement el in clients)
            {

                lvClients.Items.Add(new ListViewItem(new string[] 
                                      {ccfg.GetElementAttrib(el,"keyword").Value
                                      ,ccfg.GetElementAttrib(el,"name").Value
                                      ,ccfg.GetElementAttrib(el,"intid").Value
                                      ,ccfg.GetElementAttrib(el,"yardiid").Value
                                      ,ccfg.GetElementAttrib(el,"enabled").Value
                                        }));

            }
            isLoading = false;
            currMode = EditMode.None;

        }
        private void LoadClients()
        {
            List<XElement> clients = ccfg.GetElements(_cli, "client");
            lvClients.Items.Clear();
            isLoading = true;
            foreach (XElement el in clients)
            {

                lvClients.Items.Add(new ListViewItem(new string[] 
                                      {ccfg.GetElementAttrib(el,"keyword").Value
                                      ,ccfg.GetElement(el,"yardipropid").Value
                                      ,ccfg.GetElement(el,"name").Value
                                      //,ccfg.GetElement(_cli,"intid").Value
                                      //,ccfg.GetElement(_cli,"yardiid").Value
                                      ,ccfg.GetElement(el,"enabled").Value
                                      ,ccfg.GetElement(el,"url").Value
                                     
                                        }));

            }
            isLoading = false;
            currMode = EditMode.None;

        }

        private void LoadDetails(ListViewItem lvi)
        {
            string keyword = lvi.SubItems[0].Text;
            XElement el = ccfg.GetElementByAttrib(_cli, "client", "keyword",keyword);
            txtShortName.Text = keyword;
            txtName.Text = ccfg.GetElement(el, "name").Value;
            txtUrl.Text = ccfg.GetElement(el, "url").Value;
            chkEnable.Checked  = Convert.ToBoolean(ccfg.GetElement(el, "enabled").Value);
            txtUserId.Text = ccfg.GetElement(el, "user").Value;
            txtPassword.Text = ccfg.GetElement(el, "password").Value;
            txtDB.Text = ccfg.GetElement(el, "database").Value;
            txtServer.Text = ccfg.GetElement(el, "server").Value;
            cboPlatform.Text = ccfg.GetElement(el, "platform").Value;
            txtYardiId.Text = ccfg.GetElement(el, "yardipropid").Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string kwd = txtShortName.Text.Trim();
            try
            {
                switch (currMode)
                {
                    case EditMode.Add:
                        if (MissingFields())
                        {
                            MessageBox.Show(String.Format("All Fields are required", kwd), "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (KeywordExists(kwd))
                        {
                            MessageBox.Show(String.Format("Entry with the name {0} already exists - please correct and retry", kwd), "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        AddClientEntry();
                        ClearFields();
                        txtShortName.ReadOnly = true;
                        pnlDetl.Enabled = false;
                        lvClients.Enabled = true;
                        currMode = EditMode.None;
                        break;
                    case EditMode.Edit:
                        // validate 
                        if (MissingFields())
                        {
                            MessageBox.Show(String.Format("All Fields are required", kwd), "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        // Update Client Entry
                        UpdateClientEntry();
                        // Save document
                        // Load Document
                        ClearFields();
                        txtShortName.ReadOnly = true;
                        pnlDetl.Enabled = false;
                        lvClients.Enabled = true;
                        currMode = EditMode.None;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Save: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void UpdateClientEntry()
        {

            XElement element = XElement.Load(_cfgFile);
            if (element != null)
            {
                var xml = (from member in element.Descendants("client")
                           where
                              member.Attribute("keyword").Value == txtShortName.Text
                           select member).SingleOrDefault();

                if (xml != null)
                {
                    xml.SetAttributeValue("keyword", txtShortName.Text);
                    AddUpdateChild(xml, "name", txtName.Text);
                    AddUpdateChild(xml, "enabled", chkEnable.Checked.ToString().ToLower());
                    AddUpdateChild(xml, "url", txtUrl.Text);
                    AddUpdateChild(xml, "user", txtUserId.Text);
                    AddUpdateChild(xml, "password", txtPassword.Text);
                    AddUpdateChild(xml, "server", txtServer.Text);
                    AddUpdateChild(xml, "database", txtDB.Text);
                    AddUpdateChild(xml, "platform", cboPlatform.Text);
                    AddUpdateChild(xml, "yardipropid", txtYardiId.Text);
                    element.Save(_cfgFile);
                }
            }
            _cli = ccfg.GetConfig(_cfgFile);
            LoadClients();
        }
        private void AddUpdateChild(XElement xml, string name, string val)
        {
            if (xml.Descendants(name) == null)
                xml.Add(name, val.Trim());
            else
                xml.Descendants(name).FirstOrDefault().Value = val.Trim();
        }

        private bool MissingFields()
        {
            if (txtShortName.Text.Trim() == string.Empty ||
                txtName.Text.Trim() == string.Empty ||
                chkEnable.CheckState == CheckState.Indeterminate ||
                txtDB.Text.Trim() == string.Empty ||
                txtPassword.Text.Trim() == string.Empty ||
                txtServer.Text.Trim() == string.Empty ||
                txtUserId.Text.Trim() == string.Empty ||
                cboPlatform.Text.Trim() == string.Empty ||
                cboPlatform.Text == string.Empty ||
                txtUrl.Text.Trim() == string.Empty ||
                txtYardiId.Text.Trim() == string.Empty
                )
                return true;
            else
                return false;
        }


        private bool KeywordExists(string kw)
        {
            if (ccfg.GetElementByAttrib(_cli, "client", "keyword", txtShortName.Text.Trim()) != null)
                return true;
            else
                return false;
        }
        private void AddClientEntry()
        {

            XElement doc = XElement.Load(_cfgFile);
            XElement newClient = new XElement("client",
                                                 new XAttribute("keyword", txtShortName.Text.Trim())
                                               , new XElement("name", txtName.Text.Trim())
                                               , new XElement("enabled", chkEnable.Checked.ToString().ToLower())
                                               , new XElement("url", txtUrl.Text.Trim().ToLower())
                                               , new XElement("user", txtUserId.Text.Trim().ToLower())
                                               , new XElement("password", txtPassword.Text.Trim().ToLower())
                                               , new XElement("server", txtServer.Text.Trim().ToLower())
                                               , new XElement("database", txtDB.Text.Trim().ToLower())
                                               , new XElement("platform", cboPlatform.Text.Trim())
                                               , new XElement("yardipropid", txtYardiId.Text.Trim())
                                               );
            doc.Add(newClient);
            doc.Save(_cfgFile);
            _cli = ccfg.GetConfig(_cfgFile);
            LoadClients();

        }
        /*
        private void AddClientEntry2()
        {
            XElement doc = XElement.Load(_cfgFile);
            XElement newClient = new XElement("client",
                                                new XAttribute("keyword", txtShortName.Text.Trim())
                                               , new XAttribute("name", txtName.Text.Trim())
                                               , new XAttribute("intid", txtIntId.Text.Trim())
                                               , new XAttribute("yardiid", txtYardiId.Text.Trim())
                                               , new XAttribute("enabled", txtEnabled.Text.Trim().ToLower()));
            doc.Add(newClient);
            doc.Save(_cfgFile);
            _cli = ccfg.GetConfig(_cfgFile);
            LoadClients();

        }
         * */
        private void DeleteClientEntry()
        {

            XElement element = XElement.Load(_cfgFile);
             if (element != null)
             {
                 var xml = (from member in element.Descendants("client")
                 where
                    member.Attribute("keyword").Value == lvClients.SelectedItems[0].SubItems[0].Text
                 select member).SingleOrDefault();

                 if (xml != null)
                 {
                     xml.Remove();
                     element.Save(_cfgFile);
                 }
             }
            _cli = ccfg.GetConfig(_cfgFile);
            LoadClients();

        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLoading) return;
            var selItems = lvClients.SelectedItems;
            if (selItems.Count > 0)
            {
                currMode = EditMode.Edit;
                ListViewItem lvi = lvClients.SelectedItems[0];
                pnlDetl.Enabled = true;
                txtShortName.ReadOnly = true;
                LoadDetails(lvi);
                lvClients.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtShortName.ReadOnly = true;
            pnlDetl.Enabled = false;
            lvClients.Enabled = true;
            currMode = EditMode.None;

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this entry?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }
            try
            {
                DeleteClientEntry();
                // Delete Entry in document
                // Save Document
                LoadClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting entry: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtShortName.Clear();
            txtUrl.Clear();
            txtUserId.Clear();
            txtPassword.Clear();
            txtServer.Clear();
            txtDB.Clear();
            chkEnable.Checked = true;
            cboPlatform.SelectedIndex = 0;
            txtYardiId.Clear();
           
            currMode = EditMode.None;
        }

        private void addClientEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlDetl.Enabled = true;
            ClearFields();
            txtShortName.ReadOnly = false;
            lvClients.Enabled = false;
            currMode = EditMode.Add;
        }

        private void btnOpenClients_Click(object sender, EventArgs e)
        {
            
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = path;
            ofd.Filter = "files|*.xml";
            DialogResult dr = ofd.ShowDialog();
            if (dr != DialogResult.OK)
            {
                return;
            }


        }

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkEnable.Checked)
            //    txtEnabled.Text = "true";
            //else txtEnabled.Text = "false";
        }

        private void retrieveCollectionsInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string kword = lvClients.SelectedItems[0].SubItems[0].Text;
            ListViewItem lvi = lvClients.SelectedItems[0];
            InitializeYardiInterface(lvi);
        }
        private void InitializeYardiInterface(ListViewItem lvi)
        {
            string keyword = lvi.SubItems[0].Text;
            XElement el = ccfg.GetElementByAttrib(_cli, "client", "keyword", keyword);
            YardiInterface.YardiInterface iface = new YardiInterface.YardiInterface(ccfg.GetElement(el, "url").Value, "V100055004.lic");
            iface.URL = ccfg.GetElement(el, "url").Value;
            iface.User = ccfg.GetElement(el, "user").Value;
            iface.Pwd = ccfg.GetElement(el, "password").Value;
            iface.Database = ccfg.GetElement(el, "database").Value;
            iface.Server = ccfg.GetElement(el, "server").Value;
            iface.Platform = ccfg.GetElement(el, "platform").Value;
            //iface.EntityName = "Rent Recovery Solutions";
            iface.YardiPropId = ccfg.GetElement(el, "yardipropid").Value; ;
            XElement collData = iface.GetCollectionsLeaseInfo(iface.YardiPropId);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
