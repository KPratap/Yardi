using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PSData
{
    public class Addresses
    {
        public Addresses()
        {
            IdValues = new Dictionary<string, Address>();
        }
        public Dictionary<string, Address> IdValues;
        public void GetIdValues(XElement src)
        {
            foreach (XElement sub in src.Descendants("Address"))
            {
                if (sub.Attribute("AddressType") != null)
                {
                    string type = sub.Attribute("AddressType").Value;
                    Address adr = new Address();
                    if (sub.Element("Address") != null)
                        adr.Street = sub.Element("Address").Value;
                    if (sub.Element("City") != null)
                        adr.City = sub.Element("City").Value;
                    if (sub.Element("State") != null)
                        adr.State = sub.Element("State").Value;
                    if (sub.Element("PostalCode") != null)
                        adr.PostalCode = sub.Element("PostalCode").Value;
                    if (sub.Element("Email") != null)
                        adr.Email = sub.Element("Email").Value;
                    if (!IdValues.ContainsKey(type))
                        IdValues.Add(type,adr);
                }
            }
        }
    }
    public class Address
    {
        public Address()
        {
            Street = string.Empty;
            City = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            Email = string.Empty;
        }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }

    }
}
