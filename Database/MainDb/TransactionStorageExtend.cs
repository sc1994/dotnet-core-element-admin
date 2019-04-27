// =============系统自动生成=============
// 时间：2019/4/27 10:45
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
    public partial class TransactionStorage : BaseStorage<MainDbContext, TransactionModel>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<TransactionModel> FirstOrDefaultAsync(Expression<Func<TransactionModel, bool>> predicate)
        {
            using (var context = new MainDbContext())
                return await context.TransactionModel.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<TransactionModel>> FindAsync(Expression<Func<TransactionModel, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new MainDbContext())
            {
                var query = context.TransactionModel.Where(predicate);
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
        public override async Task<List<TransactionModel>> FindAsync(TransactionModel predicate, int index = 0, int size = 0)
        {
            Expression<Func<TransactionModel, bool>> search = null;
            var defaultModel = new TransactionModel();

            if (defaultModel.Id != predicate.Id)
                search = x => x.Id == predicate.Id;
            if (defaultModel.OrderNo != predicate.OrderNo)
            {
                if (search == null)
                    search = x => x.OrderNo == predicate.OrderNo;
                else search = search.And(x => x.OrderNo == predicate.OrderNo);
            }
            if (defaultModel.Timestamp != predicate.Timestamp)
            {
                if (search == null)
                    search = x => x.Timestamp == predicate.Timestamp;
                else search = search.And(x => x.Timestamp == predicate.Timestamp);
            }
            if (defaultModel.Username != predicate.Username)
            {
                if (search == null)
                    search = x => x.Username == predicate.Username;
                else search = search.And(x => x.Username == predicate.Username);
            }
            if (defaultModel.Price != predicate.Price)
            {
                if (search == null)
                    search = x => x.Price == predicate.Price;
                else search = search.And(x => x.Price == predicate.Price);
            }
            if (defaultModel.Status != predicate.Status)
            {
                if (search == null)
                    search = x => x.Status == predicate.Status;
                else search = search.And(x => x.Status == predicate.Status);
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
        public virtual DbSet<TransactionModel> TransactionModel { get; set; }

        /// <summary></summary>
        protected void OnModelCreatingTransaction(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                      .HasName("PRIMARY");

                entity.Property(e => e.Id)
                      .HasColumnName("Id")
                      .HasColumnType("int(11)");

                entity.Property(e => e.OrderNo)
                      .HasColumnName("OrderNo")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Timestamp)
                      .HasColumnName("Timestamp")
                      .HasColumnType("bigint(20)");

                entity.Property(e => e.Username)
                      .HasColumnName("Username")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Price)
                      .HasColumnName("Price")
                      .HasColumnType("decimal(10,10)");

                entity.Property(e => e.Status)
                      .HasColumnName("Status")
                      .HasColumnType("varchar(255)");
            });
        }
    }
}
