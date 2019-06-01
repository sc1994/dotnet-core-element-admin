using System;

namespace ElementAdmin.Application.Model.Tools
{
    /// <summary>
    /// 日志搜索
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// 搜索方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime[] Timestamp { get; set; }
    }
}
