using ElementAdmin.Domain.Entities.ElementAdminDb;

namespace ElementAdmin.Infrastructure.Repositories.ElementAdminDb
{
    /// <summary>角色表接口</summary>
    public interface IRolesStorage : IBaseStorage<RolesEntity>
    {
    }

    /// <summary>角色表</summary>
    public partial class RolesStorage : IRolesStorage
    {
    }
}
