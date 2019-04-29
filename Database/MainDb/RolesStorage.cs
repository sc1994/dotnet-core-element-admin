using Models.MainDb;

namespace Database.MainDb
{
    /// <summary>接口</summary>
    public interface IRolesStorage : IBaseStorage<RolesModel>
    {
    }

    /// <summary></summary>
    public partial class RolesStorage : IRolesStorage
    {
    }
}
