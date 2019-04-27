using System.Linq;
using Models.MainDb;
using Database.MainDb;
using Models.MainDbModel;
using Utils;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface IRoutesService : IBaseService<RoutesModel>
    {
        void SetRouteChildren(RoutesView that, RoutesModel[] all);
    }

    /// <summary>服务</summary>
    public class RoutesService : BaseService<RoutesModel>, IRoutesService
    {
        private readonly IRoutesStorage _storage;

        /// <summary>服务</summary>
        public RoutesService(IRoutesStorage storage) : base(storage)
        {
            _storage = storage;
        }

        public void SetRouteChildren(RoutesView that, RoutesModel[] all)
        {
            var match = all.Where(x => x.ParentId == that.Id).Select(Mapper.ToExtend<RoutesView>).ToList();
            foreach (var item in match)
            {
                SetRouteChildren(item, all);
            }
            that.Children = match;
        }
    }
}