using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_01.Aufgabe_05
{
    class Program
    {
        static void Main()
        {

            var words = new []
            {
                "Es ist heute ein sehr schöner Tag in Horb am Neckar.",
                "Diese Zeichenkette ist nicht lang, denke ich.",
                "Tabulatoren\tsind  auch Leerzeichen."
            };

            Console.WriteLine(words[0]);
            Console.WriteLine($"Words: {words[0].CountWords()}");
            Console.WriteLine(words[1]);
            Console.WriteLine($"Words: {words[1].CountWords()}");
            Console.WriteLine(words[2]);
            Console.WriteLine($"Words: {words[2].CountWords()}");
            Console.ReadKey();
        }

    }
}
