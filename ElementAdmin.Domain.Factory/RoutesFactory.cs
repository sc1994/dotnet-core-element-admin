using ElementAdmin.Domain.Aggregate.RolesRoot;
using ElementAdmin.Infrastructure.Common.BaseClass;
using ElementAdmin.Infrastructure.Repositories.MainDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElementAdmin.Domain.Factories
{
    public interface IRoutesFactory
    {
        Task<BaseResult<IEnumerable<RoutesAggRoot>>> GetRoutes();

        Task<BaseResult<IEnumerable<RoutesTreeAggRoot>>> GetRoutesTree();
    }

    public class RoutesFactory : BaseFactory, IRoutesFactory
    {
        private readonly IRoutesStorage _routes;
        private readonly IRoleRouteStorage _roleRoute;

        public RoutesFactory(IRoutesStorage routes, IRoleRouteStorage roleRoute)
        {
            _routes = routes;
            _roleRoute = roleRoute;
        }

        public async Task<BaseResult<IEnumerable<RoutesAggRoot>>> GetRoutes()
        {
            var roots = await new RoutesAggRoot(_routes).GetRoots();
            var result = roots.Select(x => new RoutesAggRoot(_routes, _roleRoute, x));
            return Ok(result);
        }

        public async Task<BaseResult<IEnumerable<RoutesTreeAggRoot>>> GetRoutesTree()
        {
            var roots = await _routes.FindAsync(x => x.ParentId == 0 && x.HiddenInt == 0);
            var result = roots.Select(x => new RoutesTreeAggRoot(_routes, _roleRoute, x));
            return Ok(result);
        }
    }
}
