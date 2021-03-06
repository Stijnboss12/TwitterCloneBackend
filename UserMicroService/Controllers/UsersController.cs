using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TwitterCloneBackend.Shared.Messaging.MessageModels;
using UserMicroService.Messaging;
using UserMicroService.Models;
using UserMicroService.Models.DTO;
using UserMicroService.Services.Interfaces;

namespace UserMicroService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISendMessageHandler _sendMessageHandler;
        private readonly IConfiguration _configuration;
        private readonly IBus _bus;

        public UsersController(IUserService userService, ISubscriptionService subscriptionService, ISendMessageHandler sendMessageHandler, IConfiguration configuration, IBus bus)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
            _sendMessageHandler = sendMessageHandler;
            _configuration = configuration;
            _bus = bus;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> UserLogin()
        {
            var headers = Request.Headers.ToDictionary(x => x.Key, x => x.Value);

            var user = new UserDTO() { Id = headers["id"], Username = headers["username"] };

            var loggedinuser = await _userService.UserLogin(user);

            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> UserRegister([FromBody] UserDTO userDTO)
        {
            var registeredUser = await _userService.UserLogin(userDTO);

            return Ok();
        }

        [HttpGet("ById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (id == string.Empty)
            {
                return BadRequest(ModelState);
            }

            var users = await _userService.GetUserById(id);

            return Ok(users);
        }

        [HttpGet("ByUsername/{username}")]
        public async Task<IActionResult> GetUsersByUsername(string username)
        {
            if (username == string.Empty)
            {
                return BadRequest(ModelState);
            }

            var users = await _userService.GetUsersByUsername(username);

            return Ok(users);
        }

        [HttpPost("Subscribe")]
        public async Task<IActionResult> SubscribeToUser([FromBody] SubscriptionDTO subscriptionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            subscriptionDTO.SubscriberId = Request.Headers.ToDictionary(x => x.Key, x => x.Value)["id"];

            var createdSubscription = await _subscriptionService.CreateSubscription(subscriptionDTO);

            return Ok(createdSubscription);
        }

        [HttpPost("ChangeUsername")]
        public async Task<IActionResult> ChangeUsername([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = await _userService.ChangeUsername(userDTO);

            var message = new UsernameChangeMessage() { UserId = updatedUser.Id, Username = updatedUser.Username };

            await _sendMessageHandler.SendUsernameChangeMessage(message);

            return Ok();
        }

        [HttpGet("ErrorEndpoint")]
        public async Task<IActionResult> ErrorEndpoint()
        {
            throw new NotImplementedException();
        }

        [HttpGet("TestUser")]
        public async Task<IActionResult> GetTestUser()
        {
            var user = new User()
            {
                Id = "test",
                Username = "testusername"
            };

            return Ok(user);
        }
    }
}
