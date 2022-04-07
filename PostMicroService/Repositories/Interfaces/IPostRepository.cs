using PostMicroService.Models;

namespace PostMicroService.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public Task<List<Post>> GetPosts();
        public Task<Post> CreateNewPost(Post post);
    }
}
