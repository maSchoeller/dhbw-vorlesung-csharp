using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_05.Aufgabe_14.Framework
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string? name = null)
        {
            if (!Equals(storage, value))
            {
                storage = value;
                RaisePropertyChanged(name);
            }
        }
    }
}
