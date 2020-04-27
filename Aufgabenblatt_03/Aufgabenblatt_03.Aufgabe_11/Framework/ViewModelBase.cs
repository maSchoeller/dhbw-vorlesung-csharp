using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aufgabenblatt_03.Aufgabe_11.Framework
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName]string? name = null)
        {
            if (!Equals(storage,value))
            {
                storage = value;
                OnPropertyChanged(name);
            }
        }
    }
}
