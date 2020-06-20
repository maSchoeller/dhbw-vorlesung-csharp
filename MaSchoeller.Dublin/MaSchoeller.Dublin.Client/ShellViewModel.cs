using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Extensions.Desktop.Abstracts;
using MaSchoeller.Extensions.Desktop.Mvvm;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaSchoeller.Dublin.Client
{
    public class ShellViewModel : NotifyPropertyChangedBase
    {

        public ShellViewModel(INavigationService navigationService,
                              ClientConnectionHandler connectionHandler,
                              ILogger<ShellViewModel>? logger = null)
        {
            NavigationCommand = ConfigurableCommand.Create(
            async o =>
            {
                var name = o as string;
                var result = await navigationService.TryNavigateToAsync(name!);
            },
            o =>
            {
                var name = o as string;
                return name!.ToUpperInvariant() != Route;
            }
            ).Observe(this, () => Route).Build();

            navigationService.Navigated += (s, e) =>
            {
                IsNavbarVisible = e.Route != Navigation.DefaultRoute;
                Route = e.Route;
            };

            navigationService.NavigationFailed += (s, e) =>
            {
                logger?.LogWarning("Failed navigating to Page {0}", e.Route);
            };
        }


        private bool _isNavbarVisible = false;
        public bool IsNavbarVisible { get => _isNavbarVisible; set => SetProperty(ref _isNavbarVisible, value); }

        private string _route = string.Empty;

        public string Route { get => _route; set => SetProperty(ref _route, value); }

        public ICommand NavigationCommand { get; }
    }
}
