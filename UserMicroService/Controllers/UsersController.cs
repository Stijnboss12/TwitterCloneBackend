﻿using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IUserService userService, ISubscriptionService subscriptionService)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> UserLogin()
        {
            var headers = Request.Headers.ToDictionary(x => x.Key, x => x.Value);

            var user = new UserDTO() { Id = headers["id"], Username = headers["username"] };

            var loggedinuser = await _userService.UserLogin(user);

            return Ok();
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUsers(string username)
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
    }
}
