using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_04.Aufgabe_13.Service.Models
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }

        public bool IsPremiumCustomer { get; set; }

        public override bool Equals(object obj) 
            => obj is Customer c && c.Firstname == Firstname && c.Lastname == Lastname;

    }
}
