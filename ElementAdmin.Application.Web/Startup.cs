using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace ElementAdmin.Application.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            Transfuse(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UsePathBase("/api");

            app.UseCors(x =>
                            x.AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowAnyOrigin()
                             .AllowCredentials()); // 启用跨域

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }

        public void Transfuse(IServiceCollection services)
        {
            var types = Assembly.Load("ElementAdmin.Infrastructure.Repositories").GetTypes().ToList(); // 获取命名空间下的全部实例
            types.AddRange(Assembly.Load("ElementAdmin.Domain.Factories").GetTypes());

            foreach (var type in types)
            {
                if (type.GetTypeInfo().IsInterface)
                {
                    var instance = types.FirstOrDefault(x => x.Name == type.Name.Substring(1, type.Name.Length - 1));
                    if (instance != null)
                    {
                        services.AddSingleton(type, instance); // 注入
                    }
                }
            }
        }
    }
}
