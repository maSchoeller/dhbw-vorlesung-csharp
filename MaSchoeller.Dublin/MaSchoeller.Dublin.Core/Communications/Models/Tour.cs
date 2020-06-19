using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Communications.Models
{
    [DataContract]
    public class Tour
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int EmployeeId { get; set; }
        
        [DataMember]
        public int VehicleId { get; set; }

        [DataMember]
        public string EmployeeName { get; set; } = string.Empty;

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime? End { get; set; }

    }
}
