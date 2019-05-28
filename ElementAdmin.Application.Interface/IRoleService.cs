

using System.Threading.Tasks;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Role;

namespace ElementAdmin.Application.Interface
{
    public interface IRoleService
    {
        Task<ApiResponse> GetRolesAsync();

        Task<ApiResponse> AddRoleAsync(RoleModel model);

        Task<ApiResponse> UpdateRoleAsync(RoleModel model);

        Task<ApiResponse> DeleteRoleAsync(long id);

        Task<ApiResponse> GetRoutesAsync();
    }
}