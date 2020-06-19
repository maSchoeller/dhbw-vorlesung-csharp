using MaSchoeller.Dublin.Client.Proxies.Fleets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            _licencePlate = vehicle.LicensePlate;
            _brand = vehicle.Brand;
            _model = vehicle.Model;
            _insurance = vehicle.Insurance;
            _leasingFrom = vehicle.LeasingFrom;
            _leasingTo = vehicle.LeasingTo;
            _leasingRate = vehicle.LeasingRate;
            IsSynced = true;
        }

        public DisplayVehicle()
        {
            IsSynced = false;
            LeasingFrom = DateTime.Today;
            LeasingTo = DateTime.Today;
        }

        public int Id { get; set; }
        public int Version { get; set; }


        private string _licencePlate = string.Empty;
        public string LicencePlate
        {
            get => _licencePlate;
            set
            {
                SetProperty(ref _licencePlate, value);
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

        public double? _insurance;
        public double? Insurance
        {
            get => _insurance;
            set
            {
                SetProperty(ref _insurance, value);
                EditState = EditState.Modified;
            }
        }

        public DateTime? _leasingFrom;
        public DateTime? LeasingFrom
        {
            get => _leasingFrom;
            set
            {
                SetProperty(ref _leasingFrom, value);
                EditState = EditState.Modified;
            }
        }

        public DateTime? _leasingTo;
        public DateTime? LeasingTo
        {
            get => _leasingTo;
            set
            {
                SetProperty(ref _leasingTo, value);
                EditState = EditState.Modified;
            }
        }

        public double? _leasingRate;
        public double? LeasingRate
        {
            get => _leasingRate;
            set
            {
                SetProperty(ref _leasingRate, value);
                EditState = EditState.Modified;
            }
        }

        public override bool Validate()
        {
            return !(string.IsNullOrWhiteSpace(Brand) ||
                     string.IsNullOrWhiteSpace(Model) ||
                     string.IsNullOrWhiteSpace(LicencePlate) ||
                     LeasingTo < LeasingFrom ||
                     LeasingFrom is null ||
                     LeasingTo is null ||
                     Insurance is null ||
                     LeasingRate is null);
        }


        private ObservableCollection<DisplayTour> _tours = new ObservableCollection<DisplayTour>();
        public ObservableCollection<DisplayTour> Tours
        {
            get => _tours;
            set => SetProperty(ref _tours, value);
        }

        public Vehicle? AsVehicle()
        {
            if (Validate())
            {
                return new Vehicle
                {
                    Id = Id,
                    Version = Version,
                    LicensePlate = LicencePlate,
                    Brand = Brand,
                    Model = Model,
                    Insurance = Insurance!.Value,
                    LeasingFrom = LeasingFrom!.Value,
                    LeasingTo = LeasingTo!.Value,
                    LeasingRate = LeasingRate!.Value,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
