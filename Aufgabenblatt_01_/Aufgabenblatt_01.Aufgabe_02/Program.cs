using System;

namespace Aufgabenblatt_01.Aufgabe_02
{
    class Program
    { 
        static void Main()
        {
            Console.WriteLine("Willkommen zu Aufgabe 2");
            var end = false;
            while (!end)
            {
                Console.WriteLine("Neue Eingabe:");
                Console.Write("> ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "zaehlen":
                    case "zählen":
                        Console.WriteLine("Min Wert der Zahl eingeben: ");
                        Console.Write("> ");
                        var str = Console.ReadLine();
                        if (!int.TryParse(str, out int min))
                        {
                            Console.WriteLine($"Eingabe: \"{str}\", ist keine Nummer!");
                            break;
                        }

                        Console.WriteLine("Max Wert der Zahl eingeben: ");
                        Console.Write("> ");
                        str = Console.ReadLine();
                        if (!int.TryParse(str, out int max))
                        {
                            Console.WriteLine($"Eingabe: \"{str}\", ist keine Nummer!");
                            break;
                        }

                        for (int i = min; i <= max; i++)
                        {
                            Console.WriteLine($"Nummer: {i}");
                        }
                        break;
                    case "beenden":
                        end = true;
                        break;
                    case "hilfe" :
                        Console.WriteLine("Commandos:");
                        Console.WriteLine("- zählen");
                        Console.WriteLine("- zaehlen");
                        Console.WriteLine("- beenden");
                        Console.WriteLine("- hilfe");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
