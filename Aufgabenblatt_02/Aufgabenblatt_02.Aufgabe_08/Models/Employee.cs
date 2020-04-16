using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_02.Aufgabe_08.Models
{
    [Serializable]
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        public override string ToString()
            => $"Vorname: {FirstName}, Nachname: {LastName}, Geschlecht: {Gender}";
    }
}
