using MaSchoeller.Dublin.Core.Database;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        private static readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        
        private static readonly Employee _emp1 = new Employee
        {
            Id = 1,
            EmployeeNumber = 1,
            Firstname = "Emp1",
            Lastname = "",
            Version = 1
        };
        private static readonly Employee _emp2 = new Employee
        {
            Id = 2,
            EmployeeNumber = 2,
            Firstname = "Emp2",
            Lastname = "",
            Version = 1
        };
        private static readonly Employee _empSave = new Employee
        {
            EmployeeNumber = 1,
            Firstname = "Fail",
            Lastname = ""
        };


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _employeeRepositoryMock.Setup(x => x.GetAll())
                .Returns(new[] { _emp1, _emp2 });

            _employeeRepositoryMock.Setup(x => x.FindById(1))
                .Returns(_emp1);
            _employeeRepositoryMock.Setup(x => x.FindById(2))
                .Returns(_emp2);

            _employeeRepositoryMock.Setup(x => x.Save(_empSave))
               .Returns((OperationResult.AlreadyExists,_empSave));
            _employeeRepositoryMock.Setup(x => x.Delete(_emp1)).Returns((OperationResult.Success,_emp1));
        }

        [TestMethod]
        public void TestGetAllEmployees()
        {
            var repo = _employeeRepositoryMock.Object;
            var result = repo.GetAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void TestDeleteEmployee()
        {
            var repo = _employeeRepositoryMock.Object;
            var result = repo.Delete(_emp1);

            Assert.IsNotNull(result);
            Assert.AreEqual(OperationResult.Success, result.result);
            Assert.AreEqual(_emp1, result.entity);
        }

        [TestMethod]
        public void TestSaveShouldFailEmployeeNumberAlreadyExists()
        {
            var repo = _employeeRepositoryMock.Object;
            var result = repo.Save(_empSave);

            Assert.IsNotNull(result);
            Assert.AreEqual(OperationResult.AlreadyExists, result.result);
            Assert.AreEqual(_empSave, result.entity);
        }
    }
}
