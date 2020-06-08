using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Models
{
    public class VehicleEmployee
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public Employee Employee { get; set; } = null!;

    }
}
