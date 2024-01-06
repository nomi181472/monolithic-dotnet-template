using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateResponseModel.Course
{
    public class ResponseCourseGet
    {
        public required string CourseId { get; set; }   
        public required string CourseName { get; set; }
        public required string CourseType { get; set; }
        public required string CreatedBy { get; set; }  
        public required string UpdatedBy { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime UpdateDate { get; set; }
    }
}
