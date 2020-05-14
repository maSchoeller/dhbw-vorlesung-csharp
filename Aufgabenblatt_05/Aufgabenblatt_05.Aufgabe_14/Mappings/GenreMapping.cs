using Aufgabenblatt_05.Aufgabe_14.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_05.Aufgabe_14.Mappings
{
    public class GenreMapping : ClassMap<Genre>
    {
        public GenreMapping()
        {
            Table("Genres");
            Id(g => g.Id).GeneratedBy.Native();
            Map(g => g.Name).Not.Nullable()
                                .Length(100);
            
        }
    }
}
