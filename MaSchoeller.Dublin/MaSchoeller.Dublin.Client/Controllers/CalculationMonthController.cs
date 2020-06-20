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
    public class CalculationMonthController : ControllerBase
    {
        private readonly CalculationMonthViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;

        public CalculationMonthController(CalculationMonthViewModel viewModel,
                                          ClientConnectionHandler connectionHandler)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
        }

        public override object Initialize()
        {
            return _viewModel;
        }

        public override async Task EnterAsync()
        {
            var client = _connectionHandler.CalculationClient;
            _viewModel.Costs = await client.GetCalculationMonthSetsAsync();
        }
    }
}
