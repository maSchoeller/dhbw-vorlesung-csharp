using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {


        private string _errorMessage = string.Empty;
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }


        public string ClearPassword { get; private set; } = string.Empty;
        public string Password
        {
            get => new string(PasswordConverter.ReplaceChar, ClearPassword.Length);
            set
            {
                ClearPassword = PasswordConverter.PasswordChange(ClearPassword, value);
                RaisePropertyChanged();
            }
        }

       

        private string _username = string.Empty;
        public string Username { get => _username; set => SetProperty(ref _username, value); }

        public ICommand LoginCommand { get; set; } = null!;


    }
}
