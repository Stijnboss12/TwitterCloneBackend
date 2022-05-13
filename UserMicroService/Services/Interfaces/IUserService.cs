using UserMicroService.Models;
using UserMicroService.Models.DTO;

namespace UserMicroService.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> UserLogin(UserDTO userDTO);
        public Task<User> GetUserById(string id);
        public Task<List<User>> GetUsersByUsername(string username);
        public Task<User> ChangeUsername(UserDTO userDTO);
    }
}
