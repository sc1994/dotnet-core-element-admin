using ElementAdmin.Domain.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ElementAdmin.Infrastructure;

namespace ElementAdmin.Domain.Interface
{
    public interface IRepository<TEntity>
        where TEntity : Entity.Entity
    {
        /// <summary>
        /// 保存变更
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存变更
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 移除（字段标识）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="reallyRemove">实际删除</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> RemoveAsync(TEntity entity, bool reallyRemove = false);

        /// <summary>
        /// 批量移除（字段标识）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="reallyRemove">实际删除</param>
        /// <returns></returns>
        Task RemoveRangeAsync(IEnumerable<TEntity> entities, bool reallyRemove = false);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 筛选查找
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 查找一个
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression);
    }

    public interface IRepository<in TId, TEntity> : IRepository<TEntity>
        where TEntity : Entity<TId>
    {
        /// <summary>
        /// 移除（字段标识）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reallyRemove">实际删除</param>
        /// <returns></returns>
        Task RemoveByIdAsync(TId id, bool reallyRemove = false);

        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(TId id);
    }
}
