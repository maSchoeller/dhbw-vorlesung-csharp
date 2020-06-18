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
    [ServiceBehavior]
    internal class FleetService : BaseService, IFleetService
    {
        private readonly IBuisnessUnitRepository _buisnesses;
        private readonly IEmployeeRepository _employees;
        private readonly IVehicleRepository _vehicles;
        private readonly ILogger<UserService>? _logger;

        public FleetService(ISecurityHelper connectionHelper,
                            IBuisnessUnitRepository buisnesses,
                            IEmployeeRepository employees,
                            IVehicleRepository vehicles,
                            ILogger<UserService>? logger = null)
            : base(connectionHelper)
        {
            _buisnesses = buisnesses;
            _employees = employees;
            _vehicles = vehicles;
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

            var (result, entity) = employee.Id <= 0 ? _employees.Save(employee) : _employees.Update(employee);
            return new SaveOrUpdateEmployeeResult { Reason = result, Employee = entity };
        }

        public DeleteEmployeeResult DeleteEmployee(Employee employee)
        {
            if (!Validate())
                return new DeleteEmployeeResult { Reason = OperationResult.NotAuthenticated };

            var result = _employees.Delete(employee);
            return new DeleteEmployeeResult { Reason = result.result, Employee = result.entity };
        }

        public SaveOrUpdateVehicleResult SaveOrUpdateVehicle(Vehicle vehicle)
        {
            if (!Validate())
                return new SaveOrUpdateVehicleResult { Reason = OperationResult.NotAuthenticated };

            var (result, entity) = vehicle.Id <= 0 ? _vehicles.Save(vehicle) : _vehicles.Update(vehicle);
            return new SaveOrUpdateVehicleResult { Reason = result, Vehicle = entity };
        }

        public DeleteVehicleResult DeleteVehicle(Vehicle vehicle)
        {
            if (!Validate())
                return new DeleteVehicleResult { Reason = OperationResult.NotAuthenticated };

            var result = _vehicles.Delete(vehicle);
            return new DeleteVehicleResult { Reason = result.result, Vehicle = result.entity };
        }

        public IEnumerable<Vehicle> GetAllVehicles() 
            => _vehicles.GetAll();
    }
}
