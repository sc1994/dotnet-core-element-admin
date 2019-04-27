// =============系统自动生成=============
// 时间：2019/4/27 10:00
// 备注：依次注入表结构到EF框架中。请勿在此文件中变动代码。
// =============系统自动生成=============

using Microsoft.EntityFrameworkCore;

namespace Database.MainDb
{
    /// <inheritdoc />
    /// <summary>localhost</summary>
    public partial class MainDbContext : BaseContext<MainDbContext>
    {
        /// <summary>配置数据库地址</summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=MainDb;User=root;Password=1qaz2wsx3edc;Min Pool Size=1;Max Pool Size=100;CharSet=utf8;SslMode=none;");
        }

        /// <summary>创建表结构</summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingArticle(modelBuilder);
            OnModelCreatingRoles(modelBuilder); // 
            OnModelCreatingRoutes(modelBuilder); // 
            OnModelCreatingTransaction(modelBuilder); // 
            OnModelCreatingUserInfo(modelBuilder); // 
            OnModelCreatingRoleRoute(modelBuilder); // 
        }
    }
}