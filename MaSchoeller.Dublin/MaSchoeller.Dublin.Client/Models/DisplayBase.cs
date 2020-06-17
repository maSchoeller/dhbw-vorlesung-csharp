using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Models
{
    public class DisplayBase : NotifyPropertyChangedBase
    {
        private EditState _editstate;

        public EditState EditState
        {
            get => _editstate;
            set => SetProperty(ref _editstate, value);
        }

        private string? _errorMessage;

        public string? ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
    }
}