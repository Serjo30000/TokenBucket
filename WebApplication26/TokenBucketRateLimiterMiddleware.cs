using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication26
{
    public class TokenBucketRateLimiterMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenBucketRateLimiterMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context, TokenBucket bucket)
        {
            try
            {
                bucket.UseToken();
                await _next(context);
            }
            catch (NoTokensAvailableException)
            {
                context.Response.StatusCode = 503;
            }
        }
    }
}
