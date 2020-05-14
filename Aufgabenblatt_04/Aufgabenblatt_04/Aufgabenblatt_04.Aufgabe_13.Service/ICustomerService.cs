using Aufgabenblatt_04.Aufgabe_13.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aufgabenblatt_04.Aufgabe_13.Service
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        bool AddCustomer(Customer Parameter);

        [OperationContract]
        bool RemoveCustomer(Customer Parameter);

        [OperationContract]
        List<Customer> GetAllCustomers();

        [OperationContract]
        List<Customer> GetCustomers(string name);
    }
}
