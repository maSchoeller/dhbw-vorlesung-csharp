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
    internal class CalculationService : BaseService, ICalculationService
    {
        private readonly ILogger<UserService>? _logger;

        public CalculationService(ISecurityHelper connectionHelper, ILogger<UserService>? logger = null)
            : base(connectionHelper)
        {
            _logger = logger;
        }

        public string GetTestData()
        {
            if (!Validate())
                throw new SecurityAccessDeniedException();

            return "Here is the sugar.";
        }
    }
}
