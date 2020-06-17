using FluentNHibernate.Mapping;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database
{
    public class BusinessUnitMapping : ClassMap<BusinessUnit>
    {
        public BusinessUnitMapping()
        {

            Table("BusinessUnits");
            Id(b => b.Id).GeneratedBy.Native();
            Map(b => b.Name).Not.Nullable()
                                .Length(100);
            Map(b => b.Description).Length(250);
            Version(b => b.Version);

        }
    }
}
