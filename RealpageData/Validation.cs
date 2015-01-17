using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RealpageData
{
    public class Validation
    {
        [XmlElement("Audit")]
        public List<Audit> Audits;
    }

    public class Audit
    {
       public string Node { get; set; } 
       public int Count { get; set; } 
    }
}
