using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateAPIDomainModel
{
    public class Course:Base<string>
    {
        public required string CourseName { get; set; }
        public required string CourseType { get; set; }
        public ICollection<UserRegisteredCourse> UserRegisteredCourses { get; set; }
    }
}
