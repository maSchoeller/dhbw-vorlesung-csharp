using MaSchoeller.Dublin.Client.Proxies.Users;
using MaSchoeller.Extensions.Desktop.Mvvm;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        private User? _selectedUser;

        public User? SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        private string _username = string.Empty;

        public string Username
        {
            get =>_username;
            set => SetProperty(ref _username, value);
        }

        private string _firstname = string.Empty;

        public string Firstname
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value);
        }

        private string _lastname = string.Empty;

        public string Lastname
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }

        private bool _inEdit;

        public bool InEdit
        {
            get => _inEdit;
            set => SetProperty(ref _inEdit, value);
        }

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }




        public ICommand EditCommand { get; set; } = null!;
        public ICommand NewCommand { get; set; } = null!;
        public ICommand SaveCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;

    }
}
