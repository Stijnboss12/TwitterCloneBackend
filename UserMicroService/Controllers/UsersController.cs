using Microsoft.AspNetCore.Mvc;
using UserMicroService.Models.DTO;
using UserMicroService.Services.Interfaces;

namespace UserMicroService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> UserLogin()
        {
            var headers = Request.Headers.ToDictionary(x => x.Key, x => x.Value);

            var user = new UserDTO() { Id = headers["id"], Username = headers["username"] };

            var loggedinuser = await _userService.UserLogin(user);

            return Ok();
        }
    }
}
