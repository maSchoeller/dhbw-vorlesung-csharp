using System.Collections.ObjectModel;
using MaSchoeller.Dublin.Client.Models;
using System.Windows.Input;

namespace MaSchoeller.Dublin.Client.ViewModels
{
    public class ConfigBuisnessUnitViewModel : ViewModelBase
    {

        public ConfigBuisnessUnitViewModel()
        {

        }

        private ObservableCollection<DisplayBuisnessUnit> _buisnessUnits
            = new ObservableCollection<DisplayBuisnessUnit>();
        public ObservableCollection<DisplayBuisnessUnit> BuisnessUnits
        {
            get => _buisnessUnits;
            set => SetProperty(ref _buisnessUnits, value);
        }

        private DisplayBuisnessUnit _selectedBuisnessUit;
        public DisplayBuisnessUnit SelectedBuisnessUnit
        {
            get => _selectedBuisnessUit;
            set => SetProperty(ref _selectedBuisnessUit, value);
        }

        public ICommand NewCommand { get; set; } = null!;
        public ICommand EditCommand { get; set; } = null!;
        public ICommand SaveCommand { get; set; } = null!;
    }
}
