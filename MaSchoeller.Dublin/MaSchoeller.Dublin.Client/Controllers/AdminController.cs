using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly AdminViewModel _viewModel;

        public AdminController(AdminViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override object Initialize()
        {
            return _viewModel;
        }
    }
}
