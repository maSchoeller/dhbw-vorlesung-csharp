using MaSchoeller.Dublin.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Proxies.Users
{
    public partial class User
    {
        public string Fullname => Firstname is null ? Lastname : $"{Firstname} {Lastname}";
    }

    public class UserNotifiyable : User, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public UserNotifiyable(User user)
        {
            Id = user.Id;
            Version = user.Version;
            EditState = EditState.None;
            base.Username = user.Username;
            base.Lastname = user.Lastname;
            base.Firstname = user.Firstname;
            base.IsAdmin = user.IsAdmin;
        }

        public UserNotifiyable()
        {

        }

        private void RaisePropertyChanged([CallerMemberName] string name = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public new string Username
        {
            get => base.Username;
            set
            {
                base.Username = value;
                RaisePropertyChanged();
                EditState = EditState.Modified;
            }
        }
        public new string Lastname
        {
            get => base.Lastname;
            set
            {
                base.Lastname = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Fullname));
                EditState = EditState.Modified;
            }
        }
        public new string? Firstname
        {
            get => base.Firstname;
            set
            {
                base.Firstname = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Fullname));
                EditState = EditState.Modified;
            }
        }
        public new bool IsAdmin
        {
            get => base.IsAdmin; set
            {
                base.IsAdmin = value;
                RaisePropertyChanged();
                EditState = EditState.Modified;
            }
        }

        private EditState _editState;

        public EditState EditState
        {
            get { return _editState; }
            set
            {
                _editState = value;
                RaisePropertyChanged();
            }
        }

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged();
            }
        }

        public User AsUser()
        {
            return new User()
            {
                Id = Id,
                Firstname = Firstname,
                Lastname = Lastname,
                IsAdmin = IsAdmin,
                Username = Username,
                Version = Version
            };
        }

    }

}
