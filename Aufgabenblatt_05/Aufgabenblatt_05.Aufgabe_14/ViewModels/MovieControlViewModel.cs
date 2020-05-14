using Aufgabenblatt_05.Aufgabe_14.Abstracts;
using Aufgabenblatt_05.Aufgabe_14.Framework;
using Aufgabenblatt_05.Aufgabe_14.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_05.Aufgabe_14.ViewModels
{
    public class MovieControlViewModel : ViewModelBase, IMovieControlViewModel
    {
        public MovieControlViewModel()
        {

        }

        private ObservableCollection<Movie> _movies;
        public ObservableCollection<Movie> Movies { get => _movies; set => SetProperty(ref _movies, value); }
        private Movie? _selectedMovie;
        public Movie? SelectedMovie { get => _selectedMovie; set => SetProperty(ref _selectedMovie, value); }
    }
}
