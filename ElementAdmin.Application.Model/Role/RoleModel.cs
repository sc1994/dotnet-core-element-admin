using System.Collections.Generic;
using System.Linq;
using ElementAdmin.Application.Model.Tools;
using ElementAdmin.Domain.Entity.ElementAdmin;

namespace ElementAdmin.Application.Model.Role
{
    public class RoleModel
    {
        public RoleModel() { }

        public RoleModel(
            RoleEntity entity,
            IEnumerable<RoleRouteEntity> roleRoutes,
            IEnumerable<RouteEntity> routes)
        {
            Id = entity.Id;
            RoleKey = entity.RoleKey;
            Name = entity.Name;
            Description = entity.Description;

            RouteKeys = roleRoutes
            .Where(x => x.RoleId == entity.Id)
            .Where(x => routes.Any(a => a.Id == x.RouteId))
            .Select(x => x.RouteId.ToString()).ToArray();
        }

        public long Id { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        /// <value></value>
        public string RoleKey { get; set; }

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
        public string[] RouteKeys { get; set; }

        /// <summary>
        /// 转到路由实体
        /// </summary>
        /// <returns></returns>
        public RoleEntity ToRoleEntity()
        {
            return new RoleEntity
            {
                Id = Id,
                RoleKey = RoleKey,
                Name = Name,
                Description = Description
            };
        }
    }
}