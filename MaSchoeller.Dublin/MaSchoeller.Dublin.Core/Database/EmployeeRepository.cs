using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;

namespace MaSchoeller.Dublin.Core.Database
{
    internal class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConnectionFactory factory, ILogger<EmployeeRepository>? logger = null) 
            : base(factory, logger) { }
    }
}
