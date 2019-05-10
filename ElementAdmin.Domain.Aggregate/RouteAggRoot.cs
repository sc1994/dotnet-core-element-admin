using System.Collections.Generic;
using System.Linq;
using ElementAdmin.Domain.Entities.ElementAdminDb;
using Newtonsoft.Json;

namespace ElementAdmin.Domain.Aggregate
{
    public class RouteAggRoot : RoutesEntity
    {
        private readonly IEnumerable<RoutesEntity> _routes;
        private readonly IEnumerable<RolesRoutesEntity> _rolesRoutes;

        /// <summary>
        /// 初始化路由聚合根
        /// </summary>
        /// <param name="model"></param>
        /// <param name="routes"></param>
        /// <param name="rolesRoutes"></param>
        public RouteAggRoot(RoutesEntity model, IEnumerable<RoutesEntity> routes, IEnumerable<RolesRoutesEntity> rolesRoutes)
        {
            _routes = routes;
            _rolesRoutes = rolesRoutes;

            ParentKey = model.ParentKey;
            Name = model.Name;
            RouteKey = model.RouteKey;
            Sort = model.Sort;

            InitChildren();
            InitRoles();
        }

        /// <summary>
        /// 初始化路由聚合根
        /// </summary>
        /// <param name="key"></param>
        /// <param name="routes"></param>
        public RouteAggRoot(string routeKey, IEnumerable<RoutesEntity> routes)
        {
            _routes = routes;

            RouteKey = routeKey;
        }

        /// <summary>
        /// 子项
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<RouteAggRoot> Children { get; set; }

        /// <summary>
        /// 可以使用的角色
        /// </summary>
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// 初始化子项
        /// </summary>
        private void InitChildren()
        {
            var childs = _routes.Where(x => x.ParentKey == RouteKey);
            Children = childs.Select(x => new RouteAggRoot(x, _routes, _rolesRoutes));
        }

        /// <summary>
        /// 初始化角色
        /// </summary>
        private void InitRoles()
        {
            var roles = _rolesRoutes.Where(x => x.RouteKey == RouteKey);
            Roles = roles.Select(x => x.RoleKey);
        }

        /// <summary>
        /// 获取父级
        /// </summary>
        public void GetParentKeys(ref List<string> result)
        {
            var f = _routes.FirstOrDefault(x => x.ParentKey == RouteKey);
            if (!string.IsNullOrWhiteSpace(f.ParentKey))
            {
                new RouteAggRoot(f.ParentKey, _routes).GetParentKeys(ref result);
                result.Add(f.ParentKey);
            }
        }
    }
}