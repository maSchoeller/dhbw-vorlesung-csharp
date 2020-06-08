using FluentNHibernate.Mapping;
using MaSchoeller.Dublin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {

            Table("Users");
            Id(g => g.Id).GeneratedBy.Native();
            Map(g => g.Username)
                .Not.Nullable().Length(20);
            Map(g => g.Firstname).Length(50);
            Map(g => g.Lastname)
                .Not.Nullable().Length(50);
            Map(g => g.PasswordHash)
                .Column("Password")
                .Not.Nullable().Length(60);
            Map(g => g.IsAdmin)
                .Not.Nullable();
            Version(x => x.Version);
        }
    }
}
