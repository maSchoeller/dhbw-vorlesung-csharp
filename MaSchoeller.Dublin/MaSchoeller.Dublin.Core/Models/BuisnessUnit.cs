using FluentNHibernate.MappingModel.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Models
{
    [DataContract]
    public class BuisnessUnit
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; } = string.Empty;
        [DataMember]
        public string? Description { get; set; }
        [DataMember]
        public int Version { get; set; }

        public ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
    }
}
