using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace LinqXMLExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateXMLDoc();
            //CreateXMLFromClass();
            Query1();

        }

        private static void Query1()
        {
            var names = (from person in XDocument.Load("people.xml").Descendants("Person")
                         where int.Parse(person.Element("Age").Value) < 60
                         select person.Element("Name")).ToList();
            foreach (var c in names)
                Console.WriteLine(c.Value);
        }

        private static void CreateXMLFromClass()
        {

            Person[] people = new Person[] {
                new Person{ ID = 1, Name = "Joe", Age = 35, Job = "Manager"},
                new Person{ ID = 2, Name = "Jason", Age = 18, Job = "Software Engineer"},
                new Person{ ID = 3, Name = "Lisa", Age = 53, Job = "Bakery Owner"},
                new Person{ ID = 4, Name = "Mary", Age = 90, Job = "Nurse"},
                };

            //create xml document from already constructed Person objects

            XDocument document = new XDocument(

              new XDeclaration("1.0", "utf-8", "yes"),

              new XComment("This is a comment"),

              new XElement("People",

                  from person in people
                  select new XElement("Person", new XAttribute("ID", person.ID),
                         new XElement("Name", person.Name),
                         new XElement("Age", person.Age),
                         new XElement("Job", person.Job))
              )

          );


            document.Save("PeopleClass.xml");
        }

        private static void CreateXMLDoc()
        {
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes")
                , new XComment("This is a comment")
                , new XElement("people",
                            new XElement("Person", new XAttribute("id", 1),
                                new XElement("Name", "joe")
                                , new XElement("Age", 35)
                                , new XElement("Job", "manager")
                                ),
                                                     new XElement("Person", new XAttribute("id", 2),
                                new XElement("Name", "Jason"),
                                new XElement("Age", 18),
                                new XElement("Job", "Software Engineer")
                                ),
                             new XElement("Person", new XAttribute("id", 3),
                                new XElement("Name", "Lisa"),
                                new XElement("Age", 53),
                                new XElement("Job", "Bakery Owner")
                                ),
                             new XElement("Person", new XAttribute("id", 4),
                                new XElement("Name", "Mary"),
                                new XElement("Age", 90),
                                new XElement("Job", "Nurse")
                                )
                            )
                        );
            doc.Save("people.xml");
                                            
        }
        public class Person
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Job { get; set; }
        }
    }
}
