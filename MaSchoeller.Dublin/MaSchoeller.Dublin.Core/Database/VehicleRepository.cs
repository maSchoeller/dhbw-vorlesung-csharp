using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaSchoeller.Dublin.Core.Database
{
    internal class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IConnectionFactory factory, ILogger<VehicleRepository>? logger = null)
            : base(factory, logger) { }

        public override (OperationResult result, Vehicle entity) Save(Vehicle entity)
        {
            using var session = Factory.OpenSession();
            var emp = session.Query<Vehicle>().FirstOrDefault(v => v.LicensePlate.ToUpperInvariant() == entity.LicensePlate.ToUpperInvariant());
            if (!(emp is null))
            {
                return (OperationResult.AlreadyExists, entity);
            }
            return base.Save(entity);
        }

        public override (OperationResult result, Vehicle entity) Update(Vehicle entity)
        {
            using var session = Factory.OpenSession();
            var emp = session.Query<Vehicle>().FirstOrDefault(v => v.LicensePlate.ToUpperInvariant() == entity.LicensePlate.ToUpperInvariant());
            if (!(emp is null))
            {
                return (OperationResult.AlreadyExists, entity);
            }
            return base.Update(entity);
        }

        public (OperationResult, Tour?) DeleteTour(Tour tour)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();

            var vehicleEmployee = session.Query<VehicleEmployee>().FirstOrDefault(ve => ve.Id == tour.Id);
            if (vehicleEmployee is null)
            {
                return (OperationResult.NotFound, null);
            }

            try
            {
                session.Delete(vehicleEmployee);
                transaction.Commit();
            }
            catch (Exception e)
            {
                return (OperationResult.SaveFailure, null);
            }
            return (OperationResult.Success, tour);
        }

        public IEnumerable<VehicleMonthCost> GetAllVehicleMonthCosts()
        {
            using var session = Factory.OpenSession();
            var vehicles = session.Query<Vehicle>();
            return vehicles.ToArray().SelectMany(v => GetCostsPerVehicle(v))
                           .GroupBy(v => v.Month)
                           .Select(vg => new VehicleMonthCost
                           {
                               Month = vg.Key,
                               Count = vg.Count(),
                               Costs = vg.Sum(v => v.Costs)
                           })
                           .OrderBy(v => v.Month)
                           .ToArray();
        }

        public IEnumerable<Tour> GetToursByVehicle(int id)
        {
            using var session = Factory.OpenSession();
            return session.Query<VehicleEmployee>()
                          .Where(ve => ve.Vehicle.Id == id)
                          .Select(ve => new Tour
                          {
                              Id = ve.Id,
                              EmployeeId = ve.Employee.Id,
                              EmployeeName = ve.Employee.ToString(),
                              VehicleId = ve.Vehicle.Id,
                              Start = ve.StartDate,
                              End = ve.EndDate
                          }).ToList();
        }

        public (OperationResult, Tour?) SaveTour(Tour tour)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();

            var vehicleEmployee = session.Query<VehicleEmployee>()
                .FirstOrDefault(ve => ve.Employee.Id == tour.EmployeeId && ve.Vehicle.Id == tour.VehicleId);
            if (!(vehicleEmployee is null))
            {
                return (OperationResult.AlreadyExists, null);
            }
            try
            {
                var employee = session.Query<Employee>().First(e => e.Id == tour.EmployeeId);
                var vehicle = session.Query<Vehicle>().First(v => v.Id == tour.VehicleId);
                vehicleEmployee = new VehicleEmployee
                {
                    StartDate = tour.Start,
                    EndDate = tour.End,
                    Employee = employee,
                    Vehicle = vehicle
                };
                session.Save(vehicleEmployee);
                transaction.Commit();
                tour.Id = vehicleEmployee.Id;
            }
            catch (Exception e)
            {
                return (OperationResult.SaveFailure, null);
            }
            return (OperationResult.Success, tour);
        }

        private IEnumerable<VehicleMonthCost> GetCostsPerVehicle(Vehicle vehicle)
        {
            var list = new List<VehicleMonthCost>();
            for (DateTime i = vehicle.LeasingFrom; i < vehicle.LeasingTo; i = i.AddMonths(1))
            {
                list.Add(new VehicleMonthCost
                {
                    Costs = vehicle.Insurance / 12,
                    Month = new DateTime(i.Year, i.Month, 1),
                    Count = 1
                });
            }
            return list;
        }


    }
}
