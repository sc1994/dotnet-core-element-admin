using System.Security.Authentication;
using System.Linq;
using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using ElementAdmin.Infrastructure.Redis;
using ElementAdmin.Infrastructure.Redis.RedisConst;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace ElementAdmin.Infrastructure.Attributes
{
    public class IdentityAttribute : AbstractInterceptorAttribute
    {
        private readonly string[] _roles;
        public IdentityAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var httpContext = context.ServiceProvider.GetService<IHttpContextAccessor>();
            var config = context.ServiceProvider.GetService<IConfiguration>();
            var identity = await IdentityModelTools.GetIdentityModel(httpContext, config);
            if (identity != null)
            {
                if (!_roles.All(x => identity.Roles.Contains(x)))
                {
                    IdentityModelTools.NoAccessAsync(httpContext);
                }
                await next(context);
            }
        }
    }

    public class IdentityModelAttribute : ParameterInterceptorAttribute
    {
        public override async Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            var httpContext = context.AspectContext.ServiceProvider.GetService<IHttpContextAccessor>();
            var config = context.AspectContext.ServiceProvider.GetService<IConfiguration>();
            var identity = await IdentityModelTools.GetIdentityModel(httpContext, config);
            if (identity != null)
            {
                context.AspectContext.Parameters[0] = identity;
                await next(context);
            }
        }
    }

    internal class IdentityModelTools
    {
        public async static Task<IdentityModel> GetIdentityModel(IHttpContextAccessor httpContext, IConfiguration config)
        {
            var token = httpContext.HttpContext.Request.Headers["x-token"];
            if (string.IsNullOrWhiteSpace(token))
            {
                NoAccessAsync(httpContext);
            }

            var identity = await new RedisClient(config).StringGetAsync<IdentityModel>(UserConst.IdentityKey(token));
            if (identity == null)
            {
                NoAccessAsync(httpContext);
            }
            return identity;
        }

        public static void NoAccessAsync(IHttpContextAccessor httpContext)
        {
            var response = httpContext.HttpContext.Response;
            var message = Encoding.UTF8.GetBytes("do not have permission");
            response.OnStarting(async () =>
            {
                if (httpContext.HttpContext.Response.StatusCode == 403) return;
                httpContext.HttpContext.Response.StatusCode = 403;
                await response.Body.WriteAsync(message, 0, message.Length);
            });
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
