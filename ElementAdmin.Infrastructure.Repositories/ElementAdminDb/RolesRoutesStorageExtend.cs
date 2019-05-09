// =============系统自动生成=============
// 时间：2019/5/9 14:25
// 备注：简单的数据库操作方法，以及声明表结构。请勿在此文件中变动代码。
// =============系统自动生成=============

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ElementAdmin.Domain.Entities.ElementAdminDb;

namespace ElementAdmin.Infrastructure.Repositories.ElementAdminDb
{
    /// <summary>路由角色多对多关系表</summary>
    public partial class RolesRoutesStorage : BaseStorage<ElementAdminDbContext, RolesRoutesEntity>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<RolesRoutesEntity> FirstOrDefaultAsync(Expression<Func<RolesRoutesEntity, bool>> predicate)
        {
            using (var context = new ElementAdminDbContext())
                return await context.RolesRoutesEntity.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<RolesRoutesEntity>> FindAsync(Expression<Func<RolesRoutesEntity, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new ElementAdminDbContext())
            {
                var query = context.RolesRoutesEntity.Where(predicate);
                if (index > 0 && size > 0)
                    query = query.Skip((index - 1) * size).Take(size);
                return await query.ToListAsync();
            }
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<RolesRoutesEntity>> FindAsync(RolesRoutesEntity predicate, int index = 0, int size = 0)
        {
            Expression<Func<RolesRoutesEntity, bool>> search = null;
            var defaultModel = new RolesRoutesEntity();

            if (defaultModel.Id != predicate.Id)
                search = x => x.Id == predicate.Id;
            if (defaultModel.RoleKey != predicate.RoleKey)
            {
                if (search == null)
                    search = x => x.RoleKey == predicate.RoleKey;
                else search = search.And(x => x.RoleKey == predicate.RoleKey);
            }
            if (defaultModel.RouteKey != predicate.RouteKey)
            {
                if (search == null)
                    search = x => x.RouteKey == predicate.RouteKey;
                else search = search.And(x => x.RouteKey == predicate.RouteKey);
            }
            if (search == null)
                search = x => x.Id.ToString() != ""; // 添加默认条件，不推荐，务必在查询时加上条件

            return await FindAsync(search, index, size);
        }
    }

    /// <summary>初始化 路由角色多对多关系表 结构</summary>
    public partial class ElementAdminDbContext
    {
        /// <summary>路由角色多对多关系表</summary>
        public virtual DbSet<RolesRoutesEntity> RolesRoutesEntity { get; set; }

        /// <summary>路由角色多对多关系表</summary>
        protected void OnModelCreatingRolesRoutes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesRoutesEntity>(entity =>
            {
                entity.HasKey(e => e.Id)
                      .HasName("PRIMARY");

                entity.Property(e => e.Id)
                      .HasColumnName("Id")
                      .HasColumnType("int(11)");

                entity.Property(e => e.RoleKey)
                      .HasColumnName("RoleKey")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.RouteKey)
                      .HasColumnName("RouteKey")
                      .HasColumnType("varchar(255)");
            });
        }
    }
}
