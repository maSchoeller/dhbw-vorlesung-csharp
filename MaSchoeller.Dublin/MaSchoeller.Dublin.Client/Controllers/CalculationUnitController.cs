using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class CalculationUnitController : ControllerBase
    {
        private readonly CalculationUnitViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ConnectionLostHelper _lostHelper;

        public CalculationUnitController(CalculationUnitViewModel viewModel, 
                                         ClientConnectionHandler connectionHandler, 
                                         ConnectionLostHelper lostHelper)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _lostHelper = lostHelper;
        }


        public override object Initialize()
        {
            return _viewModel;
        }

        public override async Task EnterAsync()
        {
            await _lostHelper.InvokeAsync(async () =>
            {
                var client = _connectionHandler.CalculationClient;
                _viewModel.Costs = await client.GetCalculationsBusinessUnitSetsAsync();
            });
        }
    }
}
