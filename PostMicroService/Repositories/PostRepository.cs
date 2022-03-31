using PostMicroService.Data;
using PostMicroService.Repositories.Interfaces;

namespace PostMicroService.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PostDbContext _dbContext;

        public PostRepository(PostDbContext postDbContext)
        {
            _dbContext = postDbContext;
        }
    }
}
