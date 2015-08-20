using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NSConfig;
using System.Xml.Linq;
using System.Configuration;

namespace YardiFileLocator
{
    public partial class frmMain : Form
    {
        private string _folderXML = string.Empty;
        private string _folderColl = string.Empty;
        private string _folderCollFalse = string.Empty;
        private bool _isDirty = false;
        private XDocument _cfg = null;
        private XDocument _cli = null;
        string _cfgFile = string.Empty;
        cNSConfig ccfg = new cNSConfig();
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSetXMLFolder_Click(object sender, EventArgs e)
        {
            string t = SetFolder(lblRawXML.Text, txtRawXML.Text);
            if (t == string.Empty)
                return;
            txtRawXML.Text = t;
            if (_folderXML != txtRawXML.Text) _isDirty =true;
        }

        private void btnSetCollFolder_Click(object sender, EventArgs e)
        {
             string t = SetFolder(lblColl.Text, txtColl.Text);
            if (t == string.Empty)
                return;
            txtColl.Text = t;
            if (_folderColl != txtColl.Text) _isDirty = true;
        }
        private void btnCollFalse_Click(object sender, EventArgs e)
        {
            string t = SetFolder(lblCollFalse.Text, txtCollFalse.Text);
            if (t == string.Empty)
                return;
            _folderCollFalse = t;
            txtCollFalse.Text = _folderCollFalse;
            if (_folderCollFalse != txtCollFalse.Text) _isDirty = true;
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

        private string SetFolder(string title, string path)
        {
            string selpath = string.Empty;
            FolderBrowserDialog fbr = new FolderBrowserDialog();
            fbr.Description = "Set " + title + "Folder";
            fbr.ShowNewFolderButton = true;
            fbr.SelectedPath = path;
            DialogResult dr = fbr.ShowDialog();
            if (dr == DialogResult.OK)
            {
                selpath = fbr.SelectedPath;

            }
            return selpath;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                _cfgFile = ConfigurationManager.AppSettings["configfilelocation"];
                if (_cfgFile == null)
                {
                    MessageBox.Show("Unable to find 'configfilelocation' element in the configuration file; please fix and retry!", "Error", MessageBoxButtons.OK);
                    this.Close();
                }
                _cfg = ccfg.GetConfig(_cfgFile);
                this.Text += " (" + _cfgFile + ")";
                txtRawXML.Text = RetrieveElement("rawxml" );
                txtColl.Text = RetrieveElement("collections");
                txtCollFalse.Text = RetrieveElement("collectionsfalse");
                txtEntrataLoginFolder.Text = RetrieveElement("entratalogins");
                _isDirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
                this.Close();
            }
        }

        private string RetrieveElement(string ele)
        {
           string val = string.Empty;
           XElement entry =  ccfg.GetElement(_cfg, ele);
           if (entry != null)
           {
               if (entry.Attribute("location") != null)
                   val = entry.Attribute("location").Value;

           }
           return val;
        }
        private void SaveElement(string ele, string val)
        {
            XElement elConn = ccfg.GetElement(_cfg, ele);
            if (elConn != null)
            {
                ccfg.SetElementAttrib(elConn, "location", val);
            }
            else
            {
                XElement newEl = new XElement(ele
                                , new XAttribute("location", val)
                               );
                _cfg.Descendants("filelocations").First().Add(newEl);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveElement("rawxml", txtRawXML.Text);
                SaveElement("collections", txtColl.Text);
                SaveElement("collectionsfalse", txtCollFalse.Text);
                SaveElement("entratalogins", txtEntrataLoginFolder.Text);
                _cfg.Save(_cfgFile);
                MessageBox.Show("Data saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isDirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on save: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetEntrataLoginFolder_Click(object sender, EventArgs e)
        {
            string t = SetFolder(lblEntrataLogin.Text, txtEntrataLoginFolder.Text);
            if (t == string.Empty)
                return;
            txtEntrataLoginFolder.Text = t;
        }


    }
}
