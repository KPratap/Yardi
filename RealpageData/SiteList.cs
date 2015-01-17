using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RealpageData
{
    public class SiteList
    {
        [XmlElement("Site")]
        public List<Site> Sites;
    }

    public class Site
    {
        public int EntID { get; set; }	        //Property management ID
        public string EntName { get; set; }		//Property management company name
        public string SiteID { get; set; }	    //Property ID
        public string PropNo { get; set; }	    //Property number
        public string SiteName { get; set; }	//Property name
        public string Adr1 { get; set; }	    //Property address line 1
        public string Adr2 { get; set; }	    //Property address line 2
        public string City { get; set; }	    //Property city
        public string State { get; set; }	    //Property state
        public string Zip { get; set; }	        //Property zip
        public string County { get; set; }	    //Property county
        public string Country { get; set; }	    //Property country
        public string Phone1 { get; set; }	    //Property phone number
        public string Phone2 { get; set; }	    //Property phone number 2
        public string Fax { get; set; }	        //Property fax number
        public string Email { get; set; }	    //Property email address
        public string Web { get; set; }	        //Property web address
        public string PropMgr { get; set; }	    //Property manager
       // public DateTime PropDate { get; set; }	//The fiscal property date used in OneSite
        //public DateTime SysDate { get; set; }	//The system date
    }
}
