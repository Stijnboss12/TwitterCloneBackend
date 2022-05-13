using PostMicroService.Models;

namespace PostMicroService.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public Task<List<Post>> GetPosts();
        public Task<List<Post>> GetPostsByUserId(string userId);
        public Task<Post> CreateNewPost(Post post);
        public Task<List<Post>> UpdatePosts(List<Post> posts);
    }
}
