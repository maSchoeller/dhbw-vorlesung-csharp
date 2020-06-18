using MaSchoeller.Dublin.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Proxies.Users
{
    public class DisplayUser : DisplayBase
    {
        public DisplayUser(User user)
        {
            Id = user.Id;
            Version = user.Version;
            EditState = EditState.None;
            _username = user.Username;
            _lastname = user.Lastname;
            _firstname = user.Firstname;
            _username = user.Username;
            _isAdmin = user.IsAdmin;
        }

        public DisplayUser()
        {

        }

        public int Id { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                EditState = EditState.Modified;
            }
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                SetProperty(ref _lastname, value);
                RaisePropertyChanged(nameof(Fullname));
                EditState = EditState.Modified;
            }
        }

        private string? _firstname;
        public string? Firstname
        {
            get => _firstname;
            set
            {
                SetProperty(ref _firstname, value);
                RaisePropertyChanged(nameof(Fullname));
                EditState = EditState.Modified;
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                SetProperty(ref _isAdmin, value);
                EditState = EditState.Modified;
            }
        }


        public int Version { get; set; }
        public string Fullname => Firstname is null ? Lastname : $"{Firstname} {Lastname}";

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
