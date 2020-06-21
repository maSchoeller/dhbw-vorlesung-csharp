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
    public class UserTests
    {
        private static readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private static readonly User _maSchoeller = new User
        {
            Id = 1,
            Firstname = "Marvin",
            Lastname = "Schoeller",
            Username = "maSchoeller",
            IsAdmin = true,
            PasswordHash = "$2y$10$1coNSX5TTw3cUDZdXw1AhOoRw6ewMTcVAU.1YVx.gEzQLpQNnvXjq",
            Version = 1
        };
        private static readonly User _maSchoeller2 = new User
        {
            Id = 1,
            Firstname = "Marvin",
            Lastname = "Schoeller",
            Username = "maSchoeller2",
            IsAdmin = true,
            PasswordHash = "$2y$10$1coNSX5TTw3cUDZdXw1AhOoRw6ewMTcVAU.1YVx.gEzQLpQNnvXjq",
            Version = 1
        };
        private static readonly User _shouldFail = new User
        {
            Id = 1,
            Firstname = "John",
            Lastname = "Doe",
            Username = "john",
            IsAdmin = false,
            PasswordHash = "$2y$10$1coNSX5TTw3cUDZdXw1AhOoRw6ewMTcVAU.1YVx.gEzQLpQNnvXjq",
            Version = 1
        };

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _userRepositoryMock.Setup(x => x.GetAll())
                .Returns(new[] { _maSchoeller, _maSchoeller2});

            _userRepositoryMock.Setup(x => x.FindById(1))
                .Returns(_maSchoeller);

            _userRepositoryMock.Setup(x => x.Delete(_shouldFail)).Returns((OperationResult.NotFound,_shouldFail));
            _userRepositoryMock.Setup(x => x.Delete(_maSchoeller)).Returns((OperationResult.Success,_maSchoeller));

        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            var repo = _userRepositoryMock.Object;
            var result = repo.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void TestGetUserById()
        {
            var repo = _userRepositoryMock.Object;
            var result = repo.FindById(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetByIdShouldFail()
        {
            var repo = _userRepositoryMock.Object;
            var result = repo.FindById(2);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestDeleteUserShoudlFail()
        {
            var repo = _userRepositoryMock.Object;
            var result = repo.Delete(_shouldFail);

            Assert.AreEqual(OperationResult.NotFound, result.result);
            Assert.AreEqual(_shouldFail, result.entity);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            var repo = _userRepositoryMock.Object;
            var result = repo.Delete(_maSchoeller);

            Assert.AreEqual(OperationResult.Success, result.result);
            Assert.AreEqual(_maSchoeller, result.entity);
        }
    }
}
