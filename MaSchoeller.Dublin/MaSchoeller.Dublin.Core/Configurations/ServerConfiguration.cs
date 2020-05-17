using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Configurations
{
    public class ServerConfiguration
    {
        public string TokenSecret { get; set; } = Guid.NewGuid().ToString().Replace('-','a');
        public string Hostname { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
