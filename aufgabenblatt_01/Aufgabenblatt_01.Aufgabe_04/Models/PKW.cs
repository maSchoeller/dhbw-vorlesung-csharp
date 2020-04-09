using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_01.Aufgabe_04.Models
{
    public class PKW : Fahrzeug
    {
        public override string Drive()
            => $"PKW: {Kennzeichen}";
    }
}
