using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace ElementAdmin.Application.Hubs
{
    public interface IStressTestHub
    {
        Task GetConnectedId(string connectedId);
    }

    public class StressTestHub : Hub<IStressTestHub>
    {
        private readonly IHttpContextAccessor _http;

        public StressTestHub(IHttpContextAccessor http)
        {
            _http = http;
        }

        public override async Task OnConnectedAsync()
        {
            var id = _http.HttpContext.Request.QueryString.Value.Replace("?id=", "");
            await Clients.Client(id).GetConnectedId(id);
        }

        // public override async Task OnDisconnectedAsync(Exception exception)
        // {

        // }
    }
}