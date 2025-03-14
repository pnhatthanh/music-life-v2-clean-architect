using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Utils
{
    public class CacheUtil
    {
        public static string GenerateKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"CACHE_{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(q => q.Key))
            {
                keyBuilder.Append($"-{key}_{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
