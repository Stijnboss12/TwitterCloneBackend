using Microsoft.EntityFrameworkCore;
using PostMicroService.Models;

namespace PostMicroService.Data
{
    public class PostDbContext : DbContext
    {
        public PostDbContext()
        {

        }

        public PostDbContext(DbContextOptions<PostDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}
