using System.Threading.Tasks;
using Grpc.Net.Client;
using grpc_example;

namespace grpc_testing
{
    public class GreeterClientServiceContainer
    {
        private readonly GreeterClientService _service;
        public GreeterClientServiceContainer()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new Greeter.GreeterClient(channel);
            _service = new GreeterClientService(client);
        }
    }
}