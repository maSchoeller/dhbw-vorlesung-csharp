using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Models
{
    public class DisplayTour : DisplayBase
    {
        public DisplayTour()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
