using Aufgabenblatt_01.Aufgabe_04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_01.Aufgabe_04
{
    public class Program
    {
        static Program()
        {

        }

        public int Test  => 2;

        static void Main()
        {
            var factory1 = new ObjectFactory<Fahrzeug>();
            var factory2 = new ObjectFactory<LKW>();
            var factory3 = new ObjectFactory<PKW>();
            var fahrzeuge = new[]
            {
                factory1.CreateInstance(),
                factory2.CreateInstance(),
                factory3.CreateInstance()
            };


        }
    }
}
