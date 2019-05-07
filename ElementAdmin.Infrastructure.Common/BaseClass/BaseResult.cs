namespace ElementAdmin.Infrastructure.Common.BaseClass
{
    public class BaseResult
    {
        public bool Done { get; set; }
        public string Message { get; set; }
    }

    public class BaseResult<T> : BaseResult
    {
        public T Result { get; set; } = default;
    }
}
