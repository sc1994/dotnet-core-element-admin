// =============系统自动生成=============
// 时间：2019/4/27 16:16
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
    public partial class UserInfoStorage : BaseStorage<MainDbContext, UserInfoModel>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<UserInfoModel> FirstOrDefaultAsync(Expression<Func<UserInfoModel, bool>> predicate)
        {
            using (var context = new MainDbContext())
                return await context.UserInfoModel.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<UserInfoModel>> FindAsync(Expression<Func<UserInfoModel, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new MainDbContext())
            {
                var query = context.UserInfoModel.Where(predicate);
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
        public override async Task<List<UserInfoModel>> FindAsync(UserInfoModel predicate, int index = 0, int size = 0)
        {
            Expression<Func<UserInfoModel, bool>> search = null;
            var defaultModel = new UserInfoModel();

            if (defaultModel.Id != predicate.Id)
                search = x => x.Id == predicate.Id;
            if (defaultModel.RolesString != predicate.RolesString)
            {
                if (search == null)
                    search = x => x.RolesString == predicate.RolesString;
                else search = search.And(x => x.RolesString == predicate.RolesString);
            }
            if (defaultModel.Token != predicate.Token)
            {
                if (search == null)
                    search = x => x.Token == predicate.Token;
                else search = search.And(x => x.Token == predicate.Token);
            }
            if (defaultModel.Introduction != predicate.Introduction)
            {
                if (search == null)
                    search = x => x.Introduction == predicate.Introduction;
                else search = search.And(x => x.Introduction == predicate.Introduction);
            }
            if (defaultModel.Avatar != predicate.Avatar)
            {
                if (search == null)
                    search = x => x.Avatar == predicate.Avatar;
                else search = search.And(x => x.Avatar == predicate.Avatar);
            }
            if (defaultModel.Name != predicate.Name)
            {
                if (search == null)
                    search = x => x.Name == predicate.Name;
                else search = search.And(x => x.Name == predicate.Name);
            }
            if (defaultModel.Username != predicate.Username)
            {
                if (search == null)
                    search = x => x.Username == predicate.Username;
                else search = search.And(x => x.Username == predicate.Username);
            }
            if (defaultModel.Password != predicate.Password)
            {
                if (search == null)
                    search = x => x.Password == predicate.Password;
                else search = search.And(x => x.Password == predicate.Password);
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
        public virtual DbSet<UserInfoModel> UserInfoModel { get; set; }

        /// <summary></summary>
        protected void OnModelCreatingUserInfo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfoModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                      .HasName("PRIMARY");

                entity.Property(e => e.Id)
                      .HasColumnName("Id")
                      .HasColumnType("int(11)");

                entity.Property(e => e.RolesString)
                      .HasColumnName("RolesString")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Token)
                      .HasColumnName("Token")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Introduction)
                      .HasColumnName("Introduction")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Avatar)
                      .HasColumnName("Avatar")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Username)
                      .HasColumnName("Username")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Password)
                      .HasColumnName("Password")
                      .HasColumnType("varchar(255)");
            });
        }
    }
}
