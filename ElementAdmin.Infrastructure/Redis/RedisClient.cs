using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ElementAdmin.Infrastructure.Redis
{
    public class RedisClient : IRedisClient
    {
        private const string _redisSite = "localhost:6379,password=1qaz2wsx3edc";
        private readonly IConnectionMultiplexer _connection;

        public RedisClient()
        {
            _connection = ConnectionMultiplexer.Connect(_redisSite);
        }

        public async Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expriy = null)
        {
            return await _connection.GetDatabase().StringSetAsync(key, JsonConvert.SerializeObject(value), expriy);
        }

        public async Task<T> StringGetAsync<T>(string key)
        {
            var value = await _connection.GetDatabase().StringGetAsync(key);
            if (value == default) return default;
            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<bool> KeyDelete(string key)
        {
            return await _connection.GetDatabase().KeyDeleteAsync(key);
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection.Close();
            }
        }
    }
}
