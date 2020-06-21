using MaSchoeller.Dublin.Client.Helpers;
using MaSchoeller.Dublin.Client.Proxies.Calculations;
using MaSchoeller.Dublin.Client.Proxies.Fleets;
using MaSchoeller.Dublin.Client.Proxies.Users;
using System;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Security;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Services
{
    public class ClientConnectionHandler
    {
        public const string AdminIdentifier = "admin";
        public const string NameIdentifier = "username";
        public const string FullnameIdentifier = "fullname";
        public const string RoleIdentifier = "role";

        public ClientConnectionHandler()
        {

        }

        public UserServiceClient UserClient { get; private set; } = null!;
        public FleetServiceClient FleetsClient { get; private set; } = null!;
        public CalculationServiceClient CalculationClient { get; private set; } = null!;

        public UserContext? ActiveUser { get; private set; }

        public async Task<(bool success, string? errormessage)> TryLoginAsync(string username, string password)
        {
            //await CleanupAsync();
            //Note: For scenarios like, login => logout => new login, i need a custome lifetime, only for this client connection.
            //      The alternative is autofac customeliftimescopes but the solution with an extra service abstraction looks way more easier.
            TokenEndpointBehavior behavior = new TokenEndpointBehavior();
            try
            {
                UserClient = new UserServiceClient(new WSHttpBinding(), new EndpointAddress("http://localhost:8080/users"));
                UserClient.Endpoint.EndpointBehaviors.Add(behavior);
                await UserClient.OpenAsync();
                var result = await UserClient.LoginAsync(username, password);
                if (result.Reason != Proxies.Users.OperationResult.Success)
                    return (false, DisplayMessages.PasswordOrUsernameIsWrong);
                behavior.Inspector.Token = result.Token;
                SetUserContext(result.Token);
            }
            catch (EndpointNotFoundException e)
            {
                return (false, DisplayMessages.ServerNotAvailable);
            }
            catch (Exception e)
            {
                return (false, DisplayMessages.UnkonwError);
            }

            try
            {
                FleetsClient = new FleetServiceClient(new WSHttpBinding(), new EndpointAddress("http://localhost:8080/fleets"));
                FleetsClient.Endpoint.EndpointBehaviors.Add(behavior);
                CalculationClient = new CalculationServiceClient(new WSHttpBinding(), new EndpointAddress("http://localhost:8080/calculations"));
                CalculationClient.Endpoint.EndpointBehaviors.Add(behavior);
            }
            catch (Exception e)
            {
                //Todo: add logging
                return (false, DisplayMessages.UnkonwError);
            }
            return (true, null);
        }

        private void SetUserContext(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            ActiveUser = new UserContext();

            foreach (var claim in jwtToken.Claims)
            {
                if (claim.Type == FullnameIdentifier)
                {
                    ActiveUser.Fullname = claim.Value;
                }
                if (claim.Type == NameIdentifier)
                {
                    ActiveUser.Username = claim.Value;
                }
                if (claim.Type == RoleIdentifier && claim.Value == AdminIdentifier)
                {
                    ActiveUser.IsAdmin = true;
                }
            }
        }

        //private async Task CleanupAsync()
        //{
        //    if (UserClient?.State == CommunicationState.Opened)
        //        await UserClient.CloseAsync();
        //    if (FleetsClient?.State == CommunicationState.Opened)
        //        await FleetsClient.CloseAsync();
        //    if (CalculationClient?.State == CommunicationState.Opened)
        //        await CalculationClient.CloseAsync();
        //}
    }
}
