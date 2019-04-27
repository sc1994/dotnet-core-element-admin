using Models.MainDb;

namespace Database.MainDb
{
    /// <summary>接口</summary>
    public interface IUserInfoStorage : IBaseStorage<UserInfoModel>
    {
    }

    /// <summary></summary>
    public partial class UserInfoStorage : IUserInfoStorage
    {
    }
}
