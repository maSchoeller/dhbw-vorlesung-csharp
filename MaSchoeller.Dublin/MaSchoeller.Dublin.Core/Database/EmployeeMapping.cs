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
            Id(g => g.Id).GeneratedBy.Native();
            Map(g => g.Firstname)
                .Not.Nullable().Length(50);
            Map(g => g.Lastname)
                .Not.Nullable().Length(50);
            Map(g => g.EmployeeNumber)
                .Not.Nullable();
            Map(g => g.Salutation).Length(50);
            Map(g => g.Title).Length(50);
            Version(x => x.Version);
        }
    }
}
