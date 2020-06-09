﻿using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System.ServiceModel.Security;

namespace MaSchoeller.Dublin.Core.Communications
{
    internal class FleetService : BaseService, IFleetService
    {
        private readonly ILogger<UserService>? _logger;

        public FleetService(ISecurityHelper connectionHelper, ILogger<UserService>? logger = null)
            : base(connectionHelper)
        {
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
    }
}
