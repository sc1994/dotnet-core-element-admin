// =============系统自动生成=============
// 时间：2019/4/29 15:11
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
    public partial class RoleRouteStorage : BaseStorage<MainDbContext, RoleRouteModel>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<RoleRouteModel> FirstOrDefaultAsync(Expression<Func<RoleRouteModel, bool>> predicate)
        {
            using (var context = new MainDbContext())
                return await context.RoleRouteModel.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<RoleRouteModel>> FindAsync(Expression<Func<RoleRouteModel, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new MainDbContext())
            {
                var query = context.RoleRouteModel.Where(predicate);
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
        public override async Task<List<RoleRouteModel>> FindAsync(RoleRouteModel predicate, int index = 0, int size = 0)
        {
            Expression<Func<RoleRouteModel, bool>> search = null;
            var defaultModel = new RoleRouteModel();

            if (defaultModel.Id != predicate.Id)
                search = x => x.Id == predicate.Id;
            if (defaultModel.RoleKey != predicate.RoleKey)
            {
                if (search == null)
                    search = x => x.RoleKey == predicate.RoleKey;
                else search = search.And(x => x.RoleKey == predicate.RoleKey);
            }
            if (defaultModel.RouteId != predicate.RouteId)
            {
                if (search == null)
                    search = x => x.RouteId == predicate.RouteId;
                else search = search.And(x => x.RouteId == predicate.RouteId);
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
        public virtual DbSet<RoleRouteModel> RoleRouteModel { get; set; }

        /// <summary></summary>
        protected void OnModelCreatingRoleRoute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleRouteModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                      .HasName("PRIMARY");

                entity.Property(e => e.Id)
                      .HasColumnName("Id")
                      .HasColumnType("int(11)");

                entity.Property(e => e.RoleKey)
                      .IsRequired()
                      .HasColumnName("RoleKey")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.RouteId)
                      .IsRequired()
                      .HasColumnName("RouteId")
                      .HasColumnType("int(11)");
            });
        }
    }
}
