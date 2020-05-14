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
    public class GenreAddViewModel : ViewModelBase, IGenreAddViewModel
    {
        private readonly Genre _genre;

        public GenreAddViewModel(Genre genre)
        {
            _genre = genre;
        }
        public string Name
        {
            get => _genre.Name;
            set
            {
                if (value != _genre.Name)
                {
                    _genre.Name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand OkCommand { get; set; } = null!;
        public ICommand CancelCommand { get; set; } = null!;
    }
}
