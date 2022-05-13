using UserMicroService.Models;

namespace UserMicroService.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        public Task<Subscription> GetSubscription(Subscription subscription);
        public Task<Subscription> CreateSubscription(Subscription newSubscription);

        public Task<Subscription> RemoveSubscription(Subscription subscription);
    }
}
