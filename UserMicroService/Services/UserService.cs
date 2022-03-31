using UserMicroService.Repositories.Interfaces;
using UserMicroService.Services.Interfaces;

namespace UserMicroService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
