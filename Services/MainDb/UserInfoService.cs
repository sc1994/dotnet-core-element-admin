using Models.MainDb;
using Database.MainDb;

namespace Services.MainDb
{
    /// <summary>接口</summary>
    public interface IUserInfoService : IBaseService<UserInfoModel>
    {
    }

    /// <summary>服务</summary>
    public class UserInfoService : BaseService<UserInfoModel>, IUserInfoService
    {
        private readonly IUserInfoStorage _storage;

        /// <summary>服务</summary>
        public UserInfoService(IUserInfoStorage storage) : base(storage)
        {
            _storage = storage;
        }
    }
}