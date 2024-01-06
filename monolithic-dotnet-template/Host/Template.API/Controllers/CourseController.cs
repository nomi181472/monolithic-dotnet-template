using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoResult.Common;
using ResponseModel.Common;
using TemplateAPIServices.IServices;
using TemplateRequestModel.Course;
using TemplateResponseModel.Course;

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

        [
            Route($"{nameof(GetCourse)}" + "/{courseId}"),
            HttpGet
        ]
        public async Task<ApiResponse> GetCourse(string courseId)
        {
            try
            {
               var result=await  _courseService.GetById(courseId);
                return ApiResponseHelper.Convert(true, true, $"Success", HTTPStatusCode200.Ok, result);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [
            Route(nameof(CourseAdd)),
            HttpPost
            
        ]
        public async Task<ApiResponse> CourseAdd([FromBody]RequestCourseAdd request)
        {
            try
            {
                var userId = "";
                bool result = await _courseService.Add(request, userId);
                return ApiResponseHelper.Convert(true, true,  "Success", HTTPStatusCode200.Ok, result);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [
            Route(nameof(GetCourseList)),
            HttpGet
        ]
        public async Task<ApiResponse> GetCourseList()
        {
            try
            {
                List<ResponseCourseList> courses = new List<ResponseCourseList>();
                return ApiResponseHelper.Convert(true, true, "", HTTPStatusCode200.Ok, courses);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [
            Route($"{nameof(UpdateCourse)}"+ "/{courseId}"),
            HttpPut
        ]

        public async Task<ApiResponse> UpdateCourse([FromBody] RequestCourseUpdate request,string courseId)
        {
            try
            {

                bool result = true;
                var userId = "";
                await _courseService.Update(request,courseId,userId);
                return ApiResponseHelper.Convert(true, true, "Success", HTTPStatusCode200.Ok, result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [ 
            Route($"{nameof(GetCourseList)}"+ "/{courseId}"),
            HttpDelete
            
        ]
        public async Task<ApiResponse> CourseDelete([FromRoute]string courseId)
        {
            try
            {
               
                string userId = "";
                var result=await _courseService.DeleteById(courseId, userId);
                return ApiResponseHelper.Convert(true, true, "", HTTPStatusCode200.Ok, result);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
