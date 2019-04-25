using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController
    {
        [HttpGet("list")]
        public string List()
        {
            return "";
        }
    }
}