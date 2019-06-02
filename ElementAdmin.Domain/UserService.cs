using System;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Identity;
using ElementAdmin.Domain.Entity.ElementAdmin;
using ElementAdmin.Domain.Interface.ElementAdmin;
using ElementAdmin.Infrastructure;
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
            if (register.Username == AppConst.AdminUsername
                && register.Password == AppConst.AdminPassword) // 默认用户
            {
                var token = Guid.NewGuid();
                await _redis.StringSetAsync(UserConst.IdentityKey(token.ToString()), new IdentityModel
                {
                    Avatar = "data:image/jpeg;base64,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",
                    CreateAt = DateTime.Now,
                    Introduction = "我是默认管理员",
                    Name = "管理员",
                    Roles = new[] { "admin" },
                    Routes = new[] { "Permission", "RolePermission", "UserPermission", "Developer", "Tools", "ToolOther" },
                    UpdateAt = DateTime.Now,
                    Username = AppConst.AdminUsername,
                    Token = token.ToString()
                }, DateTime.Today.AddDays(7) - DateTime.Now);
                return Ok<object>(new { token = token });
            }

            if (!register.VerifyLogin()) return Bad(register.VerifyMessage);

            var user = await _user.FindAsync(x => x.UserName == register.Username && x.Password == register.Password);
            if (user == null) return Bad("用户名或者密码错误");
            if (user.IsDelete) return Bad("此账号已被删除");

            user.Token = Guid.NewGuid();
            user.UpdateAt = DateTime.Now;
            var laterUser = await _user.UpdateAsync(user);
            var row = await _user.SaveChangesAsync();
            if (row != 1) return Bad("数据更新异常");

            var roleKeys = user.RolesString.Split(',');
            var roleRoutes = await _roleRoute.WhereAsync(x => roleKeys.Contains(x.RoleKey) && !x.IsDelete);
            var routeKeys = roleRoutes.Select(x => x.RouteKey);
            var routes = await _route.WhereAsync(x => routeKeys.Contains(x.RouteKey) && !x.IsDelete);

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

        public async Task<ApiResponse> LogUpUserAsync(RegisterUserInfo register)
        {
            var flag = await _user.FindAsync(x => x.UserName == register.Username && !x.IsDelete);
            if (!register.VerifyLogUp(flag)) return Bad(register.VerifyMessage);

            await _user.AddAsync(new UserInfoEntity
            {
                Avatar = "data:image/jpeg;base64,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",
                UserName = register.Username,
                NickName = register.Username,
                Password = register.Password,
                RolesString = "base",
                Token = Guid.NewGuid(),
                Introduction = "初来乍到"
            });
            if (await _user.SaveChangesAsync() > 0)
                return Ok();
            else
                return Bad("注册异常");
        }

        public Task<ApiResponse> SearchUserAsync(SearchUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}