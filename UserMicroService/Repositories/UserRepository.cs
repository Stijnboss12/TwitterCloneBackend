using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TwitterCloneBackend.Shared.Exceptions;
using UserMicroService.Data;
using UserMicroService.Models;
using UserMicroService.Models.DTO;
using UserMicroService.Repositories.Interfaces;

namespace UserMicroService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _dbContext = userDbContext;
        }

        public async Task<User> GetUser(string id)
        {
            return await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync() ?? new User();
        }

        public async Task<User> CreateUser(User newUser)
        {
            var user = await GetUser(newUser.Id);

            if (user.Id != string.Empty)
            {
                throw new DbUpdateException($"A user with id:{newUser.Id} already exists");
            }

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new EntityNotFoundException(id);
            }

            return user;
        }

        public async Task<List<User>> GetUsersByUsername(string username)
        {
            return await _dbContext.Users.Where(x => x.Username.Contains(username)).ToListAsync();
        }

        public async Task<User> EditUser(User userToChange)
        {
            _dbContext.Users.Update(userToChange);
            await _dbContext.SaveChangesAsync();

            return userToChange;
        }
    }
}
