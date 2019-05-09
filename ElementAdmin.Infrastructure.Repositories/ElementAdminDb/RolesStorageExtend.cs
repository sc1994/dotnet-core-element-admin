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
    /// <summary>角色表</summary>
    public partial class RolesStorage : BaseStorage<ElementAdminDbContext, RolesEntity>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<RolesEntity> FirstOrDefaultAsync(Expression<Func<RolesEntity, bool>> predicate)
        {
            using (var context = new ElementAdminDbContext())
                return await context.RolesEntity.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<RolesEntity>> FindAsync(Expression<Func<RolesEntity, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new ElementAdminDbContext())
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
            if (defaultModel.Name != predicate.Name)
            {
                if (search == null)
                    search = x => x.Name == predicate.Name;
                else search = search.And(x => x.Name == predicate.Name);
            }
            if (defaultModel.RoleKey != predicate.RoleKey)
            {
                if (search == null)
                    search = x => x.RoleKey == predicate.RoleKey;
                else search = search.And(x => x.RoleKey == predicate.RoleKey);
            }
            if (search == null)
                search = x => x.RoleKey.ToString() != ""; // 添加默认条件，不推荐，务必在查询时加上条件

            return await FindAsync(search, index, size);
        }
    }

    /// <summary>初始化 角色表 结构</summary>
    public partial class ElementAdminDbContext
    {
        /// <summary>角色表</summary>
        public virtual DbSet<RolesEntity> RolesEntity { get; set; }

        /// <summary>角色表</summary>
        protected void OnModelCreatingRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesEntity>(entity =>
            {
                entity.Property(e => e.Description)
                      .HasColumnName("Description")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasColumnType("varchar(255)");

                entity.HasKey(e => e.RoleKey)
                      .HasName("PRIMARY");

                entity.Property(e => e.RoleKey)
                      .HasColumnName("RoleKey")
                      .HasColumnType("varchar(255)");
            });
        }
    }
}
