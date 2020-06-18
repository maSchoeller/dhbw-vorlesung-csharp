using Antlr.Runtime.Misc;
using MaSchoeller.Dublin.Client.Proxies.Users;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Dublin.Client.Views;
using MaSchoeller.Extensions.Desktop.Abstracts;
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
    public class ChangePasswordDialogController
    {
        private readonly ChangePasswordViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ConnectionLostHelper _lostHelper;
        private readonly AutoResetEvent _blocker = new AutoResetEvent(false);

        public ChangePasswordDialogController(ChangePasswordViewModel viewModel,
                                              ClientConnectionHandler connectionHandler,
                                              ConnectionLostHelper lostHelper)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _lostHelper = lostHelper;
            _viewModel.ChangeCommand =
                 ConfigurableCommand.Create(PasswordChangeExecute).Build();
        }


        public void ShowDialog()
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.OldPassword = string.Empty;
            _viewModel.NewOnePassword = string.Empty;
            _viewModel.NewTwoPassword = string.Empty;

            var window = new ChangePasswordWindow
            {
                DataContext = _viewModel,
                Owner = Application.Current.MainWindow
            };

            Task.Run(() =>
            {
                _blocker.WaitOne();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (window.DialogResult is null)
                        window.DialogResult = true;
                });
            });

            window.ShowDialog();
        }

        private async void PasswordChangeExecute(object obj)
        {
            _viewModel.IsBusy = true;
            if (_viewModel.OldPasswordClear == _viewModel.NewOnePasswordClear)
            {
                _viewModel.ErrorMessage = DisplayMesages.OldAndNewAreSame;
            }
            else if (_viewModel.NewOnePasswordClear != _viewModel.NewTwoPasswordClear)
            {
                _viewModel.ErrorMessage = DisplayMesages.NewPasswordAreNotSame;
            }
            else
            {
                await _lostHelper.InvokeAsync(async () =>
                {
                    var userClient = _connectionHandler.UserClient;
                    var result = await userClient.ChangePasswordAsync(_viewModel.OldPasswordClear, _viewModel.NewOnePasswordClear);
                    if (result.Reason == OperationResult.Success)
                    {
                        _blocker.Set();
                    }
                    else
                    {
                        _viewModel.ErrorMessage = DisplayMesages.PasswordChangeError; //Todo: Refactor message
                    }
                });
            }
            _viewModel.IsBusy = false;
        }
    }
}
