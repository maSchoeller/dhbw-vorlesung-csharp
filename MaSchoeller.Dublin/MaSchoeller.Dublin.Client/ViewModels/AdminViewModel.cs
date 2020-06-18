using MaSchoeller.Dublin.Client.Proxies.Users;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
