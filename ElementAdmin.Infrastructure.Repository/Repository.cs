using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ElementAdmin.Domain.Entity;
using ElementAdmin.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElementAdmin.Infrastructure.Repository
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {
        private readonly TContext _context;

        protected Repository(TContext context)
        {
            _context = context;
        }


        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return await _context.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        public Task<EntityEntry<TEntity>> RemoveAsync(TEntity entity, bool reallyRemove = false)
        {
            if (reallyRemove)
            {
                return Task.FromResult(_context.Remove(entity));
            }

            entity.IsDelete = true;
            entity.DeleteAt = DateTime.Now; // 软删除
            return Task.FromResult(_context.Update(entity));
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities, bool reallyRemove = false)
        {
            if (reallyRemove)
            {
                return Task.Run(() => _context.RemoveRange(entities));
            }

            var arr = entities as TEntity[] ?? entities.ToArray();

            foreach (var entity in arr)
            {
                entity.IsDelete = true;
                entity.DeleteAt = DateTime.Now;
            }
            return Task.Run(() => _context.UpdateRange(arr));
        }

        public Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(_context.Update(entity));
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            return Task.Run(() => _context.UpdateRange(entities));
        }

        public abstract Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression);

        public abstract Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression);
    }

    public abstract class Repository<TId, TEntity, TContext> : Repository<TEntity, TContext>, IRepository<TId, TEntity>
        where TEntity : Entity<TId>
        where TContext : DbContext
    {
        private readonly TContext _context;

        protected Repository(TContext context) : base(context)
        {
            _context = context;
        }

        public Task RemoveByIdAsync(TId id, bool reallyRemove = false)
        {
            var first = _context.Find<TEntity>(id);
            if (first == default)
            {
                throw new DeletedRowInaccessibleException($"{nameof(id)}={id}");
            }

            if (reallyRemove)
            {
                return Task.Run(() => _context.RemoveRange(first));
            }

            return Task.Run(() => RemoveAsync(first));
        }

        public async Task<TEntity> FindByIdAsync(TId id)
        {
            return await _context.FindAsync<TEntity>(id);
        }
    }
}
