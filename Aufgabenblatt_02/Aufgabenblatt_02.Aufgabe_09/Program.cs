using Aufgabenblatt_02.Aufgabe_09.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_02.Aufgabe_09
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();

            person.NameChanged += 
                s => Console.WriteLine($"Name has changed: {s}");

            person.Firstname = "Donna";
            person.Lastname = "Summer";
            Console.ReadKey();
        }
    }
}
