﻿using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MaSchoeller.Dublin.Core.Communications
{
    [ServiceContract()]
    public interface IFleetService
    {

        [OperationContract]
        DeleteBuisnessUnitResult DeleteBusinessUnit(BusinessUnit buisnessUnit);

        [OperationContract]
        SaveOrUpdateBuisnessUnitResult SaveOrUpdateBusinessUnit(BusinessUnit buisnessUnit);

        [OperationContract]
        IEnumerable<BusinessUnit> GetAllBusinessUnits();

        [OperationContract]
        SaveOrUpdateEmployeeResult SaveOrUpdateEmployee(Employee employee);

        [OperationContract]
        DeleteEmployeeResult DeleteEmployee(Employee employee);
        
        [OperationContract]
        IEnumerable<Employee> GetAllEmployees();

        [OperationContract]
        SaveOrUpdateVehicleResult SaveOrUpdateEmployee(Vehicle vehicle);

        [OperationContract]
        DeleteVehicleResult DeleteVehicle(Vehicle vehicle);

        [OperationContract]
        IEnumerable<Vehicle> GetAllVehicles();
    }
}
