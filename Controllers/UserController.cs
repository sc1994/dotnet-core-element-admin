using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController
    {
        [HttpPost("login")]
        public ResultModel<dynamic> Login(object model)
        {
            return new ResultModel<dynamic>
            {
                Code = 20000,
                Data = new
                {
                    Token = "admin-token"
                }
            };
        }

        [HttpPost("logout")]
        public ResultModel<string> Logout()
        {
            return new ResultModel<string>
            {
                Code = 20000,
                Data = "success"
            };
        }

        [HttpGet("info")]
        public ResultModel<UserInfoModel> Info(string token)
        {
            return new ResultModel<UserInfoModel>
            {
                Code = 20000,
                Data = new UserInfoModel
                {
                    Name = "Super Admin",
                    Avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif",
                    Introduction = "I am a super administrator",
                    Roles = new List<string>
                            {
                              "admin"
                            }
                }
            };
        }
    }
}