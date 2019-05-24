using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementAdmin.Domain.Entity.ElementAdmin
{
    /// <summary>
    /// 用户数据
    /// </summary>
    [Table("EAUserInfo")]
    public class UserInfoEntity : Entity<long>
    {
        public const int NameMaxLength = 255;
        public const int UserNameMaxLength = 16;
        public const int UserNameMinLength = 5;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 18;
        public const int IntroductionMaxLength = 256;
        public const int AvatarMaxLength = 256;

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(NameMaxLength)]
        [Required, Column("UINickName")]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        [MinLength(UserNameMinLength), MaxLength(UserNameMaxLength)]
        [Required, Column("UIUserName")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [MinLength(PasswordMinLength), MaxLength(PasswordMaxLength)]
        [Required, Column("UIPassword")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 用户角色，多角色已半角逗号区分
        /// </summary>
        [MaxLength(32)]
        [Required, Column("UIRolesString")]
        public string RolesString { get; set; } = string.Empty;

        /// <summary>
        /// 任何属性的变动都会导致此字段变更
        /// </summary>
        [MaxLength(64)]
        [Required, Column("UIToken")]
        public Guid Token { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(IntroductionMaxLength)]
        [Required, Column("UIIntroduction")]
        public string Introduction { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(AvatarMaxLength)]
        [Required, Column("UIAvatar")]
        public string Avatar { get; set; } = string.Empty;
    }
}
