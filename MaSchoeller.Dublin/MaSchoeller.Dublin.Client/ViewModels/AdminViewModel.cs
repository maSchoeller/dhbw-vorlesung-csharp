using MaSchoeller.Dublin.Client.Helpers;
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
        private ObservableCollection<DisplayUser> _users = new ObservableCollection<DisplayUser>();
        public ObservableCollection<DisplayUser> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        private DisplayUser? _selectedUser;

        public DisplayUser? SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
            }
        }


        public ICommand NewCommand { get; set; } = null!;
        public ICommand SaveCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;

    }
}
