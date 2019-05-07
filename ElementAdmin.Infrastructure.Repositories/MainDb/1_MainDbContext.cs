// =============系统自动生成=============
// 时间：2019/5/6 17:55
// 备注：依次注入表结构到EF框架中。请勿在此文件中变动代码。
// =============系统自动生成=============

using Microsoft.EntityFrameworkCore;

namespace ElementAdmin.Infrastructure.Repositories.MainDb
{
    /// <inheritdoc />
    /// <summary>localhost</summary>
    public partial class MainDbContext : BaseDbContext<MainDbContext>
    {
        /// <summary>配置数据库地址</summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql("Server=10.101.72.6;Port=3307;Database=MainDb;User=root;Password=1qaz2wsx3edc;Min Pool Size=1;Max Pool Size=100;CharSet=utf8;SslMode=none;");
        }

        /// <summary>创建表结构</summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingRoles(modelBuilder);
            OnModelCreatingRoutes(modelBuilder); // 
            OnModelCreatingUserInfo(modelBuilder); // 
            OnModelCreatingRoleRoute(modelBuilder); // 
        }
    }
}
