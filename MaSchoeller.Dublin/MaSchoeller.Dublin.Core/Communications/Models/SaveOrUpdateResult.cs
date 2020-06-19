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
    public class SaveOrUpdateBuisnessUnitResult : BaseResult
    {
        [DataMember]
        public BusinessUnit? BuisnessUnit { get; set; } = null!;
    }

    [DataContract]
    public class SaveOrUpdateEmployeeResult : BaseResult
    {
        [DataMember]
        public Employee? Employee { get; set; } = null!;
    }

    [DataContract]
    public class SaveOrUpdateUserResult : BaseResult
    {
        [DataMember]
        public User? User { get; set; } = null!;
    }

    [DataContract]
    public class SaveOrUpdateVehicleResult : BaseResult
    {
        [DataMember]
        public Vehicle? Vehicle { get; set; } = null!;
    }

    [DataContract]
    public class SaveTourResult : BaseResult
    {
        [DataMember]
        public Tour? Tour { get; set; }
    }
}
