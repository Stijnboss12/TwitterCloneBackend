using UserMicroService.Models;
using UserMicroService.Models.DTO;

namespace UserMicroService.Services.Interfaces
{
    public interface ISubscriptionService
    {
        public Task<Subscription> CreateSubscription(SubscriptionDTO newSubscriptionDTO);

        public Task<Subscription> RemoveSubscription(SubscriptionDTO subscriptionDTO);
    }
}
