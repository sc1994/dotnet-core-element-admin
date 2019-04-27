using Models.MainDb;
using Database.MainDb;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface IRoutesService : IBaseService<RoutesModel>
    {
    }

    /// <summary>服务</summary>
    public class RoutesService : BaseService<RoutesModel>, IRoutesService
    {
        /// <summary>服务</summary>
        public RoutesService(IRoutesStorage storage) : base(storage)
        { }
    }
}