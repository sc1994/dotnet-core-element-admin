using System.Threading.Tasks;
using AspectCore.DynamicProxy;
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

        /// <summary>
        /// 搜索日志数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Identity("dev")]
        [NonAspect]
        Task<ApiResponse> SearchLogsAsync(ApiPageRequest<SearchModel> model);

        /// <summary>
        /// 搜索日志子项数据
        /// </summary>
        /// <param name="tracerId"></param>
        /// <returns></returns>
        [Identity("dev")]
        [NonAspect]
        Task<ApiResponse> SearchLogsChildAsync(string tracerId);

        /// <summary>
        /// 开始压测
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Identity("dev")]
        Task<ApiResponse> StartStressTestAsync(StressTestModel model);

        /// <summary>
        /// 停止压测
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        [Identity("dev")]
        ApiResponse AbortStressTest(string connectionId);
    }
}
