using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RealpageData
{
    public class OpenChargesList
    {
        [XmlElement("OpenCharges")]
        public List<OpenCharges> openChgs;
    }
    public class OpenCharges
    {
        public int LeaID { get; set; } //RealPage internal unique lease identifier
        public int ReshID { get; set; } //RealPage internal unique resident ID number
        public int SiteID { get; set; } //RealPage internal unique property identifier
        public string InDispute { get; set; } //	Indicates if the charge is disputed
        public int UnitID { get; set; } //RealPage designated unique unit number
        public string TransCode { get; set; } // String	A code that is assigned to a specific type of transaction This is the code you choose to create the transaction. Each transaction code will have a default debit and credit entry associated with it. Each time this code is used, the corresponding transactions will occur.
        public string DisputeReason { get; set; } //	Dispute reason
        public string TransCodeDesc { get; set; } //	The full description of the transaction code
        public decimal Amt { get; set; } //	Dollar amount of the transaction Resident charges assigned to the RESIDENT subjournal or any subjournal that uses the “Apply late fees on delinquent subjournal balances” option are extracted.
        public DateTime TransactionDate { get; set; } //	Transaction date

    }


}
