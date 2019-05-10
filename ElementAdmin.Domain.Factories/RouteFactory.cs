using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElementAdmin.Domain.Aggregate;
using ElementAdmin.Domain.ObjVal;
using ElementAdmin.Infrastructure.Repositories.ElementAdminDb;

namespace ElementAdmin.Domain.Factories
{
    public interface IRouteFactory
    {
        /// <summary>
        /// 获取全部路由
        /// </summary>
        /// <returns></returns>
        Task<Result<IEnumerable<RouteAggRoot>>> GetRoutesAsync();
    }

    public class RouteFactory : BaseResult, IRouteFactory
    {
        private readonly IRoutesStorage _routes;
        private readonly IRolesRoutesStorage _rolesRoutes;

        public RouteFactory(IRoutesStorage routes, IRolesRoutesStorage rolesRoutes)
        {
            _routes = routes;
            _rolesRoutes = rolesRoutes;
        }

        /// <summary>
        /// 获取路由
        /// </summary>
        /// <returns></returns>
        public async Task<Result<IEnumerable<RouteAggRoot>>> GetRoutesAsync()
        {
            var routes = await _routes.FindAsync(x => !string.IsNullOrWhiteSpace(x.RouteKey));
            var rolesRoutes = await _rolesRoutes.FindAsync(x => x.Id > 0);

            var root = routes
                .OrderBy(x => x.Sort)
                .Where(x => string.IsNullOrWhiteSpace(x.ParentKey));
            var result = root.Select(x => new RouteAggRoot(x, routes, rolesRoutes));

            return Ok<IEnumerable<RouteAggRoot>>(result);
        }
    }
}