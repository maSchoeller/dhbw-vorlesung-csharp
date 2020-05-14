using Aufgabenblatt_05.Aufgabe_13.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Augabenblatt_05.Aufgabe_13
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
