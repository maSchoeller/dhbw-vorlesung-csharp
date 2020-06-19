using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class ConfigVehicleViewModel : ViewModelBase
    {

        public event EventHandler? TabChanged;

        public ConfigVehicleViewModel()
        {

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedTab))
                    TabChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, value);
        }

        private ObservableCollection<DisplayVehicle> _vehicles 
            = new ObservableCollection<DisplayVehicle>();
        public ObservableCollection<DisplayVehicle> Vehicles
        {
            get => _vehicles;
            set => SetProperty(ref _vehicles, value);
        }


        private DisplayVehicle? _selectedVehicle;
        public DisplayVehicle? SelectedVehicle
        {
            get => _selectedVehicle;
            set => SetProperty(ref _selectedVehicle, value);
        }


        public ICommand NewCommand { get; set; } = null!;
        public ICommand SaveCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;
    }
}
