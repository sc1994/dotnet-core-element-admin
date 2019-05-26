using System;
using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static ElementAdmin.Application.Model.ApiResponse;

namespace ElementAdmin.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IUserService _user;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService user, IHttpContextAccessor httpContext, ILogger<UserController> logger)
        {
            _user = user;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ApiResponse> LoginUser(RegisterUserInfo register)
        {
            return await _user.LoginAsync(register);
        }

        [HttpGet("{token}")]
        public async Task<ApiResponse> GetUserByToken(Guid token)
        {
            return await _user.GetUserInfoByTokenAsync(token);
        }

        [HttpPost("logout")]
        public async Task<ApiResponse> LogoutUser()
        {
            var a = await _user.LogoutAsync();
            return Ok();
        }
    }
}