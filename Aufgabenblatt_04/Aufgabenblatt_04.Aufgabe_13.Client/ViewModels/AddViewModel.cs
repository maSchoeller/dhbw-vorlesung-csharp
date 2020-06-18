using Aufgabenblatt_04.Aufgabe_13.Client.CustomerServiceProxy;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Aufgabenblatt_04.Aufgabe_13.Client.ViewModels
{
    public class AddViewModel : BindableBase
    {
        private readonly Customer _customer;

        public AddViewModel(Customer customer)
        {
            _customer = customer;
        }


        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string Lastname
        {
            get => _customer.Lastname;
            set {
                if (_customer.Lastname == value)
                    return;
                _customer.Lastname = value;
                RaisePropertyChanged();
            }
        }


        public string Firstname
        {
            get => _customer.Firstname;
            set
            {
                if (_customer.Firstname == value)
                    return;
                _customer.Firstname = value;
                RaisePropertyChanged();
            }
        }
    }
}
