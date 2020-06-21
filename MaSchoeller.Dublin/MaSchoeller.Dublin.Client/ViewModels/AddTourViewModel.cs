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
    public class AddTourViewModel : ViewModelBase
    {
        private ObservableCollection<Proxies.Fleets.Employee> _employees = new ObservableCollection<Proxies.Fleets.Employee>();

        public ObservableCollection<Proxies.Fleets.Employee> Employees
        {
            get => _employees;
            set => SetProperty(ref _employees, value);
        }

        private Proxies.Fleets.Employee? _selectedEmployee;

        public Proxies.Fleets.Employee? SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }

        private DateTime? _startDate;

        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime? _endDate;

        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public ICommand AddCommand { get; set; } = null!;
        public ICommand AbordCommand { get; set; } = null!;



    }
}
