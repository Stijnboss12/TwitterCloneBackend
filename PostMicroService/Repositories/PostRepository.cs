using Microsoft.EntityFrameworkCore;
using PostMicroService.Data;
using PostMicroService.Models;
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

        public async Task<List<Post>> GetPosts()
        {
            return await _dbContext.Posts.Take(20).ToListAsync();
        }

        public async Task<Post> CreateNewPost(Post post)
        {
            post.PostedOn = DateTime.UtcNow;
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();

            return post;
        }
    }
}
