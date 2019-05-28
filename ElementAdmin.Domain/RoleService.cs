using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Role;
using ElementAdmin.Domain.Entity.ElementAdmin;
using ElementAdmin.Domain.Interface.ElementAdmin;
using static ElementAdmin.Application.Model.ApiResponse;

namespace ElementAdmin.Domain
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _role;
        private readonly IRoleRouteRepository _roleRoute;
        private readonly IRouteRepository _route;

        public RoleService(IRoleRepository role, IRoleRouteRepository roleRoute, IRouteRepository route)
        {
            _role = role;
            _roleRoute = roleRoute;
            _route = route;
        }

        public async Task<ApiResponse> AddRoleAsync(RoleModel model)
        {
            var first = await _role.FindAsync(x => x.RoleKey == model.RoleKey);
            if (first != null)
            {
                return Bad($"已存在 【{model.RoleKey}】 角色");
            }
            var role = await _role.AddAsync(model.ToRoleEntity());
            await _role.SaveChangesAsync();
            var routeEntities = model.RouteKeys.Select(x => new RoleRouteEntity
            {
                RoleId = role.Entity.Id,
                RouteId = Convert.ToInt64(x)
            });
            if (routeEntities.Any())
            {
                await _roleRoute.AddRangeAsync(routeEntities);
                await _roleRoute.SaveChangesAsync();
            }
            return Ok();
        }

        public Task<ApiResponse> DeleteRoleAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ApiResponse> GetRolesAsync()
        {
            var roles = await _role.WhereAsync(x => !x.IsDelete);
            var roleIds = roles.Select(x => x.Id);
            var roleRoutes = await _roleRoute.WhereAsync(x => roleIds.Contains(x.RoleId));
            var result = roles.Select(x => new RoleModel(x, roleRoutes));
            return Ok(result);
        }

        public async Task<ApiResponse> GetRoutesAsync()
        {
            var all = await _route.WhereAsync(x => !x.IsDelete);

            var result = new List<RouteTreeModel>();
            foreach (var item in all.Where(x => string.IsNullOrWhiteSpace(x.ParentRouteKey)))
            {
                var tree = new RouteTreeModel(item);
                AggRouteChildren(item, tree, all);
                result.Add(tree);
            }
            return Ok(result);
        }

        public Task<ApiResponse> UpdateRoleAsync(RoleModel model)
        {
            throw new System.NotImplementedException();
        }

        private void AggRouteChildren(RouteEntity item, RouteTreeModel tree, IEnumerable<RouteEntity> all)
        {
            foreach (var x in all.Where(x => x.ParentRouteKey == item.RouteKey))
            {
                var child = new RouteTreeModel(x);
                AggRouteChildren(x, child, all);
                tree.Children.Add(child);
            }
        }
    }
}