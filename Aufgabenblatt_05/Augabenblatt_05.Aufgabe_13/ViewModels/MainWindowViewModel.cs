using Aufgabenblatt_05.Aufgabe_13.Framework;
using Aufgabenblatt_05.Aufgabe_13.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Aufgabenblatt_05.Aufgabe_13.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> _employees;
        public ObservableCollection<Customer> Employees { get => _employees; set => SetProperty(ref _employees, value); } 
        public Customer SelectedEmployee { get; set; } = null!;
        public ICommand AddCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;
    }
}
