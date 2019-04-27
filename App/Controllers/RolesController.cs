using System.Collections.Generic;
using System.Threading.Tasks;
using App;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;
using Services.MainDb;
using System.Linq;
using Models.MainDbModel;
using Utils;

namespace Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBaseExtend
    {
        private readonly IRolesService _service;
        private readonly IRoutesService _routesService;
        private readonly IRoleRouteService _roleRouteService;

        public RolesController(IRolesService service, IRoutesService routesService, IRoleRouteService roleRouteService)
        {
            _service = service;
            _routesService = routesService;
            _roleRouteService = roleRouteService;
        }

        [HttpGet]
        public async Task<ResultModel> GetRoles()
        {
            var roles = await _service.FindAsync(x => !string.IsNullOrWhiteSpace(x.Key));
            if (roles.Count < 1)
            {
                roles.ToList().ForEach(x => _service.RemoveAsync(x));
                roles = new List<RolesModel>
                        {
                            new RolesModel
                            {
                                Key = "admin",
                                Name = "��������Ա",
                                Description = "��������ɶ�����ܿ���"
                            },
                            new RolesModel
                            {
                                Key = "editor",
                                Name = "�ɱ༭�ĳ�Ա",
                                Description = "����ܿ���һ���ְ�"
                            },
                            new RolesModel
                            {
                                Key = "visitor",
                                Name = "�۲���",
                                Description = "���Կ���һЩ��"
                            }
                        };
                await _service.AddRangeAsync(roles);
            }

            if (!roles.Any())
            {
                return Bad("û���κ�����");
            }

            var result = roles.Select(Mapper.ToExtend<RolesView>).ToList();

            var roleKeys = result.Select(x => x.Key);
            var routes = await _routesService.FindAsync(x => x.Id > 0);
            var roleRoutes = await _roleRouteService.FindAsync(x => roleKeys.Contains(x.RoleKey));

            result.ForEach(item =>
                           {
                               var matchRoleRoutes = roleRoutes.Where(x => x.RoleKey == item.Key);
                               var routeIds = matchRoleRoutes.Select(x => x.RouteId);
                               var matchRoutes = routes.Where(x => routeIds.Contains(x.Id)).ToArray();

                               var roots = matchRoutes.Where(x => x.ParentId == 0)
                                                      .Select(Mapper.ToExtend<RoutesView>)
                                                      .ToList();
                               roots.ForEach(x => _routesService.SetRouteChildren(x, matchRoutes));
                               item.Routes = roots;
                           });
            return Ok(result);
        }
    }
}