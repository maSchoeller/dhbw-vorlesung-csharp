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
    public class VehicleTests
    {
        private static readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();

        private static readonly Vehicle _vehicle1 = new Vehicle
        {
            Id = 1,
            LicensePlate = "1234",
            Brand = "TestMarke",
            Model = "TestModel",
            Version = 1
        };
        private static readonly Vehicle _vehicle2 = new Vehicle
        {
            Id = 2,
            LicensePlate = "12345",
            Brand = "TestMarke",
            Model = "TestModel",
            Version = 1
        };
        private static readonly Vehicle _vehicleSaveFail = new Vehicle
        {
            LicensePlate = "123456",
            Brand = "TestMarke",
            Model = "TestModel"
        };
        private static readonly Vehicle _vehicleSave = new Vehicle
        {
            LicensePlate = "1234567",
            Brand = "TestMarke",
            Model = "TestMarke",
            Insurance = 100,
            LeasingFrom = DateTime.Now,
            LeasingTo = DateTime.Now,
            LeasingRate = 100,
        };
        private static readonly Vehicle _vehicleNotExists = new Vehicle
        {
            Id = 99,
            Brand = "TestMarke",
            Model = "testModel",
        };


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new[] { _vehicle1, _vehicle2 });

            _vehicleRepositoryMock.Setup(x => x.FindById(1))
                .Returns(_vehicle1);
            _vehicleRepositoryMock.Setup(x => x.FindById(2))
                .Returns(_vehicle2);

            _vehicleRepositoryMock.Setup(x => x.Update(_vehicleSave)).Returns((OperationResult.Success, _vehicleSave)); ;
            _vehicleRepositoryMock.Setup(x => x.Save(_vehicleSave)).Returns((OperationResult.Success, _vehicleSave)); ;
            _vehicleRepositoryMock.Setup(x => x.Save(_vehicleSaveFail)).Returns((OperationResult.AlreadyExists,_vehicleSaveFail));

            _vehicleRepositoryMock.Setup(x => x.Update(_vehicle1));

            _vehicleRepositoryMock.Setup(x => x.Delete(_vehicle1)).Returns((OperationResult.Success,_vehicle1));
            _vehicleRepositoryMock.Setup(x => x.Delete(_vehicleNotExists)).Returns((OperationResult.NotFound, _vehicleNotExists));

        }

        [TestMethod]
        public void TestGetAllVehicles()
        {
            var repo = _vehicleRepositoryMock.Object;
            var result = repo.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void TestDelete()
        {
            var repo = _vehicleRepositoryMock.Object;
            var result = repo.Delete(_vehicle1);

            Assert.IsNotNull(result);
            Assert.AreEqual(OperationResult.Success, result.result);
            Assert.AreEqual(_vehicle1, result.entity);
        }

        [TestMethod]
        public void TestDeleteVehicleShouldFail()
        {
            var repo = _vehicleRepositoryMock.Object;
            var result = repo.Delete(_vehicleNotExists);

            Assert.IsNotNull(result);
            Assert.AreEqual(OperationResult.NotFound, result.result);
            Assert.AreEqual(_vehicleNotExists, result.entity);
        }

        [TestMethod]
        public void TestSaveVehicleShouldFail()
        {
            var repo = _vehicleRepositoryMock.Object;
            var result = repo.Save(_vehicleSaveFail);

            Assert.AreEqual(OperationResult.AlreadyExists, result.result);
            Assert.AreEqual(_vehicleSaveFail, result.entity);
        }

        [TestMethod]
        public void TestSaveVehicle()
        {
            var repo = _vehicleRepositoryMock.Object;
            var result = repo.Save(_vehicleSave);

            Assert.AreEqual(OperationResult.Success, result.result);
            Assert.AreEqual(_vehicleSave, result.entity);
        }

        [TestMethod]
        public void TestUpdateVehicle()
        {
            var repo = _vehicleRepositoryMock.Object;
            var result = repo.Update(_vehicleSave);

            Assert.AreEqual(OperationResult.Success, result.result);
            Assert.AreEqual(_vehicleSave, result.entity);
        }

    }
}
