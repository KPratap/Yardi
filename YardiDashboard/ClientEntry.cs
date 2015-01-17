using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YardiDashboard.RPXCollections;

namespace YardiDashboard
{
    public class ClientEntry
    {
        public bool Enabled { get; set; }
        public string RrsId { get; set; }
        public string PmcId { get; set; }
        public DateTime FirstDate { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string Ekey { get; set; }
        public string AfterMoveout { get; set; }
        public string BalanceOwed { get; set; }
        public string Phone1 { get; set; }
    }
}
