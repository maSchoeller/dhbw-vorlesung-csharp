﻿using Autofac;
using MaSchoeller.Dublin.Core.Communications;
using MaSchoeller.Dublin.Core.Configurations;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Hosting;

namespace MaSchoeller.Dublin.Core.Abstracts
{
    public static class ContainerExtensions
    {
        public static ContainerBuilder AddDublin(this ContainerBuilder container, ServerConfiguration configuration)
        {
            container.AddDublinCommen(configuration);
            container.AddCommunications();
            container.AddRepositories();
            return container;
        }

        public static ContainerBuilder AddDublinCommen(this ContainerBuilder container, ServerConfiguration configuration)
        {

            container.RegisterType<JwtHelper>()
                     .As<ISecurityHelper>()
                     .InstancePerLifetimeScope();
            container.RegisterType<ConnectionFactory>()
                     .As<IConnectionFactory>()
                     .SingleInstance();
            container.RegisterInstance(configuration).SingleInstance();

            return container;
        }

        public static ContainerBuilder AddRepositories(this ContainerBuilder container)
        {
            container.RegisterType<ConnectionFactory>()
                     .As<IConnectionFactory>()
                     .SingleInstance();

            container.RegisterType<UserRepository>()
                     .As<IUserRepository>()
                     .InstancePerLifetimeScope();
            container.RegisterType<EmployeeRepository>()
                     .As<IEmployeeRepository>()
                     .InstancePerLifetimeScope();
            container.RegisterType<VehicleRepository>()
                     .As<IVehicleRepository>()
                     .InstancePerLifetimeScope();
            return container;
        }

        public static ContainerBuilder AddCommunications(this ContainerBuilder container)
        {
            container.RegisterType<UserService>().As<IUserService>();
            container.RegisterType<FleetService>().As<IFleetService>();
            container.RegisterType<CalculationService>().As<ICalculationService>();
            container.RegisterType<CommunicationInitializer>().As<IHostedService>();
            return container;
        }
    }
}