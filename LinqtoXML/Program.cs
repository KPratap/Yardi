using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LinqtoXML
{
    class Program
    {
        static void Main(string[] args)
        {
                 XDocument doc = new XDocument(
         new XDeclaration("1.0", "utf-8", "yes"),
         new XComment("Starbuzz Customer Loyalty Data"),
         new XElement("starbuzzData",
             new XAttribute("storeName", "Park Slope"),
             new XAttribute("location", "Brooklyn, NY"),
             new XElement("person",
                 new XElement("personalInfo",
                     new XElement("name", "Janet Venutian"),
                     new XElement("zip", 11215)),
                 new XElement("favoriteDrink", "Choco Macchiato"),
                 new XElement("moneySpent", 255),
                 new XElement("visits", 50)),
             new XElement("person",
                 new XElement("personalInfo",
                     new XElement("name", "Liz Nelson"),
                     new XElement("zip", 11238)),
                 new XElement("favoriteDrink", "Double Cappuccino"),
                 new XElement("moneySpent", 150),
                 new XElement("visits", 35)),
             new XElement("person",
                 new XElement("personalInfo",
                     new XElement("name", "Matt Franks"),
                     new XElement("zip", 11217)),
                 new XElement("favoriteDrink", "Zesty Lemon Chai"),
                 new XElement("moneySpent", 75),
                 new XElement("visits", 15)),
             new XElement("person",
                 new XElement("personalInfo",
                     new XElement("name", "Joe Ng"),
                     new XElement("zip", 11217)),
                 new XElement("favoriteDrink", "Banana Split in a Cup"),
                 new XElement("moneySpent", 60),
                 new XElement("visits", 10)),
             new XElement("person",
                 new XElement("personalInfo",
                     new XElement("name", "Sarah Kalter"),
                     new XElement("zip", 11215)),
                 new XElement("favoriteDrink", "Boring Coffee"),
                 new XElement("moneySpent", 110),
                 new XElement("visits", 15))));

                 GetConfig("config.xml","yardi");
                 GetConfig("clients.xml","clients");

            //var clis = from c in    XDocument.Load("clients.xml").Descendants("client") //doc.Descendants("person")          //XElement.Load("Clients.xml").Elements("clients") 
            //            select c;
            //Console.WriteLine("Count {0}", clis.Count());
            // Execute the query
            //foreach (var cli in clis)
            //{
            //    if (cli.HasAttributes)
            //    {
            //        Console.WriteLine(cli.Attribute("Keyword").Name +  " " + cli.Attribute("Keyword").Value);
            //    }
            //}

            //Pause the application
            Console.ReadLine(); 
        }

        private static void GetConfig(string fname, string elname)
        {
            Console.WriteLine("---" + fname + "(" + elname + ") ---");
            var clis = from c in XDocument.Load(fname).Elements(elname).Descendants() //doc.Descendants("person")          //XElement.Load("Clients.xml").Elements("clients") 
                       select c;
            foreach (var v in clis)
            {
                Console.WriteLine(v.GetType() + ":" + v.Name);
                if (v.HasAttributes)
                {
                    var al = from a in v.Attributes() select a;
                    foreach (var x in al)
                        Console.WriteLine(x.Name + "," + x.Value);
                }
            }
        }
    }
}
