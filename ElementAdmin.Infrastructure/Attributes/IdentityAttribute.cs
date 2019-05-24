using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ElementAdmin.Infrastructure.Attributes
{
    public class IdentityAttribute : ParameterInterceptorAttribute
    {
        public override async Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            var httpContext = context.AspectContext.ServiceProvider.GetService<IHttpContextAccessor>();
            var token = httpContext.HttpContext.Request.Headers["x-token"];
            httpContext.HttpContext.Response.StatusCode = 403;
            await httpContext.HttpContext.Response.WriteAsync("do not have permission");
            httpContext.HttpContext.Abort();
            context.AspectContext.Parameters[0] = new IdentityModel();
            await next(context);
        }
    }

    public class IdentityModel
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public string[] Roles { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 可访问的路由
        /// </summary>
        public string[] Routes { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; } = false;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteAt { get; set; }
    }
}
