using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using grpc_example;
using Moq;
using NUnit.Framework;

namespace grpc_testing
{
    public class GreeterClientServiceTests
    {
        [Test]
        public async Task WillMockSayHelloAsync()
        {
            var client = new Mock<Greeter.GreeterClient>();
            // based on https://github.com/grpc/grpc/pull/15687/files#diff-1bcc0a32109704bea049ab7df4f68040R59
            var call = new Grpc.Core.AsyncUnaryCall<HelloReply>(
                Task.FromResult(new HelloReply()
                {
                    Message = "Hello John Smith"
                }),
                Task.FromResult(new Metadata()),
                () => Status.DefaultSuccess,
                () => new Metadata(),
                () => { }
            );
            client.Setup(m => m.SayHelloAsync(
                It.IsAny<HelloRequest>(), null, null, It.IsAny<CancellationToken>()))
                .Returns(call);
            var uut = new GreeterClientService(client.Object);

            var result = await uut.RequestHelloGreeting("John Smith");

            Assert.That(result, Is.EqualTo("Hello John Smith"));
        }
    }
}