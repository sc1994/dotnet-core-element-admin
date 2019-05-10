using ElementAdmin.Domain.Context.UserInfoContext;
using ElementAdmin.Domain.Entities.ElementAdminDb;
using ElementAdmin.Domain.ObjVal;
using ElementAdmin.Infrastructure.Repositories.ElementAdminDb;
using System;
using System.Linq;
using System.Threading.Tasks;
using Valit;

namespace ElementAdmin.Domain.Aggregate
{
    public class UserInfoAggRoot : BaseResult
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoContext UserInfoContext { get; set; }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Result<dynamic>> LoginAsync(UserLoginContext context, IUserInfoStorage user)
        {
            var valit = ValitRules<UserLoginContext>
                .Create()
                .Ensure(x => x.Username, _ => _.Required().MinLength(1))
                .Ensure(x => x.Password, _ => _.Required().MinLength(6))
                .For(context)
                .Validate();
            if (!valit.Succeeded) return Bad<dynamic>("缺少必填参数");

            var first = await user.FirstOrDefaultAsync(x =>
                x.Username == context.Username && x.Password == context.Password);
            if (first == null) return Bad<dynamic>("用户名或者密码错误");

            first.Token = Guid.NewGuid().ToString();
            await user.UpdateAsync(first);

            return Ok<dynamic>(new { token = first.Token });
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="rolesRoutes"></param>
        /// <returns></returns>
        public async Task<UserInfoAggRoot> GetUserInfoContextAsync(UserInfoEntity entity, IRolesRoutesStorage rolesRoutes)
        {
            UserInfoContext = new UserInfoContext(entity);
            var routes = await rolesRoutes.FindAsync(x => UserInfoContext.Roles.Contains(x.RoleKey));
            UserInfoContext.Routes = routes.Select(x => x.RouteKey).ToArray();
            return this;
        }
    }
}