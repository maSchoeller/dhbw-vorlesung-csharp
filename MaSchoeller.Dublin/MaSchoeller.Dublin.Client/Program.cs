using MaSchoeller.Extensions.Desktop;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                           .UseMVVMC()
                           .UseAutoFac()
                           .ConfigureDesktopDefaults<ShellWindow>(builder =>
                           {
                               builder.UseStartup<Startup>();
                           })
                           .Build();

            await host.RunAsync();
        }
    }
}
