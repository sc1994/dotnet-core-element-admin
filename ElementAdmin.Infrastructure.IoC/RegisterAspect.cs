using AspectCore.Configuration;
using System.Linq;
using ElementAdmin.Infrastructure.Attributes;
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
                // configure.NonAspectPredicates.AddService("*ToolService");
            });
        }
    }
}
