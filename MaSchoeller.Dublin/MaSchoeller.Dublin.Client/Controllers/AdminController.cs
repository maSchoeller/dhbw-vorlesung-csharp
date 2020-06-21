using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Dublin.Client.Proxies.Users;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Extensions.Desktop.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly AdminViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;
        private readonly ConnectionLostHelper _lostHelper;
        private readonly ILogger<AdminController>? _logger;

        public AdminController(AdminViewModel viewModel,
                               ClientConnectionHandler connectionHandler,
                               ConnectionLostHelper lostHelper,
                               ILogger<AdminController>? logger = null)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
            _lostHelper = lostHelper;
            _logger = logger;
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
            _viewModel.SelectedUser = new DisplayUser();
            _viewModel.Users.Add(_viewModel.SelectedUser);
        }



        private async void ExecuteDeleteCommand(object o)
        {
            _viewModel.IsBusy = true;
            if (!(_viewModel.SelectedUser is null))
            {
                var userClient = _connectionHandler.UserClient;
                await _lostHelper.InvokeAsync(async () =>
                {
                    var result = await userClient.DeleteUserAsync(_viewModel.SelectedUser!.AsUser());
                    if (result.Reason == OperationResult.Success)
                    {
                        _viewModel.Users.Remove(_viewModel.SelectedUser);
                        _viewModel.SelectedUser = null;
                    }
                    else
                    {
                        _viewModel.SelectedUser!.ErrorMessage = DisplayMessages.UserCantDelete;
                    }
                });
            }
            _viewModel.IsBusy = false;
        }

        private async void ExecuteSaveCommand(object o)
        {
            _viewModel.IsBusy = true;

            var client = _connectionHandler.UserClient;
            foreach (var user in _viewModel.Users)
            {
                if (user.EditState == EditState.Modified)
                {
                    await _lostHelper.InvokeAsync(async () =>
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
                                user.ErrorMessage = DisplayMessages.UserIdAlreadyExists;

                            }
                            break;
                            case OperationResult.SaveConflict:
                            {
                                user.EditState = EditState.InValid;
                                user.ErrorMessage = DisplayMessages.ConcurrentServerException;

                            }
                            break;
                            default:
                            {
                                user.EditState = EditState.InValid;
                                user.ErrorMessage = DisplayMessages.ErrorOnSave;

                            }
                            break;
                        }
                    });
                }
            }
            _viewModel.IsBusy = false;
        }

        public override async Task EnterAsync()
        {
            _viewModel.IsBusy = true;
            _viewModel.SelectedUser = null;
            _viewModel.Users = new ObservableCollection<DisplayUser>();
            try
            {
                var userClient = _connectionHandler.UserClient;
                var result = await userClient.GetAllUsersAsync();
                _viewModel.Users = new ObservableCollection<DisplayUser>(result.Select(u => new DisplayUser(u)));
            }
            catch (Exception e)
            {
                _logger?.LogWarning(e, "");
                _lostHelper.ShowConnectionLost();
            }
            _viewModel.IsBusy = false;

        }
    }
}
