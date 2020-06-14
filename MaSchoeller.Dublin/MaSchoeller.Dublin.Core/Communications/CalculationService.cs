using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace MaSchoeller.Dublin.Core.Communications
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
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
