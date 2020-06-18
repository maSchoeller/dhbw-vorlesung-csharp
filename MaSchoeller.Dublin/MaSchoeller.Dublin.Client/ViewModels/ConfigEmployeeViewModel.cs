using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class ConfigEmployeeViewModel : ViewModelBase
    {
        public ConfigEmployeeViewModel()
        {

        }

        private ObservableCollection<DisplayEmployee> _employees = new ObservableCollection<DisplayEmployee>();
        public ObservableCollection<DisplayEmployee> Employees
        {
            get => _employees;
            set => SetProperty(ref _employees, value);
        }


        private DisplayEmployee? _selectedEmployee;
        public DisplayEmployee? SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }

        public ICommand NewCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;
        public ICommand SaveCommand { get; set; } = null!;
        public IEnumerable<BusinessUnit> BusinessUnits { get; set; }
    }
}
