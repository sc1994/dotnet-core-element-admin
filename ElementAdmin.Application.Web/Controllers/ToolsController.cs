using ElementAdmin.Domain.Context;
using ElementAdmin.Domain.Factories;
using ElementAdmin.Domain.ObjVal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ElementAdmin.Application.Web.Controllers
{
    /// <summary>
    /// 工具集
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ToolsController
    {
        private readonly IToolsFactory _tools;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tools"></param>
        public ToolsController(IToolsFactory tools)
        {
            _tools = tools;
        }

        /// <summary>
        /// 初始化路由数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("initroutedata")]
        public async Task<Result> InitRouteData()
        {
            var mock = JsonConvert.DeserializeObject<List<InitRouteDataContext>>(
                File.ReadAllText(@"D:\Other\ElementAdmin2\ElementAdmin.Application.Web\wwwroot\routes.json"));
            return await _tools.InitRouteDataAsync(mock);
        }

        /// <summary>
        /// 初始化用户数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("inituserdata")]
        public async Task<Result> InitUserData()
        {
            return await _tools.InitUserDataAsync();
        }
    }
}