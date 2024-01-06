using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateResponseModel.Course
{
    public class ResponseCourseList
    {
        public required List<ResponseCourseGet> Courses { get; set; }   
    }
}
