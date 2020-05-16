using Autofac;
using Autofac.Extensions.DependencyInjection;
using MaSchoeller.Dublin.Core;
using MaSchoeller.Dublin.Core.Communications;
using MaSchoeller.Dublin.Core.Configurations;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                      .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                      .ConfigureContainer<ContainerBuilder>((context, services) =>
                      {
                          services.RegisterType<FleetService>().As<IFleetService>();
                          services.RegisterType<MyCustomeService>();
                          services.RegisterType<AuthenticationService>();
                          services.RegisterType<CommunicationInitializer>().As<IHostedService>();
                          services.RegisterInstance(context.Configuration.Get<ServerConfiguration>());
                      })
                      .RunConsoleAsync();
        }
    }
}
