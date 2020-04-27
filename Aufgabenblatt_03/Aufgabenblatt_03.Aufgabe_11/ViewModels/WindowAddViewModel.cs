using Aufgabenblatt_03.Aufgabe_11.Framework;
using Aufgabenblatt_03.Aufgabe_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Aufgabenblatt_03.Aufgabe_11.ViewModels
{
    public class WindowAddViewModel : ViewModelBase
    {
        public Array Genders => Enum.GetValues(typeof(Gender));
        public Employee Model { get; set; } = new Employee();
        public ICommand OkCommand { get; set; } = null!;
        public ICommand CancelCommand { get; set; } = null!;

        public string Firstname
        {
            get => Model.Firstname; 
            set {
                if (Model.Firstname != value)
                {
                    Model.Firstname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Lastname
        {
            get => Model.Lastname;
            set
            {
                if (Model.Lastname != value)
                {
                    Model.Lastname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Street
        {
            get => Model.Address.Street;
            set
            {
                if (Model.Address.Street != value)
                {
                    Model.Address.Street = value;
                    OnPropertyChanged();
                }
            }
        }

        public string City
        {
            get => Model.Address.City;
            set
            {
                if (Model.Address.City != value)
                {
                    Model.Address.City = value;
                    OnPropertyChanged();
                }
            }
        }

        public Gender Gender
        {
            get => Model.Gender;
            set
            {
                if (Model.Gender != value)
                {
                    Model.Gender = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Postcode
        {
            get => Model.Address.Postcode;
            set
            {
                if (Model.Address.Postcode != value)
                {
                    Model.Address.Postcode = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
