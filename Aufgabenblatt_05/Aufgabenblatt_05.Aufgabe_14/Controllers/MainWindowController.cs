using Aufgabenblatt_05.Aufgabe_14.Abstracts;
using Aufgabenblatt_05.Aufgabe_14.Framework;
using Aufgabenblatt_05.Aufgabe_14.Models;
using Aufgabenblatt_05.Aufgabe_14.ViewModels;
using Aufgabenblatt_05.Aufgabe_14.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Interop;
using System.Windows.Navigation;

namespace Aufgabenblatt_05.Aufgabe_14.Controllers
{
    public class MainWindowController
    {
        private readonly MainWindow _window;
        private readonly IMainViewModel _mainViewModel;
        private readonly IMovieControlViewModel _movieViewModel;
        private readonly IGenreControlViewModel _genreViewModel;
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Genre> _genreRepository;

        public MainWindowController(IMainViewModel mainViewModel,
                                    IMovieControlViewModel movieViewModel,
                                    IGenreControlViewModel genreViewModel,
                                    IRepository<Movie> movieRepository,
                                    IRepository<Genre> genreRepository)
        {
            _window = new MainWindow();
            _mainViewModel = mainViewModel;
            _mainViewModel.ControlChangeCommand = new RelayCommand(ChangeWindow, CanChangeWindow);
            _mainViewModel.AddCommand = new RelayCommand(AddItem);
            _mainViewModel.RemoveCommand = new RelayCommand(RemoveItem, CanRemoveItem);
            _movieViewModel = movieViewModel;
            _genreViewModel = genreViewModel;
            _window.DataContext = _mainViewModel;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            LoadAllGenres();
            LoadAllMovies();
            _mainViewModel.ActiveViewModel = _movieViewModel;
            SeedData();
            _window.Show();
        }



        private bool CanRemoveItem(object obj)
        {
            return _mainViewModel.ActiveViewModel switch
            {
                IGenreControlViewModel genreViewModel => genreViewModel.SelectedGenre != null,
                IMovieControlViewModel movieViewModel => movieViewModel.SelectedMovie != null,
                _ => throw new NotSupportedException("Only 'movie' and 'genre' are supported.")
            };
        }

        private void RemoveItem(object obj)
        {
            switch (_mainViewModel.ActiveViewModel)
            {
                case IGenreControlViewModel _:
                {
                    try
                    {
                        _genreRepository.Delete(_genreViewModel.SelectedGenre!);
                        LoadAllGenres();
                    }
                    catch
                    {
                        MessageBox.Show("Das Genre kann erst gelöscht werden, wenn kein Film mehr das Genre besitzt.", "Genre löschen", MessageBoxButton.OK);
                    }
                }
                break;
                case IMovieControlViewModel _:
                {

                    _movieRepository.Delete(_movieViewModel.SelectedMovie!);
                    LoadAllMovies();

                }
                break;
                default:
                    break;
            }
        }

        private void AddItem(object obj)
        {
            switch (_mainViewModel.ActiveViewModel)
            {
                case IGenreControlViewModel _:
                {
                    var genreAddController = new GenreAddController(_genreRepository);
                    var result = genreAddController.AddNewGenre();
                    if (!(result is null))
                        LoadAllGenres();
                }
                break;
                case IMovieControlViewModel _:
                {
                    var movieAddController = new MovieAddController(_movieRepository, _genreRepository);
                    var result = movieAddController.AddNewMovie();
                    if (!(result is null))
                        LoadAllMovies();
                }
                break;
            }
        }

        private bool CanChangeWindow(object obj)
        {
            return obj is WindowType name && ((_mainViewModel.ActiveViewModel is IMovieControlViewModel && name == WindowType.Genre)
                                         || (_mainViewModel.ActiveViewModel is IGenreControlViewModel && name == WindowType.Movie));
        }
        private void ChangeWindow(object obj)
        {

            switch (obj)
            {
                case WindowType name when name == WindowType.Movie:
                {
                    _mainViewModel.ActiveViewModel = _movieViewModel;
                    LoadAllMovies();
                }
                break;
                case WindowType name when name == WindowType.Genre:
                {
                    _mainViewModel.ActiveViewModel = _genreViewModel;
                    LoadAllGenres();
                }
                break;
                default:
                    throw new ArgumentException("Only 'movie' and 'genre' are supported.");
            }
        }

        private void LoadAllMovies()
            => _movieViewModel.Movies = new ObservableCollection<Movie>(_movieRepository.GetAll());

        private void LoadAllGenres()
            => _genreViewModel.Genres = new ObservableCollection<Genre>(_genreRepository.GetAll());


        public void SeedData()
        {
            var defaultGenre = "Thriller";
            if (!_genreRepository.GetAll().Any(g => g.Name == defaultGenre))
            {
                var genres = new List<Genre>
                {
                    new Genre
                    {
                        Name = defaultGenre
                    },
                    new Genre
                    {
                        Name = "Comedy"
                    },
                    new Genre
                    {
                        Name = "Action"
                    }
                };
                genres.ForEach(g => _genreRepository.Save(g));
                var movies = new List<Movie>
                {
                    new Movie
                    {
                        Title = "Bad Boys for Life",
                        Genre = genres[2]
                    },
                    new Movie
                    {
                        Title = "Sonic the Hedgehog",
                        Genre = genres[2]
                    },
                   new Movie
                   {
                       Title = "Birds of Prey: The Emancipation of Harley Quinn",
                       Genre = genres[0]
                   },
                   new Movie
                   {
                       Title = "Avengers: Endgame",
                       Genre = genres[2]
                   },
                   new Movie
                   {
                       Title = "Trolls World Tour",
                       Genre = genres[1]
                   }
                };

                movies.ForEach(m => _movieRepository.Save(m));
            }
        }
    }
}
