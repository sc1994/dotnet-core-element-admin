using System.Reflection;
using AspectCore.Configuration;
using System.Linq;
using AspectCore.Extensions.AspectScope;
using AspectCore.Injector;
using ElementAdmin.Infrastructure.Attributes;
using System;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ElementAdmin.Infrastructure.IoC
{
    public class RegisterAspect
    {
        static string[] logsNameSpace = new[]
        {
            "ElementAdmin.Application.Interface",
            "ElementAdmin.Domain.Interface",
        };

        public static void Register(IServiceCollection service)
        {
            service.ConfigureDynamicProxy(configure =>
            {
                configure.Interceptors.AddTyped<LoggerAttribute>(
                    predicates =>
                       logsNameSpace.Any(x => x.StartsWith(predicates.DeclaringType.Namespace)));
            });
        }
    }
}
