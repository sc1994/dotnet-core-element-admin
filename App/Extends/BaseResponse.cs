using System.Collections.Generic;

namespace App
{
    public class BaseResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class Response<T> : BaseResponse
    {
        public T Data { get; set; }
    }

    public class Page<T> : BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
