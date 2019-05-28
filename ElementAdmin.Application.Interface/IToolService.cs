using System.Threading.Tasks;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Tools;

namespace ElementAdmin.Application.Interface
{
    public interface IToolService
    {
        /// <summary>
        /// 初始化路由到数据库
        /// </summary>
        /// <returns></returns>
        Task<ApiResponse> InitRouteDataAsync(RouteDataModel[] routes);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        ApiResponse GetEntities();

        /// <summary>
        /// 初始化实体实现以及接口
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        ApiResponse InitEntities(string[] entities);
    }
}
