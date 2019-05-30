using System.Threading.Tasks;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Role;
using ElementAdmin.Infrastructure.Attributes;

namespace ElementAdmin.Application.Interface
{
    public interface IRoleService
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        [Identity("admin")]
        Task<ApiResponse> GetRolesAsync();

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Identity("admin")]
        Task<ApiResponse> AddRoleAsync(RoleModel model);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Identity("admin")]
        Task<ApiResponse> UpdateRoleAsync(RoleModel model);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Identity("admin")]
        Task<ApiResponse> DeleteRoleAsync(long id);

        /// <summary>
        /// 获取路由
        /// </summary>
        /// <returns></returns>
        Task<ApiResponse> GetRoutesAsync();
    }
}