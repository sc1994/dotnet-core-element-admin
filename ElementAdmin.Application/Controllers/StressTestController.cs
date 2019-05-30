using ElementAdmin.Application.Model;
using Microsoft.AspNetCore.Mvc;
using static ElementAdmin.Application.Model.ApiResponse;
using Flurl.Http;

namespace ElementAdmin.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StressTestController
    {
        [HttpGet]
        public ApiResponse StartStressTest()
        {
            var url = "";
            var data = "";
            var r = url.PostJsonAsync(data);
            return Ok();
        }
    }
}