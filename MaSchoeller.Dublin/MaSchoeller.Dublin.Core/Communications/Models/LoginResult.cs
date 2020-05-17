using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Communications.Models
{
    [DataContract]
    public class LoginResult : BaseResult
    {
        [DataMember]
        public string? Token { get; set; }
        [DataMember]
        public bool IsAdmin { get; set; }
    }
}
