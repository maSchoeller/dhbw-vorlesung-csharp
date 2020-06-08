using MaSchoeller.Extensions.Desktop.Abstracts;
using MaSchoeller.Extensions.Desktop.Mvvm;
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

        public ShellViewModel(INavigationService navigationService)
        {
            NavigationCommand = ConfigurableCommand.Create(
            async o =>
            {
                var name = o as string;
                var result = await navigationService.TryNavigateToAsync(name!);

            },
            o => {
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

            };
        }

        private bool _isNavbarVisible = false;
        public bool IsNavbarVisible
        {
            get => _isNavbarVisible;
            set => SetProperty(ref _isNavbarVisible, value);
        }

        private string _route = string.Empty;
        public string Route { get => _route; set => SetProperty(ref _route, value); }

        public ICommand NavigationCommand { get; }
    }
}
