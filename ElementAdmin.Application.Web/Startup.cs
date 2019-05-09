using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using System.Reflection;

namespace ElementAdmin.Application.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
                //c.IncludeXmlComments("wwwroot/ElementAdmin.Application.Web.xml");
            });

            Transfuse(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "API_V1");
            });

            app.UseCors(x =>
                x.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials());

            app.UseMvc();

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="services"></param>
        public void Transfuse(IServiceCollection services)
        {
            var types = Assembly.Load("ElementAdmin.Infrastructure.Repositories").GetTypes().ToList(); // ��ȡ�����ռ��µ�ȫ��ʵ��
            types.AddRange(Assembly.Load("ElementAdmin.Domain.Factories").GetTypes());

            foreach (var type in types)
            {
                if (type.GetTypeInfo().IsInterface)
                {
                    var instance = types.FirstOrDefault(x => x.Name == type.Name.Substring(1, type.Name.Length - 1));
                    if (instance != null)
                    {
                        services.AddSingleton(type, instance); // ע��
                    }
                }
            }

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
