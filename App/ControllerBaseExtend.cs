using Microsoft.AspNetCore.Mvc;
using Models;

namespace App
{
    public class ControllerBaseExtend : ControllerBase
    {
        protected ResultModel Ok()
            => new ResultModel
            {
                Code = 20000,
                Message = "success"
            };

        protected ResultModel<T> Ok<T>(T result)
            => new ResultModel<T>
            {
                Code = 20000,
                Data = result
            };

        protected ResultModel Bad(string msg)
            => new ResultModel
            {
                Code = 50000,
                Message = msg
            };

        protected ResultModel Bad(int code)
            => new ResultModel
            {
                Code = code
            };
    }
}
