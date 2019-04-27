using Models.MainDb;

namespace Database.MainDb
{
    /// <summary>接口</summary>
    public interface IRoleRouteStorage : IBaseStorage<RoleRouteModel>
    {
    }

    /// <summary></summary>
    public partial class RoleRouteStorage : IRoleRouteStorage
    {
    }
}
