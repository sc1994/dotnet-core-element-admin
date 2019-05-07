using ElementAdmin.Domain.Entities.MainDb;

namespace ElementAdmin.Infrastructure.Repositories.MainDb
{
    /// <summary>接口</summary>
    public interface IRoleRouteStorage : IBaseStorage<RoleRouteEntity>
    {
    }

    /// <summary></summary>
    public partial class RoleRouteStorage : IRoleRouteStorage
    {
    }
}
