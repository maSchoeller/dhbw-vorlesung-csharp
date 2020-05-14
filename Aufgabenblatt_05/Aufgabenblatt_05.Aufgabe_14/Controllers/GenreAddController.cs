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
    public class GenreAddController : IGenreAddController
    {
        private readonly IRepository<Genre> _repository;
        public GenreAddController(IRepository<Genre> repository)
        {
            _repository = repository;
        }

        public Genre? AddNewGenre()
        {
            var model = new Genre();
            var window = new GenreAddWindow();
            var vm = new GenreAddViewModel(model)
            {
                OkCommand = new RelayCommand(o => window.DialogResult = true),
                CancelCommand = new RelayCommand(o => window.DialogResult = false)
            };
            window.DataContext = vm;
            var result = window.ShowDialog();

            if (result.HasValue && result.Value)
            {
                _repository.Save(model);
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
