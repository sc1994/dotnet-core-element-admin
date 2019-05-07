using ElementAdmin.Domain.Entities.MainDb;

namespace ElementAdmin.Infrastructure.Repositories.MainDb
{
    /// <summary>接口</summary>
    public interface IUserInfoStorage : IBaseStorage<UserInfoEntity>
    {
    }

    /// <summary></summary>
    public partial class UserInfoStorage : IUserInfoStorage
    {
    }
}
