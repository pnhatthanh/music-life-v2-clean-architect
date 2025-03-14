using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.ExternalServices
{
    public interface ICacheService
    {
        Task<T?> GetCacheAsync<T>(string key);
        Task SetCacheAsync<T>(string key, T value, TimeSpan? timeOut);
        Task RemoveCacheAsync<T>(string key);
        Task ClearCacheAsync();
    }
}
