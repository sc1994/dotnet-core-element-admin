using App;
using Database.MainDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.MainDb;
using Models.Request.User;
using System;
using System.Threading.Tasks;
using Utils;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IUserInfoStorage _userInfo;
        private readonly ICustomService _service;

        public UserController(IHttpContextAccessor context, IUserInfoStorage userInfo, ICustomService service) : base(context)
        {
            _userInfo = userInfo;
            _service = service;
            if (_userInfo.FirstOrDefaultAsync(x => x.Username == "admin").Result == default)
            {
                _userInfo.AddAsync(new UserInfoModel
                {
                    Name = "管理员一号",
                    RolesString = "admin,editor",
                    Username = "admin",
                    Introduction = "我是一号",
                    Avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif",
                    Password = "111111",
                    Token = Guid.NewGuid().ToString()
                });
            }
        }

        [HttpPost("login")]
        public async Task<BaseResponse> Login(LoginRequest request)
        {
            _service.Call();
            var user = await _userInfo.FirstOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password);
            if (user == default)
            {
                return Bad("账号或者密码错误");
            }

            user.Token = Guid.NewGuid().ToString();
            if (!(await _userInfo.UpdateAsync(user)).done)
            {
                return Bad("更新token发生异常");
            }

            return Ok(new
            {
                user.Token
            });
        }

        [HttpPost("logout")]
        public BaseResponse Logout()
        {
            return new Response<string>
            {
                Code = 20000,
                Data = "success"
            };
        }

        [HttpGet("info")]
        public async Task<BaseResponse> GetUserInfo(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return Bad("参数异常");
            }
            var info = await _userInfo.FirstOrDefaultAsync(x => x.Token == token);

            var result = Mapper.ToExtend<UserInfoView>(info);

            return Ok(result);
        }
    }
}