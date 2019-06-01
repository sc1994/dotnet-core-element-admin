using System;

namespace ElementAdmin.Application.Model.Identity
{
    /// <summary>
    /// 搜索用户
    /// </summary>
    public class SearchUserModel
    {
        /// <summary>
        /// 昵称或者用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}