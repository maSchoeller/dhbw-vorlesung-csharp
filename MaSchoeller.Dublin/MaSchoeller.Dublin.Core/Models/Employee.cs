using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public int EmployeeNumber { get; set; }
        public string? Salutation { get; set; }
        public string? Title { get; set; }
        public BusinessUnit BusinessUnit { get; set; } = null!;
        public int Version { get; set; }

        public ICollection<VehicleEmployee> VehicleEmployees { get; set; } = new Collection<VehicleEmployee>();
    }
}
