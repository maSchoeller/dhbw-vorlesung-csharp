using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Communications.Models
{
    [DataContract]
    public class CalculationMonthSet
    {
        [DataMember]
        public DateTime Month { get; set; }
        
        [DataMember]
        public int Count { get; set; }
        
        [DataMember]
        public int Costs { get; set; }
    }
}
