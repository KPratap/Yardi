using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml.Linq;
using NSConfig;

namespace YardiConfig
{
    public partial class frmConfig : Form
    {
        string _cliFile = string.Empty;
        string _cfgFile = string.Empty;
        string _initDLFolder = string.Empty;
        private XDocument _cfg = null;
        private XDocument _cli = null;
        cNSConfig ccfg = new cNSConfig();
        bool _isDirty = false;
        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                ClearTextFields();
                _cfgFile = ConfigurationManager.AppSettings["configfilelocation"];
                if (_cfgFile == null)
                {
                    MessageBox.Show("Unable to find \'configfilelocation\' element in the configuration file; please fix and retry!","Error", MessageBoxButtons.OK);
                    this.Close();

                }
                _cfg = ccfg.GetConfig(_cfgFile);
                LoadConnectionParams();
                LoadClientParams();
                LoadDownloadParams();
                _isDirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ClearTextFields()
        {
            txtURL.Clear();
            txtPWD.Clear();
            txtUser.Clear();
            txtClientConfig.Clear();
            txtDownloadFolder.Clear();
        }
        private void LoadConnectionParams()
        {
            XElement elConn = ccfg.GetElement(_cfg, "connection");
            if (elConn != null)
            {
                txtUser.Text = ccfg.GetElementAttrib(elConn, "userid").Value;
                txtURL.Text = ccfg.GetElementAttrib(elConn, "url").Value;
                txtPWD.Text = ccfg.GetElementAttrib(elConn, "password").Value;
            }
        }
        private void SaveConnectionParams()
        {
            XElement elConn = ccfg.GetElement(_cfg, "connection");
            if (elConn != null)
            {
                ccfg.SetElementAttrib(elConn, "url", txtURL.Text.ToString());
                ccfg.SetElementAttrib(elConn, "userid", txtUser.Text.ToString());
                ccfg.SetElementAttrib(elConn, "password", txtPWD.Text.ToString());
            }
            else
            {
                XElement newEl = new XElement("connection"
                                , new XAttribute("url", txtURL.Text.Trim())
                                , new XAttribute("userid", txtUser.Text.Trim())
                                , new XAttribute("password", txtPWD.Text.Trim()));
                _cfg.Descendants("yardi").First().Add(newEl);
            }
        }
        private void LoadClientParams()
        {
            XElement elConn = ccfg.GetElement(_cfg, "clientconfig");
            if (elConn != null)
                txtClientConfig.Text = ccfg.GetElementAttrib(elConn, "filename").Value;
            else
                txtClientConfig.Clear();
        }
        private void LoadDownloadParams()
        {
            _initDLFolder = @"c:\";
            XElement elConn = ccfg.GetElement(_cfg, "downloadlocation");
            if (elConn != null)
            {
                txtDownloadFolder.Text = ccfg.GetElementAttrib(elConn, "folderpath").Value;
                _initDLFolder = txtDownloadFolder.Text;
            }
            else
                txtDownloadFolder.Clear();
        }
        private void SaveClientParams()
        {
            XElement elConn = ccfg.GetElement(_cfg, "clientconfig");
            if (elConn != null)
                ccfg.SetElementAttrib(elConn, "filename", txtClientConfig.Text.Trim());
            else
            {
                XElement newEl = new XElement("clientconfig", new XAttribute("filename", txtClientConfig.Text.Trim()));
                _cfg.Descendants("yardi").First().Add(newEl);
            }
        }
        private void SaveDownloadParams()
        {
            XElement elConn = ccfg.GetElement(_cfg, "downloadlocation");
            if (elConn != null)
            {
                ccfg.SetElementAttrib(elConn, "folderpath", txtDownloadFolder.Text);
            }
            else
            {
                XElement newEl = new XElement("downloadlocation", new XAttribute("folderpath",txtDownloadFolder.Text));
                _cfg.Descendants("yardi").First().Add(newEl);
            }
        }
        private void InitializeConfig(string p)
        {
            _cfg.Save(p);
        }
        private  void GetConfig(string fname, string elname)
        {
            string elemName;
            //Console.WriteLine("---" + fname + "(" + elname + ") ---");
            try
            {
                var rslt = from c in XDocument.Load(fname).Elements(elname).Descendants() select c;

                foreach (var v in rslt)
                {
                    elemName = v.Name.ToString();
                    if (v.HasAttributes)
                    {
                        var al = from a in v.Attributes() select a;
                        foreach (var x in al)
                        {
                            if (elemName == "connection")
                            {
                                if (x.Name == "url")
                                    txtURL.Text = x.Value;
                                if (x.Name == "userid")
                                    txtUser.Text = x.Value;
                                if (x.Name == "password")
                                    txtPWD.Text = x.Value;
                            }
                            if (elemName == "clientconfig")
                            {
                                if (x.Name == "filename")
                                {
                                    txtClientConfig.Text = x.Value;
                                    _cliFile = x.Value;
                                    _cli = ReadDoc(_cliFile);
                                }
                            }
                        }
                        //Console.WriteLine(x.Name + "," + x.Value);
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString(), "Error"); }
        }
        private XDocument ReadDoc(string file)
        {
            XDocument doc = null;
            doc = XDocument.Load(file);
            return doc;
        }
        private XDocument UpdateConfig(string elem, string val)
        {
            XDocument doc = _cfg;
            XElement xtemp = null;
            try
            {
                var rslt = doc.Descendants("connection");
                if (rslt == null)
                {
                    //doc = new XDocument(new XElement("connection"));
                    //doc.Save();
                    //,new XAttribute[] {("url",string.Empty),("userid",string.Empty),("password",string.Empty)});

                }
                xtemp = doc.Element("yardi").Descendants("connection").First();

                xtemp.SetAttributeValue("url", txtURL.Text.Trim());
                xtemp.SetAttributeValue("userid", txtUser.Text.Trim());
                xtemp.SetAttributeValue("password", txtPWD.Text.Trim());
                doc.Save(_cfgFile);
                _cfg = ReadDoc(_cfgFile);return doc;
            }
            
            catch (Exception ex)
            { MessageBox.Show(ex.ToString(), "Error"); return doc; }
        }
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            if (!_isDirty) return;
            SaveConnectionParams();
            SaveClientParams();
            SaveDownloadParams();
            _cfg.Save(_cfgFile);
            _isDirty = false;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (_isDirty)
            {
                DialogResult dr = MessageBox.Show("There are unsaved changes. Would you like to exit without saving?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                    this.Close();
                return;
            }
            else
                this.Close();
        }

        private void SetChangedFlag(object sender, EventArgs e)
        {
            _isDirty = true;
        }

        private void btnSetDownloadFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbr = new FolderBrowserDialog();
            fbr.ShowNewFolderButton = true;
            fbr.SelectedPath = _initDLFolder;
            fbr.Description = "Choose Download Location";
            DialogResult dr = fbr.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtDownloadFolder.Text = fbr.SelectedPath;
                
            }
            _initDLFolder = fbr.SelectedPath;
        }

        private void SaveSelectedPath()
        {
            
        }

    }
}
