using System.Threading.Tasks;
using Grpc.Net.Client;
using grpc_example;

namespace grpc_testing
{
    public class GreeterClientService
    {
        private readonly Greeter.GreeterClient _client;
        public GreeterClientService(Greeter.GreeterClient client)
        {
            _client = client;
        }

        public async Task<string> RequestHelloGreeting(string name)
        {
            var result = await _client.SayHelloAsync(new HelloRequest() {
                Name = name
            });

            return result.Message;
        }
    }
}