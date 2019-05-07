using System.Linq;
using ElementAdmin.Domain.Entities.MainDb;
using ElementAdmin.Infrastructure.Repositories.MainDb;
using System.Threading.Tasks;

namespace ElementAdmin.Domain.Aggregate.RolesRoot
{
    public class RoutesMetaAggRoot
    {
        public RoutesMetaAggRoot() { }

        private readonly IRoleRouteStorage _roleRoute;

        public RoutesMetaAggRoot(IRoleRouteStorage roleRoute, RoutesEntity model)
        {
            _roleRoute = roleRoute;

            Roles = GetRoles(model.Id).Result;
            Breadcrumb = model.BreadcrumbInt == 1;
            Icon = model.Icon;
            //NoCache = model. todo 
            Title = model.Title;
        }

        public string[] Roles { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        //public bool NoCache { get; set; } todo
        public bool Breadcrumb { get; set; }

        private async Task<string[]> GetRoles(int id)
        {
            var roleRoute = await _roleRoute.FindAsync(x => x.RouteId == id);
            return roleRoute.Select(x => x.RoleKey).ToArray();
        }
    }
}
