using ElementAdmin.Domain.Entity.ElementAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ElementAdmin.Infrastructure.Data.Context
{
    public class ElementAdminContext : DbContext
    {
        private readonly IHostingEnvironment _env;

        public ElementAdminContext(IHostingEnvironment env)
        {
            _env = env;
        }

        public ElementAdminContext() { }

        /// <summary>
        /// 用户数据
        /// </summary>
        public DbSet<UserInfoEntity> UserInfoEntity { get; set; }

        /// <summary>
        /// 角色数据
        /// </summary>
        public DbSet<RoleEntity> RoleEntity { get; set; }

        /// <summary>
        /// 路由数据
        /// </summary>
        public DbSet<RouteEntity> RouteEntity { get; set; }

        /// <summary>
        /// 角色路由关系数据
        /// </summary>
        public DbSet<RoleRouteEntity> RoleRouteEntity { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot config;
            if (_env == null)
            {
                config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
            else
            {
                config = new ConfigurationBuilder()
                    .SetBasePath(_env.ContentRootPath)
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
            optionsBuilder.UseMySql(config.GetConnectionString("ElementAdminConnection"));
        }
    }
}
