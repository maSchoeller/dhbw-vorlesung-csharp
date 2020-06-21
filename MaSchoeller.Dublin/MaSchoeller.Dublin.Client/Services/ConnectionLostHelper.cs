using MaSchoeller.Extensions.Desktop.Abstracts;
using MaSchoeller.Extensions.Desktop.Mvvm;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaSchoeller.Dublin.Client.Services
{
    public class ConnectionLostHelper
    {
        private readonly INavigationService _navigationService;
        private readonly ILogger<ConnectionLostHelper> _logger;

        public ConnectionLostHelper(INavigationService navigationService, 
                                    ILogger<ConnectionLostHelper> logger)
        {
            _navigationService = navigationService;
            _logger = logger;
        }

        public async Task<bool> InvokeAsync(Func<Task> callback)
        {
            try
            {
                await callback();
            }
            catch (Exception e)
            {
                _logger?.LogWarning(e, "");
                MessageBox.Show(DisplayMessages.DisconnectMessage, DisplayMessages.DisconnectMessageCaption, MessageBoxButton.OK);
                _navigationService.NavigateTo(Navigation.DefaultRoute);
                return false;
            }
            return true;
        }

        public void ShowConnectionLost()
        {
            MessageBox.Show(DisplayMessages.ConnectionLost,"Fehler",MessageBoxButton.OK);
        }
    }
}
