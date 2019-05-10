using ElementAdmin.Domain.Context.UserInfoContext;
using ElementAdmin.Domain.Factories;
using ElementAdmin.Domain.ObjVal;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElementAdmin.Application.Web.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IUserInfoFactory _user;

        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="user"></param>
        public UserController(IUserInfoFactory user)
        {
            _user = user;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<Result> UserLogin(UserLoginContext context)
        {
            return await _user.UserLoginAsync(context);
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{token}")]
        public async Task<Result> GetUserInfo(string token)
        {
            return await _user.GetUserInfoByTokenAsync(token);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public Result UserLogout()
        {
            return new Result
            {
                Code = ResultCodeEnum.成功
            };
        }
    }
}