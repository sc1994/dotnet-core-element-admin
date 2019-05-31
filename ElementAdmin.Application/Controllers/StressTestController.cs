using System.Collections.Concurrent;
using System.Collections.Generic;
using System;
using System.Linq;
using ElementAdmin.Application.Model;
using Microsoft.AspNetCore.Mvc;
using static ElementAdmin.Application.Model.ApiResponse;
using Flurl.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ElementAdmin.Application.Hubs;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace ElementAdmin.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StressTestController
    {
        private readonly IHubContext<StressTestHub> _hubContext;
        private readonly ILogger _log;


        public StressTestController(IHubContext<StressTestHub> hubContext, ILogger log)
        {
            _hubContext = hubContext;
            _log = log;
        }

        [HttpGet]
        public ApiResponse StartStressTest()
        {
            var count = 10;
            var url = "";
            var connectionId = ""; //  调用方传入

            var threadCount = new object[count]; // 创建线程数
            var cts = new CancellationTokenSource();
            if (!StressTestStore.Content.TryAdd(connectionId, cts))
            {
                return Bad("创建失败");
            }

            var thread = Parallel.ForEach(
                threadCount,
                new ParallelOptions
                {
                    CancellationToken = cts.Token
                },
                async _ =>
                {
                    var key = Guid.NewGuid();
                    var client = _hubContext.Clients.Client(connectionId);

                    var request = ""; // 随机动态参数
                    var r = url.PostJsonAsync(request); // 发送请求

                    await client.SendAsync("sending", key, url, request);
                    var result = await r;
                    // result. // read response
                    await client.SendAsync("sended", key, url, result); // todo
                });

            return Ok();
        }

        public ApiResponse AbortStressTest(string connectionId)
        {
            if (StressTestStore.Content.TryGetValue(connectionId, out var cts))
            {
                if (cts != null)
                {
                    cts.Cancel();
                    return Ok();
                }
            }
            return Bad("取消失败");
        }
    }

    static class StressTestStore
    {
        public static ConcurrentDictionary<string, CancellationTokenSource> Content { get; } = new ConcurrentDictionary<string, CancellationTokenSource>();
    }
}