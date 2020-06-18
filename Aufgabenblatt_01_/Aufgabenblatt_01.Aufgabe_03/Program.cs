using Aufgabenblatt_01.Aufgabe_03.Models;
using System;

namespace Aufgabenblatt_01.Aufgabe_04
{
    class Program
    {
        static void Main()
        {
            var fahrzeuge = new[]
            {
                new Fahrzeug
                {
                    Kennzeichen = "KA TE 4711"
                },
                new PKW
                {
                    Kennzeichen = "KA SC 1894"
                },
               new LKW
               {
                   Kennzeichen = "S OS 2342"
               }
            };

            foreach (var fahrzeug in fahrzeuge)
            {
                Console.WriteLine(fahrzeug.Drive());
                if (fahrzeug is LKW lkw)
                {
                    Console.WriteLine(lkw.Drive());
                }
            }
            Console.ReadKey();
        }
    }
}
