using ElementAdmin.Domain.Entities.MainDb;

namespace ElementAdmin.Infrastructure.Repositories.MainDb
{
    /// <summary>接口</summary>
    public interface IRoutesStorage : IBaseStorage<RoutesEntity>
    {
    }

    /// <summary></summary>
    public partial class RoutesStorage : IRoutesStorage
    {
    }
}
