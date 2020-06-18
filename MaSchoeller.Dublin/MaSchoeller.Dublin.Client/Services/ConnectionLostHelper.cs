using MaSchoeller.Extensions.Desktop.Abstracts;
using MaSchoeller.Extensions.Desktop.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaSchoeller.Dublin.Client.Services
{
    public class ConnectionLostHelper
    {
        private readonly INavigationService _navigationService;

        public ConnectionLostHelper(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task<bool> InvokeAsync(Func<Task> callback)
        {
            try
            {
                await callback();
            }
            catch (Exception e)
            {
                MessageBox.Show(DisplayMessages.DisconnectMessage, DisplayMessages.DisconnectMessageCaption, MessageBoxButton.OK);
                _navigationService.NavigateTo(Navigation.DefaultRoute);
                return false;
            }
            return true;
        }
    }
}
