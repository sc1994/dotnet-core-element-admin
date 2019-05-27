namespace ElementAdmin.Infrastructure.Redis.RedisConst
{
    public class UserConst
    {
        public static string IdentityKey(string token) => $"string:identity:{token}";
    }
}
