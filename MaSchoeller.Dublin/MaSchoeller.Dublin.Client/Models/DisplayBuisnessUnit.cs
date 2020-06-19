using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Models
{
    public class DisplayBusinessUnit : DisplayBase
    {

        public DisplayBusinessUnit(BusinessUnit buisnessUnit)
        {
            Version = buisnessUnit.Version;
            Id = buisnessUnit.Id;
            _name = buisnessUnit.Name;
            _description = buisnessUnit.Description;
            IsSynced = true;
        }

        public DisplayBusinessUnit()
        {

        }

        public int Id { get; set; }
        public int Version { get; set; }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                SetProperty(ref _description, value);
                EditState = EditState.Modified;
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                EditState = EditState.Modified;
            }
        }

        public BusinessUnit AsBusinessUnit()
        {
            return new BusinessUnit
            {
                Id = Id,
                Version = Version,
                Description = Description,
                Name = Name,
            };
        }
    }
}
