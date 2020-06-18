using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace MaSchoeller.Dublin.Core.Communications
{
    [ServiceBehavior]
    internal class CalculationService : BaseService, ICalculationService
    {
        private readonly ILogger<UserService>? _logger;

        public CalculationService(ISecurityHelper connectionHelper,
                                  IVehicleRepository vehicleRepository,
                                  ILogger<UserService>? logger = null)
            : base(connectionHelper)
        {
            _logger = logger;
        }

        public IEnumerable<CalculationMonthSet> GetCalculationMonthSets()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CalculationsBusinessUnitSet> GetCalculationsBusinessUnitSets()
        {
            throw new System.NotImplementedException();
        }
    }
}
