using Aufgabenblatt_05.Aufgabe_14.Abstracts;
using Aufgabenblatt_05.Aufgabe_14.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Aufgabenblatt_05.Aufgabe_14.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {

        public MainViewModel()
        {

        }

        private IViewModelBase? _activeViewModel;
        public IViewModelBase? ActiveViewModel
        {
            get => _activeViewModel;
            set => SetProperty(ref _activeViewModel, value);
        }

        public ICommand ControlChangeCommand { get; set; } = null!;
        public ICommand AddCommand { get; set; } = null!;
        public ICommand RemoveCommand { get; set; } = null!;
    }
}
