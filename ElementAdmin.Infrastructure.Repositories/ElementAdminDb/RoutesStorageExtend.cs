// =============系统自动生成=============
// 时间：2019/5/9 17:12
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
    /// <summary>路由表</summary>
    public partial class RoutesStorage : BaseStorage<ElementAdminDbContext, RoutesEntity>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<RoutesEntity> FirstOrDefaultAsync(Expression<Func<RoutesEntity, bool>> predicate)
        {
            using (var context = new ElementAdminDbContext())
                return await context.RoutesEntity.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<RoutesEntity>> FindAsync(Expression<Func<RoutesEntity, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new ElementAdminDbContext())
            {
                var query = context.RoutesEntity.Where(predicate);
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
        public override async Task<List<RoutesEntity>> FindAsync(RoutesEntity predicate, int index = 0, int size = 0)
        {
            Expression<Func<RoutesEntity, bool>> search = null;
            var defaultModel = new RoutesEntity();

            if (defaultModel.Name != predicate.Name)
                search = x => x.Name == predicate.Name;
            if (defaultModel.ParentKey != predicate.ParentKey)
            {
                if (search == null)
                    search = x => x.ParentKey == predicate.ParentKey;
                else search = search.And(x => x.ParentKey == predicate.ParentKey);
            }
            if (defaultModel.RouteKey != predicate.RouteKey)
            {
                if (search == null)
                    search = x => x.RouteKey == predicate.RouteKey;
                else search = search.And(x => x.RouteKey == predicate.RouteKey);
            }
            if (defaultModel.Sort != predicate.Sort)
            {
                if (search == null)
                    search = x => x.Sort == predicate.Sort;
                else search = search.And(x => x.Sort == predicate.Sort);
            }
            if (search == null)
                search = x => x.RouteKey.ToString() != ""; // 添加默认条件，不推荐，务必在查询时加上条件

            return await FindAsync(search, index, size);
        }
    }

    /// <summary>初始化 路由表 结构</summary>
    public partial class ElementAdminDbContext
    {
        /// <summary>路由表</summary>
        public virtual DbSet<RoutesEntity> RoutesEntity { get; set; }

        /// <summary>路由表</summary>
        protected void OnModelCreatingRoutes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoutesEntity>(entity =>
            {
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.ParentKey)
                      .HasColumnName("ParentKey")
                      .HasColumnType("varchar(255)");

                entity.HasKey(e => e.RouteKey)
                      .HasName("PRIMARY");

                entity.Property(e => e.RouteKey)
                      .HasColumnName("RouteKey")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Sort)
                      .HasColumnName("Sort")
                      .HasColumnType("int(11)")
                      .HasDefaultValueSql("'0'");
            });
        }
    }
}
