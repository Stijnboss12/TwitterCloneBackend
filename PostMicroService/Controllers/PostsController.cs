using Microsoft.AspNetCore.Mvc;
using PostMicroService.Services.Interfaces;

namespace PostMicroService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
    }
}
