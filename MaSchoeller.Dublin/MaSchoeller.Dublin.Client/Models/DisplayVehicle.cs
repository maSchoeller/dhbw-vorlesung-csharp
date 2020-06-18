using MaSchoeller.Dublin.Client.Proxies.Fleets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Models
{
    public class DisplayVehicle : DisplayBase
    {

        public DisplayVehicle(Vehicle vehicle)
        {
            Id = vehicle.Id;
            Version = vehicle.Version;
            _licensePlate = vehicle.LicensePlate;
            _brand = vehicle.Brand;
            _model = vehicle.Model;
            _insurance = vehicle.Insurance;
            _leasingFrom = vehicle.LeasingFrom;
            _leasingTo = vehicle.LeasingTo;
            _leasingRate = vehicle.LeasingRate;
        }

        public DisplayVehicle()
        {

        }

        public int Id { get; set; }
        public int Version { get; set; }


        private string _licensePlate = string.Empty;
        public string LicensePlate
        {
            get => _licensePlate;
            set
            {
                SetProperty(ref _licensePlate, value);
                EditState = EditState.Modified;
            }
        }

        public string _brand = string.Empty;
        public string Brand
        {
            get => _brand;
            set
            {
                SetProperty(ref _brand, value);
                EditState = EditState.Modified;
            }
        }

        public string _model = string.Empty;
        public string Model
        {
            get => _model;
            set
            {
                SetProperty(ref _model, value);
                EditState = EditState.Modified;
            }
        }

        public double _insurance;
        public double Insurance
        {
            get => _insurance;
            set
            {
                SetProperty(ref _insurance, value);
                EditState = EditState.Modified;
            }
        }

        public DateTime _leasingFrom;
        public DateTime LeasingFrom
        {
            get => _leasingFrom;
            set
            {
                SetProperty(ref _leasingFrom, value);
                EditState = EditState.Modified;
            }
        }

        public DateTime _leasingTo;
        public DateTime LeasingTo
        {
            get => _leasingTo;
            set
            {
                SetProperty(ref _leasingTo, value);
                EditState = EditState.Modified;
            }
        }

        public double _leasingRate;
        public double LeasingRate
        {
            get => _leasingRate;
            set
            {
                SetProperty(ref _leasingRate, value);
                EditState = EditState.Modified;
            }
        }


        public Vehicle AsVehicle()
        {
            return new Vehicle
            {
                Id = Id,
                Version = Version,
                LicensePlate = LicensePlate,
                Brand = Brand,
                Model = Model,
                Insurance = Insurance,
                LeasingFrom = LeasingFrom,
                LeasingTo = LeasingTo,
                LeasingRate = LeasingRate,
            };
        }
    }
}
