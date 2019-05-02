using System.Linq;
using System.Reflection;
using App;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace dotnet_core_element_admin
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddJsonOptions(option => option.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            Transfuse(services);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // 注入http请求的上下文
            //services.ConfigureDynamicProxy(config =>
            //                               {
            //                                   config.Interceptors.AddTyped<DataFilterAttribute>();
            //                               });
            //services.AddTransient(provider => new DataFilterAttribute());
            services.AddTransient<ICustomService, CustomService>();
            services.ConfigureDynamicProxy(config =>
                                           {
                                               config.Interceptors.AddTyped<CustomInterceptorAttribute>();
                                           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/api"); // 启用二级目录
            app.UseCors(x =>
                x.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowCredentials()); // 启用跨域

            app.UseDefaultFiles(); // 添加 默认文件 如 index.html  
            app.UseStaticFiles(); // 添加 静态文件夹，不传任何参数默认使用wwwroot文件夹 
            app.UseMvc();

        }

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="services"></param>
        public static void Transfuse(IServiceCollection services)
        {
            // 单例（Singleton）生命周期服务在它们第一次被请求时创建，或者如果你在 ConfigureServices运行时指定一个实例）
            // 并且每个后续请求将使用相同的实例。如果你的应用程序需要单例行为，
            // 建议让服务容器管理服务的生命周期而不是在自己的类中实现单例模式和管理对象的生命周期。
            // 参考文章 http://www.cnblogs.com/dotNETCoreSG/p/aspnetcore-3_10-dependency-injection.html

            var allTypes = Assembly.Load("Services").GetTypes().ToList();
            allTypes.AddRange(Assembly.Load("Database").GetTypes()); // 获取命名空间下的全部实例

            foreach (var type in allTypes)
            {
                if (type.GetTypeInfo().IsInterface)
                {
                    var instance = allTypes.FirstOrDefault(x => x.Name == type.Name.Substring(1, type.Name.Length - 1));
                    if (instance != null)
                    {
                        services.AddSingleton(type, instance); // 注入
                    }
                }
            }
        }
    }
}
