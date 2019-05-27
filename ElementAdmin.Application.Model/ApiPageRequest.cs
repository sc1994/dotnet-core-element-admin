namespace ElementAdmin.Application.Model
{
    public class ApiPageRequest<T>
    {
        /// <summary>
        /// 提交的表单
        /// </summary>
        public T Form { get; set; }

        /// <summary>
        /// index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// size
        /// </summary>
        public int Size { get; set; }
    }
}
