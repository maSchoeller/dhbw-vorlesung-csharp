using Antlr.Runtime.Misc;
using MaSchoeller.Dublin.Client.Proxies.Users;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Dublin.Client.Views;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class ChangePasswordController
    {
        private readonly ChangePasswordViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;

        private readonly AutoResetEvent _blocker = new AutoResetEvent(false);

        public ChangePasswordController(ChangePasswordViewModel viewModel,
                                        ClientConnectionHandler connectionHandler)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _viewModel.ChangeCommand =
                 ConfigurableCommand.Create(PasswordChangeExecute).Build();
        }


        public void ShowPasswordDialog()
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.OldPassword = string.Empty;
            _viewModel.NewOnePassword = string.Empty;
            _viewModel.NewTwoPassword = string.Empty;

            var window = new ChangePasswordWindow
            {
                DataContext = _viewModel
            };
            window.Owner = Application.Current.MainWindow;

            Task.Run(() => { _blocker.WaitOne(); Application.Current.Dispatcher.Invoke(() => window.DialogResult = true);});
            
            window.ShowDialog();
        }

        private async void PasswordChangeExecute(object obj)
        {
            _viewModel.IsBusy = true;
            if (_viewModel.OldPasswordClear == _viewModel.NewOnePasswordClear)
            {
                _viewModel.ErrorMessage = "Das alte und neue Passwort stimmen über ein.";
            }
            else if (_viewModel.NewOnePasswordClear != _viewModel.NewTwoPasswordClear)
            {
                _viewModel.ErrorMessage = "Die neuen Passwörter stimmen nicht über ein.";
            }
            else
            {
                var userClient = _connectionHandler.UserClient;
                var result = await userClient.ChangePasswordAsync(_viewModel.OldPasswordClear, _viewModel.NewOnePasswordClear);
                if (result.Reason == OperationResult.Success)
                {
                    _blocker.Set();
                }
                else
                {
                    _viewModel.ErrorMessage = "Refactor error Message"; //Todo: Refactor message
                }
            }
            _viewModel.IsBusy = false;
        }
    }
}
