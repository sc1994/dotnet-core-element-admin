using App;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.MainDb;
using Models.MainDbModel;
using Services.MainDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Controllers
{
    [Route("[controller]")]
    [Route("role")]
    [ApiController]
    public class RolesController : ControllerBaseExtend
    {
        private readonly IRolesService _service;
        private readonly IRoutesService _routesService;
        private readonly IRoleRouteService _roleRouteService;

        public RolesController(IRolesService service,
                               IRoutesService routesService,
                               IRoleRouteService roleRouteService)
        {
            _service = service;
            _routesService = routesService;
            _roleRouteService = roleRouteService;
        }

        /// <summary>
        /// ��ȡ��ɫ��Ϣ
        /// </summary>
        /// <returns></returns>
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
            var routes = (await _routesService.FindAsync(x => x.Id > 0)).ToArray();
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

        /// <summary>
        /// �༭��ɫȨ��
        /// </summary>
        /// <param name="role"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPut("{role}")]
        public async Task<ResultModel> EditRole(string role, [FromBody]RolesView view)
        {
            var roleModel = _service.FirstOrDefaultAsync(x => x.Key == role);
            if (roleModel == null) return Bad("Parameters of the abnormal");
            var roleRoutes = await _roleRouteService.FindAsync(x => x.RoleKey == role);

            // ��ƽ����
            var routeModels = new List<RoutesModel>();
            foreach (var item in view.Routes)
            {
                var tempModels = new List<RoutesModel>();
                _routesService.GetFlatRoute(item, 0, tempModels);
                routeModels.AddRange(tempModels);
            }

            // ɾ��
            var deleteIds = roleRoutes.Select(x => x.RouteId).Except(routeModels.Select(x => x.Id));
            var deleteModels = roleRoutes.Where(x => deleteIds.Contains(x.RouteId));
            if (deleteModels.Any())
            {
                await _roleRouteService.RemoveRangeAsync(deleteModels);
            }

            // ���
            var addModels = new List<RoleRouteModel>();
            foreach (var item in routeModels)
            {
                if (roleRoutes.Any(x => x.RouteId == item.Id)) continue;
                addModels.Add(new RoleRouteModel
                {
                    RoleKey = role,
                    RouteId = item.Id
                });
            }
            if (addModels.Any())
            {
                await _roleRouteService.AddRangeAsync(addModels);
            }

            return Ok(view);
        }

        /// <summary>
        /// ��ӽ�ɫ
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> AddRole([FromBody] RolesView view)
        {
            view.Key = view.Name;
            var roleModel = await _service.FirstOrDefaultAsync(x => x.Key == view.Key);
            if (roleModel != null) return Bad("�Ѵ��ڵĽ�ɫ");

            await _service.AddAsync(view);

            await _roleRouteService.AddRangeAsync(view.Routes.Select(x => new RoleRouteModel
            {
                RoleKey = view.Key,
                RouteId = x.Id
            }));

            return Ok("��ӳɹ�");
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpDelete("{role}")]
        public async Task<ResultModel> Deleterole(string role)
        {
            var roleModel = await _service.FirstOrDefaultAsync(x => x.Key == role);
            if (roleModel == null) return Bad("Parameters of the abnormal");
            await _service.RemoveAsync(roleModel);

            // ɾ����ɫȨ��
            var roleRouteModels = await _roleRouteService.FindAsync(x => x.RoleKey == role);
            if (roleRouteModels.Any())
            {
                await _roleRouteService.RemoveRangeAsync(roleRouteModels);
            }

            return Ok();
        }
    }
}