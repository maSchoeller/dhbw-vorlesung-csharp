using MaSchoeller.Extensions.Desktop.Abstracts;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
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
               var result = await navigationService.TryNavigateToAsync(name);

            }).Build();

            navigationService.Navigated += (s, e) 
                => IsNavbarVisible = e.Route != Navigation.DefaultRoute;

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

        public ICommand NavigationCommand { get; }
    }
}
