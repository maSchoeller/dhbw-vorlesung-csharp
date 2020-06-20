using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Models
{
    [DataContract]
    public class BusinessUnitMonthCost
    {
        [DataMember]
        public DateTime Month { get; set; }
        [DataMember]
        public string BusinessUnit { get; set; } = string.Empty;
        [DataMember]
        public double Costs { get; set; }
    }
}
