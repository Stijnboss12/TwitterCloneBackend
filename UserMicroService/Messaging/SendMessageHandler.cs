using MassTransit;
using TwitterCloneBackend.Shared.Messaging.MessageModels;

namespace UserMicroService.Messaging
{
    public class SendMessageHandler : ISendMessageHandler
    {
        private readonly IBus _bus;
        private Uri RabbitMQBaseUrl;

        public SendMessageHandler(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            RabbitMQBaseUrl = new Uri(configuration.GetSection("AppConfig")["RabbitMQBaseUrl"]);
        }

        public async Task SendUsernameChangeMessage(UsernameChangeMessage message)
        {
            if (message != null)
            {
                Uri uri = new Uri($"{RabbitMQBaseUrl}/MessageQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(message);
            }
        }
    }
}
