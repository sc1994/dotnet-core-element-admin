using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElementAdmin.Domain.Entity.ElementAdmin
{
    /// <summary>
    /// 路由数据
    /// </summary>
    [Table("EARoutes")]
    public class RouteEntity : Entity<long>
    {
        /// <summary>
        /// 路由key，不能重复
        /// </summary>
        [MaxLength(256)]
        [Required, Column("RRouteKey")]
        public string RouteKey { get; set; } = string.Empty;

        /// <summary>
        /// 父级路由Key
        /// </summary>
        [MaxLength(256)]
        [Required, Column("RParentKey")]
        public string ParentRouteKey { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(256)]
        [Required, Column("RName")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 排序值
        /// </summary>
        [Required, Column("RSort")]
        public int Sort { get; set; } = 0;
    }
}
