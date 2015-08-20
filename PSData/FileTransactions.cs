using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PSData
{
    public class FileTransactions
    {
        public FileTransactions()
        {
            IdValues = new Dictionary<string, FileTransaction>();
            AssignedAmount = string.Empty;
        }
        public string AssignedAmount;
        private decimal assignedAmt = 0;
        public Dictionary<string, FileTransaction> IdValues;
        public void GetIdValues(XElement src)
        {
           
            foreach (XElement sub in src.Descendants("FileTransactions"))
            {
                string id = sub.Element("Identification").Element("IDValue").Value;
                FileTransaction ftr = new FileTransaction();
                ftr.TransId = id;
                if (sub.Element("TransDate") != null)
                    ftr.TransDate = sub.Element("TransDate").Value;
                if (sub.Element("TransTypeDesc") != null)
                    ftr.TransTypeDesc = sub.Element("TransTypeDesc").Value;
                if (sub.Element("TransType") != null)
                    ftr.TransType = sub.Element("TransType").Value;
                if (sub.Element("TransAmount") != null)
                    ftr.TransAmount = sub.Element("TransAmount").Value;
                if (sub.Element("OpenAmount") != null)
                {
                    ftr.OpenAmount = sub.Element("OpenAmount").Value;
                    assignedAmt += Convert.ToDecimal(ftr.OpenAmount);
                }
                if (!IdValues.ContainsKey(id))
                    IdValues.Add(id, ftr);
             //   else 
                //    Log.WarnFmt("Skipped adding dup key {0} in element {1}",id,sub);
                if (sub.Descendants("CustomRecords") != null)
                {
                    GetCustomRecord(sub, ftr);
                }
            }
            AssignedAmount = assignedAmt.ToString();
            
        }

        private void GetCustomRecord(XElement sub, FileTransaction ftr)
        {
            foreach (XElement cr in sub.Descendants("CustomRecords").Descendants("Record"))
            {
                    if (cr.Element("Name") != null && cr.Element("Name").Value == "Charge Code Desc")
                    {
                        ftr.CustChargeCode = cr.Element("Name").Value;
                        if (cr.Element("Value") != null)
                            ftr.CustChargeCodeDesc = cr.Element("Value").Value;
                    }
            }
        }
    }
    public class FileTransaction
    {
        public FileTransaction()
        {
            TransId = string.Empty;
            TransDate = string.Empty;
            TransType = string.Empty;
            TransTypeDesc = string.Empty;
            TransAmount = string.Empty;
            OpenAmount = string.Empty;
            CustChargeCode = string.Empty;
            CustChargeCodeDesc = string.Empty;
        }
        public string TransId { get; set; }
        public string TransDate { get; set; }
        public string TransType { get; set; }
        public string TransTypeDesc { get; set; }
        public string TransAmount { get; set; }
        public string OpenAmount { get; set; }
        public string CustChargeCodeDesc { get; set; }
        public string CustChargeCode { get; set; }
    }
    public class CustomRecord
    {
        public CustomRecord()
        {
            TransDate = string.Empty;
            TransType = string.Empty;
            TransTypeDesc = string.Empty;
            TransAmount = string.Empty;
            TransOpenAmount = string.Empty;
        }
        public string TransDate { get; set; }
        public string TransType { get; set; }
        public string TransTypeDesc { get; set; }
        public string TransAmount { get; set; }
        public string TransOpenAmount { get; set; }
    }
}
