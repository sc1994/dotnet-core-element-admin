using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Tools;
using Microsoft.AspNetCore.Mvc;

namespace ElementAdmin.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToolsController
    {
        private readonly IToolService _tools;

        public ToolsController(IToolService tools)
        {
            _tools = tools;
        }

        [HttpPost("initroutedata")]
        public async Task<ApiResponse> InitRouteData(InitRouteDataModel[] routes)
        {
            return await _tools.InitRouteDataAsync(routes);
        }

        [HttpGet("getentities")]
        public ApiResponse GetEntities()
        {
            return _tools.GetEntities();
        }

        [HttpPost("initentities")]
        public ApiResponse InitEntities(string[] entities)
        {
            return _tools.InitEntities(entities);
        }
    }
}