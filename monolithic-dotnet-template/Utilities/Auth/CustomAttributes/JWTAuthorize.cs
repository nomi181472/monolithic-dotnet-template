




using System.Web.Http;

namespace Auth.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class JWTAuthorize: AuthorizeAttribute
    {

        /*protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            filterContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent("You are not authorized to access this resource.")
            };
        }*/



    }
}
