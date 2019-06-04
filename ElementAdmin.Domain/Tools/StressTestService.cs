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
using ElementAdmin.Infrastructure;

namespace ElementAdmin.Domain.Tools
{
    public partial class ToolService
    {
        public async Task<ApiResponse> StartStressTestAsync(StressTestModel model)
        {
            var client = _hubContext.Clients.Client(model.ConnectedId);
            if (!model.VerifyStart())
            {
                await client.SendAsync("next", "参数验证失败", "error");
                return Bad(model.VerifyMessage);
            }

            await client.SendAsync("next", "参数验证完毕");

            // 创建线程
            var threads = new List<StressTestItem>();
            for (var i = 0; i < model.Thread; i++)
            {
                threads.Add(new StressTestItem
                {
                    Url = model.GetUrl(),
                    Method = model.Method,
                    Headers = model.GetHeaders(),
                    Body = model.GetBody().ToJson(),
                    BodyJson = model.GetBodyJson(),
                });
            }

            var cts = new CancellationTokenSource();
            if (!StressTestStore.Content.TryAdd(model.ConnectedId, cts))
            {
                await client.SendAsync("next", "初始化线程失败", "error");
                return Bad("初始化线程失败");
            }

            var count = new ConcurrentBag<int>();
            await client.SendAsync("next", "初始化线程完毕");

            try
            {
                var first = threads.First();
                first.Sending();
                var firstResult = await first.ResultAsync();
                if (model.AssertResponse(firstResult))
                {
                    await client.SendAsync("next", "预热完毕");
                }
                else
                {
                    StressTestStore.Content.TryRemove(model.ConnectedId, out _);
                    await client.SendAsync("next", "预热成功，但是断言失败", "error");
                    return Bad("预热成功，但断言失败");
                }
            }
            catch (Exception ex)
            {
                // todo 更多信息
                StressTestStore.Content.TryRemove(model.ConnectedId, out _);
                await client.SendAsync("next", "预热失败", "error", ex);
                return Bad("预热失败");
            }

            Parallel.ForEach(
                       threads,
                       new ParallelOptions
                       {
                           CancellationToken = cts.Token
                       },
                       async item =>
                       {
                           for (var i = 0; i < model.Cycle; i++)
                           {
                               var time = DateTime.Now;
                               try
                               {
                                   item.Sending();
                                   await client.SendAsync("sending", i, item.Key); // todo 内容
                                   var result = await item.ResultAsync();
                                   if (model.AssertResponse(result))
                                   {
                                       await client.SendAsync("result", i, item.Key, (DateTime.Now - time).TotalMilliseconds); // todo 内容
                                   }
                                   else
                                   {
                                       await client.SendAsync("assertError", i, item.Key, (DateTime.Now - time).TotalMilliseconds); // todo 内容
                                   }
                               }
                               catch
                               {
                                   await client.SendAsync("error", i, item.Key, (DateTime.Now - time).TotalMilliseconds); // todo 内容
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
                headers = Url.WithHeaders(Headers.ToDictionary(x => x.Key, x => x.Value));
                headers = headers.WithTimeout(DateTime.Now.AddSeconds(30) - DateTime.Now);
            }
            else
            {
                headers = Url.WithTimeout(DateTime.Now.AddSeconds(30) - DateTime.Now);
            }
            if (Method == "post")
            {
                _sending = headers
                .PostStringAsync(@string);
            }
            else if (Method == "put")
            {
                _sending = headers
                .PutStringAsync(@string);
            }
            else
            {
                _sending = headers
                .GetAsync();
            }
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        public async Task<string> ResultAsync()
        {
            var sended = await _sending;
            return await sended.Content.ReadAsStringAsync();
        }
    }

    static class StressTestStore
    {
        public static ConcurrentDictionary<string, CancellationTokenSource> Content { get; } = new ConcurrentDictionary<string, CancellationTokenSource>();
    }
}