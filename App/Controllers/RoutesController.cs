using App;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;
using Models.MainDbModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.MainDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoutesController : ControllerBaseExtend
    {
        private readonly IRoutesService _service;
        private readonly IRoleRouteService _roleRouteService;

        public RoutesController(IRoutesService service, IRoleRouteService roleRouteService)
        {
            _service = service;
            _roleRouteService = roleRouteService;
        }

        [HttpGet]
        public async Task<ResultModel> GetRoutes()
        {
            var all = (await _service.FindAsync(x => x.Id > 0)).ToArray();
            var root = all.Where(x => x.ParentId == 0)
                .Select(Mapper.ToExtend<RoutesView>)
                .ToList();
            root.ForEach(x => _service.SetRouteChildren(x, all));
            return Ok(root);
        }

        [HttpGet("init")]
        public async Task<string> InitRoutes()
        {
            var allold = await _service.FindAsync(x => x.Id > 0);
            await _service.RemoveRangeAsync(allold);
            var allRel = await _roleRouteService.FindAsync(x => x.Id > 0);
            await _roleRouteService.RemoveRangeAsync(allRel);

            var json = System.IO.File.ReadAllText("D:\\1.txt");
            var all = JsonConvert.DeserializeObject<RoutesInitView[]>(json);

            foreach (var item in all)
            {
                await _service.InitRouteAsync(item, 0);
            }

            await _roleRouteService.InitRoleRoute();

            return "OK";
        }
    }
}