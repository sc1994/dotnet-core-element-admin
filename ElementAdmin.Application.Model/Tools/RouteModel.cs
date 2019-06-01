using System.Collections.Generic;
using System.Linq;
using ElementAdmin.Domain.Entity.ElementAdmin;

namespace ElementAdmin.Application.Model.Tools
{
    public class RouteModel
    {
        public string Name { get; set; }
        public RouteMetaModel Meta { get; set; }
        public string Path { get; set; }
        public string Redirect { get; set; }
        public RouteModel[] Children { get; set; }
        public int? Sort { get; set; }

        public RouteEntity[] ToRouteEntities()
        {
            var routeEntities = new List<RouteEntity>();
            FlatChildren("", this, ref routeEntities);
            return routeEntities.ToArray();
        }

        private void FlatChildren(string pKey, RouteModel item, ref List<RouteEntity> result)
        {
            if (item.Children?.Any() ?? false)
            {
                foreach (var x in item.Children)
                {
                    FlatChildren(item.Name, x, ref result);
                }
            }

            if (string.IsNullOrWhiteSpace(item.Name)) return;

            result.Add(new RouteEntity
            {
                Name = item.Meta?.Title ?? item.Name,
                ParentRouteKey = pKey,
                RouteKey = item.Name,
                Sort = item.Sort ?? result.Count
            });
        }
    }


    public class RouteMetaModel
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool? NoCache { get; set; }
        public string ActiveMenu { get; set; }
    }
}
