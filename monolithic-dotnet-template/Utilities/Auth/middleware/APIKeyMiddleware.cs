using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using RepoResult.Common;
using ResponseModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.middleware
{
    public class APIKeyMiddleware
    {
        readonly RequestDelegate _next;
        readonly string _key="key";
        public APIKeyMiddleware(RequestDelegate next)
        {
            _next = next;
            

        }
        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            string key = configuration[_key]!;
            StringValues keyInHeader;
            if (context.Request.Headers.TryGetValue(_key,out keyInHeader) && key.Equals(keyInHeader) || context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }
            context.Response.StatusCode = HTTPStatusCode400.Unauthorized;
            await context.Response.WriteAsJsonAsync(ApiResponseHelper.Convert(true, false, "invalid", HTTPStatusCode400.Unauthorized, null));


        }
    }
    public static class APIKeyMiddlewareExtension
    {
        public static IApplicationBuilder APIKeyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIKeyMiddleware>();
        }
    }
}
