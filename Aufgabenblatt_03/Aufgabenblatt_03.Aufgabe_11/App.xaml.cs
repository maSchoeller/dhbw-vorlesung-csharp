using Aufgabenblatt_03.Aufgabe_11.Controllers;
using System.Windows;

namespace Aufgabenblatt_03.Aufgabe_11
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var controller = new MainWindowController();
            controller.Initialize();
        }
    }
}
