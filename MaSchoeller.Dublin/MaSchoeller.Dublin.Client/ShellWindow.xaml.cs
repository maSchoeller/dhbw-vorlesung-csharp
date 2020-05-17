using MaSchoeller.Extensions.Desktop.Abstracts;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Windows;

namespace MaSchoeller.Dublin.Client
{
    public partial class ShellWindow : Window, IDesktopShell
    {
        public ShellWindow(NavigationFrame shellhost, ShellViewModel viewModel)
        {
            InitializeComponent();
            Container.Child = shellhost;
            DataContext = viewModel;
        }
    }
}
