using FluentNHibernate.Mapping;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database
{
    public class BuisnessUnitMapping : ClassMap<BuisnessUnit>
    {
        public BuisnessUnitMapping()
        {

            Table("BuisnessUnits");
            Id(g => g.Id).GeneratedBy.Native();
            Map(g => g.Name).Not.Nullable()
                                .Length(100);
            Map(g => g.Description).Length(250);
            Version(x => x.Version);
        }
    }
}
