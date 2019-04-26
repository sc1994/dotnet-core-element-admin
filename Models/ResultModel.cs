using System.Collections.Generic;

namespace Models
{
    public class ResultModel<T>
    {
        public int Code { get; set; }
        public T Data { get; set; }
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
