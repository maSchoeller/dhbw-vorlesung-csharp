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
    public class BusinessUnitTests
    {
        private static readonly Mock<IBusinessUnitRepository> _businessUnitRepositoryMock = new Mock<IBusinessUnitRepository>();
        private static readonly BusinessUnit _cnc = new BusinessUnit
        {
            Id = 1,
            Name = "CNC",
            Version = 1
        };
        private static readonly BusinessUnit _edge = new BusinessUnit
        {
            Id = 2,
            Name = "EDGE",
            Version = 1
        };
        private static readonly BusinessUnit _las = new BusinessUnit
        {
            Name = "Lifecycle and Service",
        };


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _businessUnitRepositoryMock.Setup(x => x.GetAll())
                .Returns(new[] { _cnc, _edge });

            _businessUnitRepositoryMock.Setup(x => x.FindById(1))
                .Returns(_cnc);
            _businessUnitRepositoryMock.Setup(x => x.FindById(2))
                .Returns(_edge);

            _businessUnitRepositoryMock.Setup(x => x.Save(_las)).Returns((OperationResult.AlreadyExists, _las));
        }

        [TestMethod]
        public void TestGetAllBusinessUnits()
        {
            var repo = _businessUnitRepositoryMock.Object;
            var result = repo.GetAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void TestGetBusinessUnitById()
        {
            var repo = _businessUnitRepositoryMock.Object;
            var result = repo.FindById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(_cnc, result);
        }

        [TestMethod]
        public void TestSaveBusinessShouldFailSameName()
        {
            var repo = _businessUnitRepositoryMock.Object;
            var result = repo.Save(_las);

            Assert.IsNotNull(result);
            Assert.AreEqual(OperationResult.AlreadyExists, result.result);
            Assert.AreEqual(_las, result.entity);
        }
    }
}
