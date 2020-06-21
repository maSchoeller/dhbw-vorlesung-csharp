using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaSchoeller.Dublin.Core.Database
{
    internal class BusinessUnitRepository : BaseRepository<BusinessUnit>, IBusinessUnitRepository
    {
        public BusinessUnitRepository(IConnectionFactory factory, ILogger<BusinessUnitRepository>? logger = null)
            : base(factory, logger) { }


        public override (OperationResult result, BusinessUnit entity) Save(BusinessUnit entity)
        {
            using var session = Factory.OpenSession();
            var bu = session.Query<BusinessUnit>().FirstOrDefault(e => e.Name.ToUpperInvariant() == entity.Name.ToUpperInvariant());
            if (!(bu is null))
            {
                return (OperationResult.AlreadyExists, entity);
            }
            return base.Save(entity);
        }

        public override (OperationResult result, BusinessUnit entity) Update(BusinessUnit entity)
        {
            using var session = Factory.OpenSession();
            var bu = session.Query<BusinessUnit>().FirstOrDefault(e => e.Name.ToUpperInvariant() == entity.Name.ToUpperInvariant());
            if (!(bu is null))
            {
                return (OperationResult.AlreadyExists, entity);
            }
            return base.Update(entity);
        }


        public IEnumerable<BusinessUnitMonthCost> GetAllCosts()
        {
            using var session = Factory.OpenSession();
            var result = session.Query<BusinessUnit>()
                                .Join(session.Query<Employee>(),
                                        b => b.Id,
                                        e => e.BusinessUnit.Id,
                                        (b, e) => new { BusinessUnit = b, Employee = e })
                                .Join(session.Query<VehicleEmployee>(),
                                        be => be.Employee.Id,
                                        ve => ve.Employee.Id,
                                        (be, ve) => new { BusinessUnitEmployee = be, VehicleEmployee = ve })
                                .Join(session.Query<Vehicle>(),
                                        beve => beve.VehicleEmployee.Vehicle.Id,
                                        v => v.Id,
                                        (beve, v) => new { beve.BusinessUnitEmployee.BusinessUnit, beve.VehicleEmployee, Vehicle = v })
                                .ToArray()
                                .Select(m => new { m.BusinessUnit, Costs = GetCostsPerVehicle(m.VehicleEmployee, m.Vehicle) })
                                .SelectMany(bv => bv.Costs.Select(c => new { VehicleCost = c, bv.BusinessUnit }))
                                .GroupBy(cb => new { cb.VehicleCost.Month, cb.BusinessUnit })
                                .Select(cb => new BusinessUnitMonthCost { Month = cb.Key.Month, BusinessUnit = cb.Key.BusinessUnit.Name, Costs = cb.Sum(c => c.VehicleCost.Costs) })
                                .ToArray();

            return result;
        }

        private IEnumerable<VehicleMonthCost> GetCostsPerVehicle(VehicleEmployee vehicleEmployee, Vehicle vehicle)
        {
            for (DateTime i = vehicleEmployee.StartDate; i < (vehicleEmployee.EndDate ?? DateTime.Now); i = i.AddMonths(1))
            {
                yield return new VehicleMonthCost
                {
                    Costs = vehicle.Insurance / 12,
                    Month = new DateTime(i.Year, i.Month, 1),
                    Count = 1
                };
            }
        }
    }
}
