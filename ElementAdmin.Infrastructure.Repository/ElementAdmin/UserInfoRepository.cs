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
    public class UserInfoRepository : Repository<Int64, UserInfoEntity, ElementAdminContext>, IUserInfoRepository
    {
        private readonly ElementAdminContext _context;

        public UserInfoRepository(ElementAdminContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<UserInfoEntity>> WhereAsync(Expression<Func<UserInfoEntity, bool>> expression)
        {
            return await _context.UserInfoEntity.Where(expression).ToListAsync();
        }

        public override async Task<UserInfoEntity> FindAsync(Expression<Func<UserInfoEntity, bool>> expression)
        {
            return await _context.UserInfoEntity.FirstOrDefaultAsync(expression);
        }
    }
}