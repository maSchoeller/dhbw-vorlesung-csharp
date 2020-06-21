using MaSchoeller.Dublin.Core.Configurations;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
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
    public class SecurityHelperTests
    {
        private static readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private static ISecurityHelper _securityHelper;
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
        private static readonly User _nonExistentUser = new User
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
            _userRepositoryMock.Setup(x => x.FindByName("maSchoeller"))
                .Returns(_maSchoeller);
            _userRepositoryMock.Setup(x => x.FindByName("john")).Returns(() => null);
            _securityHelper = new JwtHelper(new ServerConfiguration { TokenSecret = "sdgh difjhg dfjb dilfjb dfj beörkeprihokv jhdflkvhdflkjhglkdfubhgxfbölci" }, _userRepositoryMock.Object);
        }


        [TestMethod]
        public void TestPasswordChangeWithCorrectOldPassword()
        {
            var result = _securityHelper.UpdatePassword("maSchoeller", "geheim", "super");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestPasswordChangeWithIncorrectOldPassword()
        {
            var result = _securityHelper.UpdatePassword("maSchoeller", "falsches password", "super");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPasswordChangeWithNonExistentUser()
        {
            var result = _securityHelper.UpdatePassword("Mich gibt es nicht", "gehiem", "super");
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void TestLoginShouldWork()
        {
            var result = _securityHelper.Login("maSchoeller", "geheim");
            Assert.IsTrue(result.success);
            Assert.IsNotNull(result.user);
        }

        [TestMethod]
        public void TestLoginShouldFailWithIncorrectPassword()
        {
            var result = _securityHelper.Login("maSchoeller", "super");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.success);
            Assert.IsNotNull(result.user);
        }

        [TestMethod]
        public void TestLoginShouldFailNoExistingUser()
        {
            var result = _securityHelper.Login("john", "geheim");
            Assert.IsFalse(result.success);
            Assert.IsNull(result.user);
        }
    }
}
