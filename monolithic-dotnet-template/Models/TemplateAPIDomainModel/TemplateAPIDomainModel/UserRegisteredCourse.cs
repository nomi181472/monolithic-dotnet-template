using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateAPIDomainModel
{
    public class UserRegisteredCourse:Base<string>
    {
        public required string UserId { get; set; } 
        public virtual User User { get; set; }

        public required string CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
