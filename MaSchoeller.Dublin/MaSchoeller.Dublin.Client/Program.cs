using MaSchoeller.Extensions.Desktop;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {

            var host = Host.CreateDefaultBuilder(args)
                           .ConfigureDublinLogging()
                           .UseMVVMC()
                           .UseAutoFac()
                           .ConfigureDesktopDefaults<ShellWindow>(builder =>
                           {
                               builder.UseStartup<Startup>();
                           })
                           .Build();

            await host.RunAsync();
        }


        public static IHostBuilder ConfigureDublinLogging(this IHostBuilder host) =>
            host.ConfigureLogging(builder =>
            {
                var path = Path.Combine(Environment.CurrentDirectory, "dublin-.log");
                var logger = new LoggerConfiguration()
                                        .WriteTo.File(path, rollingInterval: RollingInterval.Day)
                                        .CreateLogger();
                builder.AddSerilog(logger);
            });
    }
}
