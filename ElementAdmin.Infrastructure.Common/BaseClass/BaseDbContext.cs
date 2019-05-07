using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElementAdmin
{
    /// <summary>数据库上下文基类</summary>
    public class BaseDbContext<TContext> : DbContext
        where TContext : DbContext
    {
        /// <summary>数据库上下文</summary>
        protected readonly DbContextOptions<TContext> Options;

        /// <summary>空参构</summary>
        public BaseDbContext()
        { }

        /// <summary>设置数据库</summary>
        public BaseDbContext(DbContextOptions<TContext> options)
            : base(options)
        {
            Options = options;
        }
    }

    /// <summary>数据基类接口</summary>
    public interface IBaseStorage<TModel>
        where TModel : class
    {
        /// <summary>新增</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(bool done, TModel after)> AddAsync(TModel model);

        /// <summary>批量新增</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(IEnumerable<TModel> list);

        /// <summary>删除</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> RemoveAsync(TModel model);

        /// <summary>批量删除</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> RemoveRangeAsync(IEnumerable<TModel> list);

        /// <summary>更新</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(bool done, TModel after)> UpdateAsync(TModel model);

        /// <summary>批量更新</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> UpdateRangeAsync(IEnumerable<TModel> list);

        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        Task<TModel> FirstOrDefaultAsync(Expression<Func<TModel, bool>> predicate);

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<List<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate, int index = 0, int size = 0);

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<List<TModel>> FindAsync(TModel predicate, int index = 0, int size = 0);
    }

    /// <summary>数据基类</summary>
    public class BaseStorage<TContext, TModel> : IBaseStorage<TModel>
        where TContext : DbContext, new()
        where TModel : class
    {
        /// <summary>新增</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool done, TModel after)> AddAsync(TModel model)
        {
            using (var context = new TContext())
            {
                var after = await context.AddAsync(model);
                var change = await context.SaveChangesAsync();
                return (change > 0, after.Entity);
            }
        }

        /// <summary>批量新增</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> AddRangeAsync(IEnumerable<TModel> list)
        {
            using (var context = new TContext())
            {
                await context.AddRangeAsync(list);
                var change = await context.SaveChangesAsync();
                return change;
            }
        }

        /// <summary>删除</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(TModel model)
        {
            using (var context = new TContext())
            {
                context.Remove(model);
                return await context.SaveChangesAsync();
            }
        }

        /// <summary>批量删除</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> RemoveRangeAsync(IEnumerable<TModel> list)
        {
            using (var context = new TContext())
            {
                context.RemoveRange(list);
                return await context.SaveChangesAsync();
            }
        }

        /// <summary>更新</summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool done, TModel after)> UpdateAsync(TModel model)
        {
            using (var context = new TContext())
            {
                var after = context.Update(model);
                var change = await context.SaveChangesAsync();
                return (change > 0, after.Entity);
            }
        }

        /// <summary>批量更新</summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> UpdateRangeAsync(IEnumerable<TModel> list)
        {
            using (var context = new TContext())
            {
                context.UpdateRange(list);
                var change = await context.SaveChangesAsync();
                return change;
            }
        }

        /// <summary>单个查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <returns></returns>
        public virtual Task<TModel> FirstOrDefaultAsync(Expression<Func<TModel, bool>> predicate)
            => throw new NotImplementedException();

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public virtual Task<List<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate, int index = 0, int size = 0)
            => throw new NotImplementedException();

        /// <summary>简单查询</summary>
        /// <param name="predicate">搜索条件</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public virtual Task<List<TModel>> FindAsync(TModel predicate, int index = 0, int size = 0)
            => throw new NotImplementedException();
    }
}

