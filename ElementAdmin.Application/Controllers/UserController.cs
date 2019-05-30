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

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ApiResponse> LoginUser(RegisterUserInfo register)
        {
            return await _user.LoginAsync(register);
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{token}")]
        public async Task<ApiResponse> GetUserByToken(Guid token)
        {
            return await _user.GetUserInfoByTokenAsync(token);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<ApiResponse> LogoutUser()
        {
            return await _user.LogoutAsync();

        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost("logup")]
        public async Task<ApiResponse> LogUpUser(RegisterUserInfo register)
        {
            return await _user.LogUpUserAsync(register);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("search")]
        public async Task<ApiResponse> SearchUser(SearchUserModel model)
        {
            return await _user.SearchUserAsync(model);
        }
    }
}