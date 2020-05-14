using Aufgabenblatt_04.Aufgabe_13.Client.CustomerServiceProxy;
using Aufgabenblatt_04.Aufgabe_13.Client.ViewModels;
using Aufgabenblatt_04.Aufgabe_13.Client.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_04.Aufgabe_13.Client.Controllers
{
    public class AddController
    {
        private AddViewModel _viewModel;
        private Customer _customer;
        public AddController()
        {
            _customer = new Customer();
            _viewModel = new AddViewModel(_customer);
        }

        public Customer CreateNewCustomer()
        {
            _customer = new Customer();
            _viewModel = new AddViewModel(_customer);
            var window = new AddWindow();
            _viewModel.OkCommand = new RelayCommand(
                _ => window.DialogResult = true,
                _ =>
                {
                    return !string.IsNullOrWhiteSpace(_customer.Firstname) && !string.IsNullOrWhiteSpace(_customer.Lastname);
                    });

            _viewModel.CancelCommand = new RelayCommand(
                _ => window.DialogResult = false);
            window.DataContext = _viewModel;
            var result = window.ShowDialog();
            if (result.HasValue && result.Value)
            {
                return _customer;
            }
            else
            {
                return null;
            }
        }


    }
}
