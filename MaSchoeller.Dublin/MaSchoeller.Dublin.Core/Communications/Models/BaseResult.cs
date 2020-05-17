﻿using System;
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
        public string? ErrorMessage { get; set; }

        [DataMember]
        public bool Success { get; set; }
    }
}