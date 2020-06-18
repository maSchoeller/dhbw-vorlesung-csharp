using Aufgabenblatt_04.Aufgabe_13.Client.CustomerServiceProxy;
using Aufgabenblatt_04.Aufgabe_13.Client.ViewModels;
using Aufgabenblatt_04.Aufgabe_13.Client.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_04.Aufgabe_13.Client.Controllers
{
    public class MainController
    {
        private readonly MainWindow _mainWindow;
        private readonly MainViewModel _mainViewModel;
        private readonly AddController _addController;
        private readonly CustomerServiceClient _serviceClient;

        public MainController()
        {
            _addController = new AddController();
            _mainViewModel = new MainViewModel();
            _serviceClient = new CustomerServiceClient();

            _mainViewModel.AddCommand = new RelayCommand( _ =>
            {
                var customer = _addController.CreateNewCustomer();
                if (!(customer is null))
                {
                    var result = _serviceClient.AddCustomer(customer);
                    if (result)
                         LoadAll();
                }

            });

            _mainViewModel.ClearCommand = new RelayCommand(
                _ => ClearCustomers(),
                _ => _mainViewModel.Customers.Count != 0);

            _mainViewModel.LoadCommand = new RelayCommand( _ => LoadAll());

            _mainViewModel.RemoveCommand = new RelayCommand(  _ => 
            { 
                var result =  _serviceClient.RemoveCustomer(_mainViewModel.SelectedCustomer);
                LoadAll();
            }, 
            _ => _mainViewModel.SelectedCustomer != null);

            _mainViewModel.SearchCommand = new RelayCommand( _ =>
            {
                var result =   _serviceClient.GetCustomers(_mainViewModel.SearchText);
                FillInCollection(result);
            }, 
            _ => !string.IsNullOrWhiteSpace(_mainViewModel.SearchText));

            _mainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };
            _mainWindow.Show();
        }

        private void ClearCustomers()
            => _mainViewModel.Customers.Clear();

        private  void LoadAll()
            => FillInCollection(_serviceClient.GetAllCustomers());

        private void FillInCollection(IEnumerable<Customer> customers)
        {
            ClearCustomers();
            foreach (var c in customers)
            {
                _mainViewModel.Customers.Add(c);
            }
        }

    }
}
