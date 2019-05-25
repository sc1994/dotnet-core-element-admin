using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Newtonsoft.Json;
using Serilog;
using AspectCore.Extensions.AspectScope;

namespace ElementAdmin.Infrastructure.Attributes
{
    public class LoggingAttribute : ScopeInterceptorAttribute
    {
        public override Scope Scope { get; set; } = Scope.None;

        /// <summary>
        /// 忽略参数（如果参数中包含了自循环，或者参数体积过大）
        /// </summary>
        public bool IgnoreParams { get; set; } = false;
        /// <summary>
        /// 忽略返回数据（如果数据中包含了自循环，或者数据体积过大）
        /// </summary>
        public bool IgnoreReturn { get; set; } = false;
        /// <summary>
        /// 忽略追踪（如果遇到性能问题）
        /// </summary>
        public bool IgnoreTracer { get; set; } = false;

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var time = DateTime.Now;
            var perf = new Dictionary<string, double>();

            var @params = context.Parameters.Select(x =>
            {
                if (x is Expression expression)
                {
                    return expression.ToString();
                }
                return x;
            }).ToArray();
            perf.Add("映射-context.Parameters", (DateTime.Now - time).TotalMilliseconds); time = DateTime.Now;
            try
            {
                await next(context); // 执行函数
                perf.Add("执行函数", (DateTime.Now - time).TotalMilliseconds); time = DateTime.Now;
            }
            catch (Exception e)
            {
                Log.Error(JsonConvert.SerializeObject(new LoggingModel
                {
                    TracerId = GetTraceId(context),
                    Parameters = @params,
                    Exception = e.ToString(),
                    MethodName = $"{context.ServiceMethod.DeclaringType}.{context.ServiceMethod.Name}"
                }));
                perf.Add("进入异常", (DateTime.Now - time).TotalMilliseconds); time = DateTime.Now;
                throw;
            }
            finally
            {
                var logContent = new LoggingModel
                {
                    TracerId = GetTraceId(context),
                    MethodName = $"{context.ServiceMethod.DeclaringType}.{context.ServiceMethod.Name}",
                    Parameters = @params
                };

                perf.Add("实例-LoggingModel", (DateTime.Now - time).TotalMilliseconds); time = DateTime.Now;
                if (context.ReturnValue != null)
                {
                    if (context.ReturnValue is Task
                        && new[] { "Task", "Task`1" }.Contains(context.ReturnValue.GetType().Name))
                    {
                        // 不做任何事情
                    }
                    else if (context.ReturnValue is Task)
                    {
                        var task = await context.UnwrapAsyncReturnValue();
                        logContent.ReturnValue = task;
                    }
                    else
                    {
                        logContent.ReturnValue = context.ReturnValue;
                    }

                }
                perf.Add("获取-ReturnValue", (DateTime.Now - time).TotalMilliseconds);
                logContent.Performance = perf;
                Log.Information(JsonConvert.SerializeObject(logContent));
            }
        }

        private string GetTraceId(AspectContext currentContext)
        {
            var scheduler = (IAspectScheduler)currentContext.ServiceProvider.GetService(typeof(IAspectScheduler));
            var firstContext = scheduler.GetCurrentContexts()?.FirstOrDefault();
            if (firstContext == null) return Guid.NewGuid().ToString();
            if (firstContext.AdditionalData.TryGetValue("trace-id", out var traceId))
            {
                return traceId.ToString();
            }
            traceId = Guid.NewGuid();
            firstContext.AdditionalData["trace-id"] = traceId;
            return traceId.ToString();
        }

    }

    public class LoggingModel
    {
        /// <summary>
        /// 追踪器
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TracerId { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object[] Parameters { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object ReturnValue { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Exception { get; set; }
        /// <summary>
        /// 性能
        /// </summary>
        public Dictionary<string, double> Performance { get; set; }
    }
}
