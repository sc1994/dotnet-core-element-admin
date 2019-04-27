using System.Threading.Tasks;
using Database.MainDb;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;
using Utils;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IUserInfoStorage _userInfo;

        public UserController(IUserInfoStorage userInfo)
        {
            _userInfo = userInfo;
        }

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
        public async Task<ResultModel> GetUserInfo(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return new ResultModel
                {
                    Data = "²ÎÊýÒì³£",
                    Code = 500
                };
            }
            var info = await _userInfo.FirstOrDefaultAsync(x => x.Token == token);

            var result = Mapper.ToExtend<UserInfoView>(info);

            return new ResultModel<UserInfoView>
            {
                Code = 20000,
                Data = result
            };
        }
    }
}