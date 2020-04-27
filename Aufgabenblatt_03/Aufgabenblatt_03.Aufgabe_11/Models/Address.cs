using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_03.Aufgabe_11.Models
{
    public class Address
    {
        public string Street { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
        public int Postcode { get; set; }
        public string City { get; set; } = string.Empty;

    }
}
