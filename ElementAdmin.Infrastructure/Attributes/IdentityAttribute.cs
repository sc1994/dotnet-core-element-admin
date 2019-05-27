using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using ElementAdmin.Infrastructure.Redis;
using ElementAdmin.Infrastructure.Redis.RedisConst;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElementAdmin.Infrastructure.Attributes
{
    public class IdentityAttribute : ParameterInterceptorAttribute
    {
        public override async Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            var httpContext = context.AspectContext.ServiceProvider.GetService<IHttpContextAccessor>();
            var config = context.AspectContext.ServiceProvider.GetService<IConfiguration>();
            var token = httpContext.HttpContext.Request.Headers["x-token"];
            if (string.IsNullOrWhiteSpace(token))
            {
                await NoAccessAsync(httpContext);
            }

            var identity = await new RedisClient(config).StringGetAsync<IdentityModel>(UserConst.IdentityKey(token));
            if (identity == null)
            {
                await NoAccessAsync(httpContext);
            }
            context.AspectContext.Parameters[0] = identity;
            await next(context);
        }

        private async Task NoAccessAsync(IHttpContextAccessor http)
        {
            http.HttpContext.Response.StatusCode = 403;
            await http.HttpContext.Response.WriteAsync("do not have permission");
            http.HttpContext.Abort();
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
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
    }
}
