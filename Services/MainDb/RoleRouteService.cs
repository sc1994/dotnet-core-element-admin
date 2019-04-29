using Database.MainDb;
using Models.MainDb;
using System.Linq;
using System.Threading.Tasks;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface IRoleRouteService : IBaseService<RoleRouteModel>
    {
        Task InitRoleRoute();
    }

    /// <summary>服务</summary>
    public class RoleRouteService : BaseService<RoleRouteModel>, IRoleRouteService
    {
        private readonly IRoleRouteStorage _storage;
        private readonly IRoutesStorage _routesStorage;

        /// <summary>服务</summary>
        public RoleRouteService(IRoleRouteStorage storage, IRoutesStorage routesStorage) : base(storage)
        {
            _storage = storage;
            _routesStorage = routesStorage;
        }

        public async Task InitRoleRoute()
        {
            var all = await _routesStorage.FindAsync(x => x.Id > 0);
            var count = 0;

            foreach (var item in all)
            {
                await _storage.AddAsync(new RoleRouteModel
                {
                    RoleKey = "admin",
                    RouteId = item.Id
                });
                if (count % 2 == 0)
                {
                    await _storage.AddAsync(new RoleRouteModel
                    {
                        RoleKey = "editor",
                        RouteId = item.Id
                    });
                }
                if (count % 3 == 0)
                {
                    await _storage.AddAsync(new RoleRouteModel
                    {
                        RoleKey = "visitor",
                        RouteId = item.Id
                    });
                }
                count++;
            }
        }
    }
}