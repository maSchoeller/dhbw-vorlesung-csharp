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
            Id(g => g.Id).GeneratedBy.Native();
            Map(g => g.Brand)
                .Not.Nullable().Length(50);
            Map(g => g.LicensePlate)
                .Not.Nullable().Length(50);
            Map(g => g.Model)
                .Not.Nullable().Length(50);
            Map(g => g.Insurance)
                .Not.Nullable();
            Map(g => g.LeasingFrom)
                .Not.Nullable();
            Map(g => g.LeasingTo)
                .Not.Nullable();
            Map(g => g.LeasingRate)
                .Not.Nullable();
            Version(x => x.Version);
        }
    }
}
