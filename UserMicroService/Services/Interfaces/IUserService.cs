using UserMicroService.Models;
using UserMicroService.Models.DTO;

namespace UserMicroService.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> UserLogin(UserDTO userDTO);
        public Task<List<User>> GetUsersByUsername(string username);
    }
}
