using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RealpageData
{
    public class ResidentList
    {
        [XmlElement("Resident")] 
        public List<Resident> Residents;
    }

    public class Resident
    {
        public int ReshID { get; set; } //RealPage internal unique resident household ID
        public int ResmID { get; set; } //Resident member identifier
        public int SiteID { get; set; } //Property ID
        public int UnitID { get; set; } //RealPage internal unique unit number
        public int LeaID { get; set; } //RealPage internal unique lease identifier
        //Resident mailing name as identified in OneSite. This field can be populated during the deposit disposition process by filling in the Mail to field.
        public string MailName { get; set; }
        public string FirstName { get; set; } //Designates the first name of the Head of Household
        public string LastName { get; set; } //Designates the last name of the Head of Household
        public string SSNo { get; set; } //Designates the Social Security number of the resident
        public string DrivLicNo { get; set; } //Designates the ID number of the resident
        public string DoB { get; set; } //Designates the date of birth of the resident
        public int LatePmtCnt { get; set; } //Number of times the resident has been late
        public int NSFCnt { get; set; } //Number of times the resident has had an NSF
        public int LastLate { get; set; } //Fiscal period in which the resident was last late
        public string NoChk { get; set; } //(Y/N) Y - Indicates to not accept checks from the resident
        public string InColl { get; set; } //(Y/N) Y - Indicates the resident has been turned over to collections
        public string Status { get; set; } //Indicates the status of the resident; H = Head of Household, etc.
        public bool Signer { get; set; } //Designates if the resident is a lease signer; 1 = yes, 0 = no
        public bool Occupant { get; set; } //Designates if the resident is an Occupant; 1 = yes, 0 = no
        public bool Cosigner { get; set; } //Designates if the resident is a lease cosigner; 1 = yes, 0 = no
        public bool Guarantor { get; set; } //Designates if the resident is a lease guarantor; 1 = yes, 0 = no
        public string EmployerName { get; set; } //	Indicates the name of the resident’s employer
        public string EmployerAddress1 { get; set; } //Indicates the address of the resident’s employer
        public string EmployerAddress2 { get; set; } //Indicates the address line 2 of the resident’s employer
        public string EmployerCity { get; set; } //Indicates the city of the resident’s employer
        public string EmployerState { get; set; } //Indicates the state of the resident’s employer
        public string EmpZip { get; set; } //Indicates the ZIP Code of the resident’s employer
        public string EmpPhone1 { get; set; } //Indicates the phone number of the resident’s employer
        public string EmpPhone2 { get; set; } //Indicates the secondary phone number of the resident’s employer
        public string EmpEmail { get; set; } //Indicates the employer e-mail of the resident’s employer
        public string JobTitleName { get; set; } //Indicates the job title of the resident
       // public string JobStartDate { get; set; } //Indicates the date the resident began the job
       // public string JobEndDate { get; set; } //Indicates the date the resident ended the job
        public string SupervisorName { get; set; } //Indicates the supervisor of the resident at the job
        public string supphoneext1 { get; set; } //Indicates the phone number of the supervisor
        public string AnnIncome { get; set; } //Designates the annual income with the employer
        public string supphoneext2 { get; set; } //Indicates the secondary phone number of the supervisor
        public int ExtractCount { get; set; } //How many times the resident’s information has been extracted
        public BillingAddress BillingAddress { get; set; }
        public RegularAddress RegularAddress { get; set; }
        public EmergAddress EmergAddress { get; set; }
        public ForwardingAddress ForwardingAddress { get; set; }
        public ScreeningAddress ScreeningAddress { get; set; }

        //public string ResidentMemberName { get; set; }	//Name of any additional household members that are not the Head of Household; data does not have this
        // Instead it has the following list.
        public List<ResidentMember> ResidentMembers { get; set; }
    }




    public class BillingAddress
    {
        public string Adr1 { get; set; } //Resident Billing Address line 1
        public string Adr2 { get; set; } //Resident billing address line 2
        public string City { get; set; } //Billing city address
        public string St { get; set; } //Billing state address
        public string Zip { get; set; } //Billing zip code
        public string Country { get; set; } //Resident billing country
        public string Phone1 { get; set; } //Phone number
        public string Phone2 { get; set; } //Phone number 2
        public string Ext1 { get; set; } //Phone number extension 1
        public string Ext2 { get; set; } //Phone number extension 2
        public string Email { get; set; } //Resident e-mail address
        public string Fax { get; set; } //Fax number
        public string Cell { get; set; } //Resident cell phone number
    }
    public class RegularAddress
    {
        public string Adr1 { get; set; } //Resident Billing Address line 1
        public string Adr2 { get; set; } //Resident billing address line 2
        public string City { get; set; } //Billing city address
        public string St { get; set; } //Billing state address
        public string Zip { get; set; } //Billing zip code
        public string Country { get; set; } //Resident billing country
        public string Phone1 { get; set; } //Phone number
        public string Phone2 { get; set; } //Phone number 2
        public string Ext1 { get; set; } //Phone number extension 1
        public string Ext2 { get; set; } //Phone number extension 2
        public string Email { get; set; } //Resident e-mail address
        public string Fax { get; set; } //Fax number
        public string Cell { get; set; } //Resident cell phone number
    }

    public class ForwardingAddress
    {
        public string Adr1 { get; set; } //Resident Billing Address line 1
        public string Adr2 { get; set; } //Resident billing address line 2
        public string City { get; set; } //Billing city address
        public string St { get; set; } //Billing state address
        public string Zip { get; set; } //Billing zip code
        public string Country { get; set; } //Resident billing country
        public string Phone1 { get; set; } //Phone number
        public string Phone2 { get; set; } //Phone number 2
        public string Ext1 { get; set; } //Phone number extension 1
        public string Ext2 { get; set; } //Phone number extension 2
        public string Email { get; set; } //Resident e-mail address
        public string Fax { get; set; } //Fax number
        public string Cell { get; set; } //Resident cell phone number
    }
    public class EmergAddress
    {
        public string EmergFirstName { get; set; } //	The first name of the emergency contact
        public string EmergLastName { get; set; } //	The last name of the emergency contact
        public string EmergRelationship { get; set; } //	Designates the relationship of the emergency contact
        public string Adr1 { get; set; } //Resident Billing Address line 1
        public string Adr2 { get; set; } //Resident billing address line 2
        public string City { get; set; } //Billing city address
        public string St { get; set; } //Billing state address
        public string Zip { get; set; } //Billing zip code
        public string Country { get; set; } //Resident billing country
        public string Phone1 { get; set; } //Phone number
        public string Phone2 { get; set; } //Phone number 2
        public string Ext1 { get; set; } //Phone number extension 1
        public string Ext2 { get; set; } //Phone number extension 2
        public string Email { get; set; } //Resident e-mail address
        public string Fax { get; set; } //Fax number
        public string Cell { get; set; } //Resident cell phone number

    }

    public class ScreeningAddress
    {
        public int ScreeningAddressAddrKeyID { get; set; } //Screening address key ID
        public int AppGID { get; set; } //AppGID
        public string AddrType { get; set; } //Screening address type
        public string AddrStreetNumber { get; set; } //Screening street number address
        public string AddrDirectionName { get; set; } //	Screening address direction name
        public string AddrStreetName { get; set; } //Screening street name address
        public string AddrApartmentNumber { get; set; } //Screening apartment number address
        public string AddrStreetType { get; set; } //Screening street type address
        public string AddrAddress1 { get; set; } //Screening address 1
        public string AddrAddress2 { get; set; } //Screening address 2
        public string AddrCityName { get; set; } //Screening city name address
        public string AddrState { get; set; } //Screening state location
        public string AddrZip { get; set; } //Screening address zip
        public string AddrCountry { get; set; } //Screening address country
        //public DateTime AddrStartDate { get; set; }
        //Start Date when the resident started to stay in the screening address specified
        //public DateTime AddrEndDate { get; set; }
        //End Date when the resident stopped to stay in the screening address specified

    }

    public class ResidentMember
    {
        public int ResmID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string SSNo { get; set; }
        public string DrivLicNo { get; set; }
        public string DoB { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress1 { get; set; }
        public string EmployerAddress2 { get; set; }
        public string EmployerCity { get; set; }
        public string EmployerState { get; set; }
        public string EmpZip { get; set; }
        public string EmpPhone1 { get; set; }
        public string EmpPhone2 { get; set; }
        public string EmpEmail { get; set; }
        public string JobTitleName { get; set; }
        public string JobStartDate { get; set; }
        public string JobEndDate { get; set; }
        public string SupervisorName { get; set; }
        public string supphoneext1 { get; set; }
        public string supphoneext2 { get; set; }
        public bool Signer { get; set; }
        public bool Cosigner { get; set; }
        public bool Guarantor { get; set; }
        public string Phone1 { get; set; }
        public string Ext1 { get; set; }
        public string Phone2 { get; set; }
        public string Ext2 { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Cell { get; set; }
        public RMRegularAddresses RMRegularAddresses { get; set; }
    }

    public class RMRegularAddresses
    {
        [XmlElement("RMRegularAddress")] 
        public List<RMRegularAddress> RMRegularAddressesesList { get; set; }
    }
    public class RMRegularAddress
    {
        public string Adr1 { get; set; }
        public string Adr2 { get; set; }
        public string City { get; set; }
        public string St { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Ext1 { get; set; }
        public string Ext2 { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Cell { get; set; }
    }
}