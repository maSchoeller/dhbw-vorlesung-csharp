using Aufgabenblatt_05.Aufgabe_14.Abstracts;
using Aufgabenblatt_05.Aufgabe_14.Framework;
using Aufgabenblatt_05.Aufgabe_14.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Aufgabenblatt_05.Aufgabe_14.ViewModels
{
    public class MovieAddViewModel : ViewModelBase, IMovieAddViewModel
    {
        private readonly Movie _movie;

        public MovieAddViewModel(Movie movie, IEnumerable<Genre> genres)
        {
            _movie = movie;
            AllGenres = genres;
        }
        public IEnumerable<Genre> AllGenres { get; }

        public string Name
        {
            get => _movie.Title;
            set
            {
                if (value != _movie.Title)
                {
                    _movie.Title = value;
                    RaisePropertyChanged();
                }
            }
        }
        public Genre SelectedGenre
        {
            get => _movie.Genre;
            set
            {
                if (value != _movie.Genre)
                {
                    _movie.Genre = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand OkCommand { get; set; } = null!;
        public ICommand CancelCommand { get; set; } = null!;

    }
}
