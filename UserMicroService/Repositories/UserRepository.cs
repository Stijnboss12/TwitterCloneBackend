using UserMicroService.Data;
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
    }
}
