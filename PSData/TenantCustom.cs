using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PSData
{
    public class TenantCustoms
    {
        public TenantCustoms()
        {
            IdValues = new TenantCustRecord();
        }
        public TenantCustRecord IdValues;
        public void GetIdValues(XElement src)
        {
            TenantCustRecord tcr = new TenantCustRecord();
            foreach (XElement sub in src.Descendants("CustomRecords"))
            {

                if (sub.Descendants("Record") != null)
                {
                    GetCustomRecord(sub, tcr);
                }
            }

            IdValues = tcr;
        }

        private void GetCustomRecord(XElement sub, TenantCustRecord tcr)
        {
            foreach (XElement cr in sub.Descendants("Record"))
            {
                if (cr.Element("Name") != null && cr.Element("Name").Value == "Other 1")
                {
                    tcr.Other1Name = cr.Element("Name").Value;
                    if (cr.Element("Value") != null)
                        tcr.Other1Val = cr.Element("Value").Value;
                }
                if (cr.Element("Name") != null && cr.Element("Name").Value == "Other 2")
                {
                    tcr.Other2Name = cr.Element("Name").Value;
                    if (cr.Element("Value") != null)
                        tcr.Other2Val = cr.Element("Value").Value;
                }
                if (cr.Element("Name") != null && cr.Element("Name").Value == "Other 3")
                {
                    tcr.Other3Name = cr.Element("Name").Value;
                    if (cr.Element("Value") != null)
                        tcr.Other3Val = cr.Element("Value").Value;
                }
                if (cr.Element("Name") != null && cr.Element("Name").Value == "Other 4")
                {
                    tcr.Other4Name = cr.Element("Name").Value;
                    if (cr.Element("Value") != null)
                        tcr.Other4Val = cr.Element("Value").Value;
                }
                if (cr.Element("Name") != null && cr.Element("Name").Value == "Employer")
                {
                    tcr.EmployerName = cr.Element("Name").Value;
                    if (cr.Element("Value") != null)
                        tcr.EmployerNameVal = cr.Element("Value").Value;
                }
                if (cr.Element("Name") != null && cr.Element("Name").Value == "Work Phone")
                {
                    tcr.EmployerPhone = cr.Element("Name").Value;
                    if (cr.Element("Value") != null)
                        tcr.EmployerPhoneVal = cr.Element("Value").Value;
                }
            }
        }
    }
    public class TenantCustRecord
    {
        public TenantCustRecord()
        {
            Other1Name = string.Empty;
            Other1Val = string.Empty;
            Other2Name = string.Empty;
            Other2Val = string.Empty;
            Other3Name = string.Empty;
            Other3Val = string.Empty;
            Other4Name = string.Empty;
            Other4Val = string.Empty;
        }
        public string Other1Name { get; set; }
        public string Other1Val { get; set; }
        public string Other2Name { get; set; }
        public string Other2Val { get; set; }
        public string Other3Name { get; set; }
        public string Other3Val { get; set; }
        public string Other4Name { get; set; }
        public string Other4Val { get; set; }
        public string EmployerName { get; set; }
        public string EmployerNameVal { get; set; }
        public string EmployerPhone { get; set; }
        public string EmployerPhoneVal { get; set; }
    }
}
