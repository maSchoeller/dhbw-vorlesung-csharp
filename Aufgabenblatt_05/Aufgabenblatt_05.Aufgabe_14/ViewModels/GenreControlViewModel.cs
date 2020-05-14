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
    public class GenreControlViewModel : ViewModelBase, IGenreControlViewModel
    {
        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres { get => _genres; set => SetProperty(ref _genres, value); }

        private Genre? _selectedGenre;
        public Genre? SelectedGenre { get => _selectedGenre; set => SetProperty(ref _selectedGenre, value); }
    }
}
