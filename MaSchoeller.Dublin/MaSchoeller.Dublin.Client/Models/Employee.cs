using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Proxies.Fleets
{
    public partial class Employee
    {
        public string Fullname => $"{Salutation} {Firstname} {Lastname}";
    }
}
