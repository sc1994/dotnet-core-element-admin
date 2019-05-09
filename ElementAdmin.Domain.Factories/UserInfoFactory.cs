using ElementAdmin.Domain.Aggregate;
using ElementAdmin.Domain.Context;
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
        Task<Result<UserInfoAggRoot>> GetUserinfoByTokenAsync(string token);
    }

    public class UserInfoFactory : IUserInfoFactory
    {
        private readonly IUserInfoStorage _user;

        public UserInfoFactory(IUserInfoStorage user)
        {
            _user = user;
        }

        public async Task<Result<dynamic>> UserLoginAsync(UserLoginContext context)
        {
            return await new UserInfoAggRoot(_user).LoginAsync(context);
        }

        public async Task<Result<UserInfoAggRoot>> GetUserinfoByTokenAsync(string token)
        {
            var first = await _user.FirstOrDefaultAsync(x => x.Token == token);
            if (first == null)
            {
                return new Result<UserInfoAggRoot>
                {
                    Code = ResultCodeEnum.失败,
                    Message = "token 错误"
                };
            }

            var result = new UserInfoAggRoot(first);
            return new Result<UserInfoAggRoot>
            {
                Data = result,
                Code = ResultCodeEnum.成功
            };
        }
    }
}
