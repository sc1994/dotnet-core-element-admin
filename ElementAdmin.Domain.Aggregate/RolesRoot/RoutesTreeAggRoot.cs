using ElementAdmin.Domain.Entities.MainDb;
using ElementAdmin.Infrastructure.Repositories.MainDb;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElementAdmin.Domain.Aggregate.RolesRoot
{
    public class RoutesTreeAggRoot
    {
        private readonly IRoutesStorage _routes;
        private readonly IRoleRouteStorage _roleRoute;

        public RoutesTreeAggRoot(IRoutesStorage routes, IRoleRouteStorage roleRoute, RoutesEntity model)
        {
            _routes = routes;
            _roleRoute = roleRoute;

            Id = model.Id;
            Label = model.Title;
            Children = GetChildrens().Result;
        }

        public int Id;
        public string Label;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<RoutesTreeAggRoot> Children { get; set; }

        private async Task<IEnumerable<RoutesTreeAggRoot>> GetChildrens()
        {
            var childs = await _routes.FindAsync(x => x.ParentId == Id && x.HiddenInt == 0);
            return childs.Select(x => new RoutesTreeAggRoot(_routes, _roleRoute, x));
        }
    }
}
