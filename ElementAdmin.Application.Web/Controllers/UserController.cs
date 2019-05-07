using ElementAdmin.Domain.Context;
using ElementAdmin.Domain.Factories;
using ElementAdmin.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElementAdmin.Application.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly ILoginFactory _login;

        public UserController(ILoginFactory login)
        {
            _login = login;
        }

        [HttpPost("login")]
        public async Task<BaseResponse> UserLogin(LoginContext context)
        {
            var result = await _login.UserLogin(context);

            if (!result.Done)
            {
                return Bad(result.Message);
            }
            return Ok(new
            {
                token = result.Result
            });
        }

        [HttpGet("info")]
        public async Task<BaseResponse> GetUserInfo(string token)
        {
            var result = await _login.GetUserInfo(token);
            if (!result.Done) return Bad(result.Message);

            return Ok(result.Result);
        }
    }
}