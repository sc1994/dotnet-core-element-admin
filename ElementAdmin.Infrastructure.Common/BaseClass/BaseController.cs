namespace ElementAdmin.Infrastructure.Common
{
    public class BaseController
    {
        protected Response<string> Ok()
            => new Response<string>
            {
                Code = 20000,
                Message = "success"
            };

        protected Response<string> Ok(string msg)
            => new Response<string>
            {
                Code = 20000,
                Message = msg
            };

        protected Response<T> Ok<T>(T result)
            => new Response<T>
            {
                Code = 20000,
                Data = result
            };

        protected Response<string> Bad(string msg)
            => new Response<string>
            {
                Code = 50000,
                Message = msg
            };

        protected BaseResponse Bad(int code)
            => new BaseResponse
            {
                Code = code
            };
    }
}
