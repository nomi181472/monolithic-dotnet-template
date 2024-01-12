using CustomExceptions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoResult.Common;
using ResponseModel.Common;
using TemplateAPIServices.IServices;

using TemplateRequestModel.User;
using Auth.IAuthServices;
using TemplateResponseModel.UserResponse;
using Template.API.Helpers;

namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly ILogger<AuthenticationController> _logger;
        readonly IUserService _userService;
        readonly IAuthTokenGenerator _tokenService;
        public AuthenticationController(ILogger<AuthenticationController> logger,IUserService userService,IAuthTokenGenerator tokenGenerator)
        {

            _logger = logger;
            _userService = userService;
            _tokenService = tokenGenerator;

        }
        [
            Route(nameof(Registration)),
            HttpPost
        ]
        public async Task<ApiResponseToken> Registration(RequestUserAdd request)
        {
            try
            {
                var result = await _userService.Registration(request);
                string token = TokenGenerator.GetJWTToken(new TOKENDTO() { Email = result.UserEmail }, _tokenService);

                return ApiResponseHelper.Convert(true, true, "user is registered", HTTPStatusCode200.Created, result, token);
            }
            catch (RecordAlreadyExistException ex)
            {
                return ApiResponseHelper.Convert(true, false, ex.Message, HTTPStatusCode400.Conflict, null,"");
            }
            catch (UnHandledCustomException ex)
            {
                return ApiResponseHelper.Convert(false, false, "something went wrong", HTTPStatusCode500.ServiceUnavailable, null,"");
            }
            catch (Exception e)
            {

                return ApiResponseHelper.Convert(false, false, "Exception", HTTPStatusCode500.InternalServerError, null, "");
            }
        }



        [
            Route(nameof(Login)),
            HttpPost
        ]
        public async Task<ApiResponseToken> Login(RequestUserLogin request)
        {
            try
            {
                var result = await _userService.Login(request);
                string token = TokenGenerator.GetJWTToken(new TOKENDTO() { Email=result.UserEmail}, _tokenService);
                return ApiResponseHelper.Convert(true, true, "user is loggedin", HTTPStatusCode200.Ok, result,token);
            }
            catch (InvalidCredentialsException ex1)
            {
                return ApiResponseHelper.Convert(false, false, ex1.Message, HTTPStatusCode400.Unauthorized, null, null);
            }
            catch (Exception e)
            {
                //logs
                return ApiResponseHelper.Convert(false, false, "Exception", HTTPStatusCode500.InternalServerError, null,null);
            }

        }


    }
}
