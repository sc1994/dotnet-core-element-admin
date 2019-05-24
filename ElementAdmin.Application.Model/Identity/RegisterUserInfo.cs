using ElementAdmin.Domain.Entity.ElementAdmin;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ElementAdmin.Application.Model.Identity
{
    public class RegisterUserInfo
    {
        public RegisterUserInfo() { }

        public RegisterUserInfo(IHttpContextAccessor httpContext)
        {
            var token = httpContext.HttpContext.Request.Headers["x-token"];
        }

        public RegisterUserInfo(UserInfoEntity entity, string[] routes)
        {
            Avatar = entity.Avatar;
            Username = entity.UserName;
            Introduction = entity.Introduction;
            Name = entity.NickName;
            Roles = entity.RolesString.Split(',');
            Routes = routes;
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
        /// 密码
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Password { get; set; }

        /// <summary>
        /// 可访问的路由
        /// </summary>
        public string[] Routes { get; set; }
    }
}