using System;
using System.Collections.Generic;


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
    public class PSClientEntry
    {
        public bool Enabled { get; set; }
        public string RrsId { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string Token { get; set; }
        public string Subdomain { get; set; }
        public string FirstDate { get; set; }
    }

    /*
 * Classes used to deserialize userinfo response and create login file
 */

    public class UserProperty
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }
    public class JsonClientEntry
    {
        public int user_id { get; set; }
        public string company_name { get; set; }
        public UserProperty property { get; set; }
        public string subdomain { get; set; }
        public string token { get; set; }

    }

}
