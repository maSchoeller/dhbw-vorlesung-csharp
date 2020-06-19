using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Models
{
    [DataContract]
    public class Vehicle
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string LicensePlate { get; set; } = string.Empty;
        [DataMember]
        public string Brand { get; set; } = string.Empty;
        [DataMember]
        public string Model { get; set; } = string.Empty;
        [DataMember]
        public double Insurance { get; set; }
        [DataMember]
        public DateTime LeasingFrom { get; set; }
        [DataMember]
        public DateTime LeasingTo { get; set; }
        [DataMember]
        public double LeasingRate { get; set; }
        [DataMember]
        public int Version { get; set; }
    }
}
