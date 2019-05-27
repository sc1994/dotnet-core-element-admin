using System.ComponentModel.DataAnnotations.Schema;

namespace ElementAdmin.Domain.Entity.ElementAdmin
{
    /// <summary>
    /// 路由角色关系数据
    /// </summary>
    [Table("EARoleRoutes")]
    public class RoleRouteEntity : Entity<long>
    {
        /// <summary>
        /// 角色外键
        /// </summary>
        [Column("RRRoleId")]
        public long RoleId { get; set; }

        /// <summary>
        /// 路由外键
        /// </summary>
        [Column("RRRouteId")]
        public long RouteId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [ForeignKey("RRRoleId")]
        public RoleEntity Role { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        [ForeignKey("RRRouteId")]
        public RouteEntity Route { get; set; }
    }
}