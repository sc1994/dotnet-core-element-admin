using System.Threading.Tasks;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Tools;
using ElementAdmin.Infrastructure.Attributes;

namespace ElementAdmin.Application.Interface
{
    public interface IToolService
    {
        /// <summary>
        /// 初始化路由到数据库
        /// </summary>
        /// <returns></returns>
        [Identity("dev")]
        Task<ApiResponse> InitRouteDataAsync(RouteModel[] routes);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [Identity("dev")]
        ApiResponse GetEntities();

        /// <summary>
        /// 初始化实体实现以及接口
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [Identity("dev")]
        ApiResponse InitEntities(string[] entities);
    }
}
