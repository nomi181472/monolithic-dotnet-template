using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateRequestModel.Course;
using TemplateResponseModel.Course;

namespace TemplateAPIServices.IServices
{
    public interface ICourseService
    {
        Task<ResponseCourseList> List();
        Task<ResponseCourseGet> GetById(string courseId);
        Task<bool> DeleteById(string courseId,string userId);
        Task<bool> Add(RequestCourseAdd request, string UserId);
        Task<bool> Update(RequestCourseUpdate request,string courseId,string userId);

    }
}
