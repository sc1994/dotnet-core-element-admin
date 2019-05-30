using System;
using System.Threading.Tasks;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Identity;
using ElementAdmin.Infrastructure.Attributes;

namespace ElementAdmin.Application.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        Task<ApiResponse> LoginAsync(RegisterUserInfo register);

        /// <summary>
        /// 获取用户信息根据token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ApiResponse> GetUserInfoByTokenAsync(Guid token);

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<ApiResponse> LogoutAsync([Identity] IdentityModel identity = null);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        Task<ApiResponse> LogUpUserAsync(RegisterUserInfo register);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResponse> SearchUserAsync(SearchUserModel model);
    }
}