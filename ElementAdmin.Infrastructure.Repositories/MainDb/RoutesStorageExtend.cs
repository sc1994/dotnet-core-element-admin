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
    public partial class RoutesStorage : BaseStorage<MainDbContext, RoutesEntity>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<RoutesEntity> FirstOrDefaultAsync(Expression<Func<RoutesEntity, bool>> predicate)
        {
            using (var context = new MainDbContext())
                return await context.RoutesEntity.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<RoutesEntity>> FindAsync(Expression<Func<RoutesEntity, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new MainDbContext())
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

            if (defaultModel.AffixInt != predicate.AffixInt)
                search = x => x.AffixInt == predicate.AffixInt;
            if (defaultModel.BreadcrumbInt != predicate.BreadcrumbInt)
            {
                if (search == null)
                    search = x => x.BreadcrumbInt == predicate.BreadcrumbInt;
                else search = search.And(x => x.BreadcrumbInt == predicate.BreadcrumbInt);
            }
            if (defaultModel.Component != predicate.Component)
            {
                if (search == null)
                    search = x => x.Component == predicate.Component;
                else search = search.And(x => x.Component == predicate.Component);
            }
            if (defaultModel.HiddenInt != predicate.HiddenInt)
            {
                if (search == null)
                    search = x => x.HiddenInt == predicate.HiddenInt;
                else search = search.And(x => x.HiddenInt == predicate.HiddenInt);
            }
            if (defaultModel.Icon != predicate.Icon)
            {
                if (search == null)
                    search = x => x.Icon == predicate.Icon;
                else search = search.And(x => x.Icon == predicate.Icon);
            }
            if (defaultModel.Id != predicate.Id)
            {
                if (search == null)
                    search = x => x.Id == predicate.Id;
                else search = search.And(x => x.Id == predicate.Id);
            }
            if (defaultModel.Name != predicate.Name)
            {
                if (search == null)
                    search = x => x.Name == predicate.Name;
                else search = search.And(x => x.Name == predicate.Name);
            }
            if (defaultModel.ParentId != predicate.ParentId)
            {
                if (search == null)
                    search = x => x.ParentId == predicate.ParentId;
                else search = search.And(x => x.ParentId == predicate.ParentId);
            }
            if (defaultModel.Path != predicate.Path)
            {
                if (search == null)
                    search = x => x.Path == predicate.Path;
                else search = search.And(x => x.Path == predicate.Path);
            }
            if (defaultModel.Redirect != predicate.Redirect)
            {
                if (search == null)
                    search = x => x.Redirect == predicate.Redirect;
                else search = search.And(x => x.Redirect == predicate.Redirect);
            }
            if (defaultModel.Title != predicate.Title)
            {
                if (search == null)
                    search = x => x.Title == predicate.Title;
                else search = search.And(x => x.Title == predicate.Title);
            }
            if (search == null)
                search = x => x.Id.ToString() != ""; // 添加默认条件，不推荐，务必在查询时加上条件

            return await FindAsync(search, index, size);
        }
    }

    /// <summary>初始化  结构</summary>
    public partial class MainDbContext
    {
        /// <summary></summary>
        public virtual DbSet<RoutesEntity> RoutesEntity { get; set; }

        /// <summary></summary>
        protected void OnModelCreatingRoutes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoutesEntity>(entity =>
            {
                entity.Property(e => e.AffixInt)
                      .HasColumnName("AffixInt")
                      .HasColumnType("int(11)");

                entity.Property(e => e.BreadcrumbInt)
                      .HasColumnName("BreadcrumbInt")
                      .HasColumnType("int(11)");

                entity.Property(e => e.Component)
                      .HasColumnName("Component")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.HiddenInt)
                      .HasColumnName("HiddenInt")
                      .HasColumnType("int(11)");

                entity.Property(e => e.Icon)
                      .HasColumnName("Icon")
                      .HasColumnType("varchar(255)");

                entity.HasKey(e => e.Id)
                      .HasName("PRIMARY");

                entity.Property(e => e.Id)
                      .HasColumnName("Id")
                      .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.ParentId)
                      .HasColumnName("ParentId")
                      .HasColumnType("int(11)");

                entity.Property(e => e.Path)
                      .HasColumnName("Path")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Redirect)
                      .HasColumnName("Redirect")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Title)
                      .HasColumnName("Title")
                      .HasColumnType("varchar(255)");
            });
        }
    }
}
