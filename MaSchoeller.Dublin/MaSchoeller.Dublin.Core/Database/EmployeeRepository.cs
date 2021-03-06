﻿using FluentNHibernate.Utils;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Xml.Schema;

namespace MaSchoeller.Dublin.Core.Database
{
    internal class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConnectionFactory factory, ILogger<EmployeeRepository>? logger = null) 
            : base(factory, logger) { }

        public override (OperationResult result, Employee entity) Save(Employee entity)
        {
            using var session = Factory.OpenSession();
            var emp = session.Query<Employee>().FirstOrDefault(e => e.EmployeeNumber == entity.EmployeeNumber);
            if (!(emp is null))
            {
                return (OperationResult.AlreadyExists, entity);
            }
            return base.Save(entity);
        }

        public override (OperationResult result, Employee entity) Update(Employee entity)
        {
            using var session = Factory.OpenSession();
            var emp = session.Query<Employee>().FirstOrDefault(e => e.EmployeeNumber == entity.EmployeeNumber);
            if (!(emp is null))
            {
                return (OperationResult.AlreadyExists, entity);
            }
            return base.Update(entity);
        }

        public IEnumerable<Employee> GetPossibleEmployeesByVehicle(int id)
        {
            using var session = Factory.OpenSession();
            var ve = session.Query<VehicleEmployee>()
                            .Where(ve => ve.Vehicle.Id == id);
            var e = session.Query<Employee>()
                           .Where(e => !ve.Any(x => x.Employee.Id == e.Id));
            return e.ToList();
        }
    }
}
