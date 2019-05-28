using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ElementAdmin.Domain.Entity.ElementAdmin;
using ElementAdmin.Domain.Interface.ElementAdmin;
using ElementAdmin.Infrastructure.Data.Context;
using Z.EntityFramework.Extensions;

namespace ElementAdmin.Infrastructure.Repository.ElementAdmin
{
    public class RoleRepository : Repository<Int64, RoleEntity, ElementAdminContext>, IRoleRepository
    {
        private readonly ElementAdminContext _context;

        public RoleRepository(ElementAdminContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<RoleEntity>> WhereAsync(Expression<Func<RoleEntity, bool>> expression)
        {
            return await _context.RoleEntity.Where(expression).ToListAsync();
        }

        public override async Task<RoleEntity> FindAsync(Expression<Func<RoleEntity, bool>> expression)
        {
            return await _context.RoleEntity.FirstOrDefaultAsync(expression);
        }

        public override async Task<int> RemoveRangeAsync(Expression<Func<RoleEntity, bool>> expression)
        {
            return await _context.RoleEntity.Where(expression).UpdateFromQueryAsync(x => new RoleEntity
            {
                IsDelete = true,
                DeleteAt = DateTime.Now
            });
        }

        public override async Task<int> UpdateRangeAsync(Expression<Func<RoleEntity, bool>> expression, Expression<Func<RoleEntity, RoleEntity>> updator)
        {
            return await _context.RoleEntity.Where(expression).UpdateFromQueryAsync(updator);
        }
    }
}