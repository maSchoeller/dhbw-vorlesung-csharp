using Aufgabenblatt_05.Aufgabe_14.Framework;
using Aufgabenblatt_05.Aufgabe_14.Models;
using System.Collections.ObjectModel;

namespace Aufgabenblatt_05.Aufgabe_14.Abstracts
{
    public interface IGenreControlViewModel : IViewModelBase
    {
        ObservableCollection<Genre> Genres { get; set; }
        Genre? SelectedGenre { get; set; }
    }
}