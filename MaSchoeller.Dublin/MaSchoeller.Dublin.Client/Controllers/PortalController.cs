﻿using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class PortalController : ControllerBase
    {
        private readonly PortalViewModel _viewModel;
        private readonly ChangePasswordDialogController _passwordController;

        public PortalController(PortalViewModel viewModel, ChangePasswordDialogController passwordController)
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
            _passwordController.ShowDialog();
        }
    }
}
