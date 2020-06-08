﻿using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using MaSchoeller.Dublin.Core;
using MaSchoeller.Dublin.Core.Abstracts;
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
            try
            {
                await Host.CreateDefaultBuilder(args)
                        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                        .ConfigureContainer<ContainerBuilder>((context, services) =>
                        {
                            services.AddDublin(context.Configuration.Get<ServerConfiguration>());
                        })
                        .RunConsoleAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Server stop ungracefully, for more informations take a look in console logs");
            }
           
        }
    }
}
