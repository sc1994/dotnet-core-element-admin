using Models.MainDb;
using Database.MainDb;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface IRolesService : IBaseService<RolesModel>
    {
    }

    /// <summary>服务</summary>
    public class RolesService : BaseService<RolesModel>, IRolesService
    {
        private readonly IRolesStorage _storage;

        /// <summary>服务</summary>
        public RolesService(IRolesStorage storage) : base(storage)
        {
            _storage = storage;
        }
    }
}