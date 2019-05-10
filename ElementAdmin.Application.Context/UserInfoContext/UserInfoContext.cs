using ElementAdmin.Domain.Entities.ElementAdminDb;

namespace ElementAdmin.Domain.Context.UserInfoContext
{
    public class UserInfoContext
    {
        public UserInfoContext(UserInfoEntity entity)
        {
            Avatar = entity.Avatar;
            Username = entity.Username;
            Introduction = entity.Introduction;
            Name = entity.Name;
            Roles = entity.RolesString.Split(',');
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
    }
}
