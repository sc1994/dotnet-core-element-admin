using ElementAdmin.Domain.Aggregate;
using ElementAdmin.Domain.Context.UserInfoContext;
using ElementAdmin.Domain.ObjVal;
using ElementAdmin.Infrastructure.Repositories.ElementAdminDb;
using System.Threading.Tasks;

namespace ElementAdmin.Domain.Factories
{
    public interface IUserInfoFactory
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<Result<dynamic>> UserLoginAsync(UserLoginContext context);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Result<UserInfoContext>> GetUserInfoByTokenAsync(string token);
    }

    public class UserInfoFactory : IUserInfoFactory
    {
        private readonly IUserInfoStorage _user;
        private readonly IRolesRoutesStorage _rolesRoutes;

        public UserInfoFactory(IUserInfoStorage user, IRolesRoutesStorage rolesRoutes)
        {
            _user = user;
            _rolesRoutes = rolesRoutes;
        }

        public async Task<Result<dynamic>> UserLoginAsync(UserLoginContext context)
        {
            return await new UserInfoAggRoot().LoginAsync(context, _user);
        }

        public async Task<Result<UserInfoContext>> GetUserInfoByTokenAsync(string token)
        {
            var first = await _user.FirstOrDefaultAsync(x => x.Token == token);
            if (first == null)
            {
                return new Result<UserInfoContext>
                {
                    Code = ResultCodeEnum.失败,
                    Message = "token 错误"
                };
            }

            var result = await new UserInfoAggRoot().GetUserInfoContextAsync(first, _rolesRoutes);
            return new Result<UserInfoContext>
            {
                Data = result.UserInfoContext,
                Code = ResultCodeEnum.成功
            };
        }
    }
}