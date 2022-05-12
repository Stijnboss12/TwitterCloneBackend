using TwitterCloneBackend.Shared.Messaging.MessageModels;

namespace UserMicroService.Messaging
{
    public interface ISendMessageHandler
    {
        public Task SendUsernameChangeMessage(UsernameChangeMessage message);
    }
}
