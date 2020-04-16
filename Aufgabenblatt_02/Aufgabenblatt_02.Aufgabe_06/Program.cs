using Aufgabenblatt_02.Aufgabe_06.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Aufgabenblatt_02.Aufgabe_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new XmlSerializer(typeof(Employee));
            var filepath = "employee.xml";
            var employee = new Employee
            {
                FirstName = "Petra",
                LastName = "Maier",
                Gender = Gender.Female
            };

            using (var fileStream = File.Open(filepath, FileMode.Create))
            {
                serializer.Serialize(fileStream, employee);
            }
            Employee obj = null;
            using (var fileStream = File.Open(filepath, FileMode.Open))
            {
                obj = serializer.Deserialize(fileStream) as Employee 
                    ?? throw new InvalidCastException($"Object in File can't be casted to Type: {nameof(Employee)}.");
            }
            Console.WriteLine($"Vorname: {obj.FirstName}, Nachname:{obj.LastName}, Geschlecht: {obj.Gender}");
            Console.ReadKey();
        }
    }
}
