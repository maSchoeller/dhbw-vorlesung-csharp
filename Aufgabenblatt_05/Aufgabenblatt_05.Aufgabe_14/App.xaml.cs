using Aufgabenblatt_05.Aufgabe_14.Abstracts;
using Aufgabenblatt_05.Aufgabe_14.Controllers;
using Aufgabenblatt_05.Aufgabe_14.Framework;
using Aufgabenblatt_05.Aufgabe_14.Models;
using Aufgabenblatt_05.Aufgabe_14.ViewModels;
using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Aufgabenblatt_05.Aufgabe_14
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new ContainerBuilder();

            container.RegisterType<GenreAddController>().As<IGenreAddController>();
            container.RegisterType<MovieAddController>().As<IMovieAddController>();

            container.RegisterType<MainViewModel>().As<IMainViewModel>();
            container.RegisterType<GenreAddViewModel>().As<IGenreAddViewModel>();
            container.RegisterType<MovieAddViewModel>().As<IMovieAddViewModel>();

            container.RegisterType<MovieControlViewModel>().As<IMovieControlViewModel>();
            container.RegisterType<GenreControlViewModel>().As<IGenreControlViewModel>();

            container.RegisterType<Repository<Movie>>().As<IRepository<Movie>>();
            container.RegisterType<Repository<Genre>>().As<IRepository<Genre>>();

            container.RegisterType<MainWindowController>();
            var services = container.Build();
            services.Resolve<MainWindowController>();
        }
    }
}
