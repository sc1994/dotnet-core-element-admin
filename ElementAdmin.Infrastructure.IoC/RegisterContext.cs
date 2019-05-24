using ElementAdmin.Infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace ElementAdmin.Infrastructure.IoC
{
    public class RegisterContext
    {
        public static void Register(IServiceCollection service)
        {
            service.AddScoped<ElementAdminContext>();
        }
    }
}
