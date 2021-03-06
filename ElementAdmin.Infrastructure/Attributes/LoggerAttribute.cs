﻿using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.AspectScope;
using Serilog;

namespace ElementAdmin.Infrastructure.Attributes
{
    public class LoggerAttribute : ScopeInterceptorAttribute
    {
        public override Scope Scope { get; set; } = Scope.None;

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var startTime = DateTime.Now.Ticks;
            var time = DateTime.Now;
            var perf = new Dictionary<string, double>();

            var (traceId, isMain) = GetTraceId(context);
            perf.Add("获取-TraceId", (DateTime.Now - time).TotalMilliseconds); time = DateTime.Now;

            var @params = context.Parameters.Select(x =>
            {
                if (x is Expression expression)
                {
                    return expression.ToString();
                }
                return x;
            }).ToArray();
            perf.Add("映射-Parameters", (DateTime.Now - time).TotalMilliseconds); time = DateTime.Now;
            try
            {
                await next(context); // 执行函数
                perf.Add("执行函数", (DateTime.Now - time).TotalMilliseconds); time = DateTime.Now;
            }
            catch (Exception e)
            {
                perf.Add("进入异常", (DateTime.Now - time).TotalMilliseconds); time = DateTime.Now;
                await LogThat(context, time, isMain, perf, e, startTime, traceId, @params);
                throw;
            }
            await LogThat(context, time, isMain, perf, null, startTime, traceId, @params);
        }

        private (string traceId, bool isMain) GetTraceId(AspectContext currentContext)
        {
            var scheduler = (IAspectScheduler)currentContext.ServiceProvider.GetService(typeof(IAspectScheduler));
            var firstContext = scheduler.GetCurrentContexts()?.FirstOrDefault();
            if (firstContext == null) return (Guid.NewGuid().ToString(), true);
            if (firstContext.AdditionalData.TryGetValue("trace-id", out var traceId))
            {
                return (traceId.ToString(), true);
            }
            traceId = Guid.NewGuid();
            firstContext.AdditionalData["trace-id"] = traceId;
            return (traceId.ToString(), false);
        }

        private async Task LogThat(
            AspectContext context,
            DateTime time,
            bool isMain,
            Dictionary<string, double> perf,
            Exception ex,
            long startTime,
            string traceId,
            object[] @params
        )
        {
            object returnValue = null;
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
                    returnValue = task;
                }
                else
                {
                    returnValue = context.ReturnValue;
                }
            }
            perf.Add("获取-ReturnValue", (DateTime.Now - time).TotalMilliseconds); time = DateTime.Now;

            var template = !isMain ?
                               "Invoke({start_timestamp},{tracer_id},{full_method},{method},{parameters},{return_value},{performance},{error})" :
                               "InvokeChild({start_timestamp},{tracer_id},{full_method},{method},{parameters},{return_value},{performance},{error})";
            var property = new object[]
            {
                startTime,
                traceId,
                $"{context.ServiceMethod.DeclaringType}.{context.ServiceMethod.Name}",
                $"{context.ServiceMethod.DeclaringType.Name}.{context.ServiceMethod.Name}",
                @params.ToJson(),
                returnValue.ToJson() // todo 尝试转成字典，一方面能解决自我嵌套的问题，但是反射的性能可能较差，或许可以静态缓存
            };

            perf.Add("初始化-logMessgae", (DateTime.Now - time).TotalMilliseconds);

            if (ex == null)
            {
                property = property.Append(perf).Append(null).ToArray();
                Log.Information(template, property);
            }
            else
            {
                property = property.Append(perf).Append(ex).ToArray();
                Log.Error(template, property);
            }
        }
    }
}
