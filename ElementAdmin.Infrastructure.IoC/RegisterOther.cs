using ElementAdmin.Infrastructure.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace ElementAdmin.Infrastructure.IoC
{
    public class RegisterOther
    {
        public static void Register(IServiceCollection service)
        {
            service.AddScoped<IRedisClient, RedisClient>();
        }
    }
}
