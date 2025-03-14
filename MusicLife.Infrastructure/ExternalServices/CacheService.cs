using Microsoft.Extensions.Caching.Distributed;
using MusicLife.Application.ExternalServices;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System.Text.Json;

namespace MusicLife.Infrastructure.ExternalServices
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connection;
        public CacheService(IConnectionMultiplexer connection, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _connection = connection;
        }
        public async Task<T?> GetCacheAsync<T>(string key)
        {
            var data=await _distributedCache.GetAsync(key);
            return data !=null ? JsonSerializer.Deserialize<T>(data) : default;
        }

        public async Task RemoveCacheAsync<T>(string pattern)
        {
           var server=_connection.GetServer(_connection.GetEndPoints().First());
            foreach(var key in server.Keys(pattern: pattern))
            {
                await _distributedCache.RemoveAsync(key!);
            }
        }

        public async Task SetCacheAsync<T>(string key, T data, TimeSpan? timeOut)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeOut ?? TimeSpan.FromMinutes(30)
            };
            var serializerData =JsonSerializer.Serialize(data);
            await _distributedCache.SetStringAsync(key, serializerData,options);
        }

        public async Task ClearCacheAsync()
        {
            var endpoints = _connection.GetEndPoints(true);
            foreach(var endpoint in endpoints)
            {
                var server=_connection.GetServer(endpoint);
                await server.FlushAllDatabasesAsync();
            }
        }
    }
}
