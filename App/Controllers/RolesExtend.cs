using System.Collections.Generic;
using System.Threading.Tasks;
using App;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;
using Services.MainDb;
using System.Linq;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesExtend : ControllerBaseExtend
    {
        private readonly IRolesService _service;
        private readonly IRoutesService _routesService;

        public RolesExtend(IRolesService service, IRoutesService routesService)
        {
            _service = service;

            _routesService = routesService;
        }

        [HttpGet]
        public async Task<ResultModel> GetRoles()
        {
            var roles = await _service.FindAsync(x => string.IsNullOrWhiteSpace(x.Key));

            if (!(roles?.Any() ?? false))
            {
                return Bad("没有任何数据");
            }

            //var roleIds = roles.Select(x => x.Key);
            //var routes = await _routesService.FindAsync(x => roleIds.Contains(x.RoleKey));

            return new ResultModel<List<RolesModel>>
            {
                Code = 20000,
                Data = new List<RolesModel>
                {
                    //new RolesModel
                    //{
                    //    Key = "",
                    //    Name = "",
                    //    Description = "",
                    //    //Routes = new List<RoutesModel>() todo
                    //}
                }
            };
        }
    }
}