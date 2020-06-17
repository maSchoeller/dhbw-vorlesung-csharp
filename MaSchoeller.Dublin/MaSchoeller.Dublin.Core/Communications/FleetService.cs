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

        public IEnumerable<BusinessUnit> GetAllBusinessUnits()
        {
            return _buisnesses.GetAll();
        }

        public SaveOrUpdateBuisnessUnitResult SaveOrUpdateBusinessUnit(BusinessUnit buisnessUnit)
        {
            if (!Validate())
                return new SaveOrUpdateBuisnessUnitResult { Reason = OperationResult.NotAuthenticated };

            if (buisnessUnit.Id <= 0)
            {
                var result = _buisnesses.Save(buisnessUnit);
                return new SaveOrUpdateBuisnessUnitResult { Reason = result.result, BuisnessUnit = result.entity };
            }
            else
            {
                var result = _buisnesses.Update(buisnessUnit);
                return new SaveOrUpdateBuisnessUnitResult { Reason = result.result, BuisnessUnit = result.entity };
            }
        }

        public DeleteBuisnessUnitResult DeleteBusinessUnit(BusinessUnit buisnessUnit)
        {
            if (!Validate())
                return new DeleteBuisnessUnitResult { Reason = OperationResult.NotAuthenticated };

            var result = _buisnesses.Delete(buisnessUnit);
            return new DeleteBuisnessUnitResult { Reason = result.result, BuisnessUnit = result.entity };
        }


        public IEnumerable<Employee> GetAllEmployees()
            => _employees.GetAll();

        public SaveOrUpdateEmployeeResult SaveOrUpdateEmployee(Employee employee)
        {
            if (!Validate())
                return new SaveOrUpdateEmployeeResult { Reason = OperationResult.NotAuthenticated };

            var r = employee.Id <= 0 ? _employees.Save(employee) : _employees.Update(employee);
            return new SaveOrUpdateEmployeeResult { Reason = r.result, Employee = r.entity };
        }

        public DeleteEmployeeResult DeleteEmployee(Employee employee)
        {
            if (!Validate())
                return new DeleteEmployeeResult { Reason = OperationResult.NotAuthenticated };

            var result = _employees.Delete(employee);
            return new DeleteEmployeeResult { Reason = result.result, Employee = result.entity };
        }

    }
}
