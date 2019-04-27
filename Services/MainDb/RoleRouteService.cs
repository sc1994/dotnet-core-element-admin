using Models.MainDb;
using Database.MainDb;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface IRoleRouteService : IBaseService<RoleRouteModel>
    {
    }

    /// <summary>服务</summary>
    public class RoleRouteService : BaseService<RoleRouteModel>, IRoleRouteService
    {
        private readonly IRoleRouteStorage _storage;

        /// <summary>服务</summary>
        public RoleRouteService(IRoleRouteStorage storage) : base(storage)
        {
            _storage = storage;
        }
    }
}