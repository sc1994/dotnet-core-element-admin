using System;
using System.Threading.Tasks;
using ElementAdmin.Infrastructure.Attributes;

namespace ElementAdmin.Infrastructure.Redis
{
    [Logger]
    public interface IRedisClient : IDisposable
    {
        /// <summary>
        /// set string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expriy"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expriy = null);
        /// <summary>
        /// get string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> StringGetAsync<T>(string key);
        /// <summary>
        /// delete key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> KeyDelete(string key);
    }
}
