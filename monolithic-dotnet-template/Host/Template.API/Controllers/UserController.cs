using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoResult.Common;
using ResponseModel.Common;
using TemplateAPIServices.IServices;

namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly ILogger<UserController> _logger;
        readonly IUserService _userService;
        public UserController( ILogger<UserController> logger,IUserService userService) {
            _logger = logger;
            _userService = userService;

        }
        public async Task<ApiResponse> GetUser(string userId)
        {
            try
            {
                return ApiResponseHelper.Convert(true, true, "", HTTPStatusCode200.Ok, "");
            }
            catch (Exception e)
            {

                throw;
            }
        }
       
    }
}
