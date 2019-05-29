using System;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Application.Interface;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Identity;
using ElementAdmin.Domain.Entity.ElementAdmin;
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
            if (register.Username == "admin") // 默认用户
            {
                var token = Guid.NewGuid();
                await _redis.StringSetAsync(UserConst.IdentityKey(token.ToString()), new IdentityModel
                {
                    Avatar = "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2922170376,2371336021&fm=27&gp=0.jpg",
                    CreateAt = DateTime.Now,
                    Introduction = "我是默认管理员",
                    Name = "管理员",
                    Roles = new[] { "admin" },
                    Routes = new[] { "Permission", "RolePermission", "UserPermission" },
                    UpdateAt = DateTime.Now,
                    Username = "admin",
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
                Avatar = "http://img2.imgtn.bdimg.com/it/u=3937854204,4209154356&fm=11&gp=0.jpg",
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
    }
}