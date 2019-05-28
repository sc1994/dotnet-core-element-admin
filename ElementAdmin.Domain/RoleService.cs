using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Role;
using ElementAdmin.Domain.Entity.ElementAdmin;
using ElementAdmin.Domain.Interface.ElementAdmin;

namespace ElementAdmin.Domain
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _role;
        private readonly IRoleRouteRepository _roleRoute;

        public RoleService(IRoleRepository role, IRoleRouteRepository roleRoute)
        {
            _role = role;
            _roleRoute = roleRoute;
        }

        public async Task<ApiResponse> AddRoleAsync(RoleModel model)
        {
            await _role.AddAsync(model.ToRoleEntity());
_roleRoute.AddRangeAsync
        }

        public Task<ApiResponse> DeleteRoleAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> GetRolesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> GetRoutesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> UpdateRoleAsync(RoleModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}