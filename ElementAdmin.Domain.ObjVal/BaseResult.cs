using Newtonsoft.Json;
using System.Collections.Generic;

namespace ElementAdmin.Domain.ObjVal
{
    public class BaseResult
    {
        protected Result Ok()
        {
            return new Result
            {
                Code = ResultCodeEnum.成功
            };
        }

        protected Result Bad(string msg)
        {
            return new Result
            {
                Code = ResultCodeEnum.成功,
                Message = msg
            };
        }

        protected Result<T> Bad<T>(string msg)
        {
            return new Result<T>
            {
                Code = ResultCodeEnum.成功,
                Message = msg
            };
        }

        protected Result<T> Ok<T>(T data)
        {
            return new Result<T>
            {
                Code = ResultCodeEnum.成功,
                Data = data
            };
        }
    }

    public class Result
    {
        public ResultCodeEnum Code { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }

        public bool IsDone()
        {
            return Code == ResultCodeEnum.成功;
        }
    }

    public class Result<T> : Result
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public T Data { get; set; }
    }

    public class Page<T> : Result
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }

    public enum ResultCodeEnum
    {
        成功 = 20000,
        失败 = 50000
    }
}
