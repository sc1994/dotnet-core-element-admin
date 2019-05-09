using ElementAdmin.Domain.Aggregate;
using ElementAdmin.Domain.ObjVal;
using ElementAdmin.Infrastructure.Repositories.ElementAdminDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElementAdmin.Domain.Factories
{
    public interface IRoleFactory
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        Task<Result<IEnumerable<RoleAggRoot>>> GetRolesAsync();

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> AddRoleAsync(RoleAggRoot model);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateRoleAsync(string key, RoleAggRoot model);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Result> DeleteRoleAsync(string key);
    }

    public class RoleFactory : BaseResult, IRoleFactory
    {
        private readonly IRolesRoutesStorage _rolesRoutes;
        private readonly IRolesStorage _roles;

        public RoleFactory(IRolesRoutesStorage rolesRoutes, IRolesStorage roles)
        {
            _rolesRoutes = rolesRoutes;
            _roles = roles;
        }

        public async Task<Result<IEnumerable<RoleAggRoot>>> GetRolesAsync()
        {
            var roles = await _roles.FindAsync(x => !string.IsNullOrWhiteSpace(x.RoleKey));
            var result = roles.Select(x => new RoleAggRoot(_rolesRoutes, x));

            return Ok(result);
        }

        public async Task<Result> AddRoleAsync(RoleAggRoot model)
        {
            return await new RoleAggRoot(_rolesRoutes, _roles, model).AddAsync();
        }

        public async Task<Result> UpdateRoleAsync(string key, RoleAggRoot model)
        {
            return await new RoleAggRoot(_rolesRoutes, _roles, model).UpdateAsync();
        }

        public async Task<Result> DeleteRoleAsync(string key)
        {
            return await new RoleAggRoot(_rolesRoutes, _roles, key).DeleteAsync();
        }
    }
}
