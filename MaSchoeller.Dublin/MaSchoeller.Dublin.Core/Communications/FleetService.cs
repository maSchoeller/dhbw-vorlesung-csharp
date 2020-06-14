using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Database;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace MaSchoeller.Dublin.Core.Communications
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    internal class FleetService : BaseService, IFleetService
    {
        private readonly IBuisnessUnitRepository _buisnesses;
        private readonly IEmployeeRepository _employees;
        private readonly ILogger<UserService>? _logger;

        public FleetService(ISecurityHelper connectionHelper,
                            IBuisnessUnitRepository buisnesses,
                            IEmployeeRepository employees,
                            ILogger<UserService>? logger = null)
            : base(connectionHelper)
        {
            _buisnesses = buisnesses;
            _employees = employees;
            _logger = logger;
        }

        public IEnumerable<BuisnessUnit> GetAllBuisnessUnits()
        {
            return _buisnesses.GetAll();
        }

        public SaveOrUpdateResult SaveOrUpdateBuisnessUnit(BuisnessUnit buisnessUnit)
        {
            if (!Validate())
                return new SaveOrUpdateResult { Reason = OperationResult.NotAuthenticated };

            OperationResult result;
            if (buisnessUnit.Id <= 0)
            {
                result = _buisnesses.Save(buisnessUnit);
            }
            else
            {
                result = _buisnesses.Update(buisnessUnit);
            }
            return new SaveOrUpdateResult { Reason = result };
        }

        public DeleteResult DeleteBuisnessUnit(BuisnessUnit buisnessUnit)
        {
            if (!Validate())
                return new DeleteResult { Reason = OperationResult.NotAuthenticated };

            var result = _buisnesses.Delete(buisnessUnit);
            return new DeleteResult { Reason = result };
        }


        public IEnumerable<Employee> GetAllEmployees() 
            => _employees.GetAll();

        public SaveOrUpdateResult SaveOrUpdateEmployee(Employee employee)
        {
            if (!Validate())
                return new SaveOrUpdateResult { Reason = OperationResult.NotAuthenticated };

            OperationResult result = employee.Id <= 0 ? _employees.Save(employee) : _employees.Update(employee);
            return new SaveOrUpdateResult { Reason = result };
        }

        public DeleteResult DeleteEmployee(Employee employee)
        {
            if (!Validate())
                return new DeleteResult { Reason = OperationResult.NotAuthenticated };

            OperationResult result = _employees.Delete(employee);
            return new DeleteResult { Reason = result };
        }

    }
}
