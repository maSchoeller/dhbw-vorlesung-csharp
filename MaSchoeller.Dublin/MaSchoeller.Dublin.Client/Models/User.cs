using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Proxies.Users
{
    public partial class User 
    {
        public string Fullname => Firstname is null ? Lastname : $"{Firstname} {Lastname}";
    }
}
