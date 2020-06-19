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
        public virtual EditState EditState
        {
            get => _editstate;
            set
            {
                var result = Validate();
                if (result)
                {
                    SetProperty(ref _editstate, value);
                }
                else
                {
                    SetProperty(ref _editstate, EditState.InValid);
                }
            }
        }

        private string? _errorMessage;
        public virtual string? ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool _isSynced = false;
        public bool IsSynced
        {
            get => _isSynced;
            set => SetProperty(ref _isSynced, value);
        }

        public virtual bool Validate()
            => true;
    }
}