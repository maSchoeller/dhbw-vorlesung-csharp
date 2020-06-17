using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
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
            BusinessUnit = employee.BusinessUnit;
            EmployeeNumber = employee.EmployeeNumber;
            Firstname = employee.Firstname;
            Lastname = employee.Lastname;
            Salutation = employee.Salutation;
            Title = employee.Title;
            Version = employee.Version;
        }

        private BusinessUnit _businessUnit;
        public BusinessUnit BusinessUnit
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


        private int _employeeNumber;
        public int EmployeeNumber
        {
            get => _employeeNumber;
            set
            {
                SetProperty(ref _employeeNumber, value);
                EditState = EditState.Modified;
            }
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                SetProperty(ref _lastname, value);
                EditState = EditState.Modified;
            }
        }

        private string _salutation;
        public string Salutation
        {
            get => _salutation;
            set
            {
                SetProperty(ref _salutation, value);
                EditState = EditState.Modified;
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
                EditState = EditState.Modified;
            }
        }

        private string _firstname;

        public string Firstname
        {
            get => _firstname;
            set
            {
                SetProperty(ref _firstname, value);
                EditState = EditState.Modified;
            }
        }

        public Employee AsEmployee()
        {
            return new Employee
            {
                Id = Id,
                BusinessUnit = BusinessUnit,
                EmployeeNumber = EmployeeNumber,
                Firstname = Firstname,
                Lastname = Lastname,
                Salutation = Salutation,
                Title = Title,
                Version = Version
            };
        }
    }
}
