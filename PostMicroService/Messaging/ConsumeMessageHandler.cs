using MassTransit;
using PostMicroService.Services.Interfaces;
using TwitterCloneBackend.Shared.Messaging.MessageModels;

namespace PostMicroService.Messaging
{
    public class ConsumeMessageHandler : IConsumer<UsernameChangeMessage>
    {
        private readonly IPostService _postService;
        public ConsumeMessageHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task Consume(ConsumeContext<UsernameChangeMessage> context)
        {
            var data = context.Message;

            await _postService.UpdateUsernameOfPosts(data.UserId, data.Username);
        }
    }
}
