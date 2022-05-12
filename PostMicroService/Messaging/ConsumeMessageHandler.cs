using MassTransit;
using TwitterCloneBackend.Shared.Messaging.MessageModels;

namespace PostMicroService.Messaging
{
    public class ConsumeMessageHandler : IConsumer<UsernameChangeMessage>
    {
        public async Task Consume(ConsumeContext<UsernameChangeMessage> context)
        {
            var data = context.Message;
        }
    }
}
