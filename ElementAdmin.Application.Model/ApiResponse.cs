using Newtonsoft.Json;

namespace ElementAdmin.Application.Model
{
    /// <summary>
    /// 响应结果
    /// </summary>
    public class ApiResponse
    {
        public ResponseCodeEnum Code { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }

        public static ApiResponse Bad(string msg)
        {
            return new ApiResponse
            {
                Code = ResponseCodeEnum.失败,
                Message = msg
            };
        }

        public static ApiResponse Ok()
        {
            return new ApiResponse
            {
                Code = ResponseCodeEnum.成功
            };
        }

        public static ApiResponse<T> Ok<T>(T data)
        {
            return new ApiResponse<T>
            {
                Code = ResponseCodeEnum.成功,
                Data = data
            };
        }
    }

    /// <summary>
    /// 响应结果
    /// </summary>
    /// <typeparam name="T">data</typeparam>
    public class ApiResponse<T> : ApiResponse
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public T Data { get; set; }
    }

    /// <summary>
    /// 相应代码枚举
    /// </summary>
    public enum ResponseCodeEnum
    {
        成功 = 20000,
        失败 = 50000
    }
}