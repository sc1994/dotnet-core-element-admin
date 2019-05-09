using ElementAdmin.Domain.Entities.ElementAdminDb;

namespace ElementAdmin.Infrastructure.Repositories.ElementAdminDb
{
    /// <summary>路由角色多对多关系表接口</summary>
    public interface IRolesRoutesStorage : IBaseStorage<RolesRoutesEntity>
    {
    }

    /// <summary>路由角色多对多关系表</summary>
    public partial class RolesRoutesStorage : IRolesRoutesStorage
    {
    }
}
