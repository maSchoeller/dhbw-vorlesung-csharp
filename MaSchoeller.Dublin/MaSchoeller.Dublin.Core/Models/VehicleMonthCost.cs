using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Models
{
    [DataContract]
    public class VehicleMonthCost
    {
        [DataMember]
        public DateTime Month { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public double Costs { get; set; }
    }
}
