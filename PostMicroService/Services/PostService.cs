using PostMicroService.Repositories.Interfaces;
using PostMicroService.Services.Interfaces;

namespace PostMicroService.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
    }
}
