using Microsoft.EntityFrameworkCore;
using UserMicroService.Data;
using UserMicroService.Models;
using UserMicroService.Repositories.Interfaces;

namespace UserMicroService.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly UserDbContext _dbContext;

        public SubscriptionRepository(UserDbContext userDbContext)
        {
            _dbContext = userDbContext;
        }

        public async Task<Subscription> GetSubscription(Subscription subscription)
        {
            return await _dbContext.Subscriptions.Where(x => x.SubscriberId == subscription.SubscriberId && x.SubscribedId == subscription.SubscribedId).FirstOrDefaultAsync() ?? new Subscription();
        }

        public async Task<Subscription> CreateSubscription(Subscription newSubscription)
        {
            await _dbContext.Subscriptions.AddAsync(newSubscription);
            await _dbContext.SaveChangesAsync();

            return newSubscription;
        }

        public async Task<Subscription> RemoveSubscription(Subscription subscription)
        {
            _dbContext.Subscriptions.Remove(subscription);
            await _dbContext.SaveChangesAsync();

            return subscription;
        }
    }
}
