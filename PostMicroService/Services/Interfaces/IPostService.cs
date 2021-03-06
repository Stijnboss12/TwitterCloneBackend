using PostMicroService.Models;
using PostMicroService.Models.DTO;

namespace PostMicroService.Services.Interfaces
{
    public interface IPostService
    {
        public Task<List<Post>> GetPosts();
        public Task<Post> CreateNewPost(PostDTO postDTO);
        public Task<List<Post>> UpdateUsernameOfPosts(string userId, string username);
    }
}
