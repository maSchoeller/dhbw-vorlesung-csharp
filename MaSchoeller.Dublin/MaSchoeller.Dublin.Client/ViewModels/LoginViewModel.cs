using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class LoginViewModel : NotifyPropertyChangedBase
    {
        private string _errorMessage = string.Empty;
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage,value); }


        private string _username = string.Empty;
        public string Username { get => _username; set => SetProperty(ref _username, value); }

        public ICommand LoginCommand { get; set; } = null!;
    }
}
