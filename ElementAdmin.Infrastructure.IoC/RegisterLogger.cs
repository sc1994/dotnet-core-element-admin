using System.Linq;
using AspectCore.Configuration;
using AspectCore.Injector;
using ElementAdmin.Infrastructure.Attributes;

namespace ElementAdmin.Infrastructure.IoC
{
    public class RegisterLogger
    {
        public static void Register(IServiceContainer service)
        {
            var all = new[] { "ElementAdmin.Application.Interface", "ElementAdmin.Domain.Interface" };
            service.Configure(configure =>
            {
                configure.Interceptors.AddTyped<LoggingAttribute>(predicates => all.Contains(predicates.DeclaringType.Namespace));
            });
        }
    }
}
