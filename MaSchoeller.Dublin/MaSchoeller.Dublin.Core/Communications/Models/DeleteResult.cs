﻿using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Communications.Models
{
    [DataContract]
    public class DeleteUserResult : BaseResult
    {
        [DataMember]
        public User? User { get; set; }
    }

    [DataContract]
    public class DeleteBuisnessUnitResult : BaseResult
    {
        [DataMember]
        public BusinessUnit? BuisnessUnit { get; set; }
    }

    [DataContract]
    public class DeleteEmployeeResult : BaseResult
    {
        [DataMember]
        public Employee? Employee { get; set; }
    }

    [DataContract]
    public class DeleteVehicleResult : BaseResult
    {
        [DataMember]
        public Vehicle? Vehicle { get; set; }
    }

    [DataContract]
    public class DeleteTourResult : BaseResult
    {
        [DataMember]
        public Tour? Tour { get; set; }
    }

}

