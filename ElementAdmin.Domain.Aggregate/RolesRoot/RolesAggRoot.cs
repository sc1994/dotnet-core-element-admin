using ElementAdmin.Domain.Entities.MainDb;
using ElementAdmin.Domain.ObjectValue;
using ElementAdmin.Infrastructure.Repositories.MainDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElementAdmin.Domain.Aggregate.RolesRoot
{
    public class RolesAggRoot : RolesObjVal
    {
        public RolesAggRoot() { }

        private readonly IRoutesStorage _routes;
        private readonly IRoleRouteStorage _roleRoute;

        public RolesAggRoot(IRoutesStorage routes, IRoleRouteStorage roleRoute, RolesEntity model) : base(model)
        {
            _routes = routes;
            _roleRoute = roleRoute;

            Routes = GetRoutes().Result;
        }


        public IEnumerable<RoutesAggRoot> Routes { get; set; }

        private async Task<IEnumerable<RoutesAggRoot>> GetRoutes()
        {
            var ids = await new RoutesAggRoot().GetRootsByRoleKey(Key);
            return ids.Select(x => new RoutesAggRoot(_routes, _roleRoute, x));
        }
    }
}
