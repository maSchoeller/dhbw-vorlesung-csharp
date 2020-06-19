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

        public (OperationResult, Tour?) DeleteTour(Tour tour)
        {
            using var session = Factory.OpenSession();
            using var transaction = session.BeginTransaction();

            var vehicleEmployee = session.Query<VehicleEmployee>().FirstOrDefault(ve => ve.Id == tour.Id);
            if (vehicleEmployee is null)
            {
                return (OperationResult.NotFound,null);
            }

            try
            {
                session.Delete(vehicleEmployee);
                transaction.Commit();
            }
            catch (Exception e)
            {
                return (OperationResult.SaveFailure,null);
            }
            return (OperationResult.Success, tour);
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
                          });
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
    }
}
