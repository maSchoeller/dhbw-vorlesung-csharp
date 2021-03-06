﻿using MaSchoeller.Dublin.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Communications.Models
{
    [DataContract]
    public class BaseResult
    {
        [DataMember]
        public OperationResult Reason { get; set; }
    }
}
