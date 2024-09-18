using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RedisInterfaces
{
    public interface IRedis
    {
        Task<T?> Get<T>(string key);
        Task Delete(string key);
        Task Set<T>(string key, T value, TimeSpan expiration);
        Task FlushAll();
    }
}
