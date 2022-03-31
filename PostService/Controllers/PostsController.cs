using Microsoft.AspNetCore.Mvc;

namespace PostService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : Controller
    {
        public PostsController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
