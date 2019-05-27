using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ElementAdmin.Domain.Entity.ElementAdmin;
using ElementAdmin.Domain.Interface.ElementAdmin;
using ElementAdmin.Infrastructure.Data.Context;

namespace ElementAdmin.Infrastructure.Repository.ElementAdmin
{
    public class RoleRouteRepository : Repository<Int64, RoleRouteEntity, ElementAdminContext>, IRoleRouteRepository
    {
        private readonly ElementAdminContext _context;

        public RoleRouteRepository(ElementAdminContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<RoleRouteEntity>> WhereAsync(Expression<Func<RoleRouteEntity, bool>> expression)
        {
            return await _context.RoleRouteEntity.Where(expression).ToListAsync();
        }

        public override async Task<RoleRouteEntity> FindAsync(Expression<Func<RoleRouteEntity, bool>> expression)
        {
            return await _context.RoleRouteEntity.FirstOrDefaultAsync(expression);
        }
    }
}