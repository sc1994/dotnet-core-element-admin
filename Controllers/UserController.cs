using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController
    {
        [HttpPost("login")]
        public string Login(object model)
        {
            return "{\"code\":20000,\"data\":{\"token\":\"admin-token\"}}";
        }

        [HttpGet("info")]
        public string Info(string token)
        {
            return "{\"code\":20000,\"data\":{\"roles\":[\"admin\"],\"introduction\":\"I am a super administrator\",\"avatar\":\"https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif\",\"name\":\"Super Admin\"}}";
        }
    }
}