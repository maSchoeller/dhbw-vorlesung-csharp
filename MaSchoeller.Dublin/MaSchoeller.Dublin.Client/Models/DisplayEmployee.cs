using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MaSchoeller.Dublin.Client.Models
{
    public class DisplayEmployee : DisplayBase
    {
        public DisplayEmployee(Employee employee)
        {
            Id = employee.Id;
            Version = employee.Version;
            _businessUnit = employee.BusinessUnit;
            _employeeNumber = employee.EmployeeNumber;
            _firstname = employee.Firstname;
            _lastname = employee.Lastname;
            _salutation = employee.Salutation;
            _title = employee.Title;
            IsSynced = true;
        }
        public DisplayEmployee()
        {

        }

        private BusinessUnit? _businessUnit;
        public BusinessUnit? BusinessUnit
        {
            get => _businessUnit;
            set
            {
                SetProperty(ref _businessUnit, value);
                EditState = EditState.Modified;
            }
        }

        public int Id { get; set; }
        public int Version { get; set; }


        private int? _employeeNumber;
        public int? EmployeeNumber
        {
            get => _employeeNumber;
            set
            {
                SetProperty(ref _employeeNumber, value);
                EditState = EditState.Modified;

            }
        }

        private string _lastname = string.Empty;
        public string Lastname
        {
            get => _lastname;
            set
            {
                SetProperty(ref _lastname, value);
                RaisePropertyChanged(nameof(Fullname));
                EditState = EditState.Modified;
            }
        }

        private string _salutation = string.Empty;
        public string Salutation
        {
            get => _salutation;
            set
            {
                SetProperty(ref _salutation, value);
                EditState = EditState.Modified;
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
                EditState = EditState.Modified;
            }
        }

        private string _firstname = string.Empty;

        public string Firstname
        {
            get => _firstname;
            set
            {
                SetProperty(ref _firstname, value);
                RaisePropertyChanged(nameof(Fullname));
                EditState = EditState.Modified;
            }
        }

        public string Fullname => $"{Firstname} {Lastname}";

        public override bool Validate()
        {
            if (EmployeeNumber is null ||
                BusinessUnit is null ||
                string.IsNullOrWhiteSpace(Firstname) ||
                string.IsNullOrWhiteSpace(Lastname))
            {
                return false;
            }
            return true;
        }

        public Employee? AsEmployee()
        {
            var result = Validate();
            return !result ? null : new Employee
            {
                Id = Id,
                BusinessUnit = BusinessUnit,
                EmployeeNumber = EmployeeNumber!.Value,
                Firstname = Firstname,
                Lastname = Lastname,
                Salutation = Salutation,
                Title = Title,
                Version = Version
            };
        }
    }
}
