using AspectCore.DynamicProxy;
using System.Threading.Tasks;

namespace ElementAdmin.Infrastructure.Attributes
{
    public class SafeAttribute : AbstractInterceptorAttribute // todo  错误日志之类，safe会影响到异常排查
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            await next(context);
        }
    }
}
