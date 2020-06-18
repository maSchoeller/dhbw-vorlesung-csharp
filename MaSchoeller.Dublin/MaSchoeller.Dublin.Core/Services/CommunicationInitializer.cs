using Autofac;
using Autofac.Integration.Wcf;
using MaSchoeller.Dublin.Core.Communications;
using MaSchoeller.Dublin.Core.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Services
{
    internal class CommunicationInitializer : IHostedService
    {
        private readonly ILifetimeScope _container;
        private readonly ServerConfiguration _options;
        private readonly ILogger<CommunicationInitializer>? _logger;
        private readonly List<ServiceHost> _hosts;

        public CommunicationInitializer(ILifetimeScope container,
                                        ServerConfiguration options,
                                        ILogger<CommunicationInitializer>? logger)
        {
            _hosts = new List<ServiceHost>();
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var baseUrl = $"http://{_options.Hostname}:{_options.Port}";
            BootStrapUserService(baseUrl);
            BootStrapFleetService(baseUrl);
            BootStrapCalculationService(baseUrl);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _hosts.ForEach(h => h.Close());
            return Task.CompletedTask;
        }


        private void BootStrapUserService(string baseUrl)
        {
            Uri baseAdress = new Uri(baseUrl + "/users");
            var host = new ServiceHost(typeof(UserService), baseAdress);

            //Note: This is totally insecure and should never happen in production, 
            //      the login credentials will transfered on an unencrypted channel, 
            //      in production use Protocols that support TLS.
            WSHttpBinding httpBinding = new WSHttpBinding();
            host.AddServiceEndpoint(typeof(IUserService), httpBinding, "");
            host.AddDependencyInjectionBehavior<IUserService>(_container);
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpsGetEnabled = true });
            try
            {
                host.Open();
                _logger?.LogInformation(LogMessages.WcfSuccessfullyStarted, baseAdress);
                _hosts.Add(host);
            }
            catch (Exception e) when (e is CommunicationObjectFaultedException ||
                                      e is TimeoutException ||
                                      e is AddressAlreadyInUseException)
            {
                _logger?.LogCritical(LogMessages.WcfPortBindingError, baseAdress);
                throw;
            }
        }

        private void BootStrapCalculationService(string baseUrl)
        {
            Uri baseAdress = new Uri(baseUrl + "/calculations");
            var host = new ServiceHost(typeof(CalculationService), baseAdress);

            //Note: This is totally insecure and should never happen in Production, 
            //      the login credentials will transfered on an unencrypted channel, 
            //      in Production use Protocols they support TLS
            WSHttpBinding httpBinding = new WSHttpBinding();
            host.AddServiceEndpoint(typeof(ICalculationService), httpBinding, "");
            host.AddDependencyInjectionBehavior<ICalculationService>(_container);
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpsGetEnabled = true });
            try
            {
                host.Open();
                _logger?.LogInformation(LogMessages.WcfSuccessfullyStarted, baseAdress);
                _hosts.Add(host);
            }
            catch (Exception e) when (e is CommunicationObjectFaultedException ||
                                      e is TimeoutException ||
                                      e is AddressAlreadyInUseException)
            {
                _logger?.LogCritical(LogMessages.WcfPortBindingError, baseAdress);
                throw;
            }
        }

        private void BootStrapFleetService(string baseUrl)
        {

            Uri baseAdress = new Uri(baseUrl + "/fleets");
            var host = new ServiceHost(typeof(FleetService), baseAdress);

            //Note: This is totally insecure and should never happen in Production, 
            //      the login credentials will transfered on an unencrypted channel, 
            //      in Production use Protocols they support TLS
            WSHttpBinding httpBinding = new WSHttpBinding();
            host.AddServiceEndpoint(typeof(IFleetService), httpBinding, "");
            host.AddDependencyInjectionBehavior<IFleetService>(_container);
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpsGetEnabled = true });
            try
            {
                host.Open();
                _logger?.LogInformation(LogMessages.WcfSuccessfullyStarted, baseAdress);
                _hosts.Add(host);
            }
            catch (Exception e) when (e is CommunicationObjectFaultedException ||
                                      e is TimeoutException ||
                                      e is AddressAlreadyInUseException)
            {
                _logger?.LogCritical(LogMessages.WcfPortBindingError, baseAdress);
                throw;
            }
            
        }
    }
}
