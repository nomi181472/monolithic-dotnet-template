using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateRequestModel.Course
{
    public class RequestCourseAdd
    {
        public required string CourseName { get; set; }
        public required string CourseType { get; set; }
    }
}
