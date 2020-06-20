using Autofac;
using MaSchoeller.Dublin.Core.Communications;
using MaSchoeller.Dublin.Core.Configurations;
using MaSchoeller.Dublin.Core.Database;
using MaSchoeller.Dublin.Core.Database.Abstracts;
using Microsoft.Extensions.Hosting;

namespace MaSchoeller.Dublin.Core.Services
{
    public static class ContainerExtensions
    {
        public static ContainerBuilder AddDublin(this ContainerBuilder container, ServerConfiguration configuration)
        {
            container.AddDublinCommon(configuration);
            container.AddCommunications();
            container.AddRepositories();
            return container;
        }

        public static ContainerBuilder AddDublinCommon(this ContainerBuilder container, ServerConfiguration configuration)
        {

            container.RegisterType<JwtHelper>()
                     .As<ISecurityHelper>()
                     .InstancePerLifetimeScope();
            container.RegisterInstance(configuration)
                     .SingleInstance();

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
            container.RegisterType<BusinessUnitRepository>()
                    .As<IBusinessUnitRepository>()
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
