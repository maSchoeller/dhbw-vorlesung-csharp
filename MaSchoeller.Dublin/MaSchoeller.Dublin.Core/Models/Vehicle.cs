using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public double Insurance { get; set; }
        public DateTime LeasingFrom { get; set; }
        public DateTime LeasingTo { get; set; }
        public double LeasingRate { get; set; }
        public int Version { get; set; }
    }
}
