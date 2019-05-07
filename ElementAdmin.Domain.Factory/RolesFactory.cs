using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Domain.Aggregate.RolesRoot;
using ElementAdmin.Domain.Entities.MainDb;
using ElementAdmin.Infrastructure.Common.BaseClass;
using ElementAdmin.Infrastructure.Repositories.MainDb;
using Microsoft.EntityFrameworkCore;

namespace ElementAdmin.Domain.Factories
{
    public interface IRolesFactory
    {
        Task<RolesAggRoot> GetRoleByKeyAsync(string key);

        Task<IEnumerable<RolesAggRoot>> GetRolesAsync();

        Task<BaseResult<IEnumerable<RolesTreeAggRoot>>> GetRoleTreesAsync();

        Task<BaseResult> EditRoleAsync(RolesTreeAggRoot model);

        Task<BaseResult> AddRoleAsync(RolesTreeAggRoot model);
    }

    public class RolesFactory : BaseFactory, IRolesFactory
    {
        private readonly IRoleRouteStorage _roleRoute;
        private readonly IRoutesStorage _routes;
        private readonly IRolesStorage _roles;

        public RolesFactory(IRoleRouteStorage roleRoute, IRoutesStorage routes, IRolesStorage roles)
        {
            _routes = routes;
            _roleRoute = roleRoute;
            _roles = roles;
        }

        public async Task<BaseResult> AddRoleAsync(RolesTreeAggRoot model)
        {
            var role = await _roles.FirstOrDefaultAsync(x => x.Key == model.Key);
            if (role != null)return Bad("已存在的key");

            await _roles.AddAsync(new RolesEntity
            {
                Key = model.Name,
                    Name = model.Name,
                    Description = model.Description
            });

            var addModels = model.RouteIds.Select(x => new RoleRouteEntity
            {
                RoleKey = model.Key,
                    RouteId = x
            });

            await _roleRoute.AddRangeAsync(addModels);
            return Ok();
        }

        public async Task<BaseResult> EditRoleAsync(RolesTreeAggRoot model)
        {
            var role = await _roles.FirstOrDefaultAsync(x => x.Key == model.Key);
            if (role == null)return Bad("错误的角色Key");

            if (role.Name != model.Name)
                role.Name = model.Name;
            if (role.Description != model.Description)
                role.Description = model.Description;
            await _roles.UpdateAsync(role);

            var roleRoutes = await _roleRoute.FindAsync(x => x.RoleKey == model.Key);
            var deleteIds = roleRoutes.Select(x => x.RouteId).Except(model.RouteIds.Select(x => x));
            var deleteModels = roleRoutes.Where(x => deleteIds.Contains(x.RouteId));
            await _roleRoute.RemoveRangeAsync(deleteModels);

            var addIds = model.RouteIds.Select(x => x).Except(roleRoutes.Select(x => x.RouteId));
            var addModels = addIds.Select(x => new RoleRouteEntity
            {
                RoleKey = model.Key,
                    RouteId = x
            });
            await _roleRoute.AddRangeAsync(addModels);
            return Ok();

        }

        public async Task<RolesAggRoot> GetRoleByKeyAsync(string key)
        {
            var role = await _roles.FirstOrDefaultAsync(x => x.Key == key);
            if (role == null)throw new NullReferenceException(nameof(role));
            return new RolesAggRoot(_routes, _roleRoute, role);
        }

        public Task<IEnumerable<RolesAggRoot>> GetRolesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult<IEnumerable<RolesTreeAggRoot>>> GetRoleTreesAsync()
        {
            var roles = await _roles.FindAsync(x => !string.IsNullOrWhiteSpace(x.Key));
            var result = roles.Select(x => new RolesTreeAggRoot(x, _roleRoute));
            return Ok(result);
        }
    }
}