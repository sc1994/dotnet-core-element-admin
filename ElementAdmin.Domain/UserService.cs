using System;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Identity;
using ElementAdmin.Domain.Interface.ElementAdmin;
using ElementAdmin.Infrastructure.Attributes;
using ElementAdmin.Infrastructure.Redis;
using ElementAdmin.Infrastructure.Redis.RedisConst;
using static ElementAdmin.Application.Model.ApiResponse;

namespace ElementAdmin.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserInfoRepository _user;
        private readonly IRoleRouteRepository _roleRoute;
        private readonly IRedisClient _redis;
        private readonly IRoleRepository _role;
        private readonly IRouteRepository _route;

        public UserService(
            IUserInfoRepository user,
            IRoleRouteRepository roleRoute,
            IRedisClient redis,
            IRoleRepository role,
            IRouteRepository route)
        {
            _user = user;
            _roleRoute = roleRoute;
            _redis = redis;
            _role = role;
            _route = route;
        }

        public async Task<ApiResponse> GetUserInfoByTokenAsync(Guid token)
        {
            if (token == default) return Bad("Token错误");

            var identity = await _redis.StringGetAsync<IdentityModel>(UserConst.IdentityKey(token.ToString()));
            var result = new RegisterUserInfo(identity);
            return Ok(result);
        }

        public async Task<ApiResponse> LogoutAsync(IdentityModel identity = null)
        {
            if (identity == null) return Bad("");
            await _redis.KeyDelete(UserConst.IdentityKey(identity.Token));
            return Ok();
        }

        public async Task<ApiResponse> LoginAsync(RegisterUserInfo register)
        {
            var user = await _user.FindAsync(x => x.UserName == register.Username && x.Password == register.Password);
            if (user == null) return Bad("用户名或者密码错误");
            if (user.IsDelete) return Bad("此账号已被删除");

            user.Token = Guid.NewGuid();
            user.UpdateAt = DateTime.Now;
            var laterUser = await _user.UpdateAsync(user);
            var row = await _user.SaveChangesAsync();
            if (row != 1) return Bad("数据更新异常");

            var roleKeys = user.RolesString.Split(',');
            var roles = await _role.WhereAsync(x => roleKeys.Contains(x.RoleKey));
            var roleIds = roles.Select(x => x.Id);
            var roleRoutes = await _roleRoute.WhereAsync(x => roleIds.Contains(x.RoleId) && !x.IsDelete);
            var routeIds = roleRoutes.Select(x => x.RouteId);
            var routes = await _route.WhereAsync(x => routeIds.Contains(x.Id) && !x.IsDelete);

            await _redis.StringSetAsync(UserConst.IdentityKey(laterUser.Entity.Token.ToString()), new IdentityModel
            {
                Avatar = user.Avatar,
                CreateAt = user.CreateAt,
                Introduction = user.Introduction,
                Name = user.NickName,
                Roles = roleKeys,
                Routes = routes.Select(x => x.RouteKey).ToArray(),
                UpdateAt = user.UpdateAt,
                Username = user.UserName,
                Token = laterUser.Entity.Token.ToString()
            }, DateTime.Today.AddDays(7) - DateTime.Now);
            return Ok<object>(new { token = laterUser.Entity.Token });
        }
    }
}