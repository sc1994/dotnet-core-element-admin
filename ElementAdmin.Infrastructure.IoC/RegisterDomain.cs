using ElementAdmin.Application.Interface;
using ElementAdmin.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace ElementAdmin.Infrastructure.IoC
{
    public class RegisterDomain
    {
        public static void Register(IServiceCollection service)
        {
            service.AddScoped<IToolService, ToolService>();
            service.AddScoped<IUserService, UserService>();
        }
    }
}
