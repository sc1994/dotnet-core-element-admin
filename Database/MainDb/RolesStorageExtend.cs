// =============系统自动生成=============
// 时间：2019/4/27 10:00
// 备注：简单的数据库操作方法，以及声明表结构。请勿在此文件中变动代码。
// =============系统自动生成=============

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models.MainDb;

namespace Database.MainDb
{
    /// <summary></summary>
    public partial class RolesStorage : BaseStorage<MainDbContext, RolesModel>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<RolesModel> FirstOrDefaultAsync(Expression<Func<RolesModel, bool>> predicate)
        {
            using (var context = new MainDbContext())
                return await context.RolesModel.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<RolesModel>> FindAsync(Expression<Func<RolesModel, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new MainDbContext())
            {
                var query = context.RolesModel.Where(predicate);
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
        public override async Task<List<RolesModel>> FindAsync(RolesModel predicate, int index = 0, int size = 0)
        {
            Expression<Func<RolesModel, bool>> search = null;
            var defaultModel = new RolesModel();

            if (defaultModel.Key != predicate.Key)
                search = x => x.Key == predicate.Key;
            if (defaultModel.Name != predicate.Name)
            {
                if (search == null)
                    search = x => x.Name == predicate.Name;
                else search = search.And(x => x.Name == predicate.Name);
            }
            if (defaultModel.Description != predicate.Description)
            {
                if (search == null)
                    search = x => x.Description == predicate.Description;
                else search = search.And(x => x.Description == predicate.Description);
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
        public virtual DbSet<RolesModel> RolesModel { get; set; }

        /// <summary></summary>
        protected void OnModelCreatingRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesModel>(entity =>
            {
                entity.HasKey(e => e.Key)
                      .HasName("PRIMARY");

                entity.Property(e => e.Key)
                      .HasColumnName("Key")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Description)
                      .HasColumnName("Description")
                      .HasColumnType("varchar(255)");
            });
        }
    }
}
