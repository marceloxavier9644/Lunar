using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Lunar.Utils.SPCBrasilIntegracao
{

    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    public class CustomMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            // Aqui você pode acessar a resposta (reply) e fazer o que desejar com ela.
            // Por exemplo, você pode converter a resposta em XML e registrá-la.
            string responseXml = reply.ToString();
            Console.WriteLine("Mensagem de Resposta SOAP:");
            Console.WriteLine(responseXml);
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            // Aqui você pode acessar a solicitação (request) e fazer o que desejar com ela.
            // Por exemplo, você pode converter a solicitação em XML e registrá-la.
            string requestXml = request.ToString();
            Console.WriteLine("Mensagem de Solicitação SOAP:");
            Console.WriteLine(requestXml);
            return null;
        }
    }

    public class CustomEndpointBehavior : IEndpointBehavior
    {
        private readonly CustomMessageInspector _messageInspector;

        public CustomEndpointBehavior(CustomMessageInspector messageInspector)
        {
            _messageInspector = messageInspector;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(_messageInspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }

}
