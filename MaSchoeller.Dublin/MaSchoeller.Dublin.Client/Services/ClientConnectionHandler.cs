using Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Services
{
    public class ClientConnectionHandler
    {
        public FleetServiceClient Client { get; private set; } = null;

        public async Task<(bool success, string errormessage)> TryLoginAsync(string username, string password)
        {
            var binding = new WSHttpBinding();

            //Note: For scenarios like, login => logout => new login, i need a custome lifetime, only for this client connection.
            //      The alternative is Autofac customeliftimescopes but the solution with an extra service abstraction looks way more easier.
            Client = new FleetServiceClient(binding, new EndpointAddress("http://localhost:8080/fleets"));
            try
            {
                Client.Open();
                var scope = new OperationContextScope(Client.InnerChannel);
                var result = await Client.LoginAsync(username, password);
                var header = MessageHeader.CreateHeader("TokenHeader", "TokenNameSpace", result.Token);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                return (true, null);
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
                return (false, "Ein Unbekannter fehler ist aufgetreten.");
            }



        }

    }
}
