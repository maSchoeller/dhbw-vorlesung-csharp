using FluentNHibernate.Mapping;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database
{
    class VehicleEmployeeMapping : ClassMap<VehicleEmployee>
    {
        public VehicleEmployeeMapping()
        {
            Table("VehicleToEmployeeRelation");
            Id(v => v.Id).GeneratedBy.Native();
            Map(v => v.StartDate)
                .Not.Nullable();
            Map(v => v.EndDate);
            References(v => v.Employee).Column("EmployeeId")
                .Not.Nullable()
                .Cascade.None();
            References(v => v.Vehicle).Column("VehicleId")
                .Not.Nullable()
                .Cascade.None();
        }
    }
}
