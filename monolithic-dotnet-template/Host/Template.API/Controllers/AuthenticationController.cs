using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoResult.Common;
using ResponseModel.Common;
using TemplateAPIServices.IServices;

using TemplateRequestModel.Auth;

namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly ILogger<AuthenticationController> _logger;
        readonly IUserService _auth;
        public AuthenticationController(ILogger<AuthenticationController> logger,IUserService userService)
        {

            _logger = logger;
            _auth = userService;

        }
        [
             Route(nameof(Login)),
            HttpPost
        ]
        public async Task<ApiResponse> Login([FromBody]RequestLogin login) {
            try
            {
               

                //bool result = await _leagueService.AddUserLeagueAndLeagueConfiguration(request, data.Item1, data.Item2);
                return ApiResponseHelper.Convert(true, true, " ", HTTPStatusCode200.Created, login);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [
            Route(nameof(Registration)),
            HttpPost

        ]

        public async Task<ApiResponse> Registration([FromBody] RequestRegistration user)
        {
            try
            {

                return ApiResponseHelper.Convert(true,true,"", HTTPStatusCode200.Created, user);
            }
            catch (Exception e)
            {

                throw e;
            }

        }


    }
}
