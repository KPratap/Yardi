using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RealpageData
{
    public class LeaseList
    {
        [XmlElement("Lease")]
        public List<Lease> Leases;
    }

    public class Lease
    {
        public int LeaID { get; set; }	        //RealPage internal unique lease identifier
        public int ReshID { get; set; }	        //RealPage internal unique resident ID number
        public int SiteID { get; set; }	        //RealPage internal unique property identifier
        public int UnitID { get; set; }	        //RealPage designated unique unit number
        public string Status { get; set; }	    //Designates the status of the lease. 
                                                //The valid options for lease statuses are: 
                                                //2 =New Applicant (not screened) 3=Approved Applicant 4=Canceled or denied application 5=Pending lease 6-Current lease 7=Former lease
        public string Type { get; set; }	        //Designates the type of lease within a status group. The valid options are: 1=Applicant 2=Pending new renewal lease 3=Current Lease 4=On Notice to move-out 5=On Notice to transfer 6=Former resident
        public string LBDate { get; set; }	    //Date the lease is scheduled to begin
        public string LEDate { get; set; }	    //Date the lease is scheduled to end
        public string MIDate { get; set; }      //Move-in date (Estimated or Actual)
        public string NTVOnDate { get; set; }   //If the unit is on notice to vacate, then this is the date the notice was recorded.
        public string NTVForDate { get; set; }	//If the unit is on notice to vacate, then this is the date they are scheduled to move out.
        public string MODate { get; set; }	    //Actual move-out date
        public int Occ { get; set; }	        //Number of occupants used in RUBS calculation
        public string Evict { get; set; }	    //Indicates if resident is under eviction
        public decimal Rent { get; set; }       //Designates the rent per the lease contract
        public decimal CurBal { get; set; }	    //This designates the current balance of the resident, including prepaids, which will be reflected as a negative balance. Write-offs will not be reflected in this balance.
        public decimal DepPd { get; set; }	    //Designates the amount of deposit paid
        public string FASDate { get; set; }	    //The date that the account or resident information was last changed
        public string MoveOutReason { get; set; }	//Evicted for non-payment of rent This is why the resident moved out of the apartment
    }
}
