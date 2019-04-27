using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;
using Models.MainDbModel;
using Services.MainDb;
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
    }
}