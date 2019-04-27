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
    public partial class ArticleStorage : BaseStorage<MainDbContext, ArticleModel>
    {
        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public override async Task<ArticleModel> FirstOrDefaultAsync(Expression<Func<ArticleModel, bool>> predicate)
        {
            using (var context = new MainDbContext())
                return await context.ArticleModel.FirstOrDefaultAsync(predicate);
        }

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public override async Task<List<ArticleModel>> FindAsync(Expression<Func<ArticleModel, bool>> predicate, int index = 0, int size = 0)
        {
            using (var context = new MainDbContext())
            {
                var query = context.ArticleModel.Where(predicate);
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
        public override async Task<List<ArticleModel>> FindAsync(ArticleModel predicate, int index = 0, int size = 0)
        {
            Expression<Func<ArticleModel, bool>> search = null;
            var defaultModel = new ArticleModel();

            if (defaultModel.Id != predicate.Id)
                search = x => x.Id == predicate.Id;
            if (defaultModel.Importance != predicate.Importance)
            {
                if (search == null)
                    search = x => x.Importance == predicate.Importance;
                else search = search.And(x => x.Importance == predicate.Importance);
            }
            if (defaultModel.Remark != predicate.Remark)
            {
                if (search == null)
                    search = x => x.Remark == predicate.Remark;
                else search = search.And(x => x.Remark == predicate.Remark);
            }
            if (defaultModel.Timestamp != predicate.Timestamp)
            {
                if (search == null)
                    search = x => x.Timestamp == predicate.Timestamp;
                else search = search.And(x => x.Timestamp == predicate.Timestamp);
            }
            if (defaultModel.Title != predicate.Title)
            {
                if (search == null)
                    search = x => x.Title == predicate.Title;
                else search = search.And(x => x.Title == predicate.Title);
            }
            if (defaultModel.Status != predicate.Status)
            {
                if (search == null)
                    search = x => x.Status == predicate.Status;
                else search = search.And(x => x.Status == predicate.Status);
            }
            if (defaultModel.Type != predicate.Type)
            {
                if (search == null)
                    search = x => x.Type == predicate.Type;
                else search = search.And(x => x.Type == predicate.Type);
            }
            if (defaultModel.Author != predicate.Author)
            {
                if (search == null)
                    search = x => x.Author == predicate.Author;
                else search = search.And(x => x.Author == predicate.Author);
            }
            if (defaultModel.CommentDisabled != predicate.CommentDisabled)
            {
                if (search == null)
                    search = x => x.CommentDisabled == predicate.CommentDisabled;
                else search = search.And(x => x.CommentDisabled == predicate.CommentDisabled);
            }
            if (defaultModel.Content != predicate.Content)
            {
                if (search == null)
                    search = x => x.Content == predicate.Content;
                else search = search.And(x => x.Content == predicate.Content);
            }
            if (defaultModel.ContentShort != predicate.ContentShort)
            {
                if (search == null)
                    search = x => x.ContentShort == predicate.ContentShort;
                else search = search.And(x => x.ContentShort == predicate.ContentShort);
            }
            if (defaultModel.DisplayTime != predicate.DisplayTime)
            {
                if (search == null)
                    search = x => x.DisplayTime == predicate.DisplayTime;
                else search = search.And(x => x.DisplayTime == predicate.DisplayTime);
            }
            if (defaultModel.Forecast != predicate.Forecast)
            {
                if (search == null)
                    search = x => x.Forecast == predicate.Forecast;
                else search = search.And(x => x.Forecast == predicate.Forecast);
            }
            if (defaultModel.ImageUri != predicate.ImageUri)
            {
                if (search == null)
                    search = x => x.ImageUri == predicate.ImageUri;
                else search = search.And(x => x.ImageUri == predicate.ImageUri);
            }
            if (defaultModel.Pageviews != predicate.Pageviews)
            {
                if (search == null)
                    search = x => x.Pageviews == predicate.Pageviews;
                else search = search.And(x => x.Pageviews == predicate.Pageviews);
            }
            if (defaultModel.Platforms != predicate.Platforms)
            {
                if (search == null)
                    search = x => x.Platforms == predicate.Platforms;
                else search = search.And(x => x.Platforms == predicate.Platforms);
            }
            if (defaultModel.Reviewer != predicate.Reviewer)
            {
                if (search == null)
                    search = x => x.Reviewer == predicate.Reviewer;
                else search = search.And(x => x.Reviewer == predicate.Reviewer);
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
        public virtual DbSet<ArticleModel> ArticleModel { get; set; }

        /// <summary></summary>
        protected void OnModelCreatingArticle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                      .HasName("PRIMARY");

                entity.Property(e => e.Id)
                      .HasColumnName("Id")
                      .HasColumnType("int(11)");

                entity.Property(e => e.Importance)
                      .HasColumnName("Importance")
                      .HasColumnType("int(11)");

                entity.Property(e => e.Remark)
                      .HasColumnName("Remark")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Timestamp)
                      .HasColumnName("Timestamp")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Title)
                      .HasColumnName("Title")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                      .HasColumnName("Status")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Type)
                      .HasColumnName("Type")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Author)
                      .HasColumnName("Author")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.CommentDisabled)
                      .HasColumnName("CommentDisabled")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Content)
                      .HasColumnName("Content")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.ContentShort)
                      .HasColumnName("ContentShort")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.DisplayTime)
                      .HasColumnName("DisplayTime")
                      .HasColumnType("datetime");

                entity.Property(e => e.Forecast)
                      .HasColumnName("Forecast")
                      .HasColumnType("decimal(10,10)");

                entity.Property(e => e.ImageUri)
                      .HasColumnName("ImageUri")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Pageviews)
                      .HasColumnName("Pageviews")
                      .HasColumnType("int(11)");

                entity.Property(e => e.Platforms)
                      .HasColumnName("Platforms")
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.Reviewer)
                      .HasColumnName("Reviewer")
                      .HasColumnType("varchar(255)");
            });
        }
    }
}
