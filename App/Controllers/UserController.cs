using System;
using System.Threading.Tasks;
using App;
using Database.MainDb;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;
using Models.Request.User;
using Utils;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBaseExtend
    {
        private readonly IUserInfoStorage _userInfo;

        public UserController(IUserInfoStorage userInfo)
        {
            _userInfo = userInfo;
            if (_userInfo.FirstOrDefaultAsync(x => x.Username == "admin").Result == default)
            {
                _userInfo.AddAsync(new UserInfoModel
                {
                    Name = "����Աһ��",
                    RolesString = "admin,editor",
                    Username = "admin",
                    Introduction = "����һ��",
                    Avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif",
                    Password = "111111",
                    Token = Guid.NewGuid().ToString()
                });
            }
        }

        [HttpPost("login")]
        public async Task<ResultModel> Login(LoginRequest request)
        {
            var user = await _userInfo.FirstOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password);
            if (user == default)
            {
                return Bad("�˺Ż����������");
            }

            user.Token = Guid.NewGuid().ToString();
            if (!(await _userInfo.UpdateAsync(user)).done)
            {
                return Bad("����token�����쳣");
            }

            return Ok(new
            {
                user.Token
            });
        }

        [HttpPost("logout")]
        public ResultModel Logout()
        {
            return new ResultModel<string>
            {
                Code = 20000,
                Data = "success"
            };
        }

        [HttpGet("info")]
        public async Task<ResultModel> GetUserInfo(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return Bad("�����쳣");
            }
            var info = await _userInfo.FirstOrDefaultAsync(x => x.Token == token);

            var result = Mapper.ToExtend<UserInfoView>(info);

            return Ok(result);
        }
    }
}