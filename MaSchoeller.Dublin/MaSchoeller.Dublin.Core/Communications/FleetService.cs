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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class FleetService : IFleetService
    {
        private readonly JwtHelper _connectionHelper;
        private readonly ILogger<FleetService>? _logger;

        public FleetService(JwtHelper connectionHelper, ILogger<FleetService>? logger = null)
        {
            _connectionHelper = connectionHelper;
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
                Token = _connectionHelper.CreateToken(username, Enumerable.Empty<string>()),
                IsAdmin = true
            };
        }

        private bool Validate()
        {
            if (OperationContext.Current.IncomingMessageHeaders.FindHeader("TokenHeader", "TokenNameSpace") == -1)
            {
                return false;
            }
            var token = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("TokenHeader", "TokenNameSpace");
            if (token is null)
            {
                return false;
            }
            return _connectionHelper.ValidateToken(token);
        }
    }
}
