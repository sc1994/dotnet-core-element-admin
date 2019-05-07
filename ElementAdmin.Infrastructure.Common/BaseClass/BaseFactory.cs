namespace ElementAdmin.Infrastructure.Common.BaseClass
{
    public class BaseFactory
    {
        public BaseResult Ok() => new BaseResult
        {
            Done = true,
            Message = default
        };

        public BaseResult<T> Ok<T>(T result) => new BaseResult<T>
        {
            Done = true,
            Message = default,
            Result = result
        };

        public BaseResult Bad(string message) => new BaseResult
        {
            Done = false,
            Message = message
        };

        public BaseResult<T> Bad<T>(string message) => new BaseResult<T>
        {
            Done = false,
            Message = message,
            Result = default
        };
    }
}