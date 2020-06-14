using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Helpers
{
    public class UserContext
    {
        public string Username { get; set; }

        public string Fullname { get; set; }

        public bool IsAdmin { get; set; }
    }
}
