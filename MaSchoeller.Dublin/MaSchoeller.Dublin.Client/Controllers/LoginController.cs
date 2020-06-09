using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Abstracts;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ClientConnectionHandler _clientHandler;
        private readonly LoginViewModel _viewModel;
        private readonly INavigationService _navigationService;

        public LoginController(ClientConnectionHandler client, LoginViewModel viewModel, INavigationService navigationService)
        {
            _clientHandler = client;
            _viewModel = viewModel;
            _navigationService = navigationService;
        }

        public override object Initialize()
        {
            _viewModel.LoginCommand = ConfigurableCommand
                .Create(TryLogin, CanTryLogin)
                .Observe(_viewModel, () => _viewModel.Username)
                .Build();

            return _viewModel;
        }
        protected override void Enter()
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.Username = string.Empty;
            _viewModel.IsBusy = false;
            _viewModel.Password = "";
        }

        #region LoginCommand
        private bool CanTryLogin(object arg)
           => !string.IsNullOrWhiteSpace(_viewModel.Username);

        private async void TryLogin(object obj)
        {
            _viewModel.IsBusy = true;
            var (success, errormessage) = await _clientHandler.TryLoginAsync(_viewModel.Username, _viewModel.ClearPassword);
            if (success)
            {
                _viewModel.ErrorMessage = "";
                _navigationService.NavigateTo(DublinNavigations.PortalRoute);
            }
            else
            {
                _viewModel.ErrorMessage = errormessage!;
            }
            _viewModel.IsBusy = false;
        }
        #endregion
    }
}
