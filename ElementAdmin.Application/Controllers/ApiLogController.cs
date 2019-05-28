using System;
using System.Threading.Tasks;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.ApiLog;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using static ElementAdmin.Application.Model.ApiResponse;

namespace ElementAdmin.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiLogController
    {
        private readonly IConfiguration _config;

        public ApiLogController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// 接口日志搜索
        /// </summary>
        /// <returns></returns>
        [HttpPost("search")]
        public async Task<ApiResponse> SearchApiLog(ApiPageRequest<SearchModel> model)
        {
            var from = (model.Index - 1) * model.Size;
            var data = "{\"from\":" + from + ",\"size\":" + model.Size + ",\"query\":{\"bool\":{\"filter\":[{\"bool\":{\"filter\":{\"term\":{\"messageTemplate.keyword\":\"Invoke({start_timestamp},{tracer_id},{full_method},{method},{parameters},{return_value},{performance},{error})\"}}}}{#MethodName}]}},\"sort\":[{\"fields.start_timestamp\":{\"order\":\"DESC\"}}]}";

            if (!string.IsNullOrWhiteSpace(model.Form.MethodName))
            {
                data = data.Replace("{#MethodName}",
                    ",{\"bool\":{\"filter\":{\"term\":{\"fields.method.keyword\":\"" + model.Form.MethodName + "\"}}}}{#Timestamp}");
            }
            else
            {
                data = data.Replace("{#MethodName}", "{#Timestamp}");
            }

            if ((model.Form.Timestamp?.Any() ?? false) && model.Form.Timestamp.Length == 2)
            {
                data = data.Replace("{#Timestamp}",
                    ",{\"bool\":{\"filter\":{\"range\":{\"fields.start_timestamp\":{\"gte\":\"" + model.Form.Timestamp[0].Ticks + "\"}}}}},{\"bool\":{\"filter\":{\"range\":{\"fields.start_timestamp\":{\"lte\":\"" + model.Form.Timestamp[1].Ticks + "\"}}}}}");
            }
            else
            {
                data = data.Replace("{#Timestamp}", "");
            }

            var @string = await SendDataToEs(data, model.Form);
            return Ok(@string);
        }

        /// <summary>
        /// 子项搜索
        /// </summary>
        /// <param name="tracerId"></param>
        /// <returns></returns>
        [HttpGet("{tracerId}")]
        public async Task<ApiResponse> SearchChildApiLog(string tracerId)
        {
            var data = "{\"query\":{\"bool\":{\"must\":[{\"term\":{\"messageTemplate.keyword\":\"InvokeChild({start_timestamp},{tracer_id},{full_method},{method},{parameters},{return_value},{performance},{error})\"}},{\"term\":{\"fields.tracer_id.keyword\":\"" + tracerId + "\"}}],\"must_not\":[],\"should\":[]}},\"from\":0,\"size\":10,\"sort\":[{\"fields.start_timestamp\":{\"order\":\"ASC\"}}]}";
            var @string = await SendDataToEs(data);
            return Ok(@string);
        }

        /// <summary>
        /// 发送数据到es
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<string> SendDataToEs(string data, SearchModel model = null)
        {
            var index = string.Empty;
            if (model != null && model.Timestamp != null && model.Timestamp.Length == 2)
            {
                var temp = model.Timestamp[1];
                do
                {
                    index += $"logstash-{temp:yyyy.MM.dd},";
                    temp = temp.AddDays(-1);
                }
                while (temp.Date >= model.Timestamp[0]);
            }
            else
            {
                index += $"logstash-{DateTime.Now:yyyy.MM.dd},";
            }
            index = index.Trim(',');
            var baseUrl = _config.GetConnectionString("ElasticsearchConnection");
            var response = await (baseUrl + $"{index}/logevent/_search") // todo 默认查询今天的
                            .WithHeader("Content-Type", "application/json")
                            .PostStringAsync(data);
            return await response.Content.ReadAsStringAsync();
        }
    }
}