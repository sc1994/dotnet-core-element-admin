using ElementAdmin.Domain.Entities.ElementAdminDb;

namespace ElementAdmin.Infrastructure.Repositories.ElementAdminDb
{
    /// <summary>用户信息表接口</summary>
    public interface IUserInfoStorage : IBaseStorage<UserInfoEntity>
    {
    }

    /// <summary>用户信息表</summary>
    public partial class UserInfoStorage : IUserInfoStorage
    {
    }
}
