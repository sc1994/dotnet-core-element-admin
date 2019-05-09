using ElementAdmin.Domain.Aggregate;
using ElementAdmin.Domain.Entities.ElementAdminDb;
using ElementAdmin.Domain.Factories;
using ElementAdmin.Domain.ObjVal;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElementAdmin.Application.Web.Controllers
{
    /// <summary>
    /// 角色
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RoleController
    {
        private readonly IRoleFactory _role;

        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="role"></param>
        public RoleController(IRoleFactory role)
        {
            _role = role;
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetRoles()
        {
            var r = await _role.GetRolesAsync();
            return r;
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> AddRole(RoleAggRoot model)
        {
            return await _role.AddRoleAsync(model);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{key}")]
        public async Task<Result> UpdateRole(string key, RoleAggRoot model)
        {
            return await _role.UpdateRoleAsync(key, model);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete("{key}")]
        public async Task<Result> DeleteRole(string key)
        {
            return await _role.DeleteRoleAsync(key);
        }
    }
}