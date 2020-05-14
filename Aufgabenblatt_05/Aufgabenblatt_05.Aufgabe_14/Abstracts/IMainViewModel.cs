using Aufgabenblatt_05.Aufgabe_14.Framework;
using System.Windows.Input;

namespace Aufgabenblatt_05.Aufgabe_14.Abstracts
{
    public interface IMainViewModel
    {
        IViewModelBase? ActiveViewModel { get; set; }
        ICommand AddCommand { get; set; }
        ICommand ControlChangeCommand { get; set; }
        ICommand RemoveCommand { get; set; }
    }
}