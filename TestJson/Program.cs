using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestJson
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dteNow = DateTime.Now;
            DateTime dt1970 = new DateTime(1970, 1, 1);
            double dtMS = (dteNow - dt1970).TotalMilliseconds;
            ulong  ms = Convert.ToUInt64(dtMS);

            //var n = dteNow.Replace("\"\\/Date(", "new Date(").Replace(")\\/\"", ")");
        }
    }
}
