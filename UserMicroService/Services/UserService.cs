using AutoMapper;
using UserMicroService.Models;
using UserMicroService.Models.DTO;
using UserMicroService.Repositories.Interfaces;
using UserMicroService.Services.Interfaces;

namespace UserMicroService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> UserLogin(UserDTO userDTO)
        {
            var loggedinUser = await _userRepository.GetUser(userDTO.Id);

            if (loggedinUser.Id != string.Empty)
            {
                return loggedinUser;
            }

            var createdUser = await _userRepository.CreateUser(_mapper.Map<User>(userDTO));

            return createdUser;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<List<User>> GetUsersByUsername(string username)
        {
            return await _userRepository.GetUsersByUsername(username);
        }

        public async Task<User> ChangeUsername(UserDTO userDTO)
        {
            var user = await _userRepository.GetUserById(userDTO.Id);

            user.Username = userDTO.Username;

            var changeduser = await _userRepository.EditUser(user);

            return changeduser;
        }
    }
}
