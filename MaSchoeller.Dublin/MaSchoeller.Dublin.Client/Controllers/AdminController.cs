using FluentNHibernate.Utils;
using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Dublin.Client.Proxies.Users;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Abstracts;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly AdminViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly INavigationService _navigationService;

        public AdminController(AdminViewModel viewModel,
                               ClientConnectionHandler connectionHandler,
                               INavigationService navigationService)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _navigationService = navigationService;
        }


        public override object Initialize()
        {
            _viewModel.NewCommand = ConfigurableCommand.Create(ExecuteNewCommand).Build();


            _viewModel.SaveCommand = ConfigurableCommand.Create(ExecuteSaveCommand, o => _viewModel.Users.Any(u => u.EditState == EditState.Modified))
                                                        .ObserveCommandManager()
                                                        .Build();

            _viewModel.DeleteCommand = ConfigurableCommand.Create(ExecuteDeleteCommand, o => !(_viewModel.SelectedUser is null))
                                                       .Observe(_viewModel, () => _viewModel.SelectedUser)
                                                       .Build();

            return _viewModel;
        }



        private void ExecuteNewCommand(object o)
        {
            _viewModel.SelectedUser = new UserNotifiyable();
            _viewModel.Users.Add(_viewModel.SelectedUser);
        }



        private async void ExecuteDeleteCommand(object o)
        {
            if (!(_viewModel.SelectedUser is null))
            {
                var userClient = _connectionHandler.UserClient;
                try
                {
                    var result = await userClient.DeleteUserAsync(_viewModel.SelectedUser!.AsUser());
                    if (result.Reason == OperationResult.Success)
                    {
                        _viewModel.Users.Remove(_viewModel.SelectedUser);
                        _viewModel.SelectedUser = null;
                    }
                    else
                    {
                        _viewModel.SelectedUser!.ErrorMessage = DisplayMesages.UserCantDelete;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(DisplayMesages.DisconnectMessage, DisplayMesages.DisconnectMessageCaption, MessageBoxButton.OK);
                    _navigationService.NavigateTo(Navigation.DefaultRoute);
                }
            }
        }

        private async void ExecuteSaveCommand(object o)
        {
            var client = _connectionHandler.UserClient;
            foreach (var user in _viewModel.Users)
            {
                if (user.EditState == EditState.Modified)
                {
                    try
                    {
                        var result = await client.SaveOrUpdateUserAsync(user.AsUser());
                        switch (result.Reason)
                        {
                            case OperationResult.Success:
                            {
                                user.EditState = EditState.None;
                                user.Version = result.User.Version;
                            }
                            break;
                            case OperationResult.AlreadyExists:
                            {
                                user.EditState = EditState.InValid;
                                user.ErrorMessage = DisplayMesages.UserIdAlreadyExists;

                            }
                            break;
                            case OperationResult.SaveConflict:
                            {
                                user.EditState = EditState.InValid;
                                user.ErrorMessage = DisplayMesages.ConcurrentServerException;

                            }
                            break;
                            default:
                            {
                                user.EditState = EditState.InValid;
                                user.ErrorMessage = DisplayMesages.ErrorOnSave;

                            }
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(DisplayMesages.DisconnectMessage, DisplayMesages.DisconnectMessageCaption, MessageBoxButton.OK);
                        _navigationService.NavigateTo(Navigation.DefaultRoute);
                    }
                }
            }
        }

        public override async Task EnterAsync()
        {
            var userClient = _connectionHandler.UserClient;
            try
            {
                var result = await userClient.GetAllUsersAsync();
                _viewModel.Users = new ObservableCollection<UserNotifiyable>(result.Select(u => new UserNotifiyable(u)));
            }
            catch (Exception e)
            {
                MessageBox.Show(DisplayMesages.DisconnectMessage, DisplayMesages.DisconnectMessageCaption, MessageBoxButton.OK);
                _navigationService.NavigateTo(Navigation.DefaultRoute);
            }
        }

    }
}
