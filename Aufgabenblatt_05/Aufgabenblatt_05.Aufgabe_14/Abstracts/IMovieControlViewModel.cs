using Aufgabenblatt_05.Aufgabe_14.Framework;
using Aufgabenblatt_05.Aufgabe_14.Models;
using System.Collections.ObjectModel;

namespace Aufgabenblatt_05.Aufgabe_14.Abstracts
{
    public interface IMovieControlViewModel : IViewModelBase
    {
        ObservableCollection<Movie> Movies { get; set; }
        Movie? SelectedMovie { get; set; }
    }
}