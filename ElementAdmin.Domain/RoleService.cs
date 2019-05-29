using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Role;
using ElementAdmin.Domain.Entity.ElementAdmin;
using ElementAdmin.Domain.Interface.ElementAdmin;
using ElementAdmin.Infrastructure;
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
            var first = await _role.FindAsync(x => x.RoleKey == model.RoleKey && !x.IsDelete);
            if (!model.VerifyAdd(first)) return Bad(model.VerifyMessgae);
           
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

        public async Task<ApiResponse> DeleteRoleAsync(long id)
        {
            var role = await _role.FindAsync(x => x.Id == id && !x.IsDelete);
            if (RoleModel.VerifyDelete(role)) return Bad("不存在数据");

            await _role.RemoveAsync(role);
            var row = await _role.SaveChangesAsync();
            if (row < 1) return Bad("删除失败");
            row = await _roleRoute.RemoveRangeAsync(x => x.RoleId == role.Id && !x.IsDelete);

            return Ok(row);
        }

        public async Task<ApiResponse> GetRolesAsync()
        {
            var roles = await _role.WhereAsync(x => !x.IsDelete);
            var roleIds = roles.Select(x => x.Id);
            var roleRoutes = await _roleRoute.WhereAsync(x => roleIds.Contains(x.RoleId) && !x.IsDelete);
            var routes = await _route.WhereAsync(x => !x.IsDelete);
            var bottom = new List<RouteEntity>();
            foreach (var item in routes)
            {
                if (!routes.Any(x => x.ParentRouteKey == item.RouteKey))
                {
                    bottom.Add(item);
                }
            }

            var result = roles.Select(x => new RoleModel(x, roleRoutes, bottom));
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

        public async Task<ApiResponse> UpdateRoleAsync(RoleModel model)
        {
            // 全量的删除新增
            var role = await _role.FindAsync(x => x.RoleKey == model.RoleKey && !x.IsDelete);
            var verify = model.VerifyUpdate(role);
            if (verify == VerifyUpdateEnum.Fail) return Bad(model.VerifyMessgae);

            if (verify == VerifyUpdateEnum.Need)
            {
                role.Name = model.Name;
                role.Description = model.Description;
                await _role.UpdateAsync(role);
                if (await _role.SaveChangesAsync() < 1) return Bad("更新失败");
            }

            var row = await _roleRoute.RemoveRangeAsync(x => x.RoleId == role.Id && !x.IsDelete);
            var last = model.RouteKeys.Select(x => new RoleRouteEntity
            {
                RoleId = role.Id,
                RouteId = Convert.ToInt16(x)
            });
            await _roleRoute.AddRangeAsync(last);

            return Ok(await _roleRoute.SaveChangesAsync());
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