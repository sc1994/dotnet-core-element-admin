using System.Linq;
using ElementAdmin.Application.Model.Tools;
using ElementAdmin.Domain.Entity.ElementAdmin;

namespace ElementAdmin.Application.Model.Role
{
    public class RoleModel
    {
        /// <summary>
        /// 角色
        /// </summary>
        /// <value></value>
        public string Key { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <value></value>
        public string Description { get; set; }

        /// <summary>
        /// 路由权限
        /// </summary>
        /// <value></value>
        public RouteDataModel[] Routes { get; set; }

        /// <summary>
        /// 转到路由实体
        /// </summary>
        /// <returns></returns>
        public RoleEntity ToRoleEntity()
        {
            return new RoleEntity
            {
                RoleKey = Key,
                Name = Name,
                Description = Description
            };
        }

        public RouteEntity[] ToRouteEntities()
        {
            return Routes.Select(x => new RouteEntity
            {
                RouteKey = x.Name,

            }).ToArray();
        }
    }
}