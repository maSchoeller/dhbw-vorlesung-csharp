using MaSchoeller.Dublin.Core.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Services
{
    internal class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConnectionFactory factory) : base(factory)
        {

        }
    }
}
