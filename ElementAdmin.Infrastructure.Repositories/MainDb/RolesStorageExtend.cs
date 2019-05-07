// =============系统自动生成=============
// 时间：2019/5/6 17:59
// 备注：简单的数据库操作方法，以及声明表结构。请勿在此文件中变动代码。
// =============系统自动生成=============

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ElementAdmin.Domain.Entities.MainDb;

namespace ElementAdmin.Infrastructure.Repositories.MainDb
{
    /// <summary></summary>
    public partial class RolesStorage : BaseStorage<MainDbContext, RolesEntity>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<RolesEntity> FirstOrDefaultAsync(Expression<Func<RolesEntity, bool>> predicate)
        {
            using (var context = new MainDbContext())
                return await context.RolesEntity.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<RolesEntity>> FindAsync(Expression<Func<RolesEntity, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new MainDbContext())
            {
                var query = context.RolesEntity.Where(predicate);
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
        public override async Task<List<RolesEntity>> FindAsync(RolesEntity predicate, int index = 0, int size = 0)
        {
            Expression<Func<RolesEntity, bool>> search = null;
            var defaultModel = new RolesEntity();

            if (defaultModel.Description != predicate.Description)
                search = x => x.Description == predicate.Description;
            if (defaultModel.Key != predicate.Key)
            {
                if (search == null)
                    search = x => x.Key == predicate.Key;
                else search = search.And(x => x.Key == predicate.Key);
            }
            if (defaultModel.Name != predicate.Name)
            {
                if (search == null)
                    search = x => x.Name == predicate.Name;
                else search = search.And(x => x.Name == predicate.Name);
            }
            if (search == null)
                search = x => x.Key.ToString() != ""; // 添加默认条件，不推荐，务必在查询时加上条件

            return await FindAsync(search, index, size);
        }
    }

    /// <summary>初始化  结构</summary>
    public partial class MainDbContext
    {
        /// <summary></summary>
        public virtual DbSet<RolesEntity> RolesEntity { get; set; }

        /// <summary></summary>
        protected void OnModelCreatingRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesEntity>(entity =>
            {
                entity.Property(e => e.Description)
                      .HasColumnName("Description")
                      .HasColumnType("varchar(255)");

                entity.HasKey(e => e.Key)
                      .HasName("PRIMARY");

                entity.Property(e => e.Key)
                      .HasColumnName("Key")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasColumnType("varchar(255)");
            });
        }
    }
}
