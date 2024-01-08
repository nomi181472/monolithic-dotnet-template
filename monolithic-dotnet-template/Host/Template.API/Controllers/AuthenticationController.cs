using CustomExceptions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoResult.Common;
using ResponseModel.Common;
using TemplateAPIServices.IServices;

using TemplateRequestModel.Auth;
using TemplateRequestModel.User;

namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly ILogger<AuthenticationController> _logger;
        readonly IUserService _userService;
   
        public AuthenticationController(ILogger<AuthenticationController> logger,IUserService userService)
        {

            _logger = logger;
            _userService = userService;

        }
        [
    Route(nameof(Registration)),
    HttpPost
]
        public async Task<ApiResponse> Registration(RequestUserAdd request)
        {
            try
            {
                var result = await _userService.Registration(request);
                return ApiResponseHelper.Convert(true, true, "user is registered", HTTPStatusCode200.Created, result);
            }
            catch (RecordAlreadyExistException ex)
            {
                return ApiResponseHelper.Convert(true, false, ex.Message, HTTPStatusCode400.Conflict, null);
            }
            catch (UnHandledCustomException ex)
            {
                return ApiResponseHelper.Convert(false, false, "something went wrong", HTTPStatusCode500.ServiceUnavailable, null);
            }
            catch (Exception e)
            {

                return ApiResponseHelper.Convert(false, false, "Exception", HTTPStatusCode500.InternalServerError, null);
            }
        }
        [
            Route(nameof(Login)),
            HttpPost
        ]
        public async Task<ApiResponse> Login(RequestUserLogin request)
        {
            try
            {
                var result = await _userService.Login(request);
                return ApiResponseHelper.Convert(true, true, "user is loggedin", HTTPStatusCode200.Ok, result);
            }
            catch (InvalidCredentialsException ex1)
            {
                return ApiResponseHelper.Convert(false, false, ex1.Message, HTTPStatusCode400.Unauthorized, null);
            }
            catch (Exception e)
            {

                return ApiResponseHelper.Convert(false, false, "Exception", HTTPStatusCode500.InternalServerError, null);
            }

        }


    }
}
