using ElementAdmin.Domain.Factories;
using ElementAdmin.Domain.ObjVal;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElementAdmin.Application.Web.Controllers
{
    /// <summary>
    /// 路由数据
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RouteController
    {
        private readonly IRouteFactory _route;

        /// <summary>
        /// 路由数据
        /// </summary>
        /// <param name="route"></param>
        public RouteController(IRouteFactory route)
        {
            _route = route;
        }

        /// <summary>
        /// 获取路由
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetRoutes()
        {
            return await _route.GetRoutesAsync();
        }
    }
}