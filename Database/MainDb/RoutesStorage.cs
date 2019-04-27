using Models.MainDb;

namespace Database.MainDb
{
    /// <summary>接口</summary>
    public interface IRoutesStorage : IBaseStorage<RoutesModel>
    {
    }

    /// <summary></summary>
    public partial class RoutesStorage : IRoutesStorage
    {
    }
}
