using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Domain.Entities.MainDb;
using ElementAdmin.Domain.ObjectValue;
using ElementAdmin.Infrastructure.Repositories.MainDb;

namespace ElementAdmin.Domain.Aggregate.RolesRoot
{
    public class RolesTreeAggRoot : RolesObjVal
    {
        public RolesTreeAggRoot() {}

        private readonly IRoleRouteStorage _roleRoute;

        public RolesTreeAggRoot(RolesEntity model, IRoleRouteStorage roleRoute) : base(model)
        {
            _roleRoute = roleRoute;
            InitRouteIds().Wait();
        }

        public IEnumerable<int> RouteIds { get; set; }

        private async Task InitRouteIds()
        {
            var roleRoutes = await _roleRoute.FindAsync(x => x.RoleKey == Key);
            RouteIds = roleRoutes.Select(x => x.RouteId);
        }
    }
}