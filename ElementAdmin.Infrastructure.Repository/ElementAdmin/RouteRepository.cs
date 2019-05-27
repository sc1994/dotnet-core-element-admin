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
    public class RouteRepository : Repository<Int64, RouteEntity, ElementAdminContext>, IRouteRepository
    {
        private readonly ElementAdminContext _context;

        public RouteRepository(ElementAdminContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<RouteEntity>> WhereAsync(Expression<Func<RouteEntity, bool>> expression)
        {
            return await _context.RouteEntity.Where(expression).ToListAsync();
        }

        public override async Task<RouteEntity> FindAsync(Expression<Func<RouteEntity, bool>> expression)
        {
            return await _context.RouteEntity.FirstOrDefaultAsync(expression);
        }
    }
}