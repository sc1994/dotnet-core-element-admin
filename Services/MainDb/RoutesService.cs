using Database.MainDb;
using Models.MainDb;
using Models.MainDbModel;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface IRoutesService : IBaseService<RoutesModel>
    {
        void SetRouteChildren(RoutesView that, RoutesModel[] all);
        Task InitRouteAsync(RoutesInitView that, int pid);
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

        public async Task InitRouteAsync(RoutesInitView that, int pid)
        {
            RoutesModel @base = that;
            @base.Title = that.Meta?.Title ?? "";
            @base.AffixInt = that.Meta?.Affix ?? false ? 1 : 0;
            @base.BreadcrumbInt = that.Meta?.Breadcrumb ?? false ? 1 : 0;
            @base.Icon = that.Meta?.Icon ?? "";
            @base.Roles = string.Join(",", that.Meta?.Roles ?? new string[] { });
            @base.HiddenInt = that.Hidden ?? false ? 1 : 0;
            @base.ParentId = pid;
            var r = await _storage.AddAsync(@base);
            if (that.Children?.Count > 0)
            {
                var id = r.after.Id;
                foreach (var x in that.Children)
                {
                    await InitRouteAsync(x, id);
                }
            }
        }
    }
}