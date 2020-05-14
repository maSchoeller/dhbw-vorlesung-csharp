using Aufgabenblatt_04.Aufgabe_13.Client.CustomerServiceProxy;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Aufgabenblatt_04.Aufgabe_13.Client.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();

        private Customer _selectedCustomer;
        public Customer SelectedCustomer { get => _selectedCustomer; set => SetProperty(ref _selectedCustomer, value); }

        private string _searchText;
        public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }

    }
}
