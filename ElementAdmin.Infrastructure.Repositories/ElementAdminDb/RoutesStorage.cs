using ElementAdmin.Domain.Entities.ElementAdminDb;

namespace ElementAdmin.Infrastructure.Repositories.ElementAdminDb
{
    /// <summary>路由表接口</summary>
    public interface IRoutesStorage : IBaseStorage<RoutesEntity>
    {
    }

    /// <summary>路由表</summary>
    public partial class RoutesStorage : IRoutesStorage
    {
    }
}
