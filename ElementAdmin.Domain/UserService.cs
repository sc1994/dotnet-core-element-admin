using System;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Identity;
using ElementAdmin.Domain.Interface.ElementAdmin;
using ElementAdmin.Infrastructure.Attributes;
using static ElementAdmin.Application.Model.ApiResponse;

namespace ElementAdmin.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserInfoRepository _user;
        private readonly IRoleRouteRepository _roleRoute;

        public UserService(IUserInfoRepository user, IRoleRouteRepository roleRoute)
        {
            _user = user;
            _roleRoute = roleRoute;
        }

        public async Task<ApiResponse> GetUserInfoByTokenAsync(Guid token)
        {
            if (token == default) return Bad("参数错误");
            var user = await _user.FindAsync(x => x.Token == token && !x.IsDelete);
            if (user == default) return Bad("Token错误");

            var roles = user.RolesString.Split(',').ToList();
            var routes = await _roleRoute.WhereAsync(x => roles.Contains(x.Role.RoleKey));
            var result = new RegisterUserInfo(user, routes.Select(x => x.Route.Name).ToArray());
            return Ok(result);
        }

        public Task<ApiResponse> LogoutAsync(IdentityModel identity = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> LoginAsync(RegisterUserInfo register)
        {
            var user = await _user.FindAsync(x => x.UserName == register.Username && x.Password == register.Password);
            if (user.IsDelete) return Bad("此账号已被删除");

            user.Token = Guid.NewGuid();
            user.UpdateAt = DateTime.Now;
            var laterUser = await _user.UpdateAsync(user);
            var row = await _user.SaveChangesAsync();
            if (row != 1) return Bad("数据更新异常");

            return Ok<object>(new { token = laterUser.Entity.Token });
        }
    }
}