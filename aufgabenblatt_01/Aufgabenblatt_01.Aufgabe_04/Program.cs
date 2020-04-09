using Aufgabenblatt_01.Aufgabe_04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_01.Aufgabe_04
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ObjectFactory();
            var fahrzeuge = new[]
            {
                factory.CreateInstance<Fahrzeug>(),
                factory.CreateInstance<LKW>(),
                factory.CreateInstance<PKW>()
            };


        }
    }
}
