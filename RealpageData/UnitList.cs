using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CAPICOM;

namespace RealpageData
{
    public class UnitList
    {
        [XmlElement("Unit")]
        public List<Unit> Units;
    }

    public class Unit
    {
        public int UnitID { get; set; }	        //Property management ID
        public int SiteID { get; set; }	    //Property ID
        public int BldgID { get; set; }	    //Property number
		public int FpID { get;set; }
        public string BldgName { get; set; }	//Property name
		public string UnitNo {get; set; }
        public string Adr1 { get; set; }	    //Property address line 1
        public string Adr2 { get; set; }	    //Property address line 2
        public string City { get; set; }	    //Property city
        public string State { get; set; }	    //Property state
        public string Zip { get; set; }	        //Property zip
        public string County { get; set; }	    //Property county
        public string Country { get; set; }	    //Property country
    }

	/* sample data
		          <UnitID>223</UnitID>
          <SiteID>1225107</SiteID>
          <BldgID>9</BldgID>
          <FpID>9</FpID>
          <BldgNo>2827</BldgNo>
          <BldgName></BldgName>
          <UnitNo>304</UnitNo>
          <Adr1>2827 Bloomfield Ln Unit 304</Adr1>
          <Adr2></Adr2>
          <City>Wilmington</City>
          <St>NC</St>
          <Zip>28412-6642</Zip>
          <County>New Hanover County</County>
          <Country>US</Country>
		  */
}
