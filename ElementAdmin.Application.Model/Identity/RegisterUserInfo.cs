using ElementAdmin.Domain.Entity.ElementAdmin;
using ElementAdmin.Infrastructure.Attributes;
using Newtonsoft.Json;

namespace ElementAdmin.Application.Model.Identity
{
    public class RegisterUserInfo
    {
        public RegisterUserInfo() { }

        public RegisterUserInfo(IdentityModel model)
        {
            Avatar = model.Avatar;
            Username = model.Username;
            Introduction = model.Introduction;
            Name = model.Name;
            Roles = model.Roles;
            Routes = model.Routes;
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

        /// <summary>
        /// 验证
        /// </summary>
        public string VerifyMessage { get; set; }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <value></value>
        public bool VerifyLogin()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                VerifyMessage = "用户名不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                VerifyMessage = "密码不能为空";
                return false;
            }
            if (Username.Length < UserInfoEntity.UserNameMinLength || Username.Length > UserInfoEntity.UserNameMaxLength)
            {
                VerifyMessage = $"用户名长度必须在{UserInfoEntity.UserNameMinLength}到{UserInfoEntity.UserNameMaxLength}之间";
                return false;
            }
            if (Password.Length < UserInfoEntity.PasswordMinLength || Password.Length > UserInfoEntity.PasswordMaxLength)
            {

                VerifyMessage = $"密码长度必须在{UserInfoEntity.UserNameMinLength}到{UserInfoEntity.UserNameMaxLength}之间";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 注册验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool VerifyLogUp(UserInfoEntity entity)
        {
            if (entity != null)
            {
                VerifyMessage = "已存在的用户名";
                return false;
            }
            return VerifyLogin();
        }
    }
}