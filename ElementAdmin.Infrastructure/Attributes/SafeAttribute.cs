using AspectCore.DynamicProxy;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Threading.Tasks;

namespace ElementAdmin.Infrastructure.Attributes
{
    public class SafeAttribute : AbstractInterceptorAttribute // todo  错误日志之类，safe会影响到异常排查
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                Log.Error(JsonConvert.SerializeObject(new LoggingModel
                {
                    Parameters = context.Parameters,
                    Exception = e.ToString(),
                    MethodName = $"{context.ServiceMethod.DeclaringType}.{context.ServiceMethod.Name}"
                }));
            }
        }
    }
}
