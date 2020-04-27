using Aufgabenblatt_03.Aufgabe_11.Framework;
using Aufgabenblatt_03.Aufgabe_11.Models;
using Aufgabenblatt_03.Aufgabe_11.ViewModels;
using Aufgabenblatt_03.Aufgabe_11.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_03.Aufgabe_11.Controllers
{
    public class MainWindowController
    {
        private readonly MainWindow _view;
        private readonly MainWindowViewModel _viewModel;

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
                _viewModel.Employees.Add(employee);
            }
        }

        private void ExecuteDeleteCommand(object _) 
            => _viewModel.Employees.Remove(_viewModel.SelectedEmployee!);

        private bool CanExecuteDeleteCommand(object _)
            => _viewModel.SelectedEmployee != null;

        public void Initialize()
        {
            SeedData();
            _view.Show();
        }

        private void SeedData()
        {
            _viewModel.Employees.Add(new Employee
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

            _viewModel.Employees.Add(new Employee
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
            _viewModel.Employees.Add(new Employee
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
