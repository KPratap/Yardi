using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LinqRWExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addToXml();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            readXml();
        }
        private void readXml()
        {
            XDocument xmlDoc = XDocument.Load("Players.xml");

            var players = from player in xmlDoc.Descendants("Player")
                          select new
                          {
                              Name = player.Element("Name").Value,
                              Team = player.Element("Team").Value,
                              Position = player.Element("Position").Value,
                          };

            txtResults.Text = "";
            foreach (var player in players)
            {
                txtResults.Text = txtResults.Text + "Name: " + player.Name + "\n";
                txtResults.Text = txtResults.Text + "Team: " + player.Team + "\n";
                txtResults.Text = txtResults.Text + "Position: " + player.Position + "\n\n";
            }

            if (txtResults.Text == "")
                txtResults.Text = "No Results.";
        }
        private void addToXml()
        {
            XDocument xmlDoc = XDocument.Load("Players.xml");

            xmlDoc.Element("Players").Add(new XElement("Player", new XElement("Name", txtName.Text),
            new XElement("Team", txtTeam.Text), new XElement("Position", cmbPosition.SelectedItem.ToString())));

            xmlDoc.Save("Players.xml");
            readXml();
        }
    }
}
