using ElementAdmin.Domain.Aggregate.UserInfoRoot;
using ElementAdmin.Domain.Context;
using ElementAdmin.Infrastructure.Common.BaseClass;
using ElementAdmin.Infrastructure.Repositories.MainDb;
using System;
using System.Threading.Tasks;

namespace ElementAdmin.Domain.Factories
{
    public interface ILoginFactory
    {
        Task<BaseResult<string>> UserLogin(LoginContext context);

        Task<BaseResult<UserInfoAggRoot>> GetUserInfo(string token);
    }

    public class LoginFactory : BaseFactory, ILoginFactory
    {

        private readonly IUserInfoStorage _userInfo;

        public LoginFactory(IUserInfoStorage userInfo)
        {
            _userInfo = userInfo;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<BaseResult<string>> UserLogin(LoginContext context)
        {
            var user = await _userInfo.FirstOrDefaultAsync(x => x.Username == context.Username && x.Password == context.Password);
            if (user == null) return Bad<string>("无效的token");

            user.Token = Guid.NewGuid().ToString();
            var update = await _userInfo.UpdateAsync(user);
            if (!update.done) return Bad<string>("更新数据失败");

            return Ok(user.Token);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BaseResult<UserInfoAggRoot>> GetUserInfo(string token)
        {
            var user = await _userInfo.FirstOrDefaultAsync(x => x.Token == token);
            if (user == null) return Bad<UserInfoAggRoot>("无效的token");

            return Ok(new UserInfoAggRoot(user));
        }
    }
}
