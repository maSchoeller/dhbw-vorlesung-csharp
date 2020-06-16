using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class ConfigBuisnessUnitController : ControllerBase
    {
        private readonly ConfigBuisnessUnitViewModel _viewModel;

        public ConfigBuisnessUnitController(ConfigBuisnessUnitViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override object Initialize()
        {
            return _viewModel;
        }


        private void ExecuteNewCommand()
        {

        }

        private void ExecuteSaveCommand()
        {

        }

        public void ExecuteDeleteCommand()
        {

        }
    }
}
