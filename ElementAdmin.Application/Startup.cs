using System;
using AspectCore.Configuration;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.AspectScope;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using ElementAdmin.Infrastructure.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using Swashbuckle.AspNetCore.Swagger;

namespace ElementAdmin.Application
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
            builder.AddEnvironmentVariables();

            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .File(new CompactJsonFormatter(),
                    "logs/log_.log",
                    rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
                //c.IncludeXmlComments("wwwroot/ElementAdmin.Application.Web.xml");
            });

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            RegisterDomain.Register(services);
            RegisterRepository.Register(services);
            RegisterContext.Register(services);

            var container = services.ToServiceContainer();

            RegisterLogger.Register(container);
            container.AddAspectScope();
            container.Configuration.Interceptors.AddTyped<EnableParameterAspectInterceptor>();
            return container.Build();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/api");

            app.UseDefaultFiles();
            app.UseStaticFiles();

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
        }
    }
}
