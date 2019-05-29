
using System.Collections.Generic;
using ElementAdmin.Domain.Entity.ElementAdmin;

namespace ElementAdmin.Application.Model.Role
{
    /// <summary>
    /// 路由树
    /// </summary>
    public class RouteTreeModel
    {
        public RouteTreeModel() { }

        public RouteTreeModel(RouteEntity entity)
        {
            RouteKey = entity.RouteKey.ToString();
            Name = entity.Name;
        }

        public string RouteKey { get; set; }

        public string Name { get; set; }

        public List<RouteTreeModel> Children { get; set; } = new List<RouteTreeModel>();
    }
}