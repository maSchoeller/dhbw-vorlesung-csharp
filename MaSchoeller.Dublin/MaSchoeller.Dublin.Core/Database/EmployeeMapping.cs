using FluentNHibernate.Mapping;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database
{
    public class EmployeeMapping : ClassMap<Employee>
    {
        public EmployeeMapping()
        {

            Table("Employees");
            Id(e => e.Id).GeneratedBy.Native();
            Map(e => e.Firstname)
                .Not.Nullable().Length(50);
            Map(e => e.Lastname)
                .Not.Nullable().Length(50);
            Map(e => e.EmployeeNumber)
                .Not.Nullable();
            Map(e => e.Salutation).Length(50);
            Map(e => e.Title).Length(50);
            Version(e => e.Version);
            References(e => e.BusinessUnit).Column("BusinessUnitId")
                .Not.Nullable()
                .Cascade.All();
            HasMany(e => e.VehicleEmployees).KeyColumn("EmployeeId")
                    .Cascade.All();
        }
    }
}
