using Aufgabenblatt_02.Aufgabe_08.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_02.Aufagbe_08
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee = new Employee
            {
                FirstName = "Herbert",
                LastName = "Müller",
                Gender = Gender.Male
            };
            Employee obj = null;
            using (var memStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memStream, employee);
                memStream.Position = 0;
                obj = formatter.Deserialize(memStream) as Employee
                    ?? throw new InvalidCastException($"Object in File can't be casted to Type: {nameof(Employee)}.");
            }
            Console.WriteLine($"Old: {employee}");
            Console.WriteLine($"New: {employee}");
            Console.ReadKey();
        }
    }
}
