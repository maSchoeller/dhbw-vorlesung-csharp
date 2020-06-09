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
    public class ChangePasswordViewModel : ViewModelBase
    {
        public ChangePasswordViewModel()
        {

        }

        public string OldPasswordClear { get; private set; } = string.Empty;
        public string OldPassword
        {
            get => new string(PasswordConverter.ReplaceChar, OldPasswordClear.Length);
            set
            {
                OldPasswordClear = PasswordConverter.PasswordChange(OldPasswordClear, value);
                RaisePropertyChanged();
            }
        }

        public string NewOnePasswordClear { get; private set; } = string.Empty;
        public string NewOnePassword
        {
            get => new string(PasswordConverter.ReplaceChar, NewOnePasswordClear.Length);
            set
            {
                NewOnePasswordClear = PasswordConverter.PasswordChange(NewOnePasswordClear, value);
                RaisePropertyChanged();
            }
        }

        public string NewTwoPasswordClear { get; private set; } = string.Empty;
        public string NewTwoPassword
        {
            get => new string(PasswordConverter.ReplaceChar, NewTwoPasswordClear.Length);
            set
            {
                NewTwoPasswordClear = PasswordConverter.PasswordChange(NewTwoPasswordClear, value);
                RaisePropertyChanged();
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand ChangeCommand { get; set; }
    }
}
