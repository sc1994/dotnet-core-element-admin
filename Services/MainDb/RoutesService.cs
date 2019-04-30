using Database.MainDb;
using Models.MainDb;
using Models.MainDbModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface IRoutesService : IBaseService<RoutesModel>
    {
        /// <summary>
        /// 设置节点的子项
        /// </summary>
        /// <param name="that"></param>
        /// <param name="allRoutes"></param>
        /// <param name="allRoleRoute"></param>
        void SetRouteChildren(RoutesView that, RoutesModel[] allRoutes, RoleRouteModel[] allRoleRoute = null);
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="that"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task InitRouteAsync(RoutesInitView that, int pid);
        /// <summary>
        /// 扁平数据
        /// </summary>
        /// <param name="view"></param>
        /// <param name="pid"></param>
        /// <param name="result"></param>
        void GetFlatRoute(RoutesView view, int pid, List<RoutesModel> result);
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

        public void SetRouteChildren(RoutesView that, RoutesModel[] allRoutes, RoleRouteModel[] allRoleRoute = null)
        {
            if (allRoleRoute != null)
            {
                var roles = allRoleRoute.Where(x => x.RouteId == that.Id)
                                        .Select(x => x.RoleKey)
                                        .ToArray();
                if (roles.Any())
                    if (that.Meta == null)
                        that.Meta = new MetaView
                        {
                            Roles = roles
                        };
                    else
                        that.Meta.Roles = roles;
            }

            var match = allRoutes.Where(x => x.ParentId == that.Id).Select(Mapper.ToExtend<RoutesView>).ToList();
            foreach (var item in match)
            {
                SetRouteChildren(item, allRoutes);
            }
            if (match.Count > 0) that.Children = match;
        }

        public void GetFlatRoute(RoutesView view, int pid, List<RoutesModel> result)
        {
            RoutesModel @base = view;
            @base.Title = view.Meta?.Title ?? "";
            @base.AffixInt = view.Meta?.Affix ?? false ? 1 : 0;
            @base.BreadcrumbInt = view.Meta?.Breadcrumb ?? false ? 1 : 0;
            @base.Icon = view.Meta?.Icon ?? "";
            @base.HiddenInt = view.Hidden ?? false ? 1 : 0;
            @base.ParentId = pid;
            result.Add(@base);
            if (view.Children?.Count > 0)
            {
                var id = @base.ParentId;
                foreach (var x in view.Children)
                {
                    GetFlatRoute(x, id, result);
                }
            }
        }

        public async Task InitRouteAsync(RoutesInitView that, int pid)
        {
            RoutesModel @base = that;
            @base.Title = that.Meta?.Title ?? "";
            @base.AffixInt = that.Meta?.Affix ?? false ? 1 : 0;
            @base.BreadcrumbInt = that.Meta?.Breadcrumb ?? false ? 1 : 0;
            @base.Icon = that.Meta?.Icon ?? "";
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