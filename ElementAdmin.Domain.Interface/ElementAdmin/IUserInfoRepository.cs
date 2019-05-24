using System;
using ElementAdmin.Domain.Entity.ElementAdmin;

namespace ElementAdmin.Domain.Interface.ElementAdmin
{
    public interface IUserInfoRepository : IRepository<Int64, UserInfoEntity>
    {

    }
}