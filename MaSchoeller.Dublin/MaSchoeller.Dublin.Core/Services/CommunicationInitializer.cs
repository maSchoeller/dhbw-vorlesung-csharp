using Autofac;
using Autofac.Integration.Wcf;
using MaSchoeller.Dublin.Core.Communications;
using MaSchoeller.Dublin.Core.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Threading;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Services
{
    public class CommunicationInitializer : IHostedService
    {
        private readonly ILifetimeScope _container;
        private readonly ServerConfiguration _options;
        private readonly ILogger<CommunicationInitializer>? _logger;
        private ServiceHost? _host;

        public CommunicationInitializer(ILifetimeScope container,
                                        ServerConfiguration options,
                                        ILogger<CommunicationInitializer>? logger)
        {
            _container = container;
            _options = options;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Uri baseAdress = new Uri($"http://{_options.Hostname}:{_options.Port}/fleets");
            //Uri baseAdressMex = new Uri($"http://{_options.Hostname}:{8080}/fleets");
            _host = new ServiceHost(typeof(FleetService), baseAdress);

            //Note: This is totally insecure and should never happen in Production, 
            //      the login credentials will transfered on an unencrypted channel, 
            //      in Production use Protocols they support TLS
            WSHttpBinding httpBinding = new WSHttpBinding();
            //httpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            //httpBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            //httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

            _host.AddServiceEndpoint(typeof(IFleetService), httpBinding, "");
            _host.AddDependencyInjectionBehavior<IFleetService>(_container);
            _host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpsGetEnabled = true });

            try
            {
                _host.Open();
                _logger?.LogInformation(LogMessages.WcfSuccessfullyStarted, baseAdress);
            }
            catch (Exception e) when (e is CommunicationObjectFaultedException ||
                                      e is TimeoutException ||
                                      e is AddressAlreadyInUseException)
            {
                _logger?.LogCritical(LogMessages.WcfPortBindingError, baseAdress);
                throw;
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _host?.Close();
            return Task.CompletedTask;
        }
    }
}
