using Aufgabenblatt_04.Aufgabe_13.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aufgabenblatt_04.Aufgabe_13.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers;

        public CustomerService()
        {
            _customers = new List<Customer>()
            {
                new Customer
                {
                    Firstname = "Jens",
                    Lastname = "Mander",
                    IsPremiumCustomer = true
                },
                new Customer
                {
                    Firstname = "Hans",
                    Lastname = "Wurst",
                    IsPremiumCustomer = false
                },
                new Customer
                {
                    Firstname = "Guybrush",
                    Lastname = "Threepwod",
                    IsPremiumCustomer = true
                }
            };
        }

        public bool AddCustomer(Customer Parameter)
        {
            if (_customers.Contains(Parameter))
                return false;
            _customers.Add(Parameter);
            return true;
        }
        public List<Customer> GetAllCustomers()
            => _customers;

        public List<Customer> GetCustomers(string name)
            => _customers.Where(c => c.Lastname.Contains(name)).ToList();

        public bool RemoveCustomer(Customer Parameter)
            => _customers.Remove(Parameter);
    }
}
