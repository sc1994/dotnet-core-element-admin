using ElementAdmin.Domain.Entities.MainDb;

namespace ElementAdmin.Infrastructure.Repositories.MainDb
{
    /// <summary>接口</summary>
    public interface IRolesStorage : IBaseStorage<RolesEntity>
    {
    }

    /// <summary></summary>
    public partial class RolesStorage : IRolesStorage
    {
    }
}
