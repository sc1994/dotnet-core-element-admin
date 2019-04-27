using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    /// <summary>数据库上下文基类</summary>
    public class BaseContext<TContext> : DbContext
        where TContext : DbContext
    {
        /// <summary>数据库上下文</summary>
        protected readonly DbContextOptions<TContext> Options;

        /// <summary>空参构</summary>
        public BaseContext()
        { }

        /// <summary>设置数据库</summary>
        public BaseContext(DbContextOptions<TContext> options)
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

    /// <summary>Enables the efficient, dynamic composition of query predicates.</summary>
    public static class PredicateBuilder
    {
        /// <summary>Creates a predicate that evaluates to true.</summary>
        public static Expression<Func<T, bool>> True<T>()
            =>  param => true;

        /// <summary>Creates a predicate that evaluates to false.</summary>
        public static Expression<Func<T, bool>> False<T>()
            =>  param => false;

        /// <summary>Creates a predicate expression from the specified lambda expression.</summary>
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate)
            =>  predicate;

        /// <summary>Combines the first predicate with the second using the logical "and".</summary>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
            => first.Compose(second, Expression.AndAlso);

        /// <summary>Combines the first predicate with the second using the logical "or".</summary>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
            =>  first.Compose(second, Expression.OrElse);

        /// <summary>Negates the predicate.</summary>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }

        /// <summary>Combines the first expression with the second using the specified merge function.</summary>
        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }

    /// <summary></summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        /// <summary></summary>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary></summary>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            => new ParameterRebinder(map).Visit(exp);

        /// <summary></summary>
        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (_map.TryGetValue(p, out var replacement))
                p = replacement;
            return base.VisitParameter(p);
        }
    }
}