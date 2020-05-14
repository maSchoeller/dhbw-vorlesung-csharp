using Aufgabenblatt_05.Aufgabe_14.Abstracts;
using Aufgabenblatt_05.Aufgabe_14.Framework;
using Aufgabenblatt_05.Aufgabe_14.Models;
using Aufgabenblatt_05.Aufgabe_14.ViewModels;
using Aufgabenblatt_05.Aufgabe_14.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabenblatt_05.Aufgabe_14.Controllers
{
    public class MovieAddController : IMovieAddController
    {
        private readonly IRepository<Movie> _repositoryMovies;
        private readonly IRepository<Genre> _repositoryGenre;

        public MovieAddController(IRepository<Movie> repositoryMovies, IRepository<Genre> repositoryGenre)
        {
            _repositoryMovies = repositoryMovies;
            _repositoryGenre = repositoryGenre;
        }

        public Movie? AddNewMovie()
        {
            var model = new Movie();
            var window = new MovieAddWindow();
            var vm = new MovieAddViewModel(model, _repositoryGenre.GetAll());
            vm.OkCommand = new RelayCommand(o => window.DialogResult = true, o => vm.SelectedGenre != null);
            vm.CancelCommand = new RelayCommand(o => window.DialogResult = false);
            window.DataContext = vm;
            var result = window.ShowDialog();

            if (result.HasValue && result.Value)
            {
                _repositoryMovies.Save(model);
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
