using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoResult.Common;
using ResponseModel.Common;
using TemplateAPIServices.IServices;

namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        readonly ILogger<CourseController> _logger;
        readonly ICourseService _courseService;
        public CourseController(ILogger<CourseController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;

        }

        public async Task<ApiResponse> GetCourse(string courseId)
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
