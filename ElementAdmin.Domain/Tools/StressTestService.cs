using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using ElementAdmin.Application.Model;
using ElementAdmin.Application.Model.Tools;
using ElementAdmin.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using static ElementAdmin.Application.Model.ApiResponse;
using Flurl.Http;
using System;
using System.Net.Http;

namespace ElementAdmin.Domain.Tools
{
    public partial class ToolService
    {
        public async Task<ApiResponse> StartStressTestAsync(StressTestModel model)
        {
            if (!model.VerifyStart())
            {
                return Bad(model.VerifyMessage);
            }
            var client = _hubContext.Clients.Client(model.ConnectedId);
            await client.SendAsync("next", "参数验证完毕，建立链接");

            // 创建线程
            var threads = new List<StressTestItem>();
            for (var i = 0; i < model.Thread; i++)
            {
                threads.Add(new StressTestItem
                {
                    Url = model.GetUrl(),
                    Method = model.Method,
                    Headers = model.GetHeaders(),
                });
            }

            var cts = new CancellationTokenSource();
            if (!StressTestStore.Content.TryAdd(model.ConnectedId, cts))
            {
                return Bad("创建线程失败");
            }

            var count = new ConcurrentBag<int>();
            await client.SendAsync("next", "初始化线程完毕");

            var first = threads.First();
            first.Sending();
            await first.SendedAsync();
            var firstResult = await first.ResultAsync();
            await client.SendAsync("preheat", "预热完毕", firstResult);

            var parallel = Parallel.ForEach(
                       threads,
                       new ParallelOptions
                       {
                           CancellationToken = cts.Token
                       },
                       async item =>
                       {
                           for (var i = 0; i < model.Cycle; i++)
                           {
                               try
                               {
                                   item.Sending();
                                   await client.SendAsync("sending", i, item.Key, item);
                                   await item.SendedAsync();
                                   await client.SendAsync("sended", i, item.Key);
                                   var result = await item.ResultAsync();
                                   await client.SendAsync("result", i, item.Key, result);
                               }
                               catch (Exception ex)
                               {
                                   await client.SendAsync("error", i, item.Key, ex);
                               }
                               count.Add(1);
                               if (count.Count >= model.Cycle * model.Thread)
                               {
                                   await client.SendAsync("over");
                                   StressTestStore.Content.TryRemove(model.ConnectedId, out _);
                               }
                               await Task.Delay(model.Delay);
                           }
                       });

            return Ok();
        }

        public ApiResponse AbortStressTest(string connectionId)
        {
            // if (StressTestStore.Content.TryGetValue(connectionId, out var cts))
            // {
            //     if (cts != null)
            //     {
            //         cts.Cancel();
            //         return Ok();
            //     }
            // }
            // return Bad("取消失败");
            throw new System.NotImplementedException();
        }
    }

    class StressTestItem
    {
        public Guid Key { get; } = Guid.NewGuid();
        public string Url { get; set; }
        public string Method { get; set; }
        public Dictionary<string, object> Headers { get; set; }
        public string Body { get; set; }
        public string BodyJson { get; set; }

        private Task<HttpResponseMessage> _sending;
        /// <summary>
        /// 发送
        /// </summary>
        public void Sending()
        {
            var @string = Body ?? BodyJson;
            IFlurlRequest headers = null;
            if (Headers?.Any() ?? false)
            {
                foreach (var item in Headers)
                {
                    if (headers == null)
                    {
                        headers = Url.WithHeader(item.Key, item.Value);
                    }
                    else
                    {
                        headers = headers.WithHeader(item.Key, item.Value);
                    }
                }
            }
            if (Method == "post")
            {
                _sending = Url
                .PostStringAsync(@string);
            }
            else if (Method == "put")
            {
                _sending = Url
                .PutStringAsync(@string);
            }
            else
            {
                _sending = Url
                .GetAsync();
            }
        }

        private HttpResponseMessage _sended;
        /// <summary>
        /// 发送结束
        /// </summary>
        public async Task SendedAsync()
        {
            _sended = await _sending;
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        public async Task<string> ResultAsync()
        {
            return await _sended.Content.ReadAsStringAsync();
        }
    }

    static class StressTestStore
    {
        public static ConcurrentDictionary<string, CancellationTokenSource> Content { get; } = new ConcurrentDictionary<string, CancellationTokenSource>();
    }
}