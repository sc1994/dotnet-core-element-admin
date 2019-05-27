using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ElementAdmin.Infrastructure.IoC
{
    public class RegisterRepository
    {
        public static void Register(IServiceCollection service)
        {
            var @class = Assembly.Load("ElementAdmin.Infrastructure.Repository").GetTypes().Where(x => !x.IsGenericType);
            var interfaces = Assembly.Load("ElementAdmin.Domain.Interface").GetTypes();

            foreach (var type in @class)
            {
                var @interface = interfaces.FirstOrDefault(x => x.Name == "I" + type.Name);
                if (@interface != null)
                {
                    service.AddScoped(@interface, type);
                }
            }
        }
    }
}
