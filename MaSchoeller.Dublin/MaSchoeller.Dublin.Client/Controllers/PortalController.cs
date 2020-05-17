using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class PortalController : ControllerBase
    {
        private readonly PortalViewModel _viewModel;

        public PortalController(PortalViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override object Initialize()
        {
            return _viewModel;
        }
        protected override void Leave()
        {
        }
    }
}
