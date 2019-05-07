using ElementAdmin.Domain.Entities.MainDb;
using ElementAdmin.Infrastructure.Repositories.MainDb;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElementAdmin.Domain.Aggregate.RolesRoot
{
    public class RoutesAggRoot
    {
        public RoutesAggRoot() { }

        private readonly IRoutesStorage _routes;
        private readonly IRoleRouteStorage _roleRoute;

        public RoutesAggRoot(IRoutesStorage routes)
        {
            _routes = routes;
        }

        public RoutesAggRoot(IRoutesStorage routes, IRoleRouteStorage roleRoute, RoutesEntity model)
        {
            _routes = routes;
            _roleRoute = roleRoute;

            Component = model.Component;
            //AlwaysShow = model. todo
            Hidden = model.HiddenInt == 1;
            Meta = new RoutesMetaAggRoot(_roleRoute, model);
            Name = model.Name;
            Path = model.Path;
            Redirect = model.Redirect;
            Children = GetChildrens(model.Id).Result;
        }

        public string Component { get; set; }
        public string Path { get; set; }
        public bool Hidden { get; set; }
        public string Redirect { get; set; }
        //public bool AlwaysShow { get; set; } todo
        public string Name { get; set; }
        public RoutesMetaAggRoot Meta { get; set; }
        public IEnumerable<RoutesAggRoot> Children { get; set; }

        private async Task<IEnumerable<RoutesAggRoot>> GetChildrens(int pid)
        {
            var childs = await _routes.FindAsync(x => x.ParentId == pid);
            return childs.Select(x => new RoutesAggRoot(_routes, _roleRoute, x));
        }

        /// <summary>
        /// 获取全部根节点
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RoutesEntity>> GetRoots()
            => await _routes.FindAsync(x => x.ParentId == 0);

        /// <summary>
        /// 获取角色根节点id集合
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RoutesEntity>> GetRootsByRoleKey(string roleKey)
        {
            using (var context = new MainDbContext())
            {
                var routeIds = await context.RoleRouteEntity
                                            .Where(x => x.RoleKey == roleKey)
                                            .Select(x => x.RouteId)
                                            .ToListAsync();

                return await context.RoutesEntity
                                    .Where(x => routeIds.Contains(x.Id))
                                    .ToListAsync();
            }
        }
    }
}
