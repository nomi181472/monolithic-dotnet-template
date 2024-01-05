using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoResult.Common;
using ResponseModel.Common;
using TemplateAPIServices.IServices;
using TemplateDTOs.UserDTOs;

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
            HttpPost
        ]
        public async Task<ApiResponse> Login([FromBody]LoginUser login) {
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
            HttpPost
        ]

        public async Task<ApiResponse> Registration([FromBody] AddUser user)
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
