using System;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Domain.Context;
using ElementAdmin.Domain.Entities.ElementAdminDb;
using ElementAdmin.Domain.ObjVal;
using ElementAdmin.Infrastructure.Repositories.ElementAdminDb;
using Valit;

namespace ElementAdmin.Domain.Aggregate
{
    public class UserInfoAggRoot : BaseResult
    {
        private readonly IUserInfoStorage _user;
        private readonly IRolesRoutesStorage _rolesRoutes;

        public UserInfoAggRoot(IUserInfoStorage user)
        {
            _user = user;
        }

        public UserInfoAggRoot(UserInfoEntity entity, IRolesRoutesStorage rolesRoutes)
        {
            _rolesRoutes = rolesRoutes;

            Avatar = entity.Avatar;
            Username = entity.Username;
            Introduction = entity.Introduction;
            Name = entity.Name;
            Roles = entity.RolesString.Split(',');

            InitRoutes().Wait();
        }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public string[] Roles { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 可访问的路由
        /// </summary>
        public string[] Routes { get; set; }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<Result<dynamic>> LoginAsync(UserLoginContext context)
        {
            var valit = ValitRules<UserLoginContext>
                .Create()
                .Ensure(x => x.Username, _ => _.Required().MinLength(1))
                .Ensure(x => x.Password, _ => _.Required().MinLength(6))
                .For(context)
                .Validate();
            if (!valit.Succeeded)return Bad<dynamic>("缺少必填参数");

            var first = await _user.FirstOrDefaultAsync(x =>
                x.Username == context.Username && x.Password == context.Password);
            if (first == null)return Bad<dynamic>("用户名或者密码错误");

            first.Token = Guid.NewGuid().ToString();
            await _user.UpdateAsync(first);

            return Ok<dynamic>(new { token = first.Token });
        }

        private async Task InitRoutes()
        {
            var routes = await _rolesRoutes.FindAsync(x => Roles.Contains(x.RoleKey));
            Routes = routes.Select(x => x.RouteKey).ToArray();
        }
    }
}