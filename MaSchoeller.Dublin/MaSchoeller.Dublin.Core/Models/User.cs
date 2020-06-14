using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Username { get; set; } = string.Empty;
        [DataMember]
        public string? Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public int Version { get; set; }


        public string FullName => Firstname is null ? Lastname : $"{Firstname} {Lastname}";
    }
}
