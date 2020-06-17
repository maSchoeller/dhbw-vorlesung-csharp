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

        private ObservableCollection<DisplayBusinessUnit> _buisnessUnits
            = new ObservableCollection<DisplayBusinessUnit>();
        public ObservableCollection<DisplayBusinessUnit> BuisnessUnits
        {
            get => _buisnessUnits;
            set => SetProperty(ref _buisnessUnits, value);
        }

        private DisplayBusinessUnit? _selectedBuisnessUit;
        public DisplayBusinessUnit? SelectedBuisnessUnit
        {
            get => _selectedBuisnessUit;
            set => SetProperty(ref _selectedBuisnessUit, value);
        }

        public ICommand NewCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;
        public ICommand SaveCommand { get; set; } = null!;
    }
}
