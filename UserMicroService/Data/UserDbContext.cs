using Microsoft.EntityFrameworkCore;
using UserMicroService.Models;

namespace UserMicroService.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext()
        {

        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}
