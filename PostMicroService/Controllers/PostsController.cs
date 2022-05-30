using Microsoft.AspNetCore.Mvc;
using PostMicroService.Models.DTO;
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

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();

            return Ok(posts);
        }

        [HttpPost("New")]
        public async Task<IActionResult> CreateNewPost([FromBody] PostDTO postDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            postDTO.UserId = Request.Headers.ToDictionary(x => x.Key, x => x.Value)["id"];
            postDTO.Username = Request.Headers.ToDictionary(x => x.Key, x => x.Value)["username"];

            var createdPost = await _postService.CreateNewPost(postDTO);

            return Ok(createdPost);
        }

        [HttpGet("ErrorEndpoint")]
        public async Task<IActionResult> ErrorEndpoint()
        {
            throw new NotImplementedException();
        }
    }
}
