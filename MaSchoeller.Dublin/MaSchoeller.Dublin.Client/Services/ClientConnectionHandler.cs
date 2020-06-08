using MaSchoeller.Dublin.Client.Proxies.Calculations;
using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Dublin.Client.Proxies.Users;
using System;
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

        public async Task<(bool success, string? errormessage)> TryLoginAsync(string username, string password)
        {
            await (UserClient?.CloseAsync() ?? Task.CompletedTask);
            await (FleetsClient?.CloseAsync() ?? Task.CompletedTask);
            await (CalculationClient?.CloseAsync() ?? Task.CompletedTask);

            var userBinding = new WSHttpBinding();
            //Note: For scenarios like, login => logout => new login, i need a custome lifetime, only for this client connection.
            //      The alternative is Autofac customeliftimescopes but the solution with an extra service abstraction looks way more easier.
            UserClient = new UserServiceClient(userBinding, new EndpointAddress("http://localhost:8080/users"));
            try
            {
                UserClient.Open();
                var scope = new OperationContextScope(UserClient.InnerChannel);
                var result = await UserClient.LoginAsync(username, password);
                var header = MessageHeader.CreateHeader("TokenHeader", "TokenNameSpace", result.Token);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
     
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
            var calcBinding = new WSHttpBinding();
            

            var fleetBinding = new WSHttpBinding();


            return (true, null);
        }
    }
}
