using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MusicLife.Application.ExternalServices;
using MusicLife.Application.Utils;
using MusicLife.Infrastructure.Configurations;
using System.Text.Json;

namespace MusicLife.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLive;
        public CacheAttribute(int timeToLive)
        {
            _timeToLive = timeToLive;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService=context.HttpContext.RequestServices.GetService<ICacheService>();
            var cacheConfig = context.HttpContext.RequestServices.GetService<IOptions<RedisSetting>>();
            var cacheKey = CacheUtil.GenerateKey(context.HttpContext.Request);
            if (cacheConfig !=null && !cacheConfig.Value.Enable)
            {
                await next();
                return;
            }
            var data = await cacheService!.GetCacheAsync<object>(cacheKey);
            if(data==null)
            {
                var excutedResult=await next();
                if (excutedResult.Result is OkObjectResult okObject)
                    await cacheService.SetCacheAsync(cacheKey, okObject, TimeSpan.FromMinutes(_timeToLive));
                return;
            }
            var result = new ContentResult
            {
                Content = JsonSerializer.Serialize(data),
                ContentType = "application/json",
                StatusCode = 200
            };
            context.Result = result;
        }
    }
}
