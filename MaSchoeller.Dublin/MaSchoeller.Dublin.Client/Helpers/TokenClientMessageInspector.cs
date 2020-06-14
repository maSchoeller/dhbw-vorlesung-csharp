using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Client.Helpers
{
    public class TokenClientMessageInspector : IClientMessageInspector
    {

        public TokenClientMessageInspector()
        {

        }

        public string? Token { get; set; }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (!(Token is null))
            {
                var header = MessageHeader.CreateHeader("TokenHeader", "TokenNameSpace", Token);
                request.Headers.Add(header);
            }
            return null!;
        }
    }

    public class TokenEndpointBehavior : IEndpointBehavior
    {
        public TokenClientMessageInspector Inspector { get; set; } = new TokenClientMessageInspector();

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(Inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint) { }
    }
}