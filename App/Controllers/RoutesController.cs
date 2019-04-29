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

        public RoutesController(IRoutesService service)
        {
            _service = service;
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
            var json = System.IO.File.ReadAllText("D:\\1.txt");
            var all = JsonConvert.DeserializeObject<RoutesInitView[]>(json);

            foreach (var item in all)
            {
                await _service.InitRouteAsync(item, 0);
            }

            return "OK";
        }
    }
}