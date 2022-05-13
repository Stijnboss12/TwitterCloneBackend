using UserMicroService.Models;

namespace UserMicroService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUser(string id);
        public Task<User> CreateUser(User newUser);
        public Task<User> GetUserById(string id);
        public Task<List<User>> GetUsersByUsername(string username);
        public Task<User> EditUser(User userToChange);
    }
}
