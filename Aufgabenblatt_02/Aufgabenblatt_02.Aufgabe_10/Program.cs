using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_02.Aufgabe_10
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=beispiel.mdb"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT NAME, VORNAME " +
                                          "FROM VERTRETER";
                    var result = await command.ExecuteReaderAsync();
                    if (result.HasRows)
                    {
                        while (await result.ReadAsync())
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write($"Name: {result.GetString(0)}\t");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine($"Vorname: {result.GetString(1)}");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }

                    
                }
            }
            Console.ReadKey();
        }
    }
}
