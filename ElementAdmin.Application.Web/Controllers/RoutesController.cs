using System.Threading.Tasks;
using ElementAdmin.Domain.Factories;
using ElementAdmin.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace ElementAdmin.Application.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoutesController : BaseController
    {
        private readonly IRoutesFactory _routes;

        public RoutesController(IRoutesFactory routes)
        {
            _routes = routes;
        }

        [HttpGet("view")]
        public async Task<BaseResponse> GetRoutes()
        {
            return Ok(await _routes.GetRoutes());
        }

        [HttpGet]
        public async Task<BaseResponse> GetRoutesTree()
        {
            var result = await _routes.GetRoutesTree();
            if (!result.Done)return Bad(result.Message);
            return Ok(result.Result);
        }

        [HttpGet("test")]

        public string RoutesTest()
        {
            return "[{\"path\":\"complex-table\",\"component\":\"@/views/dev/table/complex-table\",\"name\":\"ComplexTable\",\"meta\":{\"title\":\"综合 Table\"}}]";
        }
    }
}