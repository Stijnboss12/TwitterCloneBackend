using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
    }
}
