using System.Collections.Generic;

namespace Models
{
    public class ResultModel
    {
        public int Code { get; set; }
        public string Data { get; set; }
    }

    public class ResultModel<T> : ResultModel
    {
        public new T Data { get; set; }
    }

    public class PageModel<T>
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
