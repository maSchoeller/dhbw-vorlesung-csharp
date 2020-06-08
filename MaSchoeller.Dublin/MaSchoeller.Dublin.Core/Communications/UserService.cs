using MaSchoeller.Dublin.Core.Abstracts;
using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;

namespace MaSchoeller.Dublin.Core.Communications
{
    internal class UserService : BaseService, IUserService
    {
        private readonly IConnectionFactory _factory;
        private readonly ILogger<UserService>? _logger;

        public UserService(ISecurityHelper connectionHelper, 
                           IConnectionFactory factory, 
                           ILogger<UserService>? logger = null)
            : base(connectionHelper)
        {
            _factory = factory;
            _logger = logger;
        }

        public string GetTestData()
        {
            if (!Validate())
            {
                throw new SecurityAccessDeniedException();
            }

            return "Here is the sugar.";
        }

        public LoginResult Login(string username, string password)
        {
            return new LoginResult
            {
                Success = true,
                Token = SecurityHelper.CreateToken(username),
                IsAdmin = true
            };
        }
    }
}
