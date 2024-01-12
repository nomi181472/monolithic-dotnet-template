
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Moq;
using RepoResult.Common;
using Template.API.Controllers;
using TemplateAPIDomainModel;
using TemplateAPIServices.IServices;
using TemplateResponseModel.Course;

namespace Template.API.Test
{
    public class CourseControllerTest
    {
        [Fact]
        public async  Task GetCourseList_Success()
        {
            //controller DI
            
           
            var course = new Mock<ICourseService>();
            var logger = new Mock<ILogger<CourseController>>();
            //Arrange
            var controller = new CourseController(logger.Object, course.Object);
            var response=await controller.CourseAdd(new TemplateRequestModel.Course.RequestCourseAdd()
            {
                CourseName = "Noman",
                CourseType = "my type"
            }) ;


            //Act
            
            var list=await controller.GetCourseList();


            //Assert
            Assert.NotNull(list);
            Assert.Equal(list.StatusCode, HTTPStatusCode200.Ok);
        }
    }
}