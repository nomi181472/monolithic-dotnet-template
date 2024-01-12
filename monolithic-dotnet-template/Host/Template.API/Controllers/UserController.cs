
using Auth.CustomAttributes;
using CustomExceptions.Common;
using Microsoft.AspNetCore.Mvc;
using RepoResult.Common;
using ResponseModel.Common;

using TemplateAPIServices.IServices;
using TemplateRequestModel.User;


namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTAuthorize]
    public class UserController : ControllerBase
    {
        readonly ILogger<UserController> _logger;
        readonly IUserService _userService;
        public UserController( ILogger<UserController> logger,IUserService userService) {
            _logger = logger;
            _userService = userService;

        }
        [
            Route(nameof(GetUser)+"/{userId}"),
            HttpGet
        ]

        public async Task<ApiResponse> GetUser(string userId)
        {
            try
            {

                var result=await _userService.GetUserById(userId);
                return ApiResponseHelper.Convert(true, true, "", HTTPStatusCode200.Ok, result);
            }
            catch(UnauthorizedAccessException ex)
            {
                return ApiResponseHelper.Convert(true, false, ex.Message, HTTPStatusCode400.Unauthorized, null);
            }
            catch(RecordNotFoundException ex)
            {
                return ApiResponseHelper.Convert(true, false, ex.Message, HTTPStatusCode400.NotFound, null);
            }
            catch (Exception e)
            {

                return ApiResponseHelper.Convert(false, false, "Exception", HTTPStatusCode500.InternalServerError, null);
            }
        }

        [
            Route(nameof(ChangeUserName)),
            HttpPatch
        ]
        public async Task<ApiResponse> ChangeUserName(RequestUserChangeUserName request)
        {
            try
            {
                string userId = "e1cbbafc-1d1b-412c-a144-751c18fcc934";
                var result = await _userService.ChangeUserName(request, userId);
                return ApiResponseHelper.Convert(true, true, "user name is changed", HTTPStatusCode200.Ok,result);
            }
            catch (Exception ex)
            {

                return ApiResponseHelper.Convert(false, false, "Exception", HTTPStatusCode500.InternalServerError, null);
            }
        }

       
    }
}
