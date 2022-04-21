using AutoMapper;
using UserMicroService.Models;
using UserMicroService.Models.DTO;
using UserMicroService.Repositories.Interfaces;
using UserMicroService.Services.Interfaces;

namespace UserMicroService.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<Subscription> CreateSubscription(SubscriptionDTO newSubscriptionDTO)
        {
            var existingSubscription = await _subscriptionRepository.GetSubscription(_mapper.Map<Subscription>(newSubscriptionDTO));
            if (existingSubscription.Id != Guid.Empty)
            {
                return existingSubscription;
            }

            if (newSubscriptionDTO.SubscriberId == newSubscriptionDTO.SubscribedId)
            {
                throw new Exception("Users can not subscribe to themselves");
            }

            var createdSubscription = await _subscriptionRepository.CreateSubscription(_mapper.Map<Subscription>(newSubscriptionDTO));

            return createdSubscription;
        }

        public async Task<Subscription> RemoveSubscription(SubscriptionDTO subscriptionDTO)
        {
            var subscriptionToDelete = _mapper.Map<Subscription>(subscriptionDTO);

            var deletedSubscription = await _subscriptionRepository.RemoveSubscription(subscriptionToDelete);

            return deletedSubscription;
        }
    }
}
