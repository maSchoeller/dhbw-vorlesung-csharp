using Aufgabenblatt_03.Aufgabe_11.Framework;
using Aufgabenblatt_03.Aufgabe_11.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Aufgabenblatt_03.Aufgabe_11.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public Employee? SelectedEmployee { get; set; } = null;
        public ICommand AddCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;
    }
}
