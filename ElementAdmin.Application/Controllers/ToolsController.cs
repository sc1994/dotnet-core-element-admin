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
        public async Task<ApiResponse> InitRouteData(RouteModel[] routes)
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

        [HttpPost("search")]
        public async Task<ApiResponse> SearchLogs(ApiPageRequest<SearchModel> model)
        {
            return await _tools.SearchLogsAsync(model);
        }

        [HttpGet("search/{tracerId}")]
        public async Task<ApiResponse> SearchLogsChild(string tracerId)
        {
            return await _tools.SearchLogsChildAsync(tracerId);
        }

        [HttpPost("startstresstest")]
        public async Task<ApiResponse> StartStressTest(StressTestModel model)
        {
            return await _tools.StartStressTestAsync(model);
        }

        [HttpGet("abortstresstest/{connectionId}")]
        public ApiResponse AbortStressTest(string connectionId)
        {
            return _tools.AbortStressTest(connectionId);
        }
    }
}