using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;

namespace API.Filters
{
    public class RequestLimitAttribute : ActionFilterAttribute
    {
        public string Name { get; }
        public int NoOfRequest { get; set; }
        public int Seconds { get; set; }
        private static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());
        public RequestLimitAttribute(string name,
            int noOfRequest = 10,
            int seconds = 1)
        {
            Name = name;
            NoOfRequest = noOfRequest;
            Seconds = seconds;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ipAddress = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
            var memoryCacheKey = $"{Name}-{ipAddress}";

            Cache.TryGetValue(memoryCacheKey, out int prevReqCount);
            if (prevReqCount >= NoOfRequest)
            {
                context.Result = new ContentResult
                {
                    Content = $"Request limit is exceeded. Try again in {Seconds} seconds.",
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            }
            else
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Seconds));
                Cache.Set(memoryCacheKey, (prevReqCount + 1), cacheEntryOptions);
            }
        }
    }
}
