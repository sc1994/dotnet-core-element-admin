using AspectCore.Configuration;
using System.Linq;
using AspectCore.Extensions.AspectScope;
using AspectCore.Injector;
using ElementAdmin.Infrastructure.Attributes;

namespace ElementAdmin.Infrastructure.IoC
{
    public class RegisterAspect
    {
        public static void Register(IServiceContainer service)
        {
            var logsNameSpace = new[]
            {
                "ElementAdmin.Application.Interface",
                "ElementAdmin.Domain.Interface",
            };

            service.Configure(configure =>
            {
                configure.Interceptors.AddTyped<LoggingAttribute>(predicates => logsNameSpace.Any(x => x.StartsWith(predicates.DeclaringType.Namespace)));
            });
            service.AddAspectScope();
        }
    }
}
