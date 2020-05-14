using Aufgabenblatt_05.Aufgabe_13.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augabenblatt_05.Aufgabe_13.Framework
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("Customers");
            Map(x => x.Firstname).Length(100).Not.Nullable();
            Map(x => x.Lastname).Column("Lastname").Length(100).Not.Nullable();
            Map(x => x.Gender).CustomType<Gender>().Not.Nullable();
            Id(x => x.Id);

            Component(x => x.Address, a =>
            {
                a.Map(x => x.Street).Length(100);
                a.Map(x => x.StreetNumber).Length(5);
                a.Map(x => x.Postcode).Not.Nullable();
                a.Map(x => x.City).Length(100);
            });
        }
    }
}
