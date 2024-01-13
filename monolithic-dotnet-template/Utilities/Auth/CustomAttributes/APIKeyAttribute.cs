
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using RepoResult.Common;
using ResponseModel.Common;



namespace Auth.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class APIKeyAttribute : Attribute, IAuthorizationFilter
    {
        readonly IConfiguration _configuration;
        readonly string _key = "Key";
        public APIKeyAttribute(IConfiguration configuration)
        {
            _configuration = configuration;   
        }
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            string key = _configuration[_key]!;
            StringValues keyInHeader;
            var context = filterContext.HttpContext;
            if (context.Request.Headers.TryGetValue(_key, out keyInHeader) && key.Equals(keyInHeader) || context.Request.Path.StartsWithSegments("/swagger"))
            {
               
                return;
            }
            context.Response.StatusCode = HTTPStatusCode400.Unauthorized;
            filterContext.Result = new ObjectResult(ApiResponseHelper.Convert(true, false, "invalid", HTTPStatusCode400.Unauthorized, null));


        }
    }
}
