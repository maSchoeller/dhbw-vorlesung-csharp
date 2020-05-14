using Aufgabenblatt_05.Aufgabe_13.Framework;
using Aufgabenblatt_05.Aufgabe_13.Models;
using Aufgabenblatt_05.Aufgabe_13.ViewModels;
using Aufgabenblatt_05.Aufgabe_13.Views;
using Augabenblatt_05.Aufgabe_13.Framework;
using System.Collections.ObjectModel;

namespace Aufgabenblatt_05.Aufgabe_13.Controllers
{
    public class MainWindowController
    {
        private readonly MainWindow _view;
        private readonly MainWindowViewModel _viewModel;
        private Repository<Customer> _customerRepository;

        public MainWindowController()
        {
            _viewModel = new MainWindowViewModel()
            {
                AddCommand = new RelayCommand(ExecuteAddCommand),
                DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand),
            };
            _view = new MainWindow
            {
                DataContext = _viewModel
            };
        }

        private void ExecuteAddCommand(object _)
        {
            var controller = new WindowAddController();
            var employee = controller.AddEmployee();
            if (employee != null)
            {
                _customerRepository.Save(employee);
                _viewModel.Employees = new ObservableCollection<Customer>(_customerRepository.GetAll());
            }
        }

        private void ExecuteDeleteCommand(object _)
        {
            _customerRepository.Delete(_viewModel.SelectedEmployee!);
            _viewModel.Employees = new ObservableCollection<Customer>(_customerRepository.GetAll());
        }

        private bool CanExecuteDeleteCommand(object _)
        {
            return _viewModel.SelectedEmployee != null;
        }

        public void Initialize()
        {
            _customerRepository = new Repository<Customer>(@"Database\CustomerSample.db");
            //SeedData();
            _viewModel.Employees = new ObservableCollection<Customer>(_customerRepository.GetAll());
            _view.Show();
        }

        private void SeedData()
        {
            _customerRepository.Save(new Customer
            {
                Firstname = "Marvin",
                Lastname = "Schoeller",
                Gender = Gender.Male,
                Address = new Address
                {
                    City = "Dornstetten",
                    Postcode = 72280,
                    Street = "Schießgrabenstraße",
                    StreetNumber = "12"
                }
            });
            _customerRepository.Save(new Customer
            {

                Firstname = "Mario",
                Lastname = "Lang",
                Gender = Gender.Male,
                Address = new Address
                {
                    City = "Dornstetten",
                    Postcode = 72280,
                    Street = "Jägermühle",
                    StreetNumber = "43"
                }
            });
            _customerRepository.Save(new Customer
            {
                Firstname = "Jana",
                Lastname = "Schoeller",
                Gender = Gender.Male,
                Address = new Address
                {
                    City = "Dornstetten",
                    Postcode = 72280,
                    Street = "Badgasse",
                    StreetNumber = "12"
                }
            });
        }
    }
}
