using FluentNHibernate.Mapping;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database
{
    public class VehicleMapping : ClassMap<Vehicle>
    {
        public VehicleMapping()
        {
            Table("Vehicles");
            Id(v => v.Id).GeneratedBy.Native();
            Map(v => v.Brand)
                .Not.Nullable().Length(50);
            Map(v => v.LicensePlate)
                .Not.Nullable().Length(50);
            Map(v => v.Model)
                .Not.Nullable().Length(50);
            Map(v => v.Insurance)
                .Not.Nullable();
            Map(v => v.LeasingFrom)
                .Not.Nullable();
            Map(v => v.LeasingTo)
                .Not.Nullable();
            Map(v => v.LeasingRate)
                .Not.Nullable();
            Version(v => v.Version);
            //HasMany(v => v.VehicleEmployees).KeyColumn("VehicleId")
            //    .Inverse()
            //    .Cascade.AllDeleteOrphan();
        }
    }
}
