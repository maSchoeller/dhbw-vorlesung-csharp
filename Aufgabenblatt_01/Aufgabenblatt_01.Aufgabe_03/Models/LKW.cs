using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_01.Aufgabe_03.Models
{
    public class LKW : Fahrzeug
    {
        public new string Drive() 
            => $"LKW: { Kennzeichen}";
    }
}
