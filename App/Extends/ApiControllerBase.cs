using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading;

namespace App
{
    public class ApiControllerBase
    {
        protected readonly IHttpContextAccessor HttpContext;

        //protected readonly UserInfoView UserInfo; todo

        public ApiControllerBase(IHttpContextAccessor context)
        {
            HttpContext = context;
            //HttpContext.HttpContext.Request.Headers
            //UserInfo =  //todo 用户数据初始化
            if (1 != 1) // 权限验证
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.WriteAsync("Authentication failed");
            }
        }

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
