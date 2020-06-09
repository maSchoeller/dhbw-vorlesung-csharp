using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Proxies.Calculations;
using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Dublin.Client.Proxies.Users;
using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Services
{
    public class ClientConnectionHandler
    {

        public ClientConnectionHandler()
        {

        }

        public UserServiceClient UserClient { get; private set; } = null!;
        public FleetServiceClient FleetsClient { get; private set; } = null!;
        public CalculationServiceClient CalculationClient { get; private set; } = null!;


        private OperationContextScope? _fleetsScope;
        private OperationContextScope? _calculationScope;
        private OperationContextScope? _userScope;

        public async Task<(bool success, string? errormessage)> TryLoginAsync(string username, string password)
        {
            await CleanupAsync();
            MessageHeader header;
            TokenEndpointBehavior behavior = new TokenEndpointBehavior();
            //Note: For scenarios like, login => logout => new login, i need a custome lifetime, only for this client connection.
            //      The alternative is Autofac customeliftimescopes but the solution with an extra service abstraction looks way more easier.
            UserClient = new UserServiceClient(new WSHttpBinding(), new EndpointAddress("http://localhost:8080/users"));
                UserClient.Endpoint.EndpointBehaviors.Add(behavior);
            try
            {
                await UserClient.OpenAsync();
                _userScope = new OperationContextScope(UserClient.InnerChannel);
                var result = await UserClient.LoginAsync(username, password);
                //header = MessageHeader.CreateHeader("TokenHeader", "TokenNameSpace", result.Token);
                //OperationContext.Current.OutgoingMessageHeaders.Add(header);
                if (!result.Success)
                    return (false, result.ErrorMessage!);
                behavior.Inspector.Token = result.Token;
            }
            catch (Exception e) when (e is EndpointNotFoundException)
            {
                return (false, "Der Server ist momentan leider nicht erreichbar.");
            }
            catch (Exception e) when (e is MessageSecurityException)
            {
                return (false, "Benutzer oder Passwort sind falsch.");
            }
            catch (Exception e)
            {
                return (false, "Ein unbekannter Fehler ist aufgetreten.");
            }

            try
            {
                FleetsClient = new FleetServiceClient(new WSHttpBinding(), new EndpointAddress("http://localhost:8080/fleets"));
                _fleetsScope = new OperationContextScope(FleetsClient.InnerChannel);
                //OperationContext.Current.OutgoingMessageHeaders.Add(header);

                CalculationClient = new CalculationServiceClient(new WSHttpBinding(), new EndpointAddress("http://localhost:8080/calculations"));
                _calculationScope = new OperationContextScope(CalculationClient.InnerChannel);
                //OperationContext.Current.OutgoingMessageHeaders.Add(header);
            }
            catch (Exception e)
            {
                //Todo: add logging
                return (false, "Ein unbekannter Fehler ist aufgetreten.");
            }
            return (true, null);
        }

        private async Task CleanupAsync()
        {
            _calculationScope?.Dispose();
            _fleetsScope?.Dispose();
            _userScope?.Dispose();

            if (UserClient?.State == CommunicationState.Opened)
                await (UserClient?.CloseAsync() ?? Task.CompletedTask);
            if (FleetsClient?.State == CommunicationState.Opened)
                await (FleetsClient?.CloseAsync() ?? Task.CompletedTask);
            if (CalculationClient?.State == CommunicationState.Opened)
                await (CalculationClient?.CloseAsync() ?? Task.CompletedTask);
        }
    }
}
