using Autofac;
using MaSchoeller.Dublin.Client.Controllers;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Dublin.Client.Views;
using MaSchoeller.Extensions.Desktop.Abstracts;
using MaSchoeller.Extensions.Desktop.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Proxies;
using System;
using System.Windows;

namespace MaSchoeller.Dublin.Client
{
    public class Startup
    {
        public void ConfigureApplication(Application app)
        {
            app.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("DublinDefaultStyles.xaml", UriKind.Relative)
            });
        }

        public void ConfigureContainer(ContainerBuilder services)
        {
            services.RegisterType<FleetServiceClient>();
            services.RegisterType<ShellViewModel>();
            services.RegisterType<ClientConnectionHandler>();

            services.RegisterType<PortalViewModel>();
            services.RegisterType<LoginViewModel>();
        }

        public void ConfigureNavigation(INavigationServiceBuilder router)
        {
            router.AddRoute<LoginPage, LoginController>(Navigation.DefaultRoute);
            router.AddRoute<PortalPage, PortalController>("portal");
        }
    }
}