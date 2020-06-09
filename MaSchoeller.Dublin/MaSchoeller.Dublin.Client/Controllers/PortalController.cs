using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;
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
        private readonly ChangePasswordController _passwordController;

        public PortalController(PortalViewModel viewModel, ChangePasswordController passwordController)
        {
            _viewModel = viewModel;
            _passwordController = passwordController;
        }

        public override object Initialize()
        {
            _viewModel.ChangePassswordCommand = 
                ConfigurableCommand.Create(ChangePasswordExecute).Build();
            return _viewModel;
        }

        public void ChangePasswordExecute(object o)
        {
            _passwordController.ShowPasswordDialog();
        }
    }
}
