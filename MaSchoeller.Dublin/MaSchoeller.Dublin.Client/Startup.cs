using Autofac;
using MaSchoeller.Dublin.Client.Controllers;
using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Proxies.Users;
using MaSchoeller.Dublin.Client.Services;
using MaSchoeller.Dublin.Client.ViewModels;
using MaSchoeller.Dublin.Client.Views;
using MaSchoeller.Extensions.Desktop.Abstracts;
using MaSchoeller.Extensions.Desktop.Mvvm;
using Microsoft.Extensions.DependencyInjection;
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
            services.RegisterType<ShellViewModel>();
            services.RegisterType<ClientConnectionHandler>().SingleInstance();
            RegisterViewModels(services);
            RegisterDialogs(services);
        }

        private void RegisterDialogs(ContainerBuilder services)
        {
            services.RegisterType<ChangePasswordController>();
            services.RegisterType<ChangePasswordViewModel>();
        }

        public void ConfigureNavigation(INavigationServiceBuilder router)
        {
            router.AddRoute<LoginPage, LoginController>(Navigation.DefaultRoute);
            router.AddRoute<PortalPage, PortalController>(DublinNavigations.PortalRoute);
            router.AddRoute<AdminPage, AdminController>(DublinNavigations.AdminRoute);
            router.AddRoute<CalculationMonthPage, CalculationMonthController>(DublinNavigations.CalculationMonthRoute);
            router.AddRoute<CalculationUnitPage, CalculationUnitController>(DublinNavigations.CalculationUnitRoute);
            router.AddRoute<ConfigVehiclePage, ConfigVehicleController>(DublinNavigations.ConfigVehicleRoute);
            router.AddRoute<ConfigEmployeePage, ConfigEmployeeController>(DublinNavigations.ConfigEmployeeRoute);
            router.AddRoute<ConfigBuisnessUnitPage, ConfigBuisnessUnitController>(DublinNavigations.ConfigBuisnessUnitRoute);
        }

        private void RegisterViewModels(ContainerBuilder services)
        {
            services.RegisterType<PortalViewModel>();
            services.RegisterType<AdminViewModel>();
            services.RegisterType<LoginViewModel>();
            services.RegisterType<CalculationMonthViewModel>();
            services.RegisterType<CalculationUnitViewModel>();
            services.RegisterType<ConfigVehicleViewModel>();
            services.RegisterType<ConfigEmployeeViewModel>();
            services.RegisterType<ConfigBuisnessUnitViewModel>();
        }
    }
}