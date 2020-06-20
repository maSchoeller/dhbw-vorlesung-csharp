using MaSchoeller.Dublin.Core.Communications.Models;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
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
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBusinessUnitRepository _businessUnitRepository;
        private readonly ILogger<UserService>? _logger;

        public CalculationService(ISecurityHelper connectionHelper,
                                  IVehicleRepository vehicleRepository,
                                  IBusinessUnitRepository businessUnitRepository,
                                  ILogger<UserService>? logger = null)
            : base(connectionHelper)
        {
            _vehicleRepository = vehicleRepository;
            _businessUnitRepository = businessUnitRepository;
            _logger = logger;
        }

        public IEnumerable<VehicleMonthCost> GetCalculationMonthSets()
        {
            return _vehicleRepository.GetAllVehicleMonthCosts();
        }

        public IEnumerable<BusinessUnitMonthCost> GetCalculationsBusinessUnitSets()
        {
            return _businessUnitRepository.GetAllCosts();
        }
    }
}
