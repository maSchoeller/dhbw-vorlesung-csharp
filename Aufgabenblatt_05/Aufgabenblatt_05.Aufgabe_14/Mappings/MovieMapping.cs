using Aufgabenblatt_05.Aufgabe_14.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_05.Aufgabe_14.Mappings
{
    public class MovieMapping : ClassMap<Movie>
    {
        public MovieMapping()
        {
            Table("Movies");
            Id(m => m.Id).GeneratedBy.Native();
            Map(m => m.Title).Not.Nullable()
                                .Length(100);
            References(m => m.Genre)
                .Column("GenreId");
        }
    }
}
