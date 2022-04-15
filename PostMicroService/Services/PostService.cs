using AutoMapper;
using PostMicroService.Models;
using PostMicroService.Models.DTO;
using PostMicroService.Repositories.Interfaces;
using PostMicroService.Services.Interfaces;

namespace PostMicroService.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task<Post> CreateNewPost(PostDTO postDTO)
        {
            if (postDTO.UserId == string.Empty)
            {
                throw new ArgumentException($"UserId can not be empty when creating a new post {postDTO}");
            }

            return await _postRepository.CreateNewPost(_mapper.Map<Post>(postDTO));
        }
    }
}
