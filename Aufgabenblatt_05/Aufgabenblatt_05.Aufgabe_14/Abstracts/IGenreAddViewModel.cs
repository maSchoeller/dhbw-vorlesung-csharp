using System.Windows.Input;

namespace Aufgabenblatt_05.Aufgabe_14.Abstracts
{
    public interface IGenreAddViewModel
    {
        ICommand CancelCommand { get; set; }
        string Name { get; set; }
        ICommand OkCommand { get; set; }
    }
}