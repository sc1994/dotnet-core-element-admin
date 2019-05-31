using System;
using AspectCore.Configuration;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.AspectScope;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using ElementAdmin.Application.Hubs;
using ElementAdmin.Infrastructure.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
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

            // es 日志
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration.GetConnectionString("ElasticsearchConnection")))
                {
                    // IndexFormat = "element-admin-index-{yyyy.MM.dd.HH}",
                    // TemplateName = "default-template",
                    // TypeName = "method-invoke",
                    // MinimumLogEventLevel = LogEventLevel.Information,
                    AutoRegisterTemplate = true
                })
                .CreateLogger(); // todo es异常处理

            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
            });

            services.AddSignalR();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            RegisterDomain.Register(services);
            RegisterRepository.Register(services);
            RegisterContext.Register(services);
            RegisterOther.Register(services);
            RegisterAspect.Register(services);

            var container = services.ToServiceContainer();
            container.Configuration.Interceptors.AddTyped<EnableParameterAspectInterceptor>();
            container.AddAspectScope();

            return container.Build();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(x =>
                    x.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:9527")
                        .AllowCredentials());
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/api");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "API_V1");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();

            app.UseSignalR(builder =>
                {
                    builder.MapHub<StressTestHub>("/sth");
                });
        }
    }
}
