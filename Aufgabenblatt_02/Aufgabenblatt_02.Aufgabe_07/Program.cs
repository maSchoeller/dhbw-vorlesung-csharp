using Aufgabenblatt_02.Aufgabe_07.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Aufgabenblatt_02.Aufgabe_07
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new XmlSerializer(typeof(SalesRepresentative[]));
            var filepath = "vertreter.xml";
            SalesRepresentative[] data = null;
            using (var fileStream = File.Open(filepath, FileMode.Open))
            {
                data = serializer.Deserialize(fileStream) as SalesRepresentative[]
                    ?? throw new InvalidCastException($"Object in File can't be casted to Type: {typeof(SalesRepresentative[]).Name}.");
            }

            //Write Result 1
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Liste aller Vertreter aus Baden-Württemberg, sortiert nach Umsatz:");
            Console.WriteLine();
            Console.ResetColor();
            
            var res1 = GetSalesRepsFromBadenWuerttemberg(data);
            //Geforderte Alternative, jedoch nicht so schön.
            //var res1List = new List<(string FirstName, string LastName, decimal SalesVolume, string Company)>(res1);
            //res1List.ForEach(sale => Console.WriteLine($"Name:{sale.FirstName} {sale.LastName}, Firma: {sale.Company}, Umsatz: {sale.SalesVolume}"));
            foreach (var sale in res1)
                Console.WriteLine($"Name:{sale.FirstName} {sale.LastName}, Firma: {sale.Company}, Umsatz: {sale.SalesVolume}");

            //Write Result 2
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("Liste aller Vertreter, " +
                "gruppiert nach Unternehmen, " +
                "die mehr als 10.000,- Umsatz machen, " +
                "sortiert nach Unternehmen: ");
            Console.WriteLine();
            Console.ResetColor();
            var res2 = GetSalesRepsGroupedByCompany(data);
            foreach (var saleCompany in res2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Firma: {saleCompany.Key}");
                Console.ResetColor();
                foreach (var sale in saleCompany)
                {
                    Console.WriteLine($"   {sale}");
                }
            }

            //Write Result 3
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("Liste der 10 Vertreter, " +
                "die am wenigsten Umsatz machen, " +
                "sortiert nach Umsatz:");
            Console.WriteLine();
            Console.ResetColor();
            var res3 = GetTopTenLosers(data);
            foreach (var sale in res3)
                Console.WriteLine(sale.ToString());

            Console.ReadKey();
        }

        private static IEnumerable<(string FirstName, string LastName, decimal SalesVolume, string Company)>
            GetSalesRepsFromBadenWuerttemberg(IEnumerable<SalesRepresentative> salesRepresentatives)
        {
            return salesRepresentatives
                .Where(s => s.Area != "Baden-Württemberg")
                .Select(s => (s.FirstName, s.LastName, s.SalesVolume, s.Company))
                .OrderByDescending(s => s.SalesVolume);
        }

        private static IEnumerable<IGrouping<string, SalesRepresentative>> GetSalesRepsGroupedByCompany(
            IEnumerable<SalesRepresentative> salesRepresentatives)
        {
            return salesRepresentatives.GroupBy(s => s.Company)
                .Where(sg => sg.Sum(s => s.SalesVolume) > 10_000);
        }

        private static IEnumerable<SalesRepresentative> GetTopTenLosers(
            IEnumerable<SalesRepresentative> salesRepresentatives)
        {
            return salesRepresentatives
                .OrderBy(s => s.SalesVolume)
                .Take(10);
        }
    }
}
