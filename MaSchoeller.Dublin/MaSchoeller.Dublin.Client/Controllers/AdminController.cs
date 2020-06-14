using FluentNHibernate.Utils;
using MaSchoeller.Dublin.Client.Proxies.Users;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
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
using System.Windows.Media;

namespace MaSchoeller.Dublin.Client.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly AdminViewModel _viewModel;
        private readonly ClientConnectionHandler _connectionHandler;

        public AdminController(AdminViewModel viewModel, ClientConnectionHandler connectionHandler)
        {
            _viewModel = viewModel;
            _connectionHandler = connectionHandler;
        }
        public override object Initialize()
        {
            _viewModel.NewCommand = ConfigurableCommand.Create(ExecuteNewCommand, o => !_viewModel.InEdit)
                                                        .Observe(_viewModel, () => _viewModel.InEdit)
                                                        .Build();

            _viewModel.EditCommand = ConfigurableCommand.Create(ExecuteEditCommand, o => !_viewModel.InEdit && !(_viewModel.SelectedUser is null))
                                                       .Observe(_viewModel, () => _viewModel.InEdit)
                                                       .Observe(_viewModel, () => _viewModel.SelectedUser)
                                                       .Build();

            _viewModel.SaveCommand = ConfigurableCommand.Create(ExecuteSaveCommand, CanExecuteSaveCommand)
                                                        .Observe(_viewModel, () => _viewModel.InEdit)
                                                        .Observe(_viewModel, () => _viewModel.Username)
                                                        .Observe(_viewModel, () => _viewModel.Lastname)
                                                        .Observe(_viewModel, () => _viewModel.Firstname)
                                                        .Build();

            _viewModel.DeleteCommand = ConfigurableCommand.Create(ExecuteDeleteCommand, o => _viewModel.InEdit)
                                                       .Observe(_viewModel, () => _viewModel.InEdit)
                                                       .Build();

            return _viewModel;
        }



        private async void ExecuteNewCommand(object o)
        {
            _viewModel.InEdit = true;
        }

        private void ExecuteEditCommand(object o)
        {
            _viewModel.Username = _viewModel.SelectedUser!.Username;
            _viewModel.Firstname = _viewModel.SelectedUser.Firstname;
            _viewModel.Lastname = _viewModel.SelectedUser.Lastname;
            _viewModel.IsAdmin = _viewModel.SelectedUser.IsAdmin;
            _viewModel.InEdit = true;
        }

        private async void ExecuteDeleteCommand(object o)
        {
            if (!(_viewModel.SelectedUser is null))
            {

                var userClient = _connectionHandler.UserClient;
                var result = await userClient.DeleteUserAsync(_viewModel.SelectedUser!);
                if (result.Reason == OperationResult.Success)
                {
                    _viewModel.Users.Remove(_viewModel.SelectedUser);
                    _viewModel.SelectedUser = null;
                    ClearViewModel();
                }
                else
                {
                    //Todo: Error Message
                }
            }
            _viewModel.InEdit = false;
            ClearViewModel();
        }

        private bool CanExecuteSaveCommand(object o)
        {
            return _viewModel.InEdit &&
                   !string.IsNullOrWhiteSpace(_viewModel.Username) &&
                   !string.IsNullOrWhiteSpace(_viewModel.Lastname);
        }

        private async void ExecuteSaveCommand(object o)
        {
            var newUser = new User
            {
                Username = _viewModel.Username,
                Firstname = _viewModel.Firstname,
                Lastname = _viewModel.Lastname,
                IsAdmin = _viewModel.IsAdmin,
                Id = _viewModel.SelectedUser?.Id ?? 0,
                Version = _viewModel.SelectedUser?.Version ?? 0
            };
            

            var userClient = _connectionHandler.UserClient;
            var result = await userClient.SaveOrUpdateUserAsync(newUser);

            if (result.Reason == OperationResult.Success)
            {
                _viewModel.Users = 
                    new ObservableCollection<User>(await userClient.GetAllUsersAsync());
                _viewModel.InEdit = false;
            }
            else
            {
                _viewModel.ErrorMessage = "Something went wrong";
                _viewModel.InEdit = false;
            }

            ClearViewModel();
            _viewModel.SelectedUser = null;
        }
       
        private void ClearViewModel()
        {
            _viewModel.Username = string.Empty;
            _viewModel.Firstname = string.Empty;
            _viewModel.Lastname = string.Empty;
            _viewModel.IsAdmin = false;
        }

        public override async Task EnterAsync()
        {
            var userClient = _connectionHandler.UserClient;
            try
            {
                var result = await userClient.GetAllUsersAsync();
                _viewModel.Users = new ObservableCollection<User>(result);
            }
            catch (Exception e)
            {

            }

        }

    }
}
