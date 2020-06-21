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
    public class CalulationTests
    {
        private static readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        private static readonly Mock<IBusinessUnitRepository> _BusinessUnitRepositoryMock = new Mock<IBusinessUnitRepository>();

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _vehicleRepositoryMock.Setup(x => x.GetAllVehicleMonthCosts())
                .Returns(new[] {
                    new VehicleMonthCost
                    {
                        Costs = 400,
                        Count = 2,
                        Month = DateTime.Today
                    },
                    new VehicleMonthCost
                    {
                        Costs = 600,
                        Count = 2,
                        Month = DateTime.Today.AddMonths(-1)
                    },
                    new VehicleMonthCost
                    {
                        Costs = 500,
                        Count = 3,
                        Month = DateTime.Today.AddMonths(-2)
                    }
                });

            _BusinessUnitRepositoryMock.Setup(x => x.GetAllCosts())
                .Returns(new[] {
                    new BusinessUnitMonthCost
                    {
                        Costs = 400,
                        BusinessUnit = "CNC",
                        Month = DateTime.Today
                    },
                    new BusinessUnitMonthCost
                    {
                        Costs = 600,
                        BusinessUnit= "EDGE",
                        Month = DateTime.Today.AddMonths(-1)
                    },
                    new BusinessUnitMonthCost
                    {
                        Costs = 500,
                        BusinessUnit = "LAS",
                        Month = DateTime.Today.AddMonths(-2)
                    }
                });
        }

        [TestMethod]
        public void TestVehilceMonthCount()
        {
            var repo = _vehicleRepositoryMock.Object;
            var result = repo.GetAllVehicleMonthCosts();

            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void TestVehicleMonthCosts()
        {
            var repo = _vehicleRepositoryMock.Object;
            var result = repo.GetAllVehicleMonthCosts();

            Assert.AreEqual(400, result.FirstOrDefault(r => r.Month == DateTime.Today).Costs);
            Assert.AreEqual(600, result.FirstOrDefault(r => r.Month == DateTime.Today.AddMonths(-1)).Costs);
            Assert.AreEqual(500, result.FirstOrDefault(r => r.Month == DateTime.Today.AddMonths(-2)).Costs);
        }

        [TestMethod]
        public void TestVehicleMonthCountss()
        {
            var repo = _vehicleRepositoryMock.Object;
            var result = repo.GetAllVehicleMonthCosts();

            Assert.AreEqual(2, result.FirstOrDefault(r => r.Month == DateTime.Today).Count);
            Assert.AreEqual(2, result.FirstOrDefault(r => r.Month == DateTime.Today.AddMonths(-1)).Count);
            Assert.AreEqual(3, result.FirstOrDefault(r => r.Month == DateTime.Today.AddMonths(-2)).Count);
        }

        [TestMethod]
        public void TestBusinessUnitMonthCounts()
        {
            var repo = _BusinessUnitRepositoryMock.Object;
            var result = repo.GetAllCosts();

            Assert.AreEqual(3, result.Count());
        }


        [TestMethod]
        public void TestBusinessUnitMonthCosts()
        {
            var repo = _BusinessUnitRepositoryMock.Object;
            var result = repo.GetAllCosts();

            Assert.AreEqual(400, result.FirstOrDefault(r => r.Month == DateTime.Today).Costs);
            Assert.AreEqual(600, result.FirstOrDefault(r => r.Month == DateTime.Today.AddMonths(-1)).Costs);
            Assert.AreEqual(500, result.FirstOrDefault(r => r.Month == DateTime.Today.AddMonths(-2)).Costs);
        }

        [TestMethod]
        public void TestBusinessUnitMonthNames()
        {
            var repo = _BusinessUnitRepositoryMock.Object;
            var result = repo.GetAllCosts();

            Assert.AreEqual("CNC", result.FirstOrDefault(r => r.Month == DateTime.Today).BusinessUnit);
            Assert.AreEqual("EDGE", result.FirstOrDefault(r => r.Month == DateTime.Today.AddMonths(-1)).BusinessUnit);
            Assert.AreEqual("LAS", result.FirstOrDefault(r => r.Month == DateTime.Today.AddMonths(-2)).BusinessUnit);
        }
    }
}
