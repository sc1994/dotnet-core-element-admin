using StackExchange.Redis;

namespace ElementAdmin.Infrastructure.Redis
{
    public class RedisClient
    {
        private const string _redisSite = "118.24.27.231:6379,password=sun940622";
        private const int _dbIndex = 1;

        private readonly IDatabase _db;

        public RedisClient()
        {
            _db = ConnectionMultiplexer.ConnectAsync("118.24.27.231:6379,password=sun940622").Result.GetDatabase(10);
        }
    }
}
