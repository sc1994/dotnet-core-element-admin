using System;
using ElementAdmin.Domain.Entity.ElementAdmin;

namespace ElementAdmin.Domain.Interface.ElementAdmin
{
    public interface IRoleRepository : IRepository<Int64, RoleEntity>
    {
        
    }
}