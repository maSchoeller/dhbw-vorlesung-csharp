using Aufgabenblatt_05.Aufgabe_14.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace Aufgabenblatt_05.Aufgabe_14.Abstracts
{
    public interface IMovieAddViewModel
    {
        IEnumerable<Genre> AllGenres { get; }
        ICommand CancelCommand { get; set; }
        string Name { get; set; }
        ICommand OkCommand { get; set; }
        Genre SelectedGenre { get; set; }
    }
}