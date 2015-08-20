using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using NSConfig;
using System.Xml.Linq;
using YardiInterface;
namespace YardiClients
{
    public partial class frmClients : Form
    {
        enum EditMode { None, Add, Edit };
        EditMode currMode = EditMode.None;
        private int cntTot = 0;
        private int cntActive = 0;
        private int cntInactive = 0;
        private enum VendorName
        {
            Yardi,
            Realpage,
            Entrata
        };

        private VendorName _vendor = VendorName.Yardi;

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
            LoadVendorCombo();
            SetDetailPanel(_vendor, true, false);
        }

        private void SetDetailPanel(VendorName _vendor, bool visible, bool enable)
        {
            switch (_vendor)
            {
                case VendorName.Yardi:
                    pnlDetl.Visible = visible;
                    pnlDetl.Enabled = enable;
                    pnlDetlRP.Visible = !visible;
                    pnlDetlRP.Enabled = enable;
                    pnlDetlEntrata.Visible = !visible;
                    pnlDetlEntrata.Enabled = enable;
                    break;
                case VendorName.Realpage:
                    pnlDetlRP.Visible = visible;
                    pnlDetlRP.Enabled = enable;
                    pnlDetl.Visible = !visible;
                    pnlDetl.Enabled = enable;
                    pnlDetlEntrata.Visible = !visible;
                    pnlDetlEntrata.Enabled = enable;
                    break;
                case VendorName.Entrata:
                    pnlDetlEntrata.Visible = visible;
                    pnlDetlEntrata.Enabled = enable;
                    pnlDetlRP.Visible = !visible;
                    pnlDetlRP.Enabled = enable;
                    pnlDetl.Visible = !visible;
                    pnlDetl.Enabled = enable;
                    break;

            }
        }

        private void LoadVendorCombo()
        {
            cboVendors.Items.Add("Yardi");
            cboVendors.Items.Add("RealPage");
            cboVendors.Items.Add("Entrata");
            cboVendors.SelectedIndex = 0;
        }

        private void GetClientList(VendorName vn)
        {
            if (_cfgFile == string.Empty)
            {
                MessageBox.Show("Unable to find \'configfilelocation\' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
                this.Close();
            }
            this.Text += " (" + _cfgFile + ")";
            try
            {
                _cli = ccfg.GetConfig(_cfgFile);
                LoadClients(vn);
            }
            catch (Exception ex)
            {
                ShowError(ex);
               // this.Close();
            }

        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
            
        }

        private void LoadClients(VendorName vn)
        {
            cntTot = cntActive = cntInactive = 0;
            lblActiveCli.Text = string.Empty;
            lblInactiveCli.Text = string.Empty;
            lblTotCli.Text = string.Empty;
            lblActiveCli.ForeColor = Color.Green;
            lblInactiveCli.ForeColor = Color.Gray;

            lblTotCli.Text = string.Empty;
            List<XElement> clients = ccfg.GetElements(_cli, "client");
            lvClients.Items.Clear();
            isLoading = true;
            if (vn == VendorName.Yardi)
            {
                AddListViewColumns(vn);
                ListViewItem lvi = null;
                foreach (XElement el in clients)
                {
                    bool enabled = Convert.ToBoolean(ccfg.GetElement(el, "enabled").Value);
                    lvi =  new ListViewItem(new string[] 
                                      {ccfg.GetElementAttrib(el,"keyword").Value
                                      ,ccfg.GetElement(el,"yardipropid").Value
                                      ,ccfg.GetElement(el,"name").Value
                                      ,ccfg.GetElement(el,"enabled").Value
                                      ,ccfg.GetElement(el,"url").Value
                                        });
                    lvi.ForeColor = enabled ? Color.Green : Color.Gray;
                    lvClients.Items.Add(lvi);
                    cntTot++;
                    cntActive = enabled ? cntActive + 1 : cntActive;
                    cntInactive = !enabled ? cntInactive + 1 : cntInactive;
                }
            }

            if (vn == VendorName.Realpage)
            {
                AddListViewColumns(vn);
                ListViewItem lvi = null;
                foreach (XElement el in clients)
                {
                    bool enabled = Convert.ToBoolean(ccfg.GetElement(el, "enabled").Value);
                    lvi = new ListViewItem(new string[] 
                                      {ccfg.GetElementAttrib(el,"keyword").Value
                                      ,ccfg.GetElement(el,"siteid").Value
                                      ,ccfg.GetElement(el,"sitename").Value
                                      ,ccfg.GetElement(el,"name").Value
                                      ,ccfg.GetElement(el,"pmcid").Value
                                      ,ccfg.GetElement(el,"enabled").Value
                                      ,ccfg.GetElement(el,"siteaddress").Value
                                      ,ccfg.GetElement(el,"email").Value
                                      ,ccfg.GetElement(el,"firstdate").Value
                                      ,ccfg.GetElement(el,"aftermoveout").Value
                                      ,ccfg.GetElement(el,"balanceowed").Value
                                      ,ccfg.GetElement(el,"ekey").Value
                                      ,ccfg.GetElement(el,"phone1").Value
                                        });
                    lvi.ForeColor = enabled ? Color.Green : Color.Gray;
                    lvClients.Items.Add(lvi);
                    cntTot++;
                    cntActive = enabled ? cntActive + 1 : cntActive;
                    cntInactive = !enabled ? cntInactive + 1 : cntInactive;

                }
            }

            if (vn == VendorName.Entrata)
            {
                AddListViewColumns(vn);
                ListViewItem lvi = null;
                foreach (XElement el in clients)
                {
                    bool enabled = Convert.ToBoolean(ccfg.GetElement(el, "enabled").Value);
                    lvi = new ListViewItem(new string[] 
                                      {ccfg.GetElementAttrib(el,"keyword").Value
                                      ,ccfg.GetElement(el,"siteid").Value
                                      ,ccfg.GetElement(el,"name").Value
                                      ,ccfg.GetElement(el,"companyname").Value
                                      ,ccfg.GetElement(el,"enabled").Value
                                      ,ccfg.GetElement(el,"address").Value
                                      ,ccfg.GetElement(el,"subdomain").Value
                                      ,ccfg.GetElement(el,"token").Value
                                      ,ccfg.GetElement(el,"firstdate").Value
                                        });
                    lvi.ForeColor = enabled ? Color.Green : Color.Gray;
                    lvClients.Items.Add(lvi);
                    cntTot++;
                    cntActive = enabled ? cntActive + 1 : cntActive;
                    cntInactive = !enabled ? cntInactive + 1 : cntInactive;

                }
            }
            lblTotCli.Text = cntTot.ToString();
            lblInactiveCli.Text = cntInactive.ToString();
            lblActiveCli.Text = cntActive.ToString();
            isLoading = false;
            lvClients.Visible = true;
            currMode = EditMode.None;
        }
        private string  ReportClients(VendorName vn)
        {
            string fn = string.Empty;
            var saveRep = new FolderBrowserDialog();
            StreamWriter rep = null;
            saveRep.Description = "Choose Folder to Save Client Report";
             var dr =  saveRep.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return fn;
            }
            try
            {
                var path = saveRep.SelectedPath;
                fn = Path.Combine(path, vn +  "_InactiveClients_" + DateTime.Now.ToString("MMddyyyy_hhmm") + ".csv");
                rep = new StreamWriter(fn);
                List<XElement> clients = ccfg.GetElements(_cli, "client");
                isLoading = true;
                string line = string.Empty;
                line = GetColumnsLine(vn);
                rep.WriteLine(line);
                if (vn == VendorName.Yardi)
                {
                    var enabled = string.Empty;
                    foreach (XElement el in clients)
                    {
                        enabled = ccfg.GetElement(el, "enabled").Value;
                        if (enabled != "true")
                            continue;
                        line = string.Join("|", new string[]
                        {
                              ccfg.GetElementAttrib(el, "keyword").Value
                            , ccfg.GetElement(el, "yardipropid").Value
                            , ccfg.GetElement(el, "name").Value
                            , ccfg.GetElement(el, "enabled").Value
                            , ccfg.GetElement(el, "url").Value
                        });
                        rep.WriteLine(line);
                    }

                }

                if (vn == VendorName.Realpage)
                {
                    ListViewItem lvi = null;
                    foreach (XElement el in clients)
                    {
                        bool enabled = Convert.ToBoolean(ccfg.GetElement(el, "enabled").Value);
                        var kwd = ccfg.GetElementAttrib(el, "keyword").Value;
                        if (!string.IsNullOrEmpty(kwd))
                            continue;
                        line = string.Join("|", new string[]
                        {
                            ccfg.GetElementAttrib(el, "keyword").Value
                            , ccfg.GetElement(el, "siteid").Value
                            , ccfg.GetElement(el, "sitename").Value
                            , ccfg.GetElement(el, "name").Value
                            , ccfg.GetElement(el, "pmcid").Value
                            , ccfg.GetElement(el, "enabled").Value
                            , ccfg.GetElement(el, "siteaddress").Value
                            , ccfg.GetElement(el, "email").Value
                            , ccfg.GetElement(el, "firstdate").Value
                            , ccfg.GetElement(el, "aftermoveout").Value
                            , ccfg.GetElement(el, "balanceowed").Value
                            , ccfg.GetElement(el, "ekey").Value
                        });
                        rep.WriteLine(line);
                    }
                }
                if (vn == VendorName.Entrata)
                {
                    ListViewItem lvi = null;
                    foreach (XElement el in clients)
                    {
                        bool enabled = Convert.ToBoolean(ccfg.GetElement(el, "enabled").Value);
                        var kwd = ccfg.GetElementAttrib(el, "keyword").Value;
                        if (!string.IsNullOrEmpty(kwd))
                            continue;
                        line = string.Join("|", new string[]
                        {
                            ccfg.GetElementAttrib(el,"keyword").Value
                            ,ccfg.GetElement(el,"siteid").Value
                            ,ccfg.GetElement(el,"name").Value
                            ,ccfg.GetElement(el,"companyname").Value
                            ,ccfg.GetElement(el,"enabled").Value
                            ,ccfg.GetElement(el,"address").Value
                            ,ccfg.GetElement(el,"subdomain").Value
                            ,ccfg.GetElement(el,"token").Value
                            ,ccfg.GetElement(el,"firstdate").Value
                        });
                        rep.WriteLine(line);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (rep != null)
                    DisposeFile(rep);
            }
            return fn;
        }
        private void DisposeFile(StreamWriter writer)
        {
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }
        private void AddListViewColumns(VendorName vn)
        {
            lvClients.Columns.Clear();
            if (vn == VendorName.Yardi)
            {
                lvClients.Columns.Add("RRS Internal Id", 120);
                lvClients.Columns.Add("Yardi Property Id", 120);
                lvClients.Columns.Add("Property Name", 160);
                lvClients.Columns.Add("Enabled", 60);
                lvClients.Columns.Add("Web Service URL", 300);
                return;
            }
            if (vn == VendorName.Realpage)
            {
                lvClients.Columns.Add("RRS Id", 50);
                lvClients.Columns.Add("Site Id", 60);
                lvClients.Columns.Add("Site Name", 160);
                lvClients.Columns.Add("PMC Name", 160);
                lvClients.Columns.Add("PMC Id", 60);
                lvClients.Columns.Add("Enabled", 60);
                lvClients.Columns.Add("Address", 300);
                lvClients.Columns.Add("Email", 300);
                lvClients.Columns.Add("FirstDate", 100);
                lvClients.Columns.Add("Days after Moveout", 50);
                lvClients.Columns.Add("Minimum Balance", 100);
                lvClients.Columns.Add("Ekey", 150);
                lvClients.Columns.Add("Phone", 80);
                return;
            }
            if (vn == VendorName.Entrata)
            {
                lvClients.Columns.Add("RRS Id", 50);
                lvClients.Columns.Add("Prop Id", 60);
                lvClients.Columns.Add("Prop Name", 160);
                lvClients.Columns.Add("PMC Name", 160);
                lvClients.Columns.Add("Enabled", 60);
                lvClients.Columns.Add("Address", 300);
                lvClients.Columns.Add("SubDomain", 300);
                lvClients.Columns.Add("Token", 300);
                lvClients.Columns.Add("FirstDate", 100);
                return;
            }
        }
        private string GetColumnsLine(VendorName vn)
        {
            if (vn == VendorName.Yardi)
            {
                return string.Join("|", new string[]
                {
                    "RRS Internal Id",
                    "Yardi Property Id",
                    "Property Name",
                    "Enabled",
                    "Web Service URL"
                }
                    );
            }
            if (vn == VendorName.Realpage)
            {
                return string.Join("|", new string[]
                {
                    "RRS Id",
                    "Site Id",
                    "Site Name",
                    "PMC Name",
                    "PMC Id",
                    "Enabled",
                    "Address",
                    "Email",
                    "FirstDate",
                    "Days after Moveout",
                    "Minimum Balance",
                    "Ekey"
                });
            }
            if (vn == VendorName.Entrata)
            {
                return string.Join("|", new string[]
                {
                    "RRS Id",
                    "Site Id",
                    "Site Name",
                    "Enabled",
                    "Address",
                    "Subdomain",
                    "Token",
                    "First Date"
                });
            }
            return string.Empty;
        }
        private void LoadDetails(ListViewItem lvi)
        {
            XElement el = null;
            string keyword = lvi.SubItems[0].Text;
            txtShortName.ReadOnly = _vendor == VendorName.Yardi;
            txtInternalId_RP.ReadOnly = !string.IsNullOrEmpty(keyword);
            txtInternalId_PS.ReadOnly = !string.IsNullOrEmpty(keyword);
            if (_vendor == VendorName.Yardi)
            {
                el = ccfg.GetElementByAttrib(_cli, "client", "keyword", keyword);
                txtShortName.Text = keyword;
                txtName.Text = ccfg.GetElement(el, "name").Value;
                txtUrl.Text = ccfg.GetElement(el, "url").Value;
                chkEnable.Checked = Convert.ToBoolean(ccfg.GetElement(el, "enabled").Value);
                txtUserId.Text = ccfg.GetElement(el, "user").Value;
                txtPassword.Text = ccfg.GetElement(el, "password").Value;
                txtDB.Text = ccfg.GetElement(el, "database").Value;
                txtServer.Text = ccfg.GetElement(el, "server").Value;
                cboPlatform.Text = ccfg.GetElement(el, "platform").Value;
                txtYardiId.Text = ccfg.GetElement(el, "yardipropid").Value;
            }
            if (_vendor == VendorName.Realpage)
            {
                string site = lvi.SubItems[1].Text;
                var clients = ccfg.GetElements(_cli, "client");
                foreach (var s in clients)
                {
                    var clsite = s.Descendants("siteid").FirstOrDefault().Value;
                    if (clsite == site)
                    {
                        el = s;
                        break;
                    }
                }
                bool enabled = Convert.ToBoolean(ccfg.GetElement(el, "enabled").Value);
                txtInternalId_RP.Text = keyword;
                txtSiteName_RP.Text = ccfg.GetElement(el, "sitename").Value;
                txtPMCName_RP.Text = ccfg.GetElement(el, "name").Value;
                txtPMCId_RP.Text = ccfg.GetElement(el, "pmcid").Value;
                chkEnable_RP.Checked = enabled;
                txtEmail_RP.Text = ccfg.GetElement(el, "email").Value;
                txtSiteId_RP.Text = ccfg.GetElement(el, "siteid").Value;
                txtSiteAddress_RP.Text = ccfg.GetElement(el, "siteaddress").Value;
                txtFirstDate.Text = ccfg.GetElement(el, "firstdate").Value;
                txtAfterMoveout.Text = ccfg.GetElement(el, "aftermoveout").Value;
                txtBalanceOwed.Text = ccfg.GetElement(el, "balanceowed").Value;
                txtEncryptionKey.Text = ccfg.GetElement(el, "ekey").Value;
                txtPhone1.Text = ccfg.GetElement(el, "phone1").Value;
            }
            if (_vendor == VendorName.Entrata)
            {
                string site = lvi.SubItems[1].Text;
                var clients = ccfg.GetElements(_cli, "client");
                foreach (var s in clients)
                {
                    var clsite = s.Descendants("siteid").FirstOrDefault().Value;
                    if (clsite == site)
                    {
                        el = s;
                        break;
                    }
                }
                bool enabled = Convert.ToBoolean(ccfg.GetElement(el, "enabled").Value);
                txtInternalId_PS.Text = keyword;
                txtPMCName_PS.Text = ccfg.GetElement(el, "companyname").Value;
                txtPropName_PS.Text = ccfg.GetElement(el, "name").Value;
                chkEnable_PS.Checked = enabled;
                txtPropId_PS.Text = ccfg.GetElement(el, "siteid").Value;
                txtAddress_PS.Text = ccfg.GetElement(el, "address").Value;
                txtToken_PS.Text = ccfg.GetElement(el, "token").Value;
                txtSubDomain_PS.Text = ccfg.GetElement(el, "subdomain").Value;
                txtFirstDate_PS.Text = ccfg.GetElement(el, "firstdate").Value;
            }
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
                MessageBox.Show("Error on Save: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void UpdateClientEntry()
        {

            XElement element = XElement.Load(_cfgFile);
            if (_vendor == VendorName.Yardi)
            {
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
            }
            if (_vendor == VendorName.Realpage)
            {
                if (element != null)
                {
                    var xml = (from member in element.Descendants("client")
                               where
                                   member.Attribute("keyword").Value == txtInternalId_RP.Text
                               select member).SingleOrDefault();

                    if (xml != null) // found client entry by keyword
                    {
                        xml.SetAttributeValue("keyword", txtInternalId_RP.Text);
                        AddUpdateChild(xml, "name", txtPMCName_RP.Text);
                        AddUpdateChild(xml, "pmcid", txtPMCId_RP.Text);
                        AddUpdateChild(xml, "enabled", chkEnable_RP.Checked.ToString().ToLower());
                        AddUpdateChild(xml, "siteid", txtSiteId_RP.Text);
                        AddUpdateChild(xml, "sitename", txtSiteName_RP.Text);
                        AddUpdateChild(xml, "siteaddress", txtSiteAddress_RP.Text);
                        AddUpdateChild(xml, "email", txtEmail_RP.Text);
                        AddUpdateChild(xml, "firstdate", txtFirstDate.Text);
                        AddUpdateChild(xml, "aftermoveout", txtAfterMoveout.Text);
                        AddUpdateChild(xml, "balanceowed", txtBalanceOwed.Text);
                        AddUpdateChild(xml, "ekey", txtEncryptionKey.Text);
                        AddUpdateChild(xml, "phone1", txtPhone1.Text);
                        element.Save(_cfgFile);
                    }
                    else // try finding entry by siteid
                    {
                        foreach (var member in element.Descendants("client"))
                        {
                            var site = member.Descendants("siteid").FirstOrDefault();
                            if (site != null && site.Value == txtSiteId_RP.Text.Trim())
                            {
                                member.SetAttributeValue("keyword", txtInternalId_RP.Text);
                                AddUpdateChild(member, "name", txtPMCName_RP.Text);
                                AddUpdateChild(member, "pmcid", txtPMCId_RP.Text);
                                AddUpdateChild(member, "enabled", chkEnable_RP.Checked.ToString().ToLower());
                                AddUpdateChild(member, "siteid", txtSiteId_RP.Text);
                                AddUpdateChild(member, "sitename", txtSiteName_RP.Text);
                                AddUpdateChild(member, "siteaddress", txtSiteAddress_RP.Text);
                                AddUpdateChild(member, "email", txtEmail_RP.Text);
                                AddUpdateChild(member, "firstdate", txtFirstDate.Text);
                                AddUpdateChild(member, "aftermoveout", txtAfterMoveout.Text);
                                AddUpdateChild(member, "balanceowed", txtBalanceOwed.Text);
                                AddUpdateChild(member, "ekey", txtEncryptionKey.Text);
                                AddUpdateChild(member, "phone1", txtPhone1.Text);
                                element.Save(_cfgFile);
                                break;
                            }
                        }
                    }
                }
            }
            // Vendor is Entrata
            if (_vendor == VendorName.Entrata)
            {
                if (element != null)
                {
                    var xml = (from member in element.Descendants("client")
                               where
                                   member.Attribute("keyword").Value == txtInternalId_PS.Text
                               select member).SingleOrDefault();

                    if (xml != null) // found client entry by keyword
                    {
                        xml.SetAttributeValue("keyword", txtInternalId_RP.Text);
                        AddUpdateChild(xml, "name", txtPMCName_RP.Text);
                        AddUpdateChild(xml, "pmcid", txtPMCId_RP.Text);
                        AddUpdateChild(xml, "enabled", chkEnable_RP.Checked.ToString().ToLower());
                        AddUpdateChild(xml, "siteid", txtSiteId_RP.Text);
                        AddUpdateChild(xml, "sitename", txtSiteName_RP.Text);
                        AddUpdateChild(xml, "siteaddress", txtSiteAddress_RP.Text);
                        AddUpdateChild(xml, "email", txtEmail_RP.Text);
                        AddUpdateChild(xml, "firstdate", txtFirstDate.Text);
                        AddUpdateChild(xml, "aftermoveout", txtAfterMoveout.Text);
                        AddUpdateChild(xml, "balanceowed", txtBalanceOwed.Text);
                        AddUpdateChild(xml, "ekey", txtEncryptionKey.Text);
                        AddUpdateChild(xml, "phone1", txtPhone1.Text);

                        xml.SetAttributeValue("keyword", txtInternalId_PS.Text);
                        AddUpdateChild(xml, "enabled", chkEnable_PS.Checked.ToString().ToLower());
                        AddUpdateChild(xml, "companyname", txtPMCName_PS.Text);
                        AddUpdateChild(xml, "name", txtPropName_PS.Text);
                        AddUpdateChild(xml, "address", txtAddress_PS.Text);
                        AddUpdateChild(xml, "siteid", txtPropId_PS.Text);
                        AddUpdateChild(xml, "token", txtToken_PS.Text);
                        AddUpdateChild(xml, "firstdate", txtFirstDate_PS.Text);
                        AddUpdateChild(xml, "subdomain", txtSubDomain_PS.Text);

                        element.Save(_cfgFile);
                    }
                    else // try finding entry by siteid
                    {
                        foreach (var member in element.Descendants("client"))
                        {
                            var site = member.Descendants("siteid").FirstOrDefault();
                            if (site != null && site.Value == txtPropId_PS.Text.Trim())
                            {
                                member.SetAttributeValue("keyword", txtInternalId_PS.Text);
                                AddUpdateChild(member, "enabled", chkEnable_PS.Checked.ToString().ToLower());
                                AddUpdateChild(member, "companyname", txtPMCName_PS.Text);
                                AddUpdateChild(member, "name", txtPropName_PS.Text);
                                AddUpdateChild(member, "address", txtAddress_PS.Text);
                                AddUpdateChild(member, "siteid", txtPropId_PS.Text);
                                AddUpdateChild(member, "token", txtToken_PS.Text);
                                AddUpdateChild(member, "firstdate", txtFirstDate_PS.Text);
                                AddUpdateChild(member, "subdomain", txtSubDomain_PS.Text);
                                element.Save(_cfgFile);
                                break;
                            }
                        }
                    }
                }
            }
            _cli = ccfg.GetConfig(_cfgFile);
            LoadClients(_vendor);
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

        private bool MissingFields()
        {
            bool rc = false;
            if (_vendor == VendorName.Yardi)
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
                rc = true;
            }
            if (_vendor == VendorName.Realpage)
            {
                if (txtInternalId_RP.Text.Trim() == string.Empty ||
                 txtPMCName_RP.Text.Trim() == string.Empty ||
                 txtPMCId_RP.Text.Trim() == string.Empty ||
                 chkEnable_RP.CheckState == CheckState.Indeterminate ||
                 txtSiteAddress_RP.Text.Trim() == string.Empty ||
                 txtSiteId_RP.Text.Trim() == string.Empty ||
                 txtSiteAddress_RP.Text.Trim() == string.Empty ||
                 txtFirstDate.Text.Trim() == string.Empty ||
                 txtAfterMoveout.Text.Trim() == string.Empty ||
                 txtBalanceOwed.Text.Trim() == string.Empty ||
                 txtEncryptionKey.Text.Trim() == string.Empty 
                    )
                rc = true;
            }

            if (_vendor == VendorName.Entrata)
            {
                if (txtInternalId_PS.Text.Trim() == string.Empty ||
                 txtPMCName_PS.Text.Trim() == string.Empty ||
                 txtPropId_PS.Text.Trim() == string.Empty ||
                 chkEnable_PS.CheckState == CheckState.Indeterminate ||
                 txtAddress_PS.Text.Trim() == string.Empty ||
                 txtPropId_PS.Text.Trim() == string.Empty ||
                 txtAddress_PS.Text.Trim() == string.Empty 
                    )
                    rc = true;
            }
            return rc;
        }


        private bool KeywordExists(string kw)
        {
            var target = _vendor == VendorName.Yardi ? txtShortName.Text.Trim() : (_vendor == VendorName.Realpage ? txtInternalId_RP.Text.Trim() : txtInternalId_PS.Text.Trim());
            return (ccfg.GetElementByAttrib(_cli, "client", "keyword", target) != null);
        }
        private void AddClientEntry()
        {

            XElement doc = XElement.Load(_cfgFile);
            if (_vendor == VendorName.Yardi)
            {
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
            }
            if (_vendor == VendorName.Realpage)
            {
                XElement newClient = new XElement("client",
                    new XAttribute("keyword", txtInternalId_RP.Text.Trim())
                    , new XElement("name", txtPMCName_RP.Text.Trim())
                    , new XElement("pmcid", txtPMCId_RP.Text.Trim())
                    , new XElement("enabled", chkEnable_RP.Checked.ToString().ToLower())
                    , new XElement("email", txtEmail_RP.Text.Trim().ToLower())
                    , new XElement("siteid", txtSiteId_RP.Text.Trim().ToLower())
                    , new XElement("sitename", txtSiteName_RP.Text.Trim().ToLower())
                    , new XElement("siteaddress", txtSiteAddress_RP.Text.Trim().ToLower())
                    , new XElement("email", txtEmail_RP.Text)
                    , new XElement("firstdate", txtFirstDate.Text)
                    , new XElement("aftermoveout", txtAfterMoveout.Text)
                    , new XElement("balanceowed", txtBalanceOwed.Text)
                    , new XElement("ekey", txtEncryptionKey.Text)
                    , new XElement("phone1", txtPhone1.Text)
                    );
                doc.Add(newClient);
                doc.Save(_cfgFile);
            }
            if (_vendor == VendorName.Entrata)
            {
                XElement newClient = new XElement("client",
                    new XAttribute("keyword", txtInternalId_PS.Text.Trim())
                    , new XElement("companyname", txtPMCName_PS.Text.Trim())
                    , new XElement("enabled", chkEnable_PS.Checked.ToString().ToLower())
                    , new XElement("siteid", txtPropId_PS.Text.Trim().ToLower())
                    , new XElement("name", txtPropName_PS.Text.Trim().ToLower())
                    , new XElement("address", txtAddress_PS.Text.Trim().ToLower())
                    , new XElement("subdomain", txtSubDomain_PS.Text.Trim().ToLower())
                    , new XElement("token", txtToken_PS.Text.Trim().ToLower())
                    );
                doc.Add(newClient);
                doc.Save(_cfgFile);
            }
            _cli = ccfg.GetConfig(_cfgFile);
            LoadClients(_vendor);

        }

        private void DeleteClientEntry(VendorName vn)
        {

            XElement element = XElement.Load(_cfgFile);
            if (vn == VendorName.Yardi)
            {
                var kwd = lvClients.SelectedItems[0].SubItems[0].Text;
                if (element != null)
                {
                    var xml = (from member in element.Descendants("client")
                               where member.Attribute("keyword").Value == lvClients.SelectedItems[0].SubItems[0].Text
                               select member).SingleOrDefault();

                    if (xml != null)
                    {
                        xml.Remove();
                        element.Save(_cfgFile);
                    }
                }
                _cli = ccfg.GetConfig(_cfgFile);
                LoadClients(_vendor);
            }
            if (vn == VendorName.Realpage)
            {
                var kwd = lvClients.SelectedItems[0].SubItems[1].Text;
                if (element != null)
                {
                    var xml = (from member in element.Descendants("client")
                               where member.Descendants("siteid").FirstOrDefault().Value == kwd
                               select member).SingleOrDefault();

                    if (xml != null)
                    {
                        xml.Remove();
                        element.Save(_cfgFile);
                    }
                }
                _cli = ccfg.GetConfig(_cfgFile);
                LoadClients(_vendor);
            }
            if (vn == VendorName.Entrata)
            {
                var kwd = lvClients.SelectedItems[0].SubItems[1].Text;
                if (element != null)
                {
                    var xml = (from member in element.Descendants("client")
                               where member.Descendants("siteid").FirstOrDefault().Value == kwd
                               select member).FirstOrDefault();

                    if (xml != null)
                    {
                        xml.Remove();
                        element.Save(_cfgFile);
                    }
                }
                _cli = ccfg.GetConfig(_cfgFile);
                LoadClients(_vendor);
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLoading) return;
            var selItems = lvClients.SelectedItems;
            if (selItems.Count > 0)
            {
                currMode = EditMode.Edit;
                ListViewItem lvi = lvClients.SelectedItems[0];
                pnlDetl.Enabled = _vendor == VendorName.Yardi;
                pnlDetlRP.Enabled = _vendor == VendorName.Realpage;
                pnlDetlEntrata.Enabled = _vendor == VendorName.Entrata;
                txtShortName.ReadOnly = _vendor == VendorName.Yardi;
                var intId = lvi.SubItems[0].Text;

                txtInternalId_RP.ReadOnly = !string.IsNullOrEmpty(intId);
                txtInternalId_PS.ReadOnly = !string.IsNullOrEmpty(intId);
                LoadDetails(lvi);
                lvClients.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            pnlDetl.Enabled = _vendor == VendorName.Yardi;
            pnlDetlRP.Enabled = _vendor == VendorName.Realpage;
            pnlDetlEntrata.Enabled = _vendor == VendorName.Entrata;
            txtShortName.ReadOnly = !pnlDetl.Enabled;
            txtInternalId_RP.ReadOnly = !pnlDetlRP.Enabled;
            txtInternalId_PS.ReadOnly = !pnlDetlEntrata.Enabled;
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
                DeleteClientEntry(_vendor);
                // Delete Entry in document
                // Save Document
                LoadClients(_vendor);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting entry: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            if (_vendor == VendorName.Yardi)
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
            }
            if (_vendor == VendorName.Realpage)
            {
                txtInternalId_RP.Clear();
                txtPMCName_RP.Clear();
                txtSiteId_RP.Clear();
                txtSiteName_RP.Clear();
                txtSiteAddress_RP.Clear();
                txtEmail_RP.Clear();
                chkEnable_RP.Checked = true;
                txtFirstDate.Clear();
                txtAfterMoveout.Clear();
                txtBalanceOwed.Clear();
                txtPMCId_RP.Clear();
                txtEncryptionKey.Clear();
                txtPhone1.Clear();
            }
            if (_vendor == VendorName.Entrata)
            {
                txtInternalId_PS.Clear();
                txtPMCName_PS.Clear();
                txtPropId_PS.Clear();
                txtPropName_PS.Clear();
                txtAddress_PS.Clear();
                txtToken_PS.Clear();
                txtFirstDate_PS.Clear();
                txtSubDomain_PS.Clear();
                chkEnable_PS.Checked = true;
            }

            currMode = EditMode.None;
        }

        private void addClientEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlDetl.Enabled = _vendor == VendorName.Yardi;
            pnlDetlRP.Enabled = _vendor == VendorName.Realpage;
            pnlDetlEntrata.Enabled = _vendor == VendorName.Entrata;
            ClearFields();
            txtShortName.ReadOnly = !pnlDetl.Enabled;
            txtInternalId_RP.ReadOnly = !pnlDetlRP.Enabled;
            txtInternalId_PS.ReadOnly = !pnlDetlEntrata.Enabled;
            txtPMCName_PS.Focus();
            txtPMCName_PS.Focus();
            lvClients.Enabled = false;
            currMode = EditMode.Add;
        }

        //private void btnOpenClients_Click(object sender, EventArgs e)
        //{
            
        //    string path = Path.GetDirectoryName(Application.ExecutablePath);
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    ofd.InitialDirectory = path;
        //    ofd.Filter = "files|*.xml";
        //    DialogResult dr = ofd.ShowDialog();
        //    if (dr != DialogResult.OK)
        //    {
        //        return;
        //    }
        //}

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

        private void copyClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLoading) return;
            var selItems = lvClients.SelectedItems;
            if (selItems.Count > 0)
            {
                currMode = EditMode.Add;
                ListViewItem lvi = lvClients.SelectedItems[0];
                pnlDetl.Enabled = _vendor == VendorName.Yardi;
                pnlDetlRP.Enabled = _vendor == VendorName.Realpage;
                pnlDetlEntrata.Enabled = _vendor == VendorName.Entrata;
                var intId = lvi.SubItems[0].Text;

                txtInternalId_RP.ReadOnly = false;
                txtShortName.ReadOnly = false;
                LoadDetails(lvi);
                lvClients.Enabled = false;
            }
        }

        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            cntTot = cntActive = cntInactive = 0;
            var clientName = cboVendors.SelectedItem.ToString().ToLower();
            var selVendor =  (VendorName) cboVendors.SelectedIndex;
            if (selVendor == VendorName.Yardi)
            {
                _cfgFile = ConfigurationManager.AppSettings["configfilelocation"];
                _vendor = VendorName.Yardi;
            }
            else if (selVendor == VendorName.Realpage)
            {
                _cfgFile = ConfigurationManager.AppSettings["rpconfigfilelocation"];
                _vendor = VendorName.Realpage;
            }
            else if (selVendor == VendorName.Entrata)
            {
                _cfgFile = ConfigurationManager.AppSettings["psconfigfilelocation"];
                _vendor = VendorName.Entrata;
            }
            else
            {
                _cfgFile = string.Empty;
            }
            GetClientList(_vendor);
        }

        private void cboVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sel = (VendorName)cboVendors.SelectedIndex;
            SetDetailPanel(sel,true,false);
        }

        private void btnSave_RP_Click(object sender, EventArgs e)
        {
            string kwd = txtInternalId_RP.Text.Trim();
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
                        ClearFields();
                        txtShortName.ReadOnly = true;
                        pnlDetlRP.Enabled = false;
                        lvClients.Enabled = true;
                        currMode = EditMode.None;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Save: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnCancel_RP_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtInternalId_RP.ReadOnly = true;
            pnlDetl.Enabled = _vendor != VendorName.Yardi;
            pnlDetlRP.Enabled = _vendor != VendorName.Realpage;
            lvClients.Enabled = true;
            currMode = EditMode.None;

        }

        private void unconfiguredClientReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fn = ReportClients(_vendor);
            if (fn != string.Empty)
            {
                MessageBox.Show("Client Report Created \n" + fn, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_PS_Click(object sender, EventArgs e)
        {
            string kwd = txtInternalId_PS.Text.Trim();
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
                        ClearFields();
                        txtShortName.ReadOnly = true;
                        pnlDetlEntrata.Enabled = false;
                        lvClients.Enabled = true;
                        currMode = EditMode.None;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on Save: \n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnCancel_PS_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtInternalId_PS.ReadOnly = true;
            pnlDetl.Enabled = _vendor != VendorName.Yardi;
            pnlDetlRP.Enabled = _vendor != VendorName.Realpage;
            pnlDetlEntrata.Enabled = _vendor != VendorName.Entrata;
            lvClients.Enabled = true;
            currMode = EditMode.None;
        }
    }
}
