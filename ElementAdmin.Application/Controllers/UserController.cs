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

        public UserController(IUserService user)
        {
            _user = user;
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