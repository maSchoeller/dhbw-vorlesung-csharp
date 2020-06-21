using MaSchoeller.Dublin.Client.Proxies.Calculations;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class CalculationUnitController : ControllerBase
    {
        private readonly CalculationUnitViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ConnectionLostHelper _lostHelper;
        private readonly ILogger<CalculationUnitController>? _logger;

        public CalculationUnitController(CalculationUnitViewModel viewModel, 
                                         ClientConnectionHandler connectionHandler, 
                                         ConnectionLostHelper lostHelper,
                                         ILogger<CalculationUnitController>? logger = null)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _lostHelper = lostHelper;
            _logger = logger;
        }


        public override object Initialize()
        {
            return _viewModel;
        }

        public override async Task EnterAsync()
        {
            _viewModel.IsBusy = true;
            _viewModel.Costs = new List<BusinessUnitMonthCost>();
            try
            {
                var client = _connectionHandler.CalculationClient;
                _viewModel.Costs = await client.GetCalculationsBusinessUnitSetsAsync();
            }
            catch (Exception e)
            {
                _logger?.LogWarning(e, "");
                _lostHelper.ShowConnectionLost();
            }
            _viewModel.IsBusy = false;
        }
    }
}
