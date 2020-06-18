using MaSchoeller.Dublin.Client.Models;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class AddLeasingViewModel : ViewModelBase
    {
        public event EventHandler? TabChanged;

        private int _selectedTab = 1;
        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                if (_selectedTab != value)
                {
                    _selectedTab = value;
                    RaisePropertyChanged();
                    TabChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public DisplayVehicle Vehicle { get; set; }
    }
}
