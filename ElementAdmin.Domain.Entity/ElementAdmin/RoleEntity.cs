using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementAdmin.Domain.Entity.ElementAdmin
{
    /// <summary>
    /// 角色数据
    /// </summary>
    [Table("EARoles")]
    public class RoleEntity : Entity<long>
    {
        public RoleEntity() { }
        
        public const int RoleKeyMinLength = 2;
        public const int RoleKeyMaxLength = 16;

        /// <summary>
        /// 路由key，不可重复
        /// </summary>
        [MinLength(RoleKeyMinLength), MaxLength(RoleKeyMaxLength)]
        [Required, Column("RRoleKey")]
        public string RoleKey { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(256)]
        [Required, Column("RName")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(512)]
        [Required, Column("RDescription")]
        public string Description { get; set; } = string.Empty;
    }
}
