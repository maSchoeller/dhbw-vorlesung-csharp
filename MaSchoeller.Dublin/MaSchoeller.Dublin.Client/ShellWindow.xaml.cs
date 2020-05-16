using MaSchoeller.Dublin.ServerProxy;
using MaSchoeller.Extensions.Desktop.Abstracts;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Windows;

namespace MaSchoeller.Dublin.Client
{
    public partial class ShellWindow : Window, IDesktopShell
    {
        public ShellWindow()
        {
            InitializeComponent();
            var fleetService = new FleetServiceClient();
            fleetService.ClientCredentials.UserName.UserName = "Marvin";
            fleetService.ClientCredentials.UserName.Password = "admin";
            fleetService.Open();
            TestLabel.Content = fleetService.GetTestData();
        }
    }
}
