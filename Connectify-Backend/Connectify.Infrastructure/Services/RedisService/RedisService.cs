
using Connectify.Application.Interfaces.RedisInterfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Services.RedisService
{
    public class RedisService : Application.Interfaces.RedisInterfaces.IRedis
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }
        public async Task Delete(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

        public async Task FlushAll()
        {
            var endpoints = _redis.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = _redis.GetServer(endpoint);
                await server.FlushAllDatabasesAsync();
            }
        }

        public async Task<T?> Get<T>(string key)
        {
            var cachedData = await _db.StringGetAsync(key);
            if (!cachedData.HasValue)
                return default;

            return JsonConvert.DeserializeObject<T>(cachedData!);
        }

        public async Task Set<T>(string key, T value, TimeSpan expiration)
        {
            var jsonData = JsonConvert.SerializeObject(value, JsonConverterConfigurations.options);
            await _db.StringSetAsync(key, jsonData, expiration);
        }
    }
}
